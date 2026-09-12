using Alpha.Classes;
using Alpha.FunctionForms;
using AcuraLibrary.Forms;
using AsyncAwaitBestPractices;
using NPSDK;
using Keyence.AutoID.SDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha._0.ModuleForms
{
    public partial class ScannerForm : ModuleBaseForm
    {

        #region------------------------------------- Variables


        private string sLastRecipe = "";

        List<Task> lTasksConnect = new List<Task>();

        Dictionary<string, ReaderAccessor> lScanner = new Dictionary<string, ReaderAccessor>();

        private Task<string> tChangeScannerProgram = null;

        private cSmartAlarm cAlarm = SmartAlarm.cAlarm;

        public DialogResult dResult = DialogResult.None;


        private int iNoAutomaticRetries = 0;

        public string sScannerRecipeName { get { return fnGetScannerRecipeName(); } }
        public bool bModuleEnabled { get { return GetData(HabilitarModuloScanner); } }
        public int iMaxAutomaticRetries { get { return GetData(dcScanners_NumbersOfAutomaticRetries); } }
        public bool bValidateCodeQuality { get { return GetData(dcPSet_ValidateCodeQuality); } }
        public int iCodeQualityCheckNumber { get { return GetData(dcPSet_CodeQualityCheckNumber); } }
        public char cMaxCodeQuality { get { return GetData(dcPSet_MaxCodeQuality); } }

        public class ScannerReadResult
        {
            public bool bStatus { get; set; } = false;
            public string sError { get; set; } = null;
            public bool bForced { get; set; } = false;
            public List<ScannerData> lCodes { get; set; } = new List<ScannerData>();
        }

        public class ScannerData
        {
            public string sCode { get; set; }
            public char cQuality { get; set; }
        }

        #endregion

        public ScannerForm()
        {
            #region TABS
            plMaintenance.Enabled = false;
            plFlowAuto.Enabled = false;
            plFlowInitial.Enabled = true;
            plMachineStatus.Enabled = false;
            plProductionSetting.Enabled = false;
            plMotorControl.Enabled = false;
            plRecipeEditor.Enabled = false;
            plMotionSetup.Enabled = false;
            #endregion TABS

            InitializeComponent();

            #region DataGridView

            dataGridViewComboBoxColumn1.DataSource = SettingData;                   //Se ligan las tablas a el campo de nombre de settings para que muestre los items
            dataGridViewComboBoxColumn1.DisplayMember = "Scanners.Name";

            scannerNameDataGridViewTextBoxColumn.DataSource = SettingData;
            scannerNameDataGridViewTextBoxColumn.DisplayMember = "Scanners.Name";

            programSelectionDataGridViewTextBoxColumn.DataSource = SettingData;
            programSelectionDataGridViewTextBoxColumn.DisplayMember = "PScannerPrograms.ProgramName";


            dataGridView_ScannerPrograms.DataError += new DataGridViewDataErrorEventHandler(dataGridView_ScannerPrograms_DataError);

            foreach (DataGridViewColumn column in dataGridView_ScannerPrograms.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView_ProgramSelection.DataError += new DataGridViewDataErrorEventHandler(dataGridView_ProgramSelection_DataError);

            foreach (DataGridViewColumn column in dataGridView_ProgramSelection.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView_PostProcess.DataError += new DataGridViewDataErrorEventHandler(dataGridView_PostProcess_DataError);

            foreach (DataGridViewColumn column in dataGridView_PostProcess.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            #endregion DataGridView
            #region SmartAlarm
            cAlarm.AddRange(fc_In_Start);
            #endregion SmartAlarm

        }

        #region------------------------------------- Overrides

        public override void InitialReset()
        {
            //fnRemoveUnnecesaryProcesses();
            //fnCrearListaScanners();
            fc_In_Start.TaskReset();
        }

        public override void Initial()
        {
            fc_In_Start.TaskRun();
        }

        public override void ModuleDispose()
        {
            try
            {
                fnBorrarListadeScanners();
                webBrowser_Scanner?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public override void ModuleInitialize(string ModuleName)
        {
            base.ModuleInitialize(ModuleName);
            //fnRemoveUnnecesaryProcesses();
            fnCrearListaScanners();
        }
        public override void AlwaysRun()
        {
            //if(GetResults)
            //{

            //}
        }
        #endregion

        #region------------------------------------- General Functions 

        #region System


        /// <summary>
        /// Return the integer row number from the received table
        /// </summary>
        /// <param name="_DataSet"></param>
        /// <param name="_DataTable"></param>
        /// <param name="_DataColumn"></param>
        /// <param name="sRowName"></param>
        /// <returns></returns>
        private int getTableRowNumberByName(DataColumn _DataColumn, string sRowName)
        {
            DataTable table = _DataColumn.Table;
            int iTotalPrograms = table.Rows.Count;

            for (int i = 0; i < iTotalPrograms; i++)
            {
                DataRow row = table.Rows[i];
                string sName = row[_DataColumn].ToString();

                if (sName == sRowName)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Obtiene el Timeout alacenado en las configuraciones        
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        private int fnGetTimeOut(string _name)
        {
            int _ScannerCount = dtScanners.Rows.Count;

            for (int i = 0; i < _ScannerCount; i++)
            {
                DataRow row = dtScanners.Rows[i];
                string _Name = row[dcScanners_Name].ToString();

                if (_name == _Name)
                    return Convert.ToInt32(row[dcScanners_Timeout]);
            }

            return 0;
        }

        /// <summary>
        /// Retorna el valor de escaneo manual Habilitado / deshabilitado
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        private bool fnIsManualScanMode(string _name)
        {
            int _ScannerCount = RScannerProgram.Rows.Count;

            for (int i = 0; i < _ScannerCount; i++)
            {
                DataRow row = RScannerProgram.Rows[i];
                string _Name = row[ScannerName].ToString();

                if (_name == _Name)
                    return (bool)row[IsManualScan];
            }

            return false;
        }

        /// <summary>
        /// Retorna el valor de serial dummy almacenado para el escaner dado
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        private string fnGetDummySerial(string _name)
        {
            int _ScannerCount = RScannerProgram.Rows.Count;

            for (int i = 0; i < _ScannerCount; i++)
            {
                DataRow row = RScannerProgram.Rows[i];
                string _Name = row[ScannerName].ToString();

                if (_name == _Name)
                    return Convert.ToString(row[DummySerial]);
            }

            return "Dummy";
        }


        /// <summary>
        /// Recibe el nombre del escaner, consulta en la tabla y obtine la ip para 
        /// realizar la conexion con el monitor web
        /// </summary>
        /// <param name="_Scanner"></param>
        private void fnConectarWebMonitor(string _Scanner)
        {
            int _ScannerCount = lScanner.Count;

            for (int i = 0; i < _ScannerCount; i++)
            {
                DataRow row = dtScanners.Rows[i];
                string _Name = row[dcScanners_Name].ToString();

                if (_Scanner == _Name)
                {
                    string _Ip = row[dcScanners_IP].ToString();
                    webBrowser_Scanner.Navigate(_Ip);
                    break;
                }
            }
        }

        /// <summary>
        /// Conecta de manera asyncrona el web browser
        /// </summary>
        /// <param name="_Scanner"></param>
        /// <returns></returns>
        private async Task fnConnectWebBrowserAsync(string _Scanner)
        {
            bool isAvailable = await fnIsWebBrowserAvailable();

            if (isAvailable)
                fnConectarWebMonitor(_Scanner);
        }

        /// <summary>
        /// Validate if it's a webserver on scanner
        /// </summary>
        /// <returns></returns>
        private async Task<bool> fnIsWebBrowserAvailable()
        {
            return await Task.Run(() =>
            {
                // Evaluate current system tcp connections. 
                // We will look through the list, and if our port we would like to use
                // in our TcpClient is occupied, we will set isAvailable to false.
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
                bool isTaken = tcpConnInfoArray.Any(e => e.Port == 80);
                return !isTaken;

            }).ConfigureAwait(false);
        }

        #endregion System

        #region Imagen

        /// <summary>
        /// Retorna el parametro de almacenamiento del escaner recibido
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        private string fnGetSaveImageEnabled(string _name)
        {
            int _ScannerCount = dtScanners.Rows.Count;           // Lee la tabla 

            for (int i = 0; i < _ScannerCount; i++)                                 // Recorre la tabla en busca del nombre
            {
                DataRow row = dtScanners.Rows[i];
                string _Name = row[dcScanners_Name].ToString();

                if (_name == _Name)
                    return Convert.ToString(row[dcScanners_SaveImages]);
            }

            return "";
        }

        /// <summary>
        /// Obtiene la ultima imagen desde la memoria del escaner
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        private async Task<bool> fnSaveLastImage(string _name, string _param)
        {
            ReaderAccessor _Scanner = fn_GetScannerObjectFromList(_name);
            string rootDir = GetData(cDireccion);
            string dateString = DateTime.Now.ToShortDateString().Replace('/', '_').Replace(':', '_').Replace(' ', '_');
            string savePath = Path.Combine(rootDir, _name, dateString);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(Path.Combine(savePath, "Fail"));
                Directory.CreateDirectory(Path.Combine(savePath, "Pass"));
            }

            return await Task.Run(() =>
            {
                if (_Scanner.OpenFtp())
                {
                    if (_param == "Pass")
                        savePath = Path.Combine(savePath, "Pass");
                    else if (_param == "Fail")
                        savePath = Path.Combine(savePath, "Fail");

                    List<string> strImageFile = _Scanner.GetFileList("IMAGE");

                    if (strImageFile.Count > 0)
                    {
                        var filesSorted = new List<string>(strImageFile);
                        filesSorted.Sort();
                        string savingFile = filesSorted.Last();
                        _Scanner.GetFile(Path.Combine("IMAGE", savingFile), Path.Combine(savePath, savingFile));
                        return true;
                    }

                }

                return false;
            }).ConfigureAwait(false);
        }



        #endregion

        #region Scanner 

        /// <summary>
        /// Recibe un indice y retorna el nombre de la tabla
        /// </summary>
        /// <param name="_index"></param>
        /// <returns></returns>
        private string fnGetScannerName(int _index)
        {
            DataRow row = dtScanners.Rows[_index];
            string _name = row[dcScanners_Name].ToString();
            return _name;
        }

        /// <summary>
        /// Obtiene el nombre del scanner seleccionado en Rectea
        /// </summary>
        /// <returns></returns>
        private string fnGetScannerRecipeName()
        {
            DataRow row = RScannerProgram.Rows[0];
            string name = (string)row[ScannerName];
            return name;
        }

        /// <summary>
        /// Recibe el codigo, el proceso y los parametros que se deben realizar
        /// </summary>
        /// <param name="_code"></param>
        /// <param name="_Process"></param>
        /// <param name="_params"></param>
        /// <returns></returns>
        private string fnApplyPostProcess(string _code, string _Process, string _params01, string _params02)
        {
            int _index = 0;
            string _result = _code;

            switch (_Process)
            {
                case "Sustituir":
                    try { _result = (string.IsNullOrEmpty(_params02)) ? _code.Replace(_params01, "") : _code.Replace(_params01, _params02); }
                    catch { _result = "ER_Sus_" + _result; }
                    break;
                case "Subcadena por caracteres":
                    try { _result = (string.IsNullOrEmpty(_params02)) ? _code.Substring(_code.IndexOf(_params01)) : _code.Substring(_code.IndexOf(_params01), _code.IndexOf(_params02) - _code.IndexOf(_params01)); }
                    catch { _result = "ER_SbC_" + _result; }
                    break;
                case "Subcadena por posiciónes":
                    try { _result = (_index == 0) ? _code.Substring(Convert.ToInt32(_params01)) : _code.Substring(Convert.ToInt32(_params01), Convert.ToInt32(_params02) - Convert.ToInt32(_params01)); }
                    catch { _result = "ER_SbP_" + _result; }
                    break;
                case "Agregar prefijo":
                    try { _result = _params01 + _code; }
                    catch { _result = "ER_AP_" + _result; }
                    break;
                case "Agregar sufijo":
                    try { _result = _code + _params01; }
                    catch { _result = "ER_AS_" + _result; }
                    break;
                case "Insertar antes de":
                    try { _result = _code.Insert(_code.IndexOf(_params01), _params02); }
                    catch { _result = "ER_IA_" + _result; }
                    break;
                case "Insertar despues de":
                    try { _result = _code.Insert(_code.IndexOf(_params01) - 1, _params02); }
                    catch { _result = "ER_ID_" + _result; }
                    break;

            }

            return _result;
        }

        /// <summary>
        /// Revisa dentro de la tabla para validar si hay operaciones de post procesado que realizar
        /// Si alguna retorna error, se agrega un "ER" a el resultado y termina el postprocesado
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_code"></param>
        /// <returns></returns>
        private string fnCheckPostProcessTasks(string _name, string _code)
        {
            int _ScannerCount = RPostProcess.Rows.Count;
            string _result = _code;

            for (int i = 0; i < _ScannerCount; i++)
            {
                DataRow row = RPostProcess.Rows[i];
                string _Name = row[NombreEscaner].ToString();

                if (_name == _Name)
                {
                    string _params1 = row[Parametro01].ToString();
                    string _params2 = row[Parametro02].ToString();
                    string _process = row[Proceso].ToString();
                    _result = fnApplyPostProcess(_result, _process, _params1, _params2);

                    if (_result.Contains("ER"))
                        break;
                }
            }
            return _result;
        }

        /// <summary>
        /// Desconecta y limpia la lista de scanners
        /// </summary>
        private void fnBorrarListadeScanners()
        {
            foreach (var scanner in lScanner)
            {
                scanner.Value?.Disconnect();
                scanner.Value?.Dispose();
            }

            lScanner.Clear();
            lTasksConnect.Clear();
        }

        /// <summary>
        /// Toma la ip que recibe y agrega el scanner SR2000 a la lista
        /// Regresa verdadero si se agrega 
        /// Regresa falso si esa Ip ya esta registrada en la lista
        /// Regresa falso si la Ip no tiene el formato correcto
        /// </summary>
        /// <param name="_Ip"></param>
        /// <returns></returns>
        private bool fnAgregarScannerALista(string _Ip, int _Port, string _Name)
        {
            IPAddress tmpIP;

            if (!IPAddress.TryParse(_Ip, out tmpIP))
                return false;


            if (lScanner.Any(s => s.Value.IpAddress == _Ip))
            {
                System.Windows.MessageBox.Show("Direccion Ip duplicada");
                return false;
            }

            ReaderAccessor _SR2000 = new ReaderAccessor();
            _SR2000.IpAddress = _Ip;
            _SR2000.CommandPort = _Port;
            lScanner.Add(_Name, _SR2000);
            return true;
        }

        /// <summary>
        /// Inserta los items en el combobox del machine state
        /// </summary>
        private void fnCreateComboBoxList()
        {
            comboBox_ScannerSelect.Items.Clear();

            for (int i = 0; i < lScanner.Count; i++)
            {
                DataRow row = dtScanners.Rows[i];
                string _name = row[dcScanners_Name].ToString();
                comboBox_ScannerSelect.Items.Add(_name);
            }
        }

        /// <summary>
        /// Crea la lista de los scanners guardados en la tabla
        /// </summary>
        private void fnCrearListaScanners()
        {
            int _recipes = 1;
            fnBorrarListadeScanners();
            int _ScannerCount = dtScanners.Rows.Count;

            for (int i = 0; i < _ScannerCount; i++)
            {
                DataRow row = dtScanners.Rows[i];
                string _Ip = row[dcScanners_IP].ToString();
                int _Port = (int)row[dcScanners_Port];
                string _Name = row[dcScanners_Name].ToString();
                int _NoRecipes = Convert.ToInt32(row[dcScanners_NumberOfRecipes]);

                if (_NoRecipes > 1)
                    _recipes = Convert.ToInt32(row[dcScanners_NumberOfRecipes]);

                fnAgregarScannerALista(_Ip, _Port, _Name);
            }

            fnClearcbxComandos();
            fnAddRecipesToCBX(_recipes);
        }

        /// <summary>
        /// Borra los items del combobox de los comandos para despues añadir los nuevos
        /// </summary>
        private void fnClearcbxComandos()
        {
            cmbCommands.Items.Clear();
        }

        ///// <summary>
        ///// Crea la lista de los scanners guardados en la tabla
        ///// </summary>
        //private void fnCrearListaScanners()
        //{
        //    int _recipes = 1;
        //    fnBorrarListadeScanners();

        //   foreach (string scanner in SysPara.Scanners)
        //    {
        //        if (!lScanner.ContainsKey(scanner))
        //        {
        //            string ip = "";
        //            int port = 0;
        //            bool bFound = false;

        //            foreach (DataRow row in dtScanners.Rows)
        //            {
        //                if (row[dcScanners_Name].ToString() == scanner)
        //                {
        //                    ip = row[dcScanners_IP].ToString();
        //                    port = (int)row[dcScanners_Port];
        //                    bFound = true;
        //                }
        //            }

        //            if (!bFound)
        //            {
        //                DataRow newRow = dtScanners.Rows.Add();
        //                newRow[dcScanners_Name] = scanner;
        //                WriteSettingData();
        //            }

        //            fnAddScannerToTheList(ip, port, scanner);
        //        }
        //    }

        //    fnClearcbxCommands();
        //    fnAddRecipesToCBX(_recipes);
        //}

        /// <summary>
        /// Borra los items del combobox de los comandos para despues añadir los nuevos
        /// </summary>
        private void fnClearcbxCommands()
        {
            cmbCommands.Items.Clear();
        }

        /// <summary>
        /// Dependiendo del numero mas alto de recetas encontrado, agrega los items para cambiarlas por comando
        /// </summary>
        private void fnAddRecipesToCBX(int _value)
        {
            cmbCommands.Items.Add("LON");
            cmbCommands.Items.Add("LOFF");

            for (int i = 0; i < _value; i++)
            {
                string bload = "BLOAD," + (i + 1);

                if (!cmbCommands.Items.Contains(bload))
                    cmbCommands.Items.Add(bload);
            }
        }

        /// <summary>
        /// Escribe el comando enviado a el escanner
        /// </summary>
        /// <param name="Side"></param>
        /// <returns></returns>
        private async Task<string> fnSR2000WriteCommand(ReaderAccessor SR2000, string _command)
        {
            return await Task.Run(() =>
            {
                try
                {
                    
                    return SR2000.ExecCommand(_command, 4000);
                }
                catch
                {
                    SysPara.NPShowAlarm("9004", "Error al escribir el comando");
                    return "ERROR";
                }
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Toma la ip que recibe y agrega el scanner SR2000 a la lista
        /// Regresa verdadero si se agrega 
        /// Regresa falso si esa Ip ya esta registrada en la lista
        /// Regresa falso si la Ip no tiene el formato correcto
        /// </summary>
        /// <param name="_Ip"></param>
        /// <returns></returns>
        private bool fnAddScannerToTheList(string _Ip, int _Port, string _Name)
        {
            IPAddress tmpIP;

            if (lScanner.ContainsKey(_Name))
                return false;

            if (!IPAddress.TryParse(_Ip, out tmpIP))
                return false;


            if (lScanner.Any(s => s.Value.IpAddress == _Ip))
            {
                System.Windows.MessageBox.Show("Direccion Ip duplicada");
                return false;
            }

            ReaderAccessor _SR2000 = new ReaderAccessor();
            _SR2000.IpAddress = _Ip;
            _SR2000.CommandPort = _Port;
            lScanner.Add(_Name, _SR2000);
            return true;
        }

        /// <summary>
        /// Asynchronous task to connect SR2000 Scanner
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        private async Task<bool> fnConnectSR2000(ReaderAccessor _SR2000)
        {
            return await Task.Run(() =>
            {
                try
                {
                    fnDisconnectSR2000(_SR2000);
                    bool bConected = _SR2000.Connect();
                    return bConected;
                }
                catch (Exception ex)
                {
                    SysPara.NPShowAlarm("1301");
                }

                return false;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Desconecta el escaner recibido
        /// </summary>
        private void fnDisconnectSR2000(ReaderAccessor _SR2000)
        {
            try
            {
                _SR2000?.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Envia el comando del "TriggerOn" al escanner recibido
        /// </summary>
        /// <param name="Side"></param>
        /// <returns></returns>
        private async Task<string> fnSR2000TriggerOn(ReaderAccessor SR2000, int _timeout)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return SR2000.ExecCommand("LON", _timeout);
                }
                catch
                {
                    SysPara.NPShowAlarm("9004", "Error al enviar trigger");
                    return "ERROR";
                }

            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Trigger Off al escanner recibido
        /// </summary>
        /// <returns></returns>
        private async Task<string> fnSR2000TriggerOff(ReaderAccessor SR2000)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return SR2000.ExecCommand("LOFF", 20);
                }
                catch
                {
                    SysPara.NPShowAlarm("1301");
                    return "ERROR";
                }
            }).ConfigureAwait(false);
        }


        /// <summary>
        /// Retorna el objeto de la lista con el nombre recibido
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        private ReaderAccessor fn_GetScannerObjectFromList(string _name)
        {
            return lScanner.ContainsKey(_name) ? lScanner[_name] : null;
        }

        #endregion Scanner

        #region Funciones Publicas

        public IEnumerable<string> fn_GetScanerNameList()
        {
            return lScanner.Keys;
        }

        /// <summary>
        /// Resetea los valores publicos del escaner solicitado
        /// Si no existe el scanner retorna false
        /// </summary>
        public bool fnResetValues(string _name)
        {
            ReaderAccessor _Scan = fn_GetScannerObjectFromList(_name);

            if (_Scan == null)
                return false;

            SDKKernal.ClearAllAlarm();
            return true;
        }

        /// <summary>
        /// Retorna el valor de habilitado / deshabilitado
        /// Si no encuentra el scanner, retorna false
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public bool fnIsReaderEnabled(string _name)
        {
            int _ScannerCount = dtScanners.Rows.Count;           // Lee la tabla 

            for (int i = 0; i < _ScannerCount; i++)                                 // Recorre la tabla en busca del nombre
            {
                DataRow row = dtScanners.Rows[i];
                string _Name = row[dcScanners_Name].ToString();

                if (_name == _Name)
                    return Convert.ToBoolean(row[dcScanners_Enable]);
            }

            return false;
        }

        public bool fnGetShowPopup(string scannerName)
        {
            for (int i = 0; i < dtScanners.Rows.Count; i++)
            {
                DataRow row = dtScanners.Rows[i];

                if (scannerName == row[dcScanners_Name].ToString())
                    return (bool)row[dcScanners_ShowPopUp];
            }

            return false;
        }

        private async Task<string> fnManualScan()
        {
            return await Task.Run(() =>
            {
                ManualScanForm manualScanF = new ManualScanForm();
                manualScanF.TopMost = true;
                manualScanF.ShowDialog();
                string textRead = manualScanF.sTextRead;
                manualScanF.Dispose();
                return textRead;
            }).ConfigureAwait(false);
        }

        private ScannerReadResult fnProcessReadString(string scannerName, string readString)
        {
            ScannerReadResult readResult = new ScannerReadResult();

            if (string.IsNullOrWhiteSpace(readString) || readString == "ERROR")     //Si retorna vacio, iguala el valor a error
            {

            }
            else
            {
                readResult.bStatus = true;

                string[] arrCodes = readString.Split(',');

                foreach (string sCode in arrCodes)
                {
                    if (bValidateCodeQuality)
                    {
                        string[] arrStringSplit = sCode.Split(':');
                        ScannerData scannerData = new ScannerData();
                        scannerData.sCode = fnCheckPostProcessTasks(scannerName, arrStringSplit[0]);

                        if (arrStringSplit.Length > 1 && arrStringSplit[0].Length > 0)
                            scannerData.cQuality = arrStringSplit[1][0];

                        readResult.lCodes.Add(scannerData);
                    }
                    else
                    {
                        string sPostProcessedCode = fnCheckPostProcessTasks(scannerName, sCode);
                        readResult.lCodes.Add(new ScannerData() { sCode = sPostProcessedCode });
                    }
                }

                if (fnGetSaveImageEnabled(scannerName) == "Guardar en pass" || fnGetSaveImageEnabled(scannerName) == "Guardar siempre")
                    fnSaveLastImage(scannerName, "Pass").SafeFireAndForget(null,continueOnCapturedContext: false);
            }

            return readResult;
        }

        /// <summary>
        /// Obtiene el/los codigos leidos y los pasa a la variable publica
        /// </summary>
        /// <param name="_name"></param>
        public async Task<ScannerReadResult> fnGetMultipleCodes(string _name, bool bUseRetries = true)
        {
            ScannerReadResult readResult = new ScannerReadResult();
            ReaderAccessor _Scanner = fn_GetScannerObjectFromList(_name);          // Si el escaner no existe,

            if (_Scanner == null)
            {
                readResult.sError = $"Error: el escanner {_name} no existe";
                return readResult;
            }

            else
            {
                iNoAutomaticRetries = 0;
                DialogResult dResult = DialogResult.None;

                try
                {

                        string result = await fnSR2000TriggerOn(_Scanner, fnGetTimeOut(_name)).ConfigureAwait(false);
                        string result2 = await fnSR2000TriggerOn(_Scanner, fnGetTimeOut(_name)).ConfigureAwait(false);
                        result = result + "," + result2;
                        result = result.Trim();
                        readResult = fnProcessReadString(_name, result);
                        //await fnSR2000TriggerOff(_Scanner);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    readResult.sError = "ERROR de Lectura";
                }
            }

            return readResult;
        }

        public async void fnLoff(string Scanner)
        {
            

            ReaderAccessor _Scanner = fn_GetScannerObjectFromList(Scanner);
            await fnSR2000TriggerOff(_Scanner);
        }
        /// <summary>
        /// Recibe un string, realiza el split y lo agrega a la tabla
        /// </summary>
        /// <param name="_code"></param>
        private void fnAddResultsToTable(string _code)
        {
            string[] tmp;

            if (_code.Contains(","))
                tmp = _code.Split(',');
            else
                tmp = new string[] { _code };

            dgvCodigos.BeginInvoke((MethodInvoker)(() =>
            {
                dgvCodigos.Rows.Clear();

                for (int i = 0; i < tmp.Length; i++)
                {
                    dgvCodigos.Rows.Add();
                    DataGridViewRow row = dgvCodigos.Rows[i];
                    row.Cells["Id"].Value = i;
                    row.Cells["NoSerie"].Value = tmp[i];
                }
            }));
        }

        /// <summary>
        /// Recibe el nombre del escanner y consulta en la tabla cual programa debe cargar para la receta actual
        /// Carga la tarea previameente guardada
        /// </summary>
        public async Task<string> fnChangeProgram(string _name)
        {
            try
            {
                int iSearch = getTableRowNumberByName(ScannerName, _name);

                if (iSearch > -1)
                {
                    DataRow row = RScannerProgram.Rows[iSearch];
                    string sProgramName = row[ProgramSelection].ToString();
                    int iSearch2 = getTableRowNumberByName(ProgramName, sProgramName);

                    if (iSearch2 > -1)
                    {
                        DataRow row2 = dtPScannerPrograms.Rows[iSearch2];
                        int iProgramID = (int)row2[ProgramID];

                        if (iProgramID > 0 && iProgramID < 10)
                        {
                            ReaderAccessor _SR2000 = fn_GetScannerObjectFromList(_name);
                            return await fnSR2000WriteCommand(_SR2000, "BLOAD," + iProgramID.ToString()).ConfigureAwait(false);
                        }
                        else
                            return string.Format("ERROR para {0} / {1} debe ser entre 1 y 10, valor: '{2}'", _name, sProgramName, iProgramID.ToString());
                    }
                    else
                        return string.Format("ERROR no se encuentra programa dado de alta para {0}", _name);
                }
                else
                    return "ERROR no se encuentra escaner dado de alta en receta";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "ERROR de seleccion de escaner";
            }
        }

        /// <summary>
        /// Recibe el nombre del escanner y el programa debe cargar para la receta actual
        /// Carga la tarea previameente guardada
        /// </summary>
        public async Task<string> fnChangeProgram(string _name, string sProgramName)
        {
            try
            {
                int iSearch = getTableRowNumberByName(ScannerName, _name);
                if (iSearch > -1)
                {
                    int iSearch2 = getTableRowNumberByName(ProgramName, sProgramName);
                    if (iSearch2 > -1)
                    {
                        DataRow row2 = dtPScannerPrograms.Rows[iSearch2];
                        int iProgramID = (int)row2[ProgramID];
                        if (iProgramID > 0 && iProgramID < 10)
                        {
                            ReaderAccessor _SR2000 = fn_GetScannerObjectFromList(_name);
                            return await fnSR2000WriteCommand(_SR2000, "BLOAD," + iProgramID.ToString());
                        }
                        else
                            return string.Format("ERROR para {0} / {1} debe ser entre 1 y 10, valor: '{2}'", _name, sProgramName, iProgramID.ToString());
                    }
                    else
                        return string.Format("ERROR no se encuentra programa dado de alta para {0}", _name);
                }
                else
                    return "ERROR no se encuentra escaner dado de alta en receta";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "ERROR de seleccion de escaner";
            }
        }

        #endregion

        #region GUI

        private void dataGridView_ScannerPrograms_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

        }

        private void dataGridView_ProgramSelection_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dataGridView_PostProcess_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion GUI

        #endregion

        #region------------------------------------- Manual Controls

        private void btn_CrearLista_Click(object sender, EventArgs e)
        {
            //fnRemoveUnnecesaryProcesses();
            fnCrearListaScanners();
            pl_ControlManual.Enabled = true;
            fnCreateComboBoxList();

        }

        /// <summary>
        /// Retorna la ip del 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        private string fnGetIp(string _name)
        {
            int _ScannerCount = lScanner.Count;

            for (int i = 0; i < _ScannerCount; i++)
            {
                DataRow row = dtScanners.Rows[i];
                string _Name = row[dcScanners_Name].ToString();

                if (_name == _Name)
                    return row[dcScanners_IP].ToString();
            }
            return "";
        }

        /// <summary>
        /// Tarea para validar la conexion con el web browser antes de conectar
        /// </summary>
        /// <param name="_ip"></param>
        /// <returns></returns>
        private async Task<bool> fnValidateConnection(string _ip)
        {
            bool _result = false;

            IPAddress IP;

            if (IPAddress.TryParse(_ip, out IP))
            {
                Socket s = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

                _result = await Task.Run(() =>
                {
                    try
                    {
                        s.Connect(IP, 80);
                        return true;
                    }
                    catch (Exception ex)
                    {

                    }

                    return false;
                }).ConfigureAwait(false);

                s?.Dispose();
            }

            return _result;
        }

        private async void comboBox_ScannerSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _Scanner = comboBox_ScannerSelect.Text;
            string _ip = fnGetIp(_Scanner);

            if (await fnValidateConnection(_ip))
                await fnConnectWebBrowserAsync(_Scanner);
        }

        private async void btn_OpenCom_Scanner_Click(object sender, EventArgs e)
        {
            string _name = comboBox1.Text;

            if (lScanner.ContainsKey(_name))  // En la funcion se comprueba que este registrada la Ip
            {
                bool connected = await fnConnectSR2000(lScanner[_name]);
                //List<string> strImageFile = lScanner[_name].GetFileList("IMAGE");
            }
        }

        private void btn_CloseCom_Scanner_Click(object sender, EventArgs e)
        {
            string _name = comboBox1.Text;
            fnDisconnectSR2000(lScanner[_name]);
        }

        private async void btn_Read_Scanner_Click(object sender, EventArgs e)
        {
            string _name = comboBox1.Text;
            dgvCodigos.Rows.Clear();
            string result = await fnSR2000WriteCommand(lScanner[_name], cmbCommands.Text);
            result = result.TrimEnd();
            textBox5.Text = result.ToUpper();
            fnAddResultsToTable(result);
        }

        private void button_GetDirection_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Seleccionar carpeta para respaldo de Imagenes";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK || result == DialogResult.Yes)
                PSet.Rows[0][cDireccion] = dialog.SelectedPath;
        }

        #endregion

        #region------------------------------------- Flow Initial

        private FCResultType fc_In_Start_FlowRun(object sender, EventArgs e)
        {
            SDKPara.Arm.ClearAlarm(cAlarm.I(fc_In_Start));
            //lTasksConnect.Clear();
            //tChangeScannerProgram = null;
            if (!MiddleLayer.ScannerF.bModuleEnabled)
            {
                string _alarm = "Escaner Deshabilitado desde Producttion Settings...";
                SDKPara.Arm.ShowAlarm(cAlarm.I(fc_In_Start), _alarm);
            }
            return FCResultType.NEXT;
        }

        private FCResultType fc_In_Process_FlowRun(object sender, EventArgs e)
        {
            int index = 0;

            foreach (var reader in lScanner)
            {
                DataRow row = dtScanners.Rows[index];
                string _name = fnGetScannerName(index);
                bool _enabled = fnIsReaderEnabled(_name);
                bool _isManual = fnIsManualScanMode(_name); // Ojo, validar si el escaner manual requiere conexion, quitar esta condicion para que tome los datos y entre a la lista

                if (_enabled && !_isManual)
                {
                    //ta bool b =  fnConnectSR2000(reader.Value).Result;

                      lTasksConnect.Add( fnConnectSR2000(reader.Value));                             //Se agrega una Task para su conexion

                   
                }


                  
                index++;
            }

            return FCResultType.NEXT;
        }

        private FCResultType fc_In_OK_FlowRun(object sender, EventArgs e)
        {
            bool _FailConection = false;
            int _index = 0;

            Task t = Task.WhenAll(lTasksConnect);

            if (t.Status == TaskStatus.RanToCompletion)         // Verifica que todas las tareas hayan terminado
            {
                foreach (Task<bool> _t in lTasksConnect)            //Se comprueba el resultado de la conexión, 
                {
                    if (!_t.Result)                          // Si falla, manda la alarma por cada escanner independiente
                    {
                        _FailConection = true;
                        SDKKernal.ShowAlarm(cAlarm.E(fc_In_Start), fnGetScannerName(_index) + ": Falla de comunicacion");
                    }

                    _index++;
                }

                if (_FailConection)
                    return FCResultType.IDLE;

                SDKKernal.ClearAlarm(cAlarm.E(fc_In_Start));
                lTasksConnect.Clear();
                pl_ControlManual.Enabled = true;
                return FCResultType.NEXT;
            }

            if (fc_In_Start.tmrTimeOut.On(5000) && t.Status == TaskStatus.Faulted)  // Comprueba el tiempo y si alguna tarea retorno falla
                SDKKernal.ShowAlarm(cAlarm.E(fc_In_Start), "SR2000: Falla de comunicacion con Scanner, Favor de revisar"); // Alarma General para el NPFlowChart

            return FCResultType.IDLE;
        }

        private FCResultType fc_In_SetProgram_FlowRun(object sender, EventArgs e)
        {
            //if (SysPara.RecipeName != sLastRecipe)
            //{
            //    if (tChangeScannerProgram == null)
            //        tChangeScannerProgram = fnChangeProgram("Scanner");
            //    // Cambiar el programa de un escaner en especifico     
            //    if (tChangeScannerProgram.IsCompleted)
            //    {
            //        if (tChangeScannerProgram.Result == "OK,BLOAD\r")
            //        {
            //            sLastRecipe = SysPara.RecipeName;
            //            SDKKernal.ClearAlarm(cAlarm.E(fc_In_Start));
            //            return FCResultType.NEXT;
            //        }
            //        SDKKernal.ShowAlarm(cAlarm.E(fc_In_Start), "SR2000: Falla al cargar programa Error:" + tChangeScannerProgram.Result); // Alarma General para el NPFlowChart
            //    }
            //    return FCResultType.IDLE;
            //}
            return FCResultType.NEXT;
        }


        private FCResultType fc_In_Finish_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = true;
            return FCResultType.IDLE;
        }


        #endregion

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

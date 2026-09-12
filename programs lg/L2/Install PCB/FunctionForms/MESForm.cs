using Alpha.Classes;
using NPSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha
{
    public partial class MESForm : Form
    {
        #region VARIABLES
        PalletLinkSNDLL.PalletLinkSN palletLink = new PalletLinkSNDLL.PalletLinkSN();
        cSmartAlarm cAlm = SmartAlarm.cAlarm;

        public bool bValidateDuplicateSerialNumbers { get { return GetData(dcMESTable_ValidateDuplicateSerialNumbers); } }
        public string sCheckPoint { get { return GetData(dcMESTable_CheckPoint); } }

        public int iCounterBatchUnits { get { return GetData(dc_CounterBatchUnits); } }
        public int iLimitBatchUnits { get { return GetData(dc_LimitBatchUnits); } }

        #endregion VARIABLES

        public MESForm()
        {
            InitializeComponent();
            //Load xml data to MESData variable
            ReadMESData();
            SysPara.MESPara = new MESParameter();
            cAlm.AddRange("MES");
        }

        #region GENERAL FUNCTIONS

        #region System
        private dynamic GetDefaultValue(Type tp)
        {
            dynamic dmc = null;
            bool r1 = (tp == typeof(Boolean));
            bool r2 = (tp == typeof(Int16));
            bool r3 = (tp == typeof(Int32));
            bool r4 = (tp == typeof(Int64));
            bool r5 = (tp == typeof(UInt16));
            bool r6 = (tp == typeof(UInt32));
            bool r7 = (tp == typeof(UInt64));
            bool r8 = (tp == typeof(double));
            bool r9 = (tp == typeof(byte));
            bool r10 = (tp == typeof(string));
            if (r1)
                dmc = false;
            if (r2 || r3 || r4 || r5 || r6 || r7 || r8 || r9)
                dmc = 0;
            if (r10)
                dmc = "";
            return dmc;
        }
        public dynamic GetDataValue(string TableName, string ColumnName, int RowIndex = 0)
        {
            dynamic Value = null;

            if (MESData.Tables[TableName].Columns.IndexOf(ColumnName) >= 0)
            {
                Value = MESData.Tables[TableName].Rows[RowIndex][ColumnName, DataRowVersion.Original];
                if (Value.ToString() == "")
                {
                    //SysPara.NPShowAlarm("2025", "MES data was not set value, ColumnName=\"" + ColumnName + "\"");
                    Value = GetDefaultValue(MESData.Tables[TableName].Columns[ColumnName].DataType);
                }
            }
            else
                SysPara.NPShowAlarm("50020", "MES data was not found the column, ColumnName=\"" + ColumnName + "\"");

            return Value;
        }
        private void setValueToRowByName(DataColumn _DataValueColumn, string sValue)
        {
            DataRow row = _DataValueColumn.Table.Rows[0];
            row[_DataValueColumn] = sValue;
            MiddleLayer.MachineSetupF.plMes.Focus();
            WriteMESData();
        }

        private void setValueToRowByName(DataColumn _DataValueColumn, int iValue)
        {
            DataRow row = _DataValueColumn.Table.Rows[0];
            row[_DataValueColumn] = iValue;
            MiddleLayer.MachineSetupF.plMes.Focus();
            WriteMESData();
        }
        #endregion System

        #region MES
        /// <summary>
        /// This functions reads xml data and send to MESData global variable
        /// </summary>
        public void ReadMESData()
        {
            //Clear data set
            MESData.Clear();
            //Take path string of xml MES data file
            //string MESDataPath = String.Format("{0}\\ModuleData\\MESData\\{1}.xml", System.IO.Directory.GetCurrentDirectory(), SysPara.RecipeName);
            string MESDataPath = Path.Combine(SysPara.MESDataDirectory, $"MESData{SysPara.RecipeName}.xml"); 
            if (File.Exists(MESDataPath))
            {
                //Create new data set
                DataSet ds = new DataSet();
                //Load data at ds data set
                ds.Merge(MESData);
                //Load xml file to ds
                ds.ReadXml(MESDataPath);
                try
                {
                    //Create new dataset "dt" with MESTable ds data only
                    DataTable dt = ds.Tables[dt_MESTable.TableName].Copy();
                    //Clear MESTable in MESData
                    MESData.Tables[ds.Tables[dt_MESTable.TableName].TableName].Clear();
                    //Load the MESTable xml flieds to MESData data set.
                    MESData.Merge(dt);
                    //Create new dataset "dt" with MESTable ds data only
                    DataTable dt2 = ds.Tables[dt_SerialInfo.TableName].Copy();
                    //Clear MESTable in MESData
                    MESData.Tables[ds.Tables[dt_SerialInfo.TableName].TableName].Clear();
                    //Load the MESTable xml flieds to MESData data set.
                    MESData.Merge(dt2);
                }
                catch (Exception ex) { }
            }
            //Parse every MESData tables
            for (int i = 0; i < MESData.Tables.Count; i++)
                if (MESData.Tables[i].Rows.Count == 0)
                {
                    //Create a new object row for every MESData table
                    DataRow NewRow = MESData.Tables[i].NewRow();
                    //Add new row to every MESData table
                    MESData.Tables[i].Rows.Add(NewRow);
                }
            //When AcceptChanges is called, any DataRow object still in edit mode successfully ends its edits.
            MESData.AcceptChanges();
        }
        //Store last MESData to xml file
        public void WriteMESData()
        {
            //Take path string of xml MES data file
            string MESDataPath = Path.Combine(SysPara.MESDataDirectory, $"MESData{SysPara.RecipeName}.xml");
            //Take file info
            FileInfo fiTmp1 = new FileInfo(MESDataPath);
            //Validate file exists
            if (fiTmp1.Directory.Exists == false)
                fiTmp1.Directory.Create();
            //When AcceptChanges is called, any DataRow object still in edit mode successfully ends its edits.
            MESData.AcceptChanges();
            //Write new xml file at string path
            MESData.WriteXml(MESDataPath);
        }

        private void fnUpdateBatchUnitCounter(int iUnitsToAdd = 1)
        {
            int iCounter = GetData(dc_CounterBatchUnits);
            iCounter += iUnitsToAdd;
            setValueToRowByName(dc_CounterBatchUnits, iCounter);
        }
        public async Task fnWriteBirthFile(string[] arrSerialNumbers)
        {
            bool generateAutomaticBirth = GetData(dcSerialInfo_GenerateAutomaticBirth);
            bool generateBackupBirth = GetData(dcSerialInfo_GenerateBackupBirth);

            if (!generateAutomaticBirth && !generateBackupBirth && arrSerialNumbers.Length>0)
                return;

            DateTime datetime = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            sb.Append(datetime.ToString("yyyy - MM - dd[HH - mm - ss]"));
            sb.Append("   Empiezo");

            foreach (string sSerialNumber in arrSerialNumbers)
            {
                    sb.Append(", ");
                    sb.Append(sSerialNumber);
                    sb.Append(", OK");
            }

            if (generateBackupBirth)
            {
                DateTime now = DateTime.Now;
                string sBackupPath = Path.Combine(GetData(dcSerialInfo_BackupBirthPath), now.ToString("yyyy"), now.ToString("MM") + now.ToString("dd"));
                string sAutoBirthPath = GetData(dcSerialInfo_AutomaticBirthPath);
                string fileName = datetime.ToString("yyyyMMddHHmmss") + ".TXT";
                string sFullBackupPath = Path.Combine(sBackupPath, fileName);
                string sFullAutomaticBirthPath = Path.Combine(sAutoBirthPath, fileName);

                try
                {
                    if (!Directory.Exists(sBackupPath))
                        Directory.CreateDirectory(sBackupPath);

                    File.WriteAllText(sFullBackupPath, sb.ToString());

                    if (generateAutomaticBirth)
                    {
                        fnUpdateBatchUnitCounter(arrSerialNumbers.Length);
                        await Task.Delay(100);
                        File.Copy(sFullBackupPath, sFullAutomaticBirthPath);
                    }
                }
                catch (Exception ex)
                {

                }
           
            }
            else if (generateAutomaticBirth)
            {
                string externalPath = GetData(dcSerialInfo_AutomaticBirthPath);
                string fileName = datetime.ToString("yyyyMMddHHmmss") + ".TXT";
                string fullExternalPath = Path.Combine(externalPath, fileName);

                try
                {
                    File.WriteAllText(fullExternalPath, sb.ToString());
                }
                catch (Exception ex)
                {

                }
            }
        }

        //public struct Measurement
        //{
        //    public string measureLabel;
        //    public string measureData;
        //    public string measureMessage;

        //    public Measurement(string measureLabel, string measureData, string measureMessage)
        //    {
        //        this.measureLabel = measureLabel;
        //        this.measureData = measureData;
        //        this.measureMessage = measureMessage;
        //    }
        //}

        /// <summary>
        /// Saves Tar File
        /// </summary>
        /// <param name="sSerialNumber">Send serial, equal to null to use MesPara serial number</param>
        /// <param name="sAssemblyNumber">Sends assembly, equal to null to use MesPara partNumber</param>
        /// <param name="sAssemblyRev">Sends assembly rev, equal to '.' to use MesPara revision</param>
        /// <param name="bUseStatus">Determines if use custom status</param>
        /// <param name="bStatusOk">Sends custom status true for PASS, false for FAIL</param>
        public async Task WriteUnitMES(string sSerialNumber, string sAssemblyNumber=null, string sProcessStep=null, bool bUseStatus=false, bool bStatusOk=false)
        {
            try
            {
                bool generateExternalTar = GetData(dcMESTable_GenerateExternalTar);
                bool generateLocalTar = GetData(dcMESTable_GenerateLocalTar);
                bool bUseWebservice = GetData(dcMESTable_UseWebservice);

                if (!generateExternalTar && !generateLocalTar && !bUseWebservice)
                    return;

                //Load values to every string from MESData
                string sCustomerName = GetData(dcMESTable_CustomerName);
                string sDivision = GetData(dcMESTable_Division);
                sSerialNumber = (sSerialNumber != null) ? sSerialNumber : SysPara.MESPara.serialNumber;
                sAssemblyNumber = (sAssemblyNumber != null) ? sAssemblyNumber : GetData(dcMESTable_AssemblyNumber);
                string sStationName = GetData(dcMESTable_StationName);
                string _Rev = GetData(dcMESTable_AssemblyRev);
                sProcessStep = (sProcessStep != null) ? sProcessStep : GetData(dcMESTable_ProcessStep);
                string sSite = GetData(dcMESTable_Site);
                bStatusOk = (!bUseStatus) ? SysPara.MESPara.status : bStatusOk;
                DateTime dt = DateTime.Now;

                //Load string values to tsList
                #region Add To StringList
                TStringList tsList = new TStringList();
                TStringList tsWebArt = new TStringList();
                tsList.Add("S" + sSerialNumber);
                tsList.Add("C" + sCustomerName);
                tsList.Add("I" + sDivision);
                tsList.Add("N" + sStationName);
                tsList.Add("P" + sProcessStep);
                tsList.Add("n" + sAssemblyNumber);
                tsList.Add("r" + _Rev);
                tsList.Add("p" + sSite);
                tsList.Add("[" + dt.ToString("MM/dd/yyyy HH:mm:ss"));
                tsList.Add("]" + dt.ToString("MM/dd/yyyy HH:mm:ss"));

                if (bStatusOk)
                    tsList.Add("TP");
                else
                {
                    tsList.Add("TF");
                    foreach (KeyValuePair<string, string> fail in SysPara.MESPara.fails)
                    {
                        tsList.Add("F" + fail.Key.ToString());
                        tsList.Add(">" + fail.Value.ToString());
                    }
                }

                foreach (Measurement measurement in SysPara.MESPara.measurements)
                {
                    tsList.Add("M" + measurement.measureLabel);
                    tsList.Add("d" + measurement.measureData);
                    tsList.Add(">" + measurement.measureMessage);
                }
                SysPara.MESPara.measurements.Clear();
                #endregion

                #region Write To File
                //TAR files Path variables

                if (bUseWebservice)
                {
                    string sResult = await Task.Run(() =>
                    {
                        try
                        {
                            return palletLink.fnSendToMES(tsList.Text);
                        }
                        catch (Exception ex)
                        {
                            return ex.Message;
                        }

                    }).ConfigureAwait(false);

                    if (sResult != "PASS")
                        SDKPara.Arm.ReportAlarm(cAlm.E("MES", null), AcuraIOT.ErrorGroup.MES, AcuraIOT.ErrorSubGroup.Result, AcuraIOT.ErrorType.E, "MES: Error al enviar dato -> " + sResult);
                }

                if (generateLocalTar)
                {
                    string BackupDir = GetData(dcMESTable_UnitSaveBackupPath);
                    string sServerDir = GetData(dcMESTable_UnitSavePath);
                    string BackupPath = Path.Combine(BackupDir, dt.ToString("yyyy"), dt.ToString("MMdd"), sSerialNumber + "_" + dt.ToString("yyyyMMddHHmmss") + ".tar");
                    string sServerPath = Path.Combine(sServerDir, sSerialNumber + "_" + dt.ToString("yyyyMMddHHmmss") + ".tar");

                    try
                    {
                        FileInfo fiTmp2 = new FileInfo(BackupPath);

                        if (fiTmp2.Directory.Exists == false)
                            fiTmp2.Directory.Create();

                        tsList.SaveToFile(BackupPath);

                        if (generateExternalTar)
                        {
                            await Task.Delay(100);
                            File.Copy(BackupPath, sServerPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Write to backup failed: " + ex.Message);
                    }
                }
                else if (generateExternalTar)
                {
                    string SaveDir = GetData(dcMESTable_UnitSavePath);
                    string SavePath = Path.Combine(SaveDir, sSerialNumber + "_", dt.ToString("yyyyMMddHHmmss") + ".tar");

                    try
                    {
                        FileInfo fiTmp1 = new FileInfo(SavePath);

                        if (fiTmp1.Directory.Exists == false)
                            fiTmp1.Directory.Create();

                        tsList.SaveToFile(SavePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Write to server failed: " + ex.Message);
                    }

                }
                #endregion
            }
            catch (Exception) { }
        }


        #endregion MES


        /// <summary>
        ///get laser barcode
        /// </summary>
        /// <param name="iQuantity"></param>
        /// <returns></returns>
        public async Task<(bool, string[])> fnGenerateMultipleSerialNumbers(int iQuantity)
        {
            bool bFinalBirthStatus = false;
            string[] arrSerialNumbers = new string[iQuantity];

            for (int i = 0; i < iQuantity; i++)
            {
                // get barcode for laser
                (bool bBirthStatus, string sSerialNumber) = await fnGenerateNewSerialNumberTesla();
                arrSerialNumbers[i] = sSerialNumber;

                if (bBirthStatus)
                    bFinalBirthStatus = true;
            }

            return (bFinalBirthStatus, arrSerialNumbers);
        }

        //public async Task<(bool, string)> fnGenerateNewSerialNumber()
        //{
        //    DateTime datetime = DateTime.Now;
        //    JulianCalendar julianCal = new JulianCalendar();

        //    //string sPrefix = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_Static1.ColumnName);
        //    //string sSufix = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_Static2.ColumnName);
        //    //string sEngineeringLevel = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_PCBAPartNumber.ColumnName);

        //    int dayOfYear = julianCal.GetDayOfYear(datetime);
        //    string sDate = datetime.ToString("yy") + dayOfYear.ToString("D3");

        //    int iSerialNumber = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_ConsecutiveNumber.ColumnName);
        //    string uniqueNumber = (iSerialNumber <= 9999) ? iSerialNumber.ToString("D4") : "0000";

        //    string sSerialNumber = sPrefix + sDate + sEngineeringLevel + uniqueNumber + sSufix;
        //    setValueToRowByName(dcSerialInfo_ConsecutiveNumber, (iSerialNumber + 1).ToString());
        //    bool bBirthStatus = false;

        //    if (bValidateDuplicateSerialNumbers)
        //    {
        //        bBirthStatus = await Task.Run(() =>
        //        {
        //            try
        //            {
        //                return palletLink.BirthStatus(sSerialNumber);
        //            }
        //            catch (Exception ex)
        //            {
        //                SDKPara.Arm.ReportAlarm(cAlm.E_Stop("MES"), AcuraIOT.ErrorGroup.MES, AcuraIOT.ErrorSubGroup.Exception, AcuraIOT.ErrorType.ES, "MES: Excepción en validación de nacimiento");
        //            }

        //            return false;
        //        }).ConfigureAwait(false);
        //    }

        //    if (!bBirthStatus)
        //    {
        //        try
        //        {
        //            DateTime dt = DateTime.Now;
        //            string logPath = Path.Combine(GetData(dcMESTable_LogPath), dt.Year.ToString(), dt.Month.ToString());

        //            if (!Directory.Exists(logPath))
        //                Directory.CreateDirectory(logPath);

        //            string logFullPath = Path.Combine(logPath, dt.Day.ToString() + ".csv");
        //            File.AppendAllText(logFullPath, sSerialNumber + Environment.NewLine);
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }

        //    return (bBirthStatus, sSerialNumber);
        //}

        private int fnGetJulianDay()
        {
            
            DateTime datetime = DateTime.Now;
            int currentJulianDay = datetime.DayOfYear;
            return currentJulianDay;
        }
        /// <summary>
        /// Reinicia el contador si el dia cambio
        /// </summary>
        public void fnCheckResetConsecutiveNumber()
        {
            int iJulianDayDB = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_CurrentJulianDay.ColumnName);
            int iCurrentJulianDay = fnGetJulianDay();
            if (iCurrentJulianDay != iJulianDayDB)
            {
                setValueToRowByName(dcSerialInfo_ConsecutiveNumber, (0).ToString());
                setValueToRowByName(dcSerialInfo_CurrentJulianDay, iCurrentJulianDay.ToString());
            }
        }

        private string fnGetLetterByNumber(int i=1)
        {
            if (i < 1)
                return "";
            char c = (Char)(64 + i);
            return c.ToString();
        }

        private string fnGetIntToBase36(int val)
        {
            char[] baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string result = string.Empty;
            int targetBase = baseChars.Length;
            do
            {
                result = baseChars[val % targetBase] + result;
                val = val / targetBase;
            }
            while (val > 0);
            if (result.Length < 5)
            {
                result = string.Format("{0,5}", result);
                result = result.Replace(" ", "0");
            }


            return result;
        }

        /// <summary>
        /// return barcode for laser
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string)> fnGenerateNewSerialNumberTesla()
        {
            fnCheckResetConsecutiveNumber();
            DateTime datetime = DateTime.Now;

            //JulianCalendar julianCal = new JulianCalendar();
            //SerialFormat P1531436-00-F:SADU2115100002G

            string sPartN = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_UniquePartNumber.ColumnName);
            string sSiteCode = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_SiteCode.ColumnName);

            int dayOfYear = datetime.DayOfYear;
            string sDate = datetime.ToString("yy") + dayOfYear.ToString("D3");
            int iSerialNumber = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_ConsecutiveNumber.ColumnName);
            string uniqueNumber = fnGetIntToBase36(iSerialNumber);
            //string uniqueNumber = (iSerialNumber <= 99999) ? iSerialNumber.ToString("D5") : "00000";
            string monthLetter = fnGetLetterByNumber(datetime.Month);
            //string sStatic = GetDataValue(dt_SerialInfo.TableName, dcSerialInfo_Static.ColumnName);


            string sSerialNumber = $"P{sPartN}:S{sSiteCode}{sDate}{monthLetter}{uniqueNumber}";
            setValueToRowByName(dcSerialInfo_ConsecutiveNumber, (iSerialNumber + 1).ToString());
            bool bBirthStatus = false;

            if (bValidateDuplicateSerialNumbers)
            {
                bBirthStatus = await Task.Run(() =>
                {
                    try
                    {
                        return palletLink.BirthStatus(sSerialNumber);
                    }
                    catch (Exception ex)
                    {
                        SDKPara.Arm.ReportAlarm(cAlm.E_Stop("MES"), AcuraIOT.ErrorGroup.MES, AcuraIOT.ErrorSubGroup.Exception, AcuraIOT.ErrorType.ES, "MES: Excepción en validación de nacimiento");
                    }

                    return false;
                }).ConfigureAwait(false);
            }

            if (!bBirthStatus)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string logPath = Path.Combine(GetData(dcMESTable_LogPath), dt.Year.ToString(), dt.Month.ToString());

                    if (!Directory.Exists(logPath))
                        Directory.CreateDirectory(logPath);

                    string logFullPath = Path.Combine(logPath, dt.Day.ToString() + ".csv");
                    File.AppendAllText(logFullPath, sSerialNumber + Environment.NewLine);
                }
                catch (Exception)
                {

                }
            }

            return (bBirthStatus, sSerialNumber);
        }

        public string fnGetWeekNumber(DateTime date)
        {
            try
            {
                DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
                
                if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                    date = date.AddDays(3);

                int currWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                if (currWeek < 10)
                    return $"0{currWeek}";
                else
                    return currWeek.ToString();
            }
            catch (Exception)
            {

            }

            return "";
        }

       
        #region GUI

        //Function update stored path files from MESData to text box
        public void updateMES()
        {
            try
            {
                textLocalPath.Text = GetData(dcMESTable_UnitSaveBackupPath);
                textExternalPath.Text = GetData(dcMESTable_UnitSavePath);

            }
            catch { }
        }
        //This will show folder explorer at externat text box
        private void buttonExternalPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please select the Unit Save folder";

            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                dt_MESTable.Rows[0][dcMESTable_UnitSavePath] = dialog.SelectedPath;
            }
        }

        //This will show folder local at externat text box
        private void buttonLocalPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please select the Unit Save folder";

            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                dt_MESTable.Rows[0][dcMESTable_UnitSaveBackupPath] = dialog.SelectedPath;
            }
        }
        private async void button_TestTarGeneration_Click(object sender, EventArgs e)
        {
           await  WriteUnitMES(null, null, null, true, true);
        }
        #endregion GUI

        #endregion GENERAL FUNCTIONS

        private async void btn_PedirNumero_Click(object sender, EventArgs e)
        {
            //btnGenerateSerialNumber.Enabled = false;

            //(bool bStatus, string sSerialNumber) = await fnGenerateNewSerialNumberTesla();
            //txt_NumberGenerated.Text = sSerialNumber;

            //if (bStatus)
            //    MessageBox.Show("Número de serie repetido en MES", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //btnGenerateSerialNumber.Enabled = true;
        }

        public dynamic GetData(DataColumn cl, int index = 0)
        {
            DataTable dt_Table = cl.Table;
            dynamic Value = null;

            Value = dt_Table.Rows[index][cl.ColumnName, DataRowVersion.Original];

            if (Value.ToString() == "")
            {
                //SysPara.NPShowAlarm("2026", $"Data from table: {dt_Table.TableName} was not set value, ColumnName: { cl.ColumnName } returning default value ");
                Value = GetDefaultValue(cl.DataType);
            }
            return Value;
        }
        private void btnSelectBackupBirthPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Seleccione ruta para guardar backup de nacimiento";

            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                dt_SerialInfo.Rows[0][dcSerialInfo_BackupBirthPath] = dialog.SelectedPath;
            }
        }

        private void btnSelectAutomaticBirthPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Seleccione ruta del automatic birth";

            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                dt_SerialInfo.Rows[0][dcSerialInfo_AutomaticBirthPath] = dialog.SelectedPath;
            }
        }

        private void btnSelectLogSerialPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Seleccione ruta del Log de Seriales";

            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                dt_SerialInfo.Rows[0][dcMESTable_LogPath] = dialog.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setValueToRowByName(dc_CounterBatchUnits, 0);
        }
    }
}

using AcuraLibrary.Forms;
using NPSDK;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using Alpha.Classes;
using Alpha.FunctionForms;
using System.Collections.Generic;
using System.Linq;
using CheckPoint;
using System.Threading.Tasks;

using AcuraLibrary;

namespace Alpha.ModuleForms
{
    public partial class ProcessForm : ModuleBaseForm
    {


        #region------------------------------ Variables        

        private bool bInJustOnce;

        public JTimer tmrTaktTime = new JTimer();

        protected JTimer AcuraIOTTimer = new JTimer();

        public JTimer tmr_UpdateLabels = new JTimer();

        private bool bStateChangeDarkMode;

        private cSmartAlarm sAlarm = new cSmartAlarm();

        public int iUnitsPassCount { get; set; }
        public int iUnitsFailCount { get; set; }
        public int iJidokaCount { get; set; }

        Dictionary<string, ProcessDataForm> a_ProcessData;
        private bool bStateDryCycle = false;
        public bool bBypassMode { get { return GetData(IsByPassMode); } }
        public bool bDryCycleMode { get { return GetData(IsDryCycleMode); } }
        public bool bDryCycleNotUnit { get { return GetData(DryCycleNotUnit); } }
        public bool bMESEnabled { get { return GetData(EnableMes); } }
        public bool bShowPopupOnFail { get { return GetData(ShowPopUpOnFail); } }
        public int nAutomaticRetries { get { return GetData(NoAutomaticRetries); } }
        public int nMaxUnitBin { get { return GetData(BinUnitsNumber); } }

        public bool bDisableDoor { get { return GetData(DisableDoor); } }
        public bool bEnableSmema { get { return GetData(EnableSMEMA); } }


        #region Log Alarms
        public bool bAlmLog_GNR
        {
            get { return GetData(dc_bSaveAlmGnr); }
        }
        public bool bAlmLog_I
        {
            get { return GetData(dc_bSaveAlmI); }
        }
        public bool bAlmLog_W
        {
            get { return GetData(dc_bSaveAlmW); }
        }
        public bool bAlmLog_K
        {
            get { return GetData(dc_bSaveAlmK); }
        }
        public bool bAlmLog_E
        {
            get { return GetData(dc_bSaveAlmE); }
        }
        #endregion





        public bool bEnableCheckProcess { get { return GetData(dcEnableCheckProcess); } }
        public string sCheckProcessID { get { return GetData(dcCheckProcessID); } }


        #endregion

        public ProcessForm()
        {
            InitializeComponent();
            a_ProcessData = new Dictionary<string, ProcessDataForm>();


            #region tabControl
            //plMaintenance.Enabled = false;
            //plProductionSetting.Enabled = true;
            //plRecipeEditor.Enabled = false;
            //plFlowInitial.Enabled = false;
            //plFlowAuto.Enabled = false;
            //plMachineStatus.Enabled = true;
            //plMotionSetup.Enabled = false;
            //plMotorControl.Enabled = false;
            #endregion

            #region SmartAlarm
            sAlarm.AddRange("ProcessForm", this);
            fnDoDarkMode(SysPara.MainPara.bDarkModeActive);
            #endregion

            #region IOT
            AcuraIOT.AcuraCloudServices.SetAcuraCloudFile(SysPara.AcuraIOTDirectory);
            AcuraIOT.AcuraCloudServices.LoadInitFile();
            #endregion

        }
        #region------------------------------ Override Inicial

        public override void InitialReset()
        {
            AcuraIOT.AcuraCloudServices.SetAcuraCloudFile(SysPara.AcuraIOTDirectory);
            AcuraIOT.AcuraCloudServices.LoadInitFile();
            AcuraIOTTimer.Restart();

            //SysPara.bDryCycle = bDryCycleMode;
            SysPara.bDryCycleNotUnit = bDryCycleNotUnit;
            SysPara.bDisableDoor = bDisableDoor;

        }


        public override void StartRun()
        {
            if (a_ProcessData != null)
            {
                foreach (var item in a_ProcessData)
                    item.Value?.fnTimersRunning(true);
            }
        }

        public override void StopRun()
        {
            if (a_ProcessData != null)
            {
                foreach (var item in a_ProcessData)
                    item.Value?.fnTimersRunning(false);
            }
        }

        public override void ModuleInitialize(string ModuleName)
        {
            base.ModuleInitialize(ModuleName);

            fnInitMachineStateProcessForm();
        }

        public override void ModuleDispose()
        {
            try
            {
                if (a_ProcessData != null)
                {
                    foreach (var item in a_ProcessData)
                        item.Value?.Dispose();

                    a_ProcessData.Clear();
                }
            }
            catch (Exception ex)
            {
                MiddleLayer.ExReportF.fnAddException(ex);
            }
        }

        public override void AlwaysRun()
        {

            #region AcuRaIOT
            if (AcuraIOTTimer.On(1000))
            {
                bool taktTimeExceeded = tmrTaktTime.On((Convert.ToInt32(GetData(nLoadTime))) * 1000);

                try
                {
                    if (SysPara.SystemRun && SysPara.SystemMode == RunMode.AUTO && !taktTimeExceeded)
                        AcuraIOT.AcuraMachineState.AsyncIOTSystemMode(AcuraIOT.FeelingType.Run, "System Forms AlwaysRun");
                    else if (!SysPara.SystemRun && SDKPara.Arm.ArmUIList.Exists(t => t.DoStop))
                        AcuraIOT.AcuraMachineState.AsyncIOTSystemMode(AcuraIOT.FeelingType.MachineError, "System Forms AlwaysRun");
                    else if (!SysPara.SystemRun && (SysPara.SystemMode == RunMode.AUTO || SysPara.SystemMode == RunMode.HOME))
                        AcuraIOT.AcuraMachineState.AsyncIOTSystemMode(AcuraIOT.FeelingType.Pause, "System Forms AlwaysRun");
                    else if (taktTimeExceeded)
                        AcuraIOT.AcuraMachineState.AsyncIOTSystemMode(AcuraIOT.FeelingType.Idle, "System Forms AlwaysRun");
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                }
                AcuraIOTTimer.Restart();
            }
            #endregion

            #region DarkMode
            if (SysPara.MainPara.bDarkModeActive && !bStateChangeDarkMode)
            {
                bStateChangeDarkMode = !bStateChangeDarkMode;
                fnDoDarkMode(SysPara.MainPara.bDarkModeActive);
            }
            else if (!SysPara.MainPara.bDarkModeActive && bStateChangeDarkMode)
            {
                bStateChangeDarkMode = !bStateChangeDarkMode;
                fnDoDarkMode(SysPara.MainPara.bDarkModeActive);
            }
            #endregion

            #region Labels cycle time

            if (tmr_UpdateLabels.On(250) && SysPara.SystemRun && SysPara.SystemMode == RunMode.AUTO)
            {
                fnSetCycleTime();
                tmr_UpdateLabels.Restart();
            }

            #endregion

            #region UnitCounter

            bool bUseJidoka = GetData(EnableJidoka);

            if (bUseJidoka && a_ProcessData != null)
            {
                int iIndex = 0;

                foreach (var pf in a_ProcessData)
                {
                    bool bPartialEnabled = Convert.ToBoolean(dt_Process.Rows[iIndex][cl_b_EnableJidokaAlarm.ColumnName]);

                    if (bPartialEnabled && pf.Value != null && pf.Value.Jidoka_Alarmed)
                        SDKKernal.ShowAlarm(sAlarm.E_Stop("ProcessForm", this), "Jidoka Excedido");

                    iIndex++;
                }
            }
            #endregion
            #region Alarm DryCycle
            if (SysPara.bDryCycle)
            {
                bStateDryCycle = true;
                SDKKernal.ShowAlarm(sAlarm.I("ProcessForm", this), "Modo Seco Habilitado desde Producction Settings...");

            }
            else if (!SysPara.bDryCycle && bStateDryCycle)
            {
                bStateDryCycle = false;
                SDKKernal.ClearAlarm(sAlarm.I("ProcessForm", this));
            }
            #endregion
        }
        public override void AfterInitSDK()
        {

        }
        #endregion

        #region------------------------------ Controles Manuales

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            if (a_ProcessData != null)
            {
                foreach (var item in a_ProcessData)
                {
                    item.Value.fnResetGENERAL();
                    DataTable dt = MiddleLayer.ProcessF.RecipeData.Tables["RSet"];
                    dt.Rows[0]["TotalCount"] = item.Value.nTotalUnits;
                    dt.Rows[0]["OKCount"] = item.Value.nCountGoodUnits;
                    dt.Rows[0]["PalletCount"] = item.Value.nPalletCount;
                    dt.AcceptChanges();
                    MiddleLayer.ProcessF.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                }
            }
            MiddleLayer.GantryF.processData.InitCount();

        }

        #endregion

        #region------------------------------ Funciones Generales        


        private ProcessDataForm fnAddProcessDataForm(string sKeyForm, bool bEnableJidokaAlarm = false, int nJidokaCountLimit = -1, bool bIOT_Resport = false, bool bEnableLog = false, string sComent = "")
        {
            ProcessDataForm _pForm;
            bool bFinded = a_ProcessData.TryGetValue(sKeyForm, out _pForm);

            if (!bFinded)
            {
                bFinded = false;

                foreach (DataRow dtRow in dt_Process.Rows)
                {
                    string sModuleName = dtRow[cl_NameProcess.Ordinal].ToString();

                    if (sModuleName == sKeyForm)
                    {
                        bFinded = true;
                        nJidokaCountLimit = Convert.ToInt32(dtRow[cl_n_JidokaLimite.Ordinal]);
                        bEnableJidokaAlarm = Convert.ToBoolean(dtRow[cl_b_EnableJidokaAlarm.Ordinal]);
                        bEnableLog = Convert.ToBoolean(dtRow[dc_bLogProdcution.Ordinal]);
                        bIOT_Resport = Convert.ToBoolean(dtRow[dc_bReportAcuraIOT.Ordinal]);
                    }
                }

                if (!bFinded)
                {
                    var defCount = GetData(JidokaUnits);
                    nJidokaCountLimit = nJidokaCountLimit != -1 ? nJidokaCountLimit : (defCount != null ? (int)defCount : 3);
                    DataRow _dt = dt_Process.Rows.Add(sKeyForm, bEnableJidokaAlarm, nJidokaCountLimit, bIOT_Resport, bEnableLog, sComent != "" ? sComent : sKeyForm);
                    //Save 
                    this.WriteSettingData();
                }

                _pForm = new ProcessDataForm();
                _pForm.sTopName = sKeyForm;
                _pForm.nJidokaLimit = (uint)nJidokaCountLimit;
                _pForm.bJidokaEnabled = bEnableJidokaAlarm;
                _pForm.bEnableLOG = bEnableLog;
                _pForm.bEnableIOT = bIOT_Resport;
                _pForm.fnResetGENERAL();
                a_ProcessData.Add(sKeyForm, _pForm);
            }

            return _pForm;
        }

        /// <summary>
        /// Realiza el cambio a dark o light mode
        /// </summary>
        /// <param name="bEnabled"></param>
        private void fnDoDarkMode(bool bEnabled)
        {

        }


        /// <summary>
        /// Si esta habilitado Mes, entra una vez para crear el Tar
        /// Retorna true si esta deshabillitado o si ya termino de crear el Tar
        /// </summary>
        /// <returns></returns>
        private bool fnWriteMes()
        {
            bool _enabledMes = GetSettingValue(PSet.TableName, EnableMes.ColumnName);

            if (_enabledMes && bInJustOnce)
            {
                bInJustOnce = false;
                SysPara.MESPara.WriteUnitTars = true;
            }

            if (SysPara.MESPara.WriteUnitTarsOk || !_enabledMes)
            {
                SysPara.MESPara.WriteUnitTarsOk = false;
                bInJustOnce = true;
                return true;
            }

            return false;
        }

        #region Funciones Publicas


        /// <summary>
        /// Evalua el estado y las fallas de jidoka
        /// Retorna true cuando se cumplen
        /// Agrega el contador de unidades a la etiqueta
        /// </summary>
        /// <returns></returns>
        public bool fnFailsJidokaDone(int _jidoka)
        {
            bool _JidokaEnabled = GetSettingValue(PSet.TableName, EnableJidoka.ColumnName);
            uint _FailsAllowed = GetSettingValue(PSet.TableName, JidokaUnits.ColumnName);
            bool bJidokaModuleFail = a_ProcessData != null;

            if (bJidokaModuleFail)
            {
                foreach (var item in a_ProcessData)
                    bJidokaModuleFail &= item.Value.nJidokaCount >= _FailsAllowed;
            }

            return (bJidokaModuleFail && _JidokaEnabled);
        }
        /// <summary>
        /// Guarda el tiempo de ciclo y realiza el reset del stopwatch 
        /// </summary>
        public void fnSetCycleTime()
        {
            if (a_ProcessData != null)
            {
                try
                {
                    foreach (var item in a_ProcessData)
                    {
                        if (item.Value != null)
                            item.Value.fnSetCycleTime();
                    }
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                }
            }
        }

        private void plMachineStatus_Paint(object sender, PaintEventArgs e)
        {
            if (a_ProcessData != null)
            {
                Color clTheme = MiddleLayer.ModeColors.Background;

                foreach (var item in a_ProcessData)
                {
                    if (item.Value != null)
                        item.Value.ColorTheme = clTheme;
                }

            }
        }

        #endregion

        #endregion

        public ProcessDataForm fnGetDataForm(string sNameId)
        {
            return a_ProcessData.FirstOrDefault(pd => pd.Value.sTopName == sNameId).Value;
        }
        private void btnInitProcess_Click(object sender, EventArgs e)
        {
            fnInitMachineStateProcessForm();

            if (SysPara.SystemInitialOk)
            {
                DialogResult result = MessageBox.Show(new Form { TopMost = true }, "La inicializacion de los modulos solo se puede ejecutar cuando no esta corriendo el sistema", "Init", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            }
        }

        private void DisposeProcessData()
        {
            if (a_ProcessData != null)
            {
                a_ProcessData.Clear();

                foreach (var item in a_ProcessData)
                {
                    if (item.Value != null)
                        item.Value.Dispose();
                }
            }
        }

        private void fnInitMachineStateProcessForm()
        {
            if (SysPara.SystemInitialOk)
                return;

            //Limpia 
            DisposeProcessData();
            tlp_Show.Controls.Clear();

            if (dt_Process.Rows != null)
            {
                int nCount = dt_Process.Rows.Count;
                int nIterator = 0;

                if (nCount > 2)//TabControl
                {
                    //TabControl
                    TabControl tbControl = new TabControl();

                    foreach (DataRow dtRow in dt_Process.Rows)
                    {
                        string sModuleName = dtRow[cl_NameProcess.Ordinal].ToString();
                        TabPage tg = new TabPage(sModuleName);
                        tg.Padding = new Padding(0);
                        ProcessDataForm pDF = fnAddProcessDataForm(sModuleName);
                        tg.fnControlAdd(pDF.pn_Main);
                        tbControl.fnControlAdd(tg);
                        nIterator++;
                    }

                    tbControl.Appearance = TabAppearance.Normal;
                    tbControl.SizeMode = TabSizeMode.Fixed;
                    tbControl.Dock = DockStyle.Fill;

                    if (nIterator != 0)
                    {
                        Size sParent = plMachineStatus.Size;
                        Size sDividers = tbControl.ItemSize;
                        sDividers.Width = (sParent.Width - 20) / nCount;
                        sDividers.Width = sDividers.Width < 150 ? 150 : sDividers.Width;
                        sDividers.Height = 40;
                        tbControl.ItemSize = sDividers;
                    }

                    tlp_Show.fnLayoutControlAdd(tbControl, 0, 0);
                    tlp_Show.SetColumnSpan(tbControl, tlp_Show.ColumnCount);
                }
                else  //TableLayoutPanel
                {
                    //TableLayoutPanel
                    foreach (DataRow dtRow in dt_Process.Rows)
                    {
                        string sModuleName = dtRow[cl_NameProcess.Ordinal].ToString();
                        ProcessDataForm pDF = fnAddProcessDataForm(sModuleName);
                        tlp_Show.fnLayoutControlAdd(pDF.pn_Main, nIterator, 0);

                        if (nCount == 1)
                            tlp_Show.SetColumnSpan(pDF.pn_Main, tlp_Show.ColumnCount);

                        nIterator++;
                    }
                }
            }
        }
        public bool getCheckPoint(string sSerial)
        {
            try
            {
                if (GetData(dcEnableCheckProcess))
                {
                    int iCustomerID = GetData(dataColumn_CustomerID);
                    string sCheckPointName = GetData(dcCheckProcessID);
                    clsCheckPoint objCheckPoint = new clsCheckPoint();
                    objCheckPoint.m_iCustomerID = iCustomerID;
                    objCheckPoint.m_sCheckPointName = sCheckPointName;
                    var CheckPointResult = objCheckPoint.CheckPointStatus(sSerial);
                    return CheckPointResult.Item1;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        private void myCheckBox11_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(new Form { TopMost = true }, "Los Cambios solo se carga durante la inicializacion, guarde cambios", "Modo Seco", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
        }

        private async void btn_TestCheck_Click(object sender, EventArgs e)
        {



            //if (MiddleLayer.ConveyorF._DataRegisterCheckPointByRAC[0] == null)
            //{

            //    MiddleLayer.ConveyorF._DataRegisterCheckPointByRAC[0] = MiddleLayer.ConveyorF._CheckPointByRAC(txbSerialToCheck.Text, sCheckProcessID);
            //    //MessageBox.Show(MiddleLayer.ConveyorsF._DataRegisterCheckPointByRAC.Result.bErrors);
            //    //MiddleLayer.ConveyorsF._DataRegisterCheckPointByRAC = null;
            //}
            //await Task.Delay(1000);
            //if (MiddleLayer.ConveyorF._DataRegisterCheckPointByRAC[0].IsCompleted)
            //{
            //    MessageBox.Show(MiddleLayer.ConveyorF._DataRegisterCheckPointByRAC[0].Result.bErrors);
            //    MiddleLayer.ConveyorF._DataRegisterCheckPointByRAC[0] = null;
            //}
            //else
            //{
            //    MessageBox.Show("Error en consulta de web service");
            //    MiddleLayer.ConveyorF._DataRegisterCheckPointByRAC[0] = null;


            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void myCheckBox11_Click_1(object sender, EventArgs e)
        {
            if (myCheckBox11.Checked)
            {
                //SettingData - PSet.DisableVision
                //SettingData - PSet.DisableFeeder

                // SettingData - MSet.EPlaningCounts
                // SettingData - PSet.RobotSafetyMode
                // SettingData - PSet.IsDryCycleMode
                //SettingData - PSet.HabilitarModuloScanner
                DataTable dt = SettingData.Tables["PSet"];
                dt.Rows[0]["DisableCheckQRcodes"] = true;
                dt.AcceptChanges();
                DataTable dt1 = SettingData.Tables["PSet"];
                dt1.Rows[0]["IsDryCycleMode"] = true;
                dt1.AcceptChanges();

                DataTable dt2 = MiddleLayer.SystemF.SettingData.Tables["PSet"];
                dt2.Rows[0]["RobotSafetyMode"] = true;
                dt2.AcceptChanges();

                DataTable dt3 = MiddleLayer.GantryF.SettingData.Tables["MSet"];
                dt3.Rows[0]["EPlaningCounts"] = false;
                dt3.AcceptChanges();
                DataTable dt4 = MiddleLayer.SystemF.SettingData.Tables["PSet"];
                dt4.Rows[0]["DisableFeeder"] = true;
                dt4.AcceptChanges();
                DataTable dt5 = SettingData.Tables["PSet"];
                dt5.Rows[0]["disableCheckPCBcodes"] = true;
                dt5.AcceptChanges();

                DataTable dt6 = MiddleLayer.SystemF.SettingData.Tables["PSet"];
                dt6.Rows[0]["DisableVision"] = true;
                dt6.AcceptChanges();




                this.WriteSettingData();

                MiddleLayer.GantryF.myCheckBox5.Enabled = false;

                //MiddleLayer.ScannerF.myCheckBox1.Checked = false;
                //MiddleLayer.SystemF.myCheckBox1.Checked = true;
            }
            else
            {
                DataTable dt = SettingData.Tables["PSet"];
                dt.Rows[0]["DisableCheckQRcodes"] = false;
                dt.AcceptChanges();
                DataTable dt1 = SettingData.Tables["PSet"];
                dt1.Rows[0]["IsDryCycleMode"] = false;
                dt1.AcceptChanges();

                DataTable dt2 = MiddleLayer.SystemF.SettingData.Tables["PSet"];
                dt2.Rows[0]["RobotSafetyMode"] = false;
                dt2.AcceptChanges();

                DataTable dt4 = MiddleLayer.SystemF.SettingData.Tables["PSet"];
                dt4.Rows[0]["DisableFeeder"] = false;
                dt4.AcceptChanges();

                DataTable dt5 = SettingData.Tables["PSet"];
                dt5.Rows[0]["disableCheckPCBcodes"] = false;
                dt5.AcceptChanges();

                DataTable dt6 = MiddleLayer.SystemF.SettingData.Tables["PSet"];
                dt6.Rows[0]["DisableVision"] = false;
                dt6.AcceptChanges();


                this.WriteSettingData();
                MiddleLayer.GantryF.myCheckBox5.Enabled = true;
                //MiddleLayer.ScannerF.myCheckBox1.Checked = true;
                //MiddleLayer.SystemF.myCheckBox1.Checked = false;
            }
        }
    }
}

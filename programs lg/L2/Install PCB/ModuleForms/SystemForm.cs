using AcuraLibrary.Forms;
using NPSDK;
using static Alpha.Forms.SignalTowerForm;
using Alpha.Classes;
using TcpipHelper;

namespace Alpha.ModuleForms
{
    public partial class SystemForm : ModuleBaseForm
    {
        #region------------------------------ Variables        

        private bool bStateChange_SafetyReady = false;
        private bool bMaintenanceModeActivated = false;
        private bool bStartButtonStateChanged = true;
        private bool bStopButtonStateChanged = true;
        private bool StatusChange_SafetyForntDoor = false;
        private bool StatusChange_SafetyBackDoor = false;
        protected JTimer JT_Flash = new JTimer();
        protected JTimer jt_RunTM = new JTimer();
        private bool bFlashOn = false;
        public bool bBuzzWorking = false;
        public bool bExecuteOnlyOnceAir = true;
        bool airPressureAlarmed = false;
        public bool bSaftyReady;
        bool bFrontEmergencyStopActivated = false, bRearEmergencyStopActivated = false;

        //public NPOutput OB_ResetMotorAlarm { get { return OB_DriversReset; } }


        public bool bTurnOnLightOnDoorOpen { get { return GetData(AutoOpenFluorescentLight); } }

        #endregion

        public SystemForm()
        {
            #region tabControl
            //plMaintenance.Enabled = true;
            //plProductionSetting.Enabled = true;
            //plRecipeEditor.Enabled = false;
            //plFlowInitial.Enabled = true;
            //plFlowAuto.Enabled = false;
            //plMachineStatus.Enabled = false;
            //plMotionSetup.Enabled = false;
            //plMotorControl.Enabled = false;
            #endregion

            InitializeComponent();
        }

        #region------------------------------ Funciones Generales        

        /// <summary>
        /// Change motors Speed to slow in mtto mode
        /// </summary>
        private void fnSwitchMotorToMaintenanceSpeed()
        {
            foreach (ControlBaseInterface control in SDKPara.ControlList)
                if (control is NPMotor)
                    if (((NPMotor)control).MaxSpeed > 33)
                    {
                        ((NPMotor)control).MaxSpeed = 33;
                    }
        }

        /// <summary>
        /// Controla el status de la torreta
        /// </summary>
        /// <param name="status"></param>
        /// <param name="OB"></param>
        private void fnControl_SignalTower(Status status,NPSDK.NPOutput  OB)
        {
            switch (status)
            {
                case Status.Off:
                    OB.Off();
                    break;
                case Status.On:
                    OB.On();
                    break;
                case Status.Blink:
                    if (bFlashOn)
                        OB.On();
                    else
                        OB.Off();
                    break;
                case Status.Trans:
                    //Continues the same Mode
                    break;
            }
        }

        #region Funciones Publicas

        /// <summary>
        /// Apaga el buzzer
        /// </summary>
        public void BuzzOff()
        {
            bBuzzWorking = false;
            fnControl_SignalTower(Status.Off, OB_Buzzer);
        }
        public void BuzzOn()
        {
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "EnabledBuzz"))
            {
                bBuzzWorking = true;
                fnControl_SignalTower(Status.On, OB_Buzzer);
            }

        }
      
        #endregion

        #endregion


        #region------------------------------ Override Inicial


        public override void Initial()
        {
            bInitialOk = true;
            base.Initial();
        }

        public override void StartRun()
        {
            //OB_LeftDoorCyDown.Off();
            //OB_LeftDoorCyUp.Off();
        }

        bool bDoorOpened = false;

        private void IB_FrontEmergencyStop_Click(object sender, System.EventArgs e)
        {

        }

        private void IB_ModeSwitch_Click(object sender, System.EventArgs e)
        {

        }

        private void IB_AirPressure_Click(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            SysPara.IsEnglish = !SysPara.IsEnglish;
            MiddleLayer.switchLanguage.AutoSwitchLanguage(SysPara.IsEnglish);
        }

        private void panel5_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void cmb_ModelSelect_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
        }

        private void cmb_ModelSelect_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var recipestr = cmb_ModelSelect.SelectedItem.ToString().ToUpper();
            if (SysPara.EnableMes)
            {
                if (MESLib.CommParas.MesManager._connector != null)
                {
                    MESLib.CommParas.MesManager._connector.SV.CurrentPPID.content.EquipmentID = new VarItem<string>("10001", MESLib.CommParas.MesManager._connector._Ec.EquipmentID_1.content.ToString());
                    MESLib.CommParas.MesManager._connector.SV.CurrentPPID.content.EquipmentName = new VarItem<string>("10002", MESLib.CommParas.MesManager._connector._Ec.EquipmentName_1.content.ToString());
                    MESLib.CommParas.MesManager._connector.SV.CurrentPPID.content.CurrentPPID = new VarItem<string>("10135", recipestr);
                    MESLib.CommParas.MesManager._connector.SV.CurrentPPID.content.CurrentPPIDVersion = new VarItem<string>("10136", "1.0.0");

                    MESLib.CommParas.MesManager._connector.SV.CurrentPPIDList.content.EquipmentName = new VarItem<string>("10001", MESLib.CommParas.MesManager._connector._Ec.EquipmentName_1.content.ToString());
                    MESLib.CommParas.MesManager._connector.SV.CurrentPPIDList.content.CurrentPPID = new VarItem<string>("10135", recipestr);
                    MESLib.CommParas.MesManager._connector.SV.CurrentPPIDList.content.CurrentPPIDVersion = new VarItem<string>("10136", "1.0.0");
                }
            }
        }

        public override void AlwaysRun()
        {
            #region Simulation
            if (SysPara.Simulation)
                return;
            #endregion

            #region Start/Stop Button

            if (IB_Start.On() && bStartButtonStateChanged && MiddleLayer.MainF.btnStart.Enabled)
            {
                bStartButtonStateChanged = false;
                MiddleLayer.MainF.fnStart();
            }
            else if (IB_Start.Off())
                bStartButtonStateChanged = true;

            if (IB_Stop.On() && bStopButtonStateChanged)
            {
                bStopButtonStateChanged = false;
                MiddleLayer.StopRun();
            }
            else if (IB_Stop.Off())
                bStopButtonStateChanged = true;

            #endregion

            #region FlashTimer
            if (JT_Flash.On(500))
            {
                bFlashOn = !bFlashOn;
                JT_Flash.Restart();
            }
            #endregion

            #region Emergency Stop Scan
            //if (IB_FrontEmergencyStop.Off() && !bFrontEmergencyStopActivated)
            //{
            //    SysPara.SystemRun = false;
            //    SysPara.SystemInitialOk = false;
            //    SysPara.SystemMode = RunMode.IDLE;
            //    MiddleLayer.StopRun();
            //    MiddleLayer.ServoOff();
            //    SDKPara.Arm.ReportAlarm("40026", AcuraIOT.ErrorGroup.EmergencyStop, AcuraIOT.ErrorSubGroup.Detection, AcuraIOT.ErrorType.ES, mNameAlm.System + "Paro de emergencia frontal activado");
            //    SysPara.NPShowAlarm("40026");
            //    bFrontEmergencyStopActivated = true;
            //}
            //else if (bFrontEmergencyStopActivated && IB_FrontEmergencyStop.On())
            //{
            //    SDKPara.Arm.ReportClearAlarm("40026");
            //    bFrontEmergencyStopActivated = false;
            //}


            #endregion
            if (MiddleLayer.GantryF.IB_RobotLimit.Off())
            {
                SysPara.SystemRun = false;
                SysPara.SystemInitialOk = false;
                SysPara.SystemMode = RunMode.IDLE;
                MiddleLayer.StopRun();
                MiddleLayer.ServoOff();
                SysPara.NPShowAlarm("1633");
            }


            #region Safty ready Scan
            if (IB_SaftyReady.On())//ON
            {
                if (!bStateChange_SafetyReady)
                {
                    bStateChange_SafetyReady = true;
                    MiddleLayer.MotorAlarmReset();
                    SDKPara.Arm.ReportClearAllAlarm();
                }
                SysPara.NPShowAlarm("0001");
                SysPara.SystemPara.bSaftyReady = true;
                bSaftyReady = true;
            }
            else
            {
                SDKKernal.ClearAlarm("0001");
                
                if (bStateChange_SafetyReady)
                { bStateChange_SafetyReady = false; }
                   

                SysPara.SystemPara.bSaftyReady = false;
                bSaftyReady = false;
            }
            #endregion

            //#region Safty door Scan

            //if (IB_LeftDoorUpSensor.On() || IB_LeftDoorDownSensor.On())
            //{
            //    if (IB_ModeSwitch.Off() && IB_SaftyReady.Off())
            //    {
            //        SysPara.SystemRun = false;
            //        SysPara.SystemInitialOk = false;
            //        SysPara.SystemMode = RunMode.IDLE;
            //        MiddleLayer.StopRun();
            //    }

            //    if (!bDoorOpened)
            //    {
            //        if (bTurnOnLightOnDoorOpen)
            //            OB_FluorescentLight.On();
            //    }

            //    string sDescription = "";

            //    if (IB_LeftDoorUpSensor.Off())
            //        sDescription = "La puerta frontal esta abierta";
            //    else if (IB_LeftDoorDownSensor.Off())
            //        sDescription = "La puerta trasera esta abierta";

            //    SDKPara.Arm.ShowAlarm("3025", mNameAlm.System + sDescription);

            //    //  SDKPara.Arm.ReportAlarm("3025", AcuraIOT.ErrorGroup.Door, AcuraIOT.ErrorSubGroup.Alarm, AcuraIOT.ErrorType.W, mNameAlm.System + sDescription);
            //    bDoorOpened = true;
            //}
            //else if (bDoorOpened)
            //{
            //    if (bTurnOnLightOnDoorOpen)
            //        OB_FluorescentLight.Off();
            //    bDoorOpened = false;

            //    SDKPara.Arm.ReportClearAlarm("3025");
            //}
            //#endregion

            #region MaintainMode Scan
            if (IB_ModeSwitch.On())
            {

                if (bFlashOn) OB_StartOrStopIndecator.On();
                else OB_StartOrStopIndecator.Off();

                if (!bMaintenanceModeActivated)
                {
                    //Light Control
                    //if (bTurnOnLightOnDoorOpen)
                        OB_FluorescentLight.On();

                    //OB_LeftDoorCyDown.On();
                    //OB_LeftDoorCyUp.On();

                    MiddleLayer.StopRun();
                    SDKKernal.BackupMotorMaxSPD();
                    fnSwitchMotorToMaintenanceSpeed();
                    bMaintenanceModeActivated = true;

                    SysPara.NPShowAlarm("0012");
                }

                SysPara.IsMaintenanceMode = true;
            }
            else
            {
                //Light Control
                if (SysPara.SystemInitialOk) OB_StartOrStopIndecator.On();
                else OB_StartOrStopIndecator.Off();

                if (bMaintenanceModeActivated)
                {
                    //Light Control
               //     if (bTurnOnLightOnDoorOpen)
                        OB_FluorescentLight.Off();

                    //OB_LeftDoorCyDown.Off();
                    //OB_LeftDoorCyUp.Off();
                    MiddleLayer.StopRun();
                    SDKKernal.RestoreMotorMaxSPD();
                    SDKKernal.ClearAlarm("90012");
                    bMaintenanceModeActivated = false;
                }

                SysPara.IsMaintenanceMode = false;
            }

            #endregion

            #region Air Pressure Scan
            if (IB_AirPressure.Off())
            {
                if (bExecuteOnlyOnceAir)
                {
                    jt_RunTM.Restart();
                    bBuzzWorking = true;
                    bExecuteOnlyOnceAir = false;
                   
                }

                //if (jt_RunTM.On(500))
                //{
                    airPressureAlarmed = true;
                    SysPara.NPShowAlarm("0002");
                   
                    jt_RunTM.Restart();
                //}
            }

            if (airPressureAlarmed && IB_AirPressure.On())
            {
                airPressureAlarmed = false;
                bExecuteOnlyOnceAir = true;
                SDKKernal.ClearAlarm("0002");
                bBuzzWorking = false;
            }

            #endregion

            #region Signal Tower Control
            //Refresh Light State



            
            if (SDKPara.Arm._InformNow)
                MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MessageInformation);
            else if (SDKPara.Arm._WarningNow)
                MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MessageWarning);
            else if (SDKPara.Arm._ErrorNow)
                MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MessageError);
            else if (SysPara.SystemRun)//Running
            {
                if (SysPara.SystemMode == RunMode.AUTO)
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MachineRunning);
                else if (SysPara.SystemMode == RunMode.HOME)
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MachineInitialize);
                else
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MachineIdle);
            }
            else//Stop
            {
                MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MachineIdle);
            }

            //Control Signal Tower
            fnControl_SignalTower(MiddleLayer.SignalTowerF.GreenLightStatus, OB_AlarmLightGreen);
            fnControl_SignalTower(MiddleLayer.SignalTowerF.YellowLightStatus, OB_AlarmLightYellow);
            fnControl_SignalTower(MiddleLayer.SignalTowerF.RedLightStatus, OB_AlarmLightRed);

            if (MiddleLayer.SignalTowerF.bHaveChangeStatus)
            {
                bBuzzWorking = true;
                MiddleLayer.SignalTowerF.bHaveChangeStatus = false;
            }
            if (bBuzzWorking && GetSettingValue(PSet.TableName, EnabledBuzz.ColumnName))
                fnControl_SignalTower(MiddleLayer.SignalTowerF.BuzzerStatus, OB_Buzzer);
            #endregion
            //#region Safety fornt door Scan
            //if (IB_ForntDoor.Off())
            //{
            //    if (!StatusChange_SafetyForntDoor)
            //    {
            //        StatusChange_SafetyForntDoor = true;
            //        if (IB_ForntDoor.Off())
            //        {
            //            SDKPara.Arm.ReportAlarm("04303", AcuraIOT.ErrorGroup.Door, AcuraIOT.ErrorSubGroup.Alarm, AcuraIOT.ErrorType.E, mNameAlm.System + "open fornt safety door");
            //            SysPara.NPShowAlarm("04303");
            //        }
            //    }
            //}
            //else
            //{
            //    if (StatusChange_SafetyForntDoor)
            //    {
            //        SDKKernal.ClearAllAlarm();
            //        StatusChange_SafetyForntDoor = false;
            //    }
            //}
            //#endregion

            //#region Safety back door Scan
            //if (IB_BackDoor.Off())
            //{
            //    if (!StatusChange_SafetyBackDoor)
            //    {
            //        //OB_FluorescentLight.On();
            //        StatusChange_SafetyBackDoor = true;
            //        if (IB_BackDoor.Off())
            //        {
            //            SDKPara.Arm.ReportAlarm("04305", AcuraIOT.ErrorGroup.Door, AcuraIOT.ErrorSubGroup.Alarm, AcuraIOT.ErrorType.E, mNameAlm.System + "open back safety door");
            //            SysPara.NPShowAlarm("04305");
            //        }
            //    }
            //}
            //else
            //{
            //    if (StatusChange_SafetyBackDoor)
            //    {
            //        SDKKernal.ClearAllAlarm();
            //        StatusChange_SafetyBackDoor = false;
            //    }
            //}
            //#endregion 
            #region Gate always Open
            //MiddleLayer.ConveyorF.GateAlwaysOpen();
            #endregion
        }
        #endregion

    }
}
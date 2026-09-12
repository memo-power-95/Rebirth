using AcuraLibrary.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using NPSDK.Enums;
using NPSDK;
using Alpha.Classes;
using System.IO;
using Alpha._0.Classes;
using Alpha.FunctionForms;
using static Alpha.FunctionForms.LogForm;

namespace Alpha._0.ModuleForms
{
    public partial class ConveyorForm : ModuleBaseForm
    {

        public JTimer ConveyorInitTimeout = new JTimer();
        public JTimer Conveyor1Timeout = new JTimer();
        public bool assambSuccess = true;
        public bool isAssambling = false;
        JTimer DownConveyorTimeout = new JTimer();
        JTimer DownConveyorAutoTimeout = new JTimer();
        JTimer DownConveyorAutoTimeout2 = new JTimer();
        public bool downInitialStatus = false;
        public bool upInitialStatus = false;
        public bool isdownInitialOK = false;

        public bool IsBoardINOUT = false;

        JTimer InitialStart = new JTimer();
        JTimer tmr_Conveyor1 = new JTimer();
        JTimer tmr_Conveyor2 = new JTimer();

        public ProcessDataForm processData = MiddleLayer.ProcessF.fnGetDataForm("Alpha");
        public string StartCT;

        public string OverCT;

        int ConveyorCountOK = 0;

        int ConveyorCountNG = 0;

        public bool UPAnalogBoard = false;
        public bool UPLowerKeyBoard = false;
        public bool DownAnalogBoard = false;
        public bool DownLowerKeyBoard = false;
        public bool LastUnitInProcess = false;
        public bool InitialUnitInExitPosition = false;

        public int ScanNG = 0;
        public bool LocalMachineNG = false;

        public bool ConveyorOK = false;

        public bool Priority = false;

        public bool ZAxisPost = false;

        public bool smCallboard = false;
        public bool upboardSmema = false;

        cSmartAlarm DynamicAlarm = new cSmartAlarm();
        private Task<ScannerForm.ScannerReadResult> taskGetScannerCode = null;
        private bool bSimulateRobotDone = false;
        private bool bUnitSmemaNG = false;
        private bool bUnitForExpulse = false;
        private string sSerialNumber = "";
        ProcessDataForm pdfProcess = new ProcessDataForm();
        AutoResetEvent upST = new AutoResetEvent(false);
        //downConvryor 
        float downInputFlow = 0;
        float downOutputFlow = 0;

        public UC_DownConveyorAutoFlow DownConveyorAutoFlow;
        public UC_UpConveyorAutoFlow.UC_UpConveyorAutoFlow UpConveyorAutoFlow;

        DownConveyorSMMStare downConveyorSMMStare = new DownConveyorSMMStare();
        DownConveyorMtRevStare downConveyorMtRevStare = new DownConveyorMtRevStare();
        DownConveyorProductStare downConveyorProductStare = new DownConveyorProductStare();
        private enum DownConveyorSMMStare
        {
            Input,
            Output,
            None
        }
        private enum DownConveyorMtRevStare
        {
            Start,
            stop,
            None
        }
        private enum DownConveyorProductStare
        {
            Input,
            Output,
            None
        }
        public ConveyorForm()
        {
            InitializeComponent();
            this.plMachineStatus.Enabled = false;
            this.plRecipeEditor.Enabled = false;
            //DynamicAlarm.AddRange(fc_Initial_Start);
            //DynamicAlarm.AddRange(fc_Auto_Start);
            //pdfProcess = MiddleLayer.ProcessF.fnGetDataForm("");
            InitMotorControl(ref DownConveyorAutoFlow, panel_MoveFlowModule);
            InitUpMotorControl(ref UpConveyorAutoFlow, panel_UpFlowModule);
        }

        /// <summary>
        /// Initialize mod control (初始化模组控制)
        /// </summary>
        /// <param name="motorMoveCtrl">Mod Control(模组控件)</param>
        /// <param name="panel_MoveFlowModule">(Parent panel)插入的父面板</param>
        private void InitMotorControl(ref UC_DownConveyorAutoFlow motorMoveCtrl, Panel panel_MoveFlowModule)
        {
            if (DownConveyorAutoFlow == null)
                DownConveyorAutoFlow = new UC_DownConveyorAutoFlow();
            DownConveyorAutoFlow.Dock = DockStyle.Fill;
            panel_MoveFlowModule.Controls.Add(DownConveyorAutoFlow);
        }
        private void InitUpMotorControl(ref UC_UpConveyorAutoFlow.UC_UpConveyorAutoFlow motorMoveCtrl, Panel panel_UpFlowModule)
        {
            if (UpConveyorAutoFlow == null)
            {
                UpConveyorAutoFlow = new UC_UpConveyorAutoFlow.UC_UpConveyorAutoFlow();
                UpConveyorAutoFlow.FnIndicateStartingUnit = processData.fnIndicateStartingUnit;
                UpConveyorAutoFlow.FnIndicateFinishedUnit = processData.fnIndicateFinishedUnit;
                UpConveyorAutoFlow.SwCycleTimeStart = processData.swCycleTime.Start;
                UpConveyorAutoFlow.SwCycleTimeStop = processData.swCycleTime.Stop;
                UpConveyorAutoFlow.ShowAlarm = SysPara.NPShowAlarm;
                UpConveyorAutoFlow.AddLog = MiddleLayer.LogF.AddLog;

                UpConveyorAutoFlow.IB_SMEMAUPFailAvailable = IB_SMEMAUPFailAvailable;
                UpConveyorAutoFlow.IB_SMEMAUpStreamBoardAvailable = IB_SMEMAUpStreamBoardAvailable;
                UpConveyorAutoFlow.IB_SMEMAUpStreamMachineReady = IB_SMEMAUpStreamMachineReady;
                UpConveyorAutoFlow.IB_UpCvyBlockCyOut = IB_UpCvyBlockCyOut;
                UpConveyorAutoFlow.IB_UpCvyDecelerationSensor = IB_UpCvyDecelerationSensor;
                UpConveyorAutoFlow.IB_UpCvyDischargeSensor = IB_UpCvyDischargeSensor;
                UpConveyorAutoFlow.IB_UpCvyFeedSensor = IB_UpCvyFeedSensor;
                UpConveyorAutoFlow.IB_UpCvyInPlaceSensor = IB_UpCvyInPlaceSensor;
                UpConveyorAutoFlow.IB_UpCvyJackingCyBack = IB_UpCvyJackingCyBack;
                UpConveyorAutoFlow.IB_UpCvyJackingCyOut = IB_UpCvyJackingCyOut;
                UpConveyorAutoFlow.OB_SMMachineReady = OB_SMMachineReady;
                UpConveyorAutoFlow.OB_SM_UpBoardReady = OB_SM_UpBoardReady;
                UpConveyorAutoFlow.OB_SM_UpFaliBoard = OB_SM_UpFaliBoard;
                UpConveyorAutoFlow.OB_UpCvyBlockCyUp = OB_UpCvyBlockCyOut;
                UpConveyorAutoFlow.OB_UpCvyMtFwd = OB_UpCvyMtFwd;
                UpConveyorAutoFlow.OB_UpCvyMtSpeed = OB_UpCvyMtSpeed;
                //UpConveyorAutoFlow.Cy_LeftDoorUnite = MiddleLayer.SystemF.Cy_LeftDoorUnite;
                //UpConveyorAutoFlow.Cy_RightDoorUnite = MiddleLayer.SystemF.Cy_RightDoorUnite;
                UpConveyorAutoFlow.Cy_ST1UpStreamLiftingUnite = Cy_ST1UpStreamLiftingUnite;
            }
            UpConveyorAutoFlow.Dock = DockStyle.Fill;
            panel_UpFlowModule.Controls.Add(UpConveyorAutoFlow);
        }

        public override void InitialReset()
        {
            flowChart1.TaskReset();
            BottomConveyorInitFlow.TaskReset();
        }

        public override void Initial()
        {
            flowChart1.TaskRun();
            BottomConveyorInitFlow.TaskRun();

        }
        public override void RunReset()
        {
            //flowChart7.TaskReset();
            UpConveyorAutoFlow.fc_UpConveryStart.TaskReset();
            UpConveyorAutoFlow.subFlowChartStart.TaskReset();
        }

        public override void Run()
        {
            //flowChart7.TaskRun();
            UpConveyorAutoFlow.fc_UpConveryStart.TaskRun();
            UpConveyorAutoFlow.subFlowChartStart.TaskRun();
        }


        public override void StartRun()
        {
            //if(fc_Initial_Start.tmrTimeOut !=null)
            //    fc_Initial_Start.tmrTimeOut.Restart();
            //if (fc_Auto_Start.tmrTimeOut != null)
            //    fc_Auto_Start.tmrTimeOut.Restart();
        }
        public override void StopRun()
        {
            OB_UpCvyMtFwd.Off();
            OB_UpCvyMtRev.Off();
            //OB_TopCVY_BKW.Off();
            //OB_TopCVY_FWD.Off();
            //OB_BottomCVY_FWD.Off();
            //OB_BottomCVY_BKW.Off();

            //OB_SMEMA_TopCVY_Send.Off();
            //OB_SMEMA_TopCVY_Request.Off();
            //OB_SMEMA_TopCVY_SendNG.Off();
            //OB_SMEMA_TopCVY_SendOK.Off();
        }
        public override void AlwaysRun()
        {
            //if (isdownInitialOK)//初始化完成标志位
            //    BottomConveyorStartFlow.TaskRun();
        }


        public static void RefreshDifferentThreadUI(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                Action refreshUI = new Action(action);
                control.Invoke(refreshUI);
            }
            else
            {
                action.Invoke();
            }
        }
        /// <summary>
        /// Log  File
        /// </summary>
        /// <param name="describe"></param>
        /// <param name="filename">file name</param>
        /// <param name="flow_Chat_Name">flow_Chat_Name</param>
        /// <param name="boardNumber">Scann</param>
        public void SaveLogCSV(string describe, string filename, string flow_Chat = "", string boardNumber = "")
        {

            string log = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ";" + describe + "  " + boardNumber;
            string fullpath = "D://Log/Log/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + filename + ".CSV";
            FileInfo fi = new FileInfo(fullpath);
            if (!fi.Directory.Exists)
            { fi.Directory.Create(); }
            FileStream fs = new FileStream(fullpath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            sw.WriteLine(log);
            sw.Close();
            fs.Close();
        }
        #region ConveryInitial
        private FCResultType flowChart1_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = false;
            upInitialStatus = false;
            OB_SM_UpBoardReady.Off();
            OB_SM_UpFaliBoard.Off();


            processData.fnIndicateFinishedUnit();
            return FCResultType.NEXT;
        }
        //Wait Robot Initial finish
        private FCResultType flowChart2_FlowRun(object sender, EventArgs e)
        {
            lock (SysPara.StatusLock)
            {
                if (SysPara.RIniRet) { ConveyorInitTimeout.Restart(); return FCResultType.NEXT; }
                else { return FCResultType.IDLE; }
            }
        }
        // JackingCy back
        private FCResultType flowChart3_FlowRun(object sender, EventArgs e)
        {
            Cy_ST1UpStreamLiftingUnite.Off();
            if (IB_UpCvyJackingCyOut.Off() || IB_UpCvyJackingCyBack.On())
            {
                ConveyorInitTimeout.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                if (ConveyorInitTimeout.On(SysPara.TimeOutSec))
                { SysPara.NPShowAlarm("1004"); }     //Board support cylinder go down action overtime

                return FCResultType.IDLE;
            }
        }
        //BlockCy back
        private FCResultType flowChart4_FlowRun(object sender, EventArgs e)
        {
            OB_UpCvyBlockCyOut.On();
            if (IB_UpCvyBlockCyOut.On())
            { ConveyorInitTimeout.Restart(); return FCResultType.NEXT; }
            else
            {
                if (ConveyorInitTimeout.On(SysPara.TimeOutSec))
                { SysPara.NPShowAlarm("1006"); }     //Stop cylinder go down action overtime

                return FCResultType.IDLE;
            }
        }
        //UpCvyMtFwd off
        private FCResultType flowChart5_FlowRun(object sender, EventArgs e)
        {
            OB_UpCvyMtFwd.Off();
            ConveyorInitTimeout.Restart();
            return FCResultType.NEXT;
        }
        //Initial finish
        private FCResultType flowChart6_FlowRun(object sender, EventArgs e)
        {

            upInitialStatus = true;
            if (downInitialStatus && upInitialStatus)
            {
                bInitialOk = true;
            }
            return FCResultType.IDLE;
        }

        #endregion
        #region Convery AutoRun

        //UpConvery flow start
        private FCResultType flowChart7_FlowRun(object sender, EventArgs e)
        {
            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }
        //SMEMA Ready
        private FCResultType flowChart8_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.GantryF.GetSettingValue("MSet", "EPlaningCounts"))
            {
                int Planing = MiddleLayer.GantryF.GetSettingValue("MSet", "ProQuantity");
                int finished = MiddleLayer.GantryF.GetRecipeValue("RSet", "finishedCounts");
                if (Planing <= finished)
                {
                    SysPara.NPShowAlarm("0009");
                    return FCResultType.IDLE;
                }
            }


            OB_SMMachineReady.On();
            OB_UpCvyBlockCyOut.On();
            Conveyor1Timeout.Restart();
            if (IB_SMEMAUpStreamMachineReady.On() || SysPara.bDryCycle || upboardSmema)
            {
                if (IB_SMEMAUPFailAvailable.On())
                {
                    SysPara.bOB_SMForntMachineNg = true;
                }
                else { SysPara.bOB_SMForntMachineNg = false; }


                upboardSmema = false;
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }
        //Blockcylinder up，IB_UpCvyBlockCyOut=True
        private FCResultType flowChart9_FlowRun(object sender, EventArgs e)
        {
            if (IB_UpCvyBlockCyOut.Off(500) || SysPara.bDryCycle)
            {
                return FCResultType.NEXT;
            }
            else
            {
                if (Conveyor1Timeout.On(SysPara.TimeOutSec))
                {

                    SysPara.NPShowAlarm("1005");           //Stop cylinder go up action overtime
                }
                return FCResultType.IDLE;
            }
        }
        //Convery forward
        private FCResultType flowChart10_FlowRun(object sender, EventArgs e)
        {

            if (IB_UpCvyFeedSensor.On() || SysPara.bDryCycle)
            //if (true)
            {
                MiddleLayer.GantryF.LastProduct = false;


                if (IB_UpCvyMotorAlarm.Off())
                {
                    OB_UpCvyMtSpeed.Off();
                    OB_UpCvyMtFwd.On();

                }
                else
                {
                    SysPara.NPShowAlarm("1011");

                }

                Conveyor1Timeout.Restart();
                processData.fnIndicateStartingUnit();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }
        //IB_UpCvyFeedSensor=True，Pallet transfer in
        private FCResultType flowChart11_FlowRun(object sender, EventArgs e)
        {
            if (IB_UpCvyFeedSensor.On() || SysPara.bDryCycle)
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }
        //IB_UpCvyDecelerationSensor=true
        private FCResultType flowChart12_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.bDryCycle)
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }

            if (IB_UpCvyDecelerationSensor.On())
            {
                OB_UpCvyMtSpeed.On();
                OB_SMMachineReady.Off();
                SysPara.bOB_SMMachineReady = false;
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                if (Conveyor1Timeout.On(10000))
                {

                    SysPara.NPShowAlarm("1001");//减速感应超时，卡料
                }
                return FCResultType.IDLE;
            }
        }
        //wait product inplace
        private FCResultType flowChart13_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.bDryCycle)
            {
                if (Conveyor1Timeout.On(2000))
                {
                    Cy_ST1UpStreamLiftingUnite.On();
                    //MiddleLayer.MachineMainF.CycleTimeStart(1);
                    //MiddleLayer.MachineMainF.ClearResult();

                    SysPara.R1AssembResult = true;
                    OB_UpCvyMtFwd.Off();//皮带停转
                    OB_UpCvyMtSpeed.Off();//减速
                    Conveyor1Timeout.Restart();

                    SysPara.LeftDoorAlarmEnable = false;
                    return FCResultType.NEXT;
                }
                else { return FCResultType.IDLE; }
            }
            if (Conveyor1Timeout.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1001");           //upstream arrive Timeout！
            }
            if (IB_UpCvyInPlaceSensor.On())
            {
                // GantryForm.DelayMs(500);

                Cy_ST1UpStreamLiftingUnite.On();
                //MiddleLayer.MachineMainF.CycleTimeStart(1);
                //MiddleLayer.MachineMainF.ClearResult();
                SysPara.R1AssembResult = true;
                OB_UpCvyMtFwd.Off();
                OB_UpCvyMtSpeed.Off();


                Conveyor1Timeout.Restart();

                SysPara.LeftDoorAlarmEnable = false;
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }
        //Convery slow
        private FCResultType flowChart15_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.bDryCycle)
            {
                if (Conveyor1Timeout.On(2000))
                {
                    OB_UpCvyMtSpeed.On();
                    Conveyor1Timeout.Restart();
                    return FCResultType.NEXT;
                }
                else { return FCResultType.IDLE; }
            }
            if (Conveyor1Timeout.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1001");           //Slow down upstream Timeout！
            }
            if (IB_UpCvyDecelerationSensor.On())
            {
                OB_UpCvyMtSpeed.On();
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }
        //wait IB_UpCvyJackingCyOut=true
        private FCResultType flowChart14_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.bDryCycle)
            {
                //processData.fnInitilizeResultTable();
                MiddleLayer.GantryF.CurrentRowsIndex = 0;

                return FCResultType.NEXT;
            }
            if (IB_UpCvyJackingCyOut.On() && IB_UpCvyJackingCyBack.Off())
            {
                OB_UpCvyBlockCyOut.Off();
                //processData.fnInitilizeResultTable();
                MiddleLayer.GantryF.CurrentRowsIndex = 0;

                SysPara.cfx_FaliList.Clear();
                SysPara.cfx_AbortList.Clear();
                SysPara.cfx_PalletCode = "";
                //SysPara.Mes_StaraTime = "[" + DateTime.Now.ToString("mm/dd/yyyy hh:mm:ss");
                return FCResultType.NEXT;
            }
            else
            {
                if (Conveyor1Timeout.On(SysPara.TimeOutSec))
                {

                    SysPara.NPShowAlarm("1003");
                }
                return FCResultType.IDLE;
            }
        }
        //Judge front machine is NG
        private FCResultType flowChart16_FlowRun(object sender, EventArgs e)
        {
            //if (MiddleLayer.GantryF.GetRecipeValue("RSet", "TISEnable"))
            //{
            //    return FCResultType.NEXT;
            //}

            if (SysPara.bOB_SMForntMachineNg) { return FCResultType.NEXT; }
            else { return FCResultType.NEXT; }
        }
        //Front machine result is OK
        private FCResultType flowChart17_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        //Notice R1 Goto Convery take picture
        private FCResultType flowChart18_FlowRun(object sender, EventArgs e)
        {
            lock (SysPara.StatusLock)
            {
                //MiddleLayer.MachineMainF.ClearPreasureData();
                //SysPara.R1AssembStatus = true;
                MiddleLayer.ConveyorF.UpConveyorAutoFlow.NotifyAssembly = true;
            }
            return FCResultType.NEXT;
        }
        //Wait R1 assemb Compelet
        private FCResultType flowChart19_FlowRun(object sender, EventArgs e)
        {
            //if (SysPara.bBypassMode) { return FCResultType.NEXT; }
            //lock (SysPara.StatusLock)
            //{
            //    if (SysPara.R1AssembCompeletStatus)
            //    {
            //        SysPara.R1AssembStatus = false;
            //        SysPara.R1AssembCompeletStatus = false;
            //        processData.fnIndicateFinishedUnit();

            //        //MiddleLayer.MachineMainF.GetCycleTime_SingleST();
            //        return FCResultType.NEXT;
            //    }
            //    else { return FCResultType.IDLE; }
            //}

            //LFF
            if (SysPara.bBypassMode) { return FCResultType.NEXT; }
            lock (SysPara.StatusLock)
            {
                if (MiddleLayer.ConveyorF.UpConveyorAutoFlow.AssembCompeletStatus)  //SysPara.R1AssembCompeletStatus) 
                {
                    //SysPara.R1AssembStatus = false;
                    MiddleLayer.ConveyorF.UpConveyorAutoFlow.NotifyAssembly = false;
                    //SysPara.R1AssembCompeletStatus = false;
                    MiddleLayer.ConveyorF.UpConveyorAutoFlow.AssembCompeletStatus = false;


                    Conveyor1Timeout.Restart();

                    //MiddleLayer.MachineMainF.GetCycleTime_SingleST();
                    return FCResultType.NEXT;
                }
                else { return FCResultType.IDLE; }
            }

        }
        //Get assemb result
        private FCResultType flowChart20_FlowRun(object sender, EventArgs e)
        {
            bool assemb = SysPara.R1AssembResult;
            if (!MiddleLayer.ProcessF.GetSettingValue("PSet", "IsByPassMode") || !MiddleLayer.ProcessF.GetSettingValue("PSet", "IsDryCycleMode"))
            {
                MiddleLayer.cfxHandler.WorkCompleted(SysPara.cfx_PalletCode, SysPara.cfx_FaliList, SysPara.cfx_AbortList);
                //WriteMes(SysPara.cfx_PalletCode, SysPara.Que_Product_pallet_Code, assemb);
            }
            SysPara.Que_Product_pallet_Code.Clear();

            return FCResultType.NEXT;
        }
        //OB_SMDownFaliBoard=false
        private FCResultType flowChart21_FlowRun(object sender, EventArgs e)
        {
            OB_SM_UpFaliBoard.Off();
            OB_SM_UpBoardReady.On();
            //processData.fnTimersRunning(false);
            //processData.IsUnitProcesing = false;
            return FCResultType.NEXT;
        }
        //wait next station call board
        private FCResultType flowChart22_FlowRun(object sender, EventArgs e)
        {
            if (IB_SMEMAUpStreamBoardAvailable.On() || smCallboard || SysPara.bDryCycle)
            {

                smCallboard = false;
                MiddleLayer.SystemF.Cy_RightDoorUnite.On();

                processData.IsUnitProcesing = true;
                processData.fnTimersRunning(true);

                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }
        //OB_UpCvyJackingCyUp down
        private FCResultType flowChart23_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.bDryCycle) { Cy_ST1UpStreamLiftingUnite.Off(); return FCResultType.NEXT; }
            Cy_ST1UpStreamLiftingUnite.Off();
            // GantryForm.DelayMs(500);
            if (Conveyor1Timeout.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1004");
            }
            if (IB_UpCvyJackingCyOut.Off() && IB_UpCvyJackingCyBack.On())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart24_FlowRun(object sender, EventArgs e)
        {
            OB_UpCvyBlockCyOut.Off();


            if (IB_UpCvyBlockCyOut.On())
            {

                if (IB_UpCvyMotorAlarm.Off())
                {
                    OB_UpCvyMtSpeed.Off(); //Convery Slower off
                    OB_UpCvyMtFwd.On();      //Convery forward

                }
                else
                {
                    SysPara.NPShowAlarm("1011");
                }
                //  GantryForm.DelayMs(5000);
                return FCResultType.NEXT;
            }
            else
            {
                if (Conveyor1Timeout.On(SysPara.TimeOutSec))
                {
                    SysPara.NPShowAlarm("1006");
                }
                return FCResultType.IDLE;
            }
        }
        //Wait product in DischargeSensor
        private FCResultType flowChart25_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.bDryCycle) { return FCResultType.NEXT; }

            if (IB_UpCvyDischargeSensor.On())
            {
                //OB_UpRunnerMtFwd.Off();
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                if (Conveyor1Timeout.On(SysPara.TimeOutSec))
                {
                    SysPara.NPShowAlarm("1002");
                    OB_UpCvyMtFwd.On();
                    Conveyor1Timeout.Restart();
                }
                return FCResultType.IDLE;
            }
        }
        //Wait product transfer out
        private FCResultType flowChart26_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.bDryCycle) { Conveyor1Timeout.Restart(); return FCResultType.NEXT; }

            if (IB_UpCvyDischargeSensor.Off(1000))
            {
                Conveyor1Timeout.Restart();
                //OB_UpRunnerMtFwd.Off();
                processData.fnIndicateCancelUnit();
                return FCResultType.NEXT;
            }
            else
            {
                if (Conveyor1Timeout.On(SysPara.TimeOutSec))
                {
                    SysPara.NPShowAlarm("1002");
                }
                return FCResultType.IDLE;
            }
        }
        //Delay 3s,stop convery motor
        private FCResultType flowChart27_FlowRun(object sender, EventArgs e)
        {
            if (IB_SMEMAUpStreamBoardAvailable.Off() || SysPara.bDryCycle)
            {
                OB_UpCvyMtFwd.Off();
                //OB_SM_UpBoardReady.Off();
                OB_SM_UpFaliBoard.Off();
                Conveyor1Timeout.Restart();
                MiddleLayer.SystemF.Cy_RightDoorUnite.Off();
                if (IB_UpCvyDischargeSensor.On())
                {
                    SysPara.NPShowAlarm("1002");
                }
                SysPara.RightDoorAlarmEnable = false;

                return FCResultType.NEXT;
            }

            if (Conveyor1Timeout.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1002");
            }



            return FCResultType.IDLE;
        }
        //Return
        private FCResultType flowChart28_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        //Front machine result is NG
        private FCResultType flowChart29_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.MachineMainF.SetStatistics(false);
            return FCResultType.NEXT;
        }
        //SMEMA OB_SMDownFaliBoard=true
        private FCResultType flowChart30_FlowRun(object sender, EventArgs e)
        {
            OB_SM_UpFaliBoard.On();
            OB_SM_UpBoardReady.On();
            return FCResultType.NEXT;
        }
        #endregion
        #region bottom conveyor auto flow

        private FCResultType flowChart35_FlowRun(object sender, EventArgs e)
        {
            downConveyorProductStare = DownConveyorProductStare.None;
            downConveyorSMMStare = DownConveyorSMMStare.None;
            downConveyorMtRevStare = DownConveyorMtRevStare.None;
            downInputFlow = 0;
            downOutputFlow = 0;
            MiddleLayer.LogF.AddLog(LogType.Production, "Down Convery Autoflow Start", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart41_FlowRun(object sender, EventArgs e)
        {
            //if (MiddleLayer.SystemF.IB_ModeSwitch.Off())
            //{
            //    if (MiddleLayer.GantryF.IB_DownCvyFrontDoor.Off()
            //    || MiddleLayer.GantryF.IB_DownCvyBackDoor.Off())
            //    {
            //        //Stop DownCvy 
            //        MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();
            //        MiddleLayer.ConveyorF.OB_DownCvyMtRev.Off();
            //        OB_SM_DownBoardReady.Off();
            //        OB_SMDownCallBoard.Off();
            //        return FCResultType.IDLE;
            //    }
            //}
            if (IB_DownCvyFeedSensor.On(2000) && IB_DownCvyDischargeSensor.On(2000))
            {

                DownConveyorAutoTimeout.Restart();
                SysPara.NPShowAlarm("1010");
                MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();
                MiddleLayer.ConveyorF.OB_DownCvyMtRev.Off();
                OB_SM_DownBoardReady.Off();
                OB_SM_DownCallBoard.Off();
                return FCResultType.IDLE;
            }
            if (IB_DownCvyFeedSensor.On(3000) && downConveyorProductStare == DownConveyorProductStare.None)
            {
                downInputFlow = 2;
                DownConveyorAutoTimeout.Restart();
                downConveyorProductStare = DownConveyorProductStare.Input;
                downConveyorMtRevStare = DownConveyorMtRevStare.Start;
            }
            //return FCResultType.IDLE;
            if (downConveyorProductStare != DownConveyorProductStare.None)
            {

                if (downConveyorProductStare == DownConveyorProductStare.Input)
                {
                    //RefreshDifferentThreadUI(lalDownCnyState, () =>
                    //{
                    //    lalDownCnyState.Text = "Input";
                    //});

                    if (IB_DownCvyInPlace.On(3) && downInputFlow == 1)
                    {
                        downConveyorProductStare = DownConveyorProductStare.None;
                    }
                    if (IB_DownCvyDischargeSensor.On(3000))
                    {


                        SysPara.NPShowAlarm("1008");
                        MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();
                        MiddleLayer.ConveyorF.OB_DownCvyMtRev.Off();
                        OB_SM_DownBoardReady.Off();
                        OB_SM_DownCallBoard.Off();
                        return FCResultType.IDLE;
                    }

                }
                if (downConveyorProductStare == DownConveyorProductStare.Output)
                {
                    //RefreshDifferentThreadUI(lalDownCnyState, () =>
                    //{
                    //    lalDownCnyState.Text = "Output";
                    //});

                    if (IB_DownCvyInPlace.Off(10000) && IB_DownCvyFeedSensor.Off(10000) && IB_DownCvyDischargeSensor.Off(10000))
                    {
                        downConveyorProductStare = DownConveyorProductStare.None;
                    }
                    if (IB_DownCvyFeedSensor.On(3000))
                    {


                        SysPara.NPShowAlarm("1007");
                        MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();
                        MiddleLayer.ConveyorF.OB_DownCvyMtRev.Off();
                        OB_SM_DownBoardReady.Off();
                        OB_SM_DownCallBoard.Off();
                        OB_DownCvyMtFwd.On();
                        return FCResultType.IDLE;
                    }

                }
                return FCResultType.NEXT;
            }
            //RefreshDifferentThreadUI(lalDownCnyState, () =>
            //{
            //    lalDownCnyState.Text = "None";
            //});

            if (IB_DownCvyInPlace.On(100) || IB_DownCvyDischargeSensor.On(100))
            {

                downOutputFlow = 1;
                downConveyorProductStare = DownConveyorProductStare.Output;
                downConveyorSMMStare = DownConveyorSMMStare.Output;

            }
            else
            {
                downInputFlow = 1;
                downConveyorProductStare = DownConveyorProductStare.Input;
                downConveyorSMMStare = DownConveyorSMMStare.Input;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart43_FlowRun(object sender, EventArgs e)
        {
            if (downConveyorProductStare == DownConveyorProductStare.Input)
            {

                switch (downInputFlow)
                {
                    case 1:
                        if (IB_DownCvyFeedSensor.On())
                        {
                            downInputFlow = 2;
                            DownConveyorAutoTimeout.Restart();

                            downConveyorMtRevStare = DownConveyorMtRevStare.Start;
                        }
                        break;
                    case 2:
                        if (IB_DownCvyInPlace.On())
                        {
                            downConveyorSMMStare = DownConveyorSMMStare.None;
                            if (IB_DownCvyFeedSensor.On())
                            {
                                downConveyorMtRevStare = DownConveyorMtRevStare.stop;
                                DownConveyorAutoTimeout.Restart();
                                SysPara.NPShowAlarm("1007");
                                OB_DownCvyMtFwd.On();
                            }
                            else
                            {

                                downInputFlow = 0;
                                DownConveyorAutoTimeout.Restart();
                                downConveyorSMMStare = DownConveyorSMMStare.Output;
                                downConveyorMtRevStare = DownConveyorMtRevStare.stop;
                                downConveyorProductStare = DownConveyorProductStare.None;
                            }

                        }
                        else
                        {
                            if (DownConveyorAutoTimeout.On(10000))
                            {
                                if (IB_DownCvyFeedSensor.On())
                                {
                                    downConveyorMtRevStare = DownConveyorMtRevStare.stop;
                                    DownConveyorAutoTimeout.Restart();
                                    SysPara.NPShowAlarm("1007");
                                    OB_DownCvyMtFwd.On();
                                }
                                else
                                {

                                    downInputFlow = 0;
                                    downConveyorMtRevStare = DownConveyorMtRevStare.stop;
                                    downConveyorSMMStare = DownConveyorSMMStare.None;
                                    downConveyorProductStare = DownConveyorProductStare.None;
                                }
                            }
                        }
                        break;

                }
            }
            if (downConveyorProductStare == DownConveyorProductStare.Output)
            {
                switch (downOutputFlow)
                {
                    case 1:
                        if (IB_SMEMADownStreamBoardAvailable.On())
                        {

                            downOutputFlow = 2;
                            DownConveyorAutoTimeout.Restart();
                            downConveyorMtRevStare = DownConveyorMtRevStare.Start;
                        }
                        break;
                    case 2:
                        if (IB_DownCvyDischargeSensor.On())
                        {
                            downOutputFlow = 3;
                            DownConveyorAutoTimeout.Restart();
                        }
                        else
                        {
                            if (DownConveyorAutoTimeout.On(10000))
                            {

                                downInputFlow = 0;
                                downConveyorMtRevStare = DownConveyorMtRevStare.stop;
                                downConveyorSMMStare = DownConveyorSMMStare.None;
                                downConveyorProductStare = DownConveyorProductStare.None;
                            }
                        }
                        break;
                    case 3:
                        if (IB_DownCvyDischargeSensor.Off())
                        {
                            downConveyorSMMStare = DownConveyorSMMStare.None;
                            downOutputFlow = 4;
                            DownConveyorAutoTimeout.Restart();
                        }
                        else
                        {
                            if (DownConveyorAutoTimeout.On(10000))
                            {
                                SysPara.NPShowAlarm("1008");
                                downConveyorMtRevStare = DownConveyorMtRevStare.stop;
                            }
                        }
                        break;
                    case 4:

                        if (DownConveyorAutoTimeout.On(3000))
                        {
                            downOutputFlow = 0;
                            downConveyorMtRevStare = DownConveyorMtRevStare.stop;

                            downConveyorProductStare = DownConveyorProductStare.None;
                        }
                        break;
                }
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart44_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart46_FlowRun(object sender, EventArgs e)
        {
            if (IB_DownCvyDischargeSensor.On())
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "DownConvery product in dischargeSenser", true);
                DownConveyorAutoTimeout.Restart();
                return FCResultType.NEXT;
            }

            return FCResultType.IDLE;
        }

        private FCResultType flowChart47_FlowRun(object sender, EventArgs e)
        {
            if (IB_DownCvyDischargeSensor.Off())
            {
                DownConveyorAutoTimeout.Restart();
                return FCResultType.NEXT;
            }
            if (DownConveyorAutoTimeout.On(5000))
            {
                SysPara.NPShowAlarm("1008");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart48_FlowRun(object sender, EventArgs e)
        {
            if (IB_SMEMADownStreamBoardAvailable.Off())
            {
                OB_DownCvyMtFwd.Off();
                return FCResultType.NEXT;
            }
            if (DownConveyorAutoTimeout.On(5000))
            {
                OB_DownCvyMtFwd.Off();
                SysPara.NPShowAlarm("1008");

            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart49_FlowRun(object sender, EventArgs e)
        {
            if (downConveyorSMMStare == DownConveyorSMMStare.Input)
            {
                OB_SM_DownBoardReady.Off();
                OB_SM_DownCallBoard.On();
            }
            if (downConveyorSMMStare == DownConveyorSMMStare.Output)
            {
                OB_SM_DownBoardReady.On();
                OB_SM_DownCallBoard.Off();
            }
            if (downConveyorSMMStare == DownConveyorSMMStare.None)
            {
                OB_SM_DownBoardReady.Off();
                OB_SM_DownCallBoard.Off();
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart50_FlowRun(object sender, EventArgs e)
        {
            if (downConveyorMtRevStare == DownConveyorMtRevStare.Start)
            {
                if (IB_DownCvyMotorAlarm.On())
                {
                    SysPara.NPShowAlarm("1012");

                }

                OB_DownCvyMtFwd.On();
                OB_DownCvyMtRev.Off();
                OB_DownCvyMtSpeed.Off();

            }
            if (downConveyorMtRevStare == DownConveyorMtRevStare.stop)
            {
                OB_DownCvyMtFwd.Off();
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart52_FlowRun(object sender, EventArgs e)
        {
            if (IB_DownCvyInPlace.Off() && IB_DownCvyDischargeSensor.Off())
            {
                DownConveyorAutoTimeout.Restart();
            }
            if (DownConveyorAutoTimeout.On(3000))
            {
                return FCResultType.CASE1;
            }
            if (IB_DownCvyFeedSensor.On())
            {
                DownConveyorAutoTimeout.Restart();
                return FCResultType.NEXT;
            }
            else
            {

            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart51_FlowRun(object sender, EventArgs e)
        {
            if (IB_DownCvyInPlace.On())
            {
                OB_DownCvyMtFwd.Off();
                return FCResultType.NEXT;
            }
            else
            {
                if (DownConveyorAutoTimeout.On(10000))
                {
                    OB_DownCvyMtFwd.Off();
                    if (IB_DownCvyFeedSensor.On())
                    {

                        SysPara.NPShowAlarm("1007");
                    }

                    return FCResultType.CASE1;
                }
            }
            return FCResultType.IDLE;
        }

        #endregion
        #region down convery init flow
        private FCResultType flowChart36_FlowRun(object sender, EventArgs e)
        {
            if (isdownInitialOK)
            {
                return FCResultType.CASE1;
            }
            else
            {
                OB_DownCvyMtFwd.Off();
                OB_DownCvyMtRev.Off();
                OB_DownCvyMtSpeed.Off();
                downInitialStatus = false;
                return FCResultType.NEXT;
            }
        }

        private FCResultType flowChart37_FlowRun(object sender, EventArgs e)
        {
            if (IB_DownCvyDischargeSensor.On(10000))
            {
                SysPara.NPShowAlarm("1010");
                OB_DownCvyMtFwd.Off();
                return FCResultType.IDLE;
            }
            if (IB_DownCvyMotorAlarm.On())
            {
                OB_DownCvyMtRev.Off();
                OB_DownCvyMtSpeed.Off();
                OB_DownCvyMtFwd.Off();

                SysPara.NPShowAlarm("1012");
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart38_FlowRun(object sender, EventArgs e)
        {
            if (IB_DownCvyInPlace.On() && IB_DownCvyDischargeSensor.On())
            {
                OB_DownCvyMtFwd.Off();
                SysPara.NPShowAlarm("1010");
                return FCResultType.NEXT;
            }
            OB_DownCvyMtFwd.On();
            DownConveyorTimeout.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart39_FlowRun(object sender, EventArgs e)
        {
            OB_DownCvyMtFwd.On();
            if (IB_DownCvyInPlace.On() && IB_DownCvyDischargeSensor.On())
            {
                OB_DownCvyMtFwd.Off();
                return FCResultType.NEXT;
            }
            else
            {
                if (DownConveyorTimeout.On(5000))
                {

                    if (IB_DownCvyFeedSensor.On())
                    {
                        SysPara.NPShowAlarm("1010");
                        OB_DownCvyMtFwd.Off();
                        return FCResultType.IDLE;
                    }
                    else
                    {
                        OB_DownCvyMtFwd.Off();
                        return FCResultType.NEXT;
                    }

                }




            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart40_FlowRun(object sender, EventArgs e)
        {
            if (!isdownInitialOK)
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "DownConveyor initial finish", true);
                //BottomConveyorStartFlow.TaskReset();
                DownConveyorAutoFlow.ResetDownConveyorFlow();
                DownConveyorAutoFlow.StartDownConveyorFlow();
            }
            downInitialStatus = true;
            isdownInitialOK = true;
            if (downInitialStatus && upInitialStatus)
            {
                bInitialOk = true;
            }
            return FCResultType.IDLE;
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            smCallboard = true;
        }

        private FCResultType flowChart31_FlowRun(object sender, EventArgs e)
        {

            OB_SMMachineReady.On();
            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart32_FlowRun(object sender, EventArgs e)
        {


            SysPara.LeftDoorAlarmEnable = true;
            OB_SMMachineReady.Off();

            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart33_FlowRun(object sender, EventArgs e)
        {
            OB_SM_UpBoardReady.Off();
            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart34_FlowRun(object sender, EventArgs e)
        {

            SysPara.RightDoorAlarmEnable = true;
            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            upboardSmema = true;
        }

        private FCResultType flowChart35_FlowRun_1(object sender, EventArgs e)
        {

            ConveyorInitTimeout.Restart();
            return FCResultType.NEXT;


        }

        private FCResultType flowChart36_FlowRun_1(object sender, EventArgs e)
        {
            if (MiddleLayer.SystemF.IB_LeftDoorUpSensor.On() && MiddleLayer.SystemF.IB_LeftDoorDownSensor.Off())
            {
                return FCResultType.NEXT;
            }
            else if (Conveyor1Timeout.On(10000))
            {
                SDKKernal.ShowAlarm(MiddleLayer.SystemF.IB_LeftDoorUpSensor.Port.ToString());
                return FCResultType.IDLE;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }



        private FCResultType flowChart45_FlowRun(object sender, EventArgs e)
        {
            //if (SysPara.bDryCycle) { return FCResultType.NEXT; }
            if (IB_SMEMAUpStreamBoardAvailable.On() || smCallboard || SysPara.bDryCycle)
            {

                smCallboard = false;
                MiddleLayer.SystemF.Cy_RightDoorUnite.On();
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        public async void WriteMes(string sSerialNumber, Queue<Product_Pallet_Code> assembResult, bool bStatusOk)
        {
            SysPara.MESPara = new MESParameter();
            int length = assembResult.Count;
            for (int i = 0; i < length; i++)
            {
                Product_Pallet_Code temp = new Product_Pallet_Code();
                temp = assembResult.Dequeue();
                if (!temp.PressureResult)
                {
                    KeyValuePair<string, string> keyTemp = new KeyValuePair<string, string>("Assemb pressure" + (i + 1).ToString() + "_FAIL", temp.data);
                    SysPara.MESPara.fails.Add(keyTemp);
                }
                Measurement measureData = new Measurement();
                measureData.measureLabel = "Pressure" + (i + 1).ToString();
                measureData.measureData = temp.data;
                measureData.measureMessage = temp.PressureResult == true ? "Assemb pressure" + (i + 1).ToString() + "_PASS" : "Assemb pressure" + (i + 1).ToString() + "_FAIL";
                SysPara.MESPara.measurements.Add(measureData);
                //sSerialNumber=
                await MiddleLayer.MesF.WriteUnitMES(sSerialNumber, null, null, true, bStatusOk);
            }
            //await MiddleLayer.MesF.WriteUnitMES(sSerialNumber, null, null, true, bStatusOk);
        }
        public async void WriteMesByOne(string sSerialNumber, int index, Product_Pallet_Code assembResult, bool bStatusOk)
        {
            SysPara.MESPara = new MESParameter();
            //int length = assembResult.Count;
            //for (int i = 0; i < length; i++)
            //{
            Product_Pallet_Code temp = new Product_Pallet_Code();

            if (!assembResult.PressureResult)
            {
                KeyValuePair<string, string> keyTemp = new KeyValuePair<string, string>("Assemb pressure" + (index).ToString() + "_FAIL", temp.data);
                SysPara.MESPara.fails.Add(keyTemp);
            }
            Measurement measureData = new Measurement();
            measureData.measureLabel = "Pressure" + (index).ToString();
            measureData.measureData = assembResult.data;
            measureData.measureMessage = assembResult.PressureResult == true ? "Assemb pressure" + (index).ToString() + "_PASS" : "Assemb pressure" + (index).ToString() + "_FAIL";
            SysPara.MESPara.measurements.Add(measureData);

            await MiddleLayer.MesF.WriteUnitMES(sSerialNumber, null, null, true, bStatusOk);
            //}
            //await MiddleLayer.MesF.WriteUnitMES(sSerialNumber, null, null, true, bStatusOk);
        }
        public static bool DelayMs(int delayMilliseconds)
        {
            DateTime now = DateTime.Now;
            Double s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.TotalMilliseconds + spand.Seconds * 1000;
                Application.DoEvents();
            }
            while (s < delayMilliseconds);
            return true;
        }

        private FCResultType flowChart36_FlowRun_2(object sender, EventArgs e)
        {
            OB_UpCvyMtFwd.On();
            if (IB_UpCvyDischargeSensor.On(100) || IB_UpCvyInPlaceSensor.On(100))
            {
                SysPara.NPShowAlarm("1000");
            }
            if (ConveyorInitTimeout.On(3000))
            {
                ConveyorInitTimeout.Restart();
                OB_UpCvyMtFwd.Off();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            downConveyorProductStare = DownConveyorProductStare.None;
            downConveyorSMMStare = DownConveyorSMMStare.None;
            downConveyorMtRevStare = DownConveyorMtRevStare.None;
            downInputFlow = 0;
            downOutputFlow = 0;
            MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();
            MiddleLayer.ConveyorF.OB_DownCvyMtRev.Off();
            OB_SM_DownBoardReady.Off();
            OB_SM_DownCallBoard.Off();
            DownConveyorAutoTimeout.Restart();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            string dateTimeStr = DateTime.Now.ToString("yyyyMMdd");
            if (GetSettingValue("PSet", "dateTime") != dateTimeStr)
            {
                DataTable dt = SettingData.Tables["PSet"];
                dt.Rows[0]["dateTime"] = dateTimeStr;
                dt.AcceptChanges();
                MiddleLayer.SystemF.WriteSettingData();
                string path = @"D:\WorkLog\Image\BMP";
                string path1 = @"D:\WorkLog\Image\JPG";
                string path2 = @"D:\WorkLog\MesLog";
                string path3 = @"D:\WorkLog\UIDataLog";

                SaveFile.DeleteFile(path, 3);
                SaveFile.DeleteFile(path1, 3);
                SaveFile.DeleteFile(path2, 3);
                SaveFile.DeleteFile(path3, 3);

                string path4 = @"D:\EVMS\TP\LOG";
                SaveFile.DeleteFile1(path4, 21);

            }
            if (SaveFile.DriveFreeSpace(20))
                SDKKernal.ShowAlarm("0020");
            timer1.Enabled = true;
        }

        private FCResultType npFlowChart1_FlowRun(object sender, EventArgs e)
        {
            OB_UpCvyBlockCyOut.Off();
            if (IB_UpCvyBlockCyOut.Off())
            { ConveyorInitTimeout.Restart(); return FCResultType.NEXT; }
            else
            {
                if (ConveyorInitTimeout.On(SysPara.TimeOutSec))
                { SysPara.NPShowAlarm("1005"); }     //Stop cylinder go down action overtime

                return FCResultType.IDLE;
            }
        }

        private FCResultType npFlowChart2_FlowRun(object sender, EventArgs e)
        {
            OB_UpCvyMtFwd.On();
            if (ConveyorInitTimeout.On(3000))
            {
                OB_UpCvyMtFwd.Off();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType npFlowChart3_FlowRun(object sender, EventArgs e)
        {
            if (IB_UpCvyInPlaceSensor.On(200))
            {
                SysPara.NPShowAlarm("1000");
                return FCResultType.IDLE;
            }
            else
            {
                return FCResultType.NEXT;
            }
        }
    }
}

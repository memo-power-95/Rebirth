using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcuraLibrary.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro3D;
using System.Threading;
using NPSDK;
using Alpha;
using static Alpha.FunctionForms.LogForm;

namespace Alpha
{
    public partial class UC_DownConveyorAutoFlow : UserControl
    {
        public UC_DownConveyorAutoFlow()
        {
            InitializeComponent();
        }
        JTimer DownConveyorTimeout = new JTimer();
        JTimer DownConveyorAutoTimeout2 = new JTimer();
        JTimer DownConveyorAutoTimeout = new JTimer();
        private Thread _startWorkTh;
        public bool _bdownConveyorstart = false;
        public static bool downconveyorFwdFalg = false;
        static bool bOB_SMDownBoardReady = false;//本机有板待出
        static bool bOB_SMDownCallBoard = false;//本机要板
        public ManualResetEvent motorMoveEnable = new ManualResetEvent(false);
        private bool StatusChange_DownCvyMtFwd = false;
        private bool StatusChange_DownCallBoard = false;
        private bool StatusChange_DownBoardReady = false;


        public void StopDownConveyor()
        {
            motorMoveEnable.Reset();
            if (!StatusChange_DownCvyMtFwd)
            {
                if (MiddleLayer.ConveyorF.OB_DownCvyMtFwd.GetState())
                {
                    downconveyorFwdFalg = true;
                    StatusChange_DownCvyMtFwd = true;
                }
                else
                {
                    downconveyorFwdFalg = false;
                }
            }
            
            MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();

            if (!StatusChange_DownCallBoard)
            {
                if (MiddleLayer.ConveyorF.OB_SM_DownCallBoard.GetState())
                {
                    bOB_SMDownCallBoard = true;
                    StatusChange_DownCallBoard = true;
                }
                else
                {
                    bOB_SMDownCallBoard = false;
                }
            }

            if (!StatusChange_DownBoardReady)
            {
                if (MiddleLayer.ConveyorF.OB_SM_DownBoardReady.GetState())
                {
                    bOB_SMDownBoardReady = true;
                    StatusChange_DownBoardReady = true;
                }
                else
                {
                    bOB_SMDownBoardReady = false;
                }
            }
            
            MiddleLayer.ConveyorF.OB_SM_DownCallBoard.Off();
            MiddleLayer.ConveyorF.OB_SM_DownBoardReady.Off();
        }
        public void StartDownConveyor()
        {
            motorMoveEnable.Set();
            if (downconveyorFwdFalg)
            {
                MiddleLayer.ConveyorF.OB_DownCvyMtFwd.On();
                downconveyorFwdFalg = false;
            }
            if (bOB_SMDownCallBoard)
            {
                MiddleLayer.ConveyorF.OB_SM_DownCallBoard.On();
                StatusChange_DownCallBoard = false;
            }
            if (bOB_SMDownBoardReady)
            {
                MiddleLayer.ConveyorF.OB_SM_DownBoardReady.On();
                StatusChange_DownBoardReady = false;
            }
        }
        public void ResetDownConveyorFlow()
        {
            _bdownConveyorstart = true;
            DownConveyorStartFlow.TaskReset();
        }

        public void StartDownConveyorFlow()
        {
            if (_startWorkTh != null)
            {
                if (_startWorkTh.IsAlive)
                {
                    _startWorkTh.Abort();
                }
            }
           
            _bdownConveyorstart = false;
            _startWorkTh = new Thread(new ThreadStart(() =>
            {
                while (!_bdownConveyorstart )
                {
                    motorMoveEnable.WaitOne();
                    DownConveyorStartFlow.TaskRun();
                }
            }));
            _startWorkTh.IsBackground = true;
            _startWorkTh.Start();
            motorMoveEnable.Set();
        }



        private FCResultType BottomConveyorStartFlow_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.Production, "Down Conveyor automatic process start", SysPara.bEnableGeneralSaveLog);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart65_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.IB_DownCvyInPlace.On() || MiddleLayer.ConveyorF.IB_DownCvyDischargeSensor.On())
            {
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.CASE1;
            }
        }

        private FCResultType flowChart67_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.ConveyorF.OB_SM_DownBoardReady.On();
            MiddleLayer.ConveyorF.OB_SM_DownCallBoard.Off();
            MiddleLayer.LogF.AddLog(LogType.Production, "The down conveyor enters the material state of the machine", SysPara.bEnableGeneralSaveLog);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart70_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.IB_DownCvyInPlace.On() || MiddleLayer.ConveyorF.IB_DownCvyDischargeSensor.On())
            {

                DownConveyorAutoTimeout.Restart();
            }
            if (DownConveyorAutoTimeout.On(3000))
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "The down conveyor detects that the material has left the station and jumps to the material seeking mode", SysPara.bEnableGeneralSaveLog);
                return FCResultType.CASE1;
            }
            if (MiddleLayer.ConveyorF.IB_SMEMADownStreamBoardAvailable.On())
            {
                MiddleLayer.ConveyorF.OB_DownCvyMtRev.Off();
                MiddleLayer.ConveyorF.OB_DownCvyMtSpeed.Off();
                if (MiddleLayer.ConveyorF.IB_DownCvyMotorAlarm.On())
                    SDKKernal.ShowAlarm("1012");

                MiddleLayer.ConveyorF.OB_DownCvyMtFwd.On();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart77_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.IB_DownCvyDischargeSensor.On())
            {
                DownConveyorAutoTimeout.Restart();
                return FCResultType.NEXT;
            }

            return FCResultType.IDLE;
        }

        private FCResultType flowChart71_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.IB_DownCvyDischargeSensor.Off())
            {
                DownConveyorAutoTimeout.Restart();
                return FCResultType.NEXT;
            }
            if (DownConveyorAutoTimeout.On(10000))
            {
                DownConveyorAutoTimeout.Restart();
                SDKKernal.ShowAlarm("1008");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart48_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.IB_SMEMADownStreamBoardAvailable.Off())
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Discharge completed", SysPara.bEnableGeneralSaveLog);
                MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();
                return FCResultType.NEXT;
            }
            if (DownConveyorAutoTimeout.On(10000))
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Discharge completed", SysPara.bEnableGeneralSaveLog);
                MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart62_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.IB_DownCvyInPlace.On())
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Feeding completed", SysPara.bEnableGeneralSaveLog);
                MiddleLayer.ConveyorF.OB_DownCvyMtFwd.Off();
                return FCResultType.NEXT;
            }
            else
            {
                if (DownConveyorAutoTimeout.On(10000))
                {
                    if (MiddleLayer.ConveyorF.IB_DownCvyFeedSensor.On())
                        SDKKernal.ShowAlarm("1007");

                    return FCResultType.CASE1;

                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart78_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.IB_DownCvyInPlace.Off() && MiddleLayer.ConveyorF.IB_DownCvyDischargeSensor.Off())
            {
                DownConveyorAutoTimeout.Restart();
            }
            if (DownConveyorAutoTimeout.On(3000))
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "The down conveyor detects that there is material in the discharge port and jumps to the discharge state", SysPara.bEnableGeneralSaveLog);
                return FCResultType.CASE1;
            }
            if (MiddleLayer.ConveyorF.IB_DownCvyFeedSensor.On())
            {
                DownConveyorAutoTimeout.Restart();
                return FCResultType.NEXT;
            }

            return FCResultType.IDLE;
        }

        private FCResultType flowChart66_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.IB_DownCvyInPlace.Off() && MiddleLayer.ConveyorF.IB_DownCvyDischargeSensor.Off())
            {
                DownConveyorAutoTimeout.Restart();
            }
            if (DownConveyorAutoTimeout.On(3000))
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "The down conveyor detects that there is material in the discharge port and jumps to the discharge state", SysPara.bEnableGeneralSaveLog);
                return FCResultType.CASE1;
            }
            if (/*IB_SMEMADownStreamMachineReady.On(100) ||*/ MiddleLayer.ConveyorF.IB_DownCvyFeedSensor.On())
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "The front machine starts to discharge", SysPara.bEnableGeneralSaveLog);
                MiddleLayer.ConveyorF.OB_DownCvyMtRev.Off();
                MiddleLayer.ConveyorF.OB_DownCvyMtSpeed.Off();
                if (MiddleLayer.ConveyorF.IB_DownCvyMotorAlarm.On())
                    SDKKernal.ShowAlarm("1012");
                DownConveyorAutoTimeout.Restart();
                MiddleLayer.ConveyorF.OB_DownCvyMtFwd.On();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart64_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.ConveyorF.OB_SM_DownBoardReady.Off();
            MiddleLayer.ConveyorF.OB_SM_DownCallBoard.On();
            DownConveyorAutoTimeout.Restart();
            MiddleLayer.LogF.AddLog(LogType.Production, "The down conveyor enters the material seeking state", SysPara.bEnableGeneralSaveLog);
            return FCResultType.NEXT;
        }
    }
}

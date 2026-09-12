using Alpha.Classes;
using Alpha.FunctionForms;
using AcuraLibrary.Forms;
using NPSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enums = Alpha.Classes.Enums;
using OMRON.Compolet.CIPCompolet64;

namespace Alpha.ModuleForms
{
    public partial class IXSensorForm : ModuleBaseForm
    {

        #region ----------------------------------Variables
        cSmartAlarm cAlm = SmartAlarm.cAlarm;
        public bool bEnableIXSensor { get => GetData(dc_PSet_EnableIXsensor); }
        public decimal dHeightUpperLimit { get => GetData(dc_PSet_HeightUpperLimit); }
        public bool bEnablePopup  { get => GetData(dc_PSet_EnablePopup); }
        public decimal dHeightLowerLimit { get => GetData(dc_PSet_HeightLowerLimit); }
        #endregion

        #region -----------------------------------Enum
        private enum Stopper
        {
            Stopper1,
            Stopper2,
            Stopper3,
            Stopper4,
            All,
            None,
        }
        public enum Sides
        {
            Left,
            Right
        }
        public enum UpDownStates
        {
            Up,
            Down
        }
        public enum ClampUnClampStates
        {
            Clamp,
            Unclamp
        }
        public enum ConveyorStates
        {
            Forward,
            Reverse,
            Slow,
            Stop

        }
        public enum FlipperStates
        {
            Front,
            Back
        }
        #endregion



        public IXSensorForm()
        {
            InitializeComponent();
            cAlm.AddRange(fcInitial_IX_Start);

        }

        /****************************************************************************************************
         * Override functions
         * ***************************************************************************************************/
        #region Override Functions

        public override void InitialReset()
        {
      
            

            fcInitial_IX_Start.TaskReset();
        }
        public override void Initial()
        {
            fcInitial_IX_Start.TaskRun();
        }
        public override void RunReset()
        {
         
        }
        public override void Run()
        {
            
        
        }
        public override void StopRun()
        {
            
      
            
        }
        public override void ServoOn()
        {
            
            
        }

        public override void ServoOff()
        {
         
           

        }
        public override void ModuleInitialize(string ModuleName)
        {
            base.ModuleInitialize(ModuleName);
           
            //MiddleLayer.fnConnectCompolet(compolet, "192.168.0.50", 2);
        }

        #endregion Override Functions

     
        /****************************************************************************************************
         * fcInitial
         * ***************************************************************************************************/

        #region fcInitial
        private FCResultType fcInitial_Start_FlowRun(object sender, EventArgs e)
        {
            if (!bEnableIXSensor)
                return FCResultType.CASE1;
            return FCResultType.NEXT;
        }


        private FCResultType fcInitial_WaitGantryHome_FlowRun(object sender, EventArgs e)
        {
            if (!IXSensorControl.IsConnected)
            {
                IXSensorControl.fnConnect();
                return FCResultType.NEXT;
            }
            else
                return FCResultType.NEXT;
            return FCResultType.IDLE;
        }


        private FCResultType fcInit_BeltOpen_FlowRun(object sender, EventArgs e)
        {
            
            return FCResultType.IDLE;
        }


        private FCResultType fcInit_InspectBoard_FlowRun(object sender, EventArgs e)
        {
           
            return FCResultType.NEXT;
        }

        private FCResultType fcInit_Cylinder_FlowRun(object sender, EventArgs e)
        {
            if(IXSensorControl.IsConnected)
            {
                SDKPara.Arm.ReportClearAlarm(cAlm.E(fcInitial_IX_Start));

                return FCResultType.NEXT ;
            }
            if(fcInitial_IX_Start.tmrTimeOut.On(3000))
            {
                SDKPara.Arm.ReportAlarm(cAlm.E(fcInitial_IX_Start), AcuraIOT.ErrorGroup.Sensor, AcuraIOT.ErrorSubGroup.Communication, AcuraIOT.ErrorType.E);

            }
            return FCResultType.IDLE;
        }

        private FCResultType fcInit_CvHome_FlowRun(object sender, EventArgs e)
        {
           
            return FCResultType.IDLE;
        }

        private FCResultType fcInit_CvSetupWidth_FlowRun(object sender, EventArgs e)
        {
            
            //bool r1 = MTR_Width.Goto(GetRecipeValue("RSet", "ConveyorWidth"));
           

            return FCResultType.IDLE;
        }


        private FCResultType fcInitial_End_FlowRun(object sender, EventArgs e)
        {
            SDKPara.Arm.ReportClearAlarm(cAlm.I(fcInitial_IX_Start));
            bInitialOk = true;
            return FCResultType.IDLE;
        }

        #endregion fcInitial

        private void IXSensorControl_Load(object sender, EventArgs e)
        {

        }
    }

}

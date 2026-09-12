
namespace Alpha.ModuleForms
{
    partial class IXSensorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dcRset_HeightUpperLimit = new System.Data.DataColumn();
            this.fcInitial_IX_Start = new NPSDK.NPFlowChart();
            this.fcInitial_End = new NPSDK.NPFlowChart();
            this.fcInitial_IX_Connect = new NPSDK.NPFlowChart();
            this.fcInit_CylinderInit = new NPSDK.NPFlowChart();
            this.dcRSet_TakeBoardOutFlipped = new System.Data.DataColumn();
            this.label21 = new System.Windows.Forms.Label();
            this.dcMSet_FlipperMaxSpeed = new System.Data.DataColumn();
            this.dcMSet_FlipperAcceleration = new System.Data.DataColumn();
            this.dcMSet_FlipperDeceleration = new System.Data.DataColumn();
            this.dcMSet_FlipperSpeedRate = new System.Data.DataColumn();
            this.dcMSet_ConveyorWidthMaxSpeed = new System.Data.DataColumn();
            this.dcMSet_ConveyorWidthAcceleration = new System.Data.DataColumn();
            this.dcMSet_ConveyorWidthDeceleration = new System.Data.DataColumn();
            this.dcMSet_ConveyorWidthSpeedRate = new System.Data.DataColumn();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.panel31 = new System.Windows.Forms.Panel();
            this.Stop01 = new System.Data.DataColumn();
            this.Stop02 = new System.Data.DataColumn();
            this.Stop03 = new System.Data.DataColumn();
            this.Stop04 = new System.Data.DataColumn();
            this.dcMSet_CVBeltMaxSpeed = new System.Data.DataColumn();
            this.dcMSet_CVBeltAcceleration = new System.Data.DataColumn();
            this.dcMSet_CVBeltDeceleration = new System.Data.DataColumn();
            this.dcMSet_CVBeltSpeedRate = new System.Data.DataColumn();
            this.tmrCurrentPositions = new System.Windows.Forms.Timer(this.components);
            this.dcRSet_TimeOutBoardStabilization = new System.Data.DataColumn();
            this.IXSensorControl = new Control_IX.KeyenceIXSensorControl();
            this.label1 = new System.Windows.Forms.Label();
            this.dc_PSet_EnableIXsensor = new System.Data.DataColumn();
            this.dc_PSet_IpSensorIX = new System.Data.DataColumn();
            this.dc_PSet_PortSensorIX = new System.Data.DataColumn();
            this.tbXSafePos = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.dc_PSet_HeightUpperLimit = new System.Data.DataColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.myCheckBox2 = new tstCkBox.MyCheckBox();
            this.myCheckBox1 = new tstCkBox.MyCheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dc_PSet_EnablePopup = new System.Data.DataColumn();
            this.dcRset_HeightLowerLimit = new System.Data.DataColumn();
            this.dc_PSet_HeightLowerLimit = new System.Data.DataColumn();
            this.plProductionSetting.SuspendLayout();
            this.plFlowInitial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            this.tableLayoutPanel10.SuspendLayout();
            this.panel31.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(1084, 1055);
            // 
            // plMaintenance
            // 
            this.plMaintenance.Size = new System.Drawing.Size(1076, 1012);
            // 
            // plProductionSetting
            // 
            this.plProductionSetting.Controls.Add(this.tableLayoutPanel1);
            this.plProductionSetting.Controls.Add(this.label1);
            this.plProductionSetting.Size = new System.Drawing.Size(1076, 1012);
            // 
            // plRecipeEditor
            // 
            this.plRecipeEditor.Size = new System.Drawing.Size(1076, 1012);
            // 
            // plFlowInitial
            // 
            this.plFlowInitial.Controls.Add(this.tableLayoutPanel10);
            this.plFlowInitial.Controls.Add(this.label21);
            this.plFlowInitial.Size = new System.Drawing.Size(1076, 1012);
            // 
            // plFlowAuto
            // 
            this.plFlowAuto.Size = new System.Drawing.Size(1076, 1012);
            // 
            // plMachineStatus
            // 
            this.plMachineStatus.Size = new System.Drawing.Size(1076, 1012);
            // 
            // plMotionSetup
            // 
            this.plMotionSetup.Size = new System.Drawing.Size(1076, 1012);
            // 
            // plMotorControl
            // 
            this.plMotorControl.Size = new System.Drawing.Size(1076, 1012);
            // 
            // MSet
            // 
            this.MSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcMSet_FlipperMaxSpeed,
            this.dcMSet_FlipperAcceleration,
            this.dcMSet_FlipperDeceleration,
            this.dcMSet_FlipperSpeedRate,
            this.dcMSet_ConveyorWidthMaxSpeed,
            this.dcMSet_ConveyorWidthAcceleration,
            this.dcMSet_ConveyorWidthDeceleration,
            this.dcMSet_ConveyorWidthSpeedRate,
            this.dcMSet_CVBeltMaxSpeed,
            this.dcMSet_CVBeltAcceleration,
            this.dcMSet_CVBeltDeceleration,
            this.dcMSet_CVBeltSpeedRate});
            // 
            // PSet
            // 
            this.PSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dc_PSet_EnableIXsensor,
            this.dc_PSet_IpSensorIX,
            this.dc_PSet_PortSensorIX,
            this.dc_PSet_HeightUpperLimit,
            this.dc_PSet_EnablePopup,
            this.dc_PSet_HeightLowerLimit});
            // 
            // RSet
            // 
            this.RSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcRset_HeightUpperLimit,
            this.dcRSet_TakeBoardOutFlipped,
            this.Stop01,
            this.Stop02,
            this.Stop03,
            this.Stop04,
            this.dcRSet_TimeOutBoardStabilization,
            this.dcRset_HeightLowerLimit});
            // 
            // dcRset_HeightUpperLimit
            // 
            this.dcRset_HeightUpperLimit.AllowDBNull = false;
            this.dcRset_HeightUpperLimit.Caption = "HeightUpperLimit";
            this.dcRset_HeightUpperLimit.ColumnName = "HeightUpperLimit";
            this.dcRset_HeightUpperLimit.DataType = typeof(decimal);
            this.dcRset_HeightUpperLimit.DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // fcInitial_IX_Start
            // 
            this.fcInitial_IX_Start.AlarmCode = "";
            this.fcInitial_IX_Start.ArrowSeparation = 20;
            this.fcInitial_IX_Start.ArrowsWidth = 1;
            this.fcInitial_IX_Start.BackColor = System.Drawing.Color.White;
            this.fcInitial_IX_Start.bClearTrace = false;
            this.fcInitial_IX_Start.bIsLastFlowChart = false;
            this.fcInitial_IX_Start.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitial_IX_Start.bSkip = false;
            this.fcInitial_IX_Start.bStepByStepMode = false;
            this.fcInitial_IX_Start.CASE1 = this.fcInitial_End;
            this.fcInitial_IX_Start.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Square;
            this.fcInitial_IX_Start.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fcInitial_IX_Start.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Start.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Start.CASE2 = null;
            this.fcInitial_IX_Start.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_IX_Start.CASE2Color = System.Drawing.Color.Red;
            this.fcInitial_IX_Start.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Start.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Start.CASE3 = null;
            this.fcInitial_IX_Start.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_IX_Start.CASE3Color = System.Drawing.Color.Blue;
            this.fcInitial_IX_Start.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Start.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Start.CausesValidation = false;
            this.fcInitial_IX_Start.DefaultColor = System.Drawing.Color.White;
            this.fcInitial_IX_Start.eSkipPath = NPSDK.FCResultType.NEXT;
            this.fcInitial_IX_Start.ExecutedColor = System.Drawing.Color.LightGray;
            this.fcInitial_IX_Start.ExecutingColor = System.Drawing.Color.Lime;
            this.fcInitial_IX_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcInitial_IX_Start.Location = new System.Drawing.Point(417, 24);
            this.fcInitial_IX_Start.Margin = new System.Windows.Forms.Padding(8);
            this.fcInitial_IX_Start.menuOpening = false;
            this.fcInitial_IX_Start.Name = "fcInitial_IX_Start";
            this.fcInitial_IX_Start.NEXT = this.fcInitial_IX_Connect;
            this.fcInitial_IX_Start.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_IX_Start.NEXTColor = System.Drawing.Color.Black;
            this.fcInitial_IX_Start.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Start.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Start.Size = new System.Drawing.Size(151, 46);
            this.fcInitial_IX_Start.SkipColor = System.Drawing.Color.Yellow;
            this.fcInitial_IX_Start.SubFlowChart = null;
            this.fcInitial_IX_Start.TabIndex = 0;
            this.fcInitial_IX_Start.Text = "Inicio";
            this.fcInitial_IX_Start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitial_IX_Start.TimeOut = 10000;
            this.fcInitial_IX_Start.TimeoutColor = System.Drawing.Color.Red;
            this.fcInitial_IX_Start.tmrTimeOut = null;
            this.fcInitial_IX_Start.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.fcInitial_Start_FlowRun);
            // 
            // fcInitial_End
            // 
            this.fcInitial_End.AlarmCode = "";
            this.fcInitial_End.ArrowSeparation = 20;
            this.fcInitial_End.ArrowsWidth = 1;
            this.fcInitial_End.BackColor = System.Drawing.Color.White;
            this.fcInitial_End.bClearTrace = false;
            this.fcInitial_End.bIsLastFlowChart = false;
            this.fcInitial_End.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitial_End.bSkip = false;
            this.fcInitial_End.bStepByStepMode = false;
            this.fcInitial_End.CASE1 = null;
            this.fcInitial_End.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_End.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fcInitial_End.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_End.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_End.CASE2 = null;
            this.fcInitial_End.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_End.CASE2Color = System.Drawing.Color.Red;
            this.fcInitial_End.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_End.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_End.CASE3 = null;
            this.fcInitial_End.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_End.CASE3Color = System.Drawing.Color.Blue;
            this.fcInitial_End.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_End.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_End.CausesValidation = false;
            this.fcInitial_End.DefaultColor = System.Drawing.Color.White;
            this.fcInitial_End.eSkipPath = NPSDK.FCResultType.NEXT;
            this.fcInitial_End.ExecutedColor = System.Drawing.Color.LightGray;
            this.fcInitial_End.ExecutingColor = System.Drawing.Color.Lime;
            this.fcInitial_End.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcInitial_End.Location = new System.Drawing.Point(417, 204);
            this.fcInitial_End.Margin = new System.Windows.Forms.Padding(8);
            this.fcInitial_End.menuOpening = false;
            this.fcInitial_End.Name = "fcInitial_End";
            this.fcInitial_End.NEXT = null;
            this.fcInitial_End.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_End.NEXTColor = System.Drawing.Color.Black;
            this.fcInitial_End.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_End.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_End.Size = new System.Drawing.Size(151, 46);
            this.fcInitial_End.SkipColor = System.Drawing.Color.Yellow;
            this.fcInitial_End.SubFlowChart = null;
            this.fcInitial_End.TabIndex = 0;
            this.fcInitial_End.Text = "Fin";
            this.fcInitial_End.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitial_End.TimeOut = 10000;
            this.fcInitial_End.TimeoutColor = System.Drawing.Color.Red;
            this.fcInitial_End.tmrTimeOut = null;
            this.fcInitial_End.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.fcInitial_End_FlowRun);
            // 
            // fcInitial_IX_Connect
            // 
            this.fcInitial_IX_Connect.AlarmCode = "";
            this.fcInitial_IX_Connect.ArrowSeparation = 20;
            this.fcInitial_IX_Connect.ArrowsWidth = 1;
            this.fcInitial_IX_Connect.BackColor = System.Drawing.Color.White;
            this.fcInitial_IX_Connect.bClearTrace = false;
            this.fcInitial_IX_Connect.bIsLastFlowChart = false;
            this.fcInitial_IX_Connect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitial_IX_Connect.bSkip = false;
            this.fcInitial_IX_Connect.bStepByStepMode = false;
            this.fcInitial_IX_Connect.CASE1 = null;
            this.fcInitial_IX_Connect.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_IX_Connect.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fcInitial_IX_Connect.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Connect.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Connect.CASE2 = null;
            this.fcInitial_IX_Connect.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_IX_Connect.CASE2Color = System.Drawing.Color.Red;
            this.fcInitial_IX_Connect.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Connect.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Connect.CASE3 = null;
            this.fcInitial_IX_Connect.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_IX_Connect.CASE3Color = System.Drawing.Color.Blue;
            this.fcInitial_IX_Connect.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Connect.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Connect.CausesValidation = false;
            this.fcInitial_IX_Connect.DefaultColor = System.Drawing.Color.White;
            this.fcInitial_IX_Connect.eSkipPath = NPSDK.FCResultType.NEXT;
            this.fcInitial_IX_Connect.ExecutedColor = System.Drawing.Color.LightGray;
            this.fcInitial_IX_Connect.ExecutingColor = System.Drawing.Color.Lime;
            this.fcInitial_IX_Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcInitial_IX_Connect.Location = new System.Drawing.Point(417, 84);
            this.fcInitial_IX_Connect.Margin = new System.Windows.Forms.Padding(8);
            this.fcInitial_IX_Connect.menuOpening = false;
            this.fcInitial_IX_Connect.Name = "fcInitial_IX_Connect";
            this.fcInitial_IX_Connect.NEXT = this.fcInit_CylinderInit;
            this.fcInitial_IX_Connect.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInitial_IX_Connect.NEXTColor = System.Drawing.Color.Black;
            this.fcInitial_IX_Connect.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Connect.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInitial_IX_Connect.Size = new System.Drawing.Size(151, 46);
            this.fcInitial_IX_Connect.SkipColor = System.Drawing.Color.Yellow;
            this.fcInitial_IX_Connect.SubFlowChart = null;
            this.fcInitial_IX_Connect.TabIndex = 12;
            this.fcInitial_IX_Connect.Text = "Connectar";
            this.fcInitial_IX_Connect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitial_IX_Connect.TimeOut = 10000;
            this.fcInitial_IX_Connect.TimeoutColor = System.Drawing.Color.Red;
            this.fcInitial_IX_Connect.tmrTimeOut = null;
            this.fcInitial_IX_Connect.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.fcInitial_WaitGantryHome_FlowRun);
            // 
            // fcInit_CylinderInit
            // 
            this.fcInit_CylinderInit.AlarmCode = "";
            this.fcInit_CylinderInit.ArrowSeparation = 20;
            this.fcInit_CylinderInit.ArrowsWidth = 1;
            this.fcInit_CylinderInit.BackColor = System.Drawing.Color.White;
            this.fcInit_CylinderInit.bClearTrace = false;
            this.fcInit_CylinderInit.bIsLastFlowChart = false;
            this.fcInit_CylinderInit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInit_CylinderInit.bSkip = false;
            this.fcInit_CylinderInit.bStepByStepMode = false;
            this.fcInit_CylinderInit.CASE1 = null;
            this.fcInit_CylinderInit.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInit_CylinderInit.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fcInit_CylinderInit.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInit_CylinderInit.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInit_CylinderInit.CASE2 = null;
            this.fcInit_CylinderInit.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInit_CylinderInit.CASE2Color = System.Drawing.Color.Red;
            this.fcInit_CylinderInit.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInit_CylinderInit.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInit_CylinderInit.CASE3 = null;
            this.fcInit_CylinderInit.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInit_CylinderInit.CASE3Color = System.Drawing.Color.Blue;
            this.fcInit_CylinderInit.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInit_CylinderInit.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInit_CylinderInit.CausesValidation = false;
            this.fcInit_CylinderInit.DefaultColor = System.Drawing.Color.White;
            this.fcInit_CylinderInit.eSkipPath = NPSDK.FCResultType.NEXT;
            this.fcInit_CylinderInit.ExecutedColor = System.Drawing.Color.LightGray;
            this.fcInit_CylinderInit.ExecutingColor = System.Drawing.Color.Lime;
            this.fcInit_CylinderInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcInit_CylinderInit.Location = new System.Drawing.Point(417, 142);
            this.fcInit_CylinderInit.Margin = new System.Windows.Forms.Padding(8);
            this.fcInit_CylinderInit.menuOpening = false;
            this.fcInit_CylinderInit.Name = "fcInit_CylinderInit";
            this.fcInit_CylinderInit.NEXT = this.fcInitial_End;
            this.fcInit_CylinderInit.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fcInit_CylinderInit.NEXTColor = System.Drawing.Color.Black;
            this.fcInit_CylinderInit.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInit_CylinderInit.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fcInit_CylinderInit.Size = new System.Drawing.Size(151, 46);
            this.fcInit_CylinderInit.SkipColor = System.Drawing.Color.Yellow;
            this.fcInit_CylinderInit.SubFlowChart = null;
            this.fcInit_CylinderInit.TabIndex = 9;
            this.fcInit_CylinderInit.Text = "Verificar Conexion";
            this.fcInit_CylinderInit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInit_CylinderInit.TimeOut = 10000;
            this.fcInit_CylinderInit.TimeoutColor = System.Drawing.Color.Red;
            this.fcInit_CylinderInit.tmrTimeOut = null;
            this.fcInit_CylinderInit.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.fcInit_Cylinder_FlowRun);
            // 
            // dcRSet_TakeBoardOutFlipped
            // 
            this.dcRSet_TakeBoardOutFlipped.AllowDBNull = false;
            this.dcRSet_TakeBoardOutFlipped.ColumnName = "TakeBoardOutFlipped";
            this.dcRSet_TakeBoardOutFlipped.DataType = typeof(bool);
            this.dcRSet_TakeBoardOutFlipped.DefaultValue = false;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1076, 36);
            this.label21.TabIndex = 4;
            this.label21.Text = "Inicial Sensor IX";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dcMSet_FlipperMaxSpeed
            // 
            this.dcMSet_FlipperMaxSpeed.AllowDBNull = false;
            this.dcMSet_FlipperMaxSpeed.ColumnName = "FlipperMaxSpeed";
            this.dcMSet_FlipperMaxSpeed.DataType = typeof(float);
            this.dcMSet_FlipperMaxSpeed.DefaultValue = 100F;
            // 
            // dcMSet_FlipperAcceleration
            // 
            this.dcMSet_FlipperAcceleration.AllowDBNull = false;
            this.dcMSet_FlipperAcceleration.ColumnName = "FlipperAcceleration";
            this.dcMSet_FlipperAcceleration.DataType = typeof(float);
            this.dcMSet_FlipperAcceleration.DefaultValue = 0.5F;
            // 
            // dcMSet_FlipperDeceleration
            // 
            this.dcMSet_FlipperDeceleration.AllowDBNull = false;
            this.dcMSet_FlipperDeceleration.ColumnName = "FlipperDeceleration";
            this.dcMSet_FlipperDeceleration.DataType = typeof(float);
            this.dcMSet_FlipperDeceleration.DefaultValue = 0.5F;
            // 
            // dcMSet_FlipperSpeedRate
            // 
            this.dcMSet_FlipperSpeedRate.AllowDBNull = false;
            this.dcMSet_FlipperSpeedRate.ColumnName = "FlipperSpeedRate";
            this.dcMSet_FlipperSpeedRate.DataType = typeof(int);
            this.dcMSet_FlipperSpeedRate.DefaultValue = 10;
            // 
            // dcMSet_ConveyorWidthMaxSpeed
            // 
            this.dcMSet_ConveyorWidthMaxSpeed.AllowDBNull = false;
            this.dcMSet_ConveyorWidthMaxSpeed.ColumnName = "ConveyorWidthMaxSpeed";
            this.dcMSet_ConveyorWidthMaxSpeed.DataType = typeof(float);
            this.dcMSet_ConveyorWidthMaxSpeed.DefaultValue = 100F;
            // 
            // dcMSet_ConveyorWidthAcceleration
            // 
            this.dcMSet_ConveyorWidthAcceleration.AllowDBNull = false;
            this.dcMSet_ConveyorWidthAcceleration.ColumnName = "ConveyorWidthAcceleration";
            this.dcMSet_ConveyorWidthAcceleration.DataType = typeof(float);
            this.dcMSet_ConveyorWidthAcceleration.DefaultValue = 0.5F;
            // 
            // dcMSet_ConveyorWidthDeceleration
            // 
            this.dcMSet_ConveyorWidthDeceleration.AllowDBNull = false;
            this.dcMSet_ConveyorWidthDeceleration.ColumnName = "ConveyorWidthDeceleration";
            this.dcMSet_ConveyorWidthDeceleration.DataType = typeof(float);
            this.dcMSet_ConveyorWidthDeceleration.DefaultValue = 0.5F;
            // 
            // dcMSet_ConveyorWidthSpeedRate
            // 
            this.dcMSet_ConveyorWidthSpeedRate.AllowDBNull = false;
            this.dcMSet_ConveyorWidthSpeedRate.ColumnName = "ConveyorWidthSpeedRate";
            this.dcMSet_ConveyorWidthSpeedRate.DataType = typeof(int);
            this.dcMSet_ConveyorWidthSpeedRate.DefaultValue = 10;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.55435F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.44565F));
            this.tableLayoutPanel10.Controls.Add(this.panel31, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 477F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1076, 976);
            this.tableLayoutPanel10.TabIndex = 5;
            // 
            // panel31
            // 
            this.panel31.AutoScroll = true;
            this.panel31.Controls.Add(this.fcInitial_IX_Connect);
            this.panel31.Controls.Add(this.fcInit_CylinderInit);
            this.panel31.Controls.Add(this.fcInitial_IX_Start);
            this.panel31.Controls.Add(this.fcInitial_End);
            this.panel31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel31.Location = new System.Drawing.Point(3, 3);
            this.panel31.Name = "panel31";
            this.panel31.Size = new System.Drawing.Size(1070, 970);
            this.panel31.TabIndex = 0;
            // 
            // Stop01
            // 
            this.Stop01.ColumnName = "Stop01";
            this.Stop01.DataType = typeof(bool);
            // 
            // Stop02
            // 
            this.Stop02.ColumnName = "Stop02";
            this.Stop02.DataType = typeof(bool);
            // 
            // Stop03
            // 
            this.Stop03.ColumnName = "Stop03";
            this.Stop03.DataType = typeof(bool);
            // 
            // Stop04
            // 
            this.Stop04.ColumnName = "Stop04";
            this.Stop04.DataType = typeof(bool);
            // 
            // dcMSet_CVBeltMaxSpeed
            // 
            this.dcMSet_CVBeltMaxSpeed.AllowDBNull = false;
            this.dcMSet_CVBeltMaxSpeed.ColumnName = "CVBeltMaxSpeed";
            this.dcMSet_CVBeltMaxSpeed.DataType = typeof(double);
            this.dcMSet_CVBeltMaxSpeed.DefaultValue = 100D;
            // 
            // dcMSet_CVBeltAcceleration
            // 
            this.dcMSet_CVBeltAcceleration.AllowDBNull = false;
            this.dcMSet_CVBeltAcceleration.ColumnName = "CVBeltAcceleration";
            this.dcMSet_CVBeltAcceleration.DataType = typeof(double);
            this.dcMSet_CVBeltAcceleration.DefaultValue = 0.1D;
            // 
            // dcMSet_CVBeltDeceleration
            // 
            this.dcMSet_CVBeltDeceleration.AllowDBNull = false;
            this.dcMSet_CVBeltDeceleration.ColumnName = "CVBeltDeceleration";
            this.dcMSet_CVBeltDeceleration.DataType = typeof(double);
            this.dcMSet_CVBeltDeceleration.DefaultValue = 0.1D;
            // 
            // dcMSet_CVBeltSpeedRate
            // 
            this.dcMSet_CVBeltSpeedRate.AllowDBNull = false;
            this.dcMSet_CVBeltSpeedRate.ColumnName = "CVBeltSpeedRate";
            this.dcMSet_CVBeltSpeedRate.DataType = typeof(int);
            this.dcMSet_CVBeltSpeedRate.DefaultValue = 100;
            // 
            // tmrCurrentPositions
            // 
            this.tmrCurrentPositions.Enabled = true;
            // 
            // dcRSet_TimeOutBoardStabilization
            // 
            this.dcRSet_TimeOutBoardStabilization.AllowDBNull = false;
            this.dcRSet_TimeOutBoardStabilization.ColumnName = "TimeOutBoardStabilization";
            this.dcRSet_TimeOutBoardStabilization.DataType = typeof(double);
            this.dcRSet_TimeOutBoardStabilization.DefaultValue = 0D;
            // 
            // IXSensorControl
            // 
            this.IXSensorControl.bDone = false;
            this.IXSensorControl.DataBindings.Add(new System.Windows.Forms.Binding("SensorIP", this.SettingData, "PSet.IpSensorIX", true));
            this.IXSensorControl.DataBindings.Add(new System.Windows.Forms.Binding("SensorPort", this.SettingData, "PSet.PortSensorIX", true));
            this.IXSensorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IXSensorControl.Location = new System.Drawing.Point(0, 0);
            this.IXSensorControl.Margin = new System.Windows.Forms.Padding(37, 41, 37, 41);
            this.IXSensorControl.Name = "IXSensorControl";
            this.IXSensorControl.Result = false;
            this.IXSensorControl.ResultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.IXSensorControl.SensorIP = "";
            this.IXSensorControl.SensorPort = "";
            this.IXSensorControl.Size = new System.Drawing.Size(1070, 708);
            this.IXSensorControl.TabIndex = 1;
            this.IXSensorControl.Load += new System.EventHandler(this.IXSensorControl_Load);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1076, 36);
            this.label1.TabIndex = 5;
            this.label1.Text = "IX sensor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dc_PSet_EnableIXsensor
            // 
            this.dc_PSet_EnableIXsensor.Caption = "EnableIXsensor";
            this.dc_PSet_EnableIXsensor.ColumnName = "EnableIXsensor";
            this.dc_PSet_EnableIXsensor.DataType = typeof(bool);
            this.dc_PSet_EnableIXsensor.DefaultValue = false;
            // 
            // dc_PSet_IpSensorIX
            // 
            this.dc_PSet_IpSensorIX.Caption = "IpSensorIX";
            this.dc_PSet_IpSensorIX.ColumnName = "IpSensorIX";
            // 
            // dc_PSet_PortSensorIX
            // 
            this.dc_PSet_PortSensorIX.Caption = "PortSensorIX";
            this.dc_PSet_PortSensorIX.ColumnName = "PortSensorIX";
            // 
            // tbXSafePos
            // 
            this.tbXSafePos.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.RecipeData, "RSet.HeightUpperLimit", true));
            this.tbXSafePos.Location = new System.Drawing.Point(408, 144);
            this.tbXSafePos.Name = "tbXSafePos";
            this.tbXSafePos.Size = new System.Drawing.Size(100, 34);
            this.tbXSafePos.TabIndex = 167;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(526, 147);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(53, 29);
            this.label99.TabIndex = 166;
            this.label99.Text = "mm";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(173, 148);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(199, 29);
            this.label98.TabIndex = 165;
            this.label98.Text = "Highly upper limit";
            // 
            // dc_PSet_HeightUpperLimit
            // 
            this.dc_PSet_HeightUpperLimit.Caption = "HeightUpperLimit";
            this.dc_PSet_HeightUpperLimit.ColumnName = "HeightUpperLimit";
            this.dc_PSet_HeightUpperLimit.DataType = typeof(decimal);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.88391F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.11609F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1076, 976);
            this.tableLayoutPanel1.TabIndex = 168;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.myCheckBox2);
            this.panel1.Controls.Add(this.myCheckBox1);
            this.panel1.Controls.Add(this.tbXSafePos);
            this.panel1.Controls.Add(this.label98);
            this.panel1.Controls.Add(this.label99);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 256);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.RecipeData, "RSet.HeightLowerLimit", true));
            this.textBox1.Location = new System.Drawing.Point(408, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 34);
            this.textBox1.TabIndex = 171;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 29);
            this.label2.TabIndex = 169;
            this.label2.Text = "Height lower limit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(526, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 29);
            this.label3.TabIndex = 170;
            this.label3.Text = "mm";
            // 
            // myCheckBox2
            // 
            this.myCheckBox2.AutoSize = true;
            this.myCheckBox2.Checked = false;
            this.myCheckBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.SettingData, "PSet.EnablePopup", true));
            this.myCheckBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.myCheckBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myCheckBox2.Location = new System.Drawing.Point(0, 60);
            this.myCheckBox2.Margin = new System.Windows.Forms.Padding(1);
            this.myCheckBox2.MaximumSize = new System.Drawing.Size(0, 60);
            this.myCheckBox2.MinimumSize = new System.Drawing.Size(62, 60);
            this.myCheckBox2.Name = "myCheckBox2";
            this.myCheckBox2.Size = new System.Drawing.Size(1070, 60);
            this.myCheckBox2.TabIndex = 168;
            this.myCheckBox2.Text = "Enable the failed popup window";
            // 
            // myCheckBox1
            // 
            this.myCheckBox1.AutoSize = true;
            this.myCheckBox1.Checked = false;
            this.myCheckBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.SettingData, "PSet.EnableIXsensor", true));
            this.myCheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myCheckBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myCheckBox1.Location = new System.Drawing.Point(0, 0);
            this.myCheckBox1.Margin = new System.Windows.Forms.Padding(1);
            this.myCheckBox1.MaximumSize = new System.Drawing.Size(0, 60);
            this.myCheckBox1.MinimumSize = new System.Drawing.Size(62, 60);
            this.myCheckBox1.Name = "myCheckBox1";
            this.myCheckBox1.Size = new System.Drawing.Size(1070, 60);
            this.myCheckBox1.TabIndex = 165;
            this.myCheckBox1.Text = "Enable sensor IX";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.IXSensorControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 265);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1070, 708);
            this.panel2.TabIndex = 1;
            // 
            // dc_PSet_EnablePopup
            // 
            this.dc_PSet_EnablePopup.Caption = "EnablePopup";
            this.dc_PSet_EnablePopup.ColumnName = "EnablePopup";
            this.dc_PSet_EnablePopup.DataType = typeof(bool);
            this.dc_PSet_EnablePopup.DefaultValue = false;
            // 
            // dcRset_HeightLowerLimit
            // 
            this.dcRset_HeightLowerLimit.Caption = "HeightLowerLimit";
            this.dcRset_HeightLowerLimit.ColumnName = "HeightLowerLimit";
            this.dcRset_HeightLowerLimit.DataType = typeof(decimal);
            this.dcRset_HeightLowerLimit.DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // dc_PSet_HeightLowerLimit
            // 
            this.dc_PSet_HeightLowerLimit.Caption = "HeightLowerLimit";
            this.dc_PSet_HeightLowerLimit.ColumnName = "HeightLowerLimit";
            this.dc_PSet_HeightLowerLimit.DataType = typeof(decimal);
            // 
            // IXSensorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1084, 1055);
            this.Name = "IXSensorForm";
            this.Text = "IXSensor";
            this.plProductionSetting.ResumeLayout(false);
            this.plFlowInitial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.panel31.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Data.DataColumn dcRset_HeightUpperLimit;
        private NPSDK.NPFlowChart fcInitial_IX_Start;
        private NPSDK.NPFlowChart fcInitial_End;
        private System.Data.DataColumn dcRSet_TakeBoardOutFlipped;
        private System.Windows.Forms.Label label21;
        private System.Data.DataColumn dcMSet_FlipperMaxSpeed;
        private System.Data.DataColumn dcMSet_FlipperAcceleration;
        private System.Data.DataColumn dcMSet_FlipperDeceleration;
        private System.Data.DataColumn dcMSet_FlipperSpeedRate;
        private System.Data.DataColumn dcMSet_ConveyorWidthMaxSpeed;
        private System.Data.DataColumn dcMSet_ConveyorWidthAcceleration;
        private System.Data.DataColumn dcMSet_ConveyorWidthDeceleration;
        private System.Data.DataColumn dcMSet_ConveyorWidthSpeedRate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Panel panel31;
        private NPSDK.NPInput IB_StopIzq4_Dw;
        private System.Data.DataColumn Stop01;
        private System.Data.DataColumn Stop02;
        private System.Data.DataColumn Stop03;
        private System.Data.DataColumn Stop04;
        private NPSDK.NPInput IB_StopDer4_Dw;
        private System.Data.DataColumn dcMSet_CVBeltMaxSpeed;
        private System.Data.DataColumn dcMSet_CVBeltAcceleration;
        private System.Data.DataColumn dcMSet_CVBeltDeceleration;
        private System.Data.DataColumn dcMSet_CVBeltSpeedRate;
        private System.Windows.Forms.Timer tmrCurrentPositions;
        private System.Data.DataColumn dcRSet_TimeOutBoardStabilization;
        private NPSDK.NPFlowChart fcInit_CylinderInit;
        private NPSDK.NPFlowChart fcInitial_IX_Connect;
        private System.Windows.Forms.Label label1;
        private System.Data.DataColumn dc_PSet_EnableIXsensor;
        private System.Data.DataColumn dc_PSet_IpSensorIX;
        private System.Data.DataColumn dc_PSet_PortSensorIX;
        public Control_IX.KeyenceIXSensorControl IXSensorControl;
        private System.Windows.Forms.TextBox tbXSafePos;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label98;
        private System.Data.DataColumn dc_PSet_HeightUpperLimit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private tstCkBox.MyCheckBox myCheckBox1;
        private System.Windows.Forms.Panel panel2;
        private tstCkBox.MyCheckBox myCheckBox2;
        private System.Data.DataColumn dc_PSet_EnablePopup;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Data.DataColumn dc_PSet_HeightLowerLimit;
        private System.Data.DataColumn dcRset_HeightLowerLimit;
    }
}
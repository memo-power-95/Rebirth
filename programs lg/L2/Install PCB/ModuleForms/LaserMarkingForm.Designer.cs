
namespace Acura3.ModuleForms
{
    partial class LaserMarkingForm
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
            this.fcInitial_LaserMarking_ResetErrors = new JabilSDK.FlowChart();
            this.fcInitial_LaserMarking_End = new JabilSDK.FlowChart();
            this.fcInitial_LaserMarking_Connect = new JabilSDK.FlowChart();
            this.fcInitial_LaserMarking_Start = new JabilSDK.FlowChart();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dcPSet_IP = new System.Data.DataColumn();
            this.dcPSet_Port = new System.Data.DataColumn();
            this.dt_MarkingData = new System.Data.DataTable();
            this.dcMarkingData_Id = new System.Data.DataColumn();
            this.dcMarkingData_BlockNumber = new System.Data.DataColumn();
            this.dcMarkingData_SerialNumberIndex = new System.Data.DataColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.nudSerialQuantity = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvMarkingData = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BlockNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumberIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcRSet_SerialNumbersQuantity = new System.Data.DataColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.cmbCommands = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbLaserMarkingHistory = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lvScannedSerialNumbers = new System.Windows.Forms.ListView();
            this.lvGeneratedSerialNumbers = new System.Windows.Forms.ListView();
            this.label11 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.myCheckBox2 = new tstCkBox.MyCheckBox();
            this.dcRSet_ValidateMarking = new System.Data.DataColumn();
            this.Enabled_Laser = new System.Data.DataColumn();
            this.plProductionSetting.SuspendLayout();
            this.plRecipeEditor.SuspendLayout();
            this.plFlowInitial.SuspendLayout();
            this.plMachineStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_MarkingData)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSerialQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarkingData)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaintenance
            // 
            this.plMaintenance.Size = new System.Drawing.Size(733, 737);
            // 
            // plProductionSetting
            // 
            this.plProductionSetting.Controls.Add(this.panel1);
            this.plProductionSetting.Controls.Add(this.panel8);
            this.plProductionSetting.Size = new System.Drawing.Size(733, 737);
            // 
            // plRecipeEditor
            // 
            this.plRecipeEditor.Controls.Add(this.tabControl2);
            this.plRecipeEditor.Size = new System.Drawing.Size(733, 737);
            // 
            // plFlowInitial
            // 
            this.plFlowInitial.Controls.Add(this.fcInitial_LaserMarking_ResetErrors);
            this.plFlowInitial.Controls.Add(this.fcInitial_LaserMarking_Connect);
            this.plFlowInitial.Controls.Add(this.fcInitial_LaserMarking_End);
            this.plFlowInitial.Controls.Add(this.fcInitial_LaserMarking_Start);
            this.plFlowInitial.Size = new System.Drawing.Size(733, 737);
            // 
            // plFlowAuto
            // 
            this.plFlowAuto.Size = new System.Drawing.Size(733, 737);
            // 
            // plMachineStatus
            // 
            this.plMachineStatus.Controls.Add(this.tableLayoutPanel1);
            this.plMachineStatus.Size = new System.Drawing.Size(733, 737);
            // 
            // plMotionSetup
            // 
            this.plMotionSetup.Size = new System.Drawing.Size(733, 737);
            // 
            // plMotorControl
            // 
            this.plMotorControl.Size = new System.Drawing.Size(733, 737);
            // 
            // PSet
            // 
            this.PSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcPSet_IP,
            this.dcPSet_Port,
            this.Enabled_Laser});
            // 
            // RecipeData
            // 
            this.RecipeData.Tables.AddRange(new System.Data.DataTable[] {
            this.dt_MarkingData});
            // 
            // RSet
            // 
            this.RSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcRSet_SerialNumbersQuantity,
            this.dcRSet_ValidateMarking});
            // 
            // fcInitial_LaserMarking_ResetErrors
            // 
            this.fcInitial_LaserMarking_ResetErrors.AlarmCode = "";
            this.fcInitial_LaserMarking_ResetErrors.ArrowSeparation = 20;
            this.fcInitial_LaserMarking_ResetErrors.ArrowsWidth = 1;
            this.fcInitial_LaserMarking_ResetErrors.BackColor = System.Drawing.Color.White;
            this.fcInitial_LaserMarking_ResetErrors.bClearTrace = false;
            this.fcInitial_LaserMarking_ResetErrors.bIsLastFlowChart = false;
            this.fcInitial_LaserMarking_ResetErrors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitial_LaserMarking_ResetErrors.bSkip = false;
            this.fcInitial_LaserMarking_ResetErrors.bStepByStepMode = false;
            this.fcInitial_LaserMarking_ResetErrors.CASE1 = null;
            this.fcInitial_LaserMarking_ResetErrors.Case1ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_ResetErrors.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fcInitial_LaserMarking_ResetErrors.CASE1EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_ResetErrors.Case1StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_ResetErrors.CASE2 = null;
            this.fcInitial_LaserMarking_ResetErrors.Case2ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_ResetErrors.CASE2Color = System.Drawing.Color.Red;
            this.fcInitial_LaserMarking_ResetErrors.CASE2EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_ResetErrors.Case2StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_ResetErrors.CASE3 = null;
            this.fcInitial_LaserMarking_ResetErrors.Case3ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_ResetErrors.CASE3Color = System.Drawing.Color.Blue;
            this.fcInitial_LaserMarking_ResetErrors.CASE3EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_ResetErrors.Case3StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_ResetErrors.CausesValidation = false;
            this.fcInitial_LaserMarking_ResetErrors.DefaultColor = System.Drawing.Color.White;
            this.fcInitial_LaserMarking_ResetErrors.eSkipPath = JabilSDK.FCResultType.NEXT;
            this.fcInitial_LaserMarking_ResetErrors.ExecutedColor = System.Drawing.Color.LightGray;
            this.fcInitial_LaserMarking_ResetErrors.ExecutingColor = System.Drawing.Color.Lime;
            this.fcInitial_LaserMarking_ResetErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcInitial_LaserMarking_ResetErrors.Location = new System.Drawing.Point(24, 137);
            this.fcInitial_LaserMarking_ResetErrors.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitial_LaserMarking_ResetErrors.menuOpening = false;
            this.fcInitial_LaserMarking_ResetErrors.Name = "fcInitial_LaserMarking_ResetErrors";
            this.fcInitial_LaserMarking_ResetErrors.NEXT = this.fcInitial_LaserMarking_End;
            this.fcInitial_LaserMarking_ResetErrors.NextArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_ResetErrors.NEXTColor = System.Drawing.Color.Black;
            this.fcInitial_LaserMarking_ResetErrors.NEXTEndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_ResetErrors.NEXTStartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_ResetErrors.Size = new System.Drawing.Size(180, 42);
            this.fcInitial_LaserMarking_ResetErrors.SkipColor = System.Drawing.Color.Yellow;
            this.fcInitial_LaserMarking_ResetErrors.SubFlowChart = null;
            this.fcInitial_LaserMarking_ResetErrors.TabIndex = 1;
            this.fcInitial_LaserMarking_ResetErrors.Text = "ResetErrors";
            this.fcInitial_LaserMarking_ResetErrors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitial_LaserMarking_ResetErrors.TimeOut = 10000;
            this.fcInitial_LaserMarking_ResetErrors.TimeoutColor = System.Drawing.Color.Red;
            this.fcInitial_LaserMarking_ResetErrors.tmrTimeOut = null;
            this.fcInitial_LaserMarking_ResetErrors.FlowRun += new JabilSDK.FlowChart.FlowRunEvent(this.fcInitial_LaserMarking_ResetErrors_FlowRun);
            // 
            // fcInitial_LaserMarking_End
            // 
            this.fcInitial_LaserMarking_End.AlarmCode = "";
            this.fcInitial_LaserMarking_End.ArrowSeparation = 20;
            this.fcInitial_LaserMarking_End.ArrowsWidth = 1;
            this.fcInitial_LaserMarking_End.BackColor = System.Drawing.Color.White;
            this.fcInitial_LaserMarking_End.bClearTrace = false;
            this.fcInitial_LaserMarking_End.bIsLastFlowChart = false;
            this.fcInitial_LaserMarking_End.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitial_LaserMarking_End.bSkip = false;
            this.fcInitial_LaserMarking_End.bStepByStepMode = false;
            this.fcInitial_LaserMarking_End.CASE1 = null;
            this.fcInitial_LaserMarking_End.Case1ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_End.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fcInitial_LaserMarking_End.CASE1EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_End.Case1StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_End.CASE2 = null;
            this.fcInitial_LaserMarking_End.Case2ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_End.CASE2Color = System.Drawing.Color.Red;
            this.fcInitial_LaserMarking_End.CASE2EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_End.Case2StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_End.CASE3 = null;
            this.fcInitial_LaserMarking_End.Case3ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_End.CASE3Color = System.Drawing.Color.Blue;
            this.fcInitial_LaserMarking_End.CASE3EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_End.Case3StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_End.CausesValidation = false;
            this.fcInitial_LaserMarking_End.DefaultColor = System.Drawing.Color.White;
            this.fcInitial_LaserMarking_End.eSkipPath = JabilSDK.FCResultType.NEXT;
            this.fcInitial_LaserMarking_End.ExecutedColor = System.Drawing.Color.LightGray;
            this.fcInitial_LaserMarking_End.ExecutingColor = System.Drawing.Color.Lime;
            this.fcInitial_LaserMarking_End.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcInitial_LaserMarking_End.Location = new System.Drawing.Point(24, 193);
            this.fcInitial_LaserMarking_End.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitial_LaserMarking_End.menuOpening = false;
            this.fcInitial_LaserMarking_End.Name = "fcInitial_LaserMarking_End";
            this.fcInitial_LaserMarking_End.NEXT = null;
            this.fcInitial_LaserMarking_End.NextArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_End.NEXTColor = System.Drawing.Color.Black;
            this.fcInitial_LaserMarking_End.NEXTEndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_End.NEXTStartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_End.Size = new System.Drawing.Size(180, 42);
            this.fcInitial_LaserMarking_End.SkipColor = System.Drawing.Color.Yellow;
            this.fcInitial_LaserMarking_End.SubFlowChart = null;
            this.fcInitial_LaserMarking_End.TabIndex = 3;
            this.fcInitial_LaserMarking_End.Text = "Finish";
            this.fcInitial_LaserMarking_End.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitial_LaserMarking_End.TimeOut = 10000;
            this.fcInitial_LaserMarking_End.TimeoutColor = System.Drawing.Color.Red;
            this.fcInitial_LaserMarking_End.tmrTimeOut = null;
            this.fcInitial_LaserMarking_End.FlowRun += new JabilSDK.FlowChart.FlowRunEvent(this.fcInitial_LaserMarking_End_FlowRun);
            // 
            // fcInitial_LaserMarking_Connect
            // 
            this.fcInitial_LaserMarking_Connect.AlarmCode = "";
            this.fcInitial_LaserMarking_Connect.ArrowSeparation = 20;
            this.fcInitial_LaserMarking_Connect.ArrowsWidth = 1;
            this.fcInitial_LaserMarking_Connect.BackColor = System.Drawing.Color.White;
            this.fcInitial_LaserMarking_Connect.bClearTrace = false;
            this.fcInitial_LaserMarking_Connect.bIsLastFlowChart = false;
            this.fcInitial_LaserMarking_Connect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitial_LaserMarking_Connect.bSkip = false;
            this.fcInitial_LaserMarking_Connect.bStepByStepMode = false;
            this.fcInitial_LaserMarking_Connect.CASE1 = null;
            this.fcInitial_LaserMarking_Connect.Case1ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_Connect.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fcInitial_LaserMarking_Connect.CASE1EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Connect.Case1StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Connect.CASE2 = null;
            this.fcInitial_LaserMarking_Connect.Case2ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_Connect.CASE2Color = System.Drawing.Color.Red;
            this.fcInitial_LaserMarking_Connect.CASE2EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Connect.Case2StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Connect.CASE3 = null;
            this.fcInitial_LaserMarking_Connect.Case3ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_Connect.CASE3Color = System.Drawing.Color.Blue;
            this.fcInitial_LaserMarking_Connect.CASE3EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Connect.Case3StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Connect.CausesValidation = false;
            this.fcInitial_LaserMarking_Connect.DefaultColor = System.Drawing.Color.White;
            this.fcInitial_LaserMarking_Connect.eSkipPath = JabilSDK.FCResultType.NEXT;
            this.fcInitial_LaserMarking_Connect.ExecutedColor = System.Drawing.Color.LightGray;
            this.fcInitial_LaserMarking_Connect.ExecutingColor = System.Drawing.Color.Lime;
            this.fcInitial_LaserMarking_Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcInitial_LaserMarking_Connect.Location = new System.Drawing.Point(24, 80);
            this.fcInitial_LaserMarking_Connect.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitial_LaserMarking_Connect.menuOpening = false;
            this.fcInitial_LaserMarking_Connect.Name = "fcInitial_LaserMarking_Connect";
            this.fcInitial_LaserMarking_Connect.NEXT = this.fcInitial_LaserMarking_ResetErrors;
            this.fcInitial_LaserMarking_Connect.NextArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_Connect.NEXTColor = System.Drawing.Color.Black;
            this.fcInitial_LaserMarking_Connect.NEXTEndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Connect.NEXTStartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Connect.Size = new System.Drawing.Size(180, 42);
            this.fcInitial_LaserMarking_Connect.SkipColor = System.Drawing.Color.Yellow;
            this.fcInitial_LaserMarking_Connect.SubFlowChart = null;
            this.fcInitial_LaserMarking_Connect.TabIndex = 2;
            this.fcInitial_LaserMarking_Connect.Text = "Connect";
            this.fcInitial_LaserMarking_Connect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitial_LaserMarking_Connect.TimeOut = 10000;
            this.fcInitial_LaserMarking_Connect.TimeoutColor = System.Drawing.Color.Red;
            this.fcInitial_LaserMarking_Connect.tmrTimeOut = null;
            this.fcInitial_LaserMarking_Connect.FlowRun += new JabilSDK.FlowChart.FlowRunEvent(this.fcInitial_LaserMarking_Connect_FlowRun);
            // 
            // fcInitial_LaserMarking_Start
            // 
            this.fcInitial_LaserMarking_Start.AlarmCode = "";
            this.fcInitial_LaserMarking_Start.ArrowSeparation = 20;
            this.fcInitial_LaserMarking_Start.ArrowsWidth = 1;
            this.fcInitial_LaserMarking_Start.BackColor = System.Drawing.Color.White;
            this.fcInitial_LaserMarking_Start.bClearTrace = true;
            this.fcInitial_LaserMarking_Start.bIsLastFlowChart = false;
            this.fcInitial_LaserMarking_Start.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitial_LaserMarking_Start.bSkip = false;
            this.fcInitial_LaserMarking_Start.bStepByStepMode = false;
            this.fcInitial_LaserMarking_Start.CASE1 = null;
            this.fcInitial_LaserMarking_Start.Case1ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_Start.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fcInitial_LaserMarking_Start.CASE1EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Start.Case1StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Start.CASE2 = null;
            this.fcInitial_LaserMarking_Start.Case2ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_Start.CASE2Color = System.Drawing.Color.Red;
            this.fcInitial_LaserMarking_Start.CASE2EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Start.Case2StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Start.CASE3 = null;
            this.fcInitial_LaserMarking_Start.Case3ArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_Start.CASE3Color = System.Drawing.Color.Blue;
            this.fcInitial_LaserMarking_Start.CASE3EndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Start.Case3StartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Start.CausesValidation = false;
            this.fcInitial_LaserMarking_Start.DefaultColor = System.Drawing.Color.White;
            this.fcInitial_LaserMarking_Start.eSkipPath = JabilSDK.FCResultType.NEXT;
            this.fcInitial_LaserMarking_Start.ExecutedColor = System.Drawing.Color.LightGray;
            this.fcInitial_LaserMarking_Start.ExecutingColor = System.Drawing.Color.Lime;
            this.fcInitial_LaserMarking_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fcInitial_LaserMarking_Start.Location = new System.Drawing.Point(24, 24);
            this.fcInitial_LaserMarking_Start.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitial_LaserMarking_Start.menuOpening = false;
            this.fcInitial_LaserMarking_Start.Name = "fcInitial_LaserMarking_Start";
            this.fcInitial_LaserMarking_Start.NEXT = this.fcInitial_LaserMarking_Connect;
            this.fcInitial_LaserMarking_Start.NextArrowType = JabilSDK.FlowChart.ArrowTypes.Line;
            this.fcInitial_LaserMarking_Start.NEXTColor = System.Drawing.Color.Black;
            this.fcInitial_LaserMarking_Start.NEXTEndPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Start.NEXTStartPoint = JabilSDK.FlowChart.FlowchartSides.Right;
            this.fcInitial_LaserMarking_Start.Size = new System.Drawing.Size(180, 42);
            this.fcInitial_LaserMarking_Start.SkipColor = System.Drawing.Color.Yellow;
            this.fcInitial_LaserMarking_Start.SubFlowChart = null;
            this.fcInitial_LaserMarking_Start.TabIndex = 4;
            this.fcInitial_LaserMarking_Start.Text = "Start";
            this.fcInitial_LaserMarking_Start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitial_LaserMarking_Start.TimeOut = 10000;
            this.fcInitial_LaserMarking_Start.TimeoutColor = System.Drawing.Color.Red;
            this.fcInitial_LaserMarking_Start.tmrTimeOut = null;
            this.fcInitial_LaserMarking_Start.FlowRun += new JabilSDK.FlowChart.FlowRunEvent(this.fcInitial_LaserMarking_Start_FlowRun);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.textBox2);
            this.panel8.Controls.Add(this.textBox1);
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.label16);
            this.panel8.Controls.Add(this.label1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(733, 133);
            this.panel8.TabIndex = 276;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.SettingData, "PSet.Port", true));
            this.textBox2.Location = new System.Drawing.Point(93, 78);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(143, 28);
            this.textBox2.TabIndex = 146;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.SettingData, "PSet.IP", true));
            this.textBox1.Location = new System.Drawing.Point(93, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(143, 28);
            this.textBox1.TabIndex = 146;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port:";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(731, 32);
            this.label16.TabIndex = 145;
            this.label16.Tag = "MenuColors";
            this.label16.Text = "Configuration";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dcPSet_IP
            // 
            this.dcPSet_IP.AllowDBNull = false;
            this.dcPSet_IP.ColumnName = "IP";
            this.dcPSet_IP.DefaultValue = "192.168.0.100";
            // 
            // dcPSet_Port
            // 
            this.dcPSet_Port.AllowDBNull = false;
            this.dcPSet_Port.ColumnName = "Port";
            this.dcPSet_Port.DataType = typeof(int);
            this.dcPSet_Port.DefaultValue = 5000;
            // 
            // dt_MarkingData
            // 
            this.dt_MarkingData.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcMarkingData_Id,
            this.dcMarkingData_BlockNumber,
            this.dcMarkingData_SerialNumberIndex});
            this.dt_MarkingData.TableName = "MarkingData";
            // 
            // dcMarkingData_Id
            // 
            this.dcMarkingData_Id.AllowDBNull = false;
            this.dcMarkingData_Id.ColumnName = "Id";
            this.dcMarkingData_Id.DataType = typeof(int);
            this.dcMarkingData_Id.DefaultValue = 1;
            // 
            // dcMarkingData_BlockNumber
            // 
            this.dcMarkingData_BlockNumber.AllowDBNull = false;
            this.dcMarkingData_BlockNumber.ColumnName = "BlockNumber";
            this.dcMarkingData_BlockNumber.DataType = typeof(int);
            this.dcMarkingData_BlockNumber.DefaultValue = 1;
            // 
            // dcMarkingData_SerialNumberIndex
            // 
            this.dcMarkingData_SerialNumberIndex.AllowDBNull = false;
            this.dcMarkingData_SerialNumberIndex.ColumnName = "SerialNumberIndex";
            this.dcMarkingData_SerialNumberIndex.DataType = typeof(int);
            this.dcMarkingData_SerialNumberIndex.DefaultValue = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.nudSerialQuantity);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(2, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(721, 115);
            this.panel3.TabIndex = 240;
            // 
            // nudSerialQuantity
            // 
            this.nudSerialQuantity.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.RecipeData, "RSet.SerialNumbersQuantity", true));
            this.nudSerialQuantity.Location = new System.Drawing.Point(240, 34);
            this.nudSerialQuantity.Name = "nudSerialQuantity";
            this.nudSerialQuantity.Size = new System.Drawing.Size(78, 28);
            this.nudSerialQuantity.TabIndex = 238;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(218, 24);
            this.label10.TabIndex = 237;
            this.label10.Text = "Serial Numbers Quantity:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(2, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(721, 33);
            this.label5.TabIndex = 239;
            this.label5.Text = "Datos de marcado";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvMarkingData
            // 
            this.dgvMarkingData.AutoGenerateColumns = false;
            this.dgvMarkingData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMarkingData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarkingData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.BlockNumber,
            this.SerialNumberIndex});
            this.dgvMarkingData.DataMember = "MarkingData";
            this.dgvMarkingData.DataSource = this.RecipeData;
            this.dgvMarkingData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMarkingData.Location = new System.Drawing.Point(2, 151);
            this.dgvMarkingData.MultiSelect = false;
            this.dgvMarkingData.Name = "dgvMarkingData";
            this.dgvMarkingData.RowHeadersWidth = 51;
            this.dgvMarkingData.RowTemplate.Height = 23;
            this.dgvMarkingData.Size = new System.Drawing.Size(721, 548);
            this.dgvMarkingData.TabIndex = 241;
            this.dgvMarkingData.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMarkingData_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BlockNumber
            // 
            this.BlockNumber.DataPropertyName = "BlockNumber";
            this.BlockNumber.HeaderText = "Block Number";
            this.BlockNumber.MinimumWidth = 6;
            this.BlockNumber.Name = "BlockNumber";
            this.BlockNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SerialNumberIndex
            // 
            this.SerialNumberIndex.DataPropertyName = "SerialNumberIndex";
            this.SerialNumberIndex.HeaderText = "Serial Number Index";
            this.SerialNumberIndex.MinimumWidth = 6;
            this.SerialNumberIndex.Name = "SerialNumberIndex";
            this.SerialNumberIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dcRSet_SerialNumbersQuantity
            // 
            this.dcRSet_SerialNumbersQuantity.AllowDBNull = false;
            this.dcRSet_SerialNumbersQuantity.ColumnName = "SerialNumbersQuantity";
            this.dcRSet_SerialNumbersQuantity.DataType = typeof(int);
            this.dcRSet_SerialNumbersQuantity.DefaultValue = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtAnswer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSendCommand);
            this.panel1.Controls.Add(this.cmbCommands);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 133);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 604);
            this.panel1.TabIndex = 277;
            // 
            // txtAnswer
            // 
            this.txtAnswer.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnswer.Location = new System.Drawing.Point(0, 221);
            this.txtAnswer.Margin = new System.Windows.Forms.Padding(0);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(731, 31);
            this.txtAnswer.TabIndex = 151;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 179);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(731, 42);
            this.label3.TabIndex = 150;
            this.label3.Tag = "MenuColors";
            this.label3.Text = "Response";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.BackColor = System.Drawing.Color.White;
            this.btnSendCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSendCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSendCommand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnSendCommand.Location = new System.Drawing.Point(0, 131);
            this.btnSendCommand.Margin = new System.Windows.Forms.Padding(0);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(731, 48);
            this.btnSendCommand.TabIndex = 149;
            this.btnSendCommand.Text = "Send Command";
            this.btnSendCommand.UseVisualStyleBackColor = false;
            this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
            // 
            // cmbCommands
            // 
            this.cmbCommands.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCommands.FormattingEnabled = true;
            this.cmbCommands.Items.AddRange(new object[] {
            "RX,Ready",
            "RX,ProgramNo"});
            this.cmbCommands.Location = new System.Drawing.Point(0, 98);
            this.cmbCommands.Margin = new System.Windows.Forms.Padding(0);
            this.cmbCommands.Name = "cmbCommands";
            this.cmbCommands.Size = new System.Drawing.Size(731, 33);
            this.cmbCommands.TabIndex = 148;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDisconnect);
            this.panel2.Controls.Add(this.btnConnect);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(731, 66);
            this.panel2.TabIndex = 147;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.Red;
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDisconnect.ForeColor = System.Drawing.Color.White;
            this.btnDisconnect.Location = new System.Drawing.Point(293, 0);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(0);
            this.btnDisconnect.MaximumSize = new System.Drawing.Size(375, 375);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(294, 66);
            this.btnDisconnect.TabIndex = 148;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.DarkGreen;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(0, 0);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(0);
            this.btnConnect.MaximumSize = new System.Drawing.Size(375, 375);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(293, 66);
            this.btnConnect.TabIndex = 146;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(731, 32);
            this.label4.TabIndex = 145;
            this.label4.Tag = "MenuColors";
            this.label4.Text = "Control Manual";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLaserMarkingHistory
            // 
            this.lbLaserMarkingHistory.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lbLaserMarkingHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLaserMarkingHistory.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLaserMarkingHistory.ForeColor = System.Drawing.SystemColors.Info;
            this.lbLaserMarkingHistory.FormattingEnabled = true;
            this.lbLaserMarkingHistory.ItemHeight = 16;
            this.lbLaserMarkingHistory.Location = new System.Drawing.Point(0, 32);
            this.lbLaserMarkingHistory.Name = "lbLaserMarkingHistory";
            this.lbLaserMarkingHistory.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbLaserMarkingHistory.Size = new System.Drawing.Size(727, 307);
            this.lbLaserMarkingHistory.TabIndex = 2;
            this.lbLaserMarkingHistory.Tag = "";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(727, 32);
            this.label7.TabIndex = 146;
            this.label7.Tag = "MenuColors";
            this.label7.Text = "Comunicación de Marcadora";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 737);
            this.tableLayoutPanel1.TabIndex = 148;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbLaserMarkingHistory);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(727, 339);
            this.panel4.TabIndex = 148;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel2);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 395);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(727, 339);
            this.panel5.TabIndex = 149;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lvScannedSerialNumbers, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lvGeneratedSerialNumbers, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(727, 307);
            this.tableLayoutPanel2.TabIndex = 151;
            // 
            // lvScannedSerialNumbers
            // 
            this.lvScannedSerialNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvScannedSerialNumbers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvScannedSerialNumbers.HideSelection = false;
            this.lvScannedSerialNumbers.Location = new System.Drawing.Point(366, 3);
            this.lvScannedSerialNumbers.Name = "lvScannedSerialNumbers";
            this.lvScannedSerialNumbers.Size = new System.Drawing.Size(358, 301);
            this.lvScannedSerialNumbers.TabIndex = 52;
            this.lvScannedSerialNumbers.UseCompatibleStateImageBehavior = false;
            this.lvScannedSerialNumbers.View = System.Windows.Forms.View.Details;
            // 
            // lvGeneratedSerialNumbers
            // 
            this.lvGeneratedSerialNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGeneratedSerialNumbers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvGeneratedSerialNumbers.HideSelection = false;
            this.lvGeneratedSerialNumbers.Location = new System.Drawing.Point(3, 3);
            this.lvGeneratedSerialNumbers.Name = "lvGeneratedSerialNumbers";
            this.lvGeneratedSerialNumbers.Size = new System.Drawing.Size(357, 301);
            this.lvGeneratedSerialNumbers.TabIndex = 51;
            this.lvGeneratedSerialNumbers.UseCompatibleStateImageBehavior = false;
            this.lvGeneratedSerialNumbers.View = System.Windows.Forms.View.Details;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(727, 32);
            this.label11.TabIndex = 150;
            this.label11.Tag = "MenuColors";
            this.label11.Text = "Marking Validation";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnClearHistory);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 348);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(727, 41);
            this.panel6.TabIndex = 150;
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Location = new System.Drawing.Point(3, 3);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(136, 37);
            this.btnClearHistory.TabIndex = 148;
            this.btnClearHistory.Text = "Clear History";
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(733, 737);
            this.tabControl2.TabIndex = 242;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMarkingData);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Size = new System.Drawing.Size(725, 702);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Marking Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.myCheckBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Size = new System.Drawing.Size(664, 367);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuraciones Generales";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // myCheckBox2
            // 
            this.myCheckBox2.Checked = false;
            this.myCheckBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.RecipeData, "RSet.ValidateMarking", true));
            this.myCheckBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.myCheckBox2.Location = new System.Drawing.Point(2, 3);
            this.myCheckBox2.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.myCheckBox2.MaximumSize = new System.Drawing.Size(0, 51);
            this.myCheckBox2.MinimumSize = new System.Drawing.Size(284, 51);
            this.myCheckBox2.Name = "myCheckBox2";
            this.myCheckBox2.Size = new System.Drawing.Size(660, 51);
            this.myCheckBox2.TabIndex = 241;
            this.myCheckBox2.Text = "Validate Marking";
            // 
            // dcRSet_ValidateMarking
            // 
            this.dcRSet_ValidateMarking.AllowDBNull = false;
            this.dcRSet_ValidateMarking.ColumnName = "ValidateMarking";
            this.dcRSet_ValidateMarking.DataType = typeof(bool);
            this.dcRSet_ValidateMarking.DefaultValue = true;
            // 
            // Enabled_Laser
            // 
            this.Enabled_Laser.ColumnName = "Enabled_Laser";
            this.Enabled_Laser.DataType = typeof(bool);
            // 
            // LaserMarkingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 780);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LaserMarkingForm";
            this.Text = "LaserMarking";
            this.plProductionSetting.ResumeLayout(false);
            this.plRecipeEditor.ResumeLayout(false);
            this.plFlowInitial.ResumeLayout(false);
            this.plMachineStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_MarkingData)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSerialQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarkingData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private JabilSDK.FlowChart fcInitial_LaserMarking_ResetErrors;
        private JabilSDK.FlowChart fcInitial_LaserMarking_End;
        private JabilSDK.FlowChart fcInitial_LaserMarking_Connect;
        private JabilSDK.FlowChart fcInitial_LaserMarking_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label16;
        private System.Data.DataColumn dcPSet_IP;
        private System.Data.DataColumn dcPSet_Port;
        private System.Data.DataTable dt_MarkingData;
        private System.Data.DataColumn dcMarkingData_Id;
        private System.Data.DataColumn dcMarkingData_BlockNumber;
        private System.Data.DataColumn dcMarkingData_SerialNumberIndex;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown nudSerialQuantity;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvMarkingData;
        private System.Data.DataColumn dcRSet_SerialNumbersQuantity;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        public System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSendCommand;
        private System.Windows.Forms.ComboBox cmbCommands;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lbLaserMarkingHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListView lvScannedSerialNumbers;
        private System.Windows.Forms.ListView lvGeneratedSerialNumbers;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private tstCkBox.MyCheckBox myCheckBox2;
        private System.Data.DataColumn dcRSet_ValidateMarking;
        private System.Data.DataColumn Enabled_Laser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BlockNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumberIndex;
    }
}
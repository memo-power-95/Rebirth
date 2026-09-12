namespace Alpha
{
    partial class UC_DownConveyorAutoFlow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            this.components = new System.ComponentModel.Container();
            this.flowChart65 = new NPSDK.NPFlowChart();
            this.flowChart64 = new NPSDK.NPFlowChart();
            this.flowChart67 = new NPSDK.NPFlowChart();
            this.flowChart70 = new NPSDK.NPFlowChart();
            this.flowChart77 = new NPSDK.NPFlowChart();
            this.flowChart71 = new NPSDK.NPFlowChart();
            this.flowChart48 = new NPSDK.NPFlowChart();
            this.flowChart66 = new NPSDK.NPFlowChart();
            this.flowChart78 = new NPSDK.NPFlowChart();
            this.flowChart62 = new NPSDK.NPFlowChart();
            this.DownConveyorStartFlow = new NPSDK.NPFlowChart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowChart65
            // 
            this.flowChart65.AlarmCode = "";
            this.flowChart65.ArrowSeparation = 20;
            this.flowChart65.ArrowsWidth = 1;
            this.flowChart65.BackColor = System.Drawing.Color.White;
            this.flowChart65.bClearTrace = false;
            this.flowChart65.bIsLastFlowChart = false;
            this.flowChart65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart65.bSkip = false;
            this.flowChart65.bStepByStepMode = false;
            this.flowChart65.CASE1 = this.flowChart64;
            this.flowChart65.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart65.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart65.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Top;
            this.flowChart65.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart65.CASE2 = null;
            this.flowChart65.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart65.CASE2Color = System.Drawing.Color.Red;
            this.flowChart65.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart65.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart65.CASE3 = null;
            this.flowChart65.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart65.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart65.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart65.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart65.DefaultColor = System.Drawing.Color.White;
            this.flowChart65.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart65.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart65.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart65.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart65.Location = new System.Drawing.Point(36, 73);
            this.flowChart65.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart65.menuOpening = false;
            this.flowChart65.Name = "flowChart65";
            this.flowChart65.NEXT = this.flowChart67;
            this.flowChart65.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart65.NEXTColor = System.Drawing.Color.Black;
            this.flowChart65.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart65.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart65.Size = new System.Drawing.Size(208, 53);
            this.flowChart65.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart65.SubFlowChart = null;
            this.flowChart65.TabIndex = 110;
            this.flowChart65.Text = "Is have product";
            this.flowChart65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart65.TimeOut = 10000;
            this.flowChart65.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart65.tmrTimeOut = null;
            this.flowChart65.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart65_FlowRun);
            // 
            // flowChart64
            // 
            this.flowChart64.AlarmCode = "";
            this.flowChart64.ArrowSeparation = 20;
            this.flowChart64.ArrowsWidth = 1;
            this.flowChart64.BackColor = System.Drawing.Color.White;
            this.flowChart64.bClearTrace = false;
            this.flowChart64.bIsLastFlowChart = false;
            this.flowChart64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart64.bSkip = false;
            this.flowChart64.bStepByStepMode = false;
            this.flowChart64.CASE1 = this.flowChart67;
            this.flowChart64.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart64.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart64.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart64.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart64.CASE2 = null;
            this.flowChart64.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart64.CASE2Color = System.Drawing.Color.Red;
            this.flowChart64.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart64.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart64.CASE3 = null;
            this.flowChart64.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart64.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart64.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart64.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart64.DefaultColor = System.Drawing.Color.White;
            this.flowChart64.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart64.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart64.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart64.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart64.Location = new System.Drawing.Point(348, 144);
            this.flowChart64.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart64.menuOpening = false;
            this.flowChart64.Name = "flowChart64";
            this.flowChart64.NEXT = this.flowChart66;
            this.flowChart64.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart64.NEXTColor = System.Drawing.Color.Black;
            this.flowChart64.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart64.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart64.Size = new System.Drawing.Size(208, 60);
            this.flowChart64.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart64.SubFlowChart = null;
            this.flowChart64.TabIndex = 104;
            this.flowChart64.Text = "Request a product from the front machine";
            this.flowChart64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart64.TimeOut = 10000;
            this.flowChart64.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart64.tmrTimeOut = null;
            this.flowChart64.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart64_FlowRun);
            // 
            // flowChart67
            // 
            this.flowChart67.AlarmCode = "";
            this.flowChart67.ArrowSeparation = 20;
            this.flowChart67.ArrowsWidth = 1;
            this.flowChart67.BackColor = System.Drawing.Color.White;
            this.flowChart67.bClearTrace = false;
            this.flowChart67.bIsLastFlowChart = false;
            this.flowChart67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart67.bSkip = false;
            this.flowChart67.bStepByStepMode = false;
            this.flowChart67.CASE1 = null;
            this.flowChart67.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart67.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart67.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart67.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart67.CASE2 = null;
            this.flowChart67.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart67.CASE2Color = System.Drawing.Color.Red;
            this.flowChart67.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart67.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart67.CASE3 = null;
            this.flowChart67.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart67.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart67.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart67.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart67.DefaultColor = System.Drawing.Color.White;
            this.flowChart67.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart67.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart67.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart67.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart67.Location = new System.Drawing.Point(36, 144);
            this.flowChart67.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart67.menuOpening = false;
            this.flowChart67.Name = "flowChart67";
            this.flowChart67.NEXT = this.flowChart70;
            this.flowChart67.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart67.NEXTColor = System.Drawing.Color.Black;
            this.flowChart67.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart67.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart67.Size = new System.Drawing.Size(208, 60);
            this.flowChart67.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart67.SubFlowChart = null;
            this.flowChart67.TabIndex = 103;
            this.flowChart67.Text = "Check if there is product in the lower conveyor";
            this.flowChart67.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart67.TimeOut = 10000;
            this.flowChart67.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart67.tmrTimeOut = null;
            this.flowChart67.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart67_FlowRun);
            // 
            // flowChart70
            // 
            this.flowChart70.AlarmCode = "";
            this.flowChart70.ArrowSeparation = 20;
            this.flowChart70.ArrowsWidth = 1;
            this.flowChart70.BackColor = System.Drawing.Color.White;
            this.flowChart70.bClearTrace = false;
            this.flowChart70.bIsLastFlowChart = false;
            this.flowChart70.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart70.bSkip = false;
            this.flowChart70.bStepByStepMode = false;
            this.flowChart70.CASE1 = this.flowChart64;
            this.flowChart70.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart70.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart70.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Left;
            this.flowChart70.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart70.CASE2 = null;
            this.flowChart70.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart70.CASE2Color = System.Drawing.Color.Red;
            this.flowChart70.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart70.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart70.CASE3 = null;
            this.flowChart70.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart70.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart70.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart70.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart70.DefaultColor = System.Drawing.Color.White;
            this.flowChart70.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart70.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart70.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart70.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart70.Location = new System.Drawing.Point(36, 231);
            this.flowChart70.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart70.menuOpening = false;
            this.flowChart70.Name = "flowChart70";
            this.flowChart70.NEXT = this.flowChart77;
            this.flowChart70.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart70.NEXTColor = System.Drawing.Color.Black;
            this.flowChart70.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart70.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart70.Size = new System.Drawing.Size(208, 83);
            this.flowChart70.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart70.SubFlowChart = null;
            this.flowChart70.TabIndex = 102;
            this.flowChart70.Text = "Wait for the back machine to request the product to flow out";
            this.flowChart70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart70.TimeOut = 10000;
            this.flowChart70.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart70.tmrTimeOut = null;
            this.flowChart70.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart70_FlowRun);
            // 
            // flowChart77
            // 
            this.flowChart77.AlarmCode = "";
            this.flowChart77.ArrowSeparation = 20;
            this.flowChart77.ArrowsWidth = 1;
            this.flowChart77.BackColor = System.Drawing.Color.White;
            this.flowChart77.bClearTrace = false;
            this.flowChart77.bIsLastFlowChart = false;
            this.flowChart77.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart77.bSkip = false;
            this.flowChart77.bStepByStepMode = false;
            this.flowChart77.CASE1 = null;
            this.flowChart77.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart77.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart77.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart77.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart77.CASE2 = null;
            this.flowChart77.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart77.CASE2Color = System.Drawing.Color.Red;
            this.flowChart77.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart77.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart77.CASE3 = null;
            this.flowChart77.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart77.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart77.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart77.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart77.DefaultColor = System.Drawing.Color.White;
            this.flowChart77.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart77.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart77.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart77.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart77.Location = new System.Drawing.Point(36, 332);
            this.flowChart77.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart77.menuOpening = false;
            this.flowChart77.Name = "flowChart77";
            this.flowChart77.NEXT = this.flowChart71;
            this.flowChart77.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart77.NEXTColor = System.Drawing.Color.Black;
            this.flowChart77.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart77.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart77.Size = new System.Drawing.Size(208, 52);
            this.flowChart77.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart77.SubFlowChart = null;
            this.flowChart77.TabIndex = 106;
            this.flowChart77.Text = "The product flows out to the outlet";
            this.flowChart77.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart77.TimeOut = 10000;
            this.flowChart77.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart77.tmrTimeOut = null;
            this.flowChart77.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart77_FlowRun);
            // 
            // flowChart71
            // 
            this.flowChart71.AlarmCode = "";
            this.flowChart71.ArrowSeparation = 20;
            this.flowChart71.ArrowsWidth = 1;
            this.flowChart71.BackColor = System.Drawing.Color.White;
            this.flowChart71.bClearTrace = false;
            this.flowChart71.bIsLastFlowChart = false;
            this.flowChart71.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart71.bSkip = false;
            this.flowChart71.bStepByStepMode = false;
            this.flowChart71.CASE1 = null;
            this.flowChart71.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart71.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart71.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart71.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart71.CASE2 = null;
            this.flowChart71.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart71.CASE2Color = System.Drawing.Color.Red;
            this.flowChart71.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart71.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart71.CASE3 = null;
            this.flowChart71.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart71.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart71.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart71.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart71.DefaultColor = System.Drawing.Color.White;
            this.flowChart71.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart71.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart71.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart71.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart71.Location = new System.Drawing.Point(36, 433);
            this.flowChart71.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart71.menuOpening = false;
            this.flowChart71.Name = "flowChart71";
            this.flowChart71.NEXT = this.flowChart48;
            this.flowChart71.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart71.NEXTColor = System.Drawing.Color.Black;
            this.flowChart71.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart71.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart71.Size = new System.Drawing.Size(208, 48);
            this.flowChart71.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart71.SubFlowChart = null;
            this.flowChart71.TabIndex = 105;
            this.flowChart71.Text = "Wait for the product outflow to complete";
            this.flowChart71.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart71.TimeOut = 10000;
            this.flowChart71.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart71.tmrTimeOut = null;
            this.flowChart71.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart71_FlowRun);
            // 
            // flowChart48
            // 
            this.flowChart48.AlarmCode = "";
            this.flowChart48.ArrowSeparation = 20;
            this.flowChart48.ArrowsWidth = 1;
            this.flowChart48.BackColor = System.Drawing.Color.White;
            this.flowChart48.bClearTrace = false;
            this.flowChart48.bIsLastFlowChart = false;
            this.flowChart48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart48.bSkip = false;
            this.flowChart48.bStepByStepMode = false;
            this.flowChart48.CASE1 = null;
            this.flowChart48.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart48.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart48.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart48.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart48.CASE2 = null;
            this.flowChart48.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart48.CASE2Color = System.Drawing.Color.Red;
            this.flowChart48.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart48.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart48.CASE3 = null;
            this.flowChart48.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart48.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart48.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart48.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart48.DefaultColor = System.Drawing.Color.White;
            this.flowChart48.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart48.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart48.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart48.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart48.Location = new System.Drawing.Point(36, 519);
            this.flowChart48.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart48.menuOpening = false;
            this.flowChart48.Name = "flowChart48";
            this.flowChart48.NEXT = this.flowChart64;
            this.flowChart48.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Square;
            this.flowChart48.NEXTColor = System.Drawing.Color.Black;
            this.flowChart48.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart48.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Bottom;
            this.flowChart48.Size = new System.Drawing.Size(208, 48);
            this.flowChart48.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart48.SubFlowChart = null;
            this.flowChart48.TabIndex = 109;
            this.flowChart48.Text = "Delay stop motor forward";
            this.flowChart48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart48.TimeOut = 10000;
            this.flowChart48.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart48.tmrTimeOut = null;
            this.flowChart48.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart48_FlowRun);
            // 
            // flowChart66
            // 
            this.flowChart66.AlarmCode = "";
            this.flowChart66.ArrowSeparation = 20;
            this.flowChart66.ArrowsWidth = 1;
            this.flowChart66.BackColor = System.Drawing.Color.White;
            this.flowChart66.bClearTrace = false;
            this.flowChart66.bIsLastFlowChart = false;
            this.flowChart66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart66.bSkip = false;
            this.flowChart66.bStepByStepMode = false;
            this.flowChart66.CASE1 = this.flowChart67;
            this.flowChart66.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart66.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart66.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart66.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Left;
            this.flowChart66.CASE2 = null;
            this.flowChart66.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart66.CASE2Color = System.Drawing.Color.Red;
            this.flowChart66.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart66.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart66.CASE3 = null;
            this.flowChart66.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart66.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart66.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart66.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart66.DefaultColor = System.Drawing.Color.White;
            this.flowChart66.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart66.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart66.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart66.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart66.Location = new System.Drawing.Point(348, 242);
            this.flowChart66.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart66.menuOpening = false;
            this.flowChart66.Name = "flowChart66";
            this.flowChart66.NEXT = this.flowChart78;
            this.flowChart66.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart66.NEXTColor = System.Drawing.Color.Black;
            this.flowChart66.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart66.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart66.Size = new System.Drawing.Size(208, 60);
            this.flowChart66.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart66.SubFlowChart = null;
            this.flowChart66.TabIndex = 101;
            this.flowChart66.Text = "Wait for the product to reach the feed port";
            this.flowChart66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart66.TimeOut = 10000;
            this.flowChart66.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart66.tmrTimeOut = null;
            this.flowChart66.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart66_FlowRun);
            // 
            // flowChart78
            // 
            this.flowChart78.AlarmCode = "";
            this.flowChart78.ArrowSeparation = 20;
            this.flowChart78.ArrowsWidth = 1;
            this.flowChart78.BackColor = System.Drawing.Color.White;
            this.flowChart78.bClearTrace = false;
            this.flowChart78.bIsLastFlowChart = false;
            this.flowChart78.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart78.bSkip = false;
            this.flowChart78.bStepByStepMode = false;
            this.flowChart78.CASE1 = this.flowChart67;
            this.flowChart78.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart78.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart78.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart78.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Left;
            this.flowChart78.CASE2 = null;
            this.flowChart78.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart78.CASE2Color = System.Drawing.Color.Red;
            this.flowChart78.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart78.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart78.CASE3 = null;
            this.flowChart78.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart78.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart78.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart78.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart78.DefaultColor = System.Drawing.Color.White;
            this.flowChart78.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart78.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart78.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart78.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart78.Location = new System.Drawing.Point(348, 332);
            this.flowChart78.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart78.menuOpening = false;
            this.flowChart78.Name = "flowChart78";
            this.flowChart78.NEXT = this.flowChart62;
            this.flowChart78.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart78.NEXTColor = System.Drawing.Color.Black;
            this.flowChart78.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart78.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart78.Size = new System.Drawing.Size(208, 52);
            this.flowChart78.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart78.SubFlowChart = null;
            this.flowChart78.TabIndex = 107;
            this.flowChart78.Text = "Feed";
            this.flowChart78.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart78.TimeOut = 10000;
            this.flowChart78.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart78.tmrTimeOut = null;
            this.flowChart78.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart78_FlowRun);
            // 
            // flowChart62
            // 
            this.flowChart62.AlarmCode = "";
            this.flowChart62.ArrowSeparation = 20;
            this.flowChart62.ArrowsWidth = 1;
            this.flowChart62.BackColor = System.Drawing.Color.White;
            this.flowChart62.bClearTrace = false;
            this.flowChart62.bIsLastFlowChart = false;
            this.flowChart62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart62.bSkip = false;
            this.flowChart62.bStepByStepMode = false;
            this.flowChart62.CASE1 = this.flowChart64;
            this.flowChart62.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Square;
            this.flowChart62.CASE1Color = System.Drawing.Color.LimeGreen;
            this.flowChart62.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart62.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart62.CASE2 = null;
            this.flowChart62.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart62.CASE2Color = System.Drawing.Color.Red;
            this.flowChart62.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart62.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart62.CASE3 = null;
            this.flowChart62.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart62.CASE3Color = System.Drawing.Color.Blue;
            this.flowChart62.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart62.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart62.DefaultColor = System.Drawing.Color.White;
            this.flowChart62.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.flowChart62.ExecutedColor = System.Drawing.Color.LightGray;
            this.flowChart62.ExecutingColor = System.Drawing.Color.Lime;
            this.flowChart62.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowChart62.Location = new System.Drawing.Point(348, 433);
            this.flowChart62.Margin = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.flowChart62.menuOpening = false;
            this.flowChart62.Name = "flowChart62";
            this.flowChart62.NEXT = this.flowChart67;
            this.flowChart62.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.flowChart62.NEXTColor = System.Drawing.Color.Black;
            this.flowChart62.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.flowChart62.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Left;
            this.flowChart62.Size = new System.Drawing.Size(208, 48);
            this.flowChart62.SkipColor = System.Drawing.Color.Yellow;
            this.flowChart62.SubFlowChart = null;
            this.flowChart62.TabIndex = 108;
            this.flowChart62.Text = "Wait for the feed to complete";
            this.flowChart62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart62.TimeOut = 10000;
            this.flowChart62.TimeoutColor = System.Drawing.Color.Red;
            this.flowChart62.tmrTimeOut = null;
            this.flowChart62.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.flowChart62_FlowRun);
            // 
            // DownConveyorStartFlow
            // 
            this.DownConveyorStartFlow.AlarmCode = "";
            this.DownConveyorStartFlow.ArrowSeparation = 20;
            this.DownConveyorStartFlow.ArrowsWidth = 1;
            this.DownConveyorStartFlow.BackColor = System.Drawing.Color.White;
            this.DownConveyorStartFlow.bClearTrace = false;
            this.DownConveyorStartFlow.bIsLastFlowChart = false;
            this.DownConveyorStartFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DownConveyorStartFlow.bSkip = false;
            this.DownConveyorStartFlow.bStepByStepMode = false;
            this.DownConveyorStartFlow.CASE1 = null;
            this.DownConveyorStartFlow.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.DownConveyorStartFlow.CASE1Color = System.Drawing.Color.LimeGreen;
            this.DownConveyorStartFlow.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.DownConveyorStartFlow.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.DownConveyorStartFlow.CASE2 = null;
            this.DownConveyorStartFlow.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.DownConveyorStartFlow.CASE2Color = System.Drawing.Color.Red;
            this.DownConveyorStartFlow.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.DownConveyorStartFlow.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.DownConveyorStartFlow.CASE3 = null;
            this.DownConveyorStartFlow.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.DownConveyorStartFlow.CASE3Color = System.Drawing.Color.Blue;
            this.DownConveyorStartFlow.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.DownConveyorStartFlow.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.DownConveyorStartFlow.DefaultColor = System.Drawing.Color.White;
            this.DownConveyorStartFlow.eSkipPath =  NPSDK.FCResultType.NEXT;
            this.DownConveyorStartFlow.ExecutedColor = System.Drawing.Color.LightGray;
            this.DownConveyorStartFlow.ExecutingColor = System.Drawing.Color.Lime;
            this.DownConveyorStartFlow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownConveyorStartFlow.Location = new System.Drawing.Point(36, 13);
            this.DownConveyorStartFlow.Margin = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.DownConveyorStartFlow.menuOpening = false;
            this.DownConveyorStartFlow.Name = "DownConveyorStartFlow";
            this.DownConveyorStartFlow.NEXT = this.flowChart65;
            this.DownConveyorStartFlow.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.DownConveyorStartFlow.NEXTColor = System.Drawing.Color.Black;
            this.DownConveyorStartFlow.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.DownConveyorStartFlow.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.DownConveyorStartFlow.Size = new System.Drawing.Size(208, 52);
            this.DownConveyorStartFlow.SkipColor = System.Drawing.Color.Yellow;
            this.DownConveyorStartFlow.SubFlowChart = null;
            this.DownConveyorStartFlow.TabIndex = 100;
            this.DownConveyorStartFlow.Text = "Bottom Conveyor Start";
            this.DownConveyorStartFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DownConveyorStartFlow.TimeOut = 10000;
            this.DownConveyorStartFlow.TimeoutColor = System.Drawing.Color.Red;
            this.DownConveyorStartFlow.tmrTimeOut = null;
            this.DownConveyorStartFlow.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.BottomConveyorStartFlow_FlowRun);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(638, 664);
            this.tabControl1.TabIndex = 111;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.DownConveyorStartFlow);
            this.tabPage1.Controls.Add(this.flowChart65);
            this.tabPage1.Controls.Add(this.flowChart66);
            this.tabPage1.Controls.Add(this.flowChart48);
            this.tabPage1.Controls.Add(this.flowChart70);
            this.tabPage1.Controls.Add(this.flowChart62);
            this.tabPage1.Controls.Add(this.flowChart67);
            this.tabPage1.Controls.Add(this.flowChart78);
            this.tabPage1.Controls.Add(this.flowChart64);
            this.tabPage1.Controls.Add(this.flowChart77);
            this.tabPage1.Controls.Add(this.flowChart71);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(630, 638);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // UC_DownConveyorAutoFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "UC_DownConveyorAutoFlow";
            this.Size = new System.Drawing.Size(638, 664);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NPSDK.NPFlowChart flowChart65;
        private NPSDK.NPFlowChart flowChart64;
        private NPSDK.NPFlowChart flowChart67;
        private NPSDK.NPFlowChart flowChart70;
        private NPSDK.NPFlowChart flowChart77;
        private NPSDK.NPFlowChart flowChart71;
        private NPSDK.NPFlowChart flowChart48;
        private NPSDK.NPFlowChart flowChart66;
        private NPSDK.NPFlowChart flowChart78;
        private NPSDK.NPFlowChart flowChart62;
        public NPSDK.NPFlowChart DownConveyorStartFlow;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
    }
}

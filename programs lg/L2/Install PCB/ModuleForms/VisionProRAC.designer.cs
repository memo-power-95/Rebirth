namespace Alpha.ModuleForms
{
    partial class VisionProRAC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisionProRAC));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.EnableVision = new System.Data.DataColumn();
            this.Ctrl_CamLiveCaptureWorker = new System.ComponentModel.BackgroundWorker();
            this.RVisionDataTable = new System.Data.DataTable();
            this.PName = new System.Data.DataColumn();
            this.PxXmmX = new System.Data.DataColumn();
            this.PxXmmY = new System.Data.DataColumn();
            this.TeachPxX = new System.Data.DataColumn();
            this.TeachPxY = new System.Data.DataColumn();
            this.TeachPxU = new System.Data.DataColumn();
            this.OffsetmmX = new System.Data.DataColumn();
            this.OffsetmmY = new System.Data.DataColumn();
            this.OffsetmmU = new System.Data.DataColumn();
            this.TotalmmX = new System.Data.DataColumn();
            this.TotalmmY = new System.Data.DataColumn();
            this.TotalmmU = new System.Data.DataColumn();
            this.ResultBool = new System.Data.DataColumn();
            this.ResultDouble = new System.Data.DataColumn();
            this.VPPName = new System.Data.DataColumn();
            this.ResultString = new System.Data.DataColumn();
            this.c_VisionEnabled = new System.Data.DataColumn();
            this.Image = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_RecipeEditorImage = new System.Windows.Forms.Panel();
            this.panel_Image = new System.Windows.Forms.Panel();
            this.panel33 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label_ProductionVPPName = new System.Windows.Forms.Label();
            this.lb_VPPName = new System.Windows.Forms.Label();
            this.cmbCamera = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReloadTool = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnLiveImage = new System.Windows.Forms.Button();
            this.btnGrabImage = new System.Windows.Forms.Button();
            this.btnEditVPRO = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.label_Recipe_Light = new System.Windows.Forms.Label();
            this.cbLights = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Recipe_Program = new System.Windows.Forms.Label();
            this.cbSelectProcess = new System.Windows.Forms.ComboBox();
            this.btnTeachSelectedProgram = new System.Windows.Forms.Button();
            this.btnExVision = new System.Windows.Forms.Button();
            this.panel47 = new System.Windows.Forms.Panel();
            this.lbInspectionX = new System.Windows.Forms.Label();
            this.label_Recipe_PixelsX = new System.Windows.Forms.Label();
            this.panel32 = new System.Windows.Forms.Panel();
            this.lbInspectionY = new System.Windows.Forms.Label();
            this.label_Recipe_PixelsY = new System.Windows.Forms.Label();
            this.panel25 = new System.Windows.Forms.Panel();
            this.lbInspectionU = new System.Windows.Forms.Label();
            this.label_Recipe_DegU = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dgvPosInspection = new System.Windows.Forms.DataGridView();
            this.pNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vPPNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.totalmmXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalmmYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalmmUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultBoolDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.resultDoubleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pxXmmXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pxXmmYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teachPxXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teachPxYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teachPxUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetmmXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetmmYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetmmUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl_Recipe = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labelImagePath = new System.Windows.Forms.Label();
            this.textLocalPath = new System.Windows.Forms.TextBox();
            this.buttonLocalPath = new System.Windows.Forms.Button();
            this.PCameraSettings = new System.Data.DataTable();
            this.CameraVPPName = new System.Data.DataColumn();
            this.ImageToolVPPName = new System.Data.DataColumn();
            this.tTip = new System.Windows.Forms.ToolTip(this.components);
            this.ImageSavePath = new System.Data.DataColumn();
            this.tabControl_ProductionSettings = new System.Windows.Forms.TabControl();
            this.tabPage_VPROSettings = new System.Windows.Forms.TabPage();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.tabPage_CameraRegister = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView_CameraSettings = new System.Windows.Forms.DataGridView();
            this.cameraVPPNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageToolVPPNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_StaticPrograms = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel_StaticPrograms = new System.Windows.Forms.TableLayoutPanel();
            this.panel_ProductionSettingImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label_Production_Light = new System.Windows.Forms.Label();
            this.comboBox_Production_Light = new System.Windows.Forms.ComboBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label_Production_Program = new System.Windows.Forms.Label();
            this.comboBox_Production_Program = new System.Windows.Forms.ComboBox();
            this.button_Production_RunVision = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label_Production_ResultPixelsX = new System.Windows.Forms.Label();
            this.label_Production_PixelsX = new System.Windows.Forms.Label();
            this.button_Production_TeachSelected = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label_Production_ResultPixelsY = new System.Windows.Forms.Label();
            this.label_Production_PixelsY = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label_Production_ResultDegU = new System.Windows.Forms.Label();
            this.label_Production_DegU = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridView_VisionData = new System.Windows.Forms.DataGridView();
            this.pSNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSVPPNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSTotalmmXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSTotalmmYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSTotalmmUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSResultBoolDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pSResultDoubleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSResultStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSPxXmmXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSPxXmmYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSTeachPxXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSTeachPxYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSTeachPxUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSOffsetmmXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSOffsetmmYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pSOffsetmmUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_CameraTeach = new System.Windows.Forms.TabPage();
            this.cogAcqFifoEditV21 = new Cognex.VisionPro.CogAcqFifoEditV2();
            this.tabPage_ImageSetup = new System.Windows.Forms.TabPage();
            this.cogIPOneImageEditV21 = new Cognex.VisionPro.ImageProcessing.CogIPOneImageEditV2();
            this.PVisionDataTable = new System.Data.DataTable();
            this.PSName = new System.Data.DataColumn();
            this.PSPxXmmX = new System.Data.DataColumn();
            this.PSPxXmmY = new System.Data.DataColumn();
            this.PSTeachPxX = new System.Data.DataColumn();
            this.PSTeachPxY = new System.Data.DataColumn();
            this.PSTeachPxU = new System.Data.DataColumn();
            this.PSOffsetmmX = new System.Data.DataColumn();
            this.PSOffsetmmY = new System.Data.DataColumn();
            this.PSOffsetmmU = new System.Data.DataColumn();
            this.PSTotalmmX = new System.Data.DataColumn();
            this.PSTotalmmY = new System.Data.DataColumn();
            this.PSTotalmmU = new System.Data.DataColumn();
            this.PSResultBool = new System.Data.DataColumn();
            this.PSResultDouble = new System.Data.DataColumn();
            this.PSVPPName = new System.Data.DataColumn();
            this.PSResultString = new System.Data.DataColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.fc_In_VisionConnectionFinish = new NPSDK.NPFlowChart();
            this.fc_In_VisionConnectionOk = new NPSDK.NPFlowChart();
            this.fc_In_VisionConnectionConnect = new NPSDK.NPFlowChart();
            this.fc_In_VisionConnectionStart = new NPSDK.NPFlowChart();
            this.Calib_Timer = new System.Windows.Forms.Timer(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.EnableVisionGantry = new System.Data.DataColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.btnUPLightWhiteOff = new System.Windows.Forms.Button();
            this.btnUPLightWhiteOn = new System.Windows.Forms.Button();
            this.btnUPLightBlueOn = new System.Windows.Forms.Button();
            this.btnUPLightGreenOn = new System.Windows.Forms.Button();
            this.btnUPLightRedOn = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.btnDWLightWhiteOff = new System.Windows.Forms.Button();
            this.btnDWLightWhiteOn = new System.Windows.Forms.Button();
            this.btnDWLightBlueOn = new System.Windows.Forms.Button();
            this.btnDWLightGreenOn = new System.Windows.Forms.Button();
            this.btnDWLightRedOn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.plMaintenance.SuspendLayout();
            this.plProductionSetting.SuspendLayout();
            this.plRecipeEditor.SuspendLayout();
            this.plFlowInitial.SuspendLayout();
            this.plMotionSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RVisionDataTable)).BeginInit();
            this.Image.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_RecipeEditorImage.SuspendLayout();
            this.panel_Image.SuspendLayout();
            this.panel33.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel47.SuspendLayout();
            this.panel32.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPosInspection)).BeginInit();
            this.tabControl_Recipe.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCameraSettings)).BeginInit();
            this.tabControl_ProductionSettings.SuspendLayout();
            this.tabPage_VPROSettings.SuspendLayout();
            this.tabPage_CameraRegister.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CameraSettings)).BeginInit();
            this.tabPage_StaticPrograms.SuspendLayout();
            this.tableLayoutPanel_StaticPrograms.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_VisionData)).BeginInit();
            this.tabPage_CameraTeach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogAcqFifoEditV21)).BeginInit();
            this.tabPage_ImageSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogIPOneImageEditV21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PVisionDataTable)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel18.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            // 
            // plMaintenance
            // 
            resources.ApplyResources(this.plMaintenance, "plMaintenance");
            this.plMaintenance.Controls.Add(this.panel17);
            this.plMaintenance.Controls.Add(this.panel8);
            // 
            // plProductionSetting
            // 
            resources.ApplyResources(this.plProductionSetting, "plProductionSetting");
            this.plProductionSetting.Controls.Add(this.panel6);
            // 
            // plRecipeEditor
            // 
            resources.ApplyResources(this.plRecipeEditor, "plRecipeEditor");
            this.plRecipeEditor.Controls.Add(this.tabControl_Recipe);
            // 
            // plFlowInitial
            // 
            resources.ApplyResources(this.plFlowInitial, "plFlowInitial");
            this.plFlowInitial.Controls.Add(this.label7);
            this.plFlowInitial.Controls.Add(this.fc_In_VisionConnectionFinish);
            this.plFlowInitial.Controls.Add(this.fc_In_VisionConnectionOk);
            this.plFlowInitial.Controls.Add(this.fc_In_VisionConnectionConnect);
            this.plFlowInitial.Controls.Add(this.fc_In_VisionConnectionStart);
            // 
            // plFlowAuto
            // 
            resources.ApplyResources(this.plFlowAuto, "plFlowAuto");
            // 
            // plMachineStatus
            // 
            resources.ApplyResources(this.plMachineStatus, "plMachineStatus");
            // 
            // plMotionSetup
            // 
            resources.ApplyResources(this.plMotionSetup, "plMotionSetup");
            this.plMotionSetup.Controls.Add(this.panel2);
            this.plMotionSetup.Controls.Add(this.panel3);
            // 
            // plMotorControl
            // 
            resources.ApplyResources(this.plMotorControl, "plMotorControl");
            // 
            // SettingData
            // 
            this.SettingData.Namespace = "VisionProRAC";
            this.SettingData.Tables.AddRange(new System.Data.DataTable[] {
            this.PCameraSettings,
            this.PVisionDataTable});
            // 
            // MSet
            // 
            this.MSet.Namespace = "VisionProRAC";
            // 
            // PSet
            // 
            this.PSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.EnableVision,
            this.ImageSavePath,
            this.EnableVisionGantry});
            this.PSet.Namespace = "VisionProRAC";
            // 
            // RecipeData
            // 
            this.RecipeData.Tables.AddRange(new System.Data.DataTable[] {
            this.RVisionDataTable});
            // 
            // RSet
            // 
            this.RSet.Namespace = "VisionProRAC";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // EnableVision
            // 
            this.EnableVision.AllowDBNull = false;
            this.EnableVision.ColumnName = "EnableVision";
            this.EnableVision.DataType = typeof(bool);
            this.EnableVision.DefaultValue = true;
            // 
            // Ctrl_CamLiveCaptureWorker
            // 
            this.Ctrl_CamLiveCaptureWorker.WorkerSupportsCancellation = true;
            this.Ctrl_CamLiveCaptureWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Ctrl_CamLiveCaptureWorker_DoWork);
            // 
            // RVisionDataTable
            // 
            this.RVisionDataTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.PName,
            this.PxXmmX,
            this.PxXmmY,
            this.TeachPxX,
            this.TeachPxY,
            this.TeachPxU,
            this.OffsetmmX,
            this.OffsetmmY,
            this.OffsetmmU,
            this.TotalmmX,
            this.TotalmmY,
            this.TotalmmU,
            this.ResultBool,
            this.ResultDouble,
            this.VPPName,
            this.ResultString,
            this.c_VisionEnabled});
            this.RVisionDataTable.Namespace = "VisionProRAC";
            this.RVisionDataTable.TableName = "RVisionDataTable";
            // 
            // PName
            // 
            this.PName.AllowDBNull = false;
            this.PName.Caption = "PName";
            this.PName.ColumnName = "PName";
            this.PName.DefaultValue = "";
            // 
            // PxXmmX
            // 
            this.PxXmmX.AllowDBNull = false;
            this.PxXmmX.ColumnName = "PxXmmX";
            this.PxXmmX.DataType = typeof(double);
            this.PxXmmX.DefaultValue = 0D;
            // 
            // PxXmmY
            // 
            this.PxXmmY.AllowDBNull = false;
            this.PxXmmY.ColumnName = "PxXmmY";
            this.PxXmmY.DataType = typeof(double);
            this.PxXmmY.DefaultValue = 0D;
            // 
            // TeachPxX
            // 
            this.TeachPxX.AllowDBNull = false;
            this.TeachPxX.ColumnName = "TeachPxX";
            this.TeachPxX.DataType = typeof(double);
            this.TeachPxX.DefaultValue = 0D;
            // 
            // TeachPxY
            // 
            this.TeachPxY.AllowDBNull = false;
            this.TeachPxY.ColumnName = "TeachPxY";
            this.TeachPxY.DataType = typeof(double);
            this.TeachPxY.DefaultValue = 0D;
            // 
            // TeachPxU
            // 
            this.TeachPxU.AllowDBNull = false;
            this.TeachPxU.ColumnName = "TeachPxU";
            this.TeachPxU.DataType = typeof(double);
            this.TeachPxU.DefaultValue = 0D;
            // 
            // OffsetmmX
            // 
            this.OffsetmmX.AllowDBNull = false;
            this.OffsetmmX.ColumnName = "OffsetmmX";
            this.OffsetmmX.DataType = typeof(double);
            this.OffsetmmX.DefaultValue = 0D;
            // 
            // OffsetmmY
            // 
            this.OffsetmmY.AllowDBNull = false;
            this.OffsetmmY.ColumnName = "OffsetmmY";
            this.OffsetmmY.DataType = typeof(double);
            this.OffsetmmY.DefaultValue = 0D;
            // 
            // OffsetmmU
            // 
            this.OffsetmmU.AllowDBNull = false;
            this.OffsetmmU.ColumnName = "OffsetmmU";
            this.OffsetmmU.DataType = typeof(double);
            this.OffsetmmU.DefaultValue = 0D;
            // 
            // TotalmmX
            // 
            this.TotalmmX.AllowDBNull = false;
            this.TotalmmX.ColumnName = "TotalmmX";
            this.TotalmmX.DataType = typeof(double);
            this.TotalmmX.DefaultValue = 0D;
            // 
            // TotalmmY
            // 
            this.TotalmmY.AllowDBNull = false;
            this.TotalmmY.ColumnName = "TotalmmY";
            this.TotalmmY.DataType = typeof(double);
            this.TotalmmY.DefaultValue = 0D;
            // 
            // TotalmmU
            // 
            this.TotalmmU.AllowDBNull = false;
            this.TotalmmU.ColumnName = "TotalmmU";
            this.TotalmmU.DataType = typeof(double);
            this.TotalmmU.DefaultValue = 0D;
            // 
            // ResultBool
            // 
            this.ResultBool.AllowDBNull = false;
            this.ResultBool.ColumnName = "ResultBool";
            this.ResultBool.DataType = typeof(bool);
            this.ResultBool.DefaultValue = false;
            // 
            // ResultDouble
            // 
            this.ResultDouble.AllowDBNull = false;
            this.ResultDouble.ColumnName = "ResultDouble";
            this.ResultDouble.DataType = typeof(double);
            this.ResultDouble.DefaultValue = 0D;
            // 
            // VPPName
            // 
            this.VPPName.ColumnName = "VPPName";
            this.VPPName.DefaultValue = "";
            // 
            // ResultString
            // 
            this.ResultString.AllowDBNull = false;
            this.ResultString.ColumnName = "ResultString";
            this.ResultString.DefaultValue = "";
            // 
            // c_VisionEnabled
            // 
            this.c_VisionEnabled.ColumnName = "c_VisionEnabled";
            this.c_VisionEnabled.DataType = typeof(bool);
            // 
            // Image
            // 
            resources.ApplyResources(this.Image, "Image");
            this.Image.BackColor = System.Drawing.Color.White;
            this.Image.Controls.Add(this.tableLayoutPanel1);
            this.Image.Name = "Image";
            this.Image.Enter += new System.EventHandler(this.Image_Enter);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel_RecipeEditorImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel_RecipeEditorImage
            // 
            this.panel_RecipeEditorImage.Controls.Add(this.panel_Image);
            resources.ApplyResources(this.panel_RecipeEditorImage, "panel_RecipeEditorImage");
            this.panel_RecipeEditorImage.Name = "panel_RecipeEditorImage";
            // 
            // panel_Image
            // 
            this.panel_Image.Controls.Add(this.panel33);
            this.panel_Image.Controls.Add(this.tableLayoutPanel3);
            resources.ApplyResources(this.panel_Image, "panel_Image");
            this.panel_Image.Name = "panel_Image";
            // 
            // panel33
            // 
            this.panel33.Controls.Add(this.tableLayoutPanel2);
            this.panel33.Controls.Add(this.cmbCamera);
            resources.ApplyResources(this.panel33, "panel33");
            this.panel33.Name = "panel33";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label_ProductionVPPName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lb_VPPName, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label_ProductionVPPName
            // 
            this.label_ProductionVPPName.BackColor = System.Drawing.Color.Red;
            this.label_ProductionVPPName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label_ProductionVPPName, "label_ProductionVPPName");
            this.label_ProductionVPPName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label_ProductionVPPName.Name = "label_ProductionVPPName";
            // 
            // lb_VPPName
            // 
            this.lb_VPPName.BackColor = System.Drawing.Color.Red;
            this.lb_VPPName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lb_VPPName, "lb_VPPName");
            this.lb_VPPName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lb_VPPName.Name = "lb_VPPName";
            this.lb_VPPName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lb_VPPName_MouseDoubleClick);
            // 
            // cmbCamera
            // 
            resources.ApplyResources(this.cmbCamera, "cmbCamera");
            this.cmbCamera.FormattingEnabled = true;
            this.cmbCamera.Items.AddRange(new object[] {
            resources.GetString("cmbCamera.Items")});
            this.cmbCamera.Name = "cmbCamera";
            this.cmbCamera.SelectedIndexChanged += new System.EventHandler(this.comboBox_Camera_SelectedIndexChanged);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btnReloadTool, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.panel9, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnGrabImage, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.btnEditVPRO, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btnReloadTool
            // 
            this.btnReloadTool.BackColor = System.Drawing.Color.White;
            this.btnReloadTool.BackgroundImage = global::Alpha.Properties.Resources.btnAxisUJogN_BackgroundImage;
            resources.ApplyResources(this.btnReloadTool, "btnReloadTool");
            this.btnReloadTool.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnReloadTool.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnReloadTool.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadTool.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnReloadTool.Name = "btnReloadTool";
            this.tTip.SetToolTip(this.btnReloadTool, resources.GetString("btnReloadTool.ToolTip"));
            this.btnReloadTool.UseVisualStyleBackColor = false;
            this.btnReloadTool.Click += new System.EventHandler(this.btnReloadTool_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnLiveImage);
            resources.ApplyResources(this.panel9, "panel9");
            this.panel9.Name = "panel9";
            // 
            // btnLiveImage
            // 
            this.btnLiveImage.BackColor = System.Drawing.Color.White;
            this.btnLiveImage.BackgroundImage = global::Alpha.Properties.Resources.Play;
            resources.ApplyResources(this.btnLiveImage, "btnLiveImage");
            this.btnLiveImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnLiveImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnLiveImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLiveImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnLiveImage.Name = "btnLiveImage";
            this.tTip.SetToolTip(this.btnLiveImage, resources.GetString("btnLiveImage.ToolTip"));
            this.btnLiveImage.UseVisualStyleBackColor = false;
            this.btnLiveImage.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnGrabImage
            // 
            this.btnGrabImage.BackColor = System.Drawing.Color.White;
            this.btnGrabImage.BackgroundImage = global::Alpha.Properties.Resources.CCDSetup;
            resources.ApplyResources(this.btnGrabImage, "btnGrabImage");
            this.btnGrabImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnGrabImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnGrabImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGrabImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnGrabImage.Name = "btnGrabImage";
            this.tTip.SetToolTip(this.btnGrabImage, resources.GetString("btnGrabImage.ToolTip"));
            this.btnGrabImage.UseVisualStyleBackColor = false;
            this.btnGrabImage.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // btnEditVPRO
            // 
            this.btnEditVPRO.BackColor = System.Drawing.Color.White;
            this.btnEditVPRO.BackgroundImage = global::Alpha.Properties.Resources.cognex;
            resources.ApplyResources(this.btnEditVPRO, "btnEditVPRO");
            this.btnEditVPRO.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnEditVPRO.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnEditVPRO.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEditVPRO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnEditVPRO.Name = "btnEditVPRO";
            this.tTip.SetToolTip(this.btnEditVPRO, resources.GetString("btnEditVPRO.ToolTip"));
            this.btnEditVPRO.UseVisualStyleBackColor = false;
            this.btnEditVPRO.Click += new System.EventHandler(this.btnEditVPRO_Click);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.panel19, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnTeachSelectedProgram, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnExVision, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel47, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel32, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel25, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.button2, 7, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // panel19
            // 
            this.panel19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel19.Controls.Add(this.label_Recipe_Light);
            this.panel19.Controls.Add(this.cbLights);
            resources.ApplyResources(this.panel19, "panel19");
            this.panel19.Name = "panel19";
            // 
            // label_Recipe_Light
            // 
            this.label_Recipe_Light.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Recipe_Light, "label_Recipe_Light");
            this.label_Recipe_Light.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Recipe_Light.Name = "label_Recipe_Light";
            this.label_Recipe_Light.Tag = "";
            // 
            // cbLights
            // 
            resources.ApplyResources(this.cbLights, "cbLights");
            this.cbLights.FormattingEnabled = true;
            this.cbLights.Items.AddRange(new object[] {
            resources.GetString("cbLights.Items"),
            resources.GetString("cbLights.Items1")});
            this.cbLights.Name = "cbLights";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label_Recipe_Program);
            this.panel1.Controls.Add(this.cbSelectProcess);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label_Recipe_Program
            // 
            this.label_Recipe_Program.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Recipe_Program, "label_Recipe_Program");
            this.label_Recipe_Program.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Recipe_Program.Name = "label_Recipe_Program";
            this.label_Recipe_Program.Tag = "";
            // 
            // cbSelectProcess
            // 
            resources.ApplyResources(this.cbSelectProcess, "cbSelectProcess");
            this.cbSelectProcess.FormattingEnabled = true;
            this.cbSelectProcess.Name = "cbSelectProcess";
            // 
            // btnTeachSelectedProgram
            // 
            this.btnTeachSelectedProgram.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnTeachSelectedProgram, "btnTeachSelectedProgram");
            this.btnTeachSelectedProgram.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnTeachSelectedProgram.FlatAppearance.BorderSize = 2;
            this.btnTeachSelectedProgram.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnTeachSelectedProgram.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTeachSelectedProgram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnTeachSelectedProgram.Name = "btnTeachSelectedProgram";
            this.btnTeachSelectedProgram.UseVisualStyleBackColor = false;
            this.btnTeachSelectedProgram.Click += new System.EventHandler(this.btnTeachSelectedProgram_Click);
            // 
            // btnExVision
            // 
            this.btnExVision.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnExVision, "btnExVision");
            this.btnExVision.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnExVision.FlatAppearance.BorderSize = 2;
            this.btnExVision.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnExVision.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExVision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnExVision.Name = "btnExVision";
            this.btnExVision.UseVisualStyleBackColor = false;
            this.btnExVision.Click += new System.EventHandler(this.btnExVision_Click);
            // 
            // panel47
            // 
            this.panel47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel47.Controls.Add(this.lbInspectionX);
            this.panel47.Controls.Add(this.label_Recipe_PixelsX);
            resources.ApplyResources(this.panel47, "panel47");
            this.panel47.Name = "panel47";
            // 
            // lbInspectionX
            // 
            this.lbInspectionX.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbInspectionX, "lbInspectionX");
            this.lbInspectionX.Name = "lbInspectionX";
            // 
            // label_Recipe_PixelsX
            // 
            this.label_Recipe_PixelsX.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Recipe_PixelsX, "label_Recipe_PixelsX");
            this.label_Recipe_PixelsX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Recipe_PixelsX.Name = "label_Recipe_PixelsX";
            this.label_Recipe_PixelsX.Tag = "";
            // 
            // panel32
            // 
            this.panel32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel32.Controls.Add(this.lbInspectionY);
            this.panel32.Controls.Add(this.label_Recipe_PixelsY);
            resources.ApplyResources(this.panel32, "panel32");
            this.panel32.Name = "panel32";
            // 
            // lbInspectionY
            // 
            this.lbInspectionY.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbInspectionY, "lbInspectionY");
            this.lbInspectionY.Name = "lbInspectionY";
            // 
            // label_Recipe_PixelsY
            // 
            this.label_Recipe_PixelsY.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Recipe_PixelsY, "label_Recipe_PixelsY");
            this.label_Recipe_PixelsY.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Recipe_PixelsY.Name = "label_Recipe_PixelsY";
            this.label_Recipe_PixelsY.Tag = "";
            // 
            // panel25
            // 
            this.panel25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel25.Controls.Add(this.lbInspectionU);
            this.panel25.Controls.Add(this.label_Recipe_DegU);
            resources.ApplyResources(this.panel25, "panel25");
            this.panel25.Name = "panel25";
            // 
            // lbInspectionU
            // 
            this.lbInspectionU.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbInspectionU, "lbInspectionU");
            this.lbInspectionU.Name = "lbInspectionU";
            // 
            // label_Recipe_DegU
            // 
            this.label_Recipe_DegU.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Recipe_DegU, "label_Recipe_DegU");
            this.label_Recipe_DegU.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Recipe_DegU.Name = "label_Recipe_DegU";
            this.label_Recipe_DegU.Tag = "";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button2, "button2");
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel7
            // 
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.dgvPosInspection);
            this.panel7.Name = "panel7";
            // 
            // dgvPosInspection
            // 
            this.dgvPosInspection.AllowUserToResizeRows = false;
            this.dgvPosInspection.AutoGenerateColumns = false;
            this.dgvPosInspection.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPosInspection.BackgroundColor = System.Drawing.Color.White;
            this.dgvPosInspection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPosInspection.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPosInspection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPosInspection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pNameDataGridViewTextBoxColumn,
            this.vPPNameDataGridViewTextBoxColumn,
            this.dataGridViewCheckBoxColumn1,
            this.totalmmXDataGridViewTextBoxColumn,
            this.totalmmYDataGridViewTextBoxColumn,
            this.totalmmUDataGridViewTextBoxColumn,
            this.resultBoolDataGridViewCheckBoxColumn,
            this.resultDoubleDataGridViewTextBoxColumn,
            this.resultStringDataGridViewTextBoxColumn,
            this.pxXmmXDataGridViewTextBoxColumn,
            this.pxXmmYDataGridViewTextBoxColumn,
            this.teachPxXDataGridViewTextBoxColumn,
            this.teachPxYDataGridViewTextBoxColumn,
            this.teachPxUDataGridViewTextBoxColumn,
            this.offsetmmXDataGridViewTextBoxColumn,
            this.offsetmmYDataGridViewTextBoxColumn,
            this.offsetmmUDataGridViewTextBoxColumn});
            this.dgvPosInspection.DataMember = "RVisionDataTable";
            this.dgvPosInspection.DataSource = this.RecipeData;
            resources.ApplyResources(this.dgvPosInspection, "dgvPosInspection");
            this.dgvPosInspection.EnableHeadersVisualStyles = false;
            this.dgvPosInspection.GridColor = System.Drawing.Color.Black;
            this.dgvPosInspection.Name = "dgvPosInspection";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPosInspection.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPosInspection.RowTemplate.Height = 24;
            // 
            // pNameDataGridViewTextBoxColumn
            // 
            this.pNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pNameDataGridViewTextBoxColumn.DataPropertyName = "PName";
            resources.ApplyResources(this.pNameDataGridViewTextBoxColumn, "pNameDataGridViewTextBoxColumn");
            this.pNameDataGridViewTextBoxColumn.Name = "pNameDataGridViewTextBoxColumn";
            // 
            // vPPNameDataGridViewTextBoxColumn
            // 
            this.vPPNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.vPPNameDataGridViewTextBoxColumn.DataPropertyName = "VPPName";
            resources.ApplyResources(this.vPPNameDataGridViewTextBoxColumn, "vPPNameDataGridViewTextBoxColumn");
            this.vPPNameDataGridViewTextBoxColumn.Name = "vPPNameDataGridViewTextBoxColumn";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "c_VisionEnabled";
            resources.ApplyResources(this.dataGridViewCheckBoxColumn1, "dataGridViewCheckBoxColumn1");
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // totalmmXDataGridViewTextBoxColumn
            // 
            this.totalmmXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.totalmmXDataGridViewTextBoxColumn.DataPropertyName = "TotalmmX";
            resources.ApplyResources(this.totalmmXDataGridViewTextBoxColumn, "totalmmXDataGridViewTextBoxColumn");
            this.totalmmXDataGridViewTextBoxColumn.Name = "totalmmXDataGridViewTextBoxColumn";
            // 
            // totalmmYDataGridViewTextBoxColumn
            // 
            this.totalmmYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.totalmmYDataGridViewTextBoxColumn.DataPropertyName = "TotalmmY";
            resources.ApplyResources(this.totalmmYDataGridViewTextBoxColumn, "totalmmYDataGridViewTextBoxColumn");
            this.totalmmYDataGridViewTextBoxColumn.Name = "totalmmYDataGridViewTextBoxColumn";
            // 
            // totalmmUDataGridViewTextBoxColumn
            // 
            this.totalmmUDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.totalmmUDataGridViewTextBoxColumn.DataPropertyName = "TotalmmU";
            resources.ApplyResources(this.totalmmUDataGridViewTextBoxColumn, "totalmmUDataGridViewTextBoxColumn");
            this.totalmmUDataGridViewTextBoxColumn.Name = "totalmmUDataGridViewTextBoxColumn";
            // 
            // resultBoolDataGridViewCheckBoxColumn
            // 
            this.resultBoolDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.resultBoolDataGridViewCheckBoxColumn.DataPropertyName = "ResultBool";
            resources.ApplyResources(this.resultBoolDataGridViewCheckBoxColumn, "resultBoolDataGridViewCheckBoxColumn");
            this.resultBoolDataGridViewCheckBoxColumn.Name = "resultBoolDataGridViewCheckBoxColumn";
            // 
            // resultDoubleDataGridViewTextBoxColumn
            // 
            this.resultDoubleDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.resultDoubleDataGridViewTextBoxColumn.DataPropertyName = "ResultDouble";
            resources.ApplyResources(this.resultDoubleDataGridViewTextBoxColumn, "resultDoubleDataGridViewTextBoxColumn");
            this.resultDoubleDataGridViewTextBoxColumn.Name = "resultDoubleDataGridViewTextBoxColumn";
            // 
            // resultStringDataGridViewTextBoxColumn
            // 
            this.resultStringDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.resultStringDataGridViewTextBoxColumn.DataPropertyName = "ResultString";
            resources.ApplyResources(this.resultStringDataGridViewTextBoxColumn, "resultStringDataGridViewTextBoxColumn");
            this.resultStringDataGridViewTextBoxColumn.Name = "resultStringDataGridViewTextBoxColumn";
            // 
            // pxXmmXDataGridViewTextBoxColumn
            // 
            this.pxXmmXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pxXmmXDataGridViewTextBoxColumn.DataPropertyName = "PxXmmX";
            resources.ApplyResources(this.pxXmmXDataGridViewTextBoxColumn, "pxXmmXDataGridViewTextBoxColumn");
            this.pxXmmXDataGridViewTextBoxColumn.Name = "pxXmmXDataGridViewTextBoxColumn";
            // 
            // pxXmmYDataGridViewTextBoxColumn
            // 
            this.pxXmmYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pxXmmYDataGridViewTextBoxColumn.DataPropertyName = "PxXmmY";
            resources.ApplyResources(this.pxXmmYDataGridViewTextBoxColumn, "pxXmmYDataGridViewTextBoxColumn");
            this.pxXmmYDataGridViewTextBoxColumn.Name = "pxXmmYDataGridViewTextBoxColumn";
            // 
            // teachPxXDataGridViewTextBoxColumn
            // 
            this.teachPxXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.teachPxXDataGridViewTextBoxColumn.DataPropertyName = "TeachPxX";
            resources.ApplyResources(this.teachPxXDataGridViewTextBoxColumn, "teachPxXDataGridViewTextBoxColumn");
            this.teachPxXDataGridViewTextBoxColumn.Name = "teachPxXDataGridViewTextBoxColumn";
            // 
            // teachPxYDataGridViewTextBoxColumn
            // 
            this.teachPxYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.teachPxYDataGridViewTextBoxColumn.DataPropertyName = "TeachPxY";
            resources.ApplyResources(this.teachPxYDataGridViewTextBoxColumn, "teachPxYDataGridViewTextBoxColumn");
            this.teachPxYDataGridViewTextBoxColumn.Name = "teachPxYDataGridViewTextBoxColumn";
            // 
            // teachPxUDataGridViewTextBoxColumn
            // 
            this.teachPxUDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.teachPxUDataGridViewTextBoxColumn.DataPropertyName = "TeachPxU";
            resources.ApplyResources(this.teachPxUDataGridViewTextBoxColumn, "teachPxUDataGridViewTextBoxColumn");
            this.teachPxUDataGridViewTextBoxColumn.Name = "teachPxUDataGridViewTextBoxColumn";
            // 
            // offsetmmXDataGridViewTextBoxColumn
            // 
            this.offsetmmXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.offsetmmXDataGridViewTextBoxColumn.DataPropertyName = "OffsetmmX";
            resources.ApplyResources(this.offsetmmXDataGridViewTextBoxColumn, "offsetmmXDataGridViewTextBoxColumn");
            this.offsetmmXDataGridViewTextBoxColumn.Name = "offsetmmXDataGridViewTextBoxColumn";
            // 
            // offsetmmYDataGridViewTextBoxColumn
            // 
            this.offsetmmYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.offsetmmYDataGridViewTextBoxColumn.DataPropertyName = "OffsetmmY";
            resources.ApplyResources(this.offsetmmYDataGridViewTextBoxColumn, "offsetmmYDataGridViewTextBoxColumn");
            this.offsetmmYDataGridViewTextBoxColumn.Name = "offsetmmYDataGridViewTextBoxColumn";
            // 
            // offsetmmUDataGridViewTextBoxColumn
            // 
            this.offsetmmUDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.offsetmmUDataGridViewTextBoxColumn.DataPropertyName = "OffsetmmU";
            resources.ApplyResources(this.offsetmmUDataGridViewTextBoxColumn, "offsetmmUDataGridViewTextBoxColumn");
            this.offsetmmUDataGridViewTextBoxColumn.Name = "offsetmmUDataGridViewTextBoxColumn";
            // 
            // tabControl_Recipe
            // 
            this.tabControl_Recipe.Controls.Add(this.Image);
            resources.ApplyResources(this.tabControl_Recipe, "tabControl_Recipe");
            this.tabControl_Recipe.Name = "tabControl_Recipe";
            this.tabControl_Recipe.SelectedIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.labelImagePath, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.textLocalPath, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonLocalPath, 2, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // labelImagePath
            // 
            resources.ApplyResources(this.labelImagePath, "labelImagePath");
            this.labelImagePath.Name = "labelImagePath";
            // 
            // textLocalPath
            // 
            this.textLocalPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.SettingData, "PSet.ImageSavePath", true));
            resources.ApplyResources(this.textLocalPath, "textLocalPath");
            this.textLocalPath.Name = "textLocalPath";
            // 
            // buttonLocalPath
            // 
            resources.ApplyResources(this.buttonLocalPath, "buttonLocalPath");
            this.buttonLocalPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.buttonLocalPath.FlatAppearance.BorderSize = 2;
            this.buttonLocalPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonLocalPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.buttonLocalPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.buttonLocalPath.Name = "buttonLocalPath";
            this.buttonLocalPath.UseVisualStyleBackColor = true;
            this.buttonLocalPath.Click += new System.EventHandler(this.buttonLocalPath_Click);
            // 
            // PCameraSettings
            // 
            this.PCameraSettings.Columns.AddRange(new System.Data.DataColumn[] {
            this.CameraVPPName,
            this.ImageToolVPPName});
            this.PCameraSettings.Namespace = "VisionProRAC";
            this.PCameraSettings.TableName = "PCameraSettings";
            // 
            // CameraVPPName
            // 
            this.CameraVPPName.AllowDBNull = false;
            this.CameraVPPName.ColumnName = "CameraVPPName";
            this.CameraVPPName.DefaultValue = "";
            // 
            // ImageToolVPPName
            // 
            this.ImageToolVPPName.AllowDBNull = false;
            this.ImageToolVPPName.ColumnName = "ImageToolVPPName";
            this.ImageToolVPPName.DefaultValue = "";
            // 
            // tTip
            // 
            this.tTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tTip.UseAnimation = false;
            this.tTip.UseFading = false;
            // 
            // ImageSavePath
            // 
            this.ImageSavePath.AllowDBNull = false;
            this.ImageSavePath.ColumnName = "ImageSavePath";
            this.ImageSavePath.DefaultValue = "";
            // 
            // tabControl_ProductionSettings
            // 
            this.tabControl_ProductionSettings.Controls.Add(this.tabPage_VPROSettings);
            this.tabControl_ProductionSettings.Controls.Add(this.tabPage_CameraRegister);
            this.tabControl_ProductionSettings.Controls.Add(this.tabPage_StaticPrograms);
            this.tabControl_ProductionSettings.Controls.Add(this.tabPage_CameraTeach);
            this.tabControl_ProductionSettings.Controls.Add(this.tabPage_ImageSetup);
            resources.ApplyResources(this.tabControl_ProductionSettings, "tabControl_ProductionSettings");
            this.tabControl_ProductionSettings.Name = "tabControl_ProductionSettings";
            this.tabControl_ProductionSettings.SelectedIndex = 0;
            // 
            // tabPage_VPROSettings
            // 
            this.tabPage_VPROSettings.Controls.Add(this.checkBox2);
            this.tabPage_VPROSettings.Controls.Add(this.tableLayoutPanel5);
            resources.ApplyResources(this.tabPage_VPROSettings, "tabPage_VPROSettings");
            this.tabPage_VPROSettings.Name = "tabPage_VPROSettings";
            this.tabPage_VPROSettings.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.SettingData, "PSet.EnableVisionGantry", true));
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // tabPage_CameraRegister
            // 
            this.tabPage_CameraRegister.Controls.Add(this.panel4);
            resources.ApplyResources(this.tabPage_CameraRegister, "tabPage_CameraRegister");
            this.tabPage_CameraRegister.Name = "tabPage_CameraRegister";
            this.tabPage_CameraRegister.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridView_CameraSettings);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // dataGridView_CameraSettings
            // 
            this.dataGridView_CameraSettings.AllowUserToResizeRows = false;
            this.dataGridView_CameraSettings.AutoGenerateColumns = false;
            this.dataGridView_CameraSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_CameraSettings.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_CameraSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_CameraSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CameraSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cameraVPPNameDataGridViewTextBoxColumn,
            this.imageToolVPPNameDataGridViewTextBoxColumn});
            this.dataGridView_CameraSettings.DataMember = "PCameraSettings";
            this.dataGridView_CameraSettings.DataSource = this.SettingData;
            resources.ApplyResources(this.dataGridView_CameraSettings, "dataGridView_CameraSettings");
            this.dataGridView_CameraSettings.EnableHeadersVisualStyles = false;
            this.dataGridView_CameraSettings.GridColor = System.Drawing.Color.Black;
            this.dataGridView_CameraSettings.Name = "dataGridView_CameraSettings";
            this.dataGridView_CameraSettings.RowTemplate.Height = 24;
            // 
            // cameraVPPNameDataGridViewTextBoxColumn
            // 
            this.cameraVPPNameDataGridViewTextBoxColumn.DataPropertyName = "CameraVPPName";
            resources.ApplyResources(this.cameraVPPNameDataGridViewTextBoxColumn, "cameraVPPNameDataGridViewTextBoxColumn");
            this.cameraVPPNameDataGridViewTextBoxColumn.Name = "cameraVPPNameDataGridViewTextBoxColumn";
            // 
            // imageToolVPPNameDataGridViewTextBoxColumn
            // 
            this.imageToolVPPNameDataGridViewTextBoxColumn.DataPropertyName = "ImageToolVPPName";
            resources.ApplyResources(this.imageToolVPPNameDataGridViewTextBoxColumn, "imageToolVPPNameDataGridViewTextBoxColumn");
            this.imageToolVPPNameDataGridViewTextBoxColumn.Name = "imageToolVPPNameDataGridViewTextBoxColumn";
            // 
            // tabPage_StaticPrograms
            // 
            this.tabPage_StaticPrograms.Controls.Add(this.tableLayoutPanel_StaticPrograms);
            resources.ApplyResources(this.tabPage_StaticPrograms, "tabPage_StaticPrograms");
            this.tabPage_StaticPrograms.Name = "tabPage_StaticPrograms";
            this.tabPage_StaticPrograms.UseVisualStyleBackColor = true;
            this.tabPage_StaticPrograms.Enter += new System.EventHandler(this.tabPage_StaticPrograms_Enter);
            // 
            // tableLayoutPanel_StaticPrograms
            // 
            resources.ApplyResources(this.tableLayoutPanel_StaticPrograms, "tableLayoutPanel_StaticPrograms");
            this.tableLayoutPanel_StaticPrograms.Controls.Add(this.panel_ProductionSettingImage, 0, 0);
            this.tableLayoutPanel_StaticPrograms.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel_StaticPrograms.Controls.Add(this.panel15, 0, 2);
            this.tableLayoutPanel_StaticPrograms.Name = "tableLayoutPanel_StaticPrograms";
            // 
            // panel_ProductionSettingImage
            // 
            resources.ApplyResources(this.panel_ProductionSettingImage, "panel_ProductionSettingImage");
            this.panel_ProductionSettingImage.Name = "panel_ProductionSettingImage";
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.panel10, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel11, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.button_Production_RunVision, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel12, 4, 0);
            this.tableLayoutPanel7.Controls.Add(this.button_Production_TeachSelected, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel13, 5, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel14, 6, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.label_Production_Light);
            this.panel10.Controls.Add(this.comboBox_Production_Light);
            resources.ApplyResources(this.panel10, "panel10");
            this.panel10.Name = "panel10";
            // 
            // label_Production_Light
            // 
            this.label_Production_Light.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Production_Light, "label_Production_Light");
            this.label_Production_Light.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Production_Light.Name = "label_Production_Light";
            this.label_Production_Light.Tag = "";
            // 
            // comboBox_Production_Light
            // 
            resources.ApplyResources(this.comboBox_Production_Light, "comboBox_Production_Light");
            this.comboBox_Production_Light.FormattingEnabled = true;
            this.comboBox_Production_Light.Items.AddRange(new object[] {
            resources.GetString("comboBox_Production_Light.Items"),
            resources.GetString("comboBox_Production_Light.Items1")});
            this.comboBox_Production_Light.Name = "comboBox_Production_Light";
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.label_Production_Program);
            this.panel11.Controls.Add(this.comboBox_Production_Program);
            resources.ApplyResources(this.panel11, "panel11");
            this.panel11.Name = "panel11";
            // 
            // label_Production_Program
            // 
            this.label_Production_Program.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Production_Program, "label_Production_Program");
            this.label_Production_Program.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Production_Program.Name = "label_Production_Program";
            this.label_Production_Program.Tag = "";
            // 
            // comboBox_Production_Program
            // 
            resources.ApplyResources(this.comboBox_Production_Program, "comboBox_Production_Program");
            this.comboBox_Production_Program.FormattingEnabled = true;
            this.comboBox_Production_Program.Name = "comboBox_Production_Program";
            // 
            // button_Production_RunVision
            // 
            this.button_Production_RunVision.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button_Production_RunVision, "button_Production_RunVision");
            this.button_Production_RunVision.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.button_Production_RunVision.FlatAppearance.BorderSize = 2;
            this.button_Production_RunVision.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button_Production_RunVision.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_Production_RunVision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.button_Production_RunVision.Name = "button_Production_RunVision";
            this.button_Production_RunVision.UseVisualStyleBackColor = false;
            this.button_Production_RunVision.Click += new System.EventHandler(this.button_Production_RunVision_Click);
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.label_Production_ResultPixelsX);
            this.panel12.Controls.Add(this.label_Production_PixelsX);
            resources.ApplyResources(this.panel12, "panel12");
            this.panel12.Name = "panel12";
            // 
            // label_Production_ResultPixelsX
            // 
            this.label_Production_ResultPixelsX.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Production_ResultPixelsX, "label_Production_ResultPixelsX");
            this.label_Production_ResultPixelsX.Name = "label_Production_ResultPixelsX";
            // 
            // label_Production_PixelsX
            // 
            this.label_Production_PixelsX.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Production_PixelsX, "label_Production_PixelsX");
            this.label_Production_PixelsX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Production_PixelsX.Name = "label_Production_PixelsX";
            this.label_Production_PixelsX.Tag = "";
            // 
            // button_Production_TeachSelected
            // 
            this.button_Production_TeachSelected.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button_Production_TeachSelected, "button_Production_TeachSelected");
            this.button_Production_TeachSelected.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.button_Production_TeachSelected.FlatAppearance.BorderSize = 2;
            this.button_Production_TeachSelected.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button_Production_TeachSelected.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_Production_TeachSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.button_Production_TeachSelected.Name = "button_Production_TeachSelected";
            this.button_Production_TeachSelected.UseVisualStyleBackColor = false;
            this.button_Production_TeachSelected.Click += new System.EventHandler(this.button_Production_TeachSelected_Click);
            // 
            // panel13
            // 
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.label_Production_ResultPixelsY);
            this.panel13.Controls.Add(this.label_Production_PixelsY);
            resources.ApplyResources(this.panel13, "panel13");
            this.panel13.Name = "panel13";
            // 
            // label_Production_ResultPixelsY
            // 
            this.label_Production_ResultPixelsY.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Production_ResultPixelsY, "label_Production_ResultPixelsY");
            this.label_Production_ResultPixelsY.Name = "label_Production_ResultPixelsY";
            // 
            // label_Production_PixelsY
            // 
            this.label_Production_PixelsY.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Production_PixelsY, "label_Production_PixelsY");
            this.label_Production_PixelsY.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Production_PixelsY.Name = "label_Production_PixelsY";
            this.label_Production_PixelsY.Tag = "";
            // 
            // panel14
            // 
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.label_Production_ResultDegU);
            this.panel14.Controls.Add(this.label_Production_DegU);
            resources.ApplyResources(this.panel14, "panel14");
            this.panel14.Name = "panel14";
            // 
            // label_Production_ResultDegU
            // 
            this.label_Production_ResultDegU.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label_Production_ResultDegU, "label_Production_ResultDegU");
            this.label_Production_ResultDegU.Name = "label_Production_ResultDegU";
            // 
            // label_Production_DegU
            // 
            this.label_Production_DegU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            resources.ApplyResources(this.label_Production_DegU, "label_Production_DegU");
            this.label_Production_DegU.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label_Production_DegU.Name = "label_Production_DegU";
            this.label_Production_DegU.Tag = "MenuColors";
            // 
            // panel15
            // 
            resources.ApplyResources(this.panel15, "panel15");
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel15.Controls.Add(this.panel5);
            this.panel15.Name = "panel15";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView_VisionData);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // dataGridView_VisionData
            // 
            this.dataGridView_VisionData.AllowUserToResizeRows = false;
            this.dataGridView_VisionData.AutoGenerateColumns = false;
            this.dataGridView_VisionData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_VisionData.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_VisionData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_VisionData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_VisionData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pSNameDataGridViewTextBoxColumn,
            this.pSVPPNameDataGridViewTextBoxColumn,
            this.pSTotalmmXDataGridViewTextBoxColumn,
            this.pSTotalmmYDataGridViewTextBoxColumn,
            this.pSTotalmmUDataGridViewTextBoxColumn,
            this.pSResultBoolDataGridViewCheckBoxColumn,
            this.pSResultDoubleDataGridViewTextBoxColumn,
            this.pSResultStringDataGridViewTextBoxColumn,
            this.pSPxXmmXDataGridViewTextBoxColumn,
            this.pSPxXmmYDataGridViewTextBoxColumn,
            this.pSTeachPxXDataGridViewTextBoxColumn,
            this.pSTeachPxYDataGridViewTextBoxColumn,
            this.pSTeachPxUDataGridViewTextBoxColumn,
            this.pSOffsetmmXDataGridViewTextBoxColumn,
            this.pSOffsetmmYDataGridViewTextBoxColumn,
            this.pSOffsetmmUDataGridViewTextBoxColumn});
            this.dataGridView_VisionData.DataMember = "PVisionDataTable";
            this.dataGridView_VisionData.DataSource = this.SettingData;
            resources.ApplyResources(this.dataGridView_VisionData, "dataGridView_VisionData");
            this.dataGridView_VisionData.EnableHeadersVisualStyles = false;
            this.dataGridView_VisionData.GridColor = System.Drawing.Color.Black;
            this.dataGridView_VisionData.Name = "dataGridView_VisionData";
            this.dataGridView_VisionData.RowTemplate.Height = 24;
            // 
            // pSNameDataGridViewTextBoxColumn
            // 
            this.pSNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSNameDataGridViewTextBoxColumn.DataPropertyName = "PSName";
            resources.ApplyResources(this.pSNameDataGridViewTextBoxColumn, "pSNameDataGridViewTextBoxColumn");
            this.pSNameDataGridViewTextBoxColumn.Name = "pSNameDataGridViewTextBoxColumn";
            // 
            // pSVPPNameDataGridViewTextBoxColumn
            // 
            this.pSVPPNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSVPPNameDataGridViewTextBoxColumn.DataPropertyName = "PSVPPName";
            resources.ApplyResources(this.pSVPPNameDataGridViewTextBoxColumn, "pSVPPNameDataGridViewTextBoxColumn");
            this.pSVPPNameDataGridViewTextBoxColumn.Name = "pSVPPNameDataGridViewTextBoxColumn";
            // 
            // pSTotalmmXDataGridViewTextBoxColumn
            // 
            this.pSTotalmmXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSTotalmmXDataGridViewTextBoxColumn.DataPropertyName = "PSTotalmmX";
            resources.ApplyResources(this.pSTotalmmXDataGridViewTextBoxColumn, "pSTotalmmXDataGridViewTextBoxColumn");
            this.pSTotalmmXDataGridViewTextBoxColumn.Name = "pSTotalmmXDataGridViewTextBoxColumn";
            // 
            // pSTotalmmYDataGridViewTextBoxColumn
            // 
            this.pSTotalmmYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSTotalmmYDataGridViewTextBoxColumn.DataPropertyName = "PSTotalmmY";
            resources.ApplyResources(this.pSTotalmmYDataGridViewTextBoxColumn, "pSTotalmmYDataGridViewTextBoxColumn");
            this.pSTotalmmYDataGridViewTextBoxColumn.Name = "pSTotalmmYDataGridViewTextBoxColumn";
            // 
            // pSTotalmmUDataGridViewTextBoxColumn
            // 
            this.pSTotalmmUDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSTotalmmUDataGridViewTextBoxColumn.DataPropertyName = "PSTotalmmU";
            resources.ApplyResources(this.pSTotalmmUDataGridViewTextBoxColumn, "pSTotalmmUDataGridViewTextBoxColumn");
            this.pSTotalmmUDataGridViewTextBoxColumn.Name = "pSTotalmmUDataGridViewTextBoxColumn";
            // 
            // pSResultBoolDataGridViewCheckBoxColumn
            // 
            this.pSResultBoolDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSResultBoolDataGridViewCheckBoxColumn.DataPropertyName = "PSResultBool";
            resources.ApplyResources(this.pSResultBoolDataGridViewCheckBoxColumn, "pSResultBoolDataGridViewCheckBoxColumn");
            this.pSResultBoolDataGridViewCheckBoxColumn.Name = "pSResultBoolDataGridViewCheckBoxColumn";
            // 
            // pSResultDoubleDataGridViewTextBoxColumn
            // 
            this.pSResultDoubleDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSResultDoubleDataGridViewTextBoxColumn.DataPropertyName = "PSResultDouble";
            resources.ApplyResources(this.pSResultDoubleDataGridViewTextBoxColumn, "pSResultDoubleDataGridViewTextBoxColumn");
            this.pSResultDoubleDataGridViewTextBoxColumn.Name = "pSResultDoubleDataGridViewTextBoxColumn";
            // 
            // pSResultStringDataGridViewTextBoxColumn
            // 
            this.pSResultStringDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSResultStringDataGridViewTextBoxColumn.DataPropertyName = "PSResultString";
            resources.ApplyResources(this.pSResultStringDataGridViewTextBoxColumn, "pSResultStringDataGridViewTextBoxColumn");
            this.pSResultStringDataGridViewTextBoxColumn.Name = "pSResultStringDataGridViewTextBoxColumn";
            // 
            // pSPxXmmXDataGridViewTextBoxColumn
            // 
            this.pSPxXmmXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSPxXmmXDataGridViewTextBoxColumn.DataPropertyName = "PSPxXmmX";
            resources.ApplyResources(this.pSPxXmmXDataGridViewTextBoxColumn, "pSPxXmmXDataGridViewTextBoxColumn");
            this.pSPxXmmXDataGridViewTextBoxColumn.Name = "pSPxXmmXDataGridViewTextBoxColumn";
            // 
            // pSPxXmmYDataGridViewTextBoxColumn
            // 
            this.pSPxXmmYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSPxXmmYDataGridViewTextBoxColumn.DataPropertyName = "PSPxXmmY";
            resources.ApplyResources(this.pSPxXmmYDataGridViewTextBoxColumn, "pSPxXmmYDataGridViewTextBoxColumn");
            this.pSPxXmmYDataGridViewTextBoxColumn.Name = "pSPxXmmYDataGridViewTextBoxColumn";
            // 
            // pSTeachPxXDataGridViewTextBoxColumn
            // 
            this.pSTeachPxXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSTeachPxXDataGridViewTextBoxColumn.DataPropertyName = "PSTeachPxX";
            resources.ApplyResources(this.pSTeachPxXDataGridViewTextBoxColumn, "pSTeachPxXDataGridViewTextBoxColumn");
            this.pSTeachPxXDataGridViewTextBoxColumn.Name = "pSTeachPxXDataGridViewTextBoxColumn";
            // 
            // pSTeachPxYDataGridViewTextBoxColumn
            // 
            this.pSTeachPxYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSTeachPxYDataGridViewTextBoxColumn.DataPropertyName = "PSTeachPxY";
            resources.ApplyResources(this.pSTeachPxYDataGridViewTextBoxColumn, "pSTeachPxYDataGridViewTextBoxColumn");
            this.pSTeachPxYDataGridViewTextBoxColumn.Name = "pSTeachPxYDataGridViewTextBoxColumn";
            // 
            // pSTeachPxUDataGridViewTextBoxColumn
            // 
            this.pSTeachPxUDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSTeachPxUDataGridViewTextBoxColumn.DataPropertyName = "PSTeachPxU";
            resources.ApplyResources(this.pSTeachPxUDataGridViewTextBoxColumn, "pSTeachPxUDataGridViewTextBoxColumn");
            this.pSTeachPxUDataGridViewTextBoxColumn.Name = "pSTeachPxUDataGridViewTextBoxColumn";
            // 
            // pSOffsetmmXDataGridViewTextBoxColumn
            // 
            this.pSOffsetmmXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSOffsetmmXDataGridViewTextBoxColumn.DataPropertyName = "PSOffsetmmX";
            resources.ApplyResources(this.pSOffsetmmXDataGridViewTextBoxColumn, "pSOffsetmmXDataGridViewTextBoxColumn");
            this.pSOffsetmmXDataGridViewTextBoxColumn.Name = "pSOffsetmmXDataGridViewTextBoxColumn";
            // 
            // pSOffsetmmYDataGridViewTextBoxColumn
            // 
            this.pSOffsetmmYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSOffsetmmYDataGridViewTextBoxColumn.DataPropertyName = "PSOffsetmmY";
            resources.ApplyResources(this.pSOffsetmmYDataGridViewTextBoxColumn, "pSOffsetmmYDataGridViewTextBoxColumn");
            this.pSOffsetmmYDataGridViewTextBoxColumn.Name = "pSOffsetmmYDataGridViewTextBoxColumn";
            // 
            // pSOffsetmmUDataGridViewTextBoxColumn
            // 
            this.pSOffsetmmUDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.pSOffsetmmUDataGridViewTextBoxColumn.DataPropertyName = "PSOffsetmmU";
            resources.ApplyResources(this.pSOffsetmmUDataGridViewTextBoxColumn, "pSOffsetmmUDataGridViewTextBoxColumn");
            this.pSOffsetmmUDataGridViewTextBoxColumn.Name = "pSOffsetmmUDataGridViewTextBoxColumn";
            // 
            // tabPage_CameraTeach
            // 
            this.tabPage_CameraTeach.Controls.Add(this.cogAcqFifoEditV21);
            resources.ApplyResources(this.tabPage_CameraTeach, "tabPage_CameraTeach");
            this.tabPage_CameraTeach.Name = "tabPage_CameraTeach";
            this.tabPage_CameraTeach.UseVisualStyleBackColor = true;
            // 
            // cogAcqFifoEditV21
            // 
            this.cogAcqFifoEditV21.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.cogAcqFifoEditV21, "cogAcqFifoEditV21");
            this.cogAcqFifoEditV21.Name = "cogAcqFifoEditV21";
            this.cogAcqFifoEditV21.SuspendElectricRuns = false;
            // 
            // tabPage_ImageSetup
            // 
            this.tabPage_ImageSetup.Controls.Add(this.cogIPOneImageEditV21);
            resources.ApplyResources(this.tabPage_ImageSetup, "tabPage_ImageSetup");
            this.tabPage_ImageSetup.Name = "tabPage_ImageSetup";
            this.tabPage_ImageSetup.UseVisualStyleBackColor = true;
            // 
            // cogIPOneImageEditV21
            // 
            this.cogIPOneImageEditV21.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.cogIPOneImageEditV21, "cogIPOneImageEditV21");
            this.cogIPOneImageEditV21.Name = "cogIPOneImageEditV21";
            this.cogIPOneImageEditV21.SuspendElectricRuns = false;
            // 
            // PVisionDataTable
            // 
            this.PVisionDataTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.PSName,
            this.PSPxXmmX,
            this.PSPxXmmY,
            this.PSTeachPxX,
            this.PSTeachPxY,
            this.PSTeachPxU,
            this.PSOffsetmmX,
            this.PSOffsetmmY,
            this.PSOffsetmmU,
            this.PSTotalmmX,
            this.PSTotalmmY,
            this.PSTotalmmU,
            this.PSResultBool,
            this.PSResultDouble,
            this.PSVPPName,
            this.PSResultString});
            this.PVisionDataTable.Namespace = "VisionProRAC";
            this.PVisionDataTable.TableName = "PVisionDataTable";
            // 
            // PSName
            // 
            this.PSName.AllowDBNull = false;
            this.PSName.ColumnName = "PSName";
            this.PSName.DefaultValue = "";
            // 
            // PSPxXmmX
            // 
            this.PSPxXmmX.AllowDBNull = false;
            this.PSPxXmmX.ColumnName = "PSPxXmmX";
            this.PSPxXmmX.DataType = typeof(double);
            this.PSPxXmmX.DefaultValue = 0D;
            // 
            // PSPxXmmY
            // 
            this.PSPxXmmY.AllowDBNull = false;
            this.PSPxXmmY.ColumnName = "PSPxXmmY";
            this.PSPxXmmY.DataType = typeof(double);
            this.PSPxXmmY.DefaultValue = 0D;
            // 
            // PSTeachPxX
            // 
            this.PSTeachPxX.AllowDBNull = false;
            this.PSTeachPxX.ColumnName = "PSTeachPxX";
            this.PSTeachPxX.DataType = typeof(double);
            this.PSTeachPxX.DefaultValue = 0D;
            // 
            // PSTeachPxY
            // 
            this.PSTeachPxY.AllowDBNull = false;
            this.PSTeachPxY.ColumnName = "PSTeachPxY";
            this.PSTeachPxY.DataType = typeof(double);
            this.PSTeachPxY.DefaultValue = 0D;
            // 
            // PSTeachPxU
            // 
            this.PSTeachPxU.AllowDBNull = false;
            this.PSTeachPxU.ColumnName = "PSTeachPxU";
            this.PSTeachPxU.DataType = typeof(double);
            this.PSTeachPxU.DefaultValue = 0D;
            // 
            // PSOffsetmmX
            // 
            this.PSOffsetmmX.AllowDBNull = false;
            this.PSOffsetmmX.ColumnName = "PSOffsetmmX";
            this.PSOffsetmmX.DataType = typeof(double);
            this.PSOffsetmmX.DefaultValue = 0D;
            // 
            // PSOffsetmmY
            // 
            this.PSOffsetmmY.AllowDBNull = false;
            this.PSOffsetmmY.ColumnName = "PSOffsetmmY";
            this.PSOffsetmmY.DataType = typeof(double);
            this.PSOffsetmmY.DefaultValue = 0D;
            // 
            // PSOffsetmmU
            // 
            this.PSOffsetmmU.AllowDBNull = false;
            this.PSOffsetmmU.ColumnName = "PSOffsetmmU";
            this.PSOffsetmmU.DataType = typeof(double);
            this.PSOffsetmmU.DefaultValue = 0D;
            // 
            // PSTotalmmX
            // 
            this.PSTotalmmX.AllowDBNull = false;
            this.PSTotalmmX.ColumnName = "PSTotalmmX";
            this.PSTotalmmX.DataType = typeof(double);
            this.PSTotalmmX.DefaultValue = 0D;
            // 
            // PSTotalmmY
            // 
            this.PSTotalmmY.AllowDBNull = false;
            this.PSTotalmmY.ColumnName = "PSTotalmmY";
            this.PSTotalmmY.DataType = typeof(double);
            this.PSTotalmmY.DefaultValue = 0D;
            // 
            // PSTotalmmU
            // 
            this.PSTotalmmU.AllowDBNull = false;
            this.PSTotalmmU.ColumnName = "PSTotalmmU";
            this.PSTotalmmU.DataType = typeof(double);
            this.PSTotalmmU.DefaultValue = 0D;
            // 
            // PSResultBool
            // 
            this.PSResultBool.AllowDBNull = false;
            this.PSResultBool.ColumnName = "PSResultBool";
            this.PSResultBool.DataType = typeof(bool);
            this.PSResultBool.DefaultValue = false;
            // 
            // PSResultDouble
            // 
            this.PSResultDouble.AllowDBNull = false;
            this.PSResultDouble.ColumnName = "PSResultDouble";
            this.PSResultDouble.DataType = typeof(double);
            this.PSResultDouble.DefaultValue = 0D;
            // 
            // PSVPPName
            // 
            this.PSVPPName.AllowDBNull = false;
            this.PSVPPName.ColumnName = "PSVPPName";
            this.PSVPPName.DefaultValue = "";
            // 
            // PSResultString
            // 
            this.PSResultString.AllowDBNull = false;
            this.PSResultString.ColumnName = "PSResultString";
            this.PSResultString.DefaultValue = "";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tabControl_ProductionSettings);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // fc_In_VisionConnectionFinish
            // 
            this.fc_In_VisionConnectionFinish.AlarmCode = null;
            this.fc_In_VisionConnectionFinish.ArrowSeparation = 20;
            this.fc_In_VisionConnectionFinish.ArrowsWidth = 1;
            this.fc_In_VisionConnectionFinish.BackColor = System.Drawing.Color.White;
            this.fc_In_VisionConnectionFinish.bClearTrace = false;
            this.fc_In_VisionConnectionFinish.bIsLastFlowChart = false;
            this.fc_In_VisionConnectionFinish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_In_VisionConnectionFinish.bSkip = false;
            this.fc_In_VisionConnectionFinish.bStepByStepMode = false;
            this.fc_In_VisionConnectionFinish.CASE1 = null;
            this.fc_In_VisionConnectionFinish.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionFinish.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fc_In_VisionConnectionFinish.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionFinish.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionFinish.CASE2 = null;
            this.fc_In_VisionConnectionFinish.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionFinish.CASE2Color = System.Drawing.Color.Red;
            this.fc_In_VisionConnectionFinish.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionFinish.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionFinish.CASE3 = null;
            this.fc_In_VisionConnectionFinish.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionFinish.CASE3Color = System.Drawing.Color.Blue;
            this.fc_In_VisionConnectionFinish.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionFinish.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionFinish.CausesValidation = false;
            this.fc_In_VisionConnectionFinish.DefaultColor = System.Drawing.Color.White;
            this.fc_In_VisionConnectionFinish.eSkipPath = NPSDK.FCResultType.NEXT;
            this.fc_In_VisionConnectionFinish.ExecutedColor = System.Drawing.Color.LightGray;
            this.fc_In_VisionConnectionFinish.ExecutingColor = System.Drawing.Color.Lime;
            resources.ApplyResources(this.fc_In_VisionConnectionFinish, "fc_In_VisionConnectionFinish");
            this.fc_In_VisionConnectionFinish.menuOpening = false;
            this.fc_In_VisionConnectionFinish.Name = "fc_In_VisionConnectionFinish";
            this.fc_In_VisionConnectionFinish.NEXT = null;
            this.fc_In_VisionConnectionFinish.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionFinish.NEXTColor = System.Drawing.Color.Black;
            this.fc_In_VisionConnectionFinish.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionFinish.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionFinish.SkipColor = System.Drawing.Color.Yellow;
            this.fc_In_VisionConnectionFinish.SubFlowChart = null;
            this.fc_In_VisionConnectionFinish.TimeOut = 10000;
            this.fc_In_VisionConnectionFinish.TimeoutColor = System.Drawing.Color.Red;
            this.fc_In_VisionConnectionFinish.tmrTimeOut = null;
            this.fc_In_VisionConnectionFinish.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.fc_In_VisionConnectionFinish_FlowRun);
            // 
            // fc_In_VisionConnectionOk
            // 
            this.fc_In_VisionConnectionOk.AlarmCode = null;
            this.fc_In_VisionConnectionOk.ArrowSeparation = 20;
            this.fc_In_VisionConnectionOk.ArrowsWidth = 1;
            this.fc_In_VisionConnectionOk.BackColor = System.Drawing.Color.White;
            this.fc_In_VisionConnectionOk.bClearTrace = false;
            this.fc_In_VisionConnectionOk.bIsLastFlowChart = false;
            this.fc_In_VisionConnectionOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_In_VisionConnectionOk.bSkip = false;
            this.fc_In_VisionConnectionOk.bStepByStepMode = false;
            this.fc_In_VisionConnectionOk.CASE1 = null;
            this.fc_In_VisionConnectionOk.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionOk.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fc_In_VisionConnectionOk.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionOk.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionOk.CASE2 = null;
            this.fc_In_VisionConnectionOk.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionOk.CASE2Color = System.Drawing.Color.Red;
            this.fc_In_VisionConnectionOk.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionOk.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionOk.CASE3 = null;
            this.fc_In_VisionConnectionOk.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionOk.CASE3Color = System.Drawing.Color.Blue;
            this.fc_In_VisionConnectionOk.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionOk.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionOk.CausesValidation = false;
            this.fc_In_VisionConnectionOk.DefaultColor = System.Drawing.Color.White;
            this.fc_In_VisionConnectionOk.eSkipPath = NPSDK.FCResultType.NEXT;
            this.fc_In_VisionConnectionOk.ExecutedColor = System.Drawing.Color.LightGray;
            this.fc_In_VisionConnectionOk.ExecutingColor = System.Drawing.Color.Lime;
            resources.ApplyResources(this.fc_In_VisionConnectionOk, "fc_In_VisionConnectionOk");
            this.fc_In_VisionConnectionOk.menuOpening = false;
            this.fc_In_VisionConnectionOk.Name = "fc_In_VisionConnectionOk";
            this.fc_In_VisionConnectionOk.NEXT = this.fc_In_VisionConnectionFinish;
            this.fc_In_VisionConnectionOk.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionOk.NEXTColor = System.Drawing.Color.Black;
            this.fc_In_VisionConnectionOk.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionOk.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionOk.SkipColor = System.Drawing.Color.Yellow;
            this.fc_In_VisionConnectionOk.SubFlowChart = null;
            this.fc_In_VisionConnectionOk.TimeOut = 10000;
            this.fc_In_VisionConnectionOk.TimeoutColor = System.Drawing.Color.Red;
            this.fc_In_VisionConnectionOk.tmrTimeOut = null;
            this.fc_In_VisionConnectionOk.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.fc_In_VisionConnectionOk_FlowRun);
            // 
            // fc_In_VisionConnectionConnect
            // 
            this.fc_In_VisionConnectionConnect.AlarmCode = null;
            this.fc_In_VisionConnectionConnect.ArrowSeparation = 20;
            this.fc_In_VisionConnectionConnect.ArrowsWidth = 1;
            this.fc_In_VisionConnectionConnect.BackColor = System.Drawing.Color.White;
            this.fc_In_VisionConnectionConnect.bClearTrace = false;
            this.fc_In_VisionConnectionConnect.bIsLastFlowChart = false;
            this.fc_In_VisionConnectionConnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_In_VisionConnectionConnect.bSkip = false;
            this.fc_In_VisionConnectionConnect.bStepByStepMode = false;
            this.fc_In_VisionConnectionConnect.CASE1 = null;
            this.fc_In_VisionConnectionConnect.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionConnect.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fc_In_VisionConnectionConnect.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionConnect.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionConnect.CASE2 = null;
            this.fc_In_VisionConnectionConnect.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionConnect.CASE2Color = System.Drawing.Color.Red;
            this.fc_In_VisionConnectionConnect.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionConnect.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionConnect.CASE3 = null;
            this.fc_In_VisionConnectionConnect.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionConnect.CASE3Color = System.Drawing.Color.Blue;
            this.fc_In_VisionConnectionConnect.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionConnect.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionConnect.CausesValidation = false;
            this.fc_In_VisionConnectionConnect.DefaultColor = System.Drawing.Color.White;
            this.fc_In_VisionConnectionConnect.eSkipPath = NPSDK.FCResultType.NEXT;
            this.fc_In_VisionConnectionConnect.ExecutedColor = System.Drawing.Color.LightGray;
            this.fc_In_VisionConnectionConnect.ExecutingColor = System.Drawing.Color.Lime;
            resources.ApplyResources(this.fc_In_VisionConnectionConnect, "fc_In_VisionConnectionConnect");
            this.fc_In_VisionConnectionConnect.menuOpening = false;
            this.fc_In_VisionConnectionConnect.Name = "fc_In_VisionConnectionConnect";
            this.fc_In_VisionConnectionConnect.NEXT = this.fc_In_VisionConnectionOk;
            this.fc_In_VisionConnectionConnect.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionConnect.NEXTColor = System.Drawing.Color.Black;
            this.fc_In_VisionConnectionConnect.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionConnect.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionConnect.SkipColor = System.Drawing.Color.Yellow;
            this.fc_In_VisionConnectionConnect.SubFlowChart = null;
            this.fc_In_VisionConnectionConnect.TimeOut = 10000;
            this.fc_In_VisionConnectionConnect.TimeoutColor = System.Drawing.Color.Red;
            this.fc_In_VisionConnectionConnect.tmrTimeOut = null;
            this.fc_In_VisionConnectionConnect.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.fc_In_VisionConnectionConnect_FlowRun);
            // 
            // fc_In_VisionConnectionStart
            // 
            this.fc_In_VisionConnectionStart.AlarmCode = null;
            this.fc_In_VisionConnectionStart.ArrowSeparation = 20;
            this.fc_In_VisionConnectionStart.ArrowsWidth = 1;
            this.fc_In_VisionConnectionStart.BackColor = System.Drawing.Color.White;
            this.fc_In_VisionConnectionStart.bClearTrace = false;
            this.fc_In_VisionConnectionStart.bIsLastFlowChart = false;
            this.fc_In_VisionConnectionStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_In_VisionConnectionStart.bSkip = false;
            this.fc_In_VisionConnectionStart.bStepByStepMode = false;
            this.fc_In_VisionConnectionStart.CASE1 = this.fc_In_VisionConnectionFinish;
            this.fc_In_VisionConnectionStart.Case1ArrowType = NPSDK.NPFlowChart.ArrowTypes.Square;
            this.fc_In_VisionConnectionStart.CASE1Color = System.Drawing.Color.LimeGreen;
            this.fc_In_VisionConnectionStart.CASE1EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionStart.Case1StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionStart.CASE2 = null;
            this.fc_In_VisionConnectionStart.Case2ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionStart.CASE2Color = System.Drawing.Color.Red;
            this.fc_In_VisionConnectionStart.CASE2EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionStart.Case2StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionStart.CASE3 = null;
            this.fc_In_VisionConnectionStart.Case3ArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionStart.CASE3Color = System.Drawing.Color.Blue;
            this.fc_In_VisionConnectionStart.CASE3EndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionStart.Case3StartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionStart.CausesValidation = false;
            this.fc_In_VisionConnectionStart.DefaultColor = System.Drawing.Color.White;
            this.fc_In_VisionConnectionStart.eSkipPath = NPSDK.FCResultType.NEXT;
            this.fc_In_VisionConnectionStart.ExecutedColor = System.Drawing.Color.LightGray;
            this.fc_In_VisionConnectionStart.ExecutingColor = System.Drawing.Color.Lime;
            resources.ApplyResources(this.fc_In_VisionConnectionStart, "fc_In_VisionConnectionStart");
            this.fc_In_VisionConnectionStart.menuOpening = false;
            this.fc_In_VisionConnectionStart.Name = "fc_In_VisionConnectionStart";
            this.fc_In_VisionConnectionStart.NEXT = this.fc_In_VisionConnectionConnect;
            this.fc_In_VisionConnectionStart.NextArrowType = NPSDK.NPFlowChart.ArrowTypes.Line;
            this.fc_In_VisionConnectionStart.NEXTColor = System.Drawing.Color.Black;
            this.fc_In_VisionConnectionStart.NEXTEndPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionStart.NEXTStartPoint = NPSDK.NPFlowChart.FlowchartSides.Right;
            this.fc_In_VisionConnectionStart.SkipColor = System.Drawing.Color.Yellow;
            this.fc_In_VisionConnectionStart.SubFlowChart = null;
            this.fc_In_VisionConnectionStart.TimeOut = 10000;
            this.fc_In_VisionConnectionStart.TimeoutColor = System.Drawing.Color.Red;
            this.fc_In_VisionConnectionStart.tmrTimeOut = null;
            this.fc_In_VisionConnectionStart.FlowRun += new NPSDK.NPFlowChart.FlowRunEvent(this.fc_In_VisionConnectionStart_FlowRun);
            // 
            // Calib_Timer
            // 
            this.Calib_Timer.Tick += new System.EventHandler(this.Calib_Timer_Tick);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Name = "label7";
            // 
            // EnableVisionGantry
            // 
            this.EnableVisionGantry.Caption = "EnableVisionGantry";
            this.EnableVisionGantry.ColumnName = "EnableVisionGantry";
            this.EnableVisionGantry.DataType = typeof(bool);
            this.EnableVisionGantry.DefaultValue = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Name = "label1";
            this.label1.Tag = "";
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.btnUPLightWhiteOff);
            this.panel16.Controls.Add(this.btnUPLightWhiteOn);
            this.panel16.Controls.Add(this.btnUPLightBlueOn);
            this.panel16.Controls.Add(this.btnUPLightGreenOn);
            this.panel16.Controls.Add(this.btnUPLightRedOn);
            resources.ApplyResources(this.panel16, "panel16");
            this.panel16.Name = "panel16";
            // 
            // btnUPLightWhiteOff
            // 
            resources.ApplyResources(this.btnUPLightWhiteOff, "btnUPLightWhiteOff");
            this.btnUPLightWhiteOff.Name = "btnUPLightWhiteOff";
            this.btnUPLightWhiteOff.UseVisualStyleBackColor = true;
            this.btnUPLightWhiteOff.Click += new System.EventHandler(this.btnUPLightWhiteOff_Click);
            // 
            // btnUPLightWhiteOn
            // 
            resources.ApplyResources(this.btnUPLightWhiteOn, "btnUPLightWhiteOn");
            this.btnUPLightWhiteOn.Name = "btnUPLightWhiteOn";
            this.btnUPLightWhiteOn.UseVisualStyleBackColor = true;
            this.btnUPLightWhiteOn.Click += new System.EventHandler(this.btnUPLightWhiteOn_Click);
            // 
            // btnUPLightBlueOn
            // 
            resources.ApplyResources(this.btnUPLightBlueOn, "btnUPLightBlueOn");
            this.btnUPLightBlueOn.Name = "btnUPLightBlueOn";
            this.btnUPLightBlueOn.UseVisualStyleBackColor = true;
            this.btnUPLightBlueOn.Click += new System.EventHandler(this.btnUPLightBlueOn_Click);
            // 
            // btnUPLightGreenOn
            // 
            resources.ApplyResources(this.btnUPLightGreenOn, "btnUPLightGreenOn");
            this.btnUPLightGreenOn.Name = "btnUPLightGreenOn";
            this.btnUPLightGreenOn.UseVisualStyleBackColor = true;
            this.btnUPLightGreenOn.Click += new System.EventHandler(this.btnUPLightGreenOn_Click);
            // 
            // btnUPLightRedOn
            // 
            resources.ApplyResources(this.btnUPLightRedOn, "btnUPLightRedOn");
            this.btnUPLightRedOn.Name = "btnUPLightRedOn";
            this.btnUPLightRedOn.UseVisualStyleBackColor = true;
            this.btnUPLightRedOn.Click += new System.EventHandler(this.btnLightOn_Click);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.panel16);
            this.panel8.Controls.Add(this.label1);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // panel17
            // 
            this.panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel17.Controls.Add(this.panel18);
            this.panel17.Controls.Add(this.label2);
            resources.ApplyResources(this.panel17, "panel17");
            this.panel17.Name = "panel17";
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.btnDWLightWhiteOff);
            this.panel18.Controls.Add(this.btnDWLightWhiteOn);
            this.panel18.Controls.Add(this.btnDWLightBlueOn);
            this.panel18.Controls.Add(this.btnDWLightGreenOn);
            this.panel18.Controls.Add(this.btnDWLightRedOn);
            resources.ApplyResources(this.panel18, "panel18");
            this.panel18.Name = "panel18";
            // 
            // btnDWLightWhiteOff
            // 
            resources.ApplyResources(this.btnDWLightWhiteOff, "btnDWLightWhiteOff");
            this.btnDWLightWhiteOff.Name = "btnDWLightWhiteOff";
            this.btnDWLightWhiteOff.UseVisualStyleBackColor = true;
            this.btnDWLightWhiteOff.Click += new System.EventHandler(this.btnDWLightWhiteOff_Click);
            // 
            // btnDWLightWhiteOn
            // 
            resources.ApplyResources(this.btnDWLightWhiteOn, "btnDWLightWhiteOn");
            this.btnDWLightWhiteOn.Name = "btnDWLightWhiteOn";
            this.btnDWLightWhiteOn.UseVisualStyleBackColor = true;
            this.btnDWLightWhiteOn.Click += new System.EventHandler(this.btnDWLightWhiteOn_Click);
            // 
            // btnDWLightBlueOn
            // 
            resources.ApplyResources(this.btnDWLightBlueOn, "btnDWLightBlueOn");
            this.btnDWLightBlueOn.Name = "btnDWLightBlueOn";
            this.btnDWLightBlueOn.UseVisualStyleBackColor = true;
            this.btnDWLightBlueOn.Click += new System.EventHandler(this.btnDWLightBlueOn_Click);
            // 
            // btnDWLightGreenOn
            // 
            resources.ApplyResources(this.btnDWLightGreenOn, "btnDWLightGreenOn");
            this.btnDWLightGreenOn.Name = "btnDWLightGreenOn";
            this.btnDWLightGreenOn.UseVisualStyleBackColor = true;
            this.btnDWLightGreenOn.Click += new System.EventHandler(this.btnDWLightGreenOn_Click);
            // 
            // btnDWLightRedOn
            // 
            resources.ApplyResources(this.btnDWLightRedOn, "btnDWLightRedOn");
            this.btnDWLightRedOn.Name = "btnDWLightRedOn";
            this.btnDWLightRedOn.UseVisualStyleBackColor = true;
            this.btnDWLightRedOn.Click += new System.EventHandler(this.btnDWLightRedOn_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Name = "label2";
            this.label2.Tag = "";
            // 
            // VisionProRAC
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "VisionProRAC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.plMaintenance.ResumeLayout(false);
            this.plProductionSetting.ResumeLayout(false);
            this.plRecipeEditor.ResumeLayout(false);
            this.plFlowInitial.ResumeLayout(false);
            this.plMotionSetup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RVisionDataTable)).EndInit();
            this.Image.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_RecipeEditorImage.ResumeLayout(false);
            this.panel_Image.ResumeLayout(false);
            this.panel33.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel47.ResumeLayout(false);
            this.panel32.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPosInspection)).EndInit();
            this.tabControl_Recipe.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCameraSettings)).EndInit();
            this.tabControl_ProductionSettings.ResumeLayout(false);
            this.tabPage_VPROSettings.ResumeLayout(false);
            this.tabPage_VPROSettings.PerformLayout();
            this.tabPage_CameraRegister.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CameraSettings)).EndInit();
            this.tabPage_StaticPrograms.ResumeLayout(false);
            this.tableLayoutPanel_StaticPrograms.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_VisionData)).EndInit();
            this.tabPage_CameraTeach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogAcqFifoEditV21)).EndInit();
            this.tabPage_ImageSetup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogIPOneImageEditV21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PVisionDataTable)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Data.DataColumn EnableVision;
        private System.ComponentModel.BackgroundWorker Ctrl_CamLiveCaptureWorker;
        private System.Data.DataColumn PxXmmX;
        private System.Data.DataColumn PxXmmY;
        private System.Data.DataColumn TeachPxX;
        private System.Data.DataColumn TeachPxY;
        private System.Data.DataColumn TeachPxU;
        private System.Data.DataColumn OffsetmmX;
        private System.Data.DataColumn OffsetmmY;
        private System.Data.DataColumn OffsetmmU;
        private System.Data.DataColumn TotalmmX;
        private System.Data.DataColumn TotalmmY;
        private System.Data.DataColumn TotalmmU;
        private System.Data.DataColumn ResultDouble;
        private System.Data.DataColumn VPPName;
        private System.Windows.Forms.TabControl tabControl_Recipe;
        private System.Windows.Forms.TabPage Image;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_Image;
        private System.Windows.Forms.Panel panel33;
        private System.Windows.Forms.Label lb_VPPName;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataGridView dgvPosInspection;
        private System.Windows.Forms.Button btnExVision;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Recipe_Program;
        private System.Windows.Forms.ComboBox cbSelectProcess;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Label label_Recipe_Light;
        public System.Windows.Forms.ComboBox cbLights;
        private System.Windows.Forms.Button btnEditVPRO;
        private System.Windows.Forms.Button btnGrabImage;
        private System.Windows.Forms.Button btnLiveImage;
        private System.Windows.Forms.Button btnTeachSelectedProgram;
        private System.Windows.Forms.Panel panel25;
        public System.Windows.Forms.Label lbInspectionU;
        private System.Windows.Forms.Label label_Recipe_DegU;
        private System.Windows.Forms.Panel panel32;
        public System.Windows.Forms.Label lbInspectionY;
        private System.Windows.Forms.Label label_Recipe_PixelsY;
        private System.Windows.Forms.Panel panel47;
        public System.Windows.Forms.Label lbInspectionX;
        private System.Windows.Forms.Label label_Recipe_PixelsX;
        private System.Data.DataTable PCameraSettings;
        private System.Data.DataColumn CameraVPPName;
        private System.Data.DataColumn ImageToolVPPName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox cmbCamera;
		private System.Windows.Forms.ToolTip tTip;
		private System.Windows.Forms.Panel panel_RecipeEditorImage;
		private System.Windows.Forms.Button btnReloadTool;
        public System.Data.DataTable RVisionDataTable;
        public System.Data.DataColumn PName;
        public System.Data.DataColumn ResultString;
        public System.Data.DataColumn ResultBool;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label labelImagePath;
        public System.Windows.Forms.TextBox textLocalPath;
        private System.Windows.Forms.Button buttonLocalPath;
        private System.Data.DataColumn ImageSavePath;
        private System.Windows.Forms.TabControl tabControl_ProductionSettings;
        private System.Windows.Forms.TabPage tabPage_CameraRegister;
        private System.Windows.Forms.TabPage tabPage_CameraTeach;
        private System.Windows.Forms.TabPage tabPage_ImageSetup;
        private System.Windows.Forms.TabPage tabPage_VPROSettings;
        private Cognex.VisionPro.CogAcqFifoEditV2 cogAcqFifoEditV21;
        private Cognex.VisionPro.ImageProcessing.CogIPOneImageEditV2 cogIPOneImageEditV21;
        private System.Windows.Forms.TabPage tabPage_StaticPrograms;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_StaticPrograms;
        private System.Windows.Forms.Panel panel_ProductionSettingImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label_Production_Light;
        public System.Windows.Forms.ComboBox comboBox_Production_Light;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label_Production_Program;
        private System.Windows.Forms.ComboBox comboBox_Production_Program;
        private System.Windows.Forms.Button button_Production_RunVision;
        private System.Windows.Forms.Panel panel12;
        public System.Windows.Forms.Label label_Production_ResultPixelsX;
        private System.Windows.Forms.Label label_Production_PixelsX;
        private System.Windows.Forms.Button button_Production_TeachSelected;
        private System.Windows.Forms.Panel panel13;
        public System.Windows.Forms.Label label_Production_ResultPixelsY;
        private System.Windows.Forms.Label label_Production_PixelsY;
        private System.Windows.Forms.Panel panel14;
        public System.Windows.Forms.Label label_Production_ResultDegU;
        private System.Windows.Forms.Label label_Production_DegU;
        private System.Windows.Forms.Panel panel15;
        private System.Data.DataTable PVisionDataTable;
        private System.Windows.Forms.DataGridView dataGridView_VisionData;
        private System.Data.DataColumn PSName;
        private System.Data.DataColumn PSPxXmmX;
        private System.Data.DataColumn PSPxXmmY;
        private System.Data.DataColumn PSTeachPxX;
        private System.Data.DataColumn PSTeachPxY;
        private System.Data.DataColumn PSTeachPxU;
        private System.Data.DataColumn PSOffsetmmX;
        private System.Data.DataColumn PSOffsetmmY;
        private System.Data.DataColumn PSOffsetmmU;
        private System.Data.DataColumn PSTotalmmX;
        private System.Data.DataColumn PSTotalmmY;
        private System.Data.DataColumn PSTotalmmU;
        private System.Data.DataColumn PSResultBool;
        private System.Data.DataColumn PSResultDouble;
        private System.Data.DataColumn PSVPPName;
        private System.Data.DataColumn PSResultString;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label_ProductionVPPName;
        private System.Windows.Forms.DataGridView dataGridView_CameraSettings;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSVPPNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSTotalmmXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSTotalmmYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSTotalmmUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn pSResultBoolDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSResultDoubleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSResultStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSPxXmmXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSPxXmmYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSTeachPxXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSTeachPxYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSTeachPxUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSOffsetmmXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSOffsetmmYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pSOffsetmmUDataGridViewTextBoxColumn;
        public System.Data.DataColumn c_VisionEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn pNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vPPNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalmmXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalmmYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalmmUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn resultBoolDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDoubleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pxXmmXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pxXmmYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn teachPxXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn teachPxYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn teachPxUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetmmXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetmmYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetmmUDataGridViewTextBoxColumn;
        private NPSDK.NPFlowChart fc_In_VisionConnectionFinish;
        private NPSDK.NPFlowChart fc_In_VisionConnectionOk;
        private NPSDK.NPFlowChart fc_In_VisionConnectionConnect;
        private NPSDK.NPFlowChart fc_In_VisionConnectionStart;
        public System.Windows.Forms.Timer Calib_Timer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Data.DataColumn EnableVisionGantry;
        public System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button btnUPLightRedOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDWLightWhiteOff;
        private System.Windows.Forms.Button btnDWLightWhiteOn;
        private System.Windows.Forms.Button btnDWLightBlueOn;
        private System.Windows.Forms.Button btnDWLightGreenOn;
        private System.Windows.Forms.Button btnDWLightRedOn;
        private System.Windows.Forms.Button btnUPLightWhiteOff;
        private System.Windows.Forms.Button btnUPLightWhiteOn;
        private System.Windows.Forms.Button btnUPLightBlueOn;
        private System.Windows.Forms.Button btnUPLightGreenOn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cameraVPPNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imageToolVPPNameDataGridViewTextBoxColumn;
    }
}
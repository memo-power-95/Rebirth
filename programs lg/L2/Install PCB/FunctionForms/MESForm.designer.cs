namespace Alpha
{
    partial class MESForm
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
            this.MESData = new System.Data.DataSet();
            this.dt_MESTable = new System.Data.DataTable();
            this.dcMESTable_CustomerName = new System.Data.DataColumn();
            this.dcMESTable_Division = new System.Data.DataColumn();
            this.dcMESTable_AssemblyNumber = new System.Data.DataColumn();
            this.dcMESTable_StationName = new System.Data.DataColumn();
            this.dcMESTable_AssemblyRev = new System.Data.DataColumn();
            this.dcMESTable_ProcessStep = new System.Data.DataColumn();
            this.dcMESTable_NTUser = new System.Data.DataColumn();
            this.dcMESTable_Site = new System.Data.DataColumn();
            this.dcMESTable_UnitSavePath = new System.Data.DataColumn();
            this.dcMESTable_UnitSaveBackupPath = new System.Data.DataColumn();
            this.dcMESTable_GenerateLocalTar = new System.Data.DataColumn();
            this.dcMESTable_GenerateExternalTar = new System.Data.DataColumn();
            this.dcMESTable_SerialNumber = new System.Data.DataColumn();
            this.dcMESTable_CheckPoint = new System.Data.DataColumn();
            this.dcMESTable_UseWebservice = new System.Data.DataColumn();
            this.dt_SerialInfo = new System.Data.DataTable();
            this.dcMESTable_ValidateDuplicateSerialNumbers = new System.Data.DataColumn();
            this.dcMESTable_LogPath = new System.Data.DataColumn();
            this.dcSerialInfo_ConsecutiveNumber = new System.Data.DataColumn();
            this.dcSerialInfo_UniquePartNumber = new System.Data.DataColumn();
            this.dcSerialInfo_AutomaticBirthPath = new System.Data.DataColumn();
            this.dcSerialInfo_BackupBirthPath = new System.Data.DataColumn();
            this.dcSerialInfo_GenerateBackupBirth = new System.Data.DataColumn();
            this.dcSerialInfo_GenerateAutomaticBirth = new System.Data.DataColumn();
            this.dcSerialInfo_Static = new System.Data.DataColumn();
            this.dcSerialInfo_SiteCode = new System.Data.DataColumn();
            this.dcSerialInfo_CurrentJulianDay = new System.Data.DataColumn();
            this.dt_BatchUnits = new System.Data.DataTable();
            this.dc_CounterBatchUnits = new System.Data.DataColumn();
            this.dc_LimitBatchUnits = new System.Data.DataColumn();
            this.buttonExternalPath = new System.Windows.Forms.Button();
            this.buttonLocalPath = new System.Windows.Forms.Button();
            this.textExternalPath = new System.Windows.Forms.TextBox();
            this.textLocalPath = new System.Windows.Forms.TextBox();
            this.labelExternalPath = new System.Windows.Forms.Label();
            this.labelLocalPath = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textSerialNumber = new System.Windows.Forms.TextBox();
            this.label1SerialNumber = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.labelSite = new System.Windows.Forms.Label();
            this.textSite = new System.Windows.Forms.TextBox();
            this.textCustomer = new System.Windows.Forms.TextBox();
            this.labelCustomerName = new System.Windows.Forms.Label();
            this.textDivision = new System.Windows.Forms.TextBox();
            this.labelDivision = new System.Windows.Forms.Label();
            this.textAssemblyNumber = new System.Windows.Forms.TextBox();
            this.labelAssemblyNumber = new System.Windows.Forms.Label();
            this.textStationName = new System.Windows.Forms.TextBox();
            this.labelProcessStep = new System.Windows.Forms.Label();
            this.labelStationName = new System.Windows.Forms.Label();
            this.textProcessStep = new System.Windows.Forms.TextBox();
            this.textAssemblyRev = new System.Windows.Forms.TextBox();
            this.labelAssemblyRev = new System.Windows.Forms.Label();
            this.labelNTUser = new System.Windows.Forms.Label();
            this.textNTUser = new System.Windows.Forms.TextBox();
            this.chkGenerateLocalTar = new System.Windows.Forms.CheckBox();
            this.chkGenerateExternalTar = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tc_MES = new System.Windows.Forms.TabControl();
            this.tp_MES = new System.Windows.Forms.TabPage();
            this.button_TestTarGeneration = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MESData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_MESTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_SerialInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_BatchUnits)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tc_MES.SuspendLayout();
            this.tp_MES.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // MESData
            // 
            this.MESData.DataSetName = "NewDataSet";
            this.MESData.Tables.AddRange(new System.Data.DataTable[] {
            this.dt_MESTable,
            this.dt_SerialInfo,
            this.dt_BatchUnits});
            // 
            // dt_MESTable
            // 
            this.dt_MESTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcMESTable_CustomerName,
            this.dcMESTable_Division,
            this.dcMESTable_AssemblyNumber,
            this.dcMESTable_StationName,
            this.dcMESTable_AssemblyRev,
            this.dcMESTable_ProcessStep,
            this.dcMESTable_NTUser,
            this.dcMESTable_Site,
            this.dcMESTable_UnitSavePath,
            this.dcMESTable_UnitSaveBackupPath,
            this.dcMESTable_GenerateLocalTar,
            this.dcMESTable_GenerateExternalTar,
            this.dcMESTable_SerialNumber,
            this.dcMESTable_CheckPoint,
            this.dcMESTable_UseWebservice});
            this.dt_MESTable.Namespace = "MES";
            this.dt_MESTable.TableName = "MESTable";
            // 
            // dcMESTable_CustomerName
            // 
            this.dcMESTable_CustomerName.ColumnName = "CustomerName";
            this.dcMESTable_CustomerName.MaxLength = 30;
            // 
            // dcMESTable_Division
            // 
            this.dcMESTable_Division.Caption = "Division";
            this.dcMESTable_Division.ColumnName = "Division";
            this.dcMESTable_Division.MaxLength = 30;
            // 
            // dcMESTable_AssemblyNumber
            // 
            this.dcMESTable_AssemblyNumber.ColumnName = "AssemblyNumber";
            this.dcMESTable_AssemblyNumber.MaxLength = 30;
            // 
            // dcMESTable_StationName
            // 
            this.dcMESTable_StationName.ColumnName = "StationName";
            this.dcMESTable_StationName.MaxLength = 30;
            // 
            // dcMESTable_AssemblyRev
            // 
            this.dcMESTable_AssemblyRev.ColumnName = "AssemblyRev";
            this.dcMESTable_AssemblyRev.MaxLength = 30;
            // 
            // dcMESTable_ProcessStep
            // 
            this.dcMESTable_ProcessStep.ColumnName = "ProcessStep";
            this.dcMESTable_ProcessStep.MaxLength = 30;
            // 
            // dcMESTable_NTUser
            // 
            this.dcMESTable_NTUser.ColumnName = "NTUser";
            this.dcMESTable_NTUser.MaxLength = 30;
            // 
            // dcMESTable_Site
            // 
            this.dcMESTable_Site.ColumnName = "Site";
            this.dcMESTable_Site.MaxLength = 30;
            // 
            // dcMESTable_UnitSavePath
            // 
            this.dcMESTable_UnitSavePath.AllowDBNull = false;
            this.dcMESTable_UnitSavePath.ColumnName = "UnitSavePath";
            this.dcMESTable_UnitSavePath.DefaultValue = "";
            // 
            // dcMESTable_UnitSaveBackupPath
            // 
            this.dcMESTable_UnitSaveBackupPath.AllowDBNull = false;
            this.dcMESTable_UnitSaveBackupPath.ColumnName = "UnitSaveBackupPath";
            this.dcMESTable_UnitSaveBackupPath.DefaultValue = "";
            // 
            // dcMESTable_GenerateLocalTar
            // 
            this.dcMESTable_GenerateLocalTar.AllowDBNull = false;
            this.dcMESTable_GenerateLocalTar.ColumnName = "GenerateLocalTar";
            this.dcMESTable_GenerateLocalTar.DataType = typeof(bool);
            this.dcMESTable_GenerateLocalTar.DefaultValue = true;
            // 
            // dcMESTable_GenerateExternalTar
            // 
            this.dcMESTable_GenerateExternalTar.AllowDBNull = false;
            this.dcMESTable_GenerateExternalTar.ColumnName = "GenerateExternalTar";
            this.dcMESTable_GenerateExternalTar.DataType = typeof(bool);
            this.dcMESTable_GenerateExternalTar.DefaultValue = false;
            // 
            // dcMESTable_SerialNumber
            // 
            this.dcMESTable_SerialNumber.AllowDBNull = false;
            this.dcMESTable_SerialNumber.ColumnName = "SerialNumber";
            this.dcMESTable_SerialNumber.DefaultValue = "";
            // 
            // dcMESTable_CheckPoint
            // 
            this.dcMESTable_CheckPoint.AllowDBNull = false;
            this.dcMESTable_CheckPoint.ColumnName = "CheckPoint";
            this.dcMESTable_CheckPoint.DefaultValue = "";
            // 
            // dcMESTable_UseWebservice
            // 
            this.dcMESTable_UseWebservice.AllowDBNull = false;
            this.dcMESTable_UseWebservice.ColumnName = "UseWebservice";
            this.dcMESTable_UseWebservice.DataType = typeof(bool);
            this.dcMESTable_UseWebservice.DefaultValue = false;
            // 
            // dt_SerialInfo
            // 
            this.dt_SerialInfo.Columns.AddRange(new System.Data.DataColumn[] {
            this.dcMESTable_ValidateDuplicateSerialNumbers,
            this.dcMESTable_LogPath,
            this.dcSerialInfo_ConsecutiveNumber,
            this.dcSerialInfo_UniquePartNumber,
            this.dcSerialInfo_AutomaticBirthPath,
            this.dcSerialInfo_BackupBirthPath,
            this.dcSerialInfo_GenerateBackupBirth,
            this.dcSerialInfo_GenerateAutomaticBirth,
            this.dcSerialInfo_Static,
            this.dcSerialInfo_SiteCode,
            this.dcSerialInfo_CurrentJulianDay});
            this.dt_SerialInfo.Namespace = "MES";
            this.dt_SerialInfo.TableName = "SerialInfo";
            // 
            // dcMESTable_ValidateDuplicateSerialNumbers
            // 
            this.dcMESTable_ValidateDuplicateSerialNumbers.AllowDBNull = false;
            this.dcMESTable_ValidateDuplicateSerialNumbers.ColumnName = "ValidateDuplicateSerialNumbers";
            this.dcMESTable_ValidateDuplicateSerialNumbers.DataType = typeof(bool);
            this.dcMESTable_ValidateDuplicateSerialNumbers.DefaultValue = true;
            // 
            // dcMESTable_LogPath
            // 
            this.dcMESTable_LogPath.AllowDBNull = false;
            this.dcMESTable_LogPath.ColumnName = "LogPath";
            this.dcMESTable_LogPath.DefaultValue = "";
            // 
            // dcSerialInfo_ConsecutiveNumber
            // 
            this.dcSerialInfo_ConsecutiveNumber.AllowDBNull = false;
            this.dcSerialInfo_ConsecutiveNumber.ColumnName = "ConsecutiveNumber";
            this.dcSerialInfo_ConsecutiveNumber.DataType = typeof(int);
            this.dcSerialInfo_ConsecutiveNumber.DefaultValue = 0;
            // 
            // dcSerialInfo_UniquePartNumber
            // 
            this.dcSerialInfo_UniquePartNumber.AllowDBNull = false;
            this.dcSerialInfo_UniquePartNumber.ColumnName = "Unique P/N";
            this.dcSerialInfo_UniquePartNumber.DefaultValue = "0";
            // 
            // dcSerialInfo_AutomaticBirthPath
            // 
            this.dcSerialInfo_AutomaticBirthPath.AllowDBNull = false;
            this.dcSerialInfo_AutomaticBirthPath.ColumnName = "AutomaticBirthPath";
            this.dcSerialInfo_AutomaticBirthPath.DefaultValue = "";
            // 
            // dcSerialInfo_BackupBirthPath
            // 
            this.dcSerialInfo_BackupBirthPath.AllowDBNull = false;
            this.dcSerialInfo_BackupBirthPath.ColumnName = "BackupBirthPath";
            this.dcSerialInfo_BackupBirthPath.DefaultValue = "";
            // 
            // dcSerialInfo_GenerateBackupBirth
            // 
            this.dcSerialInfo_GenerateBackupBirth.AllowDBNull = false;
            this.dcSerialInfo_GenerateBackupBirth.ColumnName = "GenerateBackupBirth";
            this.dcSerialInfo_GenerateBackupBirth.DataType = typeof(bool);
            this.dcSerialInfo_GenerateBackupBirth.DefaultValue = false;
            // 
            // dcSerialInfo_GenerateAutomaticBirth
            // 
            this.dcSerialInfo_GenerateAutomaticBirth.AllowDBNull = false;
            this.dcSerialInfo_GenerateAutomaticBirth.ColumnName = "GenerateAutomaticBirth";
            this.dcSerialInfo_GenerateAutomaticBirth.DataType = typeof(bool);
            this.dcSerialInfo_GenerateAutomaticBirth.DefaultValue = false;
            // 
            // dcSerialInfo_Static
            // 
            this.dcSerialInfo_Static.AllowDBNull = false;
            this.dcSerialInfo_Static.ColumnName = "Static";
            this.dcSerialInfo_Static.DefaultValue = "";
            // 
            // dcSerialInfo_SiteCode
            // 
            this.dcSerialInfo_SiteCode.AllowDBNull = false;
            this.dcSerialInfo_SiteCode.ColumnName = "SiteCode";
            this.dcSerialInfo_SiteCode.DefaultValue = "";
            // 
            // dcSerialInfo_CurrentJulianDay
            // 
            this.dcSerialInfo_CurrentJulianDay.AllowDBNull = false;
            this.dcSerialInfo_CurrentJulianDay.ColumnName = "CurrentJulianDay";
            this.dcSerialInfo_CurrentJulianDay.DataType = typeof(int);
            this.dcSerialInfo_CurrentJulianDay.DefaultValue = 0;
            // 
            // dt_BatchUnits
            // 
            this.dt_BatchUnits.Columns.AddRange(new System.Data.DataColumn[] {
            this.dc_CounterBatchUnits,
            this.dc_LimitBatchUnits});
            this.dt_BatchUnits.TableName = "BatchUnits";
            // 
            // dc_CounterBatchUnits
            // 
            this.dc_CounterBatchUnits.AllowDBNull = false;
            this.dc_CounterBatchUnits.ColumnName = "CounterBatchUnits";
            this.dc_CounterBatchUnits.DataType = typeof(int);
            this.dc_CounterBatchUnits.DefaultValue = 0;
            // 
            // dc_LimitBatchUnits
            // 
            this.dc_LimitBatchUnits.AllowDBNull = false;
            this.dc_LimitBatchUnits.ColumnName = "LimitBatchUnits";
            this.dc_LimitBatchUnits.DataType = typeof(int);
            this.dc_LimitBatchUnits.DefaultValue = 100;
            // 
            // buttonExternalPath
            // 
            this.buttonExternalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonExternalPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.buttonExternalPath.FlatAppearance.BorderSize = 2;
            this.buttonExternalPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonExternalPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.buttonExternalPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExternalPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExternalPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.buttonExternalPath.Location = new System.Drawing.Point(464, 30);
            this.buttonExternalPath.Margin = new System.Windows.Forms.Padding(0);
            this.buttonExternalPath.Name = "buttonExternalPath";
            this.buttonExternalPath.Size = new System.Drawing.Size(130, 30);
            this.buttonExternalPath.TabIndex = 137;
            this.buttonExternalPath.Text = "Seleccionar";
            this.buttonExternalPath.UseVisualStyleBackColor = true;
            this.buttonExternalPath.Click += new System.EventHandler(this.buttonExternalPath_Click);
            // 
            // buttonLocalPath
            // 
            this.buttonLocalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLocalPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.buttonLocalPath.FlatAppearance.BorderSize = 2;
            this.buttonLocalPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonLocalPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.buttonLocalPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLocalPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLocalPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.buttonLocalPath.Location = new System.Drawing.Point(464, 0);
            this.buttonLocalPath.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLocalPath.Name = "buttonLocalPath";
            this.buttonLocalPath.Size = new System.Drawing.Size(130, 30);
            this.buttonLocalPath.TabIndex = 136;
            this.buttonLocalPath.Text = "Seleccionar";
            this.buttonLocalPath.UseVisualStyleBackColor = true;
            this.buttonLocalPath.Click += new System.EventHandler(this.buttonLocalPath_Click);
            // 
            // textExternalPath
            // 
            this.textExternalPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.UnitSavePath", true));
            this.textExternalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textExternalPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textExternalPath.Location = new System.Drawing.Point(100, 30);
            this.textExternalPath.Margin = new System.Windows.Forms.Padding(0);
            this.textExternalPath.MaxLength = 260;
            this.textExternalPath.Multiline = true;
            this.textExternalPath.Name = "textExternalPath";
            this.textExternalPath.Size = new System.Drawing.Size(364, 30);
            this.textExternalPath.TabIndex = 135;
            // 
            // textLocalPath
            // 
            this.textLocalPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.UnitSaveBackupPath", true));
            this.textLocalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLocalPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLocalPath.Location = new System.Drawing.Point(100, 0);
            this.textLocalPath.Margin = new System.Windows.Forms.Padding(0);
            this.textLocalPath.MaxLength = 260;
            this.textLocalPath.Multiline = true;
            this.textLocalPath.Name = "textLocalPath";
            this.textLocalPath.Size = new System.Drawing.Size(364, 30);
            this.textLocalPath.TabIndex = 134;
            // 
            // labelExternalPath
            // 
            this.labelExternalPath.AutoSize = true;
            this.labelExternalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelExternalPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExternalPath.Location = new System.Drawing.Point(0, 30);
            this.labelExternalPath.Margin = new System.Windows.Forms.Padding(0);
            this.labelExternalPath.Name = "labelExternalPath";
            this.labelExternalPath.Size = new System.Drawing.Size(100, 30);
            this.labelExternalPath.TabIndex = 133;
            this.labelExternalPath.Text = "Ruta Externa";
            this.labelExternalPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelLocalPath
            // 
            this.labelLocalPath.AutoSize = true;
            this.labelLocalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLocalPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocalPath.Location = new System.Drawing.Point(0, 0);
            this.labelLocalPath.Margin = new System.Windows.Forms.Padding(0);
            this.labelLocalPath.Name = "labelLocalPath";
            this.labelLocalPath.Size = new System.Drawing.Size(100, 30);
            this.labelLocalPath.TabIndex = 132;
            this.labelLocalPath.Text = "Ruta Local";
            this.labelLocalPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(216, 1);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(26, 30);
            this.label17.TabIndex = 167;
            this.label17.Text = "S";
            // 
            // textSerialNumber
            // 
            this.textSerialNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.SerialNumber", true));
            this.textSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textSerialNumber.Location = new System.Drawing.Point(243, 1);
            this.textSerialNumber.Margin = new System.Windows.Forms.Padding(0);
            this.textSerialNumber.MaxLength = 30;
            this.textSerialNumber.Multiline = true;
            this.textSerialNumber.Name = "textSerialNumber";
            this.textSerialNumber.Size = new System.Drawing.Size(500, 30);
            this.textSerialNumber.TabIndex = 166;
            // 
            // label1SerialNumber
            // 
            this.label1SerialNumber.AutoSize = true;
            this.label1SerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1SerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label1SerialNumber.Location = new System.Drawing.Point(1, 1);
            this.label1SerialNumber.Margin = new System.Windows.Forms.Padding(0);
            this.label1SerialNumber.Name = "label1SerialNumber";
            this.label1SerialNumber.Size = new System.Drawing.Size(214, 30);
            this.label1SerialNumber.TabIndex = 165;
            this.label1SerialNumber.Text = "Serial Number";
            this.label1SerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(216, 218);
            this.label19.Margin = new System.Windows.Forms.Padding(0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(26, 30);
            this.label19.TabIndex = 164;
            this.label19.Text = "O";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Blue;
            this.label20.Location = new System.Drawing.Point(216, 187);
            this.label20.Margin = new System.Windows.Forms.Padding(0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(26, 30);
            this.label20.TabIndex = 163;
            this.label20.Text = "r";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Blue;
            this.label21.Location = new System.Drawing.Point(216, 249);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(26, 30);
            this.label21.TabIndex = 157;
            this.label21.Text = "p";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Blue;
            this.label22.Location = new System.Drawing.Point(216, 156);
            this.label22.Margin = new System.Windows.Forms.Padding(0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(26, 30);
            this.label22.TabIndex = 162;
            this.label22.Text = "n";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Blue;
            this.label23.Location = new System.Drawing.Point(216, 125);
            this.label23.Margin = new System.Windows.Forms.Padding(0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(26, 30);
            this.label23.TabIndex = 161;
            this.label23.Text = "P";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Blue;
            this.label24.Location = new System.Drawing.Point(216, 94);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(26, 30);
            this.label24.TabIndex = 160;
            this.label24.Text = "N";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Blue;
            this.label25.Location = new System.Drawing.Point(216, 63);
            this.label25.Margin = new System.Windows.Forms.Padding(0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(26, 30);
            this.label25.TabIndex = 159;
            this.label25.Text = "I";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Blue;
            this.label26.Location = new System.Drawing.Point(216, 32);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(26, 30);
            this.label26.TabIndex = 158;
            this.label26.Text = "C";
            // 
            // labelSite
            // 
            this.labelSite.AutoSize = true;
            this.labelSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.labelSite.Location = new System.Drawing.Point(1, 249);
            this.labelSite.Margin = new System.Windows.Forms.Padding(0);
            this.labelSite.Name = "labelSite";
            this.labelSite.Size = new System.Drawing.Size(214, 30);
            this.labelSite.TabIndex = 141;
            this.labelSite.Text = "Site";
            this.labelSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textSite
            // 
            this.textSite.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.Site", true));
            this.textSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textSite.Location = new System.Drawing.Point(243, 249);
            this.textSite.Margin = new System.Windows.Forms.Padding(0);
            this.textSite.MaxLength = 30;
            this.textSite.Multiline = true;
            this.textSite.Name = "textSite";
            this.textSite.Size = new System.Drawing.Size(500, 30);
            this.textSite.TabIndex = 142;
            // 
            // textCustomer
            // 
            this.textCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.CustomerName", true));
            this.textCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textCustomer.Location = new System.Drawing.Point(243, 32);
            this.textCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.textCustomer.MaxLength = 30;
            this.textCustomer.Multiline = true;
            this.textCustomer.Name = "textCustomer";
            this.textCustomer.Size = new System.Drawing.Size(500, 30);
            this.textCustomer.TabIndex = 144;
            // 
            // labelCustomerName
            // 
            this.labelCustomerName.AutoSize = true;
            this.labelCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.labelCustomerName.Location = new System.Drawing.Point(1, 32);
            this.labelCustomerName.Margin = new System.Windows.Forms.Padding(0);
            this.labelCustomerName.Name = "labelCustomerName";
            this.labelCustomerName.Size = new System.Drawing.Size(214, 30);
            this.labelCustomerName.TabIndex = 143;
            this.labelCustomerName.Text = "Customer Name";
            this.labelCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textDivision
            // 
            this.textDivision.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.Division", true));
            this.textDivision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textDivision.Location = new System.Drawing.Point(243, 63);
            this.textDivision.Margin = new System.Windows.Forms.Padding(0);
            this.textDivision.MaxLength = 30;
            this.textDivision.Multiline = true;
            this.textDivision.Name = "textDivision";
            this.textDivision.Size = new System.Drawing.Size(500, 30);
            this.textDivision.TabIndex = 146;
            // 
            // labelDivision
            // 
            this.labelDivision.AutoSize = true;
            this.labelDivision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDivision.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.labelDivision.Location = new System.Drawing.Point(1, 63);
            this.labelDivision.Margin = new System.Windows.Forms.Padding(0);
            this.labelDivision.Name = "labelDivision";
            this.labelDivision.Size = new System.Drawing.Size(214, 30);
            this.labelDivision.TabIndex = 145;
            this.labelDivision.Text = "Division";
            this.labelDivision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textAssemblyNumber
            // 
            this.textAssemblyNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.AssemblyNumber", true));
            this.textAssemblyNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textAssemblyNumber.Location = new System.Drawing.Point(243, 156);
            this.textAssemblyNumber.Margin = new System.Windows.Forms.Padding(0);
            this.textAssemblyNumber.MaxLength = 30;
            this.textAssemblyNumber.Multiline = true;
            this.textAssemblyNumber.Name = "textAssemblyNumber";
            this.textAssemblyNumber.Size = new System.Drawing.Size(500, 30);
            this.textAssemblyNumber.TabIndex = 152;
            // 
            // labelAssemblyNumber
            // 
            this.labelAssemblyNumber.AutoSize = true;
            this.labelAssemblyNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAssemblyNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.labelAssemblyNumber.Location = new System.Drawing.Point(1, 156);
            this.labelAssemblyNumber.Margin = new System.Windows.Forms.Padding(0);
            this.labelAssemblyNumber.Name = "labelAssemblyNumber";
            this.labelAssemblyNumber.Size = new System.Drawing.Size(214, 30);
            this.labelAssemblyNumber.TabIndex = 151;
            this.labelAssemblyNumber.Text = "Assembly Number";
            this.labelAssemblyNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textStationName
            // 
            this.textStationName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.StationName", true));
            this.textStationName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textStationName.Location = new System.Drawing.Point(243, 94);
            this.textStationName.Margin = new System.Windows.Forms.Padding(0);
            this.textStationName.MaxLength = 30;
            this.textStationName.Multiline = true;
            this.textStationName.Name = "textStationName";
            this.textStationName.Size = new System.Drawing.Size(500, 30);
            this.textStationName.TabIndex = 148;
            // 
            // labelProcessStep
            // 
            this.labelProcessStep.AutoSize = true;
            this.labelProcessStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProcessStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.labelProcessStep.Location = new System.Drawing.Point(1, 125);
            this.labelProcessStep.Margin = new System.Windows.Forms.Padding(0);
            this.labelProcessStep.Name = "labelProcessStep";
            this.labelProcessStep.Size = new System.Drawing.Size(214, 30);
            this.labelProcessStep.TabIndex = 149;
            this.labelProcessStep.Text = "Process Step";
            this.labelProcessStep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelStationName
            // 
            this.labelStationName.AutoSize = true;
            this.labelStationName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.labelStationName.Location = new System.Drawing.Point(1, 94);
            this.labelStationName.Margin = new System.Windows.Forms.Padding(0);
            this.labelStationName.Name = "labelStationName";
            this.labelStationName.Size = new System.Drawing.Size(214, 30);
            this.labelStationName.TabIndex = 147;
            this.labelStationName.Text = "Station Name";
            this.labelStationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textProcessStep
            // 
            this.textProcessStep.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.ProcessStep", true));
            this.textProcessStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textProcessStep.Location = new System.Drawing.Point(243, 125);
            this.textProcessStep.Margin = new System.Windows.Forms.Padding(0);
            this.textProcessStep.MaxLength = 30;
            this.textProcessStep.Multiline = true;
            this.textProcessStep.Name = "textProcessStep";
            this.textProcessStep.Size = new System.Drawing.Size(500, 30);
            this.textProcessStep.TabIndex = 150;
            // 
            // textAssemblyRev
            // 
            this.textAssemblyRev.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.AssemblyRev", true));
            this.textAssemblyRev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textAssemblyRev.Location = new System.Drawing.Point(243, 187);
            this.textAssemblyRev.Margin = new System.Windows.Forms.Padding(0);
            this.textAssemblyRev.MaxLength = 30;
            this.textAssemblyRev.Multiline = true;
            this.textAssemblyRev.Name = "textAssemblyRev";
            this.textAssemblyRev.Size = new System.Drawing.Size(500, 30);
            this.textAssemblyRev.TabIndex = 154;
            // 
            // labelAssemblyRev
            // 
            this.labelAssemblyRev.AutoSize = true;
            this.labelAssemblyRev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAssemblyRev.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.labelAssemblyRev.Location = new System.Drawing.Point(1, 187);
            this.labelAssemblyRev.Margin = new System.Windows.Forms.Padding(0);
            this.labelAssemblyRev.Name = "labelAssemblyRev";
            this.labelAssemblyRev.Size = new System.Drawing.Size(214, 30);
            this.labelAssemblyRev.TabIndex = 153;
            this.labelAssemblyRev.Text = "Assembly Rev";
            this.labelAssemblyRev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelNTUser
            // 
            this.labelNTUser.AutoSize = true;
            this.labelNTUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNTUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.labelNTUser.Location = new System.Drawing.Point(1, 218);
            this.labelNTUser.Margin = new System.Windows.Forms.Padding(0);
            this.labelNTUser.Name = "labelNTUser";
            this.labelNTUser.Size = new System.Drawing.Size(214, 30);
            this.labelNTUser.TabIndex = 155;
            this.labelNTUser.Text = "NT User";
            this.labelNTUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textNTUser
            // 
            this.textNTUser.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.NTUser", true));
            this.textNTUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textNTUser.Location = new System.Drawing.Point(243, 218);
            this.textNTUser.Margin = new System.Windows.Forms.Padding(0);
            this.textNTUser.MaxLength = 30;
            this.textNTUser.Multiline = true;
            this.textNTUser.Name = "textNTUser";
            this.textNTUser.Size = new System.Drawing.Size(500, 30);
            this.textNTUser.TabIndex = 156;
            // 
            // chkGenerateLocalTar
            // 
            this.chkGenerateLocalTar.AutoSize = true;
            this.chkGenerateLocalTar.Checked = true;
            this.chkGenerateLocalTar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGenerateLocalTar.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.MESData, "MESTable.GenerateLocalTar", true));
            this.chkGenerateLocalTar.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkGenerateLocalTar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.chkGenerateLocalTar.Location = new System.Drawing.Point(594, 0);
            this.chkGenerateLocalTar.Margin = new System.Windows.Forms.Padding(0);
            this.chkGenerateLocalTar.Name = "chkGenerateLocalTar";
            this.chkGenerateLocalTar.Size = new System.Drawing.Size(150, 21);
            this.chkGenerateLocalTar.TabIndex = 168;
            this.chkGenerateLocalTar.Text = "Generar";
            this.chkGenerateLocalTar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGenerateLocalTar.UseVisualStyleBackColor = true;
            // 
            // chkGenerateExternalTar
            // 
            this.chkGenerateExternalTar.AutoSize = true;
            this.chkGenerateExternalTar.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.MESData, "MESTable.GenerateExternalTar", true));
            this.chkGenerateExternalTar.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkGenerateExternalTar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.chkGenerateExternalTar.Location = new System.Drawing.Point(594, 30);
            this.chkGenerateExternalTar.Margin = new System.Windows.Forms.Padding(0);
            this.chkGenerateExternalTar.Name = "chkGenerateExternalTar";
            this.chkGenerateExternalTar.Size = new System.Drawing.Size(150, 21);
            this.chkGenerateExternalTar.TabIndex = 168;
            this.chkGenerateExternalTar.Text = "Usar Archivo";
            this.chkGenerateExternalTar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGenerateExternalTar.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label1SerialNumber, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelCustomerName, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textSite, 2, 8);
            this.tableLayoutPanel2.Controls.Add(this.textSerialNumber, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.textNTUser, 2, 7);
            this.tableLayoutPanel2.Controls.Add(this.textAssemblyRev, 2, 6);
            this.tableLayoutPanel2.Controls.Add(this.textAssemblyNumber, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.textDivision, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.textProcessStep, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.textStationName, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.textCustomer, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label17, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label21, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label19, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.labelDivision, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label20, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.labelStationName, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelProcessStep, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label22, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.labelAssemblyNumber, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label23, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelAssemblyRev, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label24, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelNTUser, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.label25, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelSite, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label26, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 2, 9);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 119);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(744, 316);
            this.tableLayoutPanel2.TabIndex = 243;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label4.Location = new System.Drawing.Point(1, 280);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 30);
            this.label4.TabIndex = 168;
            this.label4.Text = "Checkpoint";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MESData, "MESTable.CheckPoint", true));
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(243, 280);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.MaxLength = 30;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(500, 30);
            this.textBox1.TabIndex = 169;
            // 
            // tc_MES
            // 
            this.tc_MES.Controls.Add(this.tp_MES);
            this.tc_MES.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_MES.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc_MES.ItemSize = new System.Drawing.Size(65, 25);
            this.tc_MES.Location = new System.Drawing.Point(0, 0);
            this.tc_MES.Margin = new System.Windows.Forms.Padding(0);
            this.tc_MES.Name = "tc_MES";
            this.tc_MES.Padding = new System.Drawing.Point(0, 0);
            this.tc_MES.SelectedIndex = 0;
            this.tc_MES.Size = new System.Drawing.Size(752, 572);
            this.tc_MES.TabIndex = 244;
            // 
            // tp_MES
            // 
            this.tp_MES.AutoScroll = true;
            this.tp_MES.Controls.Add(this.button_TestTarGeneration);
            this.tp_MES.Controls.Add(this.tableLayoutPanel2);
            this.tp_MES.Controls.Add(this.tableLayoutPanel4);
            this.tp_MES.Controls.Add(this.label6);
            this.tp_MES.Location = new System.Drawing.Point(4, 29);
            this.tp_MES.Margin = new System.Windows.Forms.Padding(0);
            this.tp_MES.Name = "tp_MES";
            this.tp_MES.Size = new System.Drawing.Size(744, 539);
            this.tp_MES.TabIndex = 0;
            this.tp_MES.Text = "MES Tar";
            this.tp_MES.UseVisualStyleBackColor = true;
            // 
            // button_TestTarGeneration
            // 
            this.button_TestTarGeneration.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_TestTarGeneration.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.button_TestTarGeneration.FlatAppearance.BorderSize = 2;
            this.button_TestTarGeneration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_TestTarGeneration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button_TestTarGeneration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_TestTarGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_TestTarGeneration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.button_TestTarGeneration.Location = new System.Drawing.Point(0, 435);
            this.button_TestTarGeneration.Margin = new System.Windows.Forms.Padding(0);
            this.button_TestTarGeneration.Name = "button_TestTarGeneration";
            this.button_TestTarGeneration.Size = new System.Drawing.Size(744, 44);
            this.button_TestTarGeneration.TabIndex = 244;
            this.button_TestTarGeneration.Text = "Test Tar Generation";
            this.button_TestTarGeneration.UseVisualStyleBackColor = true;
            this.button_TestTarGeneration.Click += new System.EventHandler(this.button_TestTarGeneration_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.Controls.Add(this.chkGenerateExternalTar, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.checkBox3, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.buttonExternalPath, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.textExternalPath, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelExternalPath, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelLocalPath, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.chkGenerateLocalTar, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.textLocalPath, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonLocalPath, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(744, 89);
            this.tableLayoutPanel4.TabIndex = 246;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.MESData, "MESTable.UseWebservice", true));
            this.checkBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.checkBox3.Location = new System.Drawing.Point(594, 60);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(150, 21);
            this.checkBox3.TabIndex = 245;
            this.checkBox3.Text = "Usar webservice";
            this.checkBox3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.MaximumSize = new System.Drawing.Size(0, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(744, 30);
            this.label6.TabIndex = 243;
            this.label6.Tag = "MenuColors";
            this.label6.Text = "Configuracion de MES Tar";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MESForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(752, 572);
            this.ControlBox = false;
            this.Controls.Add(this.tc_MES);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MESForm";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.MESData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_MESTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_SerialInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_BatchUnits)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tc_MES.ResumeLayout(false);
            this.tp_MES.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Data.DataTable dt_MESTable;
        private System.Data.DataColumn dcMESTable_CustomerName;
        private System.Data.DataColumn dcMESTable_Division;
        private System.Data.DataColumn dcMESTable_AssemblyNumber;
        private System.Data.DataColumn dcMESTable_StationName;
        private System.Data.DataColumn dcMESTable_AssemblyRev;
        private System.Data.DataColumn dcMESTable_ProcessStep;
        private System.Data.DataColumn dcMESTable_NTUser;
        private System.Data.DataColumn dcMESTable_Site;
        private System.Data.DataColumn dcMESTable_UnitSavePath;
        private System.Data.DataColumn dcMESTable_UnitSaveBackupPath;
        public System.Data.DataSet MESData;
        private System.Windows.Forms.Button buttonExternalPath;
        private System.Windows.Forms.Button buttonLocalPath;
        public System.Windows.Forms.TextBox textExternalPath;
        public System.Windows.Forms.TextBox textLocalPath;
        private System.Windows.Forms.Label labelExternalPath;
        private System.Windows.Forms.Label labelLocalPath;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textSerialNumber;
        private System.Windows.Forms.Label label1SerialNumber;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label labelSite;
        private System.Windows.Forms.TextBox textSite;
        private System.Windows.Forms.TextBox textCustomer;
        private System.Windows.Forms.Label labelCustomerName;
        private System.Windows.Forms.TextBox textDivision;
        private System.Windows.Forms.Label labelDivision;
        private System.Windows.Forms.TextBox textAssemblyNumber;
        private System.Windows.Forms.Label labelAssemblyNumber;
        private System.Windows.Forms.TextBox textStationName;
        private System.Windows.Forms.Label labelProcessStep;
        private System.Windows.Forms.Label labelStationName;
        private System.Windows.Forms.TextBox textProcessStep;
        private System.Windows.Forms.TextBox textAssemblyRev;
        private System.Windows.Forms.Label labelAssemblyRev;
        private System.Windows.Forms.Label labelNTUser;
        private System.Windows.Forms.TextBox textNTUser;
        private System.Data.DataColumn dcMESTable_GenerateLocalTar;
        private System.Data.DataColumn dcMESTable_GenerateExternalTar;
        private System.Windows.Forms.CheckBox chkGenerateLocalTar;
        private System.Windows.Forms.CheckBox chkGenerateExternalTar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TabControl tc_MES;
        private System.Windows.Forms.TabPage tp_MES;
        private System.Windows.Forms.Button button_TestTarGeneration;
        private System.Data.DataColumn dcMESTable_SerialNumber;
        private System.Data.DataTable dt_SerialInfo;
        private System.Data.DataColumn dcMESTable_ValidateDuplicateSerialNumbers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Data.DataColumn dcMESTable_CheckPoint;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Data.DataColumn dcMESTable_UseWebservice;
        private System.Data.DataColumn dcMESTable_LogPath;
        private System.Data.DataColumn dcSerialInfo_ConsecutiveNumber;
        private System.Data.DataColumn dcSerialInfo_UniquePartNumber;
        private System.Windows.Forms.Label label6;
        private System.Data.DataColumn dcSerialInfo_AutomaticBirthPath;
        private System.Data.DataColumn dcSerialInfo_BackupBirthPath;
        private System.Data.DataColumn dcSerialInfo_GenerateBackupBirth;
        private System.Data.DataColumn dcSerialInfo_GenerateAutomaticBirth;
        private System.Data.DataColumn dcSerialInfo_Static;
        private System.Data.DataColumn dcSerialInfo_SiteCode;
        private System.Data.DataColumn dcSerialInfo_CurrentJulianDay;
        private System.Data.DataTable dt_BatchUnits;
        private System.Data.DataColumn dc_CounterBatchUnits;
        private System.Data.DataColumn dc_LimitBatchUnits;
    }
}
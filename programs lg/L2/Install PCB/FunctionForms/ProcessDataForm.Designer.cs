namespace Alpha.FunctionForms
{
	partial class ProcessDataForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_TopName = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Unit_Fail = new System.Windows.Forms.Button();
            this.lbl_Unit_PalletTotal = new System.Windows.Forms.Button();
            this.lbl_Serial = new System.Windows.Forms.Button();
            this.lbl_Unit_ProductTotal = new System.Windows.Forms.Button();
            this.lbl_Unit_Pass = new System.Windows.Forms.Button();
            this.lbl_TackT = new System.Windows.Forms.Button();
            this.lbl_FPY = new System.Windows.Forms.Button();
            this.lbl_CycleTime = new System.Windows.Forms.Button();
            this.lbl_TitleSerial = new System.Windows.Forms.Label();
            this.lbl_Status = new System.Windows.Forms.Button();
            this.lbl_TitleStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_TitleCycleTime = new System.Windows.Forms.Label();
            this.Product = new System.Windows.Forms.Label();
            this.lbl_TitleTackTime = new System.Windows.Forms.Label();
            this.lbl_TitleFPY = new System.Windows.Forms.Label();
            this.lbl_TitlePass = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.hoursProductShow1 = new PointShow.HoursProductShow();
            this.lbl_Model = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel14 = new System.Windows.Forms.Panel();
            this.lbl_TotalJidokaUnits = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lbl_TittleJidoka = new System.Windows.Forms.Label();
            this.btn_ResetJidoka = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ResetGeneral = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tlp_Status = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvProcessResults = new System.Windows.Forms.DataGridView();
            this.dcTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcPalletCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcPreasureDirver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcPreasure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcPreasureLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dc_abResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dc_bcResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pn_Main = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel14.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tlp_Status.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessResults)).BeginInit();
            this.pn_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_TopName
            // 
            this.lbl_TopName.BackColor = System.Drawing.Color.White;
            this.lbl_TopName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlp_Status.SetColumnSpan(this.lbl_TopName, 2);
            this.lbl_TopName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopName.ForeColor = System.Drawing.Color.Black;
            this.lbl_TopName.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopName.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_TopName.Name = "lbl_TopName";
            this.lbl_TopName.Size = new System.Drawing.Size(926, 32);
            this.lbl_TopName.TabIndex = 149;
            this.lbl_TopName.Tag = "MenuColors";
            this.lbl_TopName.Text = "Process Name";
            this.lbl_TopName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.hoursProductShow1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbl_Model, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(926, 755);
            this.tableLayoutPanel3.TabIndex = 24;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Silver;
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(2, 78);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(922, 220);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.Controls.Add(this.lbl_Unit_Fail, 5, 2);
            this.tableLayoutPanel4.Controls.Add(this.lbl_Unit_PalletTotal, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.lbl_Serial, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbl_Unit_ProductTotal, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.lbl_Unit_Pass, 5, 1);
            this.tableLayoutPanel4.Controls.Add(this.lbl_TackT, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.lbl_FPY, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.lbl_CycleTime, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbl_TitleSerial, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbl_Status, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbl_TitleStatus, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.lbl_TitleCycleTime, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.Product, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.lbl_TitleTackTime, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lbl_TitleFPY, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.lbl_TitlePass, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.label5, 4, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 25);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(918, 192);
            this.tableLayoutPanel4.TabIndex = 29;
            // 
            // lbl_Unit_Fail
            // 
            this.lbl_Unit_Fail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Unit_Fail.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Unit_Fail.ForeColor = System.Drawing.Color.Red;
            this.lbl_Unit_Fail.Location = new System.Drawing.Point(762, 131);
            this.lbl_Unit_Fail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_Unit_Fail.Name = "lbl_Unit_Fail";
            this.lbl_Unit_Fail.Size = new System.Drawing.Size(154, 58);
            this.lbl_Unit_Fail.TabIndex = 31;
            this.lbl_Unit_Fail.Text = "0";
            this.lbl_Unit_Fail.UseVisualStyleBackColor = true;
            // 
            // lbl_Unit_PalletTotal
            // 
            this.lbl_Unit_PalletTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Unit_PalletTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Unit_PalletTotal.Location = new System.Drawing.Point(458, 131);
            this.lbl_Unit_PalletTotal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_Unit_PalletTotal.Name = "lbl_Unit_PalletTotal";
            this.lbl_Unit_PalletTotal.Size = new System.Drawing.Size(148, 58);
            this.lbl_Unit_PalletTotal.TabIndex = 38;
            this.lbl_Unit_PalletTotal.Text = "0 0 0";
            this.lbl_Unit_PalletTotal.UseVisualStyleBackColor = true;
            // 
            // lbl_Serial
            // 
            this.lbl_Serial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Serial.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Serial.Location = new System.Drawing.Point(154, 3);
            this.lbl_Serial.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_Serial.Name = "lbl_Serial";
            this.lbl_Serial.Size = new System.Drawing.Size(148, 58);
            this.lbl_Serial.TabIndex = 30;
            this.lbl_Serial.UseVisualStyleBackColor = true;
            // 
            // lbl_Unit_ProductTotal
            // 
            this.lbl_Unit_ProductTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Unit_ProductTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Unit_ProductTotal.Location = new System.Drawing.Point(154, 131);
            this.lbl_Unit_ProductTotal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_Unit_ProductTotal.Name = "lbl_Unit_ProductTotal";
            this.lbl_Unit_ProductTotal.Size = new System.Drawing.Size(148, 58);
            this.lbl_Unit_ProductTotal.TabIndex = 35;
            this.lbl_Unit_ProductTotal.Text = "000";
            this.lbl_Unit_ProductTotal.UseVisualStyleBackColor = true;
            // 
            // lbl_Unit_Pass
            // 
            this.lbl_Unit_Pass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Unit_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Unit_Pass.ForeColor = System.Drawing.Color.Green;
            this.lbl_Unit_Pass.Location = new System.Drawing.Point(762, 67);
            this.lbl_Unit_Pass.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_Unit_Pass.Name = "lbl_Unit_Pass";
            this.lbl_Unit_Pass.Size = new System.Drawing.Size(154, 58);
            this.lbl_Unit_Pass.TabIndex = 37;
            this.lbl_Unit_Pass.Text = "0";
            this.lbl_Unit_Pass.UseVisualStyleBackColor = true;
            // 
            // lbl_TackT
            // 
            this.lbl_TackT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TackT.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TackT.Location = new System.Drawing.Point(154, 67);
            this.lbl_TackT.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_TackT.Name = "lbl_TackT";
            this.lbl_TackT.Size = new System.Drawing.Size(148, 58);
            this.lbl_TackT.TabIndex = 33;
            this.lbl_TackT.Text = "00:00:00";
            this.lbl_TackT.UseVisualStyleBackColor = true;
            // 
            // lbl_FPY
            // 
            this.lbl_FPY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_FPY.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FPY.Location = new System.Drawing.Point(458, 67);
            this.lbl_FPY.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_FPY.Name = "lbl_FPY";
            this.lbl_FPY.Size = new System.Drawing.Size(148, 58);
            this.lbl_FPY.TabIndex = 36;
            this.lbl_FPY.Text = "0 %";
            this.lbl_FPY.UseVisualStyleBackColor = true;
            // 
            // lbl_CycleTime
            // 
            this.lbl_CycleTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CycleTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CycleTime.Location = new System.Drawing.Point(762, 3);
            this.lbl_CycleTime.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_CycleTime.Name = "lbl_CycleTime";
            this.lbl_CycleTime.Size = new System.Drawing.Size(154, 58);
            this.lbl_CycleTime.TabIndex = 32;
            this.lbl_CycleTime.Text = "0.0";
            this.lbl_CycleTime.UseVisualStyleBackColor = true;
            this.lbl_CycleTime.Click += new System.EventHandler(this.lbl_CycleTime_Click);
            // 
            // lbl_TitleSerial
            // 
            this.lbl_TitleSerial.AutoSize = true;
            this.lbl_TitleSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TitleSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TitleSerial.Location = new System.Drawing.Point(2, 0);
            this.lbl_TitleSerial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TitleSerial.Name = "lbl_TitleSerial";
            this.lbl_TitleSerial.Size = new System.Drawing.Size(148, 64);
            this.lbl_TitleSerial.TabIndex = 11;
            this.lbl_TitleSerial.Text = "Codigo de Serie:";
            this.lbl_TitleSerial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Status
            // 
            this.lbl_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.Location = new System.Drawing.Point(458, 3);
            this.lbl_Status.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(148, 58);
            this.lbl_Status.TabIndex = 30;
            this.lbl_Status.Text = "Status";
            this.lbl_Status.UseVisualStyleBackColor = true;
            // 
            // lbl_TitleStatus
            // 
            this.lbl_TitleStatus.AutoSize = true;
            this.lbl_TitleStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TitleStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TitleStatus.Location = new System.Drawing.Point(306, 0);
            this.lbl_TitleStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TitleStatus.Name = "lbl_TitleStatus";
            this.lbl_TitleStatus.Size = new System.Drawing.Size(148, 64);
            this.lbl_TitleStatus.TabIndex = 13;
            this.lbl_TitleStatus.Text = "Estatus：";
            this.lbl_TitleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(306, 128);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 64);
            this.label1.TabIndex = 27;
            this.label1.Text = "Total de Pallets:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TitleCycleTime
            // 
            this.lbl_TitleCycleTime.AutoSize = true;
            this.lbl_TitleCycleTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TitleCycleTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TitleCycleTime.Location = new System.Drawing.Point(610, 0);
            this.lbl_TitleCycleTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TitleCycleTime.Name = "lbl_TitleCycleTime";
            this.lbl_TitleCycleTime.Size = new System.Drawing.Size(148, 64);
            this.lbl_TitleCycleTime.TabIndex = 15;
            this.lbl_TitleCycleTime.Text = "Tiempo de Ciclo:";
            this.lbl_TitleCycleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Product
            // 
            this.Product.AutoSize = true;
            this.Product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Product.Location = new System.Drawing.Point(2, 128);
            this.Product.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Product.Name = "Product";
            this.Product.Size = new System.Drawing.Size(148, 64);
            this.Product.TabIndex = 25;
            this.Product.Text = "Total de Productos:";
            this.Product.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TitleTackTime
            // 
            this.lbl_TitleTackTime.AutoSize = true;
            this.lbl_TitleTackTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TitleTackTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TitleTackTime.Location = new System.Drawing.Point(2, 64);
            this.lbl_TitleTackTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TitleTackTime.Name = "lbl_TitleTackTime";
            this.lbl_TitleTackTime.Size = new System.Drawing.Size(148, 64);
            this.lbl_TitleTackTime.TabIndex = 17;
            this.lbl_TitleTackTime.Text = "Takt Time:";
            this.lbl_TitleTackTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TitleFPY
            // 
            this.lbl_TitleFPY.AutoSize = true;
            this.lbl_TitleFPY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TitleFPY.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TitleFPY.Location = new System.Drawing.Point(306, 64);
            this.lbl_TitleFPY.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TitleFPY.Name = "lbl_TitleFPY";
            this.lbl_TitleFPY.Size = new System.Drawing.Size(148, 64);
            this.lbl_TitleFPY.TabIndex = 19;
            this.lbl_TitleFPY.Text = "FPY:";
            this.lbl_TitleFPY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TitlePass
            // 
            this.lbl_TitlePass.AutoSize = true;
            this.lbl_TitlePass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TitlePass.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TitlePass.Location = new System.Drawing.Point(610, 64);
            this.lbl_TitlePass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TitlePass.Name = "lbl_TitlePass";
            this.lbl_TitlePass.Size = new System.Drawing.Size(148, 64);
            this.lbl_TitlePass.TabIndex = 21;
            this.lbl_TitlePass.Text = "OK:";
            this.lbl_TitlePass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(610, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 64);
            this.label5.TabIndex = 23;
            this.label5.Text = "NG:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hoursProductShow1
            // 
            this.hoursProductShow1.BackColor = System.Drawing.Color.White;
            this.hoursProductShow1.Color_BackGround = System.Drawing.Color.White;
            this.hoursProductShow1.Color_IDLE = System.Drawing.Color.Silver;
            this.hoursProductShow1.Color_NG = System.Drawing.Color.Red;
            this.hoursProductShow1.Color_OK = System.Drawing.Color.LawnGreen;
            this.hoursProductShow1.Color_Target = System.Drawing.Color.Yellow;
            this.hoursProductShow1.ColumnWidth = 20;
            this.hoursProductShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hoursProductShow1.Location = new System.Drawing.Point(4, 305);
            this.hoursProductShow1.Margin = new System.Windows.Forms.Padding(4);
            this.hoursProductShow1.MaxX = 24;
            this.hoursProductShow1.MaxY = 500;
            this.hoursProductShow1.MinX = 0;
            this.hoursProductShow1.MinY = 0;
            this.hoursProductShow1.Name = "hoursProductShow1";
            this.hoursProductShow1.SavePath = "";
            this.hoursProductShow1.ScaleX = 24;
            this.hoursProductShow1.ScaleY = 10;
            this.hoursProductShow1.Size = new System.Drawing.Size(918, 446);
            this.hoursProductShow1.StartTime = PointShow.HoursProductShow.StartTimeD.Hour_0;
            this.hoursProductShow1.TabIndex = 22;
            this.hoursProductShow1.Target = 0;
            this.hoursProductShow1.TargetEnable = true;
            this.hoursProductShow1.Xlable = "Hora del dia";
            this.hoursProductShow1.Ylable = "Cantidad";
            // 
            // lbl_Model
            // 
            this.lbl_Model.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Model.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Model.Location = new System.Drawing.Point(2, 3);
            this.lbl_Model.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lbl_Model.Name = "lbl_Model";
            this.lbl_Model.Size = new System.Drawing.Size(922, 69);
            this.lbl_Model.TabIndex = 24;
            this.lbl_Model.Text = "Model";
            this.lbl_Model.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel14
            // 
            this.tlp_Status.SetColumnSpan(this.panel14, 2);
            this.panel14.Controls.Add(this.tableLayoutPanel3);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(0, 33);
            this.panel14.Margin = new System.Windows.Forms.Padding(0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(926, 755);
            this.panel14.TabIndex = 3;
            // 
            // lbl_TotalJidokaUnits
            // 
            this.lbl_TotalJidokaUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TotalJidokaUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_TotalJidokaUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TotalJidokaUnits.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbl_TotalJidokaUnits.Location = new System.Drawing.Point(308, 0);
            this.lbl_TotalJidokaUnits.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_TotalJidokaUnits.Name = "lbl_TotalJidokaUnits";
            this.lbl_TotalJidokaUnits.Size = new System.Drawing.Size(308, 49);
            this.lbl_TotalJidokaUnits.TabIndex = 266;
            this.lbl_TotalJidokaUnits.Text = "3";
            this.lbl_TotalJidokaUnits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(154, 0);
            this.label29.Margin = new System.Windows.Forms.Padding(0);
            this.label29.MinimumSize = new System.Drawing.Size(90, 50);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(154, 50);
            this.label29.TabIndex = 265;
            this.label29.Text = "Conteo de Fallas:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TittleJidoka
            // 
            this.lbl_TittleJidoka.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.lbl_TittleJidoka.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TittleJidoka.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_TittleJidoka.ForeColor = System.Drawing.Color.White;
            this.lbl_TittleJidoka.Location = new System.Drawing.Point(0, 0);
            this.lbl_TittleJidoka.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_TittleJidoka.MinimumSize = new System.Drawing.Size(90, 50);
            this.lbl_TittleJidoka.Name = "lbl_TittleJidoka";
            this.lbl_TittleJidoka.Size = new System.Drawing.Size(154, 50);
            this.lbl_TittleJidoka.TabIndex = 256;
            this.lbl_TittleJidoka.Tag = "MenuColors";
            this.lbl_TittleJidoka.Text = "NP";
            this.lbl_TittleJidoka.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_ResetJidoka
            // 
            this.btn_ResetJidoka.BackColor = System.Drawing.Color.Red;
            this.btn_ResetJidoka.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ResetJidoka.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.btn_ResetJidoka.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btn_ResetJidoka.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ResetJidoka.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ResetJidoka.ForeColor = System.Drawing.Color.White;
            this.btn_ResetJidoka.Location = new System.Drawing.Point(616, 0);
            this.btn_ResetJidoka.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ResetJidoka.MinimumSize = new System.Drawing.Size(0, 50);
            this.btn_ResetJidoka.Name = "btn_ResetJidoka";
            this.btn_ResetJidoka.Size = new System.Drawing.Size(154, 50);
            this.btn_ResetJidoka.TabIndex = 267;
            this.btn_ResetJidoka.Text = "Restablecer Conteo NG";
            this.btn_ResetJidoka.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btn_ResetGeneral, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_ResetJidoka, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_TotalJidokaUnits, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label29, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_TittleJidoka, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(926, 49);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_ResetGeneral
            // 
            this.btn_ResetGeneral.BackColor = System.Drawing.Color.Green;
            this.btn_ResetGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ResetGeneral.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.btn_ResetGeneral.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btn_ResetGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ResetGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ResetGeneral.ForeColor = System.Drawing.Color.White;
            this.btn_ResetGeneral.Location = new System.Drawing.Point(770, 0);
            this.btn_ResetGeneral.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ResetGeneral.MinimumSize = new System.Drawing.Size(86, 50);
            this.btn_ResetGeneral.Name = "btn_ResetGeneral";
            this.btn_ResetGeneral.Size = new System.Drawing.Size(156, 50);
            this.btn_ResetGeneral.TabIndex = 268;
            this.btn_ResetGeneral.Text = "Restablecer Conteo OK";
            this.btn_ResetGeneral.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.tlp_Status.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 1138);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(926, 49);
            this.panel3.TabIndex = 152;
            // 
            // tlp_Status
            // 
            this.tlp_Status.AutoScroll = true;
            this.tlp_Status.ColumnCount = 2;
            this.tlp_Status.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Status.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Status.Controls.Add(this.panel3, 0, 3);
            this.tlp_Status.Controls.Add(this.panel2, 0, 2);
            this.tlp_Status.Controls.Add(this.lbl_TopName, 0, 0);
            this.tlp_Status.Controls.Add(this.panel14, 0, 1);
            this.tlp_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Status.Location = new System.Drawing.Point(0, 0);
            this.tlp_Status.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Status.Name = "tlp_Status";
            this.tlp_Status.RowCount = 4;
            this.tlp_Status.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlp_Status.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Status.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tlp_Status.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlp_Status.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlp_Status.Size = new System.Drawing.Size(926, 1187);
            this.tlp_Status.TabIndex = 150;
            // 
            // panel2
            // 
            this.tlp_Status.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 788);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(926, 350);
            this.panel2.TabIndex = 150;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dgvProcessResults, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(926, 350);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // dgvProcessResults
            // 
            this.dgvProcessResults.AllowUserToAddRows = false;
            this.dgvProcessResults.AllowUserToDeleteRows = false;
            this.dgvProcessResults.AllowUserToResizeRows = false;
            this.dgvProcessResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvProcessResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvProcessResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProcessResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dcTime,
            this.dcProductCode,
            this.dcPalletCode,
            this.dcPreasureDirver,
            this.dcPreasure,
            this.dcPreasureLimit,
            this.dc_abResult,
            this.dc_bcResult});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProcessResults.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvProcessResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProcessResults.EnableHeadersVisualStyles = false;
            this.dgvProcessResults.Location = new System.Drawing.Point(2, 2);
            this.dgvProcessResults.Margin = new System.Windows.Forms.Padding(2);
            this.dgvProcessResults.Name = "dgvProcessResults";
            this.dgvProcessResults.ReadOnly = true;
            this.dgvProcessResults.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProcessResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvProcessResults.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvProcessResults.RowTemplate.Height = 24;
            this.dgvProcessResults.Size = new System.Drawing.Size(922, 346);
            this.dgvProcessResults.TabIndex = 1;
            this.dgvProcessResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcessResults_CellContentClick);
            // 
            // dcTime
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.dcTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.dcTime.FillWeight = 8.30971F;
            this.dcTime.HeaderText = "Tiempo";
            this.dcTime.MinimumWidth = 6;
            this.dcTime.Name = "dcTime";
            this.dcTime.ReadOnly = true;
            this.dcTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dcTime.Width = 80;
            // 
            // dcProductCode
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.dcProductCode.DefaultCellStyle = dataGridViewCellStyle3;
            this.dcProductCode.FillWeight = 67.79863F;
            this.dcProductCode.HeaderText = "Codigo del Producto";
            this.dcProductCode.MinimumWidth = 6;
            this.dcProductCode.Name = "dcProductCode";
            this.dcProductCode.ReadOnly = true;
            this.dcProductCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dcProductCode.Width = 180;
            // 
            // dcPalletCode
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.dcPalletCode.DefaultCellStyle = dataGridViewCellStyle4;
            this.dcPalletCode.FillWeight = 78.35574F;
            this.dcPalletCode.HeaderText = "Codigo del Pallet";
            this.dcPalletCode.MinimumWidth = 6;
            this.dcPalletCode.Name = "dcPalletCode";
            this.dcPalletCode.ReadOnly = true;
            this.dcPalletCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dcPalletCode.Width = 150;
            // 
            // dcPreasureDirver
            // 
            this.dcPreasureDirver.FillWeight = 102.6234F;
            this.dcPreasureDirver.HeaderText = "Gripper";
            this.dcPreasureDirver.Name = "dcPreasureDirver";
            this.dcPreasureDirver.ReadOnly = true;
            this.dcPreasureDirver.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dcPreasureDirver.Width = 80;
            // 
            // dcPreasure
            // 
            this.dcPreasure.FillWeight = 122.5642F;
            this.dcPreasure.HeaderText = "Presion";
            this.dcPreasure.Name = "dcPreasure";
            this.dcPreasure.ReadOnly = true;
            this.dcPreasure.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dcPreasure.Width = 80;
            // 
            // dcPreasureLimit
            // 
            this.dcPreasureLimit.FillWeight = 171.1629F;
            this.dcPreasureLimit.HeaderText = "Limite de Presion";
            this.dcPreasureLimit.Name = "dcPreasureLimit";
            this.dcPreasureLimit.ReadOnly = true;
            this.dcPreasureLimit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dcPreasureLimit.Width = 130;
            // 
            // dc_abResult
            // 
            this.dc_abResult.FillWeight = 243.6548F;
            this.dc_abResult.HeaderText = "a->bResult(P<PL)";
            this.dc_abResult.Name = "dc_abResult";
            this.dc_abResult.ReadOnly = true;
            this.dc_abResult.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dc_abResult.Width = 130;
            // 
            // dc_bcResult
            // 
            this.dc_bcResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dc_bcResult.FillWeight = 5.530626F;
            this.dc_bcResult.HeaderText = "b->cResult(P>PL)";
            this.dc_bcResult.Name = "dc_bcResult";
            this.dc_bcResult.ReadOnly = true;
            this.dc_bcResult.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pn_Main
            // 
            this.pn_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_Main.Controls.Add(this.tlp_Status);
            this.pn_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_Main.Location = new System.Drawing.Point(0, 0);
            this.pn_Main.Margin = new System.Windows.Forms.Padding(2);
            this.pn_Main.Name = "pn_Main";
            this.pn_Main.Size = new System.Drawing.Size(928, 1189);
            this.pn_Main.TabIndex = 4;
            // 
            // ProcessDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(928, 1189);
            this.ControlBox = false;
            this.Controls.Add(this.pn_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProcessDataForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "ProcessDataForm";
            this.Text = "ProcessDataForm";
            this.Load += new System.EventHandler(this.ProcessDataForm_Load);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tlp_Status.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessResults)).EndInit();
            this.pn_Main.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Label lbl_TopName;
        public System.Windows.Forms.TableLayoutPanel tlp_Status;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_ResetGeneral;
        private System.Windows.Forms.Button btn_ResetJidoka;
        private System.Windows.Forms.Label lbl_TotalJidokaUnits;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lbl_TittleJidoka;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.DataGridView dgvProcessResults;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button lbl_Unit_Fail;
        private System.Windows.Forms.Button lbl_Unit_PalletTotal;
        private System.Windows.Forms.Button lbl_Serial;
        private System.Windows.Forms.Button lbl_Unit_ProductTotal;
        private System.Windows.Forms.Button lbl_TackT;
        private System.Windows.Forms.Button lbl_FPY;
        private System.Windows.Forms.Label lbl_TitleSerial;
        private System.Windows.Forms.Button lbl_Status;
        private System.Windows.Forms.Label lbl_TitleStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_TitleCycleTime;
        private System.Windows.Forms.Label Product;
        private System.Windows.Forms.Label lbl_TitleTackTime;
        private System.Windows.Forms.Label lbl_TitleFPY;
        private System.Windows.Forms.Label lbl_TitlePass;
        private System.Windows.Forms.Label label5;
        public PointShow.HoursProductShow hoursProductShow1;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Panel pn_Main;
        private System.Windows.Forms.Button lbl_Model;
        public System.Windows.Forms.Button lbl_Unit_Pass;
        public System.Windows.Forms.Button lbl_CycleTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcPalletCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcPreasureDirver;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcPreasure;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcPreasureLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dc_abResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn dc_bcResult;
    }
}
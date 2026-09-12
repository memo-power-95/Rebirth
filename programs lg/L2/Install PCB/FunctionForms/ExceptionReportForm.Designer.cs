namespace Alpha.FunctionForms
{
	partial class ExceptionReportForm
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
            this.pn_Exceptions = new System.Windows.Forms.Panel();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.pnlInputs = new System.Windows.Forms.Panel();
            this.dGV_ExceptionList = new System.Windows.Forms.DataGridView();
            this.dGV_cl_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dGV_cl_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dGV_cl_Module = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dGV_cl_Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dGV_cl_StackTrace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.cb_EnableTracking = new System.Windows.Forms.CheckBox();
            this.pn_Exceptions.SuspendLayout();
            this.panel10.SuspendLayout();
            this.pnlInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_ExceptionList)).BeginInit();
            this.SuspendLayout();
            // 
            // pn_Exceptions
            // 
            this.pn_Exceptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pn_Exceptions.Controls.Add(this.cb_EnableTracking);
            this.pn_Exceptions.Controls.Add(this.btn_Clear);
            this.pn_Exceptions.Controls.Add(this.panel10);
            this.pn_Exceptions.Controls.Add(this.label10);
            this.pn_Exceptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_Exceptions.Location = new System.Drawing.Point(0, 0);
            this.pn_Exceptions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pn_Exceptions.Name = "pn_Exceptions";
            this.pn_Exceptions.Size = new System.Drawing.Size(1282, 651);
            this.pn_Exceptions.TabIndex = 33;
            // 
            // btn_Clear
            // 
            this.btn_Clear.BackColor = System.Drawing.Color.Red;
            this.btn_Clear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkRed;
            this.btn_Clear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.ForeColor = System.Drawing.Color.White;
            this.btn_Clear.Location = new System.Drawing.Point(0, -1);
            this.btn_Clear.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(121, 51);
            this.btn_Clear.TabIndex = 36;
            this.btn_Clear.Text = "Limpiar";
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // panel10
            // 
            this.panel10.AutoScroll = true;
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.Controls.Add(this.pnlInputs);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 50);
            this.panel10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1280, 599);
            this.panel10.TabIndex = 35;
            // 
            // pnlInputs
            // 
            this.pnlInputs.AutoScroll = true;
            this.pnlInputs.Controls.Add(this.dGV_ExceptionList);
            this.pnlInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInputs.Location = new System.Drawing.Point(0, 0);
            this.pnlInputs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlInputs.Name = "pnlInputs";
            this.pnlInputs.Size = new System.Drawing.Size(1280, 599);
            this.pnlInputs.TabIndex = 0;
            // 
            // dGV_ExceptionList
            // 
            this.dGV_ExceptionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_ExceptionList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dGV_cl_Date,
            this.dGV_cl_Time,
            this.dGV_cl_Module,
            this.dGV_cl_Message,
            this.dGV_cl_StackTrace});
            this.dGV_ExceptionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGV_ExceptionList.Location = new System.Drawing.Point(0, 0);
            this.dGV_ExceptionList.Name = "dGV_ExceptionList";
            this.dGV_ExceptionList.RowHeadersWidth = 51;
            this.dGV_ExceptionList.RowTemplate.Height = 24;
            this.dGV_ExceptionList.Size = new System.Drawing.Size(1280, 599);
            this.dGV_ExceptionList.TabIndex = 0;
            this.dGV_ExceptionList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGV_ExceptionList_CellDoubleClick);
            // 
            // dGV_cl_Date
            // 
            this.dGV_cl_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dGV_cl_Date.HeaderText = "Fecha";
            this.dGV_cl_Date.MaxInputLength = 30;
            this.dGV_cl_Date.MinimumWidth = 10;
            this.dGV_cl_Date.Name = "dGV_cl_Date";
            this.dGV_cl_Date.ReadOnly = true;
            this.dGV_cl_Date.Width = 74;
            // 
            // dGV_cl_Time
            // 
            this.dGV_cl_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dGV_cl_Time.HeaderText = "Hora";
            this.dGV_cl_Time.MaxInputLength = 30;
            this.dGV_cl_Time.MinimumWidth = 10;
            this.dGV_cl_Time.Name = "dGV_cl_Time";
            this.dGV_cl_Time.ReadOnly = true;
            this.dGV_cl_Time.Width = 66;
            // 
            // dGV_cl_Module
            // 
            this.dGV_cl_Module.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dGV_cl_Module.HeaderText = "Modulo";
            this.dGV_cl_Module.MaxInputLength = 50;
            this.dGV_cl_Module.MinimumWidth = 10;
            this.dGV_cl_Module.Name = "dGV_cl_Module";
            this.dGV_cl_Module.ReadOnly = true;
            this.dGV_cl_Module.Width = 81;
            // 
            // dGV_cl_Message
            // 
            this.dGV_cl_Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dGV_cl_Message.HeaderText = "Mensaje";
            this.dGV_cl_Message.MaxInputLength = 65534;
            this.dGV_cl_Message.MinimumWidth = 10;
            this.dGV_cl_Message.Name = "dGV_cl_Message";
            this.dGV_cl_Message.ReadOnly = true;
            this.dGV_cl_Message.Width = 125;
            // 
            // dGV_cl_StackTrace
            // 
            this.dGV_cl_StackTrace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dGV_cl_StackTrace.HeaderText = "StackTrace";
            this.dGV_cl_StackTrace.MaxInputLength = 65534;
            this.dGV_cl_StackTrace.MinimumWidth = 10;
            this.dGV_cl_StackTrace.Name = "dGV_cl_StackTrace";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.MaximumSize = new System.Drawing.Size(0, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1280, 50);
            this.label10.TabIndex = 0;
            this.label10.Tag = "MenuColors";
            this.label10.Text = "Lista de Excepciones";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_EnableTracking
            // 
            this.cb_EnableTracking.AutoSize = true;
            this.cb_EnableTracking.Location = new System.Drawing.Point(124, 29);
            this.cb_EnableTracking.Name = "cb_EnableTracking";
            this.cb_EnableTracking.Size = new System.Drawing.Size(164, 21);
            this.cb_EnableTracking.TabIndex = 37;
            this.cb_EnableTracking.Text = "Deshabilitar Tracking";
            this.cb_EnableTracking.UseVisualStyleBackColor = true;
            this.cb_EnableTracking.CheckedChanged += new System.EventHandler(this.cb_EnableTracking_CheckedChanged);
            // 
            // ExceptionReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 651);
            this.Controls.Add(this.pn_Exceptions);
            this.Name = "ExceptionReportForm";
            this.Text = "ExceptionReportForm";
            this.pn_Exceptions.ResumeLayout(false);
            this.pn_Exceptions.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.pnlInputs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_ExceptionList)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.Panel pnlInputs;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.DataGridView dGV_ExceptionList;
		public System.Windows.Forms.Panel pn_Exceptions;
		private System.Windows.Forms.Button btn_Clear;
		private System.Windows.Forms.DataGridViewTextBoxColumn dGV_cl_Date;
		private System.Windows.Forms.DataGridViewTextBoxColumn dGV_cl_Time;
		private System.Windows.Forms.DataGridViewTextBoxColumn dGV_cl_Module;
		private System.Windows.Forms.DataGridViewTextBoxColumn dGV_cl_Message;
		private System.Windows.Forms.DataGridViewTextBoxColumn dGV_cl_StackTrace;
        private System.Windows.Forms.CheckBox cb_EnableTracking;
    }
}
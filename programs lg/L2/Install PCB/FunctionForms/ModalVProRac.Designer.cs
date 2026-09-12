namespace Alpha.FunctionForms
{
	partial class ModalVProRac
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pn_CogRecordContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nUP_CircleDiam = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUP_CircleDiam)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pn_CogRecordContainer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(469, 363);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.SplitterWidth = 20;
            this.splitContainer1.TabIndex = 0;
            // 
            // pn_CogRecordContainer
            // 
            this.pn_CogRecordContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_CogRecordContainer.Location = new System.Drawing.Point(0, 0);
            this.pn_CogRecordContainer.Name = "pn_CogRecordContainer";
            this.pn_CogRecordContainer.Size = new System.Drawing.Size(469, 305);
            this.pn_CogRecordContainer.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nUP_CircleDiam);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 38);
            this.panel1.TabIndex = 0;
            // 
            // nUP_CircleDiam
            // 
            this.nUP_CircleDiam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nUP_CircleDiam.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUP_CircleDiam.Location = new System.Drawing.Point(89, 2);
            this.nUP_CircleDiam.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nUP_CircleDiam.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUP_CircleDiam.Name = "nUP_CircleDiam";
            this.nUP_CircleDiam.Size = new System.Drawing.Size(72, 26);
            this.nUP_CircleDiam.TabIndex = 3;
            this.toolTip.SetToolTip(this.nUP_CircleDiam, "El radio para el cortador es de 16");
            this.nUP_CircleDiam.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUP_CircleDiam.ValueChanged += new System.EventHandler(this.nUP_CircleDiam_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 26);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tamaño de \r\nCirculo (pixeles)";
            // 
            // Timer
            // 
            this.Timer.Interval = 1000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // ModalVProRac
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(469, 363);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ModalVProRac";
            this.ShowIcon = false;
            this.Text = "ModalVProRac";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModalVProRac_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUP_CircleDiam)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.Panel pn_CogRecordContainer;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Timer Timer;
		private System.Windows.Forms.ToolTip toolTip;
		public System.Windows.Forms.NumericUpDown nUP_CircleDiam;
	}
}
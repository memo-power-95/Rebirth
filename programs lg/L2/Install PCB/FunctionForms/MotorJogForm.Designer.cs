namespace Alpha.FunctionForms
{
    partial class MotorJogForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.rb_JogMode = new System.Windows.Forms.RadioButton();
            this.rb_RelativeMode = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.trkSpeedRatio = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_Distance = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Stop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeedRatio)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(83, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Distancia Relativa:";
            // 
            // rb_JogMode
            // 
            this.rb_JogMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rb_JogMode.AutoSize = true;
            this.rb_JogMode.Checked = true;
            this.rb_JogMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_JogMode.Location = new System.Drawing.Point(228, 54);
            this.rb_JogMode.Name = "rb_JogMode";
            this.rb_JogMode.Size = new System.Drawing.Size(62, 28);
            this.rb_JogMode.TabIndex = 2;
            this.rb_JogMode.TabStop = true;
            this.rb_JogMode.Text = "Jog";
            this.rb_JogMode.UseVisualStyleBackColor = true;
            this.rb_JogMode.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rb_RelativeMode
            // 
            this.rb_RelativeMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rb_RelativeMode.AutoSize = true;
            this.rb_RelativeMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_RelativeMode.Location = new System.Drawing.Point(320, 54);
            this.rb_RelativeMode.Name = "rb_RelativeMode";
            this.rb_RelativeMode.Size = new System.Drawing.Size(102, 28);
            this.rb_RelativeMode.TabIndex = 3;
            this.rb_RelativeMode.TabStop = true;
            this.rb_RelativeMode.Text = "Relativo";
            this.rb_RelativeMode.UseVisualStyleBackColor = true;
            this.rb_RelativeMode.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(84, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "% Velocidad:";
            // 
            // trkSpeedRatio
            // 
            this.trkSpeedRatio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.trkSpeedRatio.Location = new System.Drawing.Point(214, 0);
            this.trkSpeedRatio.Maximum = 100;
            this.trkSpeedRatio.Minimum = 3;
            this.trkSpeedRatio.Name = "trkSpeedRatio";
            this.trkSpeedRatio.Size = new System.Drawing.Size(237, 45);
            this.trkSpeedRatio.SmallChange = 5;
            this.trkSpeedRatio.TabIndex = 5;
            this.trkSpeedRatio.TickFrequency = 2;
            this.trkSpeedRatio.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trkSpeedRatio.Value = 30;
            this.trkSpeedRatio.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(84, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tipo Mov. :";
            // 
            // tb_Distance
            // 
            this.tb_Distance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_Distance.BackColor = System.Drawing.SystemColors.Control;
            this.tb_Distance.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Distance.Location = new System.Drawing.Point(259, 91);
            this.tb_Distance.Name = "tb_Distance";
            this.tb_Distance.Size = new System.Drawing.Size(184, 28);
            this.tb_Distance.TabIndex = 7;
            this.tb_Distance.Text = "1";
            this.tb_Distance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Distance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Distance_KeyPress);
            this.tb_Distance.Leave += new System.EventHandler(this.tb_Distance_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Stop);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tb_Distance);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.rb_JogMode);
            this.panel1.Controls.Add(this.trkSpeedRatio);
            this.panel1.Controls.Add(this.rb_RelativeMode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 201);
            this.panel1.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 201);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(536, 804);
            this.flowLayoutPanel1.TabIndex = 9;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btn_Stop
            // 
            this.btn_Stop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Stop.Image = global::Alpha.Properties.Resources.Stop2;
            this.btn_Stop.Location = new System.Drawing.Point(99, 132);
            this.btn_Stop.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Stop.Size = new System.Drawing.Size(354, 61);
            this.btn_Stop.TabIndex = 149;
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // MotorJogForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(536, 1005);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MotorJogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MotorJogForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.MotorJogForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeedRatio)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_JogMode;
        private System.Windows.Forms.RadioButton rb_RelativeMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trkSpeedRatio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_Distance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btn_Stop;
    }
}
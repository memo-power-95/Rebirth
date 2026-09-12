namespace Alpha.MENUForms
{
    partial class MachineSetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineSetupForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tcMotorSetup = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSignalTower = new System.Windows.Forms.TabPage();
            this.plSignalTowerSetup = new System.Windows.Forms.Panel();
            this.tabPageMotionSetup = new System.Windows.Forms.TabPage();
            this.tabPageMes = new System.Windows.Forms.TabPage();
            this.plMes = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.tcMotorSetup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSignalTower.SuspendLayout();
            this.tabPageMotionSetup.SuspendLayout();
            this.tabPageMes.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(788, 39);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(131, 36);
            this.toolStripButton1.Text = "Guardar";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::Alpha.Properties.Resources.Cancel;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(153, 36);
            this.toolStripButton2.Text = "Cancelar";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // tcMotorSetup
            // 
            this.tcMotorSetup.Controls.Add(this.tabPage1);
            this.tcMotorSetup.Controls.Add(this.tabPage2);
            this.tcMotorSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMotorSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMotorSetup.ItemSize = new System.Drawing.Size(200, 40);
            this.tcMotorSetup.Location = new System.Drawing.Point(0, 0);
            this.tcMotorSetup.Margin = new System.Windows.Forms.Padding(0);
            this.tcMotorSetup.Name = "tcMotorSetup";
            this.tcMotorSetup.SelectedIndex = 0;
            this.tcMotorSetup.Size = new System.Drawing.Size(774, 611);
            this.tcMotorSetup.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcMotorSetup.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(766, 563);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Module #1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(766, 563);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Module #2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tcMotorSetup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 611);
            this.panel1.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSignalTower);
            this.tabControl1.Controls.Add(this.tabPageMotionSetup);
            this.tabControl1.Controls.Add(this.tabPageMes);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 40);
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(788, 665);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 14;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageSignalTower
            // 
            this.tabPageSignalTower.Controls.Add(this.plSignalTowerSetup);
            this.tabPageSignalTower.Location = new System.Drawing.Point(4, 44);
            this.tabPageSignalTower.Name = "tabPageSignalTower";
            this.tabPageSignalTower.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSignalTower.Size = new System.Drawing.Size(780, 617);
            this.tabPageSignalTower.TabIndex = 0;
            this.tabPageSignalTower.Text = "Torreta";
            this.tabPageSignalTower.UseVisualStyleBackColor = true;
            // 
            // plSignalTowerSetup
            // 
            this.plSignalTowerSetup.AutoScroll = true;
            this.plSignalTowerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plSignalTowerSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plSignalTowerSetup.Location = new System.Drawing.Point(3, 3);
            this.plSignalTowerSetup.Name = "plSignalTowerSetup";
            this.plSignalTowerSetup.Size = new System.Drawing.Size(774, 611);
            this.plSignalTowerSetup.TabIndex = 0;
            // 
            // tabPageMotionSetup
            // 
            this.tabPageMotionSetup.Controls.Add(this.panel1);
            this.tabPageMotionSetup.Location = new System.Drawing.Point(4, 44);
            this.tabPageMotionSetup.Name = "tabPageMotionSetup";
            this.tabPageMotionSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMotionSetup.Size = new System.Drawing.Size(780, 617);
            this.tabPageMotionSetup.TabIndex = 1;
            this.tabPageMotionSetup.Text = "Ajustes de Movimiento";
            this.tabPageMotionSetup.UseVisualStyleBackColor = true;
            // 
            // tabPageMes
            // 
            this.tabPageMes.Controls.Add(this.plMes);
            this.tabPageMes.Location = new System.Drawing.Point(4, 44);
            this.tabPageMes.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMes.Name = "tabPageMes";
            this.tabPageMes.Size = new System.Drawing.Size(780, 617);
            this.tabPageMes.TabIndex = 2;
            this.tabPageMes.Text = "MES";
            this.tabPageMes.UseVisualStyleBackColor = true;
            // 
            // plMes
            // 
            this.plMes.AutoScroll = true;
            this.plMes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMes.Location = new System.Drawing.Point(0, 0);
            this.plMes.Margin = new System.Windows.Forms.Padding(0);
            this.plMes.Name = "plMes";
            this.plMes.Size = new System.Drawing.Size(780, 617);
            this.plMes.TabIndex = 0;
            // 
            // MachineSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(788, 704);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MachineSetupForm";
            this.Text = "Machine Setup";
            this.Load += new System.EventHandler(this.MachineSetupForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tcMotorSetup.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSignalTower.ResumeLayout(false);
            this.tabPageMotionSetup.ResumeLayout(false);
            this.tabPageMes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TabControl tcMotorSetup;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSignalTower;
        private System.Windows.Forms.TabPage tabPageMotionSetup;
        private System.Windows.Forms.Panel plSignalTowerSetup;
        private System.Windows.Forms.TabPage tabPageMes;
        public System.Windows.Forms.Panel plMes;
    }
}
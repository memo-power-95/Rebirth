namespace Alpha.ModuleForms
{
    partial class LotForm
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel_Module = new System.Windows.Forms.Panel();
            this.plMachineStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(1024, 1811);
            // 
            // plMaintenance
            // 
            this.plMaintenance.Size = new System.Drawing.Size(1016, 1768);
            // 
            // plMachineStatus
            // 
            this.plMachineStatus.Controls.Add(this.Panel_Module);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Panel_Module
            // 
            this.Panel_Module.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Module.Location = new System.Drawing.Point(0, 0);
            this.Panel_Module.Name = "Panel_Module";
            this.Panel_Module.Size = new System.Drawing.Size(672, 416);
            this.Panel_Module.TabIndex = 0;
            // 
            // LotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 1811);
            this.Name = "LotForm";
            this.Text = "LOTE";
            this.plMachineStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel Panel_Module;
    }
}
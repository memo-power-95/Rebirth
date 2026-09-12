namespace Alpha.ModuleForms
{
    partial class AlarmForm
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
            this.ElementHost_AlarmTops = new System.Windows.Forms.Integration.ElementHost();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel_Log = new System.Windows.Forms.Panel();
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
            this.plMachineStatus.Controls.Add(this.Panel_Log);
            this.plMachineStatus.Controls.Add(this.ElementHost_AlarmTops);
            // 
            // ElementHost_AlarmTops
            // 
            this.ElementHost_AlarmTops.BackColor = System.Drawing.Color.DimGray;
            this.ElementHost_AlarmTops.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ElementHost_AlarmTops.Location = new System.Drawing.Point(0, 244);
            this.ElementHost_AlarmTops.Name = "ElementHost_AlarmTops";
            this.ElementHost_AlarmTops.Size = new System.Drawing.Size(672, 195);
            this.ElementHost_AlarmTops.TabIndex = 0;
            this.ElementHost_AlarmTops.Text = "elementHost1";
            this.ElementHost_AlarmTops.Child = null;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Panel_Log
            // 
            this.Panel_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Log.Location = new System.Drawing.Point(0, 0);
            this.Panel_Log.Name = "Panel_Log";
            this.Panel_Log.Size = new System.Drawing.Size(672, 244);
            this.Panel_Log.TabIndex = 1;
            // 
            // AlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 1811);
            this.Name = "AlarmForm";
            this.Text = "ALARMAS";
            this.plMachineStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Integration.ElementHost ElementHost_AlarmTops;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel Panel_Log;
    }
}
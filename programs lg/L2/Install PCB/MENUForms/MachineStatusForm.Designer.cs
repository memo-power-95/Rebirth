namespace Alpha.MENUForms
{
    partial class MachineStatusForm
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
            this.tcMachineStatus = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tcMachineStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMachineStatus
            // 
            this.tcMachineStatus.Controls.Add(this.tabPage1);
            this.tcMachineStatus.Controls.Add(this.tabPage2);
            this.tcMachineStatus.Controls.Add(this.tabPage3);
            this.tcMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMachineStatus.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMachineStatus.ItemSize = new System.Drawing.Size(200, 40);
            this.tcMachineStatus.Location = new System.Drawing.Point(0, 0);
            this.tcMachineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tcMachineStatus.Name = "tcMachineStatus";
            this.tcMachineStatus.SelectedIndex = 0;
            this.tcMachineStatus.Size = new System.Drawing.Size(677, 536);
            this.tcMachineStatus.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcMachineStatus.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(669, 488);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Module #1";
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(669, 488);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Module #2";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 44);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(669, 488);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // MachineStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(677, 536);
            this.Controls.Add(this.tcMachineStatus);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MachineStatusForm";
            this.Text = "Machine Status";
            this.Load += new System.EventHandler(this.MachineStatusForm_Load);
            this.tcMachineStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMachineStatus;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
    }
}
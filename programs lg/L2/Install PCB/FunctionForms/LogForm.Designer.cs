namespace Alpha.FunctionForms
{
    partial class LogForm
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
            this.tabLogPage = new System.Windows.Forms.TabControl();
            this.tabLogForm = new System.Windows.Forms.TabPage();
            this.tmRefresh = new System.Windows.Forms.Timer(this.components);
            this.tabLogPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabLogPage
            // 
            this.tabLogPage.Controls.Add(this.tabLogForm);
            this.tabLogPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLogPage.ItemSize = new System.Drawing.Size(120, 40);
            this.tabLogPage.Location = new System.Drawing.Point(0, 0);
            this.tabLogPage.Name = "tabLogPage";
            this.tabLogPage.SelectedIndex = 0;
            this.tabLogPage.Size = new System.Drawing.Size(788, 565);
            this.tabLogPage.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabLogPage.TabIndex = 0;
            this.tabLogPage.SelectedIndexChanged += new System.EventHandler(this.tabLogPage_SelectedIndexChanged);
            // 
            // tabLogForm
            // 
            this.tabLogForm.BackColor = System.Drawing.Color.White;
            this.tabLogForm.Location = new System.Drawing.Point(4, 44);
            this.tabLogForm.Margin = new System.Windows.Forms.Padding(0);
            this.tabLogForm.Name = "tabLogForm";
            this.tabLogForm.Size = new System.Drawing.Size(780, 517);
            this.tabLogForm.TabIndex = 0;
            this.tabLogForm.Text = "Log Form";
            // 
            // tmRefresh
            // 
            this.tmRefresh.Enabled = true;
            this.tmRefresh.Interval = 50;
            this.tmRefresh.Tick += new System.EventHandler(this.tmRefresh_Tick);
            // 
            // LogForm
            // 
            this.AccessibleDescription = "Modulo encargado de guardar y mostrar la bitacora del proceso. ";
            this.AccessibleName = "mAuxiliar_1.0.0.1";
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(788, 565);
            this.Controls.Add(this.tabLogPage);
            this.Name = "LogForm";
            this.Text = "LogForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabLogPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabLogPage;
        private System.Windows.Forms.TabPage tabLogForm;
        private System.Windows.Forms.Timer tmRefresh;
    }
}
namespace AcuraLibrary.Forms
{
    partial class ModuleBaseForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMaintenance = new System.Windows.Forms.TabPage();
            this.plMaintenance = new System.Windows.Forms.Panel();
            this.tpProductionSetting = new System.Windows.Forms.TabPage();
            this.plProductionSetting = new System.Windows.Forms.Panel();
            this.tpRecipeEditor = new System.Windows.Forms.TabPage();
            this.plRecipeEditor = new System.Windows.Forms.Panel();
            this.tpFlowInitial = new System.Windows.Forms.TabPage();
            this.plFlowInitial = new System.Windows.Forms.Panel();
            this.tpFlowAuto = new System.Windows.Forms.TabPage();
            this.plFlowAuto = new System.Windows.Forms.Panel();
            this.tpMachineStatus = new System.Windows.Forms.TabPage();
            this.plMachineStatus = new System.Windows.Forms.Panel();
            this.tpMotionSetup = new System.Windows.Forms.TabPage();
            this.plMotionSetup = new System.Windows.Forms.Panel();
            this.tpMotorControl = new System.Windows.Forms.TabPage();
            this.plMotorControl = new System.Windows.Forms.Panel();
            this.SettingData = new System.Data.DataSet();
            this.MSet = new System.Data.DataTable();
            this.PSet = new System.Data.DataTable();
            this.RecipeData = new System.Data.DataSet();
            this.RSet = new System.Data.DataTable();
            this.tabControl1.SuspendLayout();
            this.tpMaintenance.SuspendLayout();
            this.tpProductionSetting.SuspendLayout();
            this.tpRecipeEditor.SuspendLayout();
            this.tpFlowInitial.SuspendLayout();
            this.tpFlowAuto.SuspendLayout();
            this.tpMachineStatus.SuspendLayout();
            this.tpMotionSetup.SuspendLayout();
            this.tpMotorControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tpMaintenance);
            this.tabControl1.Controls.Add(this.tpProductionSetting);
            this.tabControl1.Controls.Add(this.tpRecipeEditor);
            this.tabControl1.Controls.Add(this.tpFlowInitial);
            this.tabControl1.Controls.Add(this.tpFlowAuto);
            this.tabControl1.Controls.Add(this.tpMachineStatus);
            this.tabControl1.Controls.Add(this.tpMotionSetup);
            this.tabControl1.Controls.Add(this.tpMotorControl);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(118, 35);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(680, 482);
            this.tabControl1.TabIndex = 1;
            // 
            // tpMaintenance
            // 
            this.tpMaintenance.BackColor = System.Drawing.Color.Black;
            this.tpMaintenance.Controls.Add(this.plMaintenance);
            this.tpMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpMaintenance.Location = new System.Drawing.Point(4, 39);
            this.tpMaintenance.Margin = new System.Windows.Forms.Padding(0);
            this.tpMaintenance.Name = "tpMaintenance";
            this.tpMaintenance.Size = new System.Drawing.Size(672, 439);
            this.tpMaintenance.TabIndex = 0;
            this.tpMaintenance.Text = "Maintenance";
            // 
            // plMaintenance
            // 
            this.plMaintenance.AutoScroll = true;
            this.plMaintenance.BackColor = System.Drawing.Color.White;
            this.plMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.plMaintenance.Location = new System.Drawing.Point(0, 0);
            this.plMaintenance.Margin = new System.Windows.Forms.Padding(0);
            this.plMaintenance.Name = "plMaintenance";
            this.plMaintenance.Size = new System.Drawing.Size(672, 439);
            this.plMaintenance.TabIndex = 1;
            this.plMaintenance.Paint += new System.Windows.Forms.PaintEventHandler(this.plMaintenance_Paint);
            // 
            // tpProductionSetting
            // 
            this.tpProductionSetting.BackColor = System.Drawing.Color.Black;
            this.tpProductionSetting.Controls.Add(this.plProductionSetting);
            this.tpProductionSetting.Location = new System.Drawing.Point(4, 39);
            this.tpProductionSetting.Margin = new System.Windows.Forms.Padding(0);
            this.tpProductionSetting.Name = "tpProductionSetting";
            this.tpProductionSetting.Size = new System.Drawing.Size(672, 439);
            this.tpProductionSetting.TabIndex = 1;
            this.tpProductionSetting.Text = "Production Setting";
            // 
            // plProductionSetting
            // 
            this.plProductionSetting.AutoScroll = true;
            this.plProductionSetting.BackColor = System.Drawing.Color.White;
            this.plProductionSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plProductionSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.plProductionSetting.Location = new System.Drawing.Point(0, 0);
            this.plProductionSetting.Margin = new System.Windows.Forms.Padding(0);
            this.plProductionSetting.Name = "plProductionSetting";
            this.plProductionSetting.Size = new System.Drawing.Size(672, 439);
            this.plProductionSetting.TabIndex = 2;
            // 
            // tpRecipeEditor
            // 
            this.tpRecipeEditor.BackColor = System.Drawing.Color.Black;
            this.tpRecipeEditor.Controls.Add(this.plRecipeEditor);
            this.tpRecipeEditor.Location = new System.Drawing.Point(4, 39);
            this.tpRecipeEditor.Margin = new System.Windows.Forms.Padding(0);
            this.tpRecipeEditor.Name = "tpRecipeEditor";
            this.tpRecipeEditor.Size = new System.Drawing.Size(672, 439);
            this.tpRecipeEditor.TabIndex = 2;
            this.tpRecipeEditor.Text = "Recipe Editor";
            // 
            // plRecipeEditor
            // 
            this.plRecipeEditor.AutoScroll = true;
            this.plRecipeEditor.BackColor = System.Drawing.Color.White;
            this.plRecipeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plRecipeEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.plRecipeEditor.Location = new System.Drawing.Point(0, 0);
            this.plRecipeEditor.Margin = new System.Windows.Forms.Padding(0);
            this.plRecipeEditor.Name = "plRecipeEditor";
            this.plRecipeEditor.Size = new System.Drawing.Size(672, 439);
            this.plRecipeEditor.TabIndex = 2;
            // 
            // tpFlowInitial
            // 
            this.tpFlowInitial.BackColor = System.Drawing.Color.Black;
            this.tpFlowInitial.Controls.Add(this.plFlowInitial);
            this.tpFlowInitial.Location = new System.Drawing.Point(4, 39);
            this.tpFlowInitial.Margin = new System.Windows.Forms.Padding(0);
            this.tpFlowInitial.Name = "tpFlowInitial";
            this.tpFlowInitial.Size = new System.Drawing.Size(672, 439);
            this.tpFlowInitial.TabIndex = 3;
            this.tpFlowInitial.Text = "Flow Initial";
            // 
            // plFlowInitial
            // 
            this.plFlowInitial.AutoScroll = true;
            this.plFlowInitial.BackColor = System.Drawing.Color.White;
            this.plFlowInitial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFlowInitial.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.plFlowInitial.Location = new System.Drawing.Point(0, 0);
            this.plFlowInitial.Margin = new System.Windows.Forms.Padding(0);
            this.plFlowInitial.Name = "plFlowInitial";
            this.plFlowInitial.Size = new System.Drawing.Size(672, 439);
            this.plFlowInitial.TabIndex = 3;
            // 
            // tpFlowAuto
            // 
            this.tpFlowAuto.BackColor = System.Drawing.Color.Black;
            this.tpFlowAuto.Controls.Add(this.plFlowAuto);
            this.tpFlowAuto.Location = new System.Drawing.Point(4, 39);
            this.tpFlowAuto.Margin = new System.Windows.Forms.Padding(0);
            this.tpFlowAuto.Name = "tpFlowAuto";
            this.tpFlowAuto.Size = new System.Drawing.Size(672, 439);
            this.tpFlowAuto.TabIndex = 4;
            this.tpFlowAuto.Text = "Flow Auto";
            // 
            // plFlowAuto
            // 
            this.plFlowAuto.AutoScroll = true;
            this.plFlowAuto.BackColor = System.Drawing.Color.White;
            this.plFlowAuto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFlowAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.plFlowAuto.Location = new System.Drawing.Point(0, 0);
            this.plFlowAuto.Margin = new System.Windows.Forms.Padding(0);
            this.plFlowAuto.Name = "plFlowAuto";
            this.plFlowAuto.Size = new System.Drawing.Size(672, 439);
            this.plFlowAuto.TabIndex = 4;
            // 
            // tpMachineStatus
            // 
            this.tpMachineStatus.BackColor = System.Drawing.Color.Black;
            this.tpMachineStatus.Controls.Add(this.plMachineStatus);
            this.tpMachineStatus.Location = new System.Drawing.Point(4, 39);
            this.tpMachineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tpMachineStatus.Name = "tpMachineStatus";
            this.tpMachineStatus.Size = new System.Drawing.Size(672, 439);
            this.tpMachineStatus.TabIndex = 5;
            this.tpMachineStatus.Text = "Machine Status";
            // 
            // plMachineStatus
            // 
            this.plMachineStatus.AutoScroll = true;
            this.plMachineStatus.BackColor = System.Drawing.Color.White;
            this.plMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMachineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.plMachineStatus.Location = new System.Drawing.Point(0, 0);
            this.plMachineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.plMachineStatus.Name = "plMachineStatus";
            this.plMachineStatus.Size = new System.Drawing.Size(672, 439);
            this.plMachineStatus.TabIndex = 5;
            // 
            // tpMotionSetup
            // 
            this.tpMotionSetup.BackColor = System.Drawing.Color.Black;
            this.tpMotionSetup.Controls.Add(this.plMotionSetup);
            this.tpMotionSetup.Location = new System.Drawing.Point(4, 39);
            this.tpMotionSetup.Margin = new System.Windows.Forms.Padding(0);
            this.tpMotionSetup.Name = "tpMotionSetup";
            this.tpMotionSetup.Size = new System.Drawing.Size(672, 439);
            this.tpMotionSetup.TabIndex = 6;
            this.tpMotionSetup.Text = "Motion Setup";
            // 
            // plMotionSetup
            // 
            this.plMotionSetup.AutoScroll = true;
            this.plMotionSetup.BackColor = System.Drawing.Color.White;
            this.plMotionSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMotionSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plMotionSetup.Location = new System.Drawing.Point(0, 0);
            this.plMotionSetup.Margin = new System.Windows.Forms.Padding(0);
            this.plMotionSetup.Name = "plMotionSetup";
            this.plMotionSetup.Size = new System.Drawing.Size(672, 439);
            this.plMotionSetup.TabIndex = 6;
            // 
            // tpMotorControl
            // 
            this.tpMotorControl.BackColor = System.Drawing.Color.Black;
            this.tpMotorControl.Controls.Add(this.plMotorControl);
            this.tpMotorControl.Location = new System.Drawing.Point(4, 39);
            this.tpMotorControl.Margin = new System.Windows.Forms.Padding(0);
            this.tpMotorControl.Name = "tpMotorControl";
            this.tpMotorControl.Size = new System.Drawing.Size(672, 439);
            this.tpMotorControl.TabIndex = 7;
            this.tpMotorControl.Text = "NPMotor Control";
            // 
            // plMotorControl
            // 
            this.plMotorControl.AutoScroll = true;
            this.plMotorControl.BackColor = System.Drawing.Color.White;
            this.plMotorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMotorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.plMotorControl.Location = new System.Drawing.Point(0, 0);
            this.plMotorControl.Margin = new System.Windows.Forms.Padding(0);
            this.plMotorControl.Name = "plMotorControl";
            this.plMotorControl.Size = new System.Drawing.Size(672, 439);
            this.plMotorControl.TabIndex = 7;
            // 
            // SettingData
            // 
            this.SettingData.DataSetName = "Setting";
            this.SettingData.Tables.AddRange(new System.Data.DataTable[] {
            this.MSet,
            this.PSet});
            // 
            // MSet
            // 
            this.MSet.TableName = "MSet";
            // 
            // PSet
            // 
            this.PSet.TableName = "PSet";
            // 
            // RecipeData
            // 
            this.RecipeData.DataSetName = "Recipe";
            this.RecipeData.Tables.AddRange(new System.Data.DataTable[] {
            this.RSet});
            // 
            // RSet
            // 
            this.RSet.TableName = "RSet";
            // 
            // ModuleBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 482);
            this.Controls.Add(this.tabControl1);
            this.Name = "ModuleBaseForm";
            this.Text = "ModuleBaseForm";
            this.tabControl1.ResumeLayout(false);
            this.tpMaintenance.ResumeLayout(false);
            this.tpProductionSetting.ResumeLayout(false);
            this.tpRecipeEditor.ResumeLayout(false);
            this.tpFlowInitial.ResumeLayout(false);
            this.tpFlowAuto.ResumeLayout(false);
            this.tpMachineStatus.ResumeLayout(false);
            this.tpMotionSetup.ResumeLayout(false);
            this.tpMotorControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpMaintenance;
        public System.Windows.Forms.Panel plMaintenance;
        private System.Windows.Forms.TabPage tpProductionSetting;
        public System.Windows.Forms.Panel plProductionSetting;
        private System.Windows.Forms.TabPage tpRecipeEditor;
        public System.Windows.Forms.Panel plRecipeEditor;
        private System.Windows.Forms.TabPage tpFlowInitial;
        public System.Windows.Forms.Panel plFlowInitial;
        private System.Windows.Forms.TabPage tpFlowAuto;
        public System.Windows.Forms.Panel plFlowAuto;
        private System.Windows.Forms.TabPage tpMachineStatus;
        public System.Windows.Forms.Panel plMachineStatus;
        private System.Windows.Forms.TabPage tpMotionSetup;
        public System.Windows.Forms.Panel plMotionSetup;
        private System.Windows.Forms.TabPage tpMotorControl;
        public System.Windows.Forms.Panel plMotorControl;
        public System.Data.DataSet SettingData;
        public System.Data.DataTable MSet;
        public System.Data.DataTable PSet;
        public System.Data.DataSet RecipeData;
        public System.Data.DataTable RSet;
    }
}
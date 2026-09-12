namespace Alpha.MENUForms
{
    partial class UserSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSettingForm));
            this.btnAddUser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAddUserPermission = new System.Windows.Forms.ComboBox();
            this.textAddUserName = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textPasswordConfirm = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnModifyPermission = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.cbSelectedPermission = new System.Windows.Forms.ComboBox();
            this.textSelectedUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvUserList = new System.Windows.Forms.DataGridView();
            this.dgvRightsConfig = new System.Windows.Forms.DataGridView();
            this.lbl_UserData = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbl_TypePermisions = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRightsConfig)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddUser
            // 
            this.btnAddUser.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAddUser.Enabled = false;
            this.btnAddUser.FlatAppearance.BorderSize = 0;
            this.btnAddUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(240)))));
            this.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddUser.Location = new System.Drawing.Point(3, 192);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(604, 47);
            this.btnAddUser.TabIndex = 9;
            this.btnAddUser.Text = "Agregar Usuario";
            this.btnAddUser.UseVisualStyleBackColor = false;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(267, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Contraseña Confirmar";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 29);
            this.label4.TabIndex = 5;
            this.label4.Text = "Permiso";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAddUserPermission);
            this.groupBox1.Controls.Add(this.textAddUserName);
            this.groupBox1.Controls.Add(this.textPassword);
            this.groupBox1.Controls.Add(this.textPasswordConfirm);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnAddUser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(617, 245);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar Usuario";
            // 
            // cbAddUserPermission
            // 
            this.cbAddUserPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddUserPermission.FormattingEnabled = true;
            this.cbAddUserPermission.Location = new System.Drawing.Point(229, 40);
            this.cbAddUserPermission.Name = "cbAddUserPermission";
            this.cbAddUserPermission.Size = new System.Drawing.Size(375, 37);
            this.cbAddUserPermission.TabIndex = 5;
            // 
            // textAddUserName
            // 
            this.textAddUserName.Location = new System.Drawing.Point(229, 78);
            this.textAddUserName.Name = "textAddUserName";
            this.textAddUserName.Size = new System.Drawing.Size(375, 34);
            this.textAddUserName.TabIndex = 6;
            this.textAddUserName.TextChanged += new System.EventHandler(this.AddUser_TextChanged);
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(229, 114);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(375, 34);
            this.textPassword.TabIndex = 7;
            this.textPassword.TextChanged += new System.EventHandler(this.AddUser_TextChanged);
            // 
            // textPasswordConfirm
            // 
            this.textPasswordConfirm.Location = new System.Drawing.Point(229, 153);
            this.textPasswordConfirm.Name = "textPasswordConfirm";
            this.textPasswordConfirm.PasswordChar = '*';
            this.textPasswordConfirm.Size = new System.Drawing.Size(375, 34);
            this.textPasswordConfirm.TabIndex = 8;
            this.textPasswordConfirm.TextChanged += new System.EventHandler(this.AddUser_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnModifyPermission);
            this.groupBox2.Controls.Add(this.btnDeleteUser);
            this.groupBox2.Controls.Add(this.cbSelectedPermission);
            this.groupBox2.Controls.Add(this.textSelectedUserName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(3, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(617, 251);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Usuario Seleccionado";
            // 
            // btnModifyPermission
            // 
            this.btnModifyPermission.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnModifyPermission.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnModifyPermission.Enabled = false;
            this.btnModifyPermission.FlatAppearance.BorderSize = 0;
            this.btnModifyPermission.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(240)))));
            this.btnModifyPermission.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModifyPermission.Location = new System.Drawing.Point(3, 154);
            this.btnModifyPermission.Name = "btnModifyPermission";
            this.btnModifyPermission.Size = new System.Drawing.Size(611, 47);
            this.btnModifyPermission.TabIndex = 3;
            this.btnModifyPermission.Text = "Modificar Permiso";
            this.btnModifyPermission.UseVisualStyleBackColor = false;
            this.btnModifyPermission.Click += new System.EventHandler(this.btnUserModify_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDeleteUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDeleteUser.Enabled = false;
            this.btnDeleteUser.FlatAppearance.BorderSize = 0;
            this.btnDeleteUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(240)))));
            this.btnDeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteUser.Location = new System.Drawing.Point(3, 201);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(611, 47);
            this.btnDeleteUser.TabIndex = 4;
            this.btnDeleteUser.Text = "Borrar Usuario";
            this.btnDeleteUser.UseVisualStyleBackColor = false;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnUserDelete_Click);
            // 
            // cbSelectedPermission
            // 
            this.cbSelectedPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedPermission.FormattingEnabled = true;
            this.cbSelectedPermission.Location = new System.Drawing.Point(217, 52);
            this.cbSelectedPermission.Name = "cbSelectedPermission";
            this.cbSelectedPermission.Size = new System.Drawing.Size(158, 37);
            this.cbSelectedPermission.TabIndex = 1;
            // 
            // textSelectedUserName
            // 
            this.textSelectedUserName.Enabled = false;
            this.textSelectedUserName.Location = new System.Drawing.Point(217, 90);
            this.textSelectedUserName.Name = "textSelectedUserName";
            this.textSelectedUserName.Size = new System.Drawing.Size(158, 34);
            this.textSelectedUserName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 29);
            this.label5.TabIndex = 5;
            this.label5.Text = "Permiso";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 29);
            this.label6.TabIndex = 2;
            this.label6.Text = "Nombre Usuario";
            // 
            // dgvUserList
            // 
            this.dgvUserList.AllowUserToAddRows = false;
            this.dgvUserList.AllowUserToDeleteRows = false;
            this.dgvUserList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvUserList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserList.Location = new System.Drawing.Point(0, 0);
            this.dgvUserList.MultiSelect = false;
            this.dgvUserList.Name = "dgvUserList";
            this.dgvUserList.ReadOnly = true;
            this.dgvUserList.RowHeadersWidth = 51;
            this.dgvUserList.RowTemplate.Height = 35;
            this.dgvUserList.Size = new System.Drawing.Size(347, 519);
            this.dgvUserList.TabIndex = 8;
            this.dgvUserList.TabStop = false;
            this.dgvUserList.CurrentCellChanged += new System.EventHandler(this.dgvUserList_CurrentCellChanged);
            // 
            // dgvRightsConfig
            // 
            this.dgvRightsConfig.AllowUserToAddRows = false;
            this.dgvRightsConfig.AllowUserToDeleteRows = false;
            this.dgvRightsConfig.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvRightsConfig.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRightsConfig.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvRightsConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRightsConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRightsConfig.Location = new System.Drawing.Point(0, 0);
            this.dgvRightsConfig.MultiSelect = false;
            this.dgvRightsConfig.Name = "dgvRightsConfig";
            this.dgvRightsConfig.RowHeadersWidth = 51;
            this.dgvRightsConfig.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvRightsConfig.RowTemplate.Height = 24;
            this.dgvRightsConfig.Size = new System.Drawing.Size(1062, 231);
            this.dgvRightsConfig.TabIndex = 9;
            this.dgvRightsConfig.TabStop = false;
            // 
            // lbl_UserData
            // 
            this.lbl_UserData.BackColor = System.Drawing.Color.White;
            this.lbl_UserData.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_UserData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_UserData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_UserData.Location = new System.Drawing.Point(0, 39);
            this.lbl_UserData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_UserData.Name = "lbl_UserData";
            this.lbl_UserData.Size = new System.Drawing.Size(1062, 40);
            this.lbl_UserData.TabIndex = 10;
            this.lbl_UserData.Text = "Datos de Usuario";
            this.lbl_UserData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1062, 519);
            this.panel1.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvUserList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1062, 519);
            this.splitContainer1.SplitterDistance = 347;
            this.splitContainer1.SplitterIncrement = 5;
            this.splitContainer1.SplitterWidth = 20;
            this.splitContainer1.TabIndex = 9;
            // 
            // lbl_TypePermisions
            // 
            this.lbl_TypePermisions.BackColor = System.Drawing.Color.White;
            this.lbl_TypePermisions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TypePermisions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_TypePermisions.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TypePermisions.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_TypePermisions.Location = new System.Drawing.Point(0, 598);
            this.lbl_TypePermisions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TypePermisions.Name = "lbl_TypePermisions";
            this.lbl_TypePermisions.Size = new System.Drawing.Size(1062, 40);
            this.lbl_TypePermisions.TabIndex = 13;
            this.lbl_TypePermisions.Text = "Ajuste de Permisos";
            this.lbl_TypePermisions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvRightsConfig);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 638);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1062, 231);
            this.panel2.TabIndex = 14;
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
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1062, 39);
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
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(153, 36);
            this.toolStripButton2.Text = "Cancelar";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // UserSettingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1062, 884);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_TypePermisions);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_UserData);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserSettingForm";
            this.Text = "User Setting";
            this.VisibleChanged += new System.EventHandler(this.UserSettingForm_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserSettingForm_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRightsConfig)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbSelectedPermission;
        private System.Windows.Forms.TextBox textSelectedUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnModifyPermission;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.DataGridView dgvUserList;
        private System.Windows.Forms.DataGridView dgvRightsConfig;
        private System.Windows.Forms.Label lbl_UserData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_TypePermisions;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cbAddUserPermission;
        private System.Windows.Forms.TextBox textAddUserName;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textPasswordConfirm;
    }
}
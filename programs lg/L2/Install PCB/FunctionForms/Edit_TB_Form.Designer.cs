namespace Alpha.FunctionForms
{
    partial class Edit_TB_Form
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
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            this.panel53 = new System.Windows.Forms.Panel();
            this.btnGrabImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            this.panel53.SuspendLayout();
            this.SuspendLayout();
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(0, 39);
            this.cogToolBlockEditV21.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(652, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(1177, 804);
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            this.cogToolBlockEditV21.TabIndex = 0;
            // 
            // panel53
            // 
            this.panel53.AutoScroll = true;
            this.panel53.Controls.Add(this.btnGrabImage);
            this.panel53.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel53.Location = new System.Drawing.Point(0, 0);
            this.panel53.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel53.Name = "panel53";
            this.panel53.Padding = new System.Windows.Forms.Padding(27, 2, 3, 2);
            this.panel53.Size = new System.Drawing.Size(1177, 39);
            this.panel53.TabIndex = 47;
            // 
            // btnGrabImage
            // 
            this.btnGrabImage.BackColor = System.Drawing.Color.White;
            this.btnGrabImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnGrabImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrabImage.Location = new System.Drawing.Point(27, 2);
            this.btnGrabImage.Margin = new System.Windows.Forms.Padding(27, 4, 4, 4);
            this.btnGrabImage.Name = "btnGrabImage";
            this.btnGrabImage.Size = new System.Drawing.Size(128, 35);
            this.btnGrabImage.TabIndex = 13;
            this.btnGrabImage.Text = "Grab Image";
            this.btnGrabImage.UseVisualStyleBackColor = false;
            this.btnGrabImage.Click += new System.EventHandler(this.btnGrabImage_Click);
            // 
            // Edit_TB_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 843);
            this.Controls.Add(this.cogToolBlockEditV21);
            this.Controls.Add(this.panel53);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Edit_TB_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit_TB_Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Edit_TB_Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            this.panel53.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        private System.Windows.Forms.Panel panel53;
        private System.Windows.Forms.Button btnGrabImage;
    }
}
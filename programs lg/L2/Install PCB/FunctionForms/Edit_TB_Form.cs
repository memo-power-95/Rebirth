using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Windows.Forms;

namespace Alpha.FunctionForms
{
    public partial class Edit_TB_Form : Form
    {
        public CogToolBlock EditTB;
        private string EditTB_Path;
        public Edit_TB_Form(CogToolBlock TB, String TB_Path)
        {
            InitializeComponent();
            EditTB = TB;
            EditTB_Path = TB_Path;
            cogToolBlockEditV21.Subject = EditTB;
            this.Text = EditTB.Name;
        }

        private void Edit_TB_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = MessageBox.Show("Save ToolBlock?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                EditTB = cogToolBlockEditV21.Subject;
                CogSerializer.SaveObjectToFile(EditTB, EditTB_Path);
            }
            /*else if (DialogResult == DialogResult.No)
            {
                try
                {
                    EditTB = (CogToolBlock)CogSerializer.LoadObjectFromFile(EditTB_Path);
                }
                catch (Exception ex)
                {

                }
            }*/
            else if (DialogResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void btnGrabImage_Click(object sender, EventArgs e)
        {
            //MiddleLayer.VProRACF.GrabOneImage(false, false);
            //cogToolBlockEditV21.Subject.Inputs["InputImage"].Value = (CogImage8Grey)MiddleLayer.VProRACF.ImgFlipTool.OutputImage;
        }
    }
}


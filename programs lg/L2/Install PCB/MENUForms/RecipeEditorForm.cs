using Alpha._0.ModuleForms;
using Alpha.Classes;
using AcuraLibrary;
using AcuraLibrary.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Alpha.MENUForms
{
	public partial class RecipeEditorForm : Form
    {
        public RecipeEditorForm()
        {
            InitializeComponent();
        }

        private void RecipeEditorForm_Load(object sender, EventArgs e)
        {
            tcRecipeEditor.TabPages.Clear();
            int nEnabledCount=0;
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                bool bHasContent=Stage.plRecipeEditor.Controls.Count>0;
                if (Stage.plRecipeEditor.Enabled&&bHasContent)
                {
                    nEnabledCount++;
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plRecipeEditor.Parent = tg;
                    tcRecipeEditor.TabPages.Add(tg);
                }
            }
            TabControl tabControl=tcRecipeEditor;
            if ( nEnabledCount!=0)
			{
                Size sParent=tabControl.Parent.Size;
                Size sDividers=tabControl.ItemSize;
				sDividers.Width = (sParent.Width - 50) / nEnabledCount;
				sDividers.Width = sDividers.Width < 150 ? 150 : sDividers.Width;
				tabControl.ItemSize = sDividers; 
			}
        }

        public void fn_PerformSaveRecipe()
        {
            panel1.Focus();
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].BeforRecipeEditor();

            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].AfterRecipeEditor();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel1.Focus();
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].BeforRecipeEditor();

            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].AfterRecipeEditor();
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].WriteSettingData();
            for (int j = 0; j < ModuleManager.ModuleList.Count; j++)
            {
                if (ModuleManager.ModuleList[j] is GantryForm)
                {
                    MiddleLayer.GantryF.SaveAxisPosPara();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Focus();
            MiddleLayer.OpenRecipe(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDir = new SaveFileDialog();
            SaveFileDir.Filter = "XML Files|*.xml";

            string Directory = SysPara.RecipeDataDirectory.Replace(".\\", System.IO.Directory.GetCurrentDirectory() + "\\");
            SaveFileDir.InitialDirectory = Directory;
            if (SaveFileDir.ShowDialog() == DialogResult.OK)
            {
                if (ModuleManager.ModuleList.Count > 0)
                {
                    ModuleManager.ModuleList[0].WriteRecipeData(SaveFileDir.FileName);
                    SysPara.RecipeDataDirectory = Path.GetDirectoryName(SaveFileDir.FileName);
                    SysPara.RecipeName = Path.GetFileNameWithoutExtension(SaveFileDir.FileName);
                    MiddleLayer.OpenRecipe(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MiddleLayer.MesF.ReadMESData();
            string OrgRecipeName = SysPara.RecipeName;
            OpenFileDialog OpenFileDir = new OpenFileDialog();
            OpenFileDir.Filter = "XML Files|*.xml";

            try
            {
                OpenFileDir.InitialDirectory = SysPara.RecipeDataDirectory;
            }
            catch (Exception ex)
            {
                MiddleLayer.ExReportF.fnAddException(ex);
            }

            if (OpenFileDir.ShowDialog() == DialogResult.OK)
                if (MiddleLayer.OpenRecipe(OpenFileDir.FileName))
                {
                    IniFile iniFile = new IniFile(SysPara.sIniFile);
                    iniFile.WriteString("MachineSetup", "RecipeName", OpenFileDir.SafeFileName.Replace(".xml", ""));

                    for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                        ModuleManager.ModuleList[i].AfterOpenRecipe(OrgRecipeName, OpenFileDir.FileName);
                }
        }
        private void RecipeEditorForm_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent == null)
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].ExitRecipeEditor();
            }
            else
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].IntoRecipeEditor();
            }
        }
    }
}

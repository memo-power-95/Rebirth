using Alpha._0.ModuleForms;
using AcuraLibrary;
using AcuraLibrary.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha.MENUForms
{
    public partial class ProductionSettingForm : Form
    {
        public ProductionSettingForm()
        {
            InitializeComponent();
        }

        private void ProductionSettingForm_Load(object sender, EventArgs e)
        {
            tcProductionSetting.TabPages.Clear();
            int nEnabledCount=0;
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                bool bHasContent=Stage.plProductionSetting.Controls.Count>0;
                if (Stage.plProductionSetting.Enabled&&bHasContent)
                {       
                    nEnabledCount++;
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plProductionSetting.Parent = tg;
                    tcProductionSetting.TabPages.Add(tg);
                }
            }
			TabControl tabControl=tcProductionSetting;
            if ( nEnabledCount!=0)
			{
                Size sParent=tabControl.Parent.Size;
                Size sDividers=tabControl.ItemSize;
				sDividers.Width = (sParent.Width - 50) / nEnabledCount;
				sDividers.Width = sDividers.Width < 150 ? 150 : sDividers.Width;
				tabControl.ItemSize = sDividers; 
			}
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel1.Focus();
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].WriteSettingData();
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Focus();
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].ReadSettingData();
        }

        private void ProductionSettingForm_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent == null)
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].ExitProductionSettings();
            }
            else
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].IntoProductionSettings();
            }
        }
    }
}

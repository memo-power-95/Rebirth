using AcuraLibrary.Forms;
using AcuraLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;
using Alpha._0.ModuleForms;

namespace Alpha.MENUForms
{
    public partial class MachineSetupForm : Form
    {
        public MachineSetupForm()
        {
            InitializeComponent();
            
        }

        private void MachineSetupForm_Load(object sender, EventArgs e)
        {
            //Tower
            MiddleLayer.SignalTowerF.TopLevel = false;
            MiddleLayer.SignalTowerF.Parent = plSignalTowerSetup;
            MiddleLayer.SignalTowerF.Show();
            //Mes form
            MiddleLayer.MesF.TopLevel = false;
            MiddleLayer.MesF.Parent = plMes;
            MiddleLayer.MesF.Show();
            MiddleLayer.MesF.Dock = DockStyle.Fill;

            //plSignalTowerSetup.Height = MiddleLayer.SignalTowerF.Height;
            Size sParent1=tabControl1.Parent.Size;
            Size sDividers1=tabControl1.ItemSize;
			sDividers1.Width = (sParent1.Width - 50) / tabControl1.TabPages.Count;
			sDividers1.Width = sDividers1.Width < 150 ? 150 : sDividers1.Width;
			tabControl1.ItemSize = sDividers1; 

            tcMotorSetup.TabPages.Clear();
             int nEnabledCount=0;
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                bool bHasContent=Stage.plMotionSetup.Controls.Count>0;
                if (Stage.plMotionSetup.Enabled&&bHasContent)
                {
                    nEnabledCount++;
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plMotionSetup.Parent = tg;
                    tcMotorSetup.TabPages.Add(tg);
                }
            }
            
			TabControl tabControl=tcMotorSetup;
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
            
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPageMes"])//your specific tabname
            {
                plMes.Focus();
                MiddleLayer.MesF.WriteMESData();
            }
            else
            {
                panel1.Focus();
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].WriteSettingData();
            }
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPageMes"])//your specific tabname
            {
                plMes.Focus();
                MiddleLayer.MesF.ReadMESData();
            }
            else
            {
                panel1.Focus();
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].ReadSettingData();
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPageMes"])//your specific tabname
            {
            }
        }
    }
}

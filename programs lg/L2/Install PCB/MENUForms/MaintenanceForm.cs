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
    public partial class MaintenanceForm : Form
    {
        public MaintenanceForm()
        {
            InitializeComponent();
        }

        private void MaintenanceForm_Load(object sender, EventArgs e)
        {
            tcMaintenance.TabPages.Clear();
            int nEnabledCount=0;
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                bool bHasContent=Stage.plMaintenance.Controls.Count>0;
                if (Stage.plMaintenance.Enabled&bHasContent)
                { 
                    nEnabledCount++;
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plMaintenance.Parent = tg;
                    tcMaintenance.TabPages.Add(tg);
                }
            }
			TabControl tabControl=tcMaintenance;
             if ( nEnabledCount!=0)
			{
                Size sParent=tabControl.Parent.Size;
                Size sDividers=tabControl.ItemSize;
				sDividers.Width = (sParent.Width - 50) / nEnabledCount;
				sDividers.Width = sDividers.Width < 150 ? 150 : sDividers.Width;
				tabControl.ItemSize = sDividers; 
			}
        }
    }
}

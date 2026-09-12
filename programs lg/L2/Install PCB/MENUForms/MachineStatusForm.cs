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
    public partial class MachineStatusForm : Form
    {
        public MachineStatusForm()
        {
            InitializeComponent();
        }

        private void MachineStatusForm_Load(object sender, EventArgs e)
        {
            int nEnabledCount=0;
            tcMachineStatus.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                bool bHasContent=Stage.plMachineStatus.Controls.Count>0;
                if (Stage.plMachineStatus.Enabled&&bHasContent)
                {
                    nEnabledCount++;
                    TabPage tg = new TabPage(Stage.Text);
                    tg.Padding = new Padding(0);
                    Stage.plMachineStatus.Parent = tg;
                    tcMachineStatus.TabPages.Add(tg);
                }
            }
            TabControl tabControl=tcMachineStatus;
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

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
    public partial class FlowChartForm : Form
    {
        public FlowChartForm()
        {
            InitializeComponent();
        }

        private void FlowChartForm_Load(object sender, EventArgs e)
        {
            int nEnabledCount=0;
            tcFlowInitial.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                bool bHasContent=Stage.plFlowInitial.Controls.Count>0;
                if (Stage.plFlowInitial.Enabled&&bHasContent)
                {
                    nEnabledCount++;
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plFlowInitial.Parent = tg;
                    tcFlowInitial.TabPages.Add(tg);
                }
            }
            TabControl tabControl=tcFlowInitial;
             if ( nEnabledCount!=0)
			{
                Size sParent=tabControl.Parent.Size;
                Size sDividers=tabControl.ItemSize;
				sDividers.Width = (sParent.Width - 50) / nEnabledCount;
				sDividers.Width = sDividers.Width < 150 ? 150 : sDividers.Width;
				tabControl.ItemSize = sDividers; 
			}
            nEnabledCount=0;
            tcFlowAuto.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                bool bHasContent=Stage.plFlowAuto.Controls.Count>0;
                if (Stage.plFlowAuto.Enabled&&bHasContent)
                {
                     nEnabledCount++;
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plFlowAuto.Parent = tg;
                    tcFlowAuto.TabPages.Add(tg);
                }
            }
                   
			tabControl=tcFlowAuto;
            if ( nEnabledCount!=0)
			{
                Size sParent=tabControl.Parent.Size;
                Size sDividers=tabControl.ItemSize;
				sDividers.Width = (sParent.Width - 50) / nEnabledCount;
				sDividers.Width = sDividers.Width < 150 ? 150 : sDividers.Width;
				tabControl.ItemSize = sDividers; 
			}
        }

        private void FlowChartForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                if (tabControl1.Width != 0)
                    tabControl1.ItemSize = new System.Drawing.Size(tabControl1.Width / 2 - 12, 50);
        }
    }
}

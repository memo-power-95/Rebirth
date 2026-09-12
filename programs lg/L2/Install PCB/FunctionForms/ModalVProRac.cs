using System;
using System.Windows.Forms;

namespace Alpha.FunctionForms
{
    public partial class ModalVProRac : Form
	{
		public ModalVProRac()
		{
			InitializeComponent();
            this.Show();
            this.Hide();
			this.FormClosing += Form1_FormClosing;
			
		}
		Panel pn_From;

		/// <summary>
		/// Override the Close Form event
		/// Do something
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.WindowsShutDown) return;
			fn_Close();
			e.Cancel = true;
			Timer.Enabled=false;
		}
		public void fnInheritFromPanel( Panel pn,Panel LastParents)
		{
			if (!this.Visible)
			{
				pn_From=LastParents;
				pn.Parent=pn_CogRecordContainer;
				this.Refresh();
				this.Show();
				this.FormBorderStyle=FormBorderStyle.SizableToolWindow;
				Timer.Enabled=true;
			}
			
		}

		private void ModalVProRac_FormClosing(object sender, FormClosingEventArgs e)
		{
			fn_Close();
		}

		private void fn_Close()
		{
			this.Hide();
			foreach (var item in pn_CogRecordContainer.Controls)
			{
				if (item is Panel)
				{
					((Panel)item).Parent=pn_From;
					pn_From.Refresh();
					return;
				}
			}	
		}

		private void nUP_CircleDiam_ValueChanged(object sender, EventArgs e)
		{
			//MiddleLayer.VProRACF.fn_SetCircleRadius(nUP_CircleDiam.Value);
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			
		}
	}
}

using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha.FunctionForms
{
	public partial class ExceptionReportForm : Form
	{
		private bool bTrackingEx = true;
		public bool TrackingEx
        {
			get { return bTrackingEx; }
			set { bTrackingEx = value; }
        }
		public ExceptionReportForm()
		{
			InitializeComponent();
		}
		public	bool fnAddException(Exception ex)
		{
			try
			{
				if (dGV_ExceptionList.InvokeRequired)
                {
                    dGV_ExceptionList.BeginInvoke((MethodInvoker)(() =>
						fnAddException(ex)
                    ));
                }
                else
                {
					
					if(!bTrackingEx)
                    {
						DateTime dtNow = DateTime.Now;
						dGV_ExceptionList.Rows.Add(dtNow.ToString("yyyy/MMM/dd"), dtNow.ToString("HH:mm:ss"), ex.TargetSite.DeclaringType.Name.ToString().Trim(), ex.Message.ToString().Trim(), ex.StackTrace.ToString().Trim());
					}
					return true;
                }
			}
			catch (Exception ex2)
			{
				this.fnAddException(ex2);
			}
			return false;
		}

		private void dGV_ExceptionList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			fnShowException(e);
		}

		private void fnShowException(DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
			fnMessageShow(e.RowIndex);
		}
		private async void fnMessageShow(int id)
		{
			bool bPosible=dGV_ExceptionList.Rows.Count-1>id;
			if (bPosible)
			{
				await Task.Run(() =>
				{
					//Data
					DataGridViewRow	row=dGV_ExceptionList.Rows[id];
					string sDate=row.Cells[dGV_cl_Date.Index].Value.ToString();
					string sTime=row.Cells[dGV_cl_Time.Index].Value.ToString();
					string sStackTrace=row.Cells[dGV_cl_StackTrace.Index].Value.ToString();
					string sMessage=row.Cells[dGV_cl_Message.Index].Value.ToString();
					string sModule=row.Cells[dGV_cl_Module.Index].Value.ToString();
					string sTittle = $"Exepcion {sDate}:{sTime} de Module: {sModule}";
					string sMessageDialog = "";
					sMessageDialog +=$"StackTrace:{Environment.NewLine} {sStackTrace}{Environment.NewLine}";
					sMessageDialog +=Environment.NewLine+$"Message:{Environment.NewLine} {sMessage}";
					MessageBox.Show(new Form { TopMost = true },sMessageDialog,sTittle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
				}).ConfigureAwait(false); 
			}
		}
		private void Clear()
		{
			if (dGV_ExceptionList.InvokeRequired)
                {
                    dGV_ExceptionList.BeginInvoke((MethodInvoker)(() =>
						Clear()
                    ));
                }
                else
                {
					dGV_ExceptionList.Rows.Clear();
                }
			
		}

		private void btn_Clear_Click(object sender, EventArgs e)
		{
			Clear();
		}

        private void cb_EnableTracking_CheckedChanged(object sender, EventArgs e)
        {
			bTrackingEx = !cb_EnableTracking.Checked;
		}
    }
}

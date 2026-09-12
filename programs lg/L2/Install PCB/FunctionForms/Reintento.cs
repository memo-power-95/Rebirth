using System;
using System.Windows.Forms;

namespace Alpha.FunctionForms
{
    public partial class Reintento : Form
    {
        /// <summary>
        /// DialogResult.Yes es un reintentar y Abort es no reintentar
        /// </summary>
        public DialogResult dResult = DialogResult.None;

        public Reintento()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.Show();
            this.Hide();
        }

        public void fnSetMessageAndButtons(string sMessage, bool bRetryEnabled, bool bSkipEnabled, bool bAbortEnabled)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    btnReintentar.Visible = (bRetryEnabled) ? true : false;
                    btnSaltar.Visible = (bSkipEnabled) ? true : false;
                    btnCancelar.Visible = (bAbortEnabled) ? true : false;
                    dResult = DialogResult.None;
                    label1.Text = sMessage;
                }));
            }
            else
            {
                btnReintentar.Visible = (bRetryEnabled) ? true : false;
                btnSaltar.Visible = (bSkipEnabled) ? true : false;
                btnCancelar.Visible = (bAbortEnabled) ? true : false;
                dResult = DialogResult.None;
                label1.Text = sMessage;
            }
        }

        public void fnSetTextMessageNShow(string strMessage, bool bRetryEnabled, bool bSkipEnabled, bool bAbortEnabled)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    btnReintentar.Visible = (bRetryEnabled) ? true : false;
                    btnSaltar.Visible = (bSkipEnabled) ? true : false;
                    btnCancelar.Visible = (bAbortEnabled) ? true : false;
                    dResult = DialogResult.None;
                    label1.Text = strMessage;
                    this.ShowDialog();
                }));
            }
            else
            {
                btnReintentar.Visible = (bRetryEnabled) ? true : false;
                btnSaltar.Visible = (bSkipEnabled) ? true : false;
                btnCancelar.Visible = (bAbortEnabled) ? true : false;
                dResult = DialogResult.None;
                label1.Text = strMessage;
                this.ShowDialog();
            }
        }

        public void fnChangeButtonRetryMessage(string msg)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                btnReintentar.Text = msg;
            }));
        }
        public void doHideWindow()
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                this.Hide();
            }));
        }
        private void btnReintentar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                dResult = DialogResult.Yes;
                this.Hide();
            }));
        }
        private void btnSaltar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                dResult = DialogResult.Ignore;
                this.Hide();
            }));
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                dResult = DialogResult.Abort;
                this.Hide();
            }));
        }
    }
}

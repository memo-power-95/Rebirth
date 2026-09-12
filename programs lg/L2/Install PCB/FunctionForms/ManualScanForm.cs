using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha.FunctionForms
{
    public partial class ManualScanForm : Form
    {
        public string sTextRead { get { return txtScanText.Text; } }

        public ManualScanForm()
        {
            InitializeComponent();
        }

        private void ManualScanForm_Load(object sender, EventArgs e)
        {
            txtScanText.Text = "";
            txtScanText.Focus();
            this.TopMost = true;
        }

        private void txtScanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.Hide();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtScanText.Text))
            {
                txtScanText.Text = "";
                MessageBox.Show("Error de cadena vacia o con espacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();
        }
    }
}

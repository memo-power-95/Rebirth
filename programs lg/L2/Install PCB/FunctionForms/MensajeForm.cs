using System;
using System.Windows.Forms;
using Alpha.Classes;

namespace Alpha
{
	public partial class MensajeForm : Form
    {
        public MensajeForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.Show();
            this.Hide();
        }

        public string CodigoLeido = "";
        public DialogResult dResult = DialogResult.None;      

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            dResult = DialogResult.Cancel;
            this.Hide();
        }

        /// <summary>
        /// Permite cambiar el mensaje mostrado en el cuadro de texto
        /// </summary>
        /// <param name="msg"></param>
        public void fnChangeMessage(string msg)
        {
            lblMensaje.SetText(msg);
        }

        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }
    }
}

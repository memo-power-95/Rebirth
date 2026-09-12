using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class LoadingMarqueeForm : Form
    {
        private string _Caption = string.Empty;
        public bool StopRefresh;

        public LoadingMarqueeForm()
        {
            InitializeComponent();
            picLogo.Image = Alpha.Properties.Resources.LG__logo;
        }

        public void SetCaption(string Caption)
        {
            _Caption = Caption;
        }

        public void RefreshUI()
        {
            Graphics g1 = picLogo.CreateGraphics();
            Graphics g2 = picText1.CreateGraphics();
            Graphics g3 = picText2.CreateGraphics();
            Font FontText1 = new System.Drawing.Font("Tahoma", 24, FontStyle.Bold);
            Font FontText2 = new System.Drawing.Font("Arial Black", 12, FontStyle.Regular);
            g1.DrawImage(picLogo.Image, 0, 0, picLogo.Width, picLogo.Height);

            int Count = 0;
            while (!StopRefresh)
            {
                string LoadMessage = "Cargando " + _Caption + " ";
                for (int i = 0; i <= (Count % 5); i++)
                {
                    LoadMessage += " .";
                }
                g3.Clear(Color.White);
                g3.DrawString(LoadMessage, FontText2, System.Drawing.Brushes.Black, 0, 0);
                g2.DrawString("N P 1.3.5 / S D K 2.0.2", FontText1, System.Drawing.Brushes.MidnightBlue, 160, 10);
                Count++;
                Thread.Sleep(100);
            }
        }

        private void picLogo_Click(object sender, System.EventArgs e)
        {

        }
    }
}

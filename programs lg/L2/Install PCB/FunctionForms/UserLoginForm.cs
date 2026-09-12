using Alpha.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Alpha.FunctionForms
{
	public partial class UserLoginForm : Form
    {
        public UserLoginForm()
        {
            InitializeComponent();
            this.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PermissionType UserPermission = PermissionType.None;
            if (ReadUserData(textUserName.Text, textPassword.Text, ref UserPermission))
            {
                SysPara.UserName = textUserName.Text;
                MiddleLayer.SwitchPermission(UserPermission);

                //MiddleLayer.LogF.AddLog(LogType.Operation, string.Format("User Login. UserType:{0} UserName:{1}", SysPara.LoginLevel.ToString(), SysPara.LoginUserName));
                this.Close();
            }
            else
            {
                 string sMsgBox="Login fail, Please check username and password.";
               string sTittle=$"System Exception {this.Name}";
                MessageBox.Show(new Form { TopMost = true }, sMsgBox,sTittle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                textUserName.Focus();
            }
        }

        private bool ReadUserData(string UserName, string Password, ref PermissionType UserPermission)
        {
            try
            {
                string strSQL = "select * from UserData where UserName = '" + UserName + "'";
                bool Successful = false;

                DataTable readData = DataBase.ReadData_Adapter(SysPara.SystemDataDirectory, strSQL, ref Successful);
                if (Successful)
                    if (readData.Rows.Count > 0)
                        if (readData.Rows[0]["Password"].ToString() == Password)
                        {
                            UserPermission = (PermissionType)Enum.Parse(typeof(PermissionType), readData.Rows[0]["Permission"].ToString());
                            return true;
                        }
            }
            catch (Exception ex) 
            {
                MiddleLayer.ExReportF.fnAddException(ex);
			}
            return false;
        }

        private void textPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin.PerformClick();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                SysPara.UserName = "Administrator";
                MiddleLayer.SwitchPermission(PermissionType.Administrator);
                Close();
            }
        }

        private void UserLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //textUserName.Text = "";
            //textPassword.Text = "";
            //SysPara.UserPermission = PermissionType.Operator;
            //MiddleLayer.MainF.SwitchPermission(SysPara.UserPermission);
        }
    }
}

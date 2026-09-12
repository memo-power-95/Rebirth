using Alpha.Classes;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using libzkfpcsharp;
using Sample;
using System.IO;
using System.Threading;
using System.Drawing;

namespace Alpha.MENUForms
{
	public partial class UserSettingForm : Form
    {

        #region Finger
        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        public bool IsRegister = false;
        public bool bIdentify = false;
        byte[] FPBuffer;
        public int RegisterCount = 0;
        public const int REGISTER_FINGER_COUNT = 3;
        public string sUserName = "";
        public string sUserPermission = "";
        public List<string> lstPath = new List<string>();
        public List<byte[]> lstContent = new List<byte[]>();
        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];//登记模板
        byte[] CapTmp = new byte[2048];//比对模板
        public int cbCapTmp = 2048;
        public int cbRegTmp = 0;
        public string _strFingerParam = "";
        int iFid = 1;
        public List<byte[]> lstFinger = new List<byte[]>();
        private int mfpWidth = 0;
        private int mfpHeight = 0;

        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        #endregion

        public UserSettingForm()
        {
            InitializeComponent();

            foreach (var item in Enum.GetValues(typeof(PermissionType)))
            {
                if (item.ToString() != "None")
                {
                    cbSelectedPermission.Items.Add(item); 
                    cbAddUserPermission.Items.Add(item);
                }
            }
            cbSelectedPermission.SelectedIndex = 0;
            cbAddUserPermission.SelectedIndex = 0;
            ReadAllUserData();
            ReadPermission();
        }

        private void ReadAllUserData()
        {
            try
            {
                string strSQL = "select UserName,Permission from UserData";
                bool Successful = false;

                DataTable readData = DataBase.ReadData_Adapter(SysPara.SystemDataDirectory, strSQL, ref Successful);
                DataColumn FillCol = new DataColumn();
                readData.Columns.Add(FillCol);
                if (Successful)
                {
                    dgvUserList.DataSource = readData;
                    dgvUserList.Columns[0].Width = 250;
                    dgvUserList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvUserList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvUserList.Columns[2].Width = 0;
                    dgvUserList.Columns[2].HeaderText = "";
                    dgvUserList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvUserList.ScrollBars=ScrollBars.Both;
                }
            }
            catch (Exception ex)
            {
                MiddleLayer.ExReportF.fnAddException(ex);
            }
        }

        private bool ReadPermission()
        {
            string strSQL = "select * from PermissionSetup";
            bool Successful = false;
            DataTable readData = DataBase.ReadData_Adapter(SysPara.SystemDataDirectory, strSQL, ref Successful);
            if (Successful)
            {
                if (readData.Rows.Count > 0)
                {
                    dgvRightsConfig.DataSource = readData;
                    dgvRightsConfig.ScrollBars=ScrollBars.Both;
                    dgvRightsConfig.ClearSelection();
                    dgvRightsConfig.Columns[0].ReadOnly = true;
                    for (int i = 0; i < readData.Columns.Count; i++)
                    {
                        dgvRightsConfig.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dgvRightsConfig.Columns[i].Width = 150;
                        dgvRightsConfig.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    return true;
                }
            }
            return false;
        }

        private bool WritePermission()
        {
            bool IsUserExist = false;
            for (int row = 0; row < dgvRightsConfig.Rows.Count; row++)
            {
                string strSQL = "update PermissionSetup set ";
                for (int column = 1; column < dgvRightsConfig.Columns.Count; column++)
                    strSQL += String.Format("[{0}]={1}{2}", dgvRightsConfig.Columns[column].Name, dgvRightsConfig[column, row].Value, (column == (dgvRightsConfig.Columns.Count - 1)) ? "" : ",");
                strSQL += " where [Permission]= '" + dgvRightsConfig[0, row].Value.ToString() + "'";
                int result = DataBase.DataBaseExecute(SysPara.SystemDataDirectory, strSQL);
                if (result != 0)
                {
                    IsUserExist = false;
                    break;
                }
                else
                {
                    IsUserExist = true;
                }
            }
            return IsUserExist;
        }

        private void dgvUserList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvUserList.CurrentCell != null)
            {
                if (dgvUserList.CurrentCell.RowIndex >= 0)
                {
                    int tmpIndex = Array.IndexOf(Enum.GetNames(typeof(PermissionType)), dgvUserList[1, dgvUserList.CurrentCell.RowIndex].Value.ToString());
                    cbSelectedPermission.SelectedIndex = tmpIndex;
                    textSelectedUserName.Text = dgvUserList[0, dgvUserList.CurrentCell.RowIndex].Value.ToString();
                    btnDeleteUser.Enabled = true;
                    btnModifyPermission.Enabled = true;
                    return;
                }
            }
            cbSelectedPermission.SelectedIndex = -1;
            textSelectedUserName.Text = "";
            btnDeleteUser.Enabled = false;
            btnModifyPermission.Enabled = false;
        }

        private void btnUserModify_Click(object sender, EventArgs e)
        {
            ModifyUserPermission(textSelectedUserName.Text, cbSelectedPermission.SelectedItem.ToString());
            ReadAllUserData();
        }

        public bool ModifyUserPermission(string UserName, string UserPermission)
        {
            try
            {
                string strSQL = "update [UserData] set [Permission]='" + UserPermission + "' where [UserName] ='" + UserName + "'";
                int result = DataBase.DataBaseExecute(SysPara.SystemDataDirectory, strSQL);
                if (result == 0)
                    return true;
            }
            catch (Exception ex) 
            {
                MiddleLayer.ExReportF.fnAddException(ex);
            }
            return false;
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            DeleteUser(textSelectedUserName.Text);
            ReadAllUserData();
        }

        private bool DeleteUser(string UserName)
        {
            try
            {
                string strSQL = "delete * from UserData where UserName ='" + UserName + "'";
                int result = DataBase.DataBaseExecute(SysPara.SystemDataDirectory, strSQL);
                if (result == 0)
                    return true;
            }
            catch (Exception ex) 
            {
                MiddleLayer.ExReportF.fnAddException(ex);
            }
            return false;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (CheckUserWhetherExist(textAddUserName.Text))
            {
                 string sMsgBox="The user name already exist!";
                  MethodBase MB = MethodBase.GetCurrentMethod();
                 string sTittle=$"System Exception {this.Name}";
                MessageBox.Show(new Form { TopMost = true }, sMsgBox,sTittle, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                textAddUserName.Focus();
                return;
            }

            if (textPassword.Text != textPasswordConfirm.Text)
            {
                 string sMsgBox="Password not match!";
                string sTittle=$"System Exception {this.Name}";
                MessageBox.Show(new Form { TopMost = true }, sMsgBox,sTittle, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                textPassword.Focus();
                return;
            }

            int TmpIndex = dgvUserList.Rows.Count;
            if (AddUser(textAddUserName.Text, textPassword.Text, cbAddUserPermission.SelectedItem.ToString()))
            {
                ReadAllUserData();
                textAddUserName.Text = string.Empty;
                textPassword.Text = string.Empty;
                textPasswordConfirm.Text = string.Empty;
                dgvUserList[0, TmpIndex].Selected = true;
            }
        }

        private bool CheckUserWhetherExist(string UserName)
        {
            string strSQL = "select * from UserData where UserName ='" + UserName + "'";
            bool Successful = false;

            DataTable readData = DataBase.ReadData_Adapter(SysPara.SystemDataDirectory, strSQL, ref Successful);
            if (Successful)
                if (readData.Rows.Count > 0)
                    return true;
            return false;
        }

        private bool AddUser(string UserName, string UserPassword, string UserLevel)
        {
            try
            {
                string strSQL = "insert into UserData values ('" + UserName + "','" + UserPassword + "','" + UserLevel + "')";
                int result = DataBase.DataBaseExecute(SysPara.SystemDataDirectory, strSQL);
                if (result == 0)
                    return true;
            }
            catch (Exception ex) 
            {
                MiddleLayer.ExReportF.fnAddException(ex);
            }
            return false;
        }

        private void AddUser_TextChanged(object sender, EventArgs e)
        {
            if (textAddUserName.Text != string.Empty && textPassword.Text != string.Empty && textPasswordConfirm.Text != string.Empty)
                btnAddUser.Enabled = true;
            else
                btnAddUser.Enabled = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel2.Focus();
            WritePermission();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ReadPermission();
        }

		private void UserSettingForm_Paint(object sender, PaintEventArgs e)
		{
			
            
		}

		private void UserSettingForm_VisibleChanged(object sender, EventArgs e)
		{
             //lbl_UserData.SetBackColor(MiddleLayer.ModeColors.Background);
			//lbl_TypePermisions.SetBackColor(MiddleLayer.ModeColors.Background);
		}

        

        #region Finger
        //protected override void DefWndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case MESSAGE_CAPTURED_OK:
        //            {
        //                MemoryStream ms = new MemoryStream();
        //                BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
        //                Bitmap bmp = new Bitmap(ms);
        //                MiddleLayer.UserSettingF.picFPImg.Image = bmp;
        //                if (IsRegister)
        //                {
        //                    int ret = zkfp.ZKFP_ERR_OK;
        //                    if (RegisterCount > 0 && zkfp2.DBMatch(mDBHandle, CapTmp, RegTmps[RegisterCount - 1]) <= 0)//判断三次中是否按下的是同一根手指
        //                    {
        //                       toolStripStatusLabel1.Text = "Please press the same finger 3 times for the enrollment！";
        //                        return;
        //                    }
        //                    Array.Copy(CapTmp, RegTmps[RegisterCount], cbCapTmp);//复制当前指纹的byte数组到预定义三块指纹中的一块
        //                    String strBase64 = zkfp2.BlobToBase64(CapTmp, cbCapTmp);//byte字节数组转字符串          
        //                    byte[] blob = zkfp2.Base64ToBlob(strBase64);//???再转为byte字节数组
        //                    RegisterCount++;//登记次数加1
        //                    if (RegisterCount >= REGISTER_FINGER_COUNT)//登记次数大于等于3次
        //                    {
        //                        RegisterCount = 0;
        //                        if (zkfp.ZKFP_ERR_OK == (ret = zkfp2.DBMerge(mDBHandle, RegTmps[0], RegTmps[1], RegTmps[2], RegTmp, ref cbRegTmp)))//判断三枚指纹是否融合成功
        //                        {
        //                            //before refister,judge the current fingerprint if it is registered or has another premission
        //                            string strPath = Application.StartupPath + "\\FinggerParam";
        //                            GetPath(new DirectoryInfo(strPath), "*.txt");
        //                            GetBytes(lstPath);
        //                            for (int i = 0; i < lstContent.Count; i++)
        //                            {
        //                                ret = zkfp2.DBMatch(mDBHandle, CapTmp, lstContent[i]);
        //                                if (0 < ret)
        //                                {
        //                                    string userPremission = lstPath[i].Substring(lstPath[i].LastIndexOf("\\") + 1);//admin_Adminstrator_20191111135634.txt
        //                                    userPremission = userPremission.Substring(0, userPremission.IndexOf("."));//admin_Adminstrator_20191111135634
        //                                    string[] userAndPremission = userPremission.Split('_');
        //                                    if (sUserPermission == userAndPremission[1])
        //                                    {
        //                                        MessageBox.Show("This finger was already registered,please change another finger!");
        //                                        return;
        //                                    }
        //                                    else
        //                                    {
        //                                        MessageBox.Show("This finger was already registered with the " + userAndPremission[1] + "Premission,please change another fingerprint");
        //                                        return;
        //                                    }
        //                                }
        //                            }
        //                            SaveTmp(RegTmp, sUserName + "_" + sUserPermission + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
        //                            iFid++;
        //                            toolStripStatusLabel1.Text = "enroll success！";
        //                        }
        //                        else
        //                        {
        //                            toolStripStatusLabel1.Text = "enroll fail, error code=" + ret;
        //                        }
        //                        IsRegister = false;//登记之后改变标志位
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        toolStripStatusLabel1.Text = "You need to press " + (REGISTER_FINGER_COUNT - RegisterCount) + " times fingerprint";
        //                    }
        //                }
        //                else
        //                {
        //                    if (bIdentify)
        //                    {
        //                        string strPath = Application.StartupPath + "\\FinggerParam";
        //                        GetPath(new DirectoryInfo(strPath), "*.txt");

        //                        GetBytes(lstPath);
        //                        for (int i = 0; i < lstContent.Count; i++)
        //                        {
        //                            int ret = zkfp2.DBMatch(mDBHandle, CapTmp, lstContent[i]);
        //                            if (0 < ret)
        //                            {
        //                                toolStripStatusLabel1.Text = "Login success!";
        //                                //login success,get the file name
        //                                string OrgUser = SysPara.UserName;
        //                                string userPremission = lstPath[i].Substring(lstPath[i].LastIndexOf("\\") + 1);//admin_Adminstrator_20191111135634.txt
        //                                userPremission = userPremission.Substring(0, userPremission.IndexOf("."));//admin_Adminstrator_20191111135634
        //                                string[] userAndPremission = userPremission.Split('_');
        //                                //change the user name and soft premission
        //                                SysPara.UserName = userAndPremission[0];
        //                                PermissionType UserPermission = (PermissionType)Enum.Parse(typeof(PermissionType), userAndPremission[1]);
        //                                MiddleLayer.SwitchPermission(UserPermission);
        //                                //  MiddleLayer.LogF.AddLog(LogType.Operation, string.Format("User Login.   UserType={0}   UserName={1}    by FingenPrint", UserPermission.ToString(), SysPara.UserName));

                                   
        //                                if (OrgUser != SysPara.UserName)
        //                                {
                                           
        //                                    if (!MiddleLayer.MainF.MENU_TableLayout[(int)MiddleLayer.MainF.MENU_SelectPage].Enabled)
        //                                        MiddleLayer.MainF.SwitchMainPage(MainForm.MENU_PageType.MachineStatus);
        //                                    MiddleLayer.MainF.RefreshMenuBackcolor();
        //                                }

        //                                return;
        //                            }
        //                        }
        //                        toolStripStatusLabel1.Text = "Match finger fail，Please confirm  your fingerprint is registered!";
        //                        MessageBox.Show("Login fail，Please confirm  your fingerprint is registered!");
        //                    }
        //                }
        //            }
        //            break;

        //        default:
        //            base.DefWndProc(ref m);
        //            break;
        //    }
        //}

        //public void UnInitFinger()
        //{
        //    bIsTimeToDie = true;
        //    RegisterCount = 0;
        //    Thread.Sleep(1000);
        //    zkfp2.CloseDevice(mDevHandle);
        //    //MiddleLayer.UserSettingF.toolStripStatusLabel1.Text = "The device has been closed!";

        //    zkfp2.Terminate();
        //    cbRegTmp = 0;
        //    //MiddleLayer.UserSettingF.btnAddFinger.Enabled = false;
        //    //MiddleLayer.UserSettingF.btnTestLoginFinger.Enabled = false;
        //}

        public void InitFinger()
        {
            bIdentify = true;
            FormHandle = this.Handle;
            int ret = zkfperrdef.ZKFP_ERR_OK;
            if ((ret = zkfp2.Init()) == zkfperrdef.ZKFP_ERR_OK)
            {
                int nCount = zkfp2.GetDeviceCount();
                if (nCount > 0)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        mDevHandle = zkfp2.OpenDevice(i);
                        if (IntPtr.Zero == mDevHandle)
                        {
                            MessageBox.Show("OpenDevice fail！");
                            //MiddleLayer.UserSettingF.btnAddFinger.Enabled = false;
                            //MiddleLayer.UserSettingF.btnTestLoginFinger.Enabled = false;
                            return;
                        }
                        mDBHandle = zkfp2.DBInit();
                        if (IntPtr.Zero == mDBHandle)
                        {
                            MessageBox.Show("Init DB fail！");
                            zkfp2.CloseDevice(mDevHandle);
                            mDevHandle = IntPtr.Zero;
                            //MiddleLayer.UserSettingF.btnAddFinger.Enabled = false;
                            //MiddleLayer.UserSettingF.btnTestLoginFinger.Enabled = false;
                            return;
                        }
                    }
                }
                else
                {
                    zkfp2.Terminate();
                    MessageBox.Show("No device connected!");
                    //MiddleLayer.UserSettingF.btnAddFinger.Enabled = false;
                    //MiddleLayer.UserSettingF.btnTestLoginFinger.Enabled = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Initialize fail, ret=" + ret + " !");
                //MiddleLayer.UserSettingF.btnAddFinger.Enabled = false;
                //MiddleLayer.UserSettingF.btnTestLoginFinger.Enabled = false;
                return;
            }

            //MiddleLayer.UserSettingF.btnAddFinger.Enabled = true;
            //MiddleLayer.UserSettingF.btnTestLoginFinger.Enabled = true;
            RegisterCount = 0;
            cbRegTmp = 0;
            iFid = 1;
            for (int i = 0; i < 3; i++)
            {
                RegTmps[i] = new byte[2048];
            }
            byte[] paramValue = new byte[4];
            int size = 4;
            zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);//获取图像宽
            zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

            size = 4;
            zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);//获取图像高
            zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

            FPBuffer = new byte[mfpWidth * mfpHeight];

            Thread captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();
            bIsTimeToDie = false;
            // MiddleLayer.UserSettingF.toolStripStatusLabel1.Text = "Open success！";
        }

        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);//已经采集指纹已byte字节数组形式存在
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                }
                Thread.Sleep(200);
            }
        }

        //private bool CheckUserWhetherExist(string UserName)
        //{
        //    string strSQL = "select * from UserData where UserName ='" + UserName + "'";
        //    bool Successful = false;

        //    DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
        //    if (Successful)
        //        if (readData.Rows.Count > 0)
        //            return true;
        //    return false;
        //}

        //保存指纹模板
        private void SaveTmp(byte[] Tmps, string sName)
        {
            try
            {
                string strPath = Application.StartupPath + "\\FinggerParam\\" + sName + ".txt";
                File.WriteAllBytes(strPath, Tmps);
            }
            catch (Exception ex)
            {
            }
        }

        //获取保存文件路径
        public void GetPath(DirectoryInfo directory, string patten)
        {
            lstPath.Clear();
            if (directory.Exists || patten.Trim() != string.Empty)
            {
                foreach (FileInfo info in directory.GetFiles(patten))
                {
                    string path = info.FullName;
                    lstPath.Add(path);
                }
            }
        }

        //获取数组
        public void GetBytes(List<string> list)
        {
            lstContent.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                byte[] Base = File.ReadAllBytes(list[i]);
                lstContent.Add(Base);
            }
        }

        #endregion

 
    }
}

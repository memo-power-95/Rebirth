using Alpha.Classes;
using Alpha.FunctionForms;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using NPSDK;
using static Alpha.FunctionForms.LogForm;
using System.Threading;
using AcuraLibrary.Forms;
using System.Threading.Tasks;
using Alpha;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using TcpipHelper;
using LogManager;

namespace Alpha
{
    public partial class MainForm : Form
    {
        
        #region VARIABLES
        public MENU_PageType MENU_SelectPage = MENU_PageType.MachineStatus;
        //public MENU_PageType MENU_SelectPage = MENU_PageType.MachineStatus;
        public TableLayoutPanel[] MENU_TableLayout;
        private bool IsOpenMENU = false;
        private bool IsOpenBottomMenu = true;
        private float[] tlMainOldSize = new float[6];
        private JTimer jt_TimerOf2Ss = new JTimer();

        private bool bAlmLog_GNR = true;
        private bool bAlmLog_I = true;
        private bool bAlmLog_W = true;
        private bool bAlmLog_K = true;
        private bool bAlmLog_E = true;
        private PictureBox[] MENU_Picture;
        private MENU_PageType1 MENU_SelectPage1 = MENU_PageType1.Home;
        KeyboardHook key = new KeyboardHook();
        MouseHook mouse = new MouseHook();
        public static JTimer PermissionTimer = new JTimer();
        public enum MENU_PageType
        {
            MachineStatus = 0,
            MachineSetup,
            RecipeEditor,
            ProductionSetting,
            Maintenance,
            NPFlowChart,
            UserSetting,
            Log,
            About,
            Exit,
            DarkMode,
            Hide,
        }

        #endregion VARIABLES
        public MainForm()
        {
            InitializeComponent();
            MENU_Picture = new PictureBox[] { MENU_Home, MENU_Product, MENU_Hard, MENU_Manual, MENU_Check, MENU_System, MENU_AddUser, MENU_Mes, MENU_Rapid, MENU_Data, RecipeEditorForm, MENU_Exit };
            //MENU_TableLayout = new TableLayoutPanel[] { tlMENU_MachineStatus, tlMENU_MachineSetup, tlMENU_RecipeEditor, tlMENU_ProductionSetting, tlMENU_Maintenance, tlMENU_FlowChart, tlMENU_UserSetting, tlMENU_Log, tlMENU_About, tlMENU_Exit };
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileInfo fileInfo = new FileInfo(assembly.Location);
          
        }
        public void RefreshMenuBackcolor()
        {
          
        }
 
        /// <summary>
        /// 选择用户权限
        /// </summary>
        /// <param name="Permission"></param>
        public void SwitchPermission(PermissionType Permission)
        {
            string strSQL = "select * from PermissionSetup where Permission ='" + Permission.ToString() + "'";
            bool Successful = false;

            DataTable readData = DataBase.ReadData_Adapter(SysPara.SystemDataDirectory, strSQL, ref Successful);
            if (Successful)
            {
                if (readData.Rows.Count > 0)
                {
                    MENU_Product.Enabled = Convert.ToBoolean(readData.Rows[0]["Product"]);
                    MENU_Manual.Enabled = Convert.ToBoolean(readData.Rows[0]["Manual"]);
                    MENU_Check.Enabled = Convert.ToBoolean(readData.Rows[0]["Check"]);
                    MENU_System.Enabled = Convert.ToBoolean(readData.Rows[0]["System"]);
                    MENU_AddUser.Enabled = (Permission == PermissionType.Administrator);
                    MENU_Mes.Enabled = Convert.ToBoolean(readData.Rows[0]["Mes"]);
                    MENU_Rapid.Enabled = Convert.ToBoolean(readData.Rows[0]["Rapid"]);
                    MENU_Data.Enabled = Convert.ToBoolean(readData.Rows[0]["Data"]);
                    RecipeEditorForm.Enabled = Convert.ToBoolean(readData.Rows[0]["Vision"]);
                    MENU_Exit.Enabled = Convert.ToBoolean(readData.Rows[0]["Exit"]);
                    MENU_Hard.Enabled = Convert.ToBoolean(readData.Rows[0]["Hard"]);
                    //MENU_System.Enabled = Convert.ToBoolean(readData.Rows[0]["Hard"]);
                }
            }
        }
        public enum MENU_PageType1
        {
            Save,
            Login,
            Reset,
            Run,
            Pause,
            Stop,
            Home,
            Product,
            Hard,
            Manual,
            Check,
            System,
            AddUser,
            Mes,
            Rapid,
            Data,
            Vision,
            Exit,
            Robot,
            LifeSpan,
        }
        private void ShowhMainPage(dynamic ShowPage)
        {
            if (ShowPage == null)
                return;
            //Hide Labels Unuseful

            foreach (Control control in plMainShow.Controls)
            {
                control.Parent = null;
                control.SetVisible(false);
            }
            if (ShowPage.GetType().IsSubclassOf(typeof(Form)))
            {
                ShowPage.TopLevel = false;
                ShowPage.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                ShowPage.WindowState = FormWindowState.Maximized;
                ShowPage.Dock = DockStyle.Fill;
            }
            else
                ShowPage.Dock = DockStyle.Fill;
            ShowPage.Parent = plMainShow;
            ShowPage.Show();
        }

        private void exDarkMode(bool bDarkMode)
        {
            bool IsLogin = (SysPara.UserPermission != PermissionType.None);
            Color cl_Background = MiddleLayer.ModeColors.Background;
            Color cl_Disable = MiddleLayer.ModeColors.Disable;
        }
        public void fnSwitchDarkMode()
        {
            if (SysPara.MainPara.bDarkModeButtonEnabled)
            {
                SysPara.MainPara.bDarkModeActive = !SysPara.MainPara.bDarkModeActive;
                exDarkMode(SysPara.MainPara.bDarkModeActive);//bDarkModeSwitch);
            }
        }
        public void fnStart()
        {
            //if (SysPara.bOB_SMMachineReady)
            //    MiddleLayer.ConveyorF.OB_SMMachineReady.On();
            //else
            //    MiddleLayer.ConveyorF.OB_SMMachineReady.Off();
            //if(SysPara.bOB_UpCvyMtRev)
            //    MiddleLayer.ConveyorF.OB_UpCvyMtRev.On();

            switch (SysPara.SystemMode)
            {
                case RunMode.IDLE:
                    SysPara.RunSecond = 0;
                    SysPara.StopSecond = 0;
                    SysPara.OperationSecond = 0;
                    SysPara.StartWorkTM = DateTime.Now;
                    MiddleLayer.FlowCtrl.RunReset();
                    MiddleLayer.GantryF.tm_GantryWork.Stop();
                    //MiddleLayer.StartRun();
                    break;
                case RunMode.AUTO:
                    MiddleLayer.GantryF.RunTM.Restart();
                    MiddleLayer.GantryF.OB_RobotPause.Off();
                    MiddleLayer.GantryF.OB_RobotContinue.On();
                    MiddleLayer.GantryF.FeederTM.Restart();
                    Thread.Sleep(300);
                    MiddleLayer.GantryF.OB_RobotContinue.Off();
                    if (MiddleLayer.GantryF.IB_RobotBusy.Off())
                    {
                        MiddleLayer.GantryF.OB_RobotPause.Off();
                        MiddleLayer.GantryF.OB_RobotContinue.On();
                        MiddleLayer.GantryF.FeederTM.Restart();
                        Thread.Sleep(300);
                        MiddleLayer.GantryF.OB_RobotContinue.Off();
                    }
                    MiddleLayer.StartRun();


                    return;
            }
        }
        public void SwitchMainPage(MENU_PageType PageType)
        {
            MENU_PageType oldPage = MENU_SelectPage;
            MENU_SelectPage = PageType;
            RefreshMenuBackcolor();
            switch (PageType)
            {
                case MENU_PageType.MachineStatus:
                    ShowhMainPage(MiddleLayer.MachineStatusF);
                    break;
                case MENU_PageType.MachineSetup:
                    if (!SysPara.bExpertMode)
                    {
                        MiddleLayer.StopRun();
                    }
                    ShowhMainPage(MiddleLayer.MachineSetupF);
                    break;
                case MENU_PageType.RecipeEditor:
                    if (!SysPara.bExpertMode)
                    {
                        MiddleLayer.StopRun();
                    }
                    ShowhMainPage(MiddleLayer.RecipeEditorF);
                    break;
                case MENU_PageType.ProductionSetting:
                    if (!SysPara.bExpertMode)
                    {
                        MiddleLayer.StopRun();
                    }
                    ShowhMainPage(MiddleLayer.ProductionSettingF);
                    break;
                case MENU_PageType.Maintenance:
                    if (!SysPara.bExpertMode)
                    {
                        MiddleLayer.StopRun();
                    }
                    ShowhMainPage(MiddleLayer.MaintenanceF);
                    break;
                case MENU_PageType.NPFlowChart:
                    ShowhMainPage(MiddleLayer.FlowChartF);
                    break;
                case MENU_PageType.UserSetting:
                    if (!SysPara.bExpertMode)
                    {
                        MiddleLayer.StopRun();
                    }
                    ShowhMainPage(MiddleLayer.UserSettingF);
                    break;
                case MENU_PageType.Log:
                    ShowhMainPage(MiddleLayer.LogF);
                    break;
                case MENU_PageType.About:
                    ShowhMainPage(MiddleLayer.AboutF);
                    break;
                case MENU_PageType.Exit:
                    DialogResult result = MessageBox.Show(new Form { TopMost = true }, "Cerrar programa?", "System Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                    switch (result)
                    {
                        case DialogResult.OK:

                            MiddleLayer.ConveyorF.isdownInitialOK = false;
                            MiddleLayer.StopRun();
                            MiddleLayer.DisposeProject();
                            Application.DoEvents();
                            SysPara.bCableDetectEnable = false;
                            MiddleLayer.ConveyorF.OB_SM_UpFaliBoard.Off();
                            MiddleLayer.ConveyorF.OB_SM_UpBoardReady.Off();
                            MiddleLayer.ConveyorF.OB_SMMachineReady.Off();
                            MiddleLayer.ConveyorF.OB_SM_DownCallBoard.Off();
                            MiddleLayer.ConveyorF.OB_SM_DownBoardReady.Off();

                            Close();
                            System.Environment.Exit(0);
                            break;
                        case DialogResult.Cancel:
                            MENU_SelectPage = oldPage;
                            break;
                    }
                    break;
                case MENU_PageType.Hide:
                    MENU_SelectPage = oldPage;
                    SwitchMainPage(MENU_SelectPage);
                    break;
                case MENU_PageType.DarkMode:
                    MENU_SelectPage = oldPage;
                    fnSwitchDarkMode();
                    break;
            }
        }
        public static void LabelsDarkMode(Control control)
        {
            //if (control is Label)
            //{
            //    Label lbl = (Label)control;
            //    if ((string)lbl.Tag == "MenuColors")
            //        lbl.BackColor = MiddleLayer.ModeColors.Background;
            //}
            //else
            //    foreach (Control child in control.Controls)
            //    {
            //        LabelsDarkMode(child);
            //    }
        }
        
        private void MENU_Home_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));

        }

        private void MENU_Product_Click(object sender, EventArgs e)
        {

        }

        private void MENU_Hard_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));

        }

        private void MENU_Exit_Click(object sender, EventArgs e)
        {
            MESLib.CommParas.MesManager.MesClose();
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void MENU_Rapid_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void MENU_AddUser_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void MENU_Check_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void MENU_System_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void MENU_Login_Click(object sender, EventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click Switch User button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            string OrgUser = SysPara.UserName;
            UserLoginForm UserLoginF = new UserLoginForm();
            UserLoginF.ShowDialog();

            //if (OrgUser != SysPara.UserName)
            //{
            //    if (!MENU_TableLayout[(int)MENU_SelectPage].Enabled)
            //        SwitchMainPage(MENU_PageType.MachineStatus);
            //    RefreshMenuBackcolor();
            //}
        }

        private void MENU_Mes_Click(object sender, EventArgs e)
        {
            //string ItemName = Convert.ToString(((Control)sender).Tag);
            //MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            //SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void MENU_Data_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void MENU_Vision_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void pbMotorControl_Click(object sender, EventArgs e)
        {

        }

        private void SwitchUser_Click(object sender, EventArgs e)
        {

        }

        private void pbMotorControl_Click_1(object sender, EventArgs e)
        {
            //MiddleLayer.ServoOff();
            Thread.Sleep(1000);
            MiddleLayer.ServoOn();
            //MiddleLayer.GantryF.OB_MotorZBrake.On();
            MiddleLayer.MotorJogF.Show();
        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            PermissionTimer.Restart();
            key.OnKeyDownEvent += new KeyEventHandler(hook_OnKeyDownEvent);
            key.Start();
            mouse.OnMouseActivity += new MouseEventHandler(mouse_OnMouseActivity);
            mouse.Start();
            MiddleLayer.SwitchPermission(PermissionType.Operator);
            SwitchMainPage(MENU_SelectPage);
            MiddleLayer.OpenRecipe(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            MiddleLayer.GantryF.ReadAxisPara();
            MiddleLayer.GantryF.processData.InitCount();
            //MiddleLayer.switchLanguage = new SwitchLanguage();


            if (MESLib.CommParas.Frm_AlarmTable == null)
                MESLib.CommParas.Frm_AlarmTable = new MESLib.Controls.AlarmTable();
            if (MESLib.CommParas.Frm_Logs == null)
                MESLib.CommParas.Frm_Logs = new MESLib.Controls.Logs_Winform();
            if (MESLib.CommParas.Frm_GEM == null)
                MESLib.CommParas.Frm_GEM = new MESLib.Controls.GEM_Winform();
            if (MESLib.CommParas.Frm_ECV == null)
                MESLib.CommParas.Frm_ECV = new MESLib.Controls.ECV_Winform();
            if (MESLib.CommParas.Frm_ModuleList == null)
                MESLib.CommParas.Frm_ModuleList = new MESLib.Controls.ModuleList_Winform();
            if (MESLib.CommParas.Frm_MainHeader == null)
                MESLib.CommParas.Frm_MainHeader = new MESLib.Controls.MainHeader();

            panelBottom.Controls.Add(MESLib.CommParas.Frm_AlarmTable);
            panelHeader.Controls.Add(MESLib.CommParas.Frm_MainHeader);
            MESLib.CommParas.Frm_AlarmTable.Dock = DockStyle.Fill;
            MESLib.CommParas.Frm_MainHeader.Dock = DockStyle.Fill;

            try
            {
                MiddleLayer.cfxHandler.OpenEndpoint();
                MiddleLayer.cfxHandler.StationOnline();
                MiddleLayer.cfxHandler.StationStateChanged(CFX.Structures.ResourceState.NST, DateTime.Now);
            }
            catch (Exception ex)
            {

            }


            MESLib.CommParas.MesCenter.StopRunning = new Action(() =>
            {
                btnStop_Click(null, null);
            });
            MESLib.CommParas.MesCenter.PauseRunning = new Action(() =>
            {
                btnPause_Click(null, null);
            });

            // version
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version version = assembly.GetName().Version;
            toolStripStatusLabelversion.Text = "Ver" + " " + version.ToString();
        }
        private void hook_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() != "Scroll")
              PermissionTimer.Restart();
        }
        private void mouse_OnMouseActivity(object sender, MouseEventArgs e)
        {
            PermissionTimer.Restart();
        }
        public void fnInitial()
        {
            MiddleLayer.GantryF.InitTM.Restart();
            SysPara.bBypassMode = MiddleLayer.ProcessF.GetSettingValue("PSet", "dcEnableCheckProcess");
            SysPara.bDryCycle = !MiddleLayer.ProcessF.GetSettingValue("PSet", "dcEnableCheckProcess") && MiddleLayer.ProcessF.GetSettingValue("PSet", "IsDryCycleMode");
            SysPara.bCheckPCBcodes = MiddleLayer.ProcessF.GetSettingValue("PSet", "disableCheckPCBcodes");
            switch (SysPara.SystemMode)
            {
                case RunMode.IDLE:
                case RunMode.AUTO:
                    if (SysPara.SystemInitialOk)
                    {
                        string sMsgBox = "Automatic production now, Are you sure to exit automatic production and execute initialize?";
                        string sTittle = "Initialize";

                        DialogResult result = MessageBox.Show(new Form { TopMost = true }, sMsgBox, sTittle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                        if (result == DialogResult.Cancel)
                            break;
                    }
                    MiddleLayer.FlowCtrl.InitialReset();
                    MiddleLayer.GantryF.Robot.EpsonTM.Restart();
                    MiddleLayer.GantryF.Robot.IsIdle = false;
                    MiddleLayer.StartRun();
                    break;
                case RunMode.HOME:
                    MiddleLayer.FlowCtrl.InitialReset();
                    MiddleLayer.GantryF.Robot.EpsonTM.Restart();
                    MiddleLayer.GantryF.Robot.IsIdle = false;
                    MiddleLayer.StartRun();
                    return;
            }

            AcuraIOT.AcuraMachineState.ProcessingUnit = false;
        }
        private bool StatusChange_plLogin = false;
        private bool StatusChange_plLogout = false;
        private bool StatusChange_plMotorControl = false;
        public bool AlarmB = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");



            #region  开始停止按钮显示
            bool IsCanRunPage = SysPara.bExpertMode || (MENU_SelectPage == MENU_PageType.MachineStatus) || (MENU_SelectPage == MENU_PageType.NPFlowChart) || (MENU_SelectPage == MENU_PageType.About) || (MENU_SelectPage == MENU_PageType.Log) || (MENU_SelectPage == MENU_PageType.UserSetting) || (MENU_SelectPage == MENU_PageType.DarkMode) || (MENU_SelectPage == MENU_PageType.Hide);
            bool IsLogin = (SysPara.UserPermission != PermissionType.None);
            btnStart.Enabled=IsLogin && (SysPara.bExpertMode || IsCanRunPage) & SysPara.SystemInitialOk && !SysPara.IsMaintenanceMode & !SysPara.SystemRun;
            btnPause.Enabled=(IsLogin & SysPara.SystemRun & SysPara.SystemMode != RunMode.IDLE & !SysPara.IsMaintenanceMode);
            btnInitial.Enabled = IsLogin & (SysPara.bExpertMode || IsCanRunPage) && !SysPara.IsMaintenanceMode && !SysPara.SystemRun;
            #endregion
            #region Picture show
            if (MENU_Product.Enabled)
                MENU_Product.Image = imageList1.Images[2];
            else
                MENU_Product.Image = imageList1.Images[3];
            if (MENU_Save.Enabled)
                MENU_Save.Image = imageList1.Images[4];
            else
                MENU_Save.Image = imageList1.Images[5];
            if (MENU_Hard.Enabled)
                MENU_Hard.Image = imageList1.Images[6];
            else
                MENU_Hard.Image = imageList1.Images[7];
            if (MENU_Check.Enabled)
                MENU_Check.Image = imageList1.Images[10];
            else
                MENU_Check.Image = imageList1.Images[11];
            if (MENU_System.Enabled)
                MENU_System.Image = imageList1.Images[14];
            else
                MENU_System.Image = imageList1.Images[15];
            if (MENU_AddUser.Enabled)
                MENU_AddUser.Image = imageList1.Images[16];
            else
                MENU_AddUser.Image = imageList1.Images[17];
            if (MENU_Mes.Enabled)
                MENU_Mes.Image = imageList1.Images[18];
            else
                MENU_Mes.Image = imageList1.Images[19];
            if (MENU_Rapid.Enabled)
                MENU_Rapid.Image = imageList1.Images[20];
            else
                MENU_Rapid.Image = imageList1.Images[21];
           
            MENU_Data.Image = MENU_Data.Enabled ? imageList1.Images[22] : imageList1.Images[23];
            RecipeEditorForm.Image = MENU_Exit.Enabled ? imageList1.Images[24] : imageList1.Images[25];
            MENU_Exit.Image = MENU_Exit.Enabled ? imageList1.Images[26] : imageList1.Images[27];
            btnStart.Image = btnStart.Enabled ? imageList1.Images[30] : imageList1.Images[33];
            btnPause.Image = btnPause.Enabled ? imageList1.Images[31] : imageList1.Images[34];
            btnInitial.Image = btnInitial.Enabled ? imageList1.Images[29] : imageList1.Images[37];
            #endregion
            #region Dark Mode
            //bool bAutoDarkModeEnabled = MiddleLayer.ProcessF.GetSettingValue(MiddleLayer.ProcessF.PSet.TableName, MiddleLayer.ProcessF.DarkMode.ColumnName);

            //if (bAutoDarkModeEnabled)
            //{
            //    int iDateHour = Convert.ToInt32(DateTime.Now.ToString("HH"));
            //    int iDateMinute = Convert.ToInt32(DateTime.Now.ToString("mm"));
            //    int iDarkDesHour = MiddleLayer.ProcessF.GetSettingValue(MiddleLayer.ProcessF.PSet.TableName, MiddleLayer.ProcessF.DarkModeDesHr.ColumnName);
            //    int iDarkDesMinute = MiddleLayer.ProcessF.GetSettingValue(MiddleLayer.ProcessF.PSet.TableName, MiddleLayer.ProcessF.DarkModeDesMin.ColumnName);
            //    int iDarkHabHour = MiddleLayer.ProcessF.GetSettingValue(MiddleLayer.ProcessF.PSet.TableName, MiddleLayer.ProcessF.DarkModeHabHr.ColumnName);
            //    int iDarkHabMinute = MiddleLayer.ProcessF.GetSettingValue(MiddleLayer.ProcessF.PSet.TableName, MiddleLayer.ProcessF.DarkModeHabMin.ColumnName);

            //    if (iDateHour >= iDarkDesHour && iDateHour <= iDarkHabHour)
            //    {
            //        if ((iDateHour == iDarkDesHour && iDateMinute >= iDarkDesMinute) || (iDateHour > iDarkDesHour && iDateHour < iDarkHabHour) || (iDateHour == iDarkHabHour && iDateMinute < iDarkHabMinute))
            //        {
            //            if (SysPara.MainPara.bDarkModeActive)
            //            {
            //                exDarkMode(false);
            //                SysPara.MainPara.bDarkModeActive = false;
            //            }
            //        }
            //    }

            //    if (iDateHour <= iDarkDesHour || iDateHour >= iDarkHabHour)
            //    {
            //        if ((iDateHour == iDarkHabHour && iDateMinute >= iDarkHabMinute) || iDateHour > iDarkHabHour || iDateHour < iDarkDesHour || (iDateHour == iDarkDesHour && iDateMinute < iDarkDesMinute))
            //        {
            //            if (!SysPara.MainPara.bDarkModeActive)
            //            {
            //                exDarkMode(true);
            //                SysPara.MainPara.bDarkModeActive = true;
            //            }
            //        }
            //    }

            //    SysPara.MainPara.bDarkModeButtonEnabled = false;
            //    tlMENU_DarkMode.SetEnable(false);
            //    tlMENU_DarkMode.SetBackColor(cl_Disable);
            //}
            //else
            //{
            //    if (!SysPara.MainPara.bDarkModeButtonEnabled)
            //    {
            //        SysPara.MainPara.bDarkModeButtonEnabled = true;
            //        tlMENU_DarkMode.SetEnable(true);
            //        tlMENU_DarkMode.SetBackColor(cl_Background);
            //    }
            //}
            #endregion Dark Mode
            #region Login/Logout/MotorControl UI control
            plMotorControl.SetEnable(((IsLogin) & (SysPara.SystemMode == RunMode.IDLE)) || SysPara.bExpertMode);

            if (plMotorControl.Enabled)
            {
                if (StatusChange_plMotorControl)
                {
                  
                    StatusChange_plMotorControl = false;
                }
            }
            else
            {
                if (!StatusChange_plMotorControl)
                {
                  
                    StatusChange_plMotorControl = true;
                }
            }

            #endregion
            #region Reset Permission 
            if (PermissionTimer.On(15 * 60000))
            {
                PermissionTimer.Restart();
                SysPara.UserName = "Operator";
                MiddleLayer.SwitchPermission(PermissionType.Operator);
                SwitchMainPage(MENU_PageType.MachineStatus);
                if (!MENU_Picture[(int)MENU_SelectPage1].Enabled)
                {
                    SwitchMainPage(MENU_PageType.MachineStatus);
                }
                else
                {
                    RefreshMenuBackcolor();
                }
            }
            #endregion
            #region 设备状态标签显示
            if (SysPara.SystemMode == RunMode.HOME && SysPara.SystemRun)
            {
                MachineStatus.SetText("INIT");
                MachineStatus.BackColor = Color.Orange;
            }
            else if (SysPara.SystemMode == RunMode.AUTO && SysPara.SystemRun)
            {
                MachineStatus.SetText("AUTO");
                MachineStatus.BackColor = Color.Lime;
            }
            else
            {
                MachineStatus.SetText("IDLE");
                MachineStatus.BackColor = Color.Yellow;
            }

            #endregion
            #region 用户名
            //LoginText.Text = "用户名：" + SysPara.UserName+"  " + "用户权限:"+SysPara.UserPermission;
            #endregion
            #region Alarm Message
            if (SDKPara.Arm.DoRefresh)
            {
                SDKPara.Arm.DoRefresh = false;
                if (MESLib.CommParas.Frm_AlarmTable != null)
                    MESLib.CommParas.Frm_AlarmTable.lvMessage.Items.Clear();
                for (int i = 0; i < SDKPara.Arm.ArmUIList.Count; i++)
                {
                    NPSDK.Alarm.AlarmTableType AlarmData = SDKPara.Arm.ArmUIList[i];
                    ListViewItem Alarm = new ListViewItem(AlarmData.DateTime);
                    Alarm.SubItems.Add(AlarmData.Type);
                    Alarm.SubItems.Add(AlarmData.Code);
                    Alarm.SubItems.Add(AlarmData.Content);
                    switch (AlarmData.Type)
                    {
                        case "E":
                            Alarm.SubItems[0].BackColor = Color.Red;
                            LogManager.ResultHelper.WriteAlarm(AlarmData.Code, AlarmData.Content);
                            break;
                        case "W":
                            Alarm.SubItems[0].BackColor = Color.Yellow;
                            break;
                        case "K":
                            Alarm.SubItems[0].BackColor = Color.DarkSalmon;
                            break;
                    }
                    if (MESLib.CommParas.Frm_AlarmTable != null)
                        MESLib.CommParas.Frm_AlarmTable.lvMessage.Items.Add(Alarm);
                    MiddleLayer.cfxHandler.FaultOccurred(new CFX.Structures.Fault { FaultCode = AlarmData.Code, Description = AlarmData.Content });
                    MiddleLayer.LogF.AddLog(LogType.Alarmas, AlarmData.Type + "," + AlarmData.Code + "," + AlarmData.Content, true);
                }
                if (MESLib.CommParas.Frm_AlarmTable != null)
                {
                    if (MESLib.CommParas.Frm_AlarmTable.lvMessage.Items.Count > 3)
                        MESLib.CommParas.Frm_AlarmTable.lvMessage.EnsureVisible(MESLib.CommParas.Frm_AlarmTable.lvMessage.Items.Count - 1);
                }
            }
            #endregion
            #region lower conveyor indicator
            try
            {
                if (MiddleLayer.ConveyorF.IB_DownCvyFeedSensor.On())
                {
                    this.tsslSensor1.Image = global::Alpha.Properties.Resources.green3;
                }
                else
                {
                    this.tsslSensor1.Image = global::Alpha.Properties.Resources.gray3;
                }
                if (MiddleLayer.ConveyorF.IB_DownCvyInPlace.On())
                {
                    this.tsslSensor2.Image = global::Alpha.Properties.Resources.green3;
                }
                else
                {
                    this.tsslSensor2.Image = global::Alpha.Properties.Resources.gray3;
                }
                if (MiddleLayer.ConveyorF.IB_DownCvyDischargeSensor.On())
                {
                    this.tsslSensor3.Image = global::Alpha.Properties.Resources.green3;
                }
                else
                {
                    this.tsslSensor3.Image = global::Alpha.Properties.Resources.gray3;
                }
            }
            catch
            {

            }
            #endregion


            timer1.Enabled = true;
        }

        private void MENU_Robot_Click(object sender, EventArgs e)
        {

        }

        private void tlMENU_DarkMode_Click(object sender, EventArgs e)
        {
            //string ItemName = Convert.ToString(((Control)sender).Tag);
            //MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            //SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
        }

        private void tlMENU_DarkMode_MouseEnter(object sender, EventArgs e)
        {
            //Control Parent = ((Control)sender).Parent;
            //if (Parent.Enabled)
            //    Parent.SetBackColor(MiddleLayer.ModeColors.MouseOver);
        }

        private void tlMENU_DarkMode_MouseLeave(object sender, EventArgs e)
        {
            //Control Parent = ((Control)sender).Parent;
            //if (Parent.Enabled)
            //{
            //    string ItemName = Convert.ToString(((Control)sender).Tag);
            //    Parent.SetBackColor(MiddleLayer.ModeColors.Background);
            //    if (Parent.Name.IndexOf("MENU") >= 0)
            //        if ((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName) == MENU_SelectPage)
            //            Parent.SetBackColor(MiddleLayer.ModeColors.Selected);
            //}
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click StartRun button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);

            fnStart();

            if (SysPara.EnableMes)
            {
                MESLib.CommFuns.MES_ClearAlarm();
                MESLib.CommParas.MesManager.MES_S6F11_10401((int)MESLib.CommParas.MesManager.PreNotIDLEState);
                //// check current processstate, if is pause, then to pre-no-idle-state
                //if (MESLib.CommParas.MesManager._connector != null &&
                //    (ProcessState)Convert.ToInt32(MESLib.CommParas.MesManager._connector.SV.ProcessStateInfo.content.CurrentProcessState.content) == ProcessState.PAUSE)
                //    MESLib.CommParas.MesManager.MES_S6F11_10401((int)MESLib.CommParas.MesManager.PreNotIDLEState);
                //else
                //    MESLib.CommParas.MesManager.MES_S6F11_10401(
                //        Convert.ToInt32(MESLib.CommParas.MesManager._connector.SV.ProcessStateInfo.content.PreviousProcessState.content));
            }
        }

        //MOD@@ Guillermo Carrillo - public para poder llamarlo desde
        //  RestartProcessQuick() (GantryForm.cs) y pausar automaticamente
        //  al darle a REINICIO RAPIDO. Mismo patron que btnStop_Click, que
        //  ya era public por la misma razon (MiddleLayer.MainF.btnStop_Click).
        public void btnPause_Click(object sender, EventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click Pause button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);


            MiddleLayer.StopRun();
            if (SysPara.EnableMes && MESLib.CommParas.MesManager != null
                && MESLib.CommParas.MesManager._connector != null &&
                MESLib.CommParas.MesManager.MesInitE)
            {
                MESLib.CommParas.MesManager.MES_S6F11_10401(((byte)MESLib.ProcessState.PAUSE));
            }
        }

        private void btnInitial_Click(object sender, EventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click Initial Run button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            fnInitial();
        }

        public  void btnStop_Click(object sender, EventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click Stop button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            if (SysPara.SystemRun || SysPara.SystemInitialOk)//SysPara.SystemMode == RunMode.AUTO || SysPara.SystemMode == RunMode.HOME)
            {
                SysPara.SystemInitialOk = false;
                //补充机器人停止
                //MiddleLayer.GantryF.OB_RobotStart.Off();
                
                MiddleLayer.ConveyorF.OB_SMMachineReady.Off();

                //自动标定流程的那个标志位，在主界面Stop按钮下需要置为true，
                //避免标定失败情况下流程卡住无法终止。
                MiddleLayer.GantryF.bAutoCalibFlow = true;
                MiddleLayer.StopRun();

                // MiddleLayer.GantryF._OB_RobotStop.On();

                SysPara.SystemMode = RunMode.IDLE;

                if (SysPara.EnableMes && MESLib.CommParas.MesManager != null
                    && MESLib.CommParas.MesManager._connector != null &&
                    MESLib.CommParas.MesManager.MesInitE)
                {
                    MESLib.CommParas.MesManager.MES_S6F11_10401(((byte)MESLib.ProcessState.IDLE));
                }
            }
        }

        //MOD@@ Guillermo Carrillo - Boton REINICIO RAPIDO de la barra superior
        private void btn_RestartProcess_Click(object sender, EventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format(
                "User click Restart Process button. UserType:{0} UserName:{1}",
                SysPara.UserPermission.ToString(), SysPara.UserName),
                SysPara.bEnableGeneralSaveLog);
            MiddleLayer.GantryF.RestartProcessQuick();
        }

        private void btnAlarmReset_Click(object sender, EventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click AlarmReset button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            MiddleLayer.MotorAlarmReset();
            SDKKernal.ClearAllAlarm();
            if (SysPara.AlarmNow)
            {
                MiddleLayer.cfxHandler.StationStateChanged(CFX.Structures.ResourceState.NST, DateTime.Now);
                MiddleLayer.cfxHandler.FaultCleared(SysPara.UserName);
                SysPara.AlarmNow = false;
            }

            SysPara.LeftDoorAlarmEnable = false;
            SysPara.RightDoorAlarmEnable = false;


            if (SysPara.EnableMes && MESLib.CommParas.MesManager != null
                && MESLib.CommParas.MesManager._connector != null &&
                MESLib.CommParas.MesManager.MesInitE)
            {
                MESLib.CommFuns.MES_ClearAlarm();
            }




        }
        int b = 0;
        private void btnBuzzerOff_Click(object sender, EventArgs e)
        {
            if (b == 1)
            {
                MiddleLayer.SystemF.BuzzOn();
                b = 0;
                btnBuzzerOff.BackColor = Color.White;
            }
            else
            {

                //DialogResult reult = MessageBox.Show("Do you want Close Buzzer?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                //if ((reult == DialogResult.Yes))
                //{
                    if (SysPara.AlarmNow)
                    {
                        MiddleLayer.cfxHandler.FaultAcknowledged(SysPara.UserName);
                    }
                    MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click Buzzer Off button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
                    MiddleLayer.SystemF.BuzzOff();
                    b = 1;
                    btnBuzzerOff.BackColor = Color.LimeGreen;
                //}
            }
          
        }

        #region reminder
        public void ShowWord(Control con, string word)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(con, word);
        }
        private void MouseEnter1(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            SwitchRemind((MENU_PageType1)Enum.Parse(typeof(MENU_PageType1), ItemName));
        }
        private void SwitchRemind(MENU_PageType1 PageType)
        {
            MENU_SelectPage1 = PageType;
            switch (MENU_SelectPage1)
            {
                case MENU_PageType1.Home:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Home, "主界面");
                    else
                        ShowWord(this.MENU_Home, "Home");
                    break;
                case MENU_PageType1.Product:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Product, "物料管理");
                    else
                        ShowWord(this.MENU_Product, "ProductManager");
                    break;
                case MENU_PageType1.Save:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Save, "保存");
                    else
                        ShowWord(this.MENU_Save, "Save");
                    break;
                case MENU_PageType1.Hard:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Hard, "硬件调试");
                    else
                        ShowWord(this.MENU_Hard, "HardForm");
                    break;
                case MENU_PageType1.Manual:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Manual, "手动界面");
                    else
                        ShowWord(this.MENU_Manual, "ManagerForm");
                    break;
                case MENU_PageType1.Check:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Check, "信号监视");
                    else
                        ShowWord(this.MENU_Check, "CheckForm");
                    break;
                case MENU_PageType1.System:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_System, "系统设置");
                    else
                        ShowWord(this.MENU_System, "SystemForm");
                    break;
                case MENU_PageType1.AddUser:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_AddUser, "用户设置");
                    else
                        ShowWord(this.MENU_AddUser, "UserManager");
                    break;
                case MENU_PageType1.Mes:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Mes, "数据上传");
                    else
                        ShowWord(this.MENU_Mes, "MES");
                    break;
                case MENU_PageType1.Rapid:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Rapid, "快捷键");
                    else
                        ShowWord(this.MENU_Rapid, "RapidButton");
                    break;
                case MENU_PageType1.Data:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Data, "产品数据");
                    else
                        ShowWord(this.MENU_Data, "ProductData");
                    break;
                case MENU_PageType1.Vision:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.RecipeEditorForm, "视觉界面");
                    else
                        ShowWord(this.RecipeEditorForm, "VisionForm");
                    break;
                case MENU_PageType1.Login:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Login, "用户登录");
                    else
                        ShowWord(this.MENU_Login, "UserLogin");
                    break;
                case MENU_PageType1.Reset:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.btnInitial, "初始化");
                    else
                        ShowWord(this.btnInitial, "InitialButton");
                    break;
                case MENU_PageType1.Run:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.btnStart, "运行");
                    else
                        ShowWord(this.btnStart, "RunButton");
                    break;
                case MENU_PageType1.Pause:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.btnPause, "暂停");
                    else
                        ShowWord(this.btnPause, "PauseButton");
                    break;
                case MENU_PageType1.Stop:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.btnStop, "停止");
                    else
                        ShowWord(this.btnStop, "StopButton");
                    break;
                case MENU_PageType1.Exit:
                    if (SysPara.LanguageShow == LanguageType.Chinese)
                        ShowWord(this.MENU_Exit, "退出");
                    else
                        ShowWord(this.MENU_Exit, "Exit");
                    break;
               
            }
        }
        #endregion

        private void LoginOutTime_Tick(object sender, EventArgs e)
        {
          
        }

        private void MENU_Manual_Click(object sender, EventArgs e)
        {
            SysPara.UserName = "None";
            SysPara.UserPermission = PermissionType.Operator;
            SwitchPermission(SysPara.UserPermission);
            SwitchMainPage(MENU_PageType.MachineStatus);
            RefreshMenuBackcolor();
           
        }

        private void lbl_Simulation_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void Loginout_Tick(object sender, EventArgs e)
        {
            SysPara.UserName = "None";
            SysPara.UserPermission = PermissionType.Operator;
            SwitchPermission(SysPara.UserPermission);
            SwitchMainPage(MENU_PageType.MachineStatus);
            RefreshMenuBackcolor();
        }

        private void MENU_System_Click_1(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            MiddleLayer.LogF.AddLog(LogType.UserInterface, string.Format("User click " + ItemName + " button. UserType:{0} UserName:{1}", SysPara.UserPermission.ToString(), SysPara.UserName), SysPara.bEnableGeneralSaveLog);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
            
        }

        Stopwatch idle_watch = new Stopwatch();
        bool idle_hanlding = false;
        bool idle_mode = false;

        private void Timer_IDLE_Tick(object sender, EventArgs e)
        {
            if (MESLib.CommParas.Frm_MainHeader != null)
            {
                try
                {
                    if (MESLib.CommParas.MesManager._connector != null)
                    {
                        string processstate = ((MESLib.ProcessState)Convert.ToInt32(MESLib.CommParas.MesManager._connector.SV.ProcessStateInfo.content.CurrentProcessState.content)).ToString();
                        string controlstate = ((MESLib.ControlState)Convert.ToInt32(MESLib.CommParas.MesManager._connector.SV.CurrentControlState.content)).ToString();
                        MESLib.CommParas.Frm_MainHeader.UpdateData(processstate
                        , controlstate
                        , "");
                    }
                }
                catch
                {

                }
                MESLib.CommFuns.ShowEquipmentName(); // station name
                MESLib.CommParas.Frm_MainHeader.UpdateTime();
            }
            // update recipe
            if (SysPara.EnableMes)
            {
                if (MESLib.CommParas.MesManager._connector != null)
                {
                    try
                    {
                        string _RecipeID = (string)MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");
                        //f._connector.SV.CurrentPPID.content.EquipmentName = new VarItem<string>("10001", f._connector._Ec.EquipmentName_1.content.ToString());
                        MESLib.CommParas.MesManager._connector.SV.CurrentPPID.content.CurrentPPID = new VarItem<string>("10135", _RecipeID);
                        MESLib.CommParas.MesManager._connector.SV.CurrentPPID.content.CurrentPPIDVersion = new VarItem<string>("10136", "1.0.0");

                        //f._connector.SV.CurrentPPIDList.content.EquipmentName = new VarItem<string>("10001", f._connector._Ec.EquipmentName_1.content.ToString());
                        MESLib.CommParas.MesManager._connector.SV.CurrentPPIDList.content.CurrentPPID = new VarItem<string>("10135", _RecipeID);
                        MESLib.CommParas.MesManager._connector.SV.CurrentPPIDList.content.CurrentPPIDVersion = new VarItem<string>("10136", "1.0.0");
                    }
                    catch
                    {

                    }
                }
            }
            // check idle last time
            // "221"="Y", "222"=minutes
            if (idle_hanlding)
                return;
            try
            {
                string _221 = "";
                string _222 = "";
                if (MESLib.CommParas.MesManager.NEW_EQUIPMENT_CONSTANT_SEND_STR != null &&
                    MESLib.CommParas.MesManager.NEW_EQUIPMENT_CONSTANT_SEND_STR != "")
                {
                    JObject jobj = JObject.Parse(MESLib.CommParas.MesManager.NEW_EQUIPMENT_CONSTANT_SEND_STR);

                    _221 = jobj["221"].Value<string>();
                    _222 = jobj["222"].Value<string>();
                    if (_221 == "Y")
                    {
                        if ((MESLib.ProcessState)Convert.ToInt32(MESLib.CommParas.MesManager._connector.SV.ProcessStateInfo.content.CurrentProcessState.content)
                            == MESLib.ProcessState.IDLE)
                        {
                            idle_watch.Start();
                            if (!idle_mode)
                            {
                                MESLib.CommParas.IdleStart = DateTime.Now;
                                idle_mode = true;
                            }
                        }
                        else
                        {
                            idle_mode = false;
                            idle_watch.Reset();
                        }
                        int seconds = Convert.ToInt32(_222);
                        if (idle_watch.ElapsedMilliseconds > 1000 * seconds * 60)
                        {
                            // show idle reason form
                            MESLib.CommParas.IdleEnd = DateTime.Now;
                            idle_watch.Stop();
                            idle_watch.Reset();
                            idle_hanlding = true;

                            Action idle_action = new Action(() =>
                            {
                                MESLib.Frms.FrmIDLEReason frmIDLEReason = new MESLib.Frms.FrmIDLEReason();
                                frmIDLEReason.TopMost = true;
                                frmIDLEReason.ShowDialog();
                            });

                            idle_action.BeginInvoke(Idle_Reason_CallBack, null);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void Idle_Reason_CallBack(IAsyncResult ar)
        {
            idle_mode = false;
            idle_hanlding = false;
        }
    }
}


using Alpha._0.Vision;
using AcuraLibrary.Forms;
using NPSDK;
using NPClient;
using NPFanucRobotDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPSDK;
using NPSDK;
using System.IO;
using System.IO.Ports;
using Alpha._0.Classes;
using PressureScann;
using Alpha.FunctionForms;
using LogSv;
using Cognex.VisionPro3D;
using Cognex.VisionPro;
using Microsoft.Win32;
using static Alpha.FunctionForms.LogForm;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.Display;
using System.Net.NetworkInformation;
using TcpipHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;
using logging;
using System.Dynamic;

namespace Alpha._0.ModuleForms
{
    public partial class GantryForm : ModuleBaseForm
    {
        #region MES
        Dictionary<int, InstallPCBInfo> CurrentPCBList = new Dictionary<int, InstallPCBInfo>();

        #endregion
        public string[] MaterialID_Array = new string[3];
        public bool[] EnableProductFlag = new bool[] { false, false, false };
        public static string[] LotIDs = new string[] { "", "", "" }; // pcba QRCode
        public static string[] MoudleIDs = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" }; // Pallet QRCODE
        public static string[] PocketIDs = new string[] { "A1", "B1", "C1", "D1", "E1", "F1", "A2", "B2", "C2", "D2", "E2", "F2" }; // A1-F2

        //public List<string> LotId_Used;
        //public List<string> ModuleId_Used;
        //public List<string> PocketId_Used;
        public bool LastProduct = false;

        int gripperIndex = 1;
        bool bMESPCBIsNG = false;

        bool MESCheckAllFail = false;
        public GantryForm()
        {
            InitializeComponent();

            BConnect();
            BConnect2();
            CheckForIllegalCrossThreadCalls = false;

            for (int i = 0; i < processData.dgvProcessResults.Columns.Count; i++)
            {
                SysPara.CsvHeader += processData.dgvProcessResults.Columns[i].HeaderText + ",";
            }


            H1_PickupPos.RecordDisplayList.Add(cogRecDisp_H5_Recipe);
            H1_DischargePos.RecordDisplayList.Add(cogRecDisp_H5_Recipe);
            dgv_EpsonPosition.DgvHeader = dgvHeader;
            dgv_EpsonPosition.RaiseSelectedEvent += VUDatagridView1_RaiseSelectedEvent;
            dgv_EpsonPosition.myDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgv_EpsonPosition.LoadXmlFileFromPath(Application.StartupPath + @"\EpsonPosition.xml");

            dgv_EpsonErrInfo.DgvHeader = dgvHeaderErr;
            dgv_EpsonErrInfo.RaiseSelectedEvent += dgv_ErrInfo_RaiseSelectedEvent;
            dgv_EpsonErrInfo.myDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgv_EpsonErrInfo.LoadXmlFileFromPath(Application.StartupPath + @"\EpsonErrInfo.xml");

            dgv_DischargeTechPos.DgvHeader = dgvHeaderTechPos;
            // dgv_DischargeTechPos.RaiseSelectedEvent += dgv_ErrInfo_RaiseSelectedEvent;
            dgv_DischargeTechPos.myDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv_RobotPosition.DgvHeader = dgvHeaderRobotPos;
            dgv_RobotPosition.myDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            InitBackGround();
            GetComList();

            SysPara.CableDetectTh = new Thread(new ThreadStart(tm_CableDetection_Tick));
            SysPara.CableDetectTh.IsBackground = true;
            SysPara.CableDetectTh.Start();
            tabContro.Controls.Remove(tabPage9);
        }

        //public string ProductID = "ACA05S001X.MM00";
        //public string Barcode1 = "LGIB-07-01", Barcode2 = "LGIB-07-02", Barcode3 = "LGIB-07-03", Barcode4 = "LGIB-07-04", Barcode5 = "LGIB-07-05", Barcode6 = "LGIB-07-06",
        //Barcode7 = "7LGIB-07-07", Barcode8 = "LGIB-07-08", Barcode9 = "LGIB-07-09", Barcode10 = "LGIB-07-10", Barcode11 = "LGIB-07-11", Barcode12 = "LGIB-07-12";
        //public bool EnableMes, MesInitE;
        //public string Ainfo1, Ainfo2, Ainfo3, MesBarcode;
        //public int CAinfo1;
        //SecsGem s1f1, s1f3, s1f15, s1f17, s2f13, s2f15, s2f31, s2f49, S6F11, s6f15,
        //s7f17, s7f19, s7f23, s7f25, s7f27, s10f3, s99f99;
        //HsmsConnection _connector;//连接对象
        //Task TCheckConnectStatus;//监控连接状态线程
        //bool Reconnect = true;//连接状态线程标志位
        //bool switch_offline_flag = false;
        //bool UploadBarcodeFinished = true;
        //List<string> raisedAlarm = new List<string>();
        //AutoResetEvent WriteCsvFinished = new AutoResetEvent(false);
        //AutoResetEvent s6f11Finished = new AutoResetEvent(false);
        //string s6f11Result = "";
        //bool isRadioButtonTrigger = true;

        //AutoResetEvent uploadBarcode = new AutoResetEvent(false);
        //Dictionary<string, string> mesRes = new Dictionary<string, string>();

        public int CurrentRowsIndex { set; get; } = 0;
        List<string> QRCode = new List<string>();






        public bool RobotInitial = false;

        public bool ModuleComplete = false;

        public int CCD1NG, CCD2NG, CCD3NG, CCD4NG = 0;

        public int ReclaimingNG = 0;

        public bool ReclaimingOK = false;

        public bool ScrewOK = false;

        public int ScrewPoint = 0;

        public bool ScanOK = false;

        public bool ScanNG = false;

        public int Scan = 0;

        public int Sdate = 0;

        public int Sdate2 = 0;

        public bool bConnect = false;

        public bool bConnect2 = false;

        bool TimerRead = false;

        public bool bComStart = false;

        public bool bComStart2 = false;

        public bool bSubscribe = false;

        public bool bSubscribe2 = false;

        FastenData FastenDataCurrent = new FastenData();

        FastenData2 FastenDataCurrent2 = new FastenData2();

        public bool FasternDataGetResult = false;

        public bool FasternDataGetResult2 = false;

        public bool ScrewOK1 = false;

        public bool ScrewNG1 = false;

        public bool ScrewOK2 = false;

        public bool ScrewNG2 = false;

        public bool TakeOut1 = false;

        public bool TakeOut2 = false;

        public bool throwingjudge = false;

        string gripper = "00";

        public int curProductCount = 0;

        public int scanCount = 0;

        public bool haveSendCodeToMES = false;

        public List<bool> palletUpResult = new List<bool>();
        public List<bool> palletDownResult = new List<bool>();

        public List<bool> palletResultCopy = new List<bool>();

        public double assembVisOffsetX = 0;
        public double assembVisOffsetY = 0;
        EthernetPF PFClient = new EthernetPF();

        EthernetPF2 PFClient2 = new EthernetPF2();

        public double CCD1X, CCD1Y, CCD2X, CCD2Y, CCD3X, CCD3Y, CCD4X, CCD4Y, CCD5X, CCD5Y, CCD6X, CCD6Y, CCD7X, CCD7Y, CCD8X, CCD8Y,
            CCD9X, CCD9Y, CCD10X, CCD10Y, CCD11X, CCD11Y, CCD12X, CCD12Y, CCD13X, CCD13Y, CCD14X, CCD14Y, CCD15X, CCD15Y;

        public List<H1CCD> H1CCDData = new List<H1CCD>();

        public string[] DataTotal = new string[42];//Screw Data
        public int Datanum = 0;
        public bool TPFP;

        public H1_NGCCDCheck H1_NGCCDCheck = new H1_NGCCDCheck();

        public List<bool> MesTestList = new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true };

        //Pressure
        public Pressure PressureSensor1 = new Pressure();
        public Pressure PressureSensor2 = new Pressure();
        public double Tarp1Value;
        public double Tarp2Value;
        public ProcessDataForm processData = MiddleLayer.ProcessF.fnGetDataForm("Alpha");
        public H1_PickupPos H1_PickupPos = new H1_PickupPos();
        public H1_DischargePos H1_DischargePos = new H1_DischargePos();

        BackgroundWorker bgwEpsonCommunicate;
        public Epson Robot = new Epson();

        public JTimer ReadIOTimer = new JTimer();
        public JTimer WriteIOTimer = new JTimer();
        public JTimer InitTM = new JTimer();
        public JTimer RunTM = new JTimer();
        public JTimer feederT = new JTimer();
        private int CalibPictureCount = 0;              //Calibrate time
        public bool bAutoCalibFlow = false;            //Auto CalibrateFlow
        double[] RobotPose = new double[3];
        public List<Tuple<Cog3DVect3, CogImage8Grey>> visCalibData = new List<Tuple<Cog3DVect3, CogImage8Grey>>();
        public JTimer readPressTimer = new JTimer();
        public JTimer PressTimer = new JTimer();  //preasure timeout
        public JTimer FeederTM = new JTimer();
        public bool CCDAlogrithmStatus = false;   //Visison Result flag
        public bool AssmbleCCDAlogStatus = false;   //Visison Result flag
        public bool isPickProduceOK = true;       //pick result flag
        public string curPalletCode = "";         //Current pallet code
        public int curR1HaveNum = 0;              //Current robot have produce number
        public int curR1HaveOKNum = 0;              //Current robot have produce number
        List<bool> CCDReuslt = new List<bool>();
        List<bool> scannerReuslt = new List<bool>();
        Queue<VisionPos> queCCDPosition = new Queue<VisionPos>();
        public bool bReadIOFlow = false;            //read IOFlow
        string VisX = "000.000";
        string VisY = "000.000";
        string VisU = "000.000";
        string ngPos = "000";
        List<SaveImage> FeederImages = new List<SaveImage>();
        List<SaveImage> ProductImages = new List<SaveImage>();
        string[] PCBCode = new string[12];

        public struct SaveImage
        {
            public Bitmap Image;
            public string Timer;
            public string PCBQRCode;
            public string PalletQRCode;
        }




        Reintento reintento = new Reintento();
        //MOD@@ Guillermo Carrillo - bandera del reinicio rapido
        private volatile bool bRestartQuickActive = false;

        double[] robotPos = new double[3];

        string[] dgvHeader = new string[] { "Number", "Name", "Command", "PositionNum", "GripperNum", "X", "Y", "Z", "Annotation" };
        string[] dgvHeaderErr = new string[] { "Number", "Name", "Command", "ErrorInfo" };
        string[] dgvHeaderTechPos = new string[] { "Number", "X", "Y", "U", "Annotation" };
        string[] dgvHeaderRobotPos = new string[] { "Number", "Annotation", "X", "Y", "U" };
        /// <summary>
        /// ReadIO
        /// </summary>
        private string[] _readIO = new string[16];
        public string[] ReadIO
        {
            get
            {
                return _readIO;
            }
            set
            {
                _readIO = value;
                RefreshDifferentThreadUI(bt_Input_1, () => { bt_Input_1.BackgroundImage = _readIO[0].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_2, () => { bt_Input_2.BackgroundImage = _readIO[1].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_3, () => { bt_Input_3.BackgroundImage = _readIO[2].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_4, () => { bt_Input_4.BackgroundImage = _readIO[3].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_5, () => { bt_Input_5.BackgroundImage = _readIO[4].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_6, () => { bt_Input_6.BackgroundImage = _readIO[5].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_7, () => { bt_Input_7.BackgroundImage = _readIO[6].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_8, () => { bt_Input_8.BackgroundImage = _readIO[7].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_9, () => { bt_Input_9.BackgroundImage = _readIO[8].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_10, () => { bt_Input_10.BackgroundImage = _readIO[9].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_11, () => { bt_Input_11.BackgroundImage = _readIO[10].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_12, () => { bt_Input_12.BackgroundImage = _readIO[11].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_13, () => { bt_Input_13.BackgroundImage = _readIO[12].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_14, () => { bt_Input_14.BackgroundImage = _readIO[13].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_15, () => { bt_Input_15.BackgroundImage = _readIO[14].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
                RefreshDifferentThreadUI(bt_Input_16, () => { bt_Input_16.BackgroundImage = _readIO[15].ToString() == "1" ? Properties.Resources.Green : Properties.Resources.Red; });
            }
        }

        /// <summary>
        /// WriteIO
        /// </summary>
        private string[] _writeIO = new string[16] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        public string[] WriteIO
        {
            get
            {
                return _writeIO;
            }
            set
            {
                _writeIO = value;
                RefreshDifferentThreadUI(chk_Output_1, () => { chk_Output_1.Checked = _writeIO[0] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_2, () => { chk_Output_2.Checked = _writeIO[1] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_3, () => { chk_Output_3.Checked = _writeIO[2] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_4, () => { chk_Output_4.Checked = _writeIO[3] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_5, () => { chk_Output_5.Checked = _writeIO[4] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_6, () => { chk_Output_6.Checked = _writeIO[5] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_7, () => { chk_Output_7.Checked = _writeIO[6] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_8, () => { chk_Output_8.Checked = _writeIO[7] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_9, () => { chk_Output_9.Checked = _writeIO[8] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_10, () => { chk_Output_10.Checked = _writeIO[9] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_11, () => { chk_Output_11.Checked = _writeIO[10] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_12, () => { chk_Output_12.Checked = _writeIO[11] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_13, () => { chk_Output_13.Checked = _writeIO[12] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_14, () => { chk_Output_14.Checked = _writeIO[13] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_15, () => { chk_Output_15.Checked = _writeIO[14] == "1" ? true : false; });
                RefreshDifferentThreadUI(chk_Output_16, () => { chk_Output_16.Checked = _writeIO[15] == "1" ? true : false; });
            }
        }

        public struct H1CCD
        {
            public double x;
            public double y;
            public double u;
            public double x_Low;
            public double x_Hi;
            public double y_Low;
            public double y_Hi;
            public double u_Low;
            public double u_Hi;
        }

        // NEW VARIABLES
        public int uProcessIndex = 0;
        public int iProcessQty = 0;
        public bool EnableRobot { get { return GetData(dc_Pset_EnableRobot); } }






        public void GetH1CCD()
        {
            H1CCDData.Clear();
            DataTable H1dt = RecipeData.Tables["tb_H1_ScrewCheck"];
            for (int i = 0; i < H1dt.Rows.Count; i++)
            {
                DataRow dr = H1dt.Rows[i];
                H1CCD ToCCDData = new H1CCD
                {
                    x = Convert.ToDouble(dr[0]),
                    y = Convert.ToDouble(dr[1]),
                    u = Convert.ToDouble(dr[2]),
                    x_Low = Convert.ToDouble(dr[3]),
                    x_Hi = Convert.ToDouble(dr[4]),
                    y_Low = Convert.ToDouble(dr[5]),
                    y_Hi = Convert.ToDouble(dr[6]),
                    u_Low = Convert.ToDouble(dr[7]),
                    u_Hi = Convert.ToDouble(dr[8])
                };
                H1CCDData.Add(ToCCDData);
            }
        }

        JTimer Gantry1 = new JTimer();
        JTimer Gantry2 = new JTimer();
        JTimer Gantry3 = new JTimer();

        public TCPCLient KeyenceBarcode = new TCPCLient();

        public TCPCLient KeyenceBarcode2 = new TCPCLient();

        private FANUC RobotZebra = new FANUC();

        public double[] ReadRobotR = new double[101];

        public double[] RobotAng2 = new double[101];
        double[,] RobotAng = new double[101, 6];

        double[,] ReadRobotPR = new double[101, 6];

        private double X_Vison;
        private double Y_Vison;
        private double R_Vison;   //vision Data
        double PressureData1 = 0, PressureData2 = 0;//

        public void GetComList()
        {
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                this.cmbComPort.Items.Clear();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    this.cmbComPort.Items.Add(sValue);
                    this.cmb_FFUCom.Items.Add(sValue);
                }
                //cmbComPort.SelectedIndex = 6;

            }
        }
        private void InitBackGround()
        {
            if (bgwEpsonCommunicate == null) { bgwEpsonCommunicate = new BackgroundWorker(); }
            bgwEpsonCommunicate.WorkerSupportsCancellation = true;
            bgwEpsonCommunicate.WorkerReportsProgress = true;
            bgwEpsonCommunicate.DoWork += new DoWorkEventHandler(bgwEpsonCommunicate_DoWork);
            bgwEpsonCommunicate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwEpsonCommunicate_RunWorkerCompleted);
        }


        private void bgwEpsonCommunicate_DoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                Robot.Recive();
                Thread.Sleep(10);
            } while (Robot.ConnectStatus);
        }

        private void bgwEpsonCommunicate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MiddleLayer.LogF.AddLog(LogType.Production, "Robot disconnect", SysPara.bEnableGeneralSaveLog);

            //Log.log.Write("机器人通讯断开连接", Color.Black);
        }
        public double GetAngle(double[] dOrgP1, double[] dOrgP2, double[] dNewP1, double[] dNewP2)
        {

            Pos4D OrgP1 = new Pos4D();
            OrgP1.x = dOrgP1[0];
            OrgP1.y = dOrgP1[1];

            Pos4D OrgP2 = new Pos4D();
            OrgP2.x = dOrgP2[0];
            OrgP2.y = dOrgP2[1];

            Pos4D NewP1 = new Pos4D();
            NewP1.x = dNewP1[0];
            NewP1.y = dNewP1[1];

            Pos4D NewP2 = new Pos4D();
            NewP2.x = dNewP2[0];
            NewP2.y = dNewP2[1];

            double Angle = GetLineAngle(OrgP1, OrgP2) - GetLineAngle(NewP1, NewP2);
            return Angle;
        } 

        private double GetLineAngle(Pos4D P1, Pos4D P2)
        {
            double x = P2.x + P1.x;
            double y = P2.y + P1.y;
            double hypotenuse = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            double cos = x / hypotenuse;
            double radian = Math.Acos(cos);
            double angle = 180 / (Math.PI / radian);
            if (y < 0)
                angle = -angle;
            else if ((y == 0) && (x < 0))
                angle = 180;
            return angle;
        }

        public Pos4D VisionCalculateOffset(double OreSnapR, double SnapR)
        {
            Pos4D OffsetPos = new Pos4D();

            double OffsetX = 0;
            double OffsetY = 0;
            double OffsetR = 0;
            double Ag1 = GetSettingValue("PSet", "H1-CCD1-U-1") * Math.PI / 180;
            double Ag2 = GetSettingValue("PSet", "H1-Robot-U") * Math.PI / 180;
            double RadiusX = (GetSettingValue("PSet", "H1-CCD1-X-1") - GetSettingValue("PSet", "H1-Robot-X")) / (Math.Cos(Ag1) - Math.Cos(Ag2));
            double RadiusY = (GetSettingValue("PSet", "H1-CCD1-Y-1") - GetSettingValue("PSet", "H1-Robot-Y")) / (Math.Sin(Ag1) - Math.Sin(Ag2));

            double bg1 = OreSnapR * Math.PI / 180;
            double bg2 = SnapR * Math.PI / 180;

            OffsetX = RadiusX * Math.Cos(bg1) - RadiusX * Math.Cos(bg2);
            OffsetY = RadiusY * Math.Sin(bg1) - RadiusY * Math.Sin(bg2);
            OffsetR = OreSnapR - SnapR;

            OffsetPos.x = OffsetX;
            OffsetPos.y = OffsetY;
            OffsetPos.u = OffsetR;
            return OffsetPos;
        }

        public struct Pos4D
        {
            public double x;
            public double y;
            public double z;
            public double u;
        }

        public override void ServoOn()
        {
        }
        public override void AlwaysRun()
        {
            //BConnect();
            //if (bSubscribe)
            //{
            //    if (PFClient.KeepAliveTick > 5000)
            //    {
            //        PFKeepAlive();
            //        PFClient.CommunicationAlive();

            //    }
            //}
        }
        //public  void HomeReset()
        //{
        //    MTR_H1_X.HomeReset();
        //}
        public override void ServoOff()
        {
        }

        public override void StopRun()
        {
            tm_GantryWork.Stop();
        }

        public override void SetSpeed(int SpeedRatio)
        {

        }

        public override void InitialReset()
        {
            fc_InitialStart.TaskReset();


        }

        public override void Initial()
        {
            fc_InitialStart.TaskRun();
            MiddleLayer.GantryF.tm_GantryWork.Start();

        }

        public override void RunReset()
        {
            flowChart87.TaskReset();
            flowChart10.TaskReset();
            npFlowChart53.TaskReset();

        }

        public override void Run()
        {
            if (!SysPara.bBypassMode)
            {
                flowChart10.TaskRun();
                flowChart87.TaskRun();
                npFlowChart53.TaskRun();
            }


        }


        public void RobotStart(int num)
        {
            if (num == 1)
            {

            }
            if (num == 2)
            {

            }
        }

        //public bool bConnectSP()
        //{
        //    bool r2 = serialPort2.IsOpen;
        //    bool r1 = serialPort3.IsOpen;
        //    if (r1)
        //    {
        //        //serialPort1.Close();
        //        return true;
        //    }
        //    serialPort2.Open();
        //    serialPort3.Open();
        //    return false;
        //}

        private string sPressureData(string sComment)   // :004RDGROSS=  
        {
            string sData = "";
            string sBuffer = sComment + Environment.NewLine;
            serialPort3.Write(sBuffer);
            Thread.Sleep(25);
            sData = serialPort3.ReadExisting();
            return sData;
        }

        public bool RobotStart()
        {
            //OB_Robot_Maintain.Off();
            //OB_Robot_Procedure1.Off();
            //Thread.Sleep(1000);
            ////OB_Robot_Restoration.Off();
            //OB_Robot_STOP.On();
            //Thread.Sleep(1000);
            //OB_Robot_STOP.Off();
            //OB_Robot_Start.Off();
            //OB_Robot_Maintain.On();
            //OB_Robot_Enabled.On();
            //OB_Robot_Restoration.On();
            //Thread.Sleep(1000);
            //OB_Robot_Restoration.Off();
            //Thread.Sleep(1000);
            //OB_Robot_Start.On();
            //Thread.Sleep(1000);
            //OB_Robot_Start.Off();
            //OB_Robot_Procedure1.On();
            return true;
        }

        private bool PFKeepAlive()
        {
            string Response = PFClient.SendAndWaitForResponse(MID.M9999, TimeSpan.FromSeconds(2));

            if (Response == null)
            {
                return false;
            }
            if (Response.Contains("00209999"))
            {
                PFClient.CommunicationAlive();
                return true;
            }
            return false;
        }

        private bool PFKeepAlive2()
        {
            string Response = PFClient2.SendAndWaitForResponse(MID.M9999, TimeSpan.FromSeconds(2));

            if (Response == null)
            {
                return false;
            }
            if (Response.Contains("00209999"))
            {
                PFClient2.CommunicationAlive();
                return true;
            }
            return false;
        }

        public void RefreshDifferentThreadUI(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                Action refreshUI = new Action(action);
                control.Invoke(refreshUI);
            }
            else
            {
                action.Invoke();
            }
        }

        private bool ConnectToRobot()
        {
            try
            {
                if (RobotZebra.Connect(GetRecipeValue("RSet", "RobotPoint1")))
                {
                    return true;
                }
                RobotZebra.Disconnect();
                string RobotScrewIP = GetRecipeValue("RSet", "RobotPoint1");//MSet.RobotIP
                //int iRobotProt = 5000;//GetSettingValue("MSet", "RobotPort");//MSet.RobotPort
                bool bConncet = RobotZebra.Connect(RobotScrewIP);
                DelayMs(100);
                return bConncet;
            }
            catch (Exception e)
            {
                string a = e.ToString();
                return false;
            }

        }

        public static bool DelayMs(int delayMilliseconds)
        {
            DateTime now = DateTime.Now;
            Double s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.TotalMilliseconds + spand.Seconds * 1000;
                Application.DoEvents();
            }
            while (s < delayMilliseconds);
            return true;
        }

        private void Btn_ConnectRobot_Click(object sender, EventArgs e)
        {
            if (ConnectToRobot())
            {
                btnConnectResult.Text = "ConnectSuccess";
                btnConnectResult.BackColor = Color.Green;
            }
            else
            {
                btnConnectResult.Text = "ConnectFail";
                btnConnectResult.BackColor = Color.Red;
            }
        }

        private void btnConnectResult_Click(object sender, EventArgs e)
        {

        }



        private static readonly object RobotControl = new object();





        public bool ConnectBarcode()
        {

            if (KeyenceBarcode.ConnectStatus())
            {
                return true;
            }
            else
            {
                KeyenceBarcode.Disconnect();
                string IP = GetRecipeValue("RSet", "ScannerIP");//PSet.BardodeIP
                string Port = GetRecipeValue("RSet", "ScannerPort");//PSet.BardodeIP
                int iPort = Convert.ToInt32(Port);//PSet.BarcodePort
                KeyenceBarcode.Connect(IP, iPort);
                Client.BackColor = Color.Green;
                return true;
            }
        }

        public bool ConnectBarcode2()
        {

            if (KeyenceBarcode2.ConnectStatus())
            {
                return true;
            }
            else
            {
                KeyenceBarcode2.Disconnect();
                string IP = GetRecipeValue("RSet", "ScannerIP2");//PSet.BardodeIP
                string Port = GetRecipeValue("RSet", "ScannerPort2");//PSet.BardodeIP
                int iPort = Convert.ToInt32(Port);//PSet.BarcodePort
                KeyenceBarcode2.Connect(IP, iPort);
                Client2.BackColor = Color.Green;
                return true;
            }
        }

        public void Client_Click(object sender, EventArgs e)
        {
            ConnectBarcode();
        }

        public bool TrigerBarcode()
        {
            RefreshDifferentThreadUI(tbMassage, () =>
            {
                tbMassage.Text = "";
            });

            KeyenceBarcode.Receive();
            if (!KeyenceBarcode.ConnectStatus())
            {
                ConnectBarcode();
            }
            if (KeyenceBarcode.ConnectStatus())
            {
                KeyenceBarcode.Sent("LON" + "\r\n");
                Thread.Sleep(330);
                RefreshDifferentThreadUI(tbMassage, () =>
                 {
                     tbMassage.Text = KeyenceBarcode.Receive();
                 });
            }
            return true;
        }

        private bool TrigerBarcode1InFlow(JTimer jt)
        {

            if (KeyenceBarcode.ConnectStatus())
            {
                try
                {
                    int length = KeyenceBarcode.tcpClient.Client.Available;
                    if (length > 0)
                    {
                        byte[] recived = new byte[length];
                        KeyenceBarcode.tcpClient.Client.Receive(recived);
                    }
                }
                catch
                {

                }
                KeyenceBarcode.Sent("LON" + "\r\n");
                DelayMs(450);
                SysPara.QRCode1 = (KeyenceBarcode.Receive().Replace("\r", "")).Replace("\n", "");
                processData.fnShowSerial(SysPara.QRCode1);

                if (SysPara.QRCode1 == "ERROR")
                    return false;
                return true;

            }
            return false;
        }

        public bool TrigerBarcode2()
        {
            RefreshDifferentThreadUI(tbMassage2, () =>
            {
                tbMassage2.Text = "";
            });

            KeyenceBarcode2.Receive();
            if (!KeyenceBarcode2.ConnectStatus())
            {
                ConnectBarcode2();
            }
            if (KeyenceBarcode2.ConnectStatus())
            {
                KeyenceBarcode2.Sent("LON" + "\r\n");
                Thread.Sleep(1000);
                RefreshDifferentThreadUI(tbMassage2, () =>
                {
                    tbMassage2.Text = KeyenceBarcode2.Receive();
                });
            }
            return true;
        }

        private void btServer_Click(object sender, EventArgs e)
        {
            TrigerBarcode();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //H1_VFiducial.RunLive(cogRecDisp_H1_Recipe);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            OB_RobotLight.On();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            OB_RobotLight.Off();
        }

        private void button23_Click_1(object sender, EventArgs e)
        {
            H1_PickupPos.RunLiveCCDIndex = 0;
            H1_PickupPos.RunLive(cogRecDisp_H1_Recipe, 3);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            H1_PickupPos.EditTB();
        }
        private void button53_Click(object sender, EventArgs e)
        {
            H1_NGCCDCheck.EditTB();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            //H1_VCalibration.EditTB();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            //{
            //    H1_VCalibration.RunTB();
            //    while (!H1_VCalibration.RunTBOk()) { Thread.Sleep(2); }
            //    if (H1_VCalibration.IsAccept)
            //    {
            //        RefreshDifferentThreadUI(txt_H1_CalibResult, () =>
            //        {
            //            txt_H1_CalibResult.Text = "OK";
            //            txt_H1_CalibResult.BackColor = Color.Lime;
            //            txt_H1_CalibPixelX.Text = H1_VCalibration.Calibration.x.ToString("F3");
            //            txt_H1_CalibPixelY.Text = H1_VCalibration.Calibration.y.ToString("F3");
            //        });
            //    }
            //    else
            //    {
            //        RefreshDifferentThreadUI(txt_H1_CalibResult, () =>
            //        {
            //            txt_H1_CalibResult.BackColor = Color.Red;
            //            txt_H1_CalibResult.Text = "NG";
            //            txt_H1_CalibPixelX.Text = "Null";
            //            txt_H1_CalibPixelY.Text = "Null";
            //        });
            //    }
            //}));


            //th.SetApartmentState(ApartmentState.STA);
            //th.Start();
        }

        public bool Manual_GotoXYZ(double XPos, double YPos, double ZPos)
        {
            bool InPosition = false;
            return InPosition;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //DialogResult reult = MessageBox.Show("Sure Add Calib Point?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            //if (reult == DialogResult.No)
            //{
            //    return;
            //}
            //if (H1_VCalibration.IsAccept)
            //{
            //    DataTable dt = SettingData.Tables["VisCalib"];
            //    DataRow dr = dt.NewRow();
            //    dr[0] = H1_VCalibration.Calibration.x.ToString("F3");
            //    dr[1] = H1_VCalibration.Calibration.y.ToString("F3");
            //    dr[2] = Robotx.Text;
            //    dr[3] = Roboty.Text;
            //    dt.Rows.Add(dr);
            //}
            //else
            //    MessageBox.Show("Please trigger fiducial calibration");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Delete Calib Point?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            if (dgv_H2_VisCalib.CurrentRow != null)
            {
                DataTable dt = SettingData.Tables["VisCalib"];
                dt.Rows.RemoveAt(dgv_H2_VisCalib.CurrentRow.Index);
            }
        }

        private void Replace_Click(object sender, EventArgs e)
        {
            //DialogResult reult = MessageBox.Show("Sure Replace Calib Point?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            //if (reult == DialogResult.No)
            //{
            //    return;
            //}
            //if (H1_VCalibration.IsAccept)
            //{
            //    DataTable dt = SettingData.Tables["VisCalib"];
            //    DataRow dr = dt.Rows[dgv_H2_VisCalib.CurrentRow.Index];
            //    dr[0] = H1_VCalibration.Calibration.x;
            //    dr[1] = H1_VCalibration.Calibration.y;
            //}
            //else
            //    MessageBox.Show("Please trigger fiducial calibration");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //DialogResult reult = MessageBox.Show("Sure Calibration?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            //if (reult == DialogResult.No)
            //{
            //    return;
            //}
            //if (SettingData.Tables["VisCalib"].Rows.Count >= 9)
            //{
            //    List<CalibrationData> CalibrationDataList = new List<CalibrationData>();
            //    for (int i = 0; i < SettingData.Tables["VisCalib"].Rows.Count; i++)
            //    {
            //        CalibrationData CalibData = new CalibrationData();
            //        CalibData.PixelX = Convert.ToDouble(SettingData.Tables["VisCalib"].Rows[i][0]);
            //        CalibData.PixelY = Convert.ToDouble(SettingData.Tables["VisCalib"].Rows[i][1]);
            //        CalibData.MotorPosX = Convert.ToDouble(SettingData.Tables["VisCalib"].Rows[i][2]);
            //        CalibData.MotorPosY = Convert.ToDouble(SettingData.Tables["VisCalib"].Rows[i][3]);
            //        CalibrationDataList.Add(CalibData);
            //    }
            //    if (H1_VFiducial.SetCalibration(CalibrationDataList, "Calibration"))
            //        MessageBox.Show("Calibration successful");
            //    else
            //        MessageBox.Show("Calibration fail1");
            //    if (H1_VFiducia2.SetCalibration(CalibrationDataList, "Calibration"))
            //        MessageBox.Show("Calibration successfu2");
            //    else
            //        MessageBox.Show("Calibration fail2");
            //    if (H1_VFiducia3.SetCalibration(CalibrationDataList, "Calibration"))
            //        MessageBox.Show("Calibration successful3");
            //    else
            //        MessageBox.Show("Calibration fail3");
            //}
            //else
            //    MessageBox.Show("At least 9 calibration points");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //H1_VFiducial.EditTB();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //System.Threading.Tasks.Task.Factory.StartNew(() =>
            //{
            //    H1_VFiducial.RunTB();
            //    while (!H1_VFiducial.RunTBOk()) { Thread.Sleep(2); }
            //    if (H1_VFiducial.IsAccept)
            //    {
            //        RefreshDifferentThreadUI(txt_H1_FiducialResult, () =>
            //        {
            //            txt_H1_FiducialResult.Text = "OK";
            //            txt_H1_FiducialResult.BackColor = Color.Lime;
            //            txt_H1_FiducialX.Text = H1_VFiducial.Fiducial.x.ToString("F3");
            //            txt_H1_FiducialY.Text = H1_VFiducial.Fiducial.y.ToString("F3");
            //            if (H1_VFiducial.Fiducial.u > 0)
            //            {
            //                H1_VFiducial.Fiducial.u = (H1_VFiducial.Fiducial.u * 180 / Math.PI);
            //                H1_VFiducial.Fiducial.u = H1_VFiducial.Fiducial.u - 360;
            //                txt_H1_FiducialU.Text = H1_VFiducial.Fiducial.u.ToString("F3");
            //            }
            //            else
            //            {
            //                txt_H1_FiducialU.Text = (H1_VFiducial.Fiducial.u * 180 / Math.PI).ToString("F3");
            //            }
            //        });
            //    }
            //    else
            //    {
            //        RefreshDifferentThreadUI(txt_H1_FiducialResult, () =>
            //        {
            //            txt_H1_FiducialResult.BackColor = Color.Red;
            //            txt_H1_FiducialResult.Text = "NG";
            //            txt_H1_FiducialX.Text = "Null";
            //            txt_H1_FiducialY.Text = "Null";
            //            txt_H1_FiducialU.Text = "Null";
            //        });
            //    }
            //});
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // H1_VFiducial2.RunLive(cogRecDisp_H2_Recipe);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //OB_CCDLight.On();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //OB_CCDLight.Off();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //H1_VFiducial2.EditTB();
        }

        private void Btn_TheWide_Click(object sender, EventArgs e)
        {
            //MotorControlForm MCF = new MotorControlForm();
            //MCF.Initial(sender);
            //MCF.ShowDialog();
        }

        //private void button32_Click(object sender, EventArgs e)
        //{
        //    bConnectSP();
        //}

        //private void button31_Click(object sender, EventArgs e)
        //{
        //    serialPort2.Close();
        //    serialPort3.Close();
        //}

        public bool Trigger(bool State)
        {
            bool ret = false;
            if (!serialPort2.IsOpen)
            {
                ret = false;
                return ret;
            }
            if (State)
            {
                ret = true;
                serialPort2.Write("READ[OD][OA]" + "\r\n");
                Thread.Sleep(200);
            }
            else
            {

            }
            return ret;
        }

        public bool Trigger2(bool State)
        {
            bool ret = false;
            if (!serialPort3.IsOpen)
            {
                ret = false;
                return ret;
            }
            if (State)
            {
                ret = true;
                serialPort3.Write("READ[OD][OA]" + "\r\n");
                Thread.Sleep(200);
            }
            else
            {

            }
            return ret;
        }

        //public bool reception()
        //{
        //    bool ret = false;
        //    double dist = 0;
        //    Trigger(true);
        //    string res = serialPort2.ReadExisting().Trim();
        //    //Convert.ToInt32(res, 16);
        //    //if(double.TryParse(res.Substring(7),out dist))
        //    //{
        //    RefreshDifferentThreadUI(Pressure, () =>
        //    {
        //        Pressure.Clear();
        //        string r1 = res;
        //        r1 = r1.Replace("+", "-");
        //        string[] strArray = r1.Split('-');
        //        if (strArray.Length > 1)
        //        {
        //            r1 = strArray[strArray.Length - 1];
        //            r1 = r1.Replace("?", "");
        //        }
        //        r1 = r1.Trim('.');
        //        Pressure.Text = r1;
        //        ret = true;
        //    });
        //    //}
        //    return ret;
        //}

        //public bool reception2()
        //{
        //    bool ret = false;
        //    double dist = 0;
        //    Trigger2(true);
        //    string res = serialPort3.ReadExisting();//().Trim();
        //    RefreshDifferentThreadUI(Pressure, () =>
        //    {
        //        Pressure.Clear();
        //        string r1 = res;
        //        r1 = r1.Replace("+", "-");
        //        string[] strArray = r1.Split('-');
        //        if (strArray.Length > 1)
        //        {
        //            r1 = strArray[strArray.Length - 1];
        //            r1 = r1.Replace("?", "");
        //        }
        //        r1 = r1.Trim('.');
        //        Pressure.Text = r1;
        //        ret = true;
        //    });
        //    //}
        //    return ret;
        //}

        //private void button30_Click(object sender, EventArgs e)
        //{
        //    Pressure1();
        //    //textBox13.Text = sCommand; //sPressureData(sCommand).Substring(9);
        //}

        private void button45_Click(object sender, EventArgs e)
        {

        }

        private void button46_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MiddleLayer.SystemF.IB_ModeSwitch.On())
            {
                if (!bt_ReadIO.Enabled)
                {
                    bt_ReadIO.Enabled = true;
                    bt_WriteIO.Enabled = true;
                }
            }
            else
            {
                if (bt_ReadIO.Enabled)
                {
                    bt_ReadIO.Enabled = false;
                    bt_WriteIO.Enabled = false;
                }
            }

            if (PFClient.TighteningResultUpdated)
            {
                //FasternDataGetResult = true;
                //textBox2.Text = PFClient.LastTighteningResult.TORQUE.ToString("F3");
                //textBox3.Text = PFClient.LastTighteningResult.ANGLE.ToString("F3");
                //PFClient.TighteningResultUpdated = false;
            }
            else
            {
                if (bConnect && bComStart && bSubscribe)
                {
                    PFKeepAlive();
                }
            }
            if (PFClient2.TighteningResultUpdated2)
            {
                //FasternDataGetResult = true;
                //textBox2.Text = PFClient.LastTighteningResult.TORQUE.ToString("F3");
                //textBox3.Text = PFClient.LastTighteningResult.ANGLE.ToString("F3");
                //PFClient.TighteningResultUpdated = false;
            }
            else
            {
                if (bConnect2 && bComStart2 && bSubscribe2)
                {
                    PFKeepAlive2();
                }
            }

            //bool r1 = IB_Robot_AbnormalAlarm.On();
            //bool r2 = IB_Robot_BatteryAlarm.On();
            //bool r3 = IB_Screw_Alarm.On();
            //bool r4 = IB_Feeder_Alarm.On();
            //bool r5 = !MiddleLayer.ConveyorF.IB_TopCVY_Alarm.On();
            //bool r6 = !MiddleLayer.ConveyorF.IB_BottomCVY_Alarm.On();
            ////bool r7 = MiddleLayer.ConveyorF.IB_Device_Alarm.On();
            ////bool r8 = MiddleLayer.ConveyorF.IB_Device_Lack.On();
            //bool r9 = IB_Screw_Alarm2.On();
            //bool r10 = IB_Feeder_Alarm2.On();

            //if(EnableRobot)
            //{


            //    if (r1)
            //    {
            //            SysPara.NPShowAlarm("5040");
            //    }
            //    if (r2)
            //    {
            //        SysPara.NPShowAlarm("5041");
            //    }
            //    if (r3)
            //    {
            //        SysPara.NPShowAlarm("5042");
            //    }
            //    if (r4)
            //    {
            //        SysPara.NPShowAlarm("5043");
            //    }
            //    if (r5)
            //    {
            //        SysPara.NPShowAlarm("5044");
            //    }
            //    if (r6)
            //    {
            //        SysPara.NPShowAlarm("5045");
            //    }
            //    //if (r7)
            //    //{
            //    //    SysPara.NPShowAlarm("5046");
            //    //}
            //    //if (r8)
            //    //{
            //    //    SysPara.NPShowAlarm("5047");
            //    //}
            //    if (r9)
            //    {
            //        SysPara.NPShowAlarm("5048");
            //    }
            //    if (r10)
            //    {
            //        SysPara.NPShowAlarm("5049");
            //    }
            //}
            if (MiddleLayer.ProcessF.GetSettingValue("PSet", "dcEnableCheckProcess"))
            {
                SysPara.NPShowAlarm("0008");
            }
            else
            {
                SDKKernal.ClearAlarm("0008");


            }

            if (MiddleLayer.ProcessF.GetSettingValue("PSet", "IsDryCycleMode"))
            {
                SysPara.NPShowAlarm("0011");
            }
            else
            {
                SDKKernal.ClearAlarm("0011");


            }


            if (MiddleLayer.ProcessF.GetSettingValue("PSet", "disableCheckPCBcodes"))
            {
                SysPara.NPShowAlarm("1304");
            }
            else
            {
                SDKKernal.ClearAlarm("1304");


            }
            if (MiddleLayer.ProcessF.GetSettingValue("PSet", "DisableCheckQRcodes"))
            {
                SysPara.NPShowAlarm("1303");
            }
            else
            {
                SDKKernal.ClearAlarm("1303");


            }
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "DisableVision"))
            {
                SysPara.NPShowAlarm("0014");
            }
            else
            {
                SDKKernal.ClearAlarm("0014");


            }
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "DisableFeeder"))
            {
                SysPara.NPShowAlarm("0013");
            }
            else
            {
                SDKKernal.ClearAlarm("0013");


            }



        }

        private void button38_Click(object sender, EventArgs e)
        {
            KeyenceBarcode.Disconnect();
        }

        private FCResultType flowChart2_25_FlowRun(object sender, EventArgs e)
        {
            bool r2 = SysPara.bDryCycle;
            if (r2)
            {
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }



        private FCResultType flowChart4_1_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.IDLE;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //H1_VFiducia2.EditTB();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //System.Threading.Tasks.Task.Factory.StartNew(() =>
            //{
            //    H1_VFiducia2.RunTB();
            //    while (!H1_VFiducia2.RunTBOk()) { Thread.Sleep(2); }
            //    if (H1_VFiducia2.IsAccept)
            //    {
            //        RefreshDifferentThreadUI(txt_H2_FiducialResult, () =>
            //        {
            //            txt_H2_FiducialResult.Text = "OK";
            //            txt_H2_FiducialResult.BackColor = Color.Lime;
            //            txt_H2_FiducialX.Text = H1_VFiducia2.Fiducial.x.ToString("F3");
            //            txt_H2_FiducialY.Text = H1_VFiducia2.Fiducial.y.ToString("F3");
            //            if (H1_VFiducia2.Fiducial.u > 0)
            //            {
            //                H1_VFiducia2.Fiducial.u = (H1_VFiducia2.Fiducial.u * 180 / Math.PI);
            //                H1_VFiducia2.Fiducial.u = H1_VFiducia2.Fiducial.u - 360;
            //                txt_H2_FiducialU.Text = H1_VFiducia2.Fiducial.u.ToString("F3");
            //            }
            //            else
            //            {
            //                txt_H2_FiducialU.Text = (H1_VFiducia2.Fiducial.u * 180 / Math.PI).ToString("F3");
            //            }
            //        });
            //    }
            //    else
            //    {
            //        RefreshDifferentThreadUI(txt_H2_FiducialResult, () =>
            //        {
            //            txt_H2_FiducialResult.BackColor = Color.Red;
            //            txt_H2_FiducialResult.Text = "NG";
            //            txt_H2_FiducialX.Text = "Null";
            //            txt_H2_FiducialY.Text = "Null";
            //            txt_H2_FiducialU.Text = "Null";
            //        });
            //    }
            //});
        }

        private void button37_Click(object sender, EventArgs e)
        {
            //H1_VFiducia3.EditTB();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            //System.Threading.Tasks.Task.Factory.StartNew(() =>
            //{
            //    H1_VFiducia3.RunTB();
            //    while (!H1_VFiducia3.RunTBOk()) { Thread.Sleep(2); }
            //    if (H1_VFiducia3.IsAccept)
            //    {
            //        RefreshDifferentThreadUI(txt_H3_FiducialResult, () =>
            //        {
            //            txt_H3_FiducialResult.Text = "OK";
            //            txt_H3_FiducialResult.BackColor = Color.Lime;
            //            txt_H3_FiducialX.Text = H1_VFiducia3.Fiducial.x.ToString("F3");
            //            txt_H3_FiducialY.Text = H1_VFiducia3.Fiducial.y.ToString("F3");
            //            if (H1_VFiducia3.Fiducial.u > 0)
            //            {
            //                H1_VFiducia3.Fiducial.u = (H1_VFiducia3.Fiducial.u * 180 / Math.PI);
            //                H1_VFiducia3.Fiducial.u = H1_VFiducia3.Fiducial.u - 360;
            //                txt_H3_FiducialU.Text = H1_VFiducia3.Fiducial.u.ToString("F3");
            //            }
            //            else
            //            {
            //                txt_H3_FiducialU.Text = (H1_VFiducia3.Fiducial.u * 180 / Math.PI).ToString("F3");
            //            }
            //        });
            //    }
            //    else
            //    {
            //        RefreshDifferentThreadUI(txt_H2_FiducialResult, () =>
            //        {
            //            txt_H3_FiducialResult.BackColor = Color.Red;
            //            txt_H3_FiducialResult.Text = "NG";
            //            txt_H3_FiducialX.Text = "Null";
            //            txt_H3_FiducialY.Text = "Null";
            //            txt_H3_FiducialU.Text = "Null";
            //        });
            //    }
            //});
        }

        private void button44_Click(object sender, EventArgs e)
        {
            //H1_VFiducia4.EditTB();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            //DialogResult reult = MessageBox.Show("Sure Add Calib Point?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            //if (reult == DialogResult.No)
            //{
            //    return;
            //}
            //if (H1_VFiducia3.IsAccept)
            //{
            //    DataTable dt = SettingData.Tables["VisCalib"];
            //    DataRow dr = dt.NewRow();
            //    dr[0] = H1_VFiducia3.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducia3.Fiducial.y.ToString("F3");
            //    dr[2] = "OK";
            //    //dr[3] = Roboty.Text;
            //    dt.Rows.Add(dr);
            //}
            //else
            //    MessageBox.Show("Please trigger fiducial calibration");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //OB_CCDLight.On();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //OB_CCDLight.Off();
        }

        private void button26_Click_1(object sender, EventArgs e)
        {
            //OB_CCDLight.On();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            //OB_CCDLight.Off();
        }

        //private void button29_Click(object sender, EventArgs e)
        //{
        //    H1_VFiducia4.RunLive(cogRecDisp_H4_Recipe);
        //}

        private void button30_Click(object sender, EventArgs e)
        {
            //OB_CCDLight.On();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            //OB_CCDLight.Off();
        }
        private void ResetRobot()
        {
            if (ConnectEpsonRobot())
            {
                OB_RobotPause.Off();
                OB_RobotStart.Off();
                OB_RobotProg1.Off();
                OB_RobotProg2.Off();
                OB_RobotProg3.Off();
                OB_RobotStop.Off();
                OB_RobotContinue.Off();
                OB_RobotReset.Off();

                OB_RobotStop.On();
                DelayMs(500);
                OB_RobotStop.Off();
                OB_RobotReset.On();
                DelayMs(500);
                OB_RobotReset.Off();
            }
            else
            {
                SysPara.NPShowAlarm("1635");
            }
        }
        private void startRobot()
        {
            if (IB_RobotReady.On())
            {
                OB_RobotStart.On();
                DelayMs(1000);
                OB_RobotStart.Off();
            }
        }

        public bool SendAutoCalibration(int index, int CameraIndex)
        {
            if (CameraIndex == 1)
            {
                RobotPose[0] = Convert.ToDouble(txtPickPosRobotX.Text);
                RobotPose[1] = Convert.ToDouble(txtPickPosRobotY.Text);
                RobotPose[2] = Convert.ToDouble(txtPickPosRobotU.Text);
            }
            else if (CameraIndex == 2)
            {
                RobotPose[0] = Convert.ToDouble(textBox39.Text);
                RobotPose[1] = Convert.ToDouble(textBox40.Text);
                RobotPose[2] = Convert.ToDouble(textBox41.Text);
            }
            switch (index)
            {
                case 1:
                    RobotPose[0] = RobotPose[0] - 5; RobotPose[1] = RobotPose[1] - 5; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "-05.000", "-05.000", "000.000");
                case 2:
                    RobotPose[0] = RobotPose[0] - 0; RobotPose[1] = RobotPose[1] - 5; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "000.000", "-05.000", "000.000");
                case 3:
                    RobotPose[0] = RobotPose[0] + 5; RobotPose[1] = RobotPose[1] - 5; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "005.000", "-05.000", "000.000");
                case 4:
                    RobotPose[0] = RobotPose[0] + 5; RobotPose[1] = RobotPose[1] - 0; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "005.000", "000.000", "000.000");
                case 5:
                    RobotPose[0] = RobotPose[0] + 0; RobotPose[1] = RobotPose[1] - 0; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "000.000", "000.000", "000.000");
                case 6:
                    RobotPose[0] = RobotPose[0] - 5; RobotPose[1] = RobotPose[1] - 0; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "-05.000", "000.000", "000.000");
                case 7:
                    RobotPose[0] = RobotPose[0] - 5; RobotPose[1] = RobotPose[1] + 5; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "-05.000", "005.000", "000.000");
                case 8:
                    RobotPose[0] = RobotPose[0] - 0; RobotPose[1] = RobotPose[1] + 5; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "000.000", "005.000", "000.000");
                case 9:
                    RobotPose[0] = RobotPose[0] + 5; RobotPose[1] = RobotPose[1] + 5; RobotPose[2] = RobotPose[2] - 0;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "005.000", "005.000", "000.000");
                case 10:
                    RobotPose[0] = RobotPose[0] - 5; RobotPose[1] = RobotPose[1] - 5; RobotPose[2] = RobotPose[2] + 3;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "-05.000", "-05.000", "003.000");
                case 11:
                    RobotPose[0] = RobotPose[0] - 5; RobotPose[1] = RobotPose[1] - 5; RobotPose[2] = RobotPose[2] - 3;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "-05.000", "-05.000", "-03.000");
                case 12:
                    RobotPose[0] = RobotPose[0] + 0; RobotPose[1] = RobotPose[1] + 0; RobotPose[2] = RobotPose[2] + 3;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "000.000", "000.000", "003.000");
                case 13:
                    RobotPose[0] = RobotPose[0] + 0; RobotPose[1] = RobotPose[1] + 0; RobotPose[2] = RobotPose[2] + 6;
                    return Robot.WriteToEpson("014", (100 + index).ToString("FFF"), "FF", 30000, "000.000", "000.000", "006.000");
                default:
                    return true;
            }
        }

        #region Initial
        //MOD@@ Guillermo Carrillo - Reinicio rapido
        //  Usa la MISMA ruta del proveedor: MainForm.fnInitial(), que resetea
        //  todos los modulos (Conveyor, Process, Scanner), reinicia el
        //  temporizador del Epson y arranca el ciclo. Lo unico que cambia es
        //  que la bandera bRestartQuickActive hace que tres nodos lentos de la
        //  cadena del Gantry devuelvan NEXT de inmediato:
        //    npFlowChart49  delete files / FilesMonitor
        //    flowChart1_2   Connect scanner
        //    flowChart1_3   Close CCDlight / connect CCD (3 fotos de prueba)
        //  Todo lo demas corre igual que una inicializacion completa: reset y
        //  arranque del robot, goto safety, descarga a NG, MES y banderas.
        //  La bandera se apaga en npFlowChart1 (Initial finish).
        public void RestartProcessQuick()
        {
            if (bRestartQuickActive)
            {
                MessageBox.Show("El reinicio rapido ya esta en proceso.");
                return;
            }

            reintento.Dispose();
            reintento = new Reintento();
            reintento.fnSetTextMessageNShow(
                "REINICIO RAPIDO. Se hara la inicializacion saltando escaner y camaras. Confirme que el area esta despejada.",
                true, false, true);
            if (reintento.dResult != DialogResult.Yes)
            {
                return;
            }

            MiddleLayer.LogF.AddLog(LogType.Production,
                "Reinicio rapido: inicia, se saltan escaner y camaras.", true);

            bRestartQuickActive = true;
        }

        private FCResultType fc_InitialStart_FlowRun(object sender, EventArgs e)
        {
            throwingjudge = false;
            CCDAlogrithmStatus = false;
            AssmbleCCDAlogStatus = false;
            isPickProduceOK = true;
            curPalletCode = "";
            curR1HaveNum = 0;
            SysPara.RIniRet = false;
            SysPara.JAGStarytEvent.Reset();
            SysPara.OverPressureab = false;
            SysPara.OverPressurebc = false;
            SysPara.Que_ProductCode_PickPos.Clear();
            SysPara.Que_pallet_Code.Clear();
            SysPara.Que_Product_pallet_Code.Clear();
            SysPara.R1GrabCompeletStatus = false;            //R1 Grab feeder product compelet flag
            SysPara.ScanCount = 0;                            //feeder scan count
            SysPara.R1ScanResult = false;                    //scan status
            SysPara.R1AssembStatus = false;                  //Notice robot to assemb
            MiddleLayer.ConveyorF.UpConveyorAutoFlow.NotifyAssembly = false;
            SysPara.ProductCount = 12;                        //assemb count
            SysPara.FeederProductCount = 42;                        //feeder product count
            SysPara.R1AssembResult = false;                  //assemb result
            SysPara.R1AssembCompeletStatus = false;      //R1 Assemb compelet flag
            MiddleLayer.ConveyorF.UpConveyorAutoFlow.AssembCompeletStatus = false;
            SysPara.TempAssembCompeletStatus = false;
            SysPara.PressureCompelet = false;
            SysPara.RobotPressureCompelet = false;
            scannerReuslt.Clear();
            CCDReuslt.Clear();
            queCCDPosition.Clear();
            scanCount = 0;
            RunTM.Restart();
            curProductCount = 0;
            haveSendCodeToMES = true;
            palletUpResult.Clear();
            palletDownResult.Clear();
            InitPickIndex(GetRecipeValue("RSet", "FeederPickIndex"));

            MiddleLayer.ConveyorF.UpConveyorAutoFlow.DryRunMode = MiddleLayer.ProcessF.GetSettingValue("PSet", "IsDryCycleMode");
            MiddleLayer.ConveyorF.UpConveyorAutoFlow.ByPassMode = MiddleLayer.ProcessF.GetSettingValue("PSet", "dcEnableCheckProcess");
            MiddleLayer.ConveyorF.UpConveyorAutoFlow.DisbleInOutDoor = true;

            MesTestList = new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true };
            for (int i = 0; i < PCBCode.Length; i++)
            {
                PCBCode[i] = "NA";
            }

            return FCResultType.NEXT;
        }
        //connect scanner
        private FCResultType flowChart1_2_FlowRun(object sender, EventArgs e)
        {
            //MOD@@ Guillermo Carrillo - salto del reinicio rapido: escaner ya conectado
            if (bRestartQuickActive) { return FCResultType.NEXT; }
            bool status1 = ConnectBarcode();
            if (status1)
            {
                return FCResultType.NEXT;
            }
            else
            {
                if (!status1)
                {

                    SysPara.NPShowAlarm("1300");
                }      //Scanner1 connect Failed
                return FCResultType.IDLE;
            }

        }
        //close ccd light
        private FCResultType flowChart1_3_FlowRun(object sender, EventArgs e)
        {
            //MOD@@ Guillermo Carrillo - salto del reinicio rapido: camaras ya probadas
            if (bRestartQuickActive) { OB_RobotLight.Off(); return FCResultType.NEXT; }
            OB_RobotLight.Off();
            //SettingData - MSet.NGCCDCheckExpTime
            double exposure = GetSettingValue("MSet", "NGCCDCheckExpTime");
            double exposure1 = GetSettingValue("MSet", "PickExpTime");
            double exposure2 = GetSettingValue("MSet", "DisChargeExpTime");
            if (H1_PickupPos.TakePicture(exposure1) && H1_NGCCDCheck.TakePicture(exposure) && H1_DischargePos.TakePicture(exposure2))
            {
                return FCResultType.NEXT;
            }
            else
            {

                SysPara.NPShowAlarm("1204");
                return FCResultType.IDLE;
            }

        }
        //connect preasure 
        private FCResultType flowChart1_4_FlowRun(object sender, EventArgs e)
        {
            string comStr = MiddleLayer.GantryF.GetSettingValue("MSet", "ComPort");
            string paraStr = MiddleLayer.GantryF.GetSettingValue("MSet", "BortRate") + ","
                + MiddleLayer.GantryF.GetSettingValue("MSet", "DataBits") + "," +
                MiddleLayer.GantryF.GetSettingValue("MSet", "Parity") + ","
                + MiddleLayer.GantryF.GetSettingValue("MSet", "StopBit");
            if (PressReadCOM.ConnectCom1(comStr, paraStr))
            {
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        //Reset Robot
        private FCResultType flowChart1_5_FlowRun(object sender, EventArgs e)
        {
            if (ConnectEpsonRobot())
            {
                OB_RobotPause.Off();
                OB_RobotStart.Off();
                OB_RobotProg1.Off();
                OB_RobotProg2.Off();
                OB_RobotProg3.Off();
                OB_RobotStop.Off();
                OB_RobotContinue.Off();
                OB_RobotReset.Off();

                OB_RobotStop.On();
                DelayMs(500);
                OB_RobotStop.Off();
                OB_RobotReset.On();
                DelayMs(500);
                OB_RobotReset.Off();
                return FCResultType.NEXT;
            }
            else
            {

                SysPara.NPShowAlarm("1634");
                return FCResultType.IDLE;
            }
        }
        //Start Robot
        private FCResultType flowChart1_6_FlowRun(object sender, EventArgs e)
        {

            if (IB_RobotReady.On())
            {
                startRobot();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }
        // wait reset robot compelet
        private FCResultType flowChart1_7_FlowRun(object sender, EventArgs e)
        {
            if (IB_RobotBusy.On()) { InitTM.Restart(); return FCResultType.NEXT; }
            else { return FCResultType.IDLE; }
        }
        //goto safety
        private FCResultType flowChart1_8_FlowRun(object sender, EventArgs e)
        {

            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Goto Safety");
            if (Robot.WriteToEpson(pos[2], pos[3], pos[4], 30000))
            {
                InitTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2]
               + "Pos：" + pos[3]
               + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else
            {
                if (InitTM.On(SysPara.TimeOutSec))
                {

                    SysPara.NPShowAlarm("1635");
                }
                return FCResultType.IDLE;
            }
        }

        //wait goto safety
        private FCResultType flowChart6_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                OB_FeederSafetySignal.On();
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                if (Robot.ErorrCode == "E9000" ||
                    Robot.ErorrCode == "E9001" ||
                    Robot.ErorrCode == "E9002")
                {
                    return FCResultType.CASE1;
                }
                else { return FCResultType.CASE2; }

            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635"); return FCResultType.IDLE;
            }        //R1 timeout
            else
            {
                return FCResultType.IDLE;
            }
        }
        private FCResultType flowChart45_FlowRun_1(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(MiddleLayer.GantryF.dgv_EpsonPosition, "Write Robot Speed");
            if (Robot.WriteToEpson(pos[2], pos[3], pos[4], 30000, string.Format("{0:d7}", Convert.ToInt16(GetSettingValue("PSet", "R1MachineSpeedRatio")))))
            {
                InitTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command:" + pos[2]
    + "Pos:" + pos[3]
    + "AppIndex:" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart27_FlowRun_1(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 104)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == -104)
            {

                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout
            else { return FCResultType.IDLE; }


        }
        //feeder is Alarm
        private FCResultType flowChart7_FlowRun(object sender, EventArgs e)
        {
            if (IB_FeederEmergencyStop.On())
            {
                SysPara.NPShowAlarm("1101");       //feeder alarm
                return FCResultType.IDLE;
            }
            else
            {

                if (IB_FeederAlarm.On())
                {
                    SysPara.NPShowAlarm("1100");       //feeder alarm
                    InitTM.Restart(); return FCResultType.CASE1;
                }
                else
                {
                    OB_FeederSafetySignal.On();
                    return FCResultType.NEXT;
                }
            }
        }
        //feeder reset
        private FCResultType flowChart9_FlowRun(object sender, EventArgs e)
        {
            OB_FeederAlmReset.On();
            if (InitTM.On(500)) { OB_FeederAlmReset.Off(); return FCResultType.NEXT; }
            else { return FCResultType.IDLE; }
        }
        // Initialok
        private FCResultType flowChart8_FlowRun(object sender, EventArgs e)
        {

            {
                return FCResultType.NEXT;
            }
        }
        #endregion
        #region AutoRun
        private FCResultType flowChart10_FlowRun(object sender, EventArgs e)
        {
            gripperIndex = 1;
            MESCheckAllFail = false;

            GotoMESCheck = true; // after init, the 1st time
            GotoPick = false; // init
            MESFinishReport = false; // init

            MiddleLayer.ConveyorF.UpConveyorAutoFlow.AssembCompeletStatus = false;
            MiddleLayer.ConveyorF.UpConveyorAutoFlow.NotifyAssembly = false;
            SysPara.TempAssembCompeletStatus = false;
            // to check the data form work flow, and no update to mes
            if (SysPara.MesDebug)
            {
                MessageBox.Show("It's 'Mes Debug' Mode now, please notice");
            }

            if (SysPara.bBypassMode) { return FCResultType.IDLE; }
            if (SysPara.bDryCycle)
            {
                SysPara.ScanCount = 0;
                DataTable dt = RecipeData.Tables["RSet"];
                dt.Rows[0]["FeederPickIndex"] = SysPara.ScanCount;
                dt.AcceptChanges();
                this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            }


            for (int i = 0; i < MaterialID_Array.Length; i++)
            {
                MaterialID_Array[i] = "";
            }
            for (int i = 0; i < MaterialID_Array_Copy.Length; i++)
            {
                MaterialID_Array_Copy[i] = "";
            }
            CurrentPCBList.Clear();

            RunTM.Restart();
            return FCResultType.NEXT;
        }
        //Waiting for Feeder incoming
        private FCResultType flowChart11_FlowRun(object sender, EventArgs e)
        {
            if (MESLib.CommParas.EnableMes)
            {
                MESLib.CommParas.LOT_START_VALUE_LIST.Clear();
                if (
                    SysPara.Que_ProductCode_PickPos.Count == 0)
                {
                    if (!MESCheckAllFail)
                    {
                        for (int i = 0; i < LotIDs[i].Length; i++)
                        {
                            LotIDs[i] = "";
                        }
                    }
                }
            }
            SysPara.ScanCount = GetRecipeValue("RSet", "FeederPickIndex");

            int ScanNumber = SysPara.ScanCount;
            if (scannerReuslt.Count == 0)
            {
                if ((SysPara.ScanCount) % 3 != 0)
                {
                    ScanNumber = 3 * ((SysPara.ScanCount / 3) + 1);
                }
                if (ScanNumber >= 42)
                {
                    //SysPara.JAGStarytEvent.Set();
                    ScanNumber = 0;
                    feederT.Restart();
                }
                SysPara.ScanCount = ScanNumber;
                DataTable dt = RecipeData.Tables["RSet"];
                dt.Rows[0]["FeederPickIndex"] = SysPara.ScanCount;
                dt.AcceptChanges();
                this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));


            }

            if (SysPara.bDryCycle)
            { return FCResultType.NEXT; }

            if (SysPara.Que_Product_pallet_Code.Count == 0 && SysPara.Que_ProductCode_PickPos.Count == 0)
            {
                SysPara.PalletProductCount = 0;
                if (!MESCheckAllFail)
                    MESLib.CommParas.MesManager.InitMesParas();

            }

            if (GetSettingValue("MSet", "EPlaningCounts"))
            {
                int Planing = GetSettingValue("MSet", "ProQuantity");
                int finished = GetRecipeValue("RSet", "finishedCounts");
                if (Planing <= finished)
                {
                    SysPara.NPShowAlarm("0009");
                    return FCResultType.IDLE;
                }
            }


            if ((IB_FeederInPlace.On() && feederT.On(300)) || MiddleLayer.SystemF.GetSettingValue("PSet", "DisableFeeder"))
            {

                return FCResultType.NEXT;
            }
            else
            {
                if (RunTM.On(40000))
                {
                    SysPara.NPShowAlarm("1102");        //feeder inplace timeout
                }
            }
            return FCResultType.IDLE;
        }
        //Scan pos index Counter
        private FCResultType flowChart12_FlowRun(object sender, EventArgs e)
        {
            SysPara.ScanCount = GetRecipeValue("RSet", "FeederPickIndex");
            if (SysPara.ScanCount >= SysPara.FeederProductCount) { SysPara.ScanCount = 0; }
            SysPara.ScanCount++;
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["FeederPickIndex"] = SysPara.ScanCount;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            MiddleLayer.LogF.AddLog(LogType.Production, "feeder scan Index：" + SysPara.ScanCount, SysPara.bEnableGeneralSaveLog);

            //Log.log.Write("feeder scan Index：" + SysPara.ScanCount, Color.Black);
            return FCResultType.NEXT;
        }


        //Judge scan success and count
        private FCResultType flowChart16_FlowRun(object sender, EventArgs e)
        {

            scannerReuslt.Add(SysPara.R1ScanResult);

            //MOD@@ Guillermo Carrillo - guarda de indice: MaterialID_Array
            if (SysPara.Que_ProductCode_PickPos.Count < MaterialID_Array.Length)
            {
                MaterialID_Array[SysPara.Que_ProductCode_PickPos.Count] = SysPara.QRCode1;
            }

            ProductCode_PickPos code_PickPos = new ProductCode_PickPos();       //scan success，Record productCode and Index
            code_PickPos.Code = SysPara.QRCode1;
            code_PickPos.PosIndex = SysPara.ScanCount;
            if (isPickProduceOK)
            {
                SysPara.Que_ProductCode_PickPos.Enqueue(code_PickPos);
            }        //if pick success,Enqueue in Queue
            else                                                                                //if pick fail,replace the first
            {
                //MOD@@ Guillermo Carrillo - guarda de indice: Que_ProductCode_PickPos
                if (SysPara.Que_ProductCode_PickPos.Count > 0
                    && curR1HaveNum >= 0
                    && curR1HaveNum < SysPara.Que_ProductCode_PickPos.Count)
                {
                    ProductCode_PickPos[] pickPos = SysPara.Que_ProductCode_PickPos.ToArray();
                    pickPos[curR1HaveNum] = code_PickPos;
                    SysPara.Que_ProductCode_PickPos = new Queue<ProductCode_PickPos>(pickPos);
                }
            }
            
            int productCount = 3;

            if (GetSettingValue("MSet", "EPlaningCounts"))
            {
                int Planing = GetSettingValue("MSet", "ProQuantity");
                int finished = GetRecipeValue("RSet", "finishedCounts");
                if ((Planing - finished) < 3)
                {
                    productCount = Planing - finished;
                }
            }

            if (SysPara.Que_ProductCode_PickPos.Count == 3 || !isPickProduceOK)
            {
                for (int i = SysPara.Que_ProductCode_PickPos.Count; i < 3; i++)
                {
                    //     ProductCode_PickPos code_PickPos = new ProductCode_PickPos();       //scan success，Record productCode and Index
                    code_PickPos.Code = "ERROR";
                    code_PickPos.PosIndex = i;
                    SysPara.Que_ProductCode_PickPos.Enqueue(code_PickPos);
                }


                if (!scannerReuslt.Contains(true))
                {
                    //MOD@@ Guillermo Carrillo - alarma 1302: dialogo de reintento de lectura QR
                    SysPara.NPShowAlarm("1302");
                    reintento.Dispose();
                    reintento = new Reintento();
                    reintento.fnSetTextMessageNShow("No se leyo el QR. Acomode las PCB y presione Yes para reintentar, o No para continuar.", true, false, true);
                    if (reintento.dResult == DialogResult.Yes)
                    {
                        QRCode.Clear();
                        scannerReuslt.Clear();
                        SysPara.Que_ProductCode_PickPos.Clear();
                        curR1HaveNum = 0;
                        curR1HaveOKNum = 0;
                        return FCResultType.IDLE;
                    }

                    QRCode.Clear();
                    scannerReuslt.Clear();
                    SysPara.Que_ProductCode_PickPos.Clear();
                    return FCResultType.CASE1;
                    
                }
                isPickProduceOK = true;
                //scannerReuslt.Clear();
                return FCResultType.NEXT;
            }
            else
            {

                return FCResultType.CASE1;
            }
        }
        //R1 Goto Product CCD
        private FCResultType flowChart17_FlowRun(object sender, EventArgs e)
        {
            CCDAlogrithmStatus = false;
            OB_RobotLight.On();
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Feeder product CamPosition");
            ProductCode_PickPos code_PickPos = new ProductCode_PickPos();
            //if (SysPara.Que_ProductCode_PickPos.Count > 0)
            //{
            //    code_PickPos = SysPara.Que_ProductCode_PickPos.ToList()[CCDReuslt.Count];
            //}

            //
            //if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", (int)code_PickPos.PosIndex), pos[4], 30000))
            if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", SysPara.ScanCount, pos[4], 30000)))
            {
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, $"Command：{pos[2]} Pos：{SysPara.ScanCount:D3} AppIndex：{pos[4]}", SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT; // 进入flowChart18
            }
            else
            {
                return FCResultType.IDLE; // 重新执行flowChart17
            }
        }
        //Wait R1 on CCDPos
        private FCResultType flowChart18_FlowRun(object sender, EventArgs e)
        {

            //RSet.FeederPickIndex


            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }
            else { return FCResultType.IDLE; }
        }

        public void saviImageToMES(bool isOK, string pickOrDischarge)
        {
            bool isBMPSave = MiddleLayer.GantryF.GetSettingValue("PSet", "ImgSave");
            bool isJPGSave = MiddleLayer.GantryF.GetSettingValue("PSet", "JPGImgSave");
            string imgSaveName = DateTime.Now.ToString("yyyyMMddHHmmss") + pickOrDischarge + (SysPara.Que_Product_pallet_Code.Count + CCDReuslt.Count + 1).ToString() + ".bmp";
            string saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\";// + (isOK ? "OK\\" : "NG\\");

            if (isJPGSave)
            {
                using (Bitmap img = new Bitmap(cogRecDisp_H5_Recipe.CreateContentBitmap(CogDisplayContentBitmapConstants.Display)))
                {
                    if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
                    img.Save(saveImgPath + imgSaveName);
                }
            }
            if (isBMPSave)
            {
                saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\" + "BMP\\"
                    + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "\\" + (isOK ? "OK\\" : "NG\\");

                using (CogImageFileBMP objImg = new CogImageFileBMP())
                {
                    if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
                    objImg.Open(saveImgPath + imgSaveName, CogImageFileModeConstants.Write);
                    objImg.Append(H1_PickupPos.InputImage);
                    objImg.Close();
                }
            }

        }

        //CCD take picture
        private FCResultType flowChart19_FlowRun(object sender, EventArgs e)
        {
            int FeederPickIndex = GetRecipeValue("RSet", "FeederPickIndex");
            bool InvertGrabFlag = false;
            SysPara.visionPos = new VisionPos();
            if (FeederPickIndex <= 21)
            {
                InvertGrabFlag = false;
            }
            else
            {
                InvertGrabFlag = true;

            }

            H1_PickupPos.TB.Inputs["InvertGrabFlag"].Value = InvertGrabFlag;
            H1_PickupPos.TB.Inputs["AxisXPos"].Value = robotPos[0];
            H1_PickupPos.TB.Inputs["AxisYPos"].Value = robotPos[1];
            H1_PickupPos.TB.Inputs["ClawIndex"].Value = CCDReuslt.Count + 1;
            
            CogTransform2DLinear destRobot = new CogTransform2DLinear();
            CogTransform2DLinear destPart = new CogTransform2DLinear();
            CogTransform2DLinear curRobot = new CogTransform2DLinear();
            CogTransform2DLinear curPart = new CogTransform2DLinear();
            double offsetx, offsety, deltaA;
            ProductCode_PickPos newCode;
            try
            {
                if (SysPara.bDryCycle)
                {
                    OB_RobotLight.Off();
                    CCDAlogrithmStatus = true;
                    return FCResultType.NEXT;
                }

                double exposure = MiddleLayer.GantryF.GetSettingValue("MSet", "PickExpTime");
                
                if (H1_PickupPos.TakePicture(exposure))    //CCD take picture
                {
                    if (!MiddleLayer.SystemF.GetSettingValue("PSet", "DisableVision"))
                    {
                        H1_PickupPos.RunTB();
                        RefreshDifferentThreadUI(cogRecDisp_H5_Recipe, () =>
                        {
                            cogRecDisp_H5_Recipe.Image = H1_PickupPos.TB.Outputs["OutputImage"].Value as CogImage8Grey; ;
                            cogRecDisp_H5_Recipe.Record = H1_PickupPos.TB.CreateLastRunRecord().SubRecords[0];
                            cogRecDisp_H5_Recipe.Fit();
                        });

                        if (H1_PickupPos.IsAccept)
                        {
                            


                            string str1 = (string)H1_PickupPos.GetOutput("Model");
                            string str2 = MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");
                            if (!str2.Contains(str1))
                            {
                                SysPara.NPShowAlarm("0010");
                                MiddleLayer.LogF.AddLog(LogType.Production, "PCB model is mistake,please check", SysPara.bEnableGeneralSaveLog);
                                CCDAlogrithmStatus = false;
                                return FCResultType.NEXT;

                            }
                            
                            if (InvertGrabFlag)//wcc 2023.0713       
                            {

                                if (!(bool)H1_PickupPos.GetOutput("CheckPCBIsOKInvert"))
                                {
                                    SDKKernal.ShowAlarm("1212");
                                    reintento.fnSetTextMessageNShow("Check if the PCB is tilted?", true, false, true);
                                    if (reintento.dResult == DialogResult.Yes)
                                    {
                                        return FCResultType.IDLE;
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            else
                            {

                                if (!(bool)H1_PickupPos.GetOutput("CheckPCBIsOK"))
                                {
                                    SDKKernal.ShowAlarm("1212");
                                    reintento.fnSetTextMessageNShow("Check if the PCB is tilted?", true, false, true);
                                    if (reintento.dResult == DialogResult.Yes)
                                    {
                                        return FCResultType.IDLE;
                                    }
                                    else
                                    {

                                    }

                                }
                            }

                            offsetx = (double)H1_PickupPos.GetOutput("X");
                            offsety = (double)H1_PickupPos.GetOutput("Y");
                            deltaA = (double)H1_PickupPos.GetOutput("Angle");

                            SysPara.visionPos.x = offsetx;
                            SysPara.visionPos.y = offsety;
                            SysPara.visionPos.u = deltaA;

                            try
                            {
                                MiddleLayer.LogF.AddLog(LogType.Production, "PosX：" + offsetx.ToString() + "PosY：" + offsety.ToString(), SysPara.bEnableGeneralSaveLog);

                                double max_X = GetRecipeValue("RSet", "FeederMaxValueX");
                                double max_Y = GetRecipeValue("RSet", "FeederMaxValueY");
                                double min_X = GetRecipeValue("RSet", "FeederMinValueX");
                                double min_Y = GetRecipeValue("RSet", "FeederMinValueY");
                                bool bValue = max_X > offsetx && min_X < offsetx &&
                                    max_Y > offsety && min_Y < offsety ? true : false;

                                if (!bValue)
                                {
                                    SDKKernal.ShowAlarm("1206");
                                    return FCResultType.IDLE;
                                }
                            }
                            catch (Exception)
                            {
                                SDKKernal.ShowAlarm("1206");
                                return FCResultType.IDLE;
                            }

                            CCDAlogrithmStatus = true;
                            // 更新扫码结果

//HOLIS AMIGUINI

                            SysPara.QRCode1 = "Null";
                            string PCBBarcode = (string)H1_PickupPos.GetOutput("PCBBarcode");


                            if (PCBBarcode != "Null")//!string.IsNullOrEmpty(PCBBarcode) && PCBBarcode != "Null"
                            {
                                SysPara.QRCode1 = PCBBarcode;
                                SysPara.R1ScanResult = true;
                                //CCDAlogrithmStatus = true;
                            }
                            else
                            {
                                //SysPara.QRCode1 = "ERROR";
                                //SysPara.R1ScanResult = false;
                                return FCResultType.CASE1;
                            }

                            //try
                            //{
                            //    SaveImage image = new SaveImage();
                            //    image.Image = new Bitmap(cogRecDisp_H5_Recipe.CreateContentBitmap(CogDisplayContentBitmapConstants.Display));
                            //    image.Timer = DateTime.Now.ToString("yyyyMMddHHmmss");
                            //    image.PCBQRCode = SysPara.QRCode1;
                            //    image.PalletQRCode = "NA";
                            //    FeederImages.Add(image);
                            //}
                            //catch (Exception)
                            //{


                            //}

                            // 更新队列
                            scannerReuslt.Add(SysPara.R1ScanResult);
                            QRCode.Add(SysPara.QRCode1);
                            //MOD@@ Guillermo Carrillo - guarda de indice: CCDReuslt vs MaterialID_Array
                            if (CCDReuslt.Count < MaterialID_Array.Length)
                            {
                                MaterialID_Array[CCDReuslt.Count] = SysPara.QRCode1;
                            }
                            newCode = new ProductCode_PickPos
                            {
                                Code = SysPara.QRCode1,
                                PosIndex = SysPara.ScanCount
                            };

                            //SysPara.Que_ProductCode_PickPos.Enqueue(newCode);

                            if (isPickProduceOK)
                            {
                                SysPara.Que_ProductCode_PickPos.Enqueue(newCode);
                            }
                            else
                            {
                                if (SysPara.Que_ProductCode_PickPos.Count > 0)
                                {
                                    ProductCode_PickPos[] pickPos = SysPara.Que_ProductCode_PickPos.ToArray();
                                    pickPos[curR1HaveNum] = newCode;
                                    SysPara.Que_ProductCode_PickPos = new Queue<ProductCode_PickPos>(pickPos);
                                }
                            }
                            return FCResultType.NEXT;

                        }
                        else
                        {
                            SysPara.visionPos.x = 0;
                            SysPara.visionPos.y = 0;
                            SysPara.visionPos.u = 0;
                            CCDAlogrithmStatus = false;
                            return FCResultType.NEXT;

                        }
                    }
                    else
                    {
                        SysPara.visionPos.x = 0;
                        SysPara.visionPos.y = 0;
                        SysPara.visionPos.u = 0;
                        CCDAlogrithmStatus = true;
                        return FCResultType.NEXT;
                    }

                }
                else
                {
                    CCDAlogrithmStatus = false;
                    SysPara.NPShowAlarm("1204");        //CCD fail
                    return FCResultType.NEXT;
                }
            }
            catch (Exception ex)
            {
                CCDAlogrithmStatus = false;
                SysPara.NPShowAlarm("1200");        //CCD fail
                return FCResultType.IDLE;
            }
        }
        //Judge Vision success
        private FCResultType flowChart20_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.bDryCycle)
            {
                CCDAlogrithmStatus = true;
            }

            //scannerReuslt.Add(false);

            //MOD@@ Guillermo Carrillo - guarda de indice: scannerReuslt
            if (CCDReuslt.Count < scannerReuslt.Count && scannerReuslt[CCDReuslt.Count])
            {

                if (!CCDAlogrithmStatus)
                {
                    //MOD@@ Guillermo Carrillo - guarda de indice: CCDReuslt vs MaterialID_Array
                    if (CCDReuslt.Count < MaterialID_Array.Length)
                    {
                        MaterialID_Array[CCDReuslt.Count] = "";
                    }
                }
                CCDReuslt.Add(CCDAlogrithmStatus);


            }
            else
            {
                //MOD@@ Guillermo Carrillo - guarda de indice: CCDReuslt vs MaterialID_Array
                if (CCDReuslt.Count < MaterialID_Array.Length)
                {
                    MaterialID_Array[CCDReuslt.Count] = "";
                }
                CCDReuslt.Add(false);
            }
            queCCDPosition.Enqueue(SysPara.visionPos);


            int productCount = 3;

            if (GetSettingValue("MSet", "EPlaningCounts"))
            {
                int Planing = GetSettingValue("MSet", "ProQuantity");
                int finished = GetRecipeValue("RSet", "finishedCounts");
                if ((Planing - finished) < 3)
                {
                    productCount = Planing - finished;
                }
            }


            if (CCDReuslt.Count >= 3)
            {

                OB_RobotLight.Off();
                OB_FeederSafetySignal.Off();
                curR1HaveOKNum = 0;
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.CASE1;
            }
        }
        //R1 Goto Grab Product
        private FCResultType flowChart21_FlowRun(object sender, EventArgs e)
        {


            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Feeder Product PickPosition");


            //Log.log.Write("Command：" + pos[2] + "Pos：" + string.Format("{0:d3}", (int)SysPara.Que_ProductCode_PickPos.ToList()[curR1HaveNum].PosIndex) + "AppIndex：" + string.Format("{0:d3}",
            //   curR1HaveNum + 1)+"X:"+ VisX + "Y:" + VisY+ "U:" + VisU, Color.Black);
            if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", (int)SysPara.Que_ProductCode_PickPos.ToList()[curR1HaveNum].PosIndex), string.Format("{0:d2}",
                curR1HaveNum + 1), 40000, VisX, VisY, VisU))
            {
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2] + "Pos：" + string.Format("{0:d3}", (int)SysPara.Que_ProductCode_PickPos.ToList()[curR1HaveNum].PosIndex) + "AppIndex：" + string.Format("{0:d3}",
               curR1HaveNum + 1) + "X:" + VisX + "Y:" + VisY + "U:" + VisU, SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        //Wait R1 Grab success
        private FCResultType flowChart22_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {

                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                if (Robot.ErorrCode == "E061")
                {
                    CCDReuslt[0] = false;
                    return FCResultType.NEXT;
                }
                else if (Robot.ErorrCode == "E062")
                {
                    CCDReuslt[1] = false;
                    return FCResultType.NEXT;
                }
                else if (Robot.ErorrCode == "E063")
                {
                    CCDReuslt[2] = false;
                    return FCResultType.NEXT;
                }
                else if (Robot.ErorrCode == "E1601") // 2023.2.6, Ren, drop pcb and do not go to NG
                {
                    CCDReuslt[0] = false;
                    MaterialID_Array[0] = "";
                    SysPara.NPShowAlarm("1601");
                    return FCResultType.IDLE;
                }
                else if (Robot.ErorrCode == "E1603") // 2023.2.6, Ren, drop pcb and do not go to NG
                {
                    CCDReuslt[1] = false;
                    MaterialID_Array[1] = "";
                    SysPara.NPShowAlarm("1603");
                    return FCResultType.IDLE;
                }
                else if (Robot.ErorrCode == "E1605") // 2023.2.6, Ren, drop pcb and do not go to NG
                {
                    CCDReuslt[2] = false;
                    MaterialID_Array[2] = "";
                    SysPara.NPShowAlarm("1605");
                    return FCResultType.IDLE;
                }
                else
                {
                    return FCResultType.IDLE;
                }

            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout
            else { return FCResultType.IDLE; }
        }

        //Judge Grab count=3
        private FCResultType flowChart23_FlowRun(object sender, EventArgs e)
        {
            curR1HaveNum++;                                                                                 //Success，Count+1
            if (SysPara.Que_ProductCode_PickPos.Count > 0)
            {
                ProductCode_PickPos[] pickPos = SysPara.Que_ProductCode_PickPos.ToArray();
                if (curR1HaveNum > pickPos.Count())
                {
                    curR1HaveNum = pickPos.Count();
                }

                //MOD@@ Guillermo Carrillo - guarda de indice: pickPos[curR1HaveNum-1] (causa del indice -1)
                if (curR1HaveNum >= 1
                    && curR1HaveNum <= pickPos.Count()
                    && curR1HaveNum <= CCDReuslt.Count)
                {
                    pickPos[curR1HaveNum - 1].CCDResult = CCDReuslt[curR1HaveNum - 1];
                }

                //if (!pickPos[curR1HaveNum - 1].CCDResult)
                //{ MaterialID_Array[curR1HaveNum - 1] = ""; }//huan

                SysPara.Que_ProductCode_PickPos = new Queue<ProductCode_PickPos>(pickPos);
            }
            int productCount = 3;
            if (GetSettingValue("MSet", "EPlaningCounts"))
            {
                int Planing = GetSettingValue("MSet", "ProQuantity");
                int finished = GetRecipeValue("RSet", "finishedCounts");
                if ((Planing - finished) < 3)
                {
                    productCount = Planing - finished;
                }
            }
            bool ret = (curR1HaveNum == 2) && (CCDReuslt.Contains(false));

            if (curR1HaveOKNum == productCount || curR1HaveNum == 3 || ret)
            {
                OB_FeederSafetySignal.On();
                if (SysPara.ScanCount >= SysPara.FeederProductCount)
                {
                    SysPara.JAGStarytEvent.Set();//feeder Grab product compelet
                    SysPara.R1GrabCompeletStatus = true;
                    SysPara.ScanCount = 0;
                }
                if (!CCDReuslt.Contains(true) && productCount == 3 && (Int16)GetRecipeValue("RSet", "PaaletProductCount") == 12)
                {
                    if (MesTestList.GetRange(SysPara.Que_Product_pallet_Code.Count, 3).Contains(true))
                    {
                        curR1HaveOKNum = 0;
                        curR1HaveNum = 0;
                        CCDReuslt.Clear();
                        scannerReuslt.Clear();
                        QRCode.Clear();
                        SysPara.Que_ProductCode_PickPos.Clear();
                        SysPara.NPShowAlarm("1302");
                        return FCResultType.CASE2;
                    }
                    else
                    {
                        CCDReuslt.Clear();
                        curR1HaveOKNum = 0;
                        curR1HaveNum = 0;
                        return FCResultType.NEXT;
                    }

                }
                CCDReuslt.Clear();
                curR1HaveNum = 0;
                return FCResultType.NEXT;
            }
            else { return FCResultType.CASE1; }
        }

        //Wait R1 can assemb Signal
        private FCResultType flowChart24_FlowRun(object sender, EventArgs e)
        {
            lock (SysPara.StatusLock)
            {
                if (MiddleLayer.ConveyorF.UpConveyorAutoFlow.NotifyAssembly)  //SysPara.R1AssembStatus)
                {
                    SysPara.DischargeCount = 0;
                    //MiddleLayer.ConveyorF.UpConveyorAutoFlow.NotifyAssembly = false;
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }
        //Judge need to scan
        private FCResultType flowChart28_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Que_pallet_Code.Count > 0)
            {
                return FCResultType.CASE1;
            }
            else
            {

                foreach (var q in SysPara.Que_ProductCode_PickPos)
                {
                    string position = "P1";
                    MESLib.MesLog.AddInfo(position + " " +
                        q.PosIndex + " " + q.Code + " " + q.CCDResult);
                }


                return FCResultType.NEXT;
            }
        }
        //R1 Goto Convery Scan
        private FCResultType flowChart29_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Convery Pallet ScanPosition");
            if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", (scanCount + 1), pos[4], 30000)))
            {
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2] + "Pos：" + string.Format("{0:d3}", (scanCount + 1) + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog));
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }
        //Wait R1 on scan pos
        private FCResultType flowChart30_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                RunTM.Restart();
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {

                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout
            else { return FCResultType.IDLE; }
        }
        //TriggerScan
        private FCResultType flowChart31_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ProcessF.GetSettingValue("PSet", "DisableCheckQRcodes") || SysPara.bDryCycle)
            {
                for(int i = 0;i<12;i++)
                {
                    SysPara.Que_pallet_Code.Enqueue("LG1T-AA-" + (i + 1).ToString("0#"));
                }
                return FCResultType.NEXT;
            }
            if (TrigerBarcode1InFlow(RunTM))
            {
                string[] code = SysPara.QRCode1.Split(',');
                if (code.Length >= 3)
                {
                    for (int i = 0; i < code.Length; i++)
                    {

                        for (int j = i + 1; j < code.Length; j++)
                        {

                            if (code[i] == code[j])
                            {
                                SysPara.NPShowAlarm("1301");
                                return FCResultType.IDLE;
                            }

                        }
                    }
                    if (code[0].Contains("ERROR") || code[1].Contains("ERROR") || code[2].Contains("ERROR"))
                    {
                        SysPara.NPShowAlarm("1301");
                        return FCResultType.IDLE;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            string qr = code[i].Substring(0, 4);
                            if (qr != MiddleLayer.GantryF.GetRecipeValue("RSet", "TrayQRcode"))
                            {
                                SysPara.NPShowAlarm("1306", "QRcode Error:" + qr);
                                return FCResultType.IDLE;
                            }

                        }

                        // get the header of the pallet qrcode.
                        string header = "";
                        try
                        {
                            header = code[0].Substring(0, 7);
                            if ((!code[1].StartsWith(header)) || (!code[2].StartsWith(header)))
                            {
                                SysPara.NPShowAlarm("1306", $"QRcode Not Match: {code[0]}, {code[1]}, {code[2]}");
                                MESLib.MesLog.AddInfo($"QRcode Not Match: {code[0]}, {code[1]}, {code[2]}");
                                return FCResultType.IDLE;
                            }
                        }
                        catch(Exception ex)
                        {
                            SysPara.NPShowAlarm("1306", $"QRcode Error: {ex.Message}");
                            MESLib.MesLog.AddInfo($"QRcode Error: {ex.Message}");
                            return FCResultType.IDLE;
                        }

                        MESLib.MesLog.AddInfo($"QRcodes: {code[0]}, {code[1]}, {code[2]}");
                        for (int i = 0; i < 12; i++)
                        {
                            string createdqrcode = $"{header}-{(i + 1).ToString("0#")}";
                            SysPara.Que_pallet_Code.Enqueue(createdqrcode);
                            MESLib.MesLog.AddInfo($"Created QRcodes: {createdqrcode}");

                            MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[i] = createdqrcode;
                        }

                        return FCResultType.NEXT;
                    }

                }
                else
                {

                    SysPara.NPShowAlarm("1301");
                    return FCResultType.IDLE;

                }
            }
            else
            {

                SysPara.NPShowAlarm("1301");
                return FCResultType.IDLE;

            }
            //return FCResultType.NEXT;
        }
        private readonly object _palletQueueLock = new object();
        ////Judge Tray code is valuable
        private FCResultType flowChart32_FlowRun(object sender, EventArgs e)
        {
            lock (_palletQueueLock)
            {
                if (SysPara.Que_pallet_Code.Count > 0)
                {
                    if (SysPara.Que_ProductCode_PickPos.ToArray()[0].CCDResult)
                    {
                        if (SysPara.bDryCycle)
                        {

                            return FCResultType.NEXT;
                        }
                        //MOD@@ Guillermo Carrillo - guarda de indice: MesTestList
                        if (SysPara.Que_Product_pallet_Code.Count < MesTestList.Count && !MesTestList[SysPara.Que_Product_pallet_Code.Count]/* && SysPara.Que_Product_pallet_Code.Count < 3*/)
                        {
                            //AssmbleCCDAlogStatus = false;
                            //      queCCDPosition.Dequeue();
                            curPalletCode = SysPara.Que_pallet_Code.Dequeue();//LFF-0305
                            Product_Pallet_Code ProductPalletCode_temp = new Product_Pallet_Code();
                            ProductPalletCode_temp.data = "";
                            ProductPalletCode_temp.PalletCode = curPalletCode;
                            ProductPalletCode_temp.ProdcutCode = "";
                            ProductPalletCode_temp.PressureResult = false;

                            SysPara.Que_Product_pallet_Code.Enqueue(ProductPalletCode_temp);
                            SysPara.DischargeFlag = true;
                            return FCResultType.IDLE;
                        }
                        else
                        {

                            //MOD@@ Guillermo Carrillo - guarda de indice: MESReportResult[gripperIndex-1]
                            if (gripperIndex >= 1 && gripperIndex <= SysPara.MESReportResult.Length && !SysPara.MESReportResult[gripperIndex - 1])
                            {
                                bMESPCBIsNG = true;
                                return FCResultType.CASE2;
                            }
                            else
                                bMESPCBIsNG = false;




                            curPalletCode = SysPara.Que_pallet_Code.Dequeue();//LFF-0305
                            SysPara.DischargeFlag = true;
                            if (curPalletCode != "ERROR")
                            {
                                //curPalletCode = SysPara.Que_pallet_Code.Dequeue();

                                return FCResultType.NEXT;
                            }
                            else
                            {
                                //AssmbleCCDAlogStatus = false;

                                return FCResultType.CASE3;
                            }
                        }

                    }
                    else
                    {
                        //AssmbleCCDAlogStatus = false;
                        SysPara.DischargeFlag = false;
                        SysPara.PressureDirver = 0;
                        SysPara.PressureData = 0;
                        SysPara.OverPressureab = true;
                        queCCDPosition.Dequeue();
                        return FCResultType.CASE1;
                    }
                    //if code is valuable,call R1Goto take picture

                }
                else
                {
                    SysPara.DischargeFlag = false;
                    SysPara.PressureDirver = 0;
                    SysPara.PressureData = 0;
                    SysPara.OverPressureab = true;
                    //queCCDPosition.Dequeue();
                    return FCResultType.CASE1;

                }
            }
            
        }
        //R1 Goto tray CDD
        private FCResultType flowChart33_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Convery Product CamPosition");
            if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", (int)(SysPara.Que_Product_pallet_Code.Count + CCDReuslt.Count + 1)), pos[4], 30000))
            {
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2]
               + "Pos：" + string.Format("{0:d3}", SysPara.Que_Product_pallet_Code.Count + CCDReuslt.Count + 1)
               + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }
        //Wait R1 on tray CCDPos
        private FCResultType flowChart34_FlowRun(object sender, EventArgs e)
        {

            //LFFF
            //    int ret = Robot.ReadFromEpson();
            //    if (ret == 1)
            //    {
            //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
            //    OB_RobotLight.On();
            //return FCResultType.NEXT;
            //}
            //else if (ret == 0)
            //{


            //    //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
            //    return FCResultType.IDLE;
            //}
            //    else if (RunTM.On(SysPara.TimeOutSec)) { SysPara.NPShowAlarm("15008"); return FCResultType.IDLE; }        //R1 timeout
            //     else { return FCResultType.IDLE; }

            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                OB_RobotLight.On(); return FCResultType.NEXT;
            }
            else if (ret == 0)
            {


                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {


                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;

            }        //R1 timeout
            else { return FCResultType.IDLE; }







        }
        //CCD take picture
        private FCResultType flowChart35_FlowRun(object sender, EventArgs e)
        {
            assembVisOffsetX = 0;
            assembVisOffsetY = 0;
            //bool isSave = MiddleLayer.GantryF.GetSettingValue("PSet", "ImgSave");
            //bool isJPGSave = MiddleLayer.GantryF.GetSettingValue("PSet", "JPGImgSave");
            //string imgSaveName = DateTime.Now.ToString("HH_mm_ss") + "_Discharge_" + (SysPara.Que_Product_pallet_Code.Count + CCDReuslt.Count + 1).ToString() + ".bmp";
            if (SysPara.bDryCycle)
            {
                //OB_RobotLight.Off();
                AssmbleCCDAlogStatus = true;
                return FCResultType.NEXT;
            }

            AssmbleCCDAlogStatus = true;

            double exposure = MiddleLayer.GantryF.GetSettingValue("MSet", "DisChargeExpTime");



            if (H1_DischargePos.TakePicture(exposure))    //CCD take picture
            {

                if (!MiddleLayer.SystemF.GetSettingValue("PSet", "DisableVision"))
                {
                    //if (isSave)
                    //{
                    //    string saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\" + "BMP\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                    //    using (CogImageFileBMP objImg = new CogImageFileBMP())
                    //    {
                    //        if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
                    //        objImg.Open(saveImgPath + imgSaveName, CogImageFileModeConstants.Write);
                    //        objImg.Append(H1_DischargePos.InputImage);
                    //        objImg.Close();
                    //    }
                    //}
                    H1_DischargePos.RunTB();
                    RefreshDifferentThreadUI(cogRecDisp_H5_Recipe, () =>
                    {
                        cogRecDisp_H5_Recipe.Image = H1_DischargePos.InputImage;
                        cogRecDisp_H5_Recipe.Record = H1_DischargePos.TB.CreateLastRunRecord().SubRecords[0];
                        cogRecDisp_H5_Recipe.Fit();
                    });
                    //if (isJPGSave)
                    //{
                    //    string saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\" + "JPG\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                    //    using (Bitmap img = new Bitmap(cogRecDisp_H5_Recipe.CreateContentBitmap(CogDisplayContentBitmapConstants.Display)))
                    //    {
                    //        if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
                    //        img.Save(saveImgPath + imgSaveName);
                    //    }
                    //}
                    SaveImage image = new SaveImage();
                    image.Image = new Bitmap(cogRecDisp_H5_Recipe.CreateContentBitmap(CogDisplayContentBitmapConstants.Display));
                    image.Timer = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //MOD@@ Guillermo Carrillo - alarma 1211: bloque try del reintento de camara
                    try
                    {
                        image.PalletQRCode = MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[CCDReuslt.Count * 2];
                    }
                    catch { image.PalletQRCode = "NA"; }
                    image.PCBQRCode = "NA";
                    ProductImages.Add(image);
                    bool coverIsTilt = false;

                    if (H1_DischargePos.IsAccept)
                    {
                        coverIsTilt = (bool)H1_DischargePos.GetOutput("CoverIstilt");

                        if (coverIsTilt)
                        {
                            //MOD@@ Guillermo Carrillo - alarma 1211: confirmacion por votacion
                            //  Toma TILT_TOTAL lecturas y alarma solo si al menos
                            //  TILT_UMBRAL coinciden. Para ajustar la sensibilidad
                            //  basta cambiar estos dos numeros:
                            //    mas tolerante -> subir TILT_UMBRAL
                            //    mas estricto  -> bajar TILT_UMBRAL
                            //  Revisar el log "CoverIstilt votos": si sale seguido
                            //  el maximo (5/5), el problema no es ruido de captura
                            //  sino el umbral de VisionPro o tapas realmente ladeadas,
                            //  y agregar lecturas no va a ayudar.
                            const int TILT_TOTAL = 5;
                            const int TILT_UMBRAL = 3;

                            int tiltVotes = 1;
                            for (int t = 0; t < TILT_TOTAL - 1; t++)
                            {
                                Thread.Sleep(150);
                                if (!H1_DischargePos.TakePicture(exposure)) { continue; }
                                if (!H1_DischargePos.IsAccept) { continue; }
                                if ((bool)H1_DischargePos.GetOutput("CoverIstilt")) { tiltVotes++; }
                            }

                            MiddleLayer.LogF.AddLog(LogType.Production,
                                "CoverIstilt votos: " + tiltVotes + "/" + TILT_TOTAL,
                                SysPara.bEnableGeneralSaveLog);

                            if (tiltVotes >= TILT_UMBRAL)
                            {
                                //SysPara.NPShowAlarm("1211");        //Cover is tilt
                                SDKKernal.ShowAlarm("1211");
                                return FCResultType.IDLE;
                            }
                            // ---- fin reintento 1211 ----
                        }
                        palletUpResult.Add((bool)H1_DischargePos.GetOutput("LeftUpResult"));
                        palletUpResult.Add((bool)H1_DischargePos.GetOutput("RightUpResult"));

                        //     palletDownResult.Add((bool)H1_DischargePos.GetOutput("LeftDownResult"));
                        //     palletDownResult.Add((bool)H1_DischargePos.GetOutput("RightDownResult"));
                        if (CCDReuslt.Count == 2)
                        {
                            assembVisOffsetX = (double)H1_DischargePos.GetOutput("OffsetX");
                            assembVisOffsetY = (double)H1_DischargePos.GetOutput("OffsetY");
                        }
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        palletUpResult.Add(false);
                        palletUpResult.Add(false);
                        AssmbleCCDAlogStatus = false;
                        //OB_RobotLight.Off();
                        saviImageToMES(AssmbleCCDAlogStatus, SysPara.strDischarge);
                        SysPara.NPShowAlarm("1205");        //CCD Alogrithm fail
                        return FCResultType.NEXT;
                    }
                }
                else
                {
                    palletUpResult.Add(false);
                    palletUpResult.Add(false);
                    //CCDAlogrithmStatus = true;
                    return FCResultType.NEXT;
                }

            }
            else
            {
                palletUpResult.Add(false);
                palletUpResult.Add(false);
                //OB_RobotLight.Off();
                AssmbleCCDAlogStatus = false;
                saviImageToMES(AssmbleCCDAlogStatus, SysPara.strDischarge);
                SysPara.NPShowAlarm("1204");        //CCD fail
                return FCResultType.NEXT;
            }

        }
        //Judge CCD Count
        private FCResultType flowChart36_FlowRun(object sender, EventArgs e)
        {
            SysPara.EachPalletActualCount = 0; ;//LFF-0305



            CCDReuslt.Add(AssmbleCCDAlogStatus);

            int CCDProductCount = 6;
            if (GetSettingValue("MSet", "EPlaningCounts"))
            {
                int Planing = GetSettingValue("MSet", "ProQuantity");
                int finished = GetRecipeValue("RSet", "finishedCounts");
                if ((Planing - finished) < 12)
                {
                    if ((Planing - finished) % 2 != 0)
                        CCDProductCount = (Planing - finished) / 2 + 1;
                    else
                        CCDProductCount = (Planing - finished) / 2;
                }

            }

            if (CCDReuslt.Count >= 6)//LFF-0126
            {
                for (int i = CCDReuslt.Count; i < 6; i++)
                {
                    CCDReuslt.Add(false);
                }


                OB_RobotLight.Off();
                palletUpResult.AddRange(palletDownResult);
                if (!SysPara.bDryCycle)
                {
                    MesTestList = palletUpResult;
                }

                foreach (bool temp in MesTestList)
                {
                    if (temp)
                    {
                        SysPara.EachPalletActualCount++;//LFF-0305

                    }
                }


                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.CASE1;
            }
        }
        //R1 Goto tray assemb
        private FCResultType flowChart37_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Convery Pallet AssembPosition");
            //try
            //{
            //    int num = InvertOrder(SysPara.Que_ProductCode_PickPos.Count);
            //    string saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\";
            //    string ImgSaveName = string.Format(@"{0}_{1}_NA_NA_{2}_{3}.bmp",
            //           FeederImages[num].PCBQRCode,
            //           MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[SysPara.Que_Product_pallet_Code.Count],
            //      FeederImages[num].Timer,
            //       MiddleLayer.MesF2.GetSettingValue("PSet", "EQPID")
            //       );

            //    PCBCode[SysPara.Que_Product_pallet_Code.Count] = FeederImages[num].PCBQRCode;
            //    using (Bitmap img = new Bitmap(FeederImages[num].Image))
            //    {
            //        if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
            //        img.Save(saveImgPath + ImgSaveName);
            //    }

            //}
            //catch (Exception)
            //{


            //}



            if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", SysPara.Que_Product_pallet_Code.Count + 1),
                string.Format("{0:d2}", InvertOrder(SysPara.Que_ProductCode_PickPos.Count)), 30000, VisX, VisY, VisU))
            {
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2]
                + "Pos：" + string.Format("{0:d3}", SysPara.Que_Product_pallet_Code.Count + 1)
                + "AppIndex：" + InvertOrder(SysPara.Que_ProductCode_PickPos.Count) + "X:" + VisX + "Y:" + VisY + "U:" + VisU, SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }
        //pressureTest
        private FCResultType flowChart38_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotationforTech(dgv_DischargeTechPos, "DisChargePos" + (SysPara.Que_Product_pallet_Code.Count + 1).ToString());

            double offsetx, offsety, deltaA;
            SysPara.visionPos = new VisionPos();

            if (AssmbleCCDAlogStatus)
            {
                //offsetx = Convert.ToDouble(pos[1]) + assembVisOffsetX;
                //offsety = Convert.ToDouble(pos[2]) + assembVisOffsetY;
                offsetx = assembVisOffsetX;
                offsety = assembVisOffsetY;
                SysPara.visionPos.x = offsetx;
                SysPara.visionPos.y = offsety;
                SysPara.visionPos.u = 0;
            }
            else
            {
                SysPara.visionPos.x = 0; //Convert.ToDouble(pos[1]);
                SysPara.visionPos.y = 0; //Convert.ToDouble(pos[2]);
                SysPara.visionPos.u = 0;//Convert.ToDouble(pos[3]);
                queCCDPosition.Enqueue(SysPara.visionPos);
                return FCResultType.NEXT;
            }
            queCCDPosition.Enqueue(SysPara.visionPos);
            return FCResultType.NEXT;
        }
        //Wait R1 assemb compelet
        private FCResultType flowChart39_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                //try
                //{
                //    int num = InvertOrder(SysPara.Que_ProductCode_PickPos.Count);
                //    string saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\";
                //    string ImgSaveName = string.Format(@"{0}_{1}_NA_NA_{2}_{3}.bmp",
                //           FeederImages[num].PCBQRCode,
                //           MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[SysPara.Que_Product_pallet_Code.Count],
                //      FeederImages[num].Timer,
                //       MiddleLayer.MesF2.GetSettingValue("PSet", "EQPID")
                //       );

                //    PCBCode[SysPara.Que_Product_pallet_Code.Count] = FeederImages[num].PCBQRCode;
                //    using (Bitmap img = new Bitmap(FeederImages[num].Image))
                //    {
                //        if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
                //        img.Save(saveImgPath + ImgSaveName);
                //    }

                //}
                //catch (Exception)
                //{

                //}





                RunTM.Restart();
                SysPara.OverPressureab = false;
                SysPara.OverPressurebc = false;
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);

                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                if (GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3] == "")
                {
                    SysPara.RobotPressureCompelet = true;
                    //SysPara.OverPressure = true;
                    return FCResultType.CASE1;
                }
                else { return FCResultType.IDLE; }
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout
            else { return FCResultType.IDLE; }
        }
        //Wait PressureTest compelet
        private FCResultType flowChart40_FlowRun(object sender, EventArgs e)
        {
            if (RunTM.On(SysPara.TimeOutSec))
            {

                //SysPara.NPShowAlarm("8103");
                return FCResultType.IDLE;
            }
            if (SysPara.PressureCompelet)
            {
                SysPara.PressureCompelet = false;
                SysPara.RobotPressureCompelet = false;



                if (SysPara.OverPressureab || SysPara.OverPressurebc)
                {
                    processData.fnAddCountUnit(false);
                    return FCResultType.CASE1;
                }
                else
                {
                    processData.fnAddCountUnit(true);
                    return FCResultType.NEXT;
                }
            }
            else { return FCResultType.IDLE; }
        }
        //Judge assemb count=12
        private FCResultType flowChart41_FlowRun(object sender, EventArgs e)
        {

            SysPara.WaitNextPallet = false;
            if (SysPara.Que_ProductCode_PickPos.Count > 0)
            {
                SysPara.ProductPalletCode = new Product_Pallet_Code();
                SysPara.ProductPalletCode.ProdcutCode = SysPara.Que_ProductCode_PickPos.Dequeue().Code;        //this tray assemb success,Enqueue this record
                SysPara.ProductPalletCode.PalletCode = curPalletCode;



                //MOD@@ Guillermo Carrillo - guarda de indice: Que_Product_pallet_Code
                if (SysPara.Que_Product_pallet_Code.Count < (Int16)GetRecipeValue("RSet", "PaaletProductCount") && SysPara.Que_Product_pallet_Code.Count < MesTestList.Count && MesTestList[SysPara.Que_Product_pallet_Code.Count])
                {
                    SysPara.ProductPalletCode.data = SysPara.PressureData.ToString();
                    SysPara.ProductPalletCode.PressureResult = AssmbleCCDAlogStatus && !SysPara.OverPressureab && !SysPara.OverPressurebc;

                    SysPara.ResultData.Time = DateTime.Now.ToString("MM-dd-hh-mm-ss");
                    SysPara.ResultData.ProductCode = SysPara.ProductPalletCode.ProdcutCode;
                    SysPara.ResultData.PalletCode = curPalletCode;
                    SysPara.ResultData.PreasureDirver = SysPara.PressureDirver;
                    SysPara.ResultData.Preasure = SysPara.PressureData;
                    SysPara.ResultData.PreasureMax = GetRecipeValue("RSet", "PressMax");
                    SysPara.ResultData.abResult = AssmbleCCDAlogStatus && !SysPara.OverPressureab ? "OK" : "NG"; ;
                    SysPara.ResultData.bcResult = AssmbleCCDAlogStatus && !SysPara.OverPressurebc && !SysPara.OverPressureab ? "OK" : "NG";

                    if (SysPara.DischargeFlag)
                    {

                        // Mes Check
                        if (SysPara.EnableMes)
                        {

                            try
                            {
                                // record result
                                int index = MESLib.CommParas.MesManager.GetIndex(SysPara.ResultData.PalletCode,
                                    MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs);

                                if (SysPara.ResultData.PalletCode != "")
                                {
                                    var pcbinfo = CurrentPCBList[index];

                                    pcbinfo.Result_A = SysPara.ResultData.abResult;
                                    pcbinfo.Result_B = SysPara.ResultData.bcResult;
                                    if (pcbinfo.Result_A == "NG" || pcbinfo.Result_B == "NG")
                                    {
                                        ;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show("Check NG Fail: " + ex.Message);
                            }
                        }


                        //if (SysPara.ResultData.abResult == "NG" || SysPara.ResultData.bcResult == "NG")
                        //{
                        //    SDKKernal.ShowAlarm("1638");
                        //}
                        //processData.fnUpdateResult(SysPara.ResultData);
                        processData.fnUpdateResult(SysPara.ResultData, CurrentRowsIndex);
                    }
                    if (GetSettingValue("MSet", "EPlaningCounts"))
                    {
                        DataTable dtt = RecipeData.Tables["RSet"];
                        dtt.Rows[0]["finishedCounts"] = GetRecipeValue("RSet", "finishedCounts") + 1;
                        dtt.AcceptChanges();
                        this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

                    }


                    gripperIndex++;
                    CurrentRowsIndex++;
                    //processData.fnAddCountUnit(AssmbleCCDAlogStatus && !SysPara.OverPressureab && !SysPara.OverPressurebc);
                }
                else
                {
                    SysPara.ProductPalletCode.data = "0";
                    SysPara.ProductPalletCode.PressureResult = false;

                    SysPara.ResultData.Time = DateTime.Now.ToString("MM-dd-hh-mm-ss");
                    SysPara.ResultData.ProductCode = SysPara.ProductPalletCode.ProdcutCode;
                    SysPara.ResultData.PalletCode = curPalletCode;
                    SysPara.ResultData.PreasureDirver = SysPara.PressureDirver;
                    SysPara.ResultData.Preasure = 0;
                    SysPara.ResultData.PreasureMax = GetRecipeValue("RSet", "PressMax");
                    SysPara.ResultData.abResult = "N/A";
                    SysPara.ResultData.bcResult = "N/A";
                    //processData.fnUpdateResult(SysPara.ResultData);
                    processData.fnUpdateResult(SysPara.ResultData, CurrentRowsIndex);
                    CurrentRowsIndex++;
                    gripperIndex++;
                }
                // add one row at the up part, 2023.1.27, zenan
                int maxrows = Convert.ToInt32(MiddleLayer.ProcessF.GetSettingValue("PSet", "MaxDisplay"));
                if (maxrows <= 0)
                {
                    maxrows = 12;
                }
                if (processData.fnRowsCount() > maxrows)
                {
                    processData.fnRemoveLastResult();
                }

                if (SysPara.DischargeFlag)
                {
                    if (!bMESPCBIsNG)
                    {
                        SysPara.Que_Product_pallet_Code.Enqueue(SysPara.ProductPalletCode);
                        SysPara.DischargeCount++;
                    }

                }

                int ProductCount = 12;
                if (GetSettingValue("MSet", "EPlaningCounts"))
                {
                    int Planing = GetSettingValue("MSet", "ProQuantity");
                    int finished = GetRecipeValue("RSet", "finishedCounts");
                    if ((Planing - finished) < 12)
                    {
                        ProductCount = Planing - finished + SysPara.Que_Product_pallet_Code.Count;
                    }

                }
                //SysPara.Que_Product_pallet_Code.Count
                if (SysPara.DischargeCount == SysPara.EachPalletActualCount || SysPara.Que_Product_pallet_Code.Count == 12)          //pallet is full,notice transfer out 
                {
                    MiddleLayer.ConveyorF.UpConveyorAutoFlow.NotifyAssembly = false;
                    SysPara.Que_Product_pallet_Code.Clear();
                    if (SysPara.Que_ProductCode_PickPos.Count == 0)
                    {
                        string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Convery Pallet AssembPosition");
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {

                                for (int i = 0; i < ProductImages.Count; i++)
                                {
                                    string saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\";
                                    string ImgSaveName = string.Format(@"{0}_{1}_NA_NA_{2}_{3}.bmp",
                                           PCBCode[i * 2],
                                           ProductImages[i].PalletQRCode,
                                      ProductImages[i].Timer,
                                       MiddleLayer.MesF2.GetSettingValue("PSet", "EQPID")
                                       );
                                    using (Bitmap img = new Bitmap(ProductImages[i].Image))
                                    {
                                        if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
                                        img.Save(saveImgPath + ImgSaveName);
                                    }
                                }
                            }
                            catch (Exception) { }
                        });
                        queCCDPosition.Clear();
                        MesTestList = new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true };
                        palletUpResult.Clear();
                        palletDownResult.Clear();
                        SysPara.Que_pallet_Code.Clear();
                        AssmbleCCDAlogStatus = true;
                        CCDReuslt.Clear();
                        scannerReuslt.Clear();
                        gripperIndex = 1;
                        RunTM.Restart();
                        lock (SysPara.StatusLock)
                        {
                            MiddleLayer.ConveyorF.UpConveyorAutoFlow.AssembCompeletStatus = true;
                            SysPara.TempAssembCompeletStatus = true;
                        }
                        if (GetRecipeValue("RSet", "FeederPickIndex") % 3 != 0)
                        {
                            int FeederPickIndex = GetRecipeValue("RSet", "FeederPickIndex") + (3 - GetRecipeValue("RSet", "FeederPickIndex") % 3);
                            if (FeederPickIndex >= 42)
                            {
                                feederT.Restart();
                                SysPara.JAGStarytEvent.Set();
                                FeederPickIndex = 0;
                            }
                            DataTable dt = RecipeData.Tables["RSet"];
                            dt.Rows[0]["FeederPickIndex"] = FeederPickIndex;
                            dt.AcceptChanges();
                            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

                        }
                        LastProduct = true;
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        if (SysPara.Que_ProductCode_PickPos.Count > 0)
                        {
                            for (int i = 0; i < SysPara.Que_ProductCode_PickPos.Count; i++)
                            {
                                if (!SysPara.Que_ProductCode_PickPos.ToArray()[i].CCDResult)
                                {
                                    SysPara.Que_ProductCode_PickPos.Dequeue();
                                    gripperIndex++;
                                    i = -1;
                                }
                                else
                                {
                                    i += 5;
                                }
                            }
                        }
                        SysPara.WaitNextPallet = true;
                        if (SysPara.Que_ProductCode_PickPos.Count == 0)
                        {
                            SysPara.WaitNextPallet = false;
                            gripperIndex = 1;
                        }
                        queCCDPosition.Clear();
                        MesTestList = new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true };
                        palletUpResult.Clear();
                        palletDownResult.Clear();
                        SysPara.Que_pallet_Code.Clear();
                        AssmbleCCDAlogStatus = true;
                        CCDReuslt.Clear();
                        scannerReuslt.Clear();
                        RunTM.Restart();
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                for (int i = 0; i < ProductImages.Count; i++)
                                {
                                    string saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\";
                                    string ImgSaveName = string.Format(@"{0}_{1}_NA_NA_{2}_{3}.bmp",
                                           PCBCode[i * 2],
                                           ProductImages[i].PalletQRCode,
                                      ProductImages[i].Timer,
                                       MiddleLayer.MesF2.GetSettingValue("PSet", "EQPID")
                                       );
                                    using (Bitmap img = new Bitmap(ProductImages[i].Image))
                                    {
                                        if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
                                        img.Save(saveImgPath + ImgSaveName);
                                    }
                                }
                            }
                            catch (Exception) { }
                        });
                        lock (SysPara.StatusLock)
                        {
                            MiddleLayer.ConveyorF.UpConveyorAutoFlow.AssembCompeletStatus = true;
                            SysPara.TempAssembCompeletStatus = true;
                        }
                        if (GetRecipeValue("RSet", "FeederPickIndex") % 3 != 0)
                        {
                            int FeederPickIndex = GetRecipeValue("RSet", "FeederPickIndex") + (3 - GetRecipeValue("RSet", "FeederPickIndex") % 3);
                            if (FeederPickIndex >= 42)
                            {
                                feederT.Restart();
                                SysPara.JAGStarytEvent.Set();
                                FeederPickIndex = 0;
                            }
                            DataTable dt = RecipeData.Tables["RSet"];
                            dt.Rows[0]["FeederPickIndex"] = FeederPickIndex;
                            dt.AcceptChanges();
                            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                        }
                        return FCResultType.NEXT;
                    }
                }
                else if (SysPara.Que_ProductCode_PickPos.Count > 0)             //App have product,go on assemb next one tary
                {
                    return FCResultType.CASE1;
                }
                else
                {
                    queCCDPosition.Clear();
                    CCDReuslt.Clear();
                    scannerReuslt.Clear();
                    RunTM.Restart();
                    QRCode.Clear();
                    gripperIndex = 1;
                    return FCResultType.NEXT;
                }
            }
            else
            {
                queCCDPosition.Clear();
                CCDReuslt.Clear();
                scannerReuslt.Clear();
                RunTM.Restart();
                return FCResultType.NEXT;
            }             //pick next
        }
        //null
        private FCResultType flowChart45_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        //null
        private FCResultType flowChart46_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        //Pallet full
        private FCResultType flowChart42_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        //R1 Goto safety
        private FCResultType flowChart43_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Goto Safety");
            if (Robot.WriteToEpson(pos[2], pos[3], pos[4], 30000))
            {
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2]
               + "Pos：" + pos[3]
               + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }
        //Wait R1 on safety
        private FCResultType flowChart44_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                lock (SysPara.StatusLock)
                {
                    //SysPara.R1AssembCompeletStatus = true;
                    MiddleLayer.ConveyorF.UpConveyorAutoFlow.AssembCompeletStatus = true;
                }
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                // Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;

            }        //R1 timeout
            else { return FCResultType.IDLE; }
        }
        //Scan again
        private FCResultType flowChart26_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        //Judge Feeder change next one
        private FCResultType flowChart25_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.ScanCount >= SysPara.FeederProductCount)
            {
                SysPara.JAGStarytEvent.Set();
                SysPara.ScanCount = 0;
            }
            if (SysPara.R1GrabCompeletStatus)
            {
                DelayMs(2000);
                SysPara.R1GrabCompeletStatus = false;
            }
            return FCResultType.NEXT;
        }
        //null
        private FCResultType flowChart27_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        #endregion

        #region R1_Pick1Vision
        //CCD1_标定视觉编辑
        private void btnPickPos1AlogrithmEditing_Click(object sender, EventArgs e)
        {
            H1_PickupPos.EditTB();
        }
        //CCD1_视觉标定
        private void btnPickPosStartCalib_Click(object sender, EventArgs e)
        {
            reintento.Dispose();
            reintento = new Reintento();
            reintento.fnSetTextMessageNShow("Are You Sure To Calibrate", true, false, true);
            if (reintento.dResult != DialogResult.Yes)
            {
                return;
            }

            this.flowChart47.TaskReset();
            bAutoCalibFlow = false;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                while (!bAutoCalibFlow)
                {
                    flowChart47.TaskRun();
                }
            });
        }
        /// <summary>
        /// 机器人位置示教
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeachPickVisPos_Click(object sender, EventArgs e)
        {
            H1_PickupPos.RunTB();
            DataTable dt = RecipeData.Tables["Rset"];
            if (H1_PickupPos.IsAccept)
            {
                dt.Rows[0]["PickTeachVisX"] = H1_PickupPos.GetOutput("X").ToString();
                dt.Rows[0]["PickTeachVisY"] = H1_PickupPos.GetOutput("Y").ToString();
                dt.Rows[0]["PickTeachVisU"] = H1_PickupPos.GetOutput("Angle").ToString();
                dt.AcceptChanges();
            }
            else
            {
                dt.Rows[0]["PickTeachVisX"] = Convert.ToString(999.999);
                dt.Rows[0]["PickTeachVisY"] = Convert.ToString(999.999);
                dt.Rows[0]["PickTeachVisU"] = Convert.ToString(999.999);
                dt.AcceptChanges();
            }
        }
        //CCD1_机器人示教
        private void btnTeachPickRobotPos_Click(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["Rset"];
            double? x, y, u;
            //if (RobotZebra.ReadPR(20, ref ReadRobotPR)) //PR20
            if (Robot.ConnectStatus)
            {
                Robot.WriteToEpson("012", "005", "FF", 2000, "FFFFFFF", "FFFFFFF", "FFFFFFF");
                DelayMs(2000);
                double[] p = Robot.ReadPositionFromEpson();
                DelayMs(100);
                p = Robot.ReadPositionFromEpson();
                x = p[0]; y = p[1]; u = p[2];
                RobotPose = p;
            }
            else
            {
                x = 0;
                y = 0;
                u = 0;
            }
            dt.Rows[0]["PickTeachRobotX"] = x.ToString();
            dt.Rows[0]["PickTeachRobotY"] = y.ToString();
            dt.Rows[0]["PickTeachRobotU"] = u.ToString();
            dt.AcceptChanges();
        }

        private void btnTeachPickVisPos2_Click(object sender, EventArgs e)
        {
            H1_PickupPos.RunTB();
            //while (!H2_PickupPos.RunTBOk())
            //{ Thread.Sleep(2); }
            DataTable dt = RecipeData.Tables["Rset"];
            if (H1_PickupPos.IsAccept)
            {
                dt.Rows[0]["PickTeachVisX2"] = H1_PickupPos.GetOutput("X").ToString();
                dt.Rows[0]["PickTeachVisY2"] = H1_PickupPos.GetOutput("Y").ToString();
                dt.Rows[0]["PickTeachVisU2"] = H1_PickupPos.GetOutput("Angle").ToString();
                dt.AcceptChanges();
            }
            else
            {
                dt.Rows[0]["PickTeachVisX2"] = Convert.ToString(999.999);
                dt.Rows[0]["PickTeachVisY2"] = Convert.ToString(999.999);
                dt.Rows[0]["PickTeachVisU2"] = Convert.ToString(999.999);
                dt.AcceptChanges();
            }
        }

        private void btnTeachPickRobotPos2_Click(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["Rset"];
            double? x, y, u;
            if (RobotZebra.ReadPR(20, ref ReadRobotPR)) //PR20
            {
                x = ReadRobotPR[20, 0];
                y = ReadRobotPR[20, 1];
                u = ReadRobotPR[20, 5];
            }
            else
            {
                x = null;
                y = null;
                u = null;
            }
            dt.Rows[0]["PickTeachRobotX2"] = x;
            dt.Rows[0]["PickTeachRobotY2"] = y;
            dt.Rows[0]["PickTeachRobotU2"] = u;
            dt.AcceptChanges();
        }

        private FCResultType flowChart47_FlowRun(object sender, EventArgs e)
        {
            ResetRobot();
            DelayMs(100);
            startRobot();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart48_FlowRun(object sender, EventArgs e)
        {
            CalibPictureCount = 0;
            visCalibData.Clear();
            if (Robot.WriteToEpson("014", "005", "FF", 3000, "FFFFFFF", "FFFFFFF", "FFFFFFF"))//005/011 pick camPos/assemb camPos
            {
                RobotPose = Robot.ReadRobotXYU;
                DelayMs(100);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart49_FlowRun(object sender, EventArgs e)
        {
            //goto CCD Tech Position 
            DelayMs(5000);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart50_FlowRun(object sender, EventArgs e)
        {
            if (SendAutoCalibration(CalibPictureCount + 1, 1))
            {
                DelayMs(100);
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart51_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R timeout
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart52_FlowRun(object sender, EventArgs e)
        {
            //到位后开始拍照
            OB_RobotLight.On();
            DelayMs(200);
            CogImage8Grey img = null;
            Cog3DVect3 xyu = new Cog3DVect3();
            double exposure1 = GetSettingValue("MSet", "PickExpTime");
            if (H1_PickupPos.TakePicture(exposure1))
            {
                img = H1_PickupPos.InputImage;
                if (Robot.ConnectStatus)
                {
                    if (Robot.WriteToEpson("015", "000", "FF", 20000, "FFFFFFF", "FFFFFFF", "FFFFFFF"))//Robot.ReadFromEpson()==1)
                    {
                        double[] Pos = new double[3];
                        Pos = RobotPose;
                        xyu = new Cog3DVect3(Pos[0], Pos[1], Pos[2]);
                    }
                    else { return FCResultType.IDLE; }
                }
                else { return FCResultType.IDLE; }

                Tuple<Cog3DVect3, CogImage8Grey> xyu_Image = new Tuple<Cog3DVect3, CogImage8Grey>(xyu, img);
                visCalibData.Add(xyu_Image);
                CalibPictureCount++;
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart53_FlowRun(object sender, EventArgs e)
        {
            if (CalibPictureCount >= 11)
            {
                return FCResultType.CASE1;
            }
            else
                return FCResultType.NEXT;
        }

        private FCResultType flowChart54_FlowRun(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                if (!H1_PickupPos.TB.Inputs.Contains("CalibDatas"))
                {
                    H1_PickupPos.TB.Inputs.Add(new Cognex.VisionPro.ToolBlock.CogToolBlockTerminal("CalibDatas", new List<Tuple<Cog3DVect3, CogImage8Grey>>()));
                }
                H1_PickupPos.visCalibData = visCalibData;
                H1_PickupPos.TB.Inputs["ISCalib"].Value = true;
                H1_PickupPos.TB.Inputs["GrabPosIndex"].Value = 0;
                H1_PickupPos.RunTB();
                H1_PickupPos.TB.Inputs["ISCalib"].Value = false;
                OB_RobotLight.Off();
                DataTable dt = RecipeData.Tables["R1_PickVisCalib"];
                List<Tuple<Cog3DVect2, Cog3DVect2>> visRobotData = new List<Tuple<Cog3DVect2, Cog3DVect2>>();
                visRobotData = H1_PickupPos.GetOutput("VisRobotData") as List<Tuple<Cog3DVect2, Cog3DVect2>>;
                if (H1_PickupPos.IsAccept)
                {
                    dt.Clear();

                    for (int i = 0; i < visRobotData.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        row["Calib_Index"] = (i + 1).ToString();
                        row["Calib_PixelX"] = visRobotData[i].Item1.X.ToString("f3");
                        row["Calib_PixelY"] = visRobotData[i].Item1.Y.ToString("f3");
                        row["Calib_RobotX"] = visRobotData[i].Item2.X.ToString("f3");
                        row["Calib_RobotY"] = visRobotData[i].Item2.Y.ToString("f3");
                        dt.Rows.Add(row);
                    }
                    dt.AcceptChanges();
                }
                else
                {
                    dt.Rows[0]["Calib_Index"] = "0";
                    dt.Rows[0]["Calib_PixelX"] = "999";
                    dt.Rows[0]["Calib_PixelY"] = "999";
                    dt.Rows[0]["Calib_RobotX"] = "999";
                    dt.Rows[0]["Calib_RobotY"] = "999";
                    dt.AcceptChanges();
                }
            });
            //WriteToRobotR(30, 0);
            bAutoCalibFlow = true;
            return FCResultType.IDLE;
        }
        #endregion

        #region R1_DischargeVision
        private void button2_Click_2(object sender, EventArgs e)
        {
            H1_DischargePos.EditTB();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            reintento.Dispose();
            reintento = new Reintento();
            reintento.fnSetTextMessageNShow("Are You Sure To Calibrate", true, false, true);
            if (reintento.dResult != DialogResult.Yes)
            {
                return;
            }
            this.flowChart62.TaskReset();
            bAutoCalibFlow = false;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                while (!bAutoCalibFlow)
                {
                    flowChart62.TaskRun();
                }
            });
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            H1_DischargePos.RunTB();
            DataTable dt = RecipeData.Tables["Rset"];
            if (H1_DischargePos.IsAccept)
            {
                dt.Rows[0]["DischargeTeachVisX"] = H1_DischargePos.GetOutput("X").ToString();
                dt.Rows[0]["DischargeTeachVisY"] = H1_DischargePos.GetOutput("Y").ToString();
                dt.Rows[0]["DischargeTeachVisU"] = H1_DischargePos.GetOutput("Angle").ToString();
                dt.AcceptChanges();
            }
            else
            {
                dt.Rows[0]["DischargeTeachVisX"] = Convert.ToString(999.999);
                dt.Rows[0]["DischargeTeachVisY"] = Convert.ToString(999.999);
                dt.Rows[0]["DischargeTeachVisU"] = Convert.ToString(999.999);
                dt.AcceptChanges();
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["Rset"];
            double? x, y, u;
            if (Robot.ConnectStatus)
            {
                Robot.WriteToEpson("012", "011", "FF", 2000, "FFFFFFF", "FFFFFFF", "FFFFFFF");
                DelayMs(2000);
                double[] p = Robot.ReadPositionFromEpson();
                DelayMs(100);
                p = Robot.ReadPositionFromEpson();
                x = p[0]; y = p[1]; u = p[2];
                RobotPose = p;
            }
            else
            {
                x = 0;
                y = 0;
                u = 0;
            }
            dt.Rows[0]["DischargeTeachRobotX"] = x;
            dt.Rows[0]["DischargeTeachRobotY"] = y;
            dt.Rows[0]["DischargeTeachRobotU"] = u;
            dt.AcceptChanges();
        }

        private void button52_Click(object sender, EventArgs e)
        {
            H1_DischargePos.RunTB();
            DataTable dt = RecipeData.Tables["Rset"];
            if (H1_DischargePos.IsAccept)
            {
                dt.Rows[0]["DischargeTeachVisX2"] = H1_DischargePos.GetOutput("X").ToString();
                dt.Rows[0]["DischargeTeachVisY2"] = H1_DischargePos.GetOutput("Y").ToString();
                dt.Rows[0]["DischargeTeachVisU2"] = H1_DischargePos.GetOutput("Angle").ToString();
                dt.AcceptChanges();
            }
            else
            {
                dt.Rows[0]["DischargeTeachVisX2"] = Convert.ToString(999.999);
                dt.Rows[0]["DischargeTeachVisY2"] = Convert.ToString(999.999);
                dt.Rows[0]["DischargeTeachVisU2"] = Convert.ToString(999.999);
                dt.AcceptChanges();
            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["Rset"];
            double? x, y, u;
            if (RobotZebra.ReadPR(20, ref ReadRobotPR)) //PR20
            {
                x = ReadRobotPR[20, 0];
                y = ReadRobotPR[20, 1];
                u = ReadRobotPR[20, 5];
            }
            else
            {
                x = null;
                y = null;
                u = null;
            }
            dt.Rows[0]["DischargeTeachRobotX2"] = x;
            dt.Rows[0]["DischargeTeachRobotY2"] = y;
            dt.Rows[0]["DischargeTeachRobotU2"] = u;
            dt.AcceptChanges();
        }

        private FCResultType flowChart62_FlowRun(object sender, EventArgs e)
        {
            ResetRobot();
            DelayMs(100);
            startRobot();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart61_FlowRun(object sender, EventArgs e)
        {
            //CalibPictureCount = 0;
            //visCalibData.Clear();
            //return FCResultType.NEXT;
            CalibPictureCount = 0;
            visCalibData.Clear();
            if (Robot.WriteToEpson("014", "011", "FF", 3000, "FFFFFFF", "FFFFFFF", "FFFFFFF"))//005/011 pick camPos/assemb camPos
            {
                RobotPose = Robot.ReadRobotXYU;
                DelayMs(100);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart60_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R timeout
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart57_FlowRun(object sender, EventArgs e)
        {
            if (SendAutoCalibration(CalibPictureCount + 1, 2))
            {
                DelayMs(100);
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart58_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R timeout
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart59_FlowRun(object sender, EventArgs e)
        {
            //到位后开始拍照
            OB_RobotLight.On();
            DelayMs(20);
            CogImage8Grey img = null;
            Cog3DVect3 xyu = new Cog3DVect3();
            double exposure2 = GetSettingValue("MSet", "DisChargeExpTime");
            if (H1_DischargePos.TakePicture(exposure2))
            {
                img = H1_DischargePos.InputImage;
                if (Robot.ConnectStatus)
                {
                    if (Robot.WriteToEpson("015", "000", "FF", 20000, "FFFFFFF", "FFFFFFF", "FFFFFFF"))//Robot.ReadFromEpson()==1)
                    {
                        double[] Pos = new double[3];
                        Pos = RobotPose;
                        xyu = new Cog3DVect3(Pos[0], Pos[1], Pos[2]);
                    }
                    else { return FCResultType.IDLE; }
                }
                else { return FCResultType.IDLE; }
                Bitmap IMG = img.ToBitmap();
                IMG.Save("D:/" + CalibPictureCount.ToString() + ".bmp");
                Tuple<Cog3DVect3, CogImage8Grey> xyu_Image = new Tuple<Cog3DVect3, CogImage8Grey>(xyu, img);
                visCalibData.Add(xyu_Image);
                CalibPictureCount++;
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }


        private FCResultType flowChart56_FlowRun(object sender, EventArgs e)
        {
            if (CalibPictureCount >= 11)
            {
                return FCResultType.CASE1;
            }
            else
                return FCResultType.NEXT;
        }

        private FCResultType flowChart55_FlowRun(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                if (!H1_DischargePos.TB.Inputs.Contains("CalibDatas"))
                {
                    H1_DischargePos.TB.Inputs.Add(new Cognex.VisionPro.ToolBlock.CogToolBlockTerminal("CalibDatas", new List<Tuple<Cog3DVect3, CogImage8Grey>>()));
                }
                H1_DischargePos.visCalibData = visCalibData;
                H1_DischargePos.TB.Inputs["ISCalib"].Value = true;
                H1_DischargePos.TB.Inputs["GrabPosIndex"].Value = 0;
                H1_DischargePos.RunTB();
                H1_DischargePos.TB.Inputs["ISCalib"].Value = false;
                OB_RobotLight.Off();
                DataTable dt = RecipeData.Tables["R1_DischargeVisCalib"];
                List<Tuple<Cog3DVect2, Cog3DVect2>> visRobotData = new List<Tuple<Cog3DVect2, Cog3DVect2>>();
                visRobotData = H1_DischargePos.GetOutput("VisRobotData") as List<Tuple<Cog3DVect2, Cog3DVect2>>;
                if (H1_DischargePos.IsAccept)
                {
                    dt.Clear();

                    for (int i = 0; i < visRobotData.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        row["Calib_Index"] = (i + 1).ToString();
                        row["Calib_PixelX"] = visRobotData[i].Item1.X.ToString("f3");
                        row["Calib_PixelY"] = visRobotData[i].Item1.Y.ToString("f3");
                        row["Calib_RobotX"] = visRobotData[i].Item2.X.ToString("f3");
                        row["Calib_RobotY"] = visRobotData[i].Item2.Y.ToString("f3");
                        dt.Rows.Add(row);
                    }
                    dt.AcceptChanges();
                }
                else
                {
                    dt.Rows[0]["Calib_Index"] = "0";
                    dt.Rows[0]["Calib_PixelX"] = "999";
                    dt.Rows[0]["Calib_PixelY"] = "999";
                    dt.Rows[0]["Calib_RobotX"] = "999";
                    dt.Rows[0]["Calib_RobotY"] = "999";
                    dt.AcceptChanges();
                }
            });
            //WriteToRobotR(30, 0);
            bAutoCalibFlow = true;
            return FCResultType.IDLE;
        }
        #endregion







        public void BConnect()
        {
            bConnect = PFClient.Connect();
            bComStart = PFClient.StartCommunication();
            bSubscribe = PFClient.SubscribeLastTighteningResult();
            PFKeepAlive();
            PFClient.CommunicationAlive();
        }

        public void BConnect2()
        {
            bConnect2 = PFClient2.Connect();
            bComStart2 = PFClient2.StartCommunication();
            bSubscribe2 = PFClient2.SubscribeLastTighteningResult();
            PFKeepAlive2();
            PFClient2.CommunicationAlive();
        }







        public void SaveDataOK()
        {
            string[] sTemp = new string[4];
            sTemp[0] = FastenDataCurrent.Point.ToString();
            sTemp[1] = FastenDataCurrent.Torque.ToString();
            sTemp[2] = FastenDataCurrent.Angle.ToString();
            sTemp[3] = FastenDataCurrent.Result.ToString();
            RefreshDifferentThreadUI(dgv_H1_ScrewResults, () =>
            {
                dgv_H1_ScrewResults.Rows.Add(sTemp);
                dgv_H1_ScrewResults.Update();
                dgv_H1_ScrewResults.Refresh();
            });
        }

        public void SaveDataOK2()
        {
            string[] sTemp = new string[4];
            sTemp[0] = FastenDataCurrent2.Point2.ToString();
            sTemp[1] = FastenDataCurrent2.Torque2.ToString();
            sTemp[2] = FastenDataCurrent2.Angle2.ToString();
            sTemp[3] = FastenDataCurrent2.Result2.ToString();
            RefreshDifferentThreadUI(dgv_H2_ScrewResults, () =>
            {
                dgv_H2_ScrewResults.Rows.Add(sTemp);
                dgv_H2_ScrewResults.Update();
                dgv_H2_ScrewResults.Refresh();
            });
        }

        public void SaveDataNG()
        {
            string[] sTemp = new string[4];
            sTemp[0] = FastenDataCurrent.Point.ToString();
            sTemp[1] = FastenDataCurrent.Torque.ToString();
            sTemp[2] = FastenDataCurrent.Angle.ToString();
            sTemp[3] = FastenDataCurrent.Result.ToString();
            RefreshDifferentThreadUI(dgv_H1_ScrewResults, () =>
            {
                dgv_H1_ScrewResults.Rows.Add(sTemp);
                dgv_H1_ScrewResults.Refresh();
                dgv_H1_ScrewResults.Update();
            });
        }

        public void SaveDataNG2()
        {
            string[] sTemp = new string[4];
            sTemp[0] = FastenDataCurrent2.Point2.ToString();
            sTemp[1] = FastenDataCurrent2.Torque2.ToString("F3");
            sTemp[2] = FastenDataCurrent2.Angle2.ToString("F3");
            sTemp[3] = FastenDataCurrent2.Result2.ToString();
            RefreshDifferentThreadUI(dgv_H2_ScrewResults, () =>
            {
                dgv_H2_ScrewResults.Rows.Add(sTemp);
                dgv_H2_ScrewResults.Refresh();
                dgv_H2_ScrewResults.Update();
            });
        }


        private void button46_Click_1(object sender, EventArgs e)
        {
            //H1_VFiducia5.EditTB();
        }

        private void button47_Click(object sender, EventArgs e)
        {
            //SysPara.gBarcodeInfo = null;
            //Task.Factory.StartNew(() =>
            //{
            //    H1_VFiducia5.RunTB();
            //    while (!H1_VFiducia5.RunTBOk()) { Thread.Sleep(1); }
            //    if (H1_VFiducia5.IsAccept)
            //    {
            //        RefreshDifferentThreadUI(txt_barcode, () =>
            //        {
            //            //txt_H1_CalibResult.Text = "OK";
            //            //txt_H1_CalibResult.BackColor = Color.Lime;
            //            //txt_H1_CalibPixelX.Text = H1_VCalibration.Calibration.x.ToString("F3");
            //            //txt_H1_CalibPixelY.Text = H1_VCalibration.Calibration.y.ToString("F3");
            //            txt_barcode.Text = SysPara.gBarcodeInfo;
            //        });
            //    }
            //    else
            //    {
            //        RefreshDifferentThreadUI(txt_H1_CalibResult, () =>
            //        {
            //            //txt_H1_CalibResult.BackColor = Color.Red;
            //            //txt_H1_CalibResult.Text = "NG";
            //            //txt_H1_CalibPixelX.Text = "Null";
            //            //txt_H1_CalibPixelY.Text = "Null";
            //            txt_barcode.Text = "Read Barcode Fail!";
            //        });
            //    }
            //});
        }



        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void Client2_Click(object sender, EventArgs e)
        {
            ConnectBarcode2();
        }

        private void btServer2_Click(object sender, EventArgs e)
        {
            TrigerBarcode2();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            KeyenceBarcode2.Disconnect();
        }



        private void button49_Click(object sender, EventArgs e)
        {
            string sTtime = "[" + DateTime.Now.ToString() + "\r\n" + "]" + DateTime.Now.ToString();
            string sData = "";

            for (int i = 0; i < MiddleLayer.GantryF.DataTotal.Length; i++)
            {
                sData += MiddleLayer.GantryF.DataTotal[i] + "\r\n";
            }
            MiddleLayer.GantryF.TPFP = true;
            SaveFile.SaveMES(MiddleLayer.GantryF.textBox23.Text, sData, MiddleLayer.GantryF.TPFP, sTtime);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            int Data1 = 1;
            int data2 = 85;
            string data3 = "OK";
            DataTotal[Datanum++] = "MTorque: " + Data1;
            DataTotal[Datanum++] = "MAngle: " + data2;
            DataTotal[Datanum++] = "M: " + data3;

        }

        private void button44_Click_1(object sender, EventArgs e)
        {
            Datanum = 0;
            Array.Clear(DataTotal, 0, DataTotal.Length);

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void Btn_ConnectRobot_Click_1(object sender, EventArgs e)
        {
            if (ConnectEpsonRobot())
            {
                btnConnectResult.Text = "ConnectSuccess";
                btnConnectResult.BackColor = Color.Green;
            }
            else
            {
                btnConnectResult.Text = "ConnectFail";
                btnConnectResult.BackColor = Color.Red;
            }
        }

        private void button39_Click_1(object sender, EventArgs e)
        {
            //H1_VFiducia4.EditTB();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            //System.Threading.Tasks.Task.Factory.StartNew(() =>
            //{
            //    H1_VFiducia4.RunTB();
            //    while (!H1_VFiducia4.RunTBOk()) { Thread.Sleep(2); }
            //    if (H1_VFiducia4.IsAccept)
            //    {
            //        RefreshDifferentThreadUI(txt_H4_FiducialResult, () =>
            //        {
            //            txt_H4_FiducialResult.Text = "OK";
            //            txt_H4_FiducialResult.BackColor = Color.Lime;
            //            txt_H4_FiducialX.Text = H1_VFiducia4.Fiducial.x.ToString("F3");
            //            txt_H4_FiducialY.Text = H1_VFiducia4.Fiducial.y.ToString("F3");
            //            if (H1_VFiducia4.Fiducial.u > 0)
            //            {
            //                H1_VFiducia4.Fiducial.u = (H1_VFiducia4.Fiducial.u * 180 / Math.PI);
            //                H1_VFiducia4.Fiducial.u = H1_VFiducia4.Fiducial.u - 360;
            //                txt_H4_FiducialU.Text = H1_VFiducia4.Fiducial.u.ToString("F3");
            //            }
            //            else
            //            {
            //                txt_H4_FiducialU.Text = (H1_VFiducia4.Fiducial.u * 180 / Math.PI).ToString("F3");
            //            }
            //        });
            //    }
            //    else
            //    {
            //        RefreshDifferentThreadUI(txt_H4_FiducialResult, () =>
            //        {
            //            txt_H4_FiducialResult.BackColor = Color.Red;
            //            txt_H4_FiducialResult.Text = "NG";
            //            txt_H4_FiducialX.Text = "Null";
            //            txt_H4_FiducialY.Text = "Null";
            //            txt_H4_FiducialU.Text = "Null";
            //        });
            //    }
            //});
        }

        private void button35_Click(object sender, EventArgs e)
        {
            //DialogResult reult = MessageBox.Show("Sure Add Vision?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            //if (reult == DialogResult.No)
            //{
            //    return;
            //}

            //if (H1_VFiducia4.IsAccept)
            //{
            //    DataTable dt = RecipeData.Tables["tb_H1_Screw2Check"];
            //    DataRow dr = dt.NewRow();
            //    dr[0] = H1_VFiducia4.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducia4.Fiducial.y.ToString("F3");
            //    dr[2] = (H1_VFiducia4.Fiducial.u * 180 / 3.14).ToString("F3");
            //    dt.Rows.Add(dr);
            //    //SysPara.items++;
            //    return;
            //}
            //else
            //{
            //    MessageBox.Show("Vision error!");
            //    return;
            //}
        }

        private void button31_Click_1(object sender, EventArgs e)
        {
            //if (dgv_H4_VisionData.CurrentRow != null)
            //{
            //    DataTable dt = RecipeData.Tables["tb_H1_Screw2Check"];
            //    DataRow dr = dt.Rows[dgv_H4_VisionData.CurrentRow.Index];
            //    dr[0] = H1_VFiducia4.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducia4.Fiducial.y.ToString("F3");
            //    dr[2] = (H1_VFiducia4.Fiducial.u * 180 / 3.14).ToString("F3");
            //    //SysPara.items++;
            //}
            //else
            //{
            //    MessageBox.Show("Vision error!");
            //}
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (dgv_H4_VisionData.CurrentRow != null)
            {
                DataTable dt = RecipeData.Tables["tb_H1_Screw2Check"];
                dt.Rows.RemoveAt(dgv_H4_VisionData.CurrentRow.Index);
            }

            //SysPara.items++;
        }



        private void tabPage6_Click(object sender, EventArgs e)
        {

        }



        private void H1_VisionDataAdd_Click(object sender, EventArgs e)
        {
            //DialogResult reult = MessageBox.Show("Sure Add Vision?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            //if (reult == DialogResult.No)
            //{
            //    return;
            //}

            //if (H1_VFiducial.IsAccept)
            //{
            //    DataTable dt = RecipeData.Tables["tb_H1_PickCheck"];
            //    DataRow dr = dt.NewRow();
            //    dr[0] = H1_VFiducial.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducial.Fiducial.y.ToString("F3");
            //    dr[2] = (H1_VFiducial.Fiducial.u * 180 / 3.14).ToString("F3");
            //    dt.Rows.Add(dr);
            //    //SysPara.items++;
            //    return;
            //}
            //else
            //{
            //    MessageBox.Show("Vision error!");
            //    return;
            //}
        }

        private void H1_VisionDataReplace_Click(object sender, EventArgs e)
        {
            //if (dgv_H1_VisionData.CurrentRow != null)
            //{
            //    DataTable dt = RecipeData.Tables["tb_H1_PickCheck"];
            //    DataRow dr = dt.Rows[dgv_H1_VisionData.CurrentRow.Index];
            //    dr[0] = H1_VFiducial.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducial.Fiducial.y.ToString("F3");
            //    dr[2] = (H1_VFiducial.Fiducial.u * 180 / 3.14).ToString("F3");
            //    //SysPara.items++;
            //}
            //else
            //{
            //    MessageBox.Show("Vision error!");
            //}
        }

        private void H1_VisionDataDelete_Click(object sender, EventArgs e)
        {
            if (dgv_H1_VisionData.CurrentRow != null)
            {
                DataTable dt = RecipeData.Tables["tb_H1_PickCheck"];
                dt.Rows.RemoveAt(dgv_H1_VisionData.CurrentRow.Index);
            }

            //SysPara.items++;
        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            //DialogResult reult = MessageBox.Show("Sure Add Vision?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            //if (reult == DialogResult.No)
            //{
            //    return;
            //}

            //if (H1_VFiducia2.IsAccept)
            //{
            //    DataTable dt = RecipeData.Tables["tb_H1_PlaceCheck"];
            //    DataRow dr = dt.NewRow();
            //    dr[0] = H1_VFiducia2.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducia2.Fiducial.y.ToString("F3");
            //    dr[2] = (H1_VFiducia2.Fiducial.u * 180 / 3.14).ToString("F3");
            //    dt.Rows.Add(dr);
            //    //SysPara.items++;
            //    return;
            //}
            //else
            //{
            //    MessageBox.Show("Vision error!");
            //    return;
            //}
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            //if (dgv_H2_VisionData.CurrentRow != null)
            //{
            //    DataTable dt = RecipeData.Tables["tb_H1_PlaceCheck"];
            //    DataRow dr = dt.Rows[dgv_H2_VisionData.CurrentRow.Index];
            //    dr[0] = H1_VFiducia2.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducia2.Fiducial.y.ToString("F3");
            //    dr[2] = (H1_VFiducia2.Fiducial.u * 180 / 3.14).ToString("F3");
            //    //SysPara.items++;
            //}
            //else
            //{
            //    MessageBox.Show("Vision error!");
            //}
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (dgv_H2_VisionData.CurrentRow != null)
            {
                DataTable dt = RecipeData.Tables["tb_H1_PlaceCheck"];
                dt.Rows.RemoveAt(dgv_H2_VisionData.CurrentRow.Index);
            }

            //SysPara.items++;
        }

        private void button41_Click_1(object sender, EventArgs e)
        {
            //DialogResult reult = MessageBox.Show("Sure Add Vision?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            //if (reult == DialogResult.No)
            //{
            //    return;
            //}

            //if (H1_VFiducia3.IsAccept)
            //{
            //    DataTable dt = RecipeData.Tables["tb_H1_ScrewCheck"];
            //    DataRow dr = dt.NewRow();
            //    dr[0] = H1_VFiducia3.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducia3.Fiducial.y.ToString("F3");
            //    dr[2] = (H1_VFiducia3.Fiducial.u * 180 / 3.14).ToString("F3");
            //    dt.Rows.Add(dr);
            //    //SysPara.items++;
            //    return;
            //}
            //else
            //{
            //    MessageBox.Show("Vision error!");
            //    return;
            //}
        }

        private void btnReadR_Click_1(object sender, EventArgs e)
        {
            if (Robot.ReadFromEpson() == 1)
            {
                txtReadData.Text = Robot.ReadRobotStr;
                btnReadR.BackColor = Color.Green;
            }
            else
            {
                btnReadR.BackColor = Color.Red;
            }
        }

        private void btnWritePR_Click_1(object sender, EventArgs e)
        {
            if (Robot.WriteToEpson(textBox49.Text, "FFF", "FF", 59000, numWritePR1.Value.ToString(), numWritePR2.Value.ToString(), numWritePR3.Value.ToString()))
            {
                btnWritePR.BackColor = Color.Green;
            }
            else
            {
                btnWritePR.BackColor = Color.Red;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            processData.fnInitilizeResultTable();
        }

        private void btnWriteR_Click_1(object sender, EventArgs e)
        {
            if (Robot.WriteToEpson(textBox49.Text, "FFF", "FF", 59000))
            {
                btnWriteR.BackColor = Color.Green;
            }
            else
            {
                btnWriteR.BackColor = Color.Red;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string[] pos1 = GetDataByAnnotation(dgv_EpsonPosition, "GetPosition");
            if (pos1 != null)
            {
                if (Robot.WriteToEpson(pos1[2], pos1[3], pos1[4], 30000))
                {
                    int ret = Robot.ReadFromEpson();
                    if (ret == 12)
                    {
                        txtReadPRX.Text = Robot.ReadRobotXYU[0].ToString();
                        txtReadPRY.Text = Robot.ReadRobotXYU[1].ToString();
                        txtReadPRZ.Text = Robot.ReadRobotXYU[2].ToString();
                    }
                }
                else
                {
                    txtReadPRX.Text = "-999.999";
                    txtReadPRY.Text = "-999.999";
                    txtReadPRZ.Text = "-999.999";
                }
            }
        }

        private void bt_ReadIO_Click(object sender, EventArgs e)
        {
            bt_ReadIO.Enabled = false;
            this.flowChart1.TaskReset();
            bReadIOFlow = false;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                while (!bReadIOFlow)
                {
                    flowChart1.TaskRun();
                }
            });
        }

        private void bt_WriteIO_Click_1(object sender, EventArgs e)
        {
            WriteIOTimer.Restart();
            bool ret;
            bt_WriteIO.Enabled = false;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    ret = GetRobotDO();
                    if (ret || WriteIOTimer.On(5000)) { bt_WriteIO.Enabled = true; break; }
                }
            });
        }
        #region readIOFlow
        private FCResultType flowChart1_FlowRun(object sender, EventArgs e)
        {
            ReadIOTimer.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_FlowRun(object sender, EventArgs e)
        {
            string[] pos1 = GetDataByAnnotation(dgv_EpsonPosition, "Read RobotDI");
            if (ReadIOTimer.On(5000))
            {
                ReadIOTimer.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Read RobotDI sent Command timeout", SysPara.bEnableGeneralSaveLog);
                // Log.log.Write("Read RobotDI 指令发送超时", Color.Red);
                return FCResultType.NEXT;
            }
            if (Robot.WriteToEpson(pos1[2], pos1[3], pos1[4], 30000))
            {
                ReadIOTimer.Restart();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart3_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ReadIOTimer.On(5000))
            {
                bReadIOFlow = true;
                bt_ReadIO.Enabled = true;
                MiddleLayer.LogF.AddLog(LogType.Production, "Read RobotDI recive command timeout", SysPara.bEnableGeneralSaveLog);

                //Log.log.Write("Read RobotDI 接收数据超时", Color.Red);
                return FCResultType.NEXT;
            }
            if (ret == 103)
            {
                ReadIO = Robot.Input;
                return FCResultType.NEXT;
            }

            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart4_FlowRun(object sender, EventArgs e)
        {
            string[] pos1 = GetDataByAnnotation(dgv_EpsonPosition, "Read RobotDO");
            if (ReadIOTimer.On(5000))
            {
                ReadIOTimer.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Read RobotDO sent Command timeout", SysPara.bEnableGeneralSaveLog);

                // Log.log.Write("Read RobotDO 指令发送超时", Color.Red);
                return FCResultType.NEXT;
            }
            if (Robot.WriteToEpson(pos1[2], pos1[3], pos1[4], 30000))
            {
                ReadIOTimer.Restart();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }



        private FCResultType flowChart5_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ReadIOTimer.On(5000))
            {
                bReadIOFlow = true;
                bt_ReadIO.Enabled = true;
                MiddleLayer.LogF.AddLog(LogType.Production, "Read RobotDO recive command timeout", SysPara.bEnableGeneralSaveLog);

                //Log.log.Write("Read RobotDO 接收数据超时", Color.Red);
                return FCResultType.NEXT;
            }
            if (ret == 102)
            {
                WriteIO = Robot.Output;
                bReadIOFlow = true;
                bt_ReadIO.Enabled = true;
                return FCResultType.IDLE;
            }

            else { return FCResultType.IDLE; }
        }
        #endregion




        private void button32_Click(object sender, EventArgs e)
        {
            //if (dgv_H3_VisionData.CurrentRow != null)
            //{
            //    DataTable dt = RecipeData.Tables["tb_H1_ScrewCheck"];
            //    DataRow dr = dt.Rows[dgv_H3_VisionData.CurrentRow.Index];
            //    dr[0] = H1_VFiducia3.Fiducial.x.ToString("F3");
            //    dr[1] = H1_VFiducia3.Fiducial.y.ToString("F3");
            //    dr[2] = (H1_VFiducia3.Fiducial.u * 180 / 3.14).ToString("F3");
            //    //SysPara.items++;
            //}
            //else
            //{
            //    MessageBox.Show("Vision error!");
            //}
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (dgv_H3_VisionData.CurrentRow != null)
            {
                DataTable dt = RecipeData.Tables["tb_H1_ScrewCheck"];
                dt.Rows.RemoveAt(dgv_H3_VisionData.CurrentRow.Index);
            }

            //SysPara.items++;
        }




        #region 机器人点位表
        private void VUDatagridView1_RaiseSelectedEvent(bool bComplete)
        {
            int index = dgv_EpsonPosition.myDgv.CurrentRow.Index;

            string[] selectedRowArr = dgv_EpsonPosition.GetDataArrayByIndex(index);

            DataGridViewRow dataGirdRow = dgv_EpsonPosition.GetDataGridRow(index);

            //tbx_X.Text = selectedRowArr[2];
            //tbx_Y.Text = selectedRowArr[3];
            //tbx_Z.Text = selectedRowArr[4];
        }



        private void dgv_ErrInfo_RaiseSelectedEvent(bool bComplete)
        {
            int index = dgv_EpsonErrInfo.myDgv.CurrentRow.Index;

            string[] selectedRowArr = dgv_EpsonErrInfo.GetDataArrayByIndex(index);

            DataGridViewRow dataGirdRow = dgv_EpsonErrInfo.GetDataGridRow(index);

            //tbx_X.Text = selectedRowArr[2];
            //tbx_Y.Text = selectedRowArr[3];
            //tbx_Z.Text = selectedRowArr[4];
        }



        public string[] GetDataByAnnotation(VUControl.VUDatagridView dgv, string annotation)
        {
            string[] GetDataInfo = null;
            try
            {
                GetDataInfo = dgv.GetDataArrayByFlagName(1, annotation);
            }
            catch (Exception)
            {

                SysPara.NPShowAlarm("2110");
            }

            return GetDataInfo;
        }

        public string[] GetDataByAnnotationforTech(VUControl.VUDatagridView dgv, string annotation)
        {
            string[] GetDataInfo = dgv.GetDataArrayByFlagName(4, annotation);
            return GetDataInfo;
        }
        public bool ConnectEpsonRobot()
        {
            if (Robot.ConnectToEpson(MiddleLayer.GantryF.GetRecipeValue("RSet", "RobotPoint1"), MiddleLayer.GantryF.GetRecipeValue("RSet", "RobotPort")))
            {
                if (!bgwEpsonCommunicate.IsBusy)
                    bgwEpsonCommunicate.RunWorkerAsync();
                return true;
            }
            else
            {
                return false;
            }
        }


        private void chk_Output_1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_1.Checked)
            {
                chk_Output_1.BackgroundImage = Properties.Resources.Green;
                WriteIO[0] = "1";
            }
            else
            {
                chk_Output_1.BackgroundImage = Properties.Resources.Red;
                WriteIO[0] = "0";
            }
        }

        private void chk_Output_2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_2.Checked)
            {
                chk_Output_2.BackgroundImage = Properties.Resources.Green;
                WriteIO[1] = "1";
            }
            else
            {
                chk_Output_2.BackgroundImage = Properties.Resources.Red;
                WriteIO[1] = "0";
            }
        }
        private void chk_Output_3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_3.Checked)
            {
                chk_Output_3.BackgroundImage = Properties.Resources.Green;
                WriteIO[2] = "1";
            }
            else
            {
                chk_Output_3.BackgroundImage = Properties.Resources.Red;
                WriteIO[2] = "0";
            }
        }

        private void chk_Output_4_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_4.Checked)
            {
                chk_Output_4.BackgroundImage = Properties.Resources.Green;
                WriteIO[3] = "1";
            }
            else
            {
                chk_Output_4.BackgroundImage = Properties.Resources.Red;
                WriteIO[3] = "0";
            }
        }

        private void chk_Output_5_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_5.Checked)
            {
                chk_Output_5.BackgroundImage = Properties.Resources.Green;
                WriteIO[4] = "1";
            }
            else
            {
                chk_Output_5.BackgroundImage = Properties.Resources.Red;
                WriteIO[4] = "0";
            }
        }

        private void chk_Output_6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_6.Checked)
            {
                chk_Output_6.BackgroundImage = Properties.Resources.Green;
                WriteIO[5] = "1";
            }
            else
            {
                chk_Output_6.BackgroundImage = Properties.Resources.Red;
                WriteIO[5] = "0";
            }
        }

        private void chk_Output_7_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_7.Checked)
            {
                chk_Output_7.BackgroundImage = Properties.Resources.Green;
                WriteIO[6] = "1";
            }
            else
            {
                chk_Output_7.BackgroundImage = Properties.Resources.Red;
                WriteIO[6] = "0";
            }
        }

        private void chk_Output_8_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_8.Checked)
            {
                chk_Output_8.BackgroundImage = Properties.Resources.Green;
                WriteIO[7] = "1";
            }
            else
            {
                chk_Output_8.BackgroundImage = Properties.Resources.Red;
                WriteIO[7] = "0";
            }
        }

        private void chk_Output_9_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_9.Checked)
            {
                chk_Output_9.BackgroundImage = Properties.Resources.Green;
                WriteIO[8] = "1";
            }
            else
            {
                chk_Output_9.BackgroundImage = Properties.Resources.Red;
                WriteIO[8] = "0";
            }
        }

        private void chk_Output_10_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_10.Checked)
            {
                chk_Output_10.BackgroundImage = Properties.Resources.Green;
                WriteIO[9] = "1";
            }
            else
            {
                chk_Output_10.BackgroundImage = Properties.Resources.Red;
                WriteIO[9] = "0";
            }
        }

        private void chk_Output_11_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_11.Checked)
            {
                chk_Output_11.BackgroundImage = Properties.Resources.Green;
                WriteIO[10] = "1";
            }
            else
            {
                chk_Output_11.BackgroundImage = Properties.Resources.Red;
                WriteIO[10] = "0";
            }
        }


        private void chk_Output_12_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_12.Checked)
            {
                chk_Output_12.BackgroundImage = Properties.Resources.Green;
                WriteIO[11] = "1";
            }
            else
            {
                chk_Output_12.BackgroundImage = Properties.Resources.Red;
                WriteIO[11] = "0";
            }
        }

        private void chk_Output_13_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_13.Checked)
            {
                chk_Output_13.BackgroundImage = Properties.Resources.Green;
                WriteIO[12] = "1";
            }
            else
            {
                chk_Output_13.BackgroundImage = Properties.Resources.Red;
                WriteIO[12] = "0";
            }
        }



        private void chk_Output_14_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_14.Checked)
            {
                chk_Output_14.BackgroundImage = Properties.Resources.Green;
                WriteIO[13] = "1";
            }
            else
            {
                chk_Output_14.BackgroundImage = Properties.Resources.Red;
                WriteIO[13] = "0";
            }
        }

        private void chk_Output_15_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_15.Checked)
            {
                chk_Output_15.BackgroundImage = Properties.Resources.Green;
                WriteIO[14] = "1";
            }
            else
            {
                chk_Output_15.BackgroundImage = Properties.Resources.Red;
                WriteIO[14] = "0";
            }
        }

        private void chk_Output_16_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Output_16.Checked)
            {
                chk_Output_16.BackgroundImage = Properties.Resources.Green;
                WriteIO[15] = "1";
            }
            else
            {
                chk_Output_16.BackgroundImage = Properties.Resources.Red;
                WriteIO[15] = "0";
            }
        }
        public bool GetRobotDO()
        {
            string[] pos1 = GetDataByAnnotation(dgv_EpsonPosition, "Write RobotDO");
            string[] s1 = new string[7];
            string[] s2 = new string[7];
            string[] s3 = new string[7];
            for (int i = 0; i < 21; i++)
            {
                if (i < 7) { s1[i] = WriteIO[i]; }
                else if (i >= 7 && i < 14) { s2[i - 7] = WriteIO[i]; }
                else if (i >= 14 && i < 16) { s3[i - 14] = WriteIO[i]; }
                else { s3[i - 14] = "F"; }
            }
            bool ret = Robot.WriteToEpson(pos1[2], pos1[3], "FF", 30000, string.Join("", s1), string.Join("", s2), string.Join("", s3));
            return ret;
        }
        private void tabControl4_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl2.SelectedIndex == 2)
            {
                if (Robot.ConnectStatus)
                {
                    bt_ReadIO_Click(null, null);
                }
            }
        }

        private void label115_DoubleClick_1(object sender, EventArgs e)
        {

        }

        private void trackBar2_MouseUptrackBar2_MouseUp_1(object sender, MouseEventArgs e)
        {

        }

        private FCResultType flowChart63_FlowRun(object sender, EventArgs e)
        {
            //MOD@@ Guillermo Carrillo - guarda de indice: curR1HaveNum
            if (curR1HaveNum >= 0
                && curR1HaveNum < CCDReuslt.Count
                && curR1HaveNum < scannerReuslt.Count
                && (CCDReuslt[curR1HaveNum]) && (scannerReuslt[curR1HaveNum]))
            {
                curR1HaveOKNum++;
                return FCResultType.NEXT;
            }//&& (scannerReuslt[curR1HaveNum])
            else
            {
                if (queCCDPosition.Count > 0)
                    queCCDPosition.Dequeue();
                return FCResultType.CASE1;
            }
        }

        private FCResultType flowChart65_FlowRun(object sender, EventArgs e)
        {
            if (!GetRecipeValue("RSet", "NGbuffer1"))
            {
                SysPara.ngPos = "001";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer2"))
            {
                SysPara.ngPos = "002";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer3"))
            {
                SysPara.ngPos = "003";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer4"))
            {
                SysPara.ngPos = "004";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer5"))
            {
                SysPara.ngPos = "005";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer6"))
            {
                SysPara.ngPos = "006";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer7"))
            {
                SysPara.ngPos = "007";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer8"))
            {
                SysPara.ngPos = "008";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer9"))
            {
                SysPara.ngPos = "009";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer10"))
            {
                SysPara.ngPos = "010";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer11"))
            {
                SysPara.ngPos = "011";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer12"))
            {
                SysPara.ngPos = "012";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer13"))
            {
                SysPara.ngPos = "013";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer14"))
            {
                SysPara.ngPos = "014";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer15"))
            {
                SysPara.ngPos = "015";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer16"))
            {
                SysPara.ngPos = "016";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer17"))
            {
                SysPara.ngPos = "017";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer18"))
            {
                SysPara.ngPos = "018";
                return FCResultType.NEXT;
            }

            else
            {

                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "NG had full", SysPara.bEnableGeneralSaveLog);
                SysPara.NPShowAlarm("0006");
                reintento.Dispose();
                reintento = new Reintento();
                reintento.fnSetTextMessageNShow("NG tray is full!!! sure has been replaced?", true, false, true);
                flowChart72.Enabled = true;
                flowChart72.BackColor = Color.Red;
                if (reintento.dResult == DialogResult.Yes)
                {
                    DataTable dt = RecipeData.Tables["RSet"];
                    dt.Rows[0]["NGbuffer1"] = false;
                    dt.Rows[0]["NGbuffer2"] = false;
                    dt.Rows[0]["NGbuffer3"] = false;
                    dt.Rows[0]["NGbuffer4"] = false;
                    dt.Rows[0]["NGbuffer5"] = false;
                    dt.Rows[0]["NGbuffer6"] = false;
                    dt.Rows[0]["NGbuffer7"] = false;
                    dt.Rows[0]["NGbuffer8"] = false;
                    dt.Rows[0]["NGbuffer9"] = false;
                    dt.Rows[0]["NGbuffer10"] = false;
                    dt.Rows[0]["NGbuffer11"] = false;
                    dt.Rows[0]["NGbuffer12"] = false;
                    dt.Rows[0]["NGbuffer13"] = false;
                    dt.Rows[0]["NGbuffer14"] = false;
                    dt.Rows[0]["NGbuffer15"] = false;
                    dt.Rows[0]["NGbuffer16"] = false;
                    dt.Rows[0]["NGbuffer17"] = false;
                    dt.Rows[0]["NGbuffer18"] = false;
                    dt.AcceptChanges();
                    this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                    SysPara.ngPos = "001";
                    return FCResultType.NEXT;
                }
                else
                {
                    DelayMs(500);
                    return FCResultType.IDLE;
                }
            }
        }

        private void flowChart65_DoubleClick(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["NGbuffer1"] = false;
            dt.Rows[0]["NGbuffer2"] = false;
            dt.Rows[0]["NGbuffer3"] = false;
            dt.Rows[0]["NGbuffer4"] = false;
            dt.Rows[0]["NGbuffer5"] = false;
            dt.Rows[0]["NGbuffer6"] = false;
            dt.Rows[0]["NGbuffer7"] = false;
            dt.Rows[0]["NGbuffer8"] = false;
            dt.Rows[0]["NGbuffer9"] = false;
            dt.Rows[0]["NGbuffer10"] = false;
            dt.Rows[0]["NGbuffer11"] = false;
            dt.Rows[0]["NGbuffer12"] = false;
            dt.Rows[0]["NGbuffer13"] = false;
            dt.Rows[0]["NGbuffer14"] = false;
            dt.Rows[0]["NGbuffer15"] = false;
            dt.Rows[0]["NGbuffer16"] = false;
            dt.Rows[0]["NGbuffer17"] = false;
            dt.Rows[0]["NGbuffer18"] = false;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            flowChart65.Enabled = false;
            flowChart65.BackColor = Color.White;
        }



        private FCResultType flowChart66_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "GetPosition");
            if (Robot.WriteToEpson(pos[2], pos[3], pos[4], 30000))
            {
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2] + "Pos：" + pos[3] + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            H1_PickupPos.RunTB();
            //while (!H2_PickupPos.RunTBOk())
            //{ Thread.Sleep(2); }
            DataTable dt = RecipeData.Tables["Rset"];
            if (H1_PickupPos.IsAccept)
            {
                dt.Rows[0]["PickTeachVisX3"] = H1_PickupPos.GetOutput("X").ToString();
                dt.Rows[0]["PickTeachVisY3"] = H1_PickupPos.GetOutput("Y").ToString();
                dt.Rows[0]["PickTeachVisU3"] = H1_PickupPos.GetOutput("Angle").ToString();
                dt.AcceptChanges();
            }
            else
            {
                dt.Rows[0]["PickTeachVisX3"] = Convert.ToString(999.999);
                dt.Rows[0]["PickTeachVisY3"] = Convert.ToString(999.999);
                dt.Rows[0]["PickTeachVisU3"] = Convert.ToString(999.999);
                dt.AcceptChanges();
            }
        }

        private FCResultType flowChart67_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "GetPosition");
            if (Robot.WriteToEpson(pos[2], pos[3], pos[4], 30000))
            {
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2] + "Pos：" + pos[3] + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            H1_DischargePos.RunTB();
            DataTable dt = RecipeData.Tables["Rset"];
            if (H1_DischargePos.IsAccept)
            {
                dt.Rows[0]["DischargeTeachVisX3"] = H1_DischargePos.GetOutput("X").ToString();
                dt.Rows[0]["DischargeTeachVisY3"] = H1_DischargePos.GetOutput("Y").ToString();
                dt.Rows[0]["DischargeTeachVisU3"] = H1_DischargePos.GetOutput("Angle").ToString();
                dt.AcceptChanges();
            }
            else
            {
                dt.Rows[0]["DischargeTeachVisX3"] = Convert.ToString(999.999);
                dt.Rows[0]["DischargeTeachVisY3"] = Convert.ToString(999.999);
                dt.Rows[0]["DischargeTeachVisU3"] = Convert.ToString(999.999);
                dt.AcceptChanges();
            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["Rset"];
            double? x, y, u;
            if (Robot.ConnectStatus)
            {
                Robot.WriteToEpson("012", "011", "FF", 2000, "FFFFFFF", "FFFFFFF", "FFFFFFF");
                DelayMs(2000);
                double[] p = Robot.ReadPositionFromEpson();
                DelayMs(100);
                p = Robot.ReadPositionFromEpson();
                x = p[0]; y = p[1]; u = p[2];
                RobotPose = p;
            }
            else
            {
                x = 0;
                y = 0;
                u = 0;
            }
            dt.Rows[0]["DischargeTeachRobotX3"] = x;
            dt.Rows[0]["DischargeTeachRobotY3"] = y;
            dt.Rows[0]["DischargeTeachRobotU3"] = u;
            dt.AcceptChanges();
        }

        private FCResultType flowChart68_FlowRun(object sender, EventArgs e)
        {
            string x = "000.000";
            string y = "000.000";
            string u = "000.000";
            if (queCCDPosition.Count > 0)
            {
                VisionPos visPos = queCCDPosition.Dequeue();
                if (visPos.x <= 0)
                {
                    if ((Math.Abs(visPos.x) - 100.0) >= 0)
                    {
                        VisX = visPos.x.ToString("000.00");
                    }
                    else
                    {
                        VisX = visPos.x.ToString("00.000");
                    }
                }
                else { VisX = visPos.x.ToString("000.000"); }

                if (visPos.y <= 0)
                {
                    if ((Math.Abs(visPos.y) - 100.0) >= 0)
                    {
                        VisY = visPos.y.ToString("000.00");
                    }
                    else
                    {
                        VisY = visPos.y.ToString("00.000");
                    }
                }
                else { VisY = visPos.y.ToString("000.000"); }
                if (visPos.u <= 0)
                {
                    if ((Math.Abs(visPos.u) - 100.0) >= 0)
                    {
                        VisU = visPos.u.ToString("000.00");
                    }
                    else
                    {
                        VisU = visPos.u.ToString("00.000");
                    }
                }
                else { VisU = visPos.u.ToString("000.000"); }
                //VisX = (visPos.x < 0) ? visPos.x.ToString("00.000") : visPos.x.ToString("000.000");
                //VisY = (visPos.y < 0) ? visPos.y.ToString("00.000") : visPos.y.ToString("000.000");
                //VisU = (visPos.u < 0) ? visPos.u.ToString("00.000") : visPos.u.ToString("000.000");
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart69_FlowRun(object sender, EventArgs e)
        {
            string x = "000.000";
            string y = "000.000";
            string u = "000.000";
            if (queCCDPosition.Count > 0)
            {
                VisionPos visPos = queCCDPosition.Dequeue();
                if (visPos.x < 0)
                {
                    if ((Math.Abs(visPos.x) - 100.0) >= 0)
                    {
                        VisX = visPos.x.ToString("000.00");
                    }
                    else
                    {
                        VisX = visPos.x.ToString("00.000");
                    }
                }
                else { VisX = visPos.x.ToString("000.000"); }

                if (visPos.y < 0)
                {
                    if ((Math.Abs(visPos.y) - 100.0) >= 0)
                    {
                        VisY = visPos.y.ToString("000.00");
                    }
                    else
                    {
                        VisY = visPos.y.ToString("00.000");
                    }
                }
                else { VisY = visPos.y.ToString("000.000"); }
                if (visPos.u < 0)
                {
                    if ((Math.Abs(visPos.u) - 100.0) >= 0)
                    {
                        VisU = visPos.u.ToString("000.00");
                    }
                    else
                    {
                        VisU = visPos.u.ToString("00.000");
                    }
                }
                else { VisU = visPos.u.ToString("000.000"); }
                //VisX = (visPos.x < 0) ? visPos.x.ToString("00.000") : visPos.x.ToString("000.000");
                //VisY = (visPos.y < 0) ? visPos.y.ToString("00.000") : visPos.y.ToString("000.000");
                //VisU = (visPos.u < 0) ? visPos.u.ToString("00.000") : visPos.u.ToString("000.000");
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart64_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1) { OB_RobotLight.On(); MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing  cammer position ok", SysPara.bEnableGeneralSaveLog); return FCResultType.NEXT; }
            else if (ret == 0)
            {
                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[1], SysPara.bEnableGeneralSaveLog);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {
                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing  cammer position timeout", SysPara.bEnableGeneralSaveLog);
                SysPara.NPShowAlarm("1635"); return FCResultType.IDLE;
            }
            else { return FCResultType.IDLE; }

        }

        private void groupBox24_Enter(object sender, EventArgs e)
        {

        }

        private void button59_Click(object sender, EventArgs e)
        {
            processData.fnShowSerial("1223");
        }

        private FCResultType flowChart71_FlowRun(object sender, EventArgs e)
        {
            string cmd = "001";
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "DisableVision"))
            {
                cmd = "002";
            }
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "RobotSafetyMode"))
            {
                cmd = "003";
            }
            string[] pos = GetDataByAnnotation(MiddleLayer.GantryF.dgv_EpsonPosition, "Disable Vision");
            if (Robot.WriteToEpson(pos[2], cmd, pos[4], 30000))
            {
                InitTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command:" + pos[2]
                + "Pos:" + cmd
                + "AppIndex:" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart70_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                // Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart73_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(MiddleLayer.GantryF.dgv_EpsonPosition, "Write Robot Speed");
            if (Robot.WriteToEpson(pos[2], pos[3], pos[4], 30000, string.Format("{0:d7}", 30)))
            {
                InitTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command:" + pos[2]
                + "Pos:" + pos[3]
                + "AppIndex:" + pos[4], SysPara.bEnableGeneralSaveLog);
                InitTM.Restart();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart72_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 104)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == -104)
            {
                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout
            else { return FCResultType.IDLE; }
        }


        //R1 Goto tray assemb Bpositon
        private FCResultType flowChart74_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Convery Pallet AssembPosition");
            int GripperIndex = gripperIndex; //InvertOrder(SysPara.Que_ProductCode_PickPos.Count);
            Thread.Sleep(50);
            if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", SysPara.Que_Product_pallet_Code.Count + 1),
                string.Format("{0:d2}", GripperIndex), 30000, VisX, VisY, VisU))
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {

                        int num = InvertOrder(SysPara.Que_ProductCode_PickPos.Count);
                        //MOD@@ Guillermo Carrillo - guarda de indice: FeederImages[num-1]
                        if (num < 1 || num > FeederImages.Count) { return; }
                        string saveImgPath = MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath") + "\\";
                        string ImgSaveName = string.Format(@"{0}_{1}_NA_NA_{2}_{3}.bmp",
                               FeederImages[num - 1].PCBQRCode,
                               MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[SysPara.Que_Product_pallet_Code.Count],
                          FeederImages[num - 1].Timer,
                           MiddleLayer.MesF2.GetSettingValue("PSet", "EQPID")
                           );

                        //MOD@@ Guillermo Carrillo - guarda de indice: PCBCode
                        if (SysPara.Que_Product_pallet_Code.Count < PCBCode.Length)
                        {
                            PCBCode[SysPara.Que_Product_pallet_Code.Count] = FeederImages[num - 1].PCBQRCode;
                        }
                        using (Bitmap img = new Bitmap(FeederImages[num - 1].Image))
                        {
                            if (!Directory.Exists(saveImgPath)) { Directory.CreateDirectory(saveImgPath); }
                            img.Save(saveImgPath + ImgSaveName);
                        }
                        if (num == 3)
                            FeederImages.Clear();

                    }
                    catch (Exception)
                    {

                    }
                });




                //processData.fnAddCountUnit(true);
                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2]
               + "Pos：" + string.Format("{0:d3}", SysPara.Que_Product_pallet_Code.Count + 1)
               + "AppIndex：" + InvertOrder(SysPara.Que_ProductCode_PickPos.Count) + "X:" + VisX + "Y:" + VisY + "U:" + VisU, SysPara.bEnableGeneralSaveLog);
                SysPara.OverPressureab = false;
                SysPara.OverPressurebc = false;
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart76_FlowRun(object sender, EventArgs e)
        {

            SysPara.DischargeFlag = true;

            switch (InvertOrder(SysPara.Que_ProductCode_PickPos.Count))
            {
                case 1:
                    PressReadCOM.ContinuedRead_COM1();
                    break;
                case 2:
                    PressReadCOM.ContinuedRead_COM2();
                    break;
                case 3:
                    PressReadCOM.ContinuedRead_COM3();
                    break;
                default:
                    break;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart75_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                SysPara.RobotPressureCompelet = true;
                SysPara.OverPressureab = false;
                SysPara.OverPressurebc = false;
                RunTM.Restart();
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                // Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                if (Robot.ErorrCode == "E1642" || Robot.ErorrCode == "E1643" || Robot.ErorrCode == "E1644")
                {
                    SysPara.RobotPressureCompelet = true;
                    SysPara.OverPressureab = true;
                    return FCResultType.NEXT;
                }
                //else if (Robot.ErorrCode == "E060")
                //{
                //    SysPara.RobotPressureCompelet = true;
                //    SysPara.OverPressurebc = true;
                //    return FCResultType.NEXT;
            }

            else if (RunTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635");
                RunTM.Restart();
            }        //R1 timeout

            return FCResultType.IDLE;

        }
        //{
        //Judge need to takepicture
        private FCResultType flowChart77_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Que_Product_pallet_Code.Count >= 3)
            {
                return FCResultType.CASE1;
            }
            else
            {
                ProductImages.Clear();
                palletUpResult.Clear();
                palletDownResult.Clear();
                return FCResultType.NEXT;
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {

            if (Robot.ConnectStatus)
            {
                string[] pos1 = GetDataByAnnotation(dgv_EpsonPosition, "Write RobotDO");
                string[] s1 = new string[7];
                string[] s2 = new string[7];
                string[] s3 = new string[7];
                for (int i = 0; i < 21; i++)
                {
                    if (i < 7) { s1[i] = WriteIO[i]; }
                    else if (i >= 7 && i < 14) { s2[i - 7] = WriteIO[i]; }
                    else if (i >= 14 && i < 16) { s3[i - 14] = WriteIO[i]; }
                    else { s3[i - 14] = "F"; }
                }
                s1[6] = "0";
                s2[0] = "1";
                s2[1] = "0";
                s2[2] = "1";
                s2[3] = "0";
                s2[4] = "1";
                bool ret = Robot.WriteToEpson(pos1[2], pos1[3], "FF", 30000, string.Join("", s1), string.Join("", s2), string.Join("", s3));
            }
        }

        private FCResultType flowChart78_FlowRun(object sender, EventArgs e)
        {
            robotPos = new double[3];
            robotPos = Robot.ReadPositionFromEpson();
            if (RunTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }
            if (robotPos[0] == 0 && robotPos[1] == 0)
            {
                return FCResultType.IDLE;
            }
            else
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Read robot xyu:" + robotPos[0].ToString() + "," + robotPos[1].ToString() + "," + robotPos[2].ToString(), SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
        }

        private FCResultType flowChart79_FlowRun(object sender, EventArgs e)
        {
            robotPos = new double[3];
            robotPos = Robot.ReadPositionFromEpson();
            if (RunTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635"); return FCResultType.IDLE;
            }
            if (robotPos[0] == 0 && robotPos[1] == 0)
            {
                return FCResultType.IDLE;
            }
            else
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Read robot xyu:" + robotPos[0].ToString() + "," + robotPos[1].ToString() + "," + robotPos[2].ToString(), SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            if (MiddleLayer.GantryF.Robot.ConnectStatus)
            {
                string[] pos = MiddleLayer.GantryF.GetDataByAnnotation(MiddleLayer.GantryF.dgv_EpsonPosition, "GetPosition");
                if (MiddleLayer.GantryF.Robot.WriteToEpson(pos[2], pos[3], pos[4], 30000))
                {
                    Thread.Sleep(50);
                    robotPos = new double[3];
                    robotPos = Robot.ReadPositionFromEpson();
                    string[] posArr = new string[] { robotPos[0].ToString(), robotPos[1].ToString(), robotPos[2].ToString(), "DisChargePos", };
                    dgv_DischargeTechPos.WriteRowToDataGrid(posArr);
                }
                else
                {
                    MiddleLayer.LogF.AddLog(LogType.Production, "Get position fail", SysPara.bEnableGeneralSaveLog);
                }
            }
        }






        #endregion

        //倒序处理 1->3,3->1
        public int InvertOrder(int Num)
        {
            if (curProductCount < Num)
            {

                curProductCount = 3;

            }


            switch (curProductCount)
            {
                case 3:
                    return Num * (-1) + 4;
                case 2:
                    return Num * (-1) + 3;
                case 1:
                    return Num * (-1) + 2;
                default:
                    return Num * (-1) + 4;
            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            double X = 0, Y = 0, U = 0;
            int index = 0;
            string[] posArr;
            for (int i = 0; i < (int)GetSettingValue("PSet", "Row"); i++)
            {
                for (int j = 0; j < (int)GetSettingValue("PSet", "Column"); j++)
                {
                    X = GetSettingValue("PSet", "StartX") + j * GetSettingValue("PSet", "OffsetX");
                    Y = GetSettingValue("PSet", "StartY") + i * GetSettingValue("PSet", "OffsetY");
                    U = 90.543;
                    index++;
                    if (rdb_FeederCamPos.Checked)
                    {
                        posArr = new string[] { "FeederCamPos" + index.ToString(), X.ToString(), Y.ToString(), U.ToString(), };
                    }
                    else
                    {
                        posArr = new string[] { "ConveryCamPos" + index.ToString(), X.ToString(), Y.ToString(), U.ToString(), };
                    }
                    dgv_RobotPosition.WriteRowToDataGrid(posArr);
                }
            }
        }

        private void button63_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                txb_ImagePath.Text = fbDialog.SelectedPath;
            }
            txb_ImagePath.Focus();
        }
        #region feeder
        private FCResultType flowChart87_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "DisableFeeder"))
            {
                return FCResultType.IDLE;
            }
            else
            {
                FeederTM.Restart();
                return FCResultType.NEXT;
            }

        }

        private FCResultType flowChart86_FlowRun(object sender, EventArgs e)
        {
            if (IB_FeederReady.On())
            {
                //OB_FeederCompleted.Off();
                OB_FeederStart.Off();
                DelayMs(100);
                OB_FeederStart.On();
                DelayMs(1000);
                OB_FeederStart.Off();
                FeederTM.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                if (FeederTM.On(SysPara.TimeOutSec))
                {

                    SysPara.NPShowAlarm("1101");        //feeder Ready timeout
                }
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart80_FlowRun(object sender, EventArgs e)
        {
            if (IB_FeederInPlace.On())
            {
                OB_FeederStart.Off();
                // OB_FeederCompleted.Off();
                SysPara.JAGStarytEvent.Reset();
                return FCResultType.NEXT;
            }
            else
            {
                if (FeederTM.On(40000))
                {

                    SysPara.NPShowAlarm("1102");        //feeder in place timeout
                }
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart81_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "DisableFeeder"))
            {

                return FCResultType.IDLE;
            }


            if (SysPara.JAGStarytEvent.WaitOne(0))
            {
                if (MiddleLayer.GantryF.IB_FeederAlarm.On())
                {

                    SysPara.NPShowAlarm("1100");       //Feeder Alarm
                    return FCResultType.CASE2;
                }
                if (MiddleLayer.GantryF.IB_FeederLack.On())
                {
                    SysPara.NPShowAlarm("1104");           //Feeder Lack
                    return FCResultType.CASE2;
                }
                if (!MiddleLayer.GantryF.IB_FeederReady.On())
                {

                    SysPara.NPShowAlarm("1101");       //Feeder not ready
                    return FCResultType.CASE2;
                }
                return FCResultType.NEXT;
            }
            if (MiddleLayer.GantryF.IB_FeederAlarm.On())
            {

                SysPara.NPShowAlarm("1100");       //Feeder Alarm
            }
            if (MiddleLayer.GantryF.IB_FeederLack.On())
            {
                SysPara.NPShowAlarm("1104");           //Feeder Lack
            }
            if (!MiddleLayer.GantryF.IB_FeederReady.On())
            {

                SysPara.NPShowAlarm("1101");       //Feeder not ready
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart82_FlowRun(object sender, EventArgs e)
        {
            //OB_FeederCompleted.Off();
            //OB_FeederCompleted.On();
            //DelayMs(500);
            //OB_FeederCompleted.Off();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart84_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.GantryF.IB_FeederAlarm.On())
            {

                SysPara.NPShowAlarm("1100");       //Feeder Alarm
                return FCResultType.IDLE;
            }
            if (MiddleLayer.GantryF.IB_FeederLack.On())
            {

                SysPara.NPShowAlarm("1104");           //Feeder Lack
                return FCResultType.IDLE;
            }
            if (!MiddleLayer.GantryF.IB_FeederReady.On())
            {

                SysPara.NPShowAlarm("1101");       //Feeder not ready
                return FCResultType.IDLE;
            }

            SysPara.NPShowAlarm("1101");
            reintento.Dispose();
            reintento = new Reintento();
            reintento.fnSetTextMessageNShow("confirm feeder whether to ready,yes or no?", true, false, true);
            if (DialogResult.Yes == reintento.dResult)
            {
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart85_FlowRun(object sender, EventArgs e)
        {
            //OB_FeederCompleted.Off();
            //DelayMs(200);
            OB_FeederCompleted.On();
           // DelayMs(500);
            //OB_FeederCompleted.Off();
            //OB_FeederStart.On();
            //DelayMs(500);
            MiddleLayer.LogF.AddLog(LogType.Production, "OB_FeederCompleted=true", true);
            FeederTM.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart83_FlowRun(object sender, EventArgs e)
        {
            if (FeederTM.On(500))
            {
                //OB_FeederCompleted.Off();
            }
            if (IB_FeederInPlace.On())
            {
                OB_FeederStart.Off();
                MiddleLayer.LogF.AddLog(LogType.Production, " OB_FeederStart=false", true);
                //OB_FeederCompleted.Off();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }
        #endregion

        private FCResultType flowChart88_FlowRun(object sender, EventArgs e)
        {
            SysPara.visionPos = new VisionPos();
            try
            {
                double exposure = GetSettingValue("MSet", "NGCCDCheckExpTime");
                if (H1_NGCCDCheck.TakePicture(exposure))         //CCD take picture
                {

                    RefreshDifferentThreadUI(cogRecDisp_H5_Recipe, () =>
                    {
                        cogRecDisp_H5_Recipe.Image = H1_NGCCDCheck.InputImage;
                        cogRecDisp_H5_Recipe.Fit();
                    });

                    //if (!MiddleLayer.SystemF.GetSettingValue("PSet", "DisableVision"))
                    //{
                    H1_NGCCDCheck.RunTB();
                    if (Convert.ToBoolean(H1_NGCCDCheck.TB.Outputs["IsHaveProduct"].Value) == false)
                    {
                        MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot NG throwing position1 not have product", SysPara.bEnableGeneralSaveLog);
                        OB_RobotLight.Off();
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        string bufferIndex = "NGbuffer1";
                        switch (SysPara.ngPos)
                        {
                            case "001":
                                bufferIndex = "NGbuffer1";
                                break;
                            case "002":
                                bufferIndex = "NGbuffer2";
                                break;
                            case "003":
                                bufferIndex = "NGbuffer3";
                                break;
                            case "004":
                                bufferIndex = "NGbuffer4";
                                break;
                            case "005":
                                bufferIndex = "NGbuffer5";
                                break;
                            case "006":
                                bufferIndex = "NGbuffer6";
                                break;
                            case "007":
                                bufferIndex = "NGbuffer7";
                                break;
                            case "008":
                                bufferIndex = "NGbuffer8";
                                break;
                            case "009":
                                bufferIndex = "NGbuffer9";
                                break;
                            case "010":
                                bufferIndex = "NGbuffer10";
                                break;
                            case "011":
                                bufferIndex = "NGbuffer11";
                                break;
                            case "012":
                                bufferIndex = "NGbuffer12";
                                break;
                            case "013":
                                bufferIndex = "NGbuffer13";
                                break;
                            case "014":
                                bufferIndex = "NGbuffer14";
                                break;
                            case "015":
                                bufferIndex = "NGbuffer15";
                                break;
                            case "016":
                                bufferIndex = "NGbuffer16";
                                break;
                            case "017":
                                bufferIndex = "NGbuffer17";
                                break;
                            case "018":
                                bufferIndex = "NGbuffer18";
                                break;
                            default:
                                break;
                        }
                        DataTable dt = RecipeData.Tables["RSet"];       //Init feeder index
                        dt.Rows[0][bufferIndex] = true;
                        dt.AcceptChanges();
                        this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

                        MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot NG throwing position1 have product!!!need clear", SysPara.bEnableGeneralSaveLog);

                        return FCResultType.CASE1;
                    }
                }
                else
                {
                    string bufferIndex = "NGbuffer1";
                    switch (SysPara.ngPos)
                    {
                        case "001":
                            bufferIndex = "NGbuffer1";
                            break;
                        case "002":
                            bufferIndex = "NGbuffer2";
                            break;
                        case "003":
                            bufferIndex = "NGbuffer3";
                            break;
                        case "004":
                            bufferIndex = "NGbuffer4";
                            break;
                        case "005":
                            bufferIndex = "NGbuffer5";
                            break;
                        case "006":
                            bufferIndex = "NGbuffer6";
                            break;
                        case "007":
                            bufferIndex = "NGbuffer7";
                            break;
                        case "008":
                            bufferIndex = "NGbuffer8";
                            break;
                        case "009":
                            bufferIndex = "NGbuffer9";
                            break;
                        case "010":
                            bufferIndex = "NGbuffer10";
                            break;
                        case "011":
                            bufferIndex = "NGbuffer11";
                            break;
                        case "012":
                            bufferIndex = "NGbuffer12";
                            break;
                        case "013":
                            bufferIndex = "NGbuffer13";
                            break;
                        case "014":
                            bufferIndex = "NGbuffer14";
                            break;
                        case "015":
                            bufferIndex = "NGbuffer15";
                            break;
                        case "016":
                            bufferIndex = "NGbuffer16";
                            break;
                        case "017":
                            bufferIndex = "NGbuffer17";
                            break;
                        case "018":
                            bufferIndex = "NGbuffer18";
                            break;
                        default:
                            break;
                    }
                    DataTable dt = RecipeData.Tables["RSet"];       //Init feeder index
                    dt.Rows[0][bufferIndex] = true;
                    dt.AcceptChanges();
                    this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

                    CCDAlogrithmStatus = false;

                    SysPara.NPShowAlarm("1204");        //CCD fail
                    return FCResultType.CASE1;
                }
            }
            catch
            {
                string bufferIndex = "NGbuffer1";
                switch (SysPara.ngPos)
                {
                    case "001":
                        bufferIndex = "NGbuffer1";
                        break;
                    case "002":
                        bufferIndex = "NGbuffer2";
                        break;
                    case "003":
                        bufferIndex = "NGbuffer3";
                        break;
                    case "004":
                        bufferIndex = "NGbuffer4";
                        break;
                    case "005":
                        bufferIndex = "NGbuffer5";
                        break;
                    case "006":
                        bufferIndex = "NGbuffer6";
                        break;
                    case "007":
                        bufferIndex = "NGbuffer7";
                        break;
                    case "008":
                        bufferIndex = "NGbuffer8";
                        break;
                    case "009":
                        bufferIndex = "NGbuffer9";
                        break;
                    case "010":
                        bufferIndex = "NGbuffer10";
                        break;
                    case "011":
                        bufferIndex = "NGbuffer11";
                        break;
                    case "012":
                        bufferIndex = "NGbuffer12";
                        break;
                    case "013":
                        bufferIndex = "NGbuffer13";
                        break;
                    case "014":
                        bufferIndex = "NGbuffer14";
                        break;
                    case "015":
                        bufferIndex = "NGbuffer15";
                        break;
                    case "016":
                        bufferIndex = "NGbuffer16";
                        break;
                    case "017":
                        bufferIndex = "NGbuffer17";
                        break;
                    case "018":
                        bufferIndex = "NGbuffer18";
                        break;
                    default:
                        break;
                }
                DataTable dt = RecipeData.Tables["RSet"];       //Init feeder index
                dt.Rows[0][bufferIndex] = true;
                dt.AcceptChanges();
                this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

                CCDAlogrithmStatus = false;
                SysPara.NPShowAlarm("1204");        //CCD fail
                return FCResultType.CASE1;
            }
        }

        private FCResultType flowChart89_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart90_FlowRun(object sender, EventArgs e)
        {
            if (Robot.WriteToEpson("017", SysPara.ngPos, "FF", 20000))
            {
                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing position cammer", SysPara.bEnableGeneralSaveLog);
                RunTM.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart91_FlowRun(object sender, EventArgs e)
        {



            if (Robot.WriteToEpson("016", SysPara.ngPos, string.Format("{0:d2}", gripperIndex), 20000))
            {
                int QRCodeCount = gripperIndex; /*InvertOrder(SysPara.Que_ProductCode_PickPos.Count);*/
                //MOD@@ Guillermo Carrillo - guarda de indice: QRCode[gripperIndex-1]
                string QR = (gripperIndex >= 1 && gripperIndex <= QRCode.Count) ? QRCode[gripperIndex - 1] : "";

                string str = string.Format(QRCodeCount.ToString() + "# Product,QRCode:{0},NGIndex:" + SysPara.ngPos, QR);
                MiddleLayer.LogF.AddLog(LogType.NGProduct, str, true);
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing position", SysPara.bEnableGeneralSaveLog);
                RunTM.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart93_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1) { MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing position ok", SysPara.bEnableGeneralSaveLog); return FCResultType.NEXT; }
            else if (ret == 0)
            {
                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[1], SysPara.bEnableGeneralSaveLog);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing position timeout", SysPara.bEnableGeneralSaveLog); SysPara.NPShowAlarm("15008"); 
                return FCResultType.IDLE;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart92_FlowRun(object sender, EventArgs e)
        {
            string bufferIndex = "NGbuffer1";
            switch (SysPara.ngPos)
            {
                case "001":
                    bufferIndex = "NGbuffer1";
                    break;
                case "002":
                    bufferIndex = "NGbuffer2";
                    break;
                case "003":
                    bufferIndex = "NGbuffer3";
                    break;
                case "004":
                    bufferIndex = "NGbuffer4";
                    break;
                case "005":
                    bufferIndex = "NGbuffer5";
                    break;
                case "006":
                    bufferIndex = "NGbuffer6";
                    break;
                case "007":
                    bufferIndex = "NGbuffer7";
                    break;
                case "008":
                    bufferIndex = "NGbuffer8";
                    break;
                case "009":
                    bufferIndex = "NGbuffer9";
                    break;
                case "010":
                    bufferIndex = "NGbuffer10";
                    break;
                case "011":
                    bufferIndex = "NGbuffer11";
                    break;
                case "012":
                    bufferIndex = "NGbuffer12";
                    break;
                case "013":
                    bufferIndex = "NGbuffer13";
                    break;
                case "014":
                    bufferIndex = "NGbuffer14";
                    break;
                case "015":
                    bufferIndex = "NGbuffer15";
                    break;
                case "016":
                    bufferIndex = "NGbuffer16";
                    break;
                case "017":
                    bufferIndex = "NGbuffer17";
                    break;
                case "018":
                    bufferIndex = "NGbuffer18";
                    break;
                default:
                    break;
            }
            DataTable dt = RecipeData.Tables["RSet"];       //Init feeder index
            dt.Rows[0][bufferIndex] = true;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            if (Convert.ToBoolean(dt.Rows[0]["NGbuffer1"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer2"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer3"]) == true && Convert.ToBoolean(dt.Rows[0]["NGbuffer4"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer5"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer6"]) == true && Convert.ToBoolean(dt.Rows[0]["NGbuffer7"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer8"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer9"]) == true && Convert.ToBoolean(dt.Rows[0]["NGbuffer10"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer11"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer12"]) == true && Convert.ToBoolean(dt.Rows[0]["NGbuffer13"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer14"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer15"]) == true && Convert.ToBoolean(dt.Rows[0]["NGbuffer16"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer17"]) == true
                && Convert.ToBoolean(dt.Rows[0]["NGbuffer18"]) == true)
            {
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "ng had full", SysPara.bEnableGeneralSaveLog);
                SysPara.NPShowAlarm("0006");
                reintento.Dispose();
                reintento = new Reintento();
                reintento.fnSetTextMessageNShow("NG tray is full!!! sure has been replaced?", true, false, true);
                flowChart72.Enabled = true;
                flowChart72.BackColor = Color.Red;
                if (reintento.dResult == DialogResult.Yes)
                {
                    DataTable dt1 = RecipeData.Tables["RSet"];
                    dt1.Rows[0]["NGbuffer1"] = false;
                    dt1.Rows[0]["NGbuffer2"] = false;
                    dt1.Rows[0]["NGbuffer3"] = false;
                    dt1.Rows[0]["NGbuffer4"] = false;
                    dt1.Rows[0]["NGbuffer5"] = false;
                    dt1.Rows[0]["NGbuffer6"] = false;
                    dt1.Rows[0]["NGbuffer7"] = false;
                    dt1.Rows[0]["NGbuffer8"] = false;
                    dt1.Rows[0]["NGbuffer9"] = false;
                    dt1.Rows[0]["NGbuffer10"] = false;
                    dt1.Rows[0]["NGbuffer11"] = false;
                    dt1.Rows[0]["NGbuffer12"] = false;
                    dt1.Rows[0]["NGbuffer13"] = false;
                    dt1.Rows[0]["NGbuffer14"] = false;
                    dt1.Rows[0]["NGbuffer15"] = false;
                    dt1.Rows[0]["NGbuffer16"] = false;
                    dt1.Rows[0]["NGbuffer17"] = false;
                    dt1.Rows[0]["NGbuffer18"] = false;
                    dt1.AcceptChanges();
                    this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                    SysPara.ngPos = "001";
                    //processData.fnAddCountUnit(false);
                    return FCResultType.NEXT;
                }
                else
                {
                    DelayMs(500);
                    return FCResultType.IDLE;
                }
            }
            //processData.fnAddCountUnit(false);
            throwingjudge = true;
            return FCResultType.NEXT;
        }

        DateTime Timestamp_10730 = DateTime.Now;
        JTimer Timer_10730 = new JTimer();
        JTimer Timer_LGIT_MULTI_MATERIALID_CONFIRM = new JTimer();
        private FCResultType npFlowChart2_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }

                List<string> _Material_List = new List<string>();
                var keys = CurrentPCBList.Keys.ToList();
                string traycode = "";
                for (int i = 0; i < keys.Count; i++)
                {
                    int index = keys[i];
                    try
                    {
                        traycode = MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[index];
                    }
                    catch (Exception)
                    {
                        traycode = MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[index - 1];
                    }
                    //string qrcode = CurrentPCBList[index];
                    _Material_List.Add(traycode);
                }

                Timestamp_10730 = DateTime.Now;
                MESLib.CommParas.MesManager.LGIT_MULTI_MATERIALID_CONFIRM_OK = false;
                MESLib.CommParas.MesManager.LGIT_MULTI_MATERIALID_FAIL_OK = false;
                MESLib.CommParas.MesManager.MES_S6F11_10730(_Material_List,
                    MESLib.CommParas.MesManager.PRODID, "", "24");
                Timer_10730.Restart();
                Timer_LGIT_MULTI_MATERIALID_CONFIRM.Restart();

            }

            return FCResultType.NEXT;
        }


        List<string> GetBarcodeFromSV(object obj)
        {
            List<string> listDic = new List<string>();
            foreach (var list in (IList<object>)obj)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var prop in (IDictionary<string, object>)list)
                {
                    if (prop.Value is VarItem<string>)
                    {
                        if ("10105" == (prop.Value as VarItem<string>).id)
                            listDic.Add((prop.Value as VarItem<string>).content);
                    }
                }
            }
            return listDic;
        }

        private void button54_Click(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["finishedCounts"] = 0;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

        }



        private FCResultType npFlowChart1_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = true;
            SysPara.RIniRet = true;
            //MOD@@ Guillermo Carrillo - fin del reinicio rapido
            //  Este nodo cierra tanto la inicializacion completa como el
            //  reinicio rapido. Apagar la bandera detiene el empuje desde
            //  AlwaysRun. Si la bandera ya venia apagada no hace nada.
            if (bRestartQuickActive)
            {
                bRestartQuickActive = false;
                MiddleLayer.LogF.AddLog(LogType.Production,
                    "Reinicio rapido: terminado, robot listo.", true);
            }
            return FCResultType.IDLE;
        }

        private void button26_Click_2(object sender, EventArgs e)
        {
            OB_RobotLight.On();
        }

        private void button64_Click(object sender, EventArgs e)
        {
            OB_RobotLight.Off();
        }

        private void button65_Click(object sender, EventArgs e)
        {
            H1_NGCCDCheck.EditTB();
        }

        private void tm_CableDetection_Tick()
        {
            string Reader = "172.168.100.100";
            string Camera = "192.168.200.201";
            //string Reader = "192.168.150.100";//GetRecipeValue("RSet", "ScannerIP");
            //string Camera = "192.168.200.201";
            //string plcIP = "172.168.100.100";
            string robotIP = "192.168.100.100";
            while (SysPara.bCableDetectEnable)
            {
                DelayMs(3000);
                if (!DectNetworkCable(Reader))
                {

                    SysPara.NPShowAlarm("1300");
                }
                if (!DectNetworkCable(Camera))
                {

                    SysPara.NPShowAlarm("1200");
                }
                //if (!DectNetworkCable(plcIP))
                //{
                //    SysPara.NPShowAlarm("00421");
                //}
                if (!DectNetworkCable(robotIP))
                {
                    SysPara.NPShowAlarm("1634");
                }
                //if (MiddleLayer.SystemF.OB_LeftDoorCyDown.GetState())
                //{
                //    if (SysPara.LeftDoorAlarmEnable)
                //    {
                //        if (!MiddleLayer.SystemF.IB_LeftDoorDownSensor.On(200))
                //        {
                //            SysPara.NPShowAlarm("00421", "The left door was illegally opened");
                //            SysPara.LeftDoorAlarmEnable = false;
                //        }
                //    }
                //}
                //if (MiddleLayer.SystemF.OB_RightDoorCyDown.GetState())
                //{
                //    if (SysPara.RightDoorAlarmEnable)
                //    {
                //        if (!MiddleLayer.SystemF.IB_RightDoorDownSensor.On(200))
                //        {
                //            SysPara.NPShowAlarm("00422", "The right door was illegally opened");
                //            SysPara.RightDoorAlarmEnable = false;
                //        }
                //    }
                //}
                #region scan feeder error code
                //if (IB_ST03600.On()) { SysPara.NPShowAlarm("03600"); }
                //if (IB_ST03601.On()) { SysPara.NPShowAlarm("03601"); }
                //if (IB_ST03602.On()) { SysPara.NPShowAlarm("03602"); }
                //if (IB_ST03603.On()) { SysPara.NPShowAlarm("03603"); }
                //if (IB_ST03604.On()) { SysPara.NPShowAlarm("03604"); }
                //if (IB_ST03605.On()) { SysPara.NPShowAlarm("03605"); }
                //if (IB_ST03606.On()) { SysPara.NPShowAlarm("03606"); }
                //if (IB_ST03607.On()) { SysPara.NPShowAlarm("03607"); }
                //if (IB_ST03608.On()) { SysPara.NPShowAlarm("03608"); }
                //if (IB_ST03609.On()) { SysPara.NPShowAlarm("03609"); }
                //if (IB_ST03610.On()) { SysPara.NPShowAlarm("03610"); }
                //if (IB_ST03611.On()) { SysPara.NPShowAlarm("03611"); }
                //if (IB_ST03612.On()) { SysPara.NPShowAlarm("03612"); }
                //if (IB_ST03613.On()) { SysPara.NPShowAlarm("03613"); }
                //if (IB_ST03614.On()) { SysPara.NPShowAlarm("03614"); }
                //if (IB_ST03615.On()) { SysPara.NPShowAlarm("03615"); }

                //if (IB_ST03700.On()) { SysPara.NPShowAlarm("03700"); }
                //if (IB_ST03701.On()) { SysPara.NPShowAlarm("03701"); }
                //if (IB_ST03702.On()) { SysPara.NPShowAlarm("03702"); }
                //if (IB_ST03703.On()) { SysPara.NPShowAlarm("03703"); }
                //if (IB_ST03704.On()) { SysPara.NPShowAlarm("03704"); }
                //if (IB_ST03705.On()) { SysPara.NPShowAlarm("03705"); }
                //if (IB_ST03706.On()) { SysPara.NPShowAlarm("03706"); }
                //if (IB_ST03707.On()) { SysPara.NPShowAlarm("03707"); }
                //if (IB_ST03708.On()) { SysPara.NPShowAlarm("03708"); }

                //if (IB_ST03805.On()) { SysPara.NPShowAlarm("03805"); }
                //if (IB_ST03806.On()) { SysPara.NPShowAlarm("03806"); }
                //if (IB_ST03807.On()) { SysPara.NPShowAlarm("03807"); }
                //if (IB_ST03808.On()) { SysPara.NPShowAlarm("03808"); }
                //#endregion
            }
        }

        private bool DectNetworkCable(string ip)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pr = ping.Send(ip);
                if (pr.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void tm_GantryWork_Tick(object sender, EventArgs e)
        {

            //if (MiddleLayer.SystemF.IB_ModeSwitch.Off())
            //{
            //    if (IB_RobotBusy.On())
            //    {
            //        if (MiddleLayer.SystemF.OB_LeftDoorCyUp.GetState())
            //        {
            //            MiddleLayer.SystemF.Cy_LeftDoorUnite.Off();
            //        }
            //        if (MiddleLayer.SystemF.OB_RightDoorCyUp.GetState())
            //        {
            //            MiddleLayer.SystemF.Cy_RightDoorUnite.Off();
            //        }
            //        if (!MiddleLayer.SystemF.IB_LeftDoorUpSensor.Off() || !MiddleLayer.SystemF.IB_RightDoorUpSensor.Off())
            //        {
            //            SysPara.NPShowAlarm("15010");
            //        }
            //    }
            //}            
        }

        private FCResultType flowChart95_FlowRun(object sender, EventArgs e)
        {
            gripper = "00";
            switch (Robot.ErorrCode)
            {
                case "E9000":
                    gripper = "01";
                    break;
                case "E9001":
                    gripper = "02";
                    break;
                case "E9002":
                    gripper = "03";
                    break;
                default:
                    break;
            }
            if (!GetRecipeValue("RSet", "NGbuffer1"))
            {
                SysPara.ngPos = "001";
            }
            else if (!GetRecipeValue("RSet", "NGbuffer2"))
            {
                SysPara.ngPos = "002";
            }
            else if (!GetRecipeValue("RSet", "NGbuffer3"))
            {
                SysPara.ngPos = "003";
            }
            else if (!GetRecipeValue("RSet", "NGbuffer4"))
            {
                SysPara.ngPos = "004";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer5"))
            {
                SysPara.ngPos = "005";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer6"))
            {
                SysPara.ngPos = "006";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer7"))
            {
                SysPara.ngPos = "007";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer8"))
            {
                SysPara.ngPos = "008";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer9"))
            {
                SysPara.ngPos = "009";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer10"))
            {
                SysPara.ngPos = "010";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer11"))
            {
                SysPara.ngPos = "011";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer12"))
            {
                SysPara.ngPos = "012";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer13"))
            {
                SysPara.ngPos = "013";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer14"))
            {
                SysPara.ngPos = "014";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer15"))
            {
                SysPara.ngPos = "015";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer16"))
            {
                SysPara.ngPos = "016";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer17"))
            {
                SysPara.ngPos = "017";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer18"))
            {
                SysPara.ngPos = "018";
                return FCResultType.NEXT;
            }
            else
            {
                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "NG had full", SysPara.bEnableGeneralSaveLog);
                SysPara.NPShowAlarm("0006");
                reintento.Dispose();
                reintento = new Reintento();
                reintento.fnSetTextMessageNShow("NG tray is full!!! sure has been replaced?", true, false, true);
                flowChart72.Enabled = true;
                flowChart72.BackColor = Color.Red;
                if (reintento.dResult == DialogResult.Yes)
                {
                    DataTable dt = RecipeData.Tables["RSet"];
                    dt.Rows[0]["NGbuffer1"] = false;
                    dt.Rows[0]["NGbuffer2"] = false;
                    dt.Rows[0]["NGbuffer3"] = false;
                    dt.Rows[0]["NGbuffer4"] = false;
                    dt.Rows[0]["NGbuffer5"] = false;
                    dt.Rows[0]["NGbuffer6"] = false;
                    dt.Rows[0]["NGbuffer7"] = false;
                    dt.Rows[0]["NGbuffer8"] = false;
                    dt.Rows[0]["NGbuffer9"] = false;
                    dt.Rows[0]["NGbuffer10"] = false;
                    dt.Rows[0]["NGbuffer11"] = false;
                    dt.Rows[0]["NGbuffer12"] = false;
                    dt.Rows[0]["NGbuffer13"] = false;
                    dt.Rows[0]["NGbuffer14"] = false;
                    dt.Rows[0]["NGbuffer15"] = false;
                    dt.Rows[0]["NGbuffer16"] = false;
                    dt.Rows[0]["NGbuffer17"] = false;
                    dt.Rows[0]["NGbuffer18"] = false;
                    dt.AcceptChanges();
                    this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                    SysPara.ngPos = "001";
                    return FCResultType.NEXT;
                }
                else
                {
                    DelayMs(500);
                    return FCResultType.IDLE;
                }
                //return FCResultType.IDLE;
            }

            return FCResultType.NEXT;
        }

        private FCResultType flowChart96_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart100_FlowRun(object sender, EventArgs e)
        {
            if (Robot.WriteToEpson("017", SysPara.ngPos, "FF", 20000))
            {
                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing position cammer", SysPara.bEnableGeneralSaveLog);
                InitTM.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart101_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                OB_RobotLight.On();
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing  cammer position ok", SysPara.bEnableGeneralSaveLog); 
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[1], SysPara.bEnableGeneralSaveLog);
                return FCResultType.IDLE;
            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing  cammer position timeout", SysPara.bEnableGeneralSaveLog);

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart94_FlowRun(object sender, EventArgs e)
        {
            SysPara.visionPos = new VisionPos();
            try
            {
                double exposure = GetSettingValue("MSet", "NGCCDCheckExpTime");
                if (H1_NGCCDCheck.TakePicture(exposure))         //CCD take picture
                {

                    RefreshDifferentThreadUI(cogRecDisp_H5_Recipe, () =>
                    {
                        cogRecDisp_H5_Recipe.Image = H1_NGCCDCheck.InputImage;
                        cogRecDisp_H5_Recipe.Fit();
                    });

                    //if (!MiddleLayer.SystemF.GetSettingValue("PSet", "DisableVision"))
                    //{
                    H1_NGCCDCheck.RunTB();
                    if (Convert.ToBoolean(H1_NGCCDCheck.TB.Outputs["IsHaveProduct"].Value) == false)
                    {
                        MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot NG throwing position1 not have product", SysPara.bEnableGeneralSaveLog);
                        OB_RobotLight.Off();
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot NG throwing position1 have product!!!need clear", SysPara.bEnableGeneralSaveLog);

                        string bufferIndex = "NGbuffer1";
                        switch (SysPara.ngPos)
                        {
                            case "001":
                                bufferIndex = "NGbuffer1";
                                break;
                            case "002":
                                bufferIndex = "NGbuffer2";
                                break;
                            case "003":
                                bufferIndex = "NGbuffer3";
                                break;
                            case "004":
                                bufferIndex = "NGbuffer4";
                                break;
                            case "005":
                                bufferIndex = "NGbuffer5";
                                break;
                            case "006":
                                bufferIndex = "NGbuffer6";
                                break;
                            case "007":
                                bufferIndex = "NGbuffer7";
                                break;
                            case "008":
                                bufferIndex = "NGbuffer8";
                                break;
                            case "009":
                                bufferIndex = "NGbuffer9";
                                break;
                            case "010":
                                bufferIndex = "NGbuffer10";
                                break;
                            case "011":
                                bufferIndex = "NGbuffer11";
                                break;
                            case "012":
                                bufferIndex = "NGbuffer12";
                                break;
                            case "013":
                                bufferIndex = "NGbuffer13";
                                break;
                            case "014":
                                bufferIndex = "NGbuffer14";
                                break;
                            case "015":
                                bufferIndex = "NGbuffer15";
                                break;
                            case "016":
                                bufferIndex = "NGbuffer16";
                                break;
                            case "017":
                                bufferIndex = "NGbuffer17";
                                break;
                            case "018":
                                bufferIndex = "NGbuffer18";
                                break;
                            default:
                                break;
                        }
                        DataTable dt = RecipeData.Tables["RSet"];
                        dt.Rows[0][bufferIndex] = true;
                        dt.AcceptChanges();
                        this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                        return FCResultType.CASE1;
                    }
                }
                else
                {
                    CCDAlogrithmStatus = false;

                    SysPara.NPShowAlarm("1204");        //CCD fail
                    return FCResultType.CASE1;
                }
            }
            catch
            {
                CCDAlogrithmStatus = false;

                SysPara.NPShowAlarm("1204");        //CCD fail
                return FCResultType.CASE1;
            }
        }

        private FCResultType flowChart97_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "NG Position Assemb");


            //Log.log.Write("Command：" + pos[2]
            //    + "Pos：" + ngPos
            //    + "AppIndex：" + string.Format("{0:d2}", InvertOrder(SysPara.Que_ProductCode_PickPos.Count)), Color.Black);
            if (Robot.WriteToEpson(pos[2], SysPara.ngPos, gripper, 30000))
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2]
                + "Pos：" + SysPara.ngPos
                + "AppIndex：" + gripper, SysPara.bEnableGeneralSaveLog);
                InitTM.Restart();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart98_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1) { return FCResultType.NEXT; }
            else if (ret == 0)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);

                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout

            else { return FCResultType.IDLE; }
        }

        private FCResultType flowChart99_FlowRun(object sender, EventArgs e)
        {
            string bufferIndex = "NGbuffer1";
            switch (SysPara.ngPos)
            {
                case "001":
                    bufferIndex = "NGbuffer1";
                    break;
                case "002":
                    bufferIndex = "NGbuffer2";
                    break;
                case "003":
                    bufferIndex = "NGbuffer3";
                    break;
                case "004":
                    bufferIndex = "NGbuffer4";
                    break;
                case "005":
                    bufferIndex = "NGbuffer5";
                    break;
                case "006":
                    bufferIndex = "NGbuffer6";
                    break;
                case "007":
                    bufferIndex = "NGbuffer7";
                    break;
                case "008":
                    bufferIndex = "NGbuffer8";
                    break;
                case "009":
                    bufferIndex = "NGbuffer9";
                    break;
                case "010":
                    bufferIndex = "NGbuffer10";
                    break;
                case "011":
                    bufferIndex = "NGbuffer11";
                    break;
                case "012":
                    bufferIndex = "NGbuffer12";
                    break;
                case "013":
                    bufferIndex = "NGbuffer13";
                    break;
                case "014":
                    bufferIndex = "NGbuffer14";
                    break;
                case "015":
                    bufferIndex = "NGbuffer15";
                    break;
                case "016":
                    bufferIndex = "NGbuffer16";
                    break;
                case "017":
                    bufferIndex = "NGbuffer17";
                    break;
                case "018":
                    bufferIndex = "NGbuffer18";
                    break;
                default:
                    break;
            }
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0][bufferIndex] = true;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            return FCResultType.NEXT;
        }

        private FCResultType flowChart102_FlowRun(object sender, EventArgs e)
        {
            if (IB_FeederInPlace.Off())
            {
                OB_FeederCompleted.Off();
                MiddleLayer.LogF.AddLog(LogType.Production, "OB_FeederCompleted=false", true);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["PaaletProductCount"] = (Int16)numericUpDown8.Value;
            dt.AcceptChanges();
        }

        private FCResultType flowChart103_FlowRun(object sender, EventArgs e)
        {
            scanCount = 0;
            int ScanProductCount = 4;
            if (GetSettingValue("MSet", "EPlaningCounts"))
            {
                int Planing = GetSettingValue("MSet", "ProQuantity");
                int finished = GetRecipeValue("RSet", "finishedCounts");
                if ((Planing - finished) < 12)
                {
                    if ((Planing - finished) % 3 != 0)
                        ScanProductCount = (Planing - finished) / 3 + 1;
                    else
                        ScanProductCount = (Planing - finished) / 3;
                }

            }

            return FCResultType.NEXT;
            if (scanCount >= 4)
            {
                //if (SysPara.Que_pallet_Code.Count == 12)
                //{
                //    List<string> reversePart1CodeQue = SysPara.Que_pallet_Code.Skip(6).Take(3).ToList();
                //    List<string> reversePart2CodeQue = SysPara.Que_pallet_Code.Skip(9).Take(3).ToList();
                //    List<string> temp = SysPara.Que_pallet_Code.ToList();
                //    temp.RemoveRange(6, 6);
                //    temp.AddRange(reversePart2CodeQue);
                //    temp.AddRange(reversePart1CodeQue);
                //    SysPara.Que_pallet_Code = new Queue<string>(temp);
                //}

                scanCount = 0;

                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.CASE1;
            }
        }

        private FCResultType flowChart104_FlowRun(object sender, EventArgs e)
        {
            if (((Int16)GetRecipeValue("RSet", "PaaletProductCount") - SysPara.Que_Product_pallet_Code.Count) / 3 != 0)
            {
                curProductCount = 3;
            }
            else
            {
                curProductCount = 3;
                //curProductCount = ((Int16)GetRecipeValue("RSet", "PaaletProductCount") - SysPara.Que_Product_pallet_Code.Count);
            }

            foreach (var q in SysPara.Que_ProductCode_PickPos)
            {
                string position = "P2";
                MESLib.MesLog.AddInfo(position + " " +
                    q.PosIndex + " " + q.Code + " " + q.CCDResult);
            }

            return FCResultType.NEXT;
        }

        public int GetRAppIndex(int num)
        {
            switch (num % 3)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                default:
                    return 0;
            }
        }

        private void button27_Click_1(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["FeederPickIndex"] = 0;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
        }

        private FCResultType npFlowChart3_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10730", Timestamp_10730,
                    Timer_10730, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MiddleLayer.MainF.btnStop_Click(null, null);
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart4_FlowRun(object sender, EventArgs e)
        {
            if (MESLib.CommParas.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.LGIT_MULTI_MATERIALID_CONFIRM_OK
                    || MESLib.CommParas.MesManager.LGIT_MULTI_MATERIALID_FAIL_OK)
                {
                    if (MESLib.CommParas.MesManager.LGIT_MULTI_MATERIALID_CONFIRM_OK)
                    {
                        // get PROCID
                        JObject jobj = JObject.Parse(MESLib.CommParas.MesManager.LGIT_MULTI_MATERIALID_CONFIRM_STR)["MATERIALLIST"].Value<JObject>();
                        foreach (var p in jobj.Properties())
                        {
                            var name = p.Name;
                            var value = JObject.Parse(p.Value.ToString());
                            MESLib.CommParas.MesManager.PROCID = value["PROCID"].Value<string>();
                            if (MESLib.CommParas.MesManager.PROCID.Trim() != "")
                                break;
                        }
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        //SysPara.NPShowAlarm("7020");
                        ////< A7020 Type = "E" Content = "MES GET LGIT_FAIL" DoStop = "True" />
                        //return FCResultType.IDLE;
                        string failinfo = MESLib.CommParas.MesManager.LGIT_MULTI_MATERIALID_FAIL_STR;


                        string[] msg = new string[2];

                        try
                        {
                            JObject jobj = JObject.Parse(failinfo);
                            msg[0] = jobj["EQPID"].Value<string>();
                            msg[1] = jobj["TEXT"].Value<string>();
                        }
                        catch
                        {
                            msg[0] = "NO EQPID";
                            msg[1] = "NO TEXT";
                        }
                        //DisposeFrm(FrmServerMessage);
                        SysPara.NPShowAlarm("70100");
                        MESLib.Frms.FrmServerMessage FrmServerMessage = new MESLib.Frms.FrmServerMessage(msg[0], msg[1]);
                        FrmServerMessage.fnSetTextMessageNShow();
                        if (FrmServerMessage.dResult == DialogResult.Yes)
                        {
                            return FCResultType.CASE1;
                        }
                        else
                        {
                            MESLib.CommParas.MesCenter.StopRunning();
                            return FCResultType.IDLE;
                        }
                    }
                }
                else
                {
                    if (Timer_LGIT_MULTI_MATERIALID_CONFIRM.On(60000))
                    {
                        // START OUTTIME
                        MESLib.CommParas.MesManager.MES_S6F11_11002("2");
                        SysPara.NPShowAlarm("7023");
                        Timer_LGIT_MULTI_MATERIALID_CONFIRM.Restart();
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                    return FCResultType.IDLE;
                }
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart12_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                Timestamp_10401 = DateTime.Now;
                Timer_10401.Restart();
                MESLib.CommParas.MesManager.MES_S6F11_10401(((byte)MESLib.ProcessState.EXECUTING));
                return FCResultType.NEXT;
            }
            return FCResultType.NEXT;
        }

        DateTime Timestamp_10731 = DateTime.Now;
        JTimer Timer_10731 = new JTimer();

        private FCResultType npFlowChart6_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10731", Timestamp_10731,
                    Timer_10731, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }
        DateTime Timestamp_10701 = DateTime.Now;
        JTimer Timer_10701 = new JTimer();
        JTimer Timer_PP_SELECT_OUTTIME = new JTimer();
        JTimer Timer_LOT_START_OUTTIME = new JTimer();
        private FCResultType npFlowChart7_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                var recipestr = SysPara.RecipeName;

                MESLib.CommParas.MesManager.LGIT_PP_SELECT_OK = false;
                MESLib.CommParas.MesManager.LGIT_PP_UPLOAD_FAIL_OK = false;
                MESLib.CommParas.MesManager.LGIT_LOT_FAIL_OK = false;

                Timestamp_10701 = DateTime.Now;
                List<string> _Material_List = new List<string>();
                List<string> _Pocket_List = new List<string>();
                string _RecipeID = (string)MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");

                var keys = CurrentPCBList.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    int index = keys[i];
                    string materialid = CurrentPCBList[index].MaterialID;

                    _Material_List.Add(materialid);
                    _Pocket_List.Add(MESLib.CommParas.MesManager.POCKETIDs[index]);
                }
                //if (SysPara.MesDebug)
                //{
                MESLib.MesLog.AddInfo("10701");
                MESLib.MesLog.AddInfo(_Material_List._ToString());
                MESLib.MesLog.AddInfo(_Pocket_List._ToString());
                //return FCResultType.NEXT;
                //}
                // get pocketid  list and material id list
                MESLib.CommParas.MesManager.MES_S6F11_10701(_Pocket_List,
                    _Material_List,
                    MESLib.CommParas.MesManager.PRODID,
                    _RecipeID, "1.0.0");
                Timer_PP_SELECT_OUTTIME.Restart();
                Timer_10701.Restart();
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart8_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10701", Timestamp_10701,
                    Timer_10701, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        public string[] Get_LOT_FAIL_Message()
        {
            string[] msg = new string[] { "", "" };
            JObject lotsinfo = MESLib.CommParas.MesManager.LGIT_LOT_START_FAIL_DATA["LOTINFOLIST"].Value<JObject>();

            try
            {
                // module list
                foreach (var p in lotsinfo.Properties())
                {
                    var name = p.Name;
                    var value = JObject.Parse(p.Value.ToString());
                    msg[0] = value["MODULEID"].Value<string>();
                    break;
                }

            }
            catch (Exception ex)
            {
                msg[0] = ex.Message;
            }
            // get text
            try
            {
                msg[1] = MESLib.CommParas.MesManager.LGIT_LOT_START_FAIL_DATA["TEXT"].Value<string>();
            }
            catch (Exception ex)
            {
                msg[1] = ex.Message;
            }
            return msg;
        }

        private FCResultType npFlowChart9_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    if (MessageBox.Show("Product OK?", "info", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {

                        return FCResultType.NEXT;
                    }
                    else
                    {
                        MESLib.CommParas.MesManager.LGIT_LOT_FAIL_OK = true;
                    }
                }
                if (MESLib.CommParas.MesManager.LGIT_PP_SELECT_OK
                    || MESLib.CommParas.MesManager.LGIT_PP_UPLOAD_FAIL_OK
                    || MESLib.CommParas.MesManager.LGIT_LOT_FAIL_OK)
                {
                    if (MESLib.CommParas.MesManager.LGIT_PP_SELECT_OK)
                    {
                        var objstr = JObject.Parse(MESLib.CommParas.MesManager.LGIT_PP_SELECT_STR);

                        foreach (var p in objstr["LOTINFOLIST"].Value<JObject>().Properties())
                        {
                            string name = p.Name;
                            JObject jobj = JObject.Parse(p.Value.ToString());

                            string lotid = jobj["LOTID"].Value<string>();
                            string moduleid = jobj["MODULEID"].Value<string>();
                            string procid = jobj["PROCID"].Value<string>();
                            string pordid = jobj["PRODID"].Value<string>();

                            int index = -1;
                            // find the index
                            foreach (var pair in CurrentPCBList)
                            {
                                int key = pair.Key;
                                var value = pair.Value;
                                string _lotid = value.MaterialID;
                                if (lotid == _lotid)
                                {
                                    index = key;
                                    break;
                                }
                            }

                            if (index == -1)
                            {
                                // Alarm
                                SysPara.NPShowAlarm("70104"); // report and RCMD not match
                                MESLib.CommParas.MesCenter.PauseRunning();
                            }
                            // fill MesManager Info: lotid, moduleid
                            MESLib.CommParas.MesManager.LOTIDs[index] = lotid;
                            MESLib.CommParas.MesManager.MODULEIDs[index] = moduleid;
                        }

                        //MESLib.CommParas.MesManager.MES_S6F11_10401((byte)MESLib.ProcessState.SETUP);
                        //MESLib.CommParas.MesManager.MES_S6F11_10401((byte)MESLib.ProcessState.READY);

                        return FCResultType.NEXT;
                    }
                    else if (MESLib.CommParas.MesManager.LGIT_PP_UPLOAD_FAIL_OK)
                    {
                        SysPara.NPShowAlarm("70106");
                        //////< A7020 Type = "E" Content = "MES GET LGIT_FAIL" DoStop = "True" />
                        ////return FCResultType.IDLE;
                        //JObject failinfo = JObject.Parse(MESLib.CommParas.MesManager.LGIT_PP_UPLOAD_FAIL_STR);
                        //string[] msg = new string[2];
                        //try
                        //{
                        //    msg[0] = failinfo["EQPID"].Value<string>();
                        //    msg[1] = failinfo["TEXT"].Value<string>();
                        //}
                        //catch
                        //{
                        //    msg[0] = "EQPID";
                        //    msg[1] = "LGIT_PP_UPLOAD_FAIL: REASON UNKNOWN";
                        //}
                        ////DisposeFrm(FrmServerMessage);

                        //MESLib.Frms.FrmServerMessage FrmServerMessage = new MESLib.Frms.FrmServerMessage(msg[0], msg[1]);
                        //FrmServerMessage.fnSetTextMessageNShow();
                        //if (FrmServerMessage.dResult == DialogResult.Yes)
                        //{
                        //    return FCResultType.CASE1;
                        //}
                        //else
                        //{
                        //    MESLib.CommParas.MesCenter.StopRunning();
                        //    return FCResultType.IDLE;
                        //}                
                        //SysPara.NPShowAlarm("7020");
                        ////< A7020 Type = "E" Content = "MES GET LGIT_FAIL" DoStop = "True" />
                        //return FCResultType.IDLE;
                        JObject failinfo = new JObject();
                        string[] msg = new string[2];
                        try
                        {
                            failinfo = JObject.Parse(MESLib.CommParas.MesManager.LGIT_PP_UPLOAD_FAIL_STR);
                            msg[0] = failinfo["EQPID"].Value<string>();
                            msg[1] = failinfo["TEXT"].Value<string>();

                        }
                        catch
                        {
                            msg[0] = "EQPID";
                            msg[1] = "LGIT_PP_UPLOAD_FAIL: REASON UNKNOWN";
                        }
                        //string _LOT_ID = "";
                        //try
                        //{
                        //    _LOT_ID =
                        //    failinfo["LOTINFOLIST"].Value<JObject>()["LOTINFO1"].Value<JObject>()["MODULEID"].Value<string>();
                        //}
                        //catch
                        //{
                        //    _LOT_ID = "No Lot Id";
                        //}
                        //DisposeFrm(FrmServerMessage);
                        foreach (var p in CurrentPCBList)
                        {
                            int index = p.Key;
                            //MOD@@ Guillermo Carrillo - guarda de indice: palletResultCopy[index]
                            if (index >= 0 && index < palletResultCopy.Count)
                            {
                                palletResultCopy[index] = true;
                            }
                        }
                        // remove all materials(as don't know which  one is fail
                        for (int i = 0; i < MaterialID_Array.Length; i++)
                        {
                            // remove from material id
                            MESLib.MesLog.AddInfo("Remove: " + MaterialID_Array[i]);
                            MaterialID_Array[i] = "";
                            SysPara.MESReportResult[i] = false; // mark for main workflow
                        }
                        MESLib.MesLog.AddInfo("----------------LGIT_PP_UPLOAD_FAIL BACK----------------");

                        MESCheckAllFail = true;
                        for (int i = 0; i < MaterialID_Array.Length; i++)
                        {
                            if (MaterialID_Array[i] != null &&
                                MaterialID_Array[i].Trim() != "" &&
                                MaterialID_Array[i].ToLower() != "error")
                            {
                                MESCheckAllFail = false;
                                break;
                            }
                        }
                        if (MESCheckAllFail)
                        {
                            RunTM.Restart();
                            MESLib.MesLog.AddInfo("----------------LGIT_PP_UPLOAD_FAIL All Fail----------------");
                            return FCResultType.CASE3;
                        }
                        return FCResultType.CASE2;



                        MESLib.Frms.FrmServerMessage FrmServerMessage = new MESLib.Frms.FrmServerMessage(
                            "LGIT_PP_UPLOAD_FAIL", "EQPID", msg[0], msg[1], "Retry", "CLOSE", "NG Buffer",
                            true, true, true);
                        FrmServerMessage.fnSetTextMessageNShow();
                        if (FrmServerMessage.dResult == DialogResult.Yes)
                        {
                            return FCResultType.CASE1;
                        }
                        else if (FrmServerMessage.dResult == DialogResult.No)
                        {
                            MESLib.CommParas.MesCenter.StopRunning();
                            return FCResultType.IDLE;
                        }
                        else if (FrmServerMessage.dResult == DialogResult.Abort)
                        {
                            // recover the palletResultCopy via current pcb list
                            foreach (var p in CurrentPCBList)
                            {
                                int index = p.Key;
                                //MOD@@ Guillermo Carrillo - guarda de indice: palletResultCopy[index]
                                if (index >= 0 && index < palletResultCopy.Count)
                                {
                                    palletResultCopy[index] = true;
                                }
                            }
                            // remove all materials(as don't know which  one is fail
                            for (int i = 0; i < MaterialID_Array.Length; i++)
                            {
                                // remove from material id
                                MESLib.MesLog.AddInfo("Remove: " + MaterialID_Array[i]);
                                MaterialID_Array[i] = "";
                                SysPara.MESReportResult[i] = false; // mark for main workflow
                            }
                            MESLib.MesLog.AddInfo("----------------LGIT_PP_UPLOAD_FAIL BACK----------------");

                            MESCheckAllFail = true;
                            for (int i = 0; i < MaterialID_Array.Length; i++)
                            {
                                if (MaterialID_Array[i] != null &&
                                    MaterialID_Array[i].Trim() != "" &&
                                    MaterialID_Array[i].ToLower() != "error")
                                {
                                    MESCheckAllFail = false;
                                    break;
                                }
                            }
                            if (MESCheckAllFail)
                            {
                                MESLib.MesLog.AddInfo("----------------LGIT_PP_UPLOAD_FAIL All Fail----------------");
                                return FCResultType.CASE3;
                            }
                            return FCResultType.CASE2;
                        }
                    }
                    else if (MESLib.CommParas.MesManager.LGIT_LOT_FAIL_OK)
                    {
                        SysPara.NPShowAlarm("70106");
                        ////< A7020 Type = "E" Content = "MES GET LGIT_FAIL" DoStop = "True" />
                        //return FCResultType.IDLE;
                        JObject failinfo = new JObject();
                        string[] msg = new string[2];
                        try
                        {
                            failinfo = JObject.Parse(MESLib.CommParas.MesManager.LGIT_LOT_FAIL_STR);
                            msg[0] = failinfo["EQPID"].Value<string>();
                            msg[1] = failinfo["TEXT"].Value<string>();

                        }
                        catch
                        {
                            msg[0] = "EQPID";
                            msg[1] = "LGIT_LOT_ID_FAIL: REASON UNKNOWN";
                        }
                        string _LOT_ID = "";
                        try
                        {
                            _LOT_ID =
                            failinfo["LOTINFOLIST"].Value<JObject>()["LOTINFO1"].Value<JObject>()["MODULEID"].Value<string>();
                        }
                        catch
                        {
                            _LOT_ID = "No Lot Id";
                        }
                        //DisposeFrm(FrmServerMessage);
                        foreach (var p in CurrentPCBList)
                        {
                            int index = p.Key;
                            //MOD@@ Guillermo Carrillo - guarda de indice: palletResultCopy[index]
                            if (index >= 0 && index < palletResultCopy.Count)
                            {
                                palletResultCopy[index] = true;
                            }
                        }
                        // change materiallist
                        for (int i = 0; i < MaterialID_Array.Length; i++)
                        {
                            if (MaterialID_Array[i] == _LOT_ID)
                            {
                                // remove from material id
                                MESLib.MesLog.AddInfo("Remove: " + MaterialID_Array[i]);
                                MaterialID_Array[i] = "";
                                SysPara.MESReportResult[i] = false; // mark for main workflow
                            }
                        }
                        MESLib.MesLog.AddInfo("----------------LOT_ID_FAIL BACK----------------");

                        MESCheckAllFail = true;
                        for (int i = 0; i < MaterialID_Array.Length; i++)
                        {
                            if (MaterialID_Array[i] != null &&
                                MaterialID_Array[i].Trim() != "" &&
                                MaterialID_Array[i].ToLower() != "error")
                            {
                                MESCheckAllFail = false;
                                break;
                            }
                        }
                        if (MESCheckAllFail)
                        {
                            MESLib.MesLog.AddInfo("----------------LOT_ID_FAIL All Fail----------------");
                            return FCResultType.CASE3;
                        }
                        return FCResultType.CASE2;

                        MESLib.Frms.FrmServerMessage FrmServerMessage = new MESLib.Frms.FrmServerMessage(
                            "LGIT_LOT_ID_FAIL", "LOT ID", _LOT_ID, msg[1], "Retry", "CLOSE", "NG Buffer",
                            true, true, true);
                        FrmServerMessage.fnSetTextMessageNShow();
                        if (FrmServerMessage.dResult == DialogResult.Yes)
                        {
                            return FCResultType.CASE1;
                        }
                        else if (FrmServerMessage.dResult == DialogResult.No)
                        {
                            MESLib.CommParas.MesCenter.StopRunning();
                            return FCResultType.IDLE;
                        }
                        else if (FrmServerMessage.dResult == DialogResult.Abort)
                        {
                            // recover the palletResultCopy via current pcb list
                            foreach (var p in CurrentPCBList)
                            {
                                int index = p.Key;
                                //MOD@@ Guillermo Carrillo - guarda de indice: palletResultCopy[index]
                                if (index >= 0 && index < palletResultCopy.Count)
                                {
                                    palletResultCopy[index] = true;
                                }
                            }
                            // change materiallist
                            for (int i = 0; i < MaterialID_Array.Length; i++)
                            {
                                if (MaterialID_Array[i] == _LOT_ID)
                                {
                                    // remove from material id
                                    MESLib.MesLog.AddInfo("Remove: " + MaterialID_Array[i]);
                                    MaterialID_Array[i] = "";
                                    SysPara.MESReportResult[i] = false; // mark for main workflow
                                }
                            }
                            MESLib.MesLog.AddInfo("----------------LOT_ID_FAIL BACK----------------");

                            MESCheckAllFail = true;
                            for (int i = 0; i < MaterialID_Array.Length; i++)
                            {
                                if (MaterialID_Array[i] != null &&
                                    MaterialID_Array[i].Trim() != "" &&
                                    MaterialID_Array[i].ToLower() != "error")
                                {
                                    MESCheckAllFail = false;
                                    break;
                                }
                            }
                            if (MESCheckAllFail)
                            {
                                MESLib.MesLog.AddInfo("----------------LOT_ID_FAIL All Fail----------------");
                                return FCResultType.CASE3;
                            }
                            return FCResultType.CASE2;
                        }
                    }
                }
                else
                {
                    if (Timer_PP_SELECT_OUTTIME.On(60000))
                    {
                        // START OUTTIME
                        MESLib.CommParas.MesManager.MES_S6F11_11002("2");
                        SysPara.NPShowAlarm("7023");
                        Timer_PP_SELECT_OUTTIME.Restart();
                        return FCResultType.IDLE;
                    }
                    return FCResultType.IDLE;
                }
            }
            return FCResultType.NEXT;
        }

        DateTime Timestamp_10704 = DateTime.Now;
        JTimer Timer_10704 = new JTimer();
        private FCResultType npFlowChart10_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                var recipestr = SysPara.RecipeName;

                try
                {
                    //List<string> _LotID_List = new List<string>();
                    List<string> _Pocket_List = new List<string>();
                    //List<string> _Module_List = new List<string>();
                    List<string> _Material_List = new List<string>();

                    var keys = CurrentPCBList.Keys.ToList();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        int index = keys[i];

                        _Pocket_List.Add(MESLib.CommParas.MesManager.POCKETIDs[index]);
                        _Material_List.Add(CurrentPCBList[index].MaterialID);
                    }
                    string _RecipeID = (string)MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");

                    Timestamp_10704 = DateTime.Now;
                    Timer_10704.Restart();
                    MESLib.CommParas.MesManager.LGIT_LOT_FAIL_OK = false;
                    MESLib.CommParas.MesManager.LGIT_LOT_START_OK = false;

                    MESLib.CommParas.MesManager.MES_S6F11_10704(_Material_List,
                        _Pocket_List, _Material_List,
                        MESLib.CommParas.MesManager.PROCID, MESLib.CommParas.MesManager.PRODID,
                        _RecipeID, "1.0.0");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("10704 Error: " + ex.Message);
                    throw new Exception("10704 Error: " + ex.Message);

                }
            }

            return FCResultType.NEXT;
        }
        JTimer Timer_LGIT_LOT_FAIL = new JTimer();

        private FCResultType npFlowChart11_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10704", Timestamp_10704,
                    Timer_10704, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }
        DateTime Timestamp_10401 = DateTime.Now;
        JTimer Timer_10401 = new JTimer();
        private FCResultType npFlowChart12_1FlowRun(object sender, EventArgs e)
        {

            if (SysPara.EnableMes)
            {
                Timestamp_10401 = DateTime.Now;
                Timer_10401.Restart();
                MESLib.CommParas.MesManager.MES_S6F11_10401(((byte)MESLib.ProcessState.EXECUTING));
                return FCResultType.NEXT;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart13_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10401", Timestamp_10401,
                    Timer_10401, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        RunTM.Restart();
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            RunTM.Restart();
            return FCResultType.NEXT;
        }

        DateTime Timestamp_10710 = DateTime.Now;
        JTimer Timer_10710 = new JTimer();
        private FCResultType npFlowChart15_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (MESCheckAllFail)
                    return FCResultType.NEXT;
                if (CurrentPCBList.Count == 0)
                    return FCResultType.NEXT;
                Timestamp_10710 = DateTime.Now;

                MESLib.MesLog.AddInfo("10710");

                List<bool> _Result_List = new List<bool>();
                List<string> _DefectCode_List = new List<string>();
                List<string> _Carrier_List = new List<string>();


                List<string> _Pocket_List = new List<string>();
                List<string> _Module_List = new List<string>();
                List<string> _LotID_List = new List<string>();

                List<string> _MaterialID_List = new List<string>();

                var keys = CurrentPCBList.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    int index = keys[i];

                    string lotid = MESLib.CommParas.MesManager.LOTIDs[index];
                    string pocketid = MESLib.CommParas.MesManager.POCKETIDs[index];
                    string moduleid = MESLib.CommParas.MesManager.MODULEIDs[index];
                    string materialid = CurrentPCBList[index].MaterialID;
                    bool result = true;
                    string defectcode = "";
                    if (CurrentPCBList[index].Result_A == "NG")
                    {
                        MESLib.MesLog.AddInfo(string.Format("Index: {0}, A Fail", index));
                        result = false;
                        defectcode = "1";
                    }
                    if (CurrentPCBList[index].Result_B == "NG")
                    {
                        MESLib.MesLog.AddInfo(string.Format("Index: {0}, B Fail", index));
                        result = false;
                        defectcode = "2";
                    }

                    _Pocket_List.Add(pocketid);
                    _Module_List.Add(moduleid);
                    _LotID_List.Add(lotid);

                    _Result_List.Add(result);
                    _DefectCode_List.Add(defectcode);

                    _MaterialID_List.Add(materialid);

                    _Carrier_List.Add(MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[index]);

                    // clear material list
                    for (int j = 0; j < MaterialID_Array_Copy.Length; j++)
                    {
                        if (MaterialID_Array_Copy[j] == materialid)
                        {
                            MaterialID_Array_Copy[j] = "";
                        }
                    }
                }
                string _RecipeID = (string)MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");

                MESLib.MesLog.AddInfo(_LotID_List._ToString());
                MESLib.MesLog.AddInfo(_Pocket_List._ToString());
                MESLib.MesLog.AddInfo(_Result_List._ToString());


                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                MESLib.CommParas.MesManager.MES_S6F11_10710(_LotID_List, _Pocket_List, _Module_List,
                    _LotID_List, _Pocket_List, _Carrier_List, _Module_List,
                    MESLib.CommParas.MesManager.PROCID, MESLib.CommParas.MesManager.PRODID,
                    _Result_List, _DefectCode_List,
                    _Carrier_List, "24",
                    _RecipeID, "1.0.0");
                Timer_10701.Restart();
            }

            Timer_10710.Restart();
            return FCResultType.NEXT;

        }

        private FCResultType npFlowChart14_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (MESCheckAllFail)
                    return FCResultType.NEXT;
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10710", Timestamp_10710,
                    Timer_10710, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart16_FlowRun(object sender, EventArgs e)
        {
            // remove record which was done from material list
            if (CurrentPCBList != null)
            {
                foreach (var p in CurrentPCBList)
                {
                    var index = p.Key;
                    var value = p.Value;
                    for (int i = 0; i < MaterialID_Array_Copy.Length; i++)
                    {
                        if (value.MaterialID == MaterialID_Array_Copy[i])
                        {
                            MaterialID_Array_Copy[i] = "";
                            break;
                        }
                    }
                }
            }
            if (SysPara.EnableMes)
            {
                if (MESCheckAllFail)
                    return FCResultType.NEXT;
                bool pallectallset = true;

                for (int i = 0; i < palletResultCopy.Count; i++)
                {
                    if (palletResultCopy[i])
                    {
                        pallectallset = false;
                        break;
                    }
                }

                // check setting
                if (GetSettingValue("MSet", "EPlaningCounts"))
                {

                    int Planing = GetSettingValue("MSet", "ProQuantity");
                    int finished = GetRecipeValue("RSet", "finishedCounts");

                    if (Planing <= finished)
                    {
                        pallectallset = true;
                    }
                }

                if (SysPara.MesDebug)
                {
                    if (pallectallset)
                    {
                        MESLib.MesLog.AddInfo("Pallet finished");
                    }
                    return FCResultType.NEXT;
                }

                if (pallectallset)
                {

                    Timestamp_10401 = DateTime.Now;
                    Timer_10401.Restart();
                    MESLib.CommParas.MesManager.MES_S6F11_10401(((byte)MESLib.ProcessState.IDLE));
                    return FCResultType.NEXT;
                }
            }

            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart17_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (MESCheckAllFail)
                    return FCResultType.NEXT;
                if (SysPara.MesDebug)
                {
                    if (SysPara.WaitNextPallet)
                    {
                        MESLib.MesLog.AddInfo("Wait Next Pallect, Material Left:");
                        MESLib.MesLog.AddInfo(MaterialID_Array_Copy._ToString());
                        return FCResultType.CASE2;
                    }
                    MESLib.MesLog.AddInfo("Get New Material, Material Left:");
                    MESLib.MesLog.AddInfo(MaterialID_Array_Copy._ToString());
                    return FCResultType.NEXT;
                }
                bool pallectallset = true;
                for (int i = 0; i < palletResultCopy.Count; i++)
                {
                    if (palletResultCopy[i])
                    {
                        pallectallset = false;
                        break;
                    }
                }
                if (pallectallset)
                {
                    if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10401", Timestamp_10401,
                           Timer_10401, out int ACK, out string data, out bool timeout))
                    {
                        if (ACK == 0)
                        {
                            MESLib.MesLog.AddInfo($"Complete Report End - {DateTime.Now.ToString("HH:mm:ss:fff")}");
                            if (SysPara.WaitNextPallet)
                                return FCResultType.CASE2;
                            return FCResultType.NEXT;
                        }
                        else
                        {
                            SysPara.NPShowAlarm("70100");
                            return FCResultType.IDLE;
                        }
                    }
                    else
                    {
                        if (timeout)
                        {
                            // T3 Fail
                            MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                            SysPara.NPShowAlarm("70102");
                            MESLib.CommParas.MesCenter.StopRunning();
                            return FCResultType.IDLE;
                        }
                    }
                    return FCResultType.IDLE;
                }
            }
            MESLib.MesLog.AddInfo($"Complete Report End - {DateTime.Now.ToString("HH:mm:ss:fff")}");
            if (SysPara.WaitNextPallet)
                return FCResultType.CASE2;
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart18_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.LGIT_LOT_START_OK || MESLib.CommParas.MesManager.LGIT_LOT_FAIL_OK)
                {
                    if (MESLib.CommParas.MesManager.LGIT_LOT_START_OK)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        //SysPara.NPShowAlarm("7020");
                        ////< A7020 Type = "E" Content = "MES GET LGIT_FAIL" DoStop = "True" />
                        //return FCResultType.IDLE;
                        //string[] msg = Get_LOT_FAIL_Message();//----0711
                        //DisposeFrm(FrmServerMessage);

                        //MESLib.Frms.FrmServerMessage FrmServerMessage = new MESLib.Frms.FrmServerMessage(msg[0], msg[1]);//----0711
                        //FrmServerMessage.fnSetTextMessageNShow();//----0711
                        //if (FrmServerMessage.dResult == DialogResult.Yes)//----0711
                        //{//----0711
                        return FCResultType.CASE1;
                        //}//----0711
                        //else//----0711
                        //{//----0711
                        //    MiddleLayer.MainF.btnStop_Click(null, null);//----0711
                        //    return FCResultType.IDLE;//----0711
                        //}//----0711
                    }
                }
                if (Timer_LGIT_LOT_FAIL.On(1000))
                    return FCResultType.NEXT;
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;

        }

        private FCResultType npFlowChart24_FlowRun(object sender, EventArgs e)
        {
            MESLib.CommFuns.ArrayDefault(MaterialID_Array);
            SysPara.EnableMes = MiddleLayer.MesF2.GetSettingValue("PSet", "Enable_Mes");
            MESLib.CommParas.EnableMes = SysPara.EnableMes;
            if (!MESLib.CommParas.MesManager.MesInitE)
            {
                MESLib.CommParas.MesManager.MesInit();
                MESLib.CommParas.MesManager.MesInitE = true;
            }
            MESLib.CommParas.MesManager.AllRecipes = new List<string>() { "HPCB", "VPCB", "HPCB_OP", "VPCB_OP" };
            string EQPName = MiddleLayer.MesF2.GetSettingValue("PSet", "EQPName");
            //MESLib.CommParas.MesManager._connector.MDLN = "Install PCB #01";
            MESLib.CommParas.MesManager._connector.MDLN = EQPName;
            string _RecipeID = (string)MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");
            MESLib.CommParas.MesManager.RecipeSelected = _RecipeID;
            //f._connector.SV.CurrentPPID.content.EquipmentName = new VarItem<string>("10001", f._connector._Ec.EquipmentName_1.content.ToString());
            MESLib.CommParas.MesManager._connector.SV.CurrentPPID.content.CurrentPPID = new VarItem<string>("10135", _RecipeID);
            MESLib.CommParas.MesManager._connector.SV.CurrentPPID.content.CurrentPPIDVersion = new VarItem<string>("10136", "1.0.0");

            //f._connector.SV.CurrentPPIDList.content.EquipmentName = new VarItem<string>("10001", f._connector._Ec.EquipmentName_1.content.ToString());
            MESLib.CommParas.MesManager._connector.SV.CurrentPPIDList.content.CurrentPPID = new VarItem<string>("10135", _RecipeID);
            MESLib.CommParas.MesManager._connector.SV.CurrentPPIDList.content.CurrentPPIDVersion = new VarItem<string>("10136", "1.0.0");
            MESLib.CommParas.IP = MiddleLayer.MesF2.GetSettingValue("PSet", "Mes_IP");
            MESLib.CommParas.Port = MiddleLayer.MesF2.GetSettingValue("PSet", "Mes_Port");

            if (SysPara.EnableMes)
            {
                // secgem
                MESLib.CommFuns.StartSecsGem(out string error);
                if (error != string.Empty)
                {
                    SysPara.NPShowAlarm("70105"); // secgem open fail
                    MESLib.CommParas.MesCenter.PauseRunning();
                    return FCResultType.IDLE;
                }
                System.Threading.Thread.Sleep(1500);
                if (!MESLib.CommParas.MesManager.MesConnect(MESLib.CommParas.IP, MESLib.CommParas.Port))
                {
                    SysPara.NPShowAlarm("70109"); // secgem open fail
                    //System.Threading.Thread.Sleep(1000);
                    //MESLib.CommParas.MesManager.MesClose();
                    return FCResultType.IDLE;
                }

            }

            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart23_FlowRun(object sender, EventArgs e)
        {
            //MOD@@ Guillermo Carrillo - salto del reinicio rapido
            //  Evita que la cadena se atore en IDLE esperando OFFLINE_REASON_OK
            if (bRestartQuickActive) { return FCResultType.NEXT; }
            if (SysPara.EnableMes)
            {
                if (!MESLib.CommParas.MesManager.LGIT_SETCODE_OFFLINE_REASON_OK)
                {
                    return FCResultType.IDLE;
                }
            }
            return FCResultType.NEXT;

        }

        private FCResultType npFlowChart22_FlowRun(object sender, EventArgs e)
        {
            //MOD@@ Guillermo Carrillo - salto del reinicio rapido
            //  Evita que la cadena se atore en IDLE esperando IDLE_REASON_OK
            if (bRestartQuickActive) { return FCResultType.NEXT; }
            if (SysPara.EnableMes)
            {
                if (MESLib.CommParas.MesManager.LGIT_SETCODE_IDLE_REASON_OK)
                    return FCResultType.NEXT;
                else
                    return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart21_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (MESLib.CommParas.MesManager.LGIT_SETCODE_MODEL_LIST_OK && MESLib.CommParas.MesManager.LGIT_SETCODE_MODEL_LIST_UPDATED)
                {
                    //MESLib.CommFuns.UpdateModuleList();
                    // MainForm Render deal with this event
                }
                else
                {
                    return FCResultType.IDLE;
                }
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart20_FlowRun(object sender, EventArgs e)
        {
            //MOD@@ Guillermo Carrillo - salto del reinicio rapido
            //  Evita que la cadena se atore en IDLE esperando OP_CALL_OK
            if (bRestartQuickActive) { return FCResultType.NEXT; }
            if (SysPara.EnableMes)
            {
                if (MESLib.CommParas.MesManager.LGIT_OP_CALL_OK)
                    return FCResultType.NEXT;
                else
                    return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart19_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                MESLib.CommParas.MesManager.MES_S6F11_10104(MESLib.ControlState.Online_Remote);
            }
            return FCResultType.NEXT;
        }
        private FCResultType npFlowChart6_FlowRun_1(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                List<string> _Material_List = new List<string>();
                var keys = CurrentPCBList.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    int index = keys[i];
                    //string qrcode = CurrentPCBList[index].MaterialID;
                    string traycode = MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[index];

                    _Material_List.Add(traycode);
                }

                Timestamp_10731 = DateTime.Now;
                MESLib.CommParas.MesManager.MES_S6F11_10731(_Material_List,
                    MESLib.CommParas.MesManager.PRODID, MESLib.CommParas.MesManager.PROCID, "24");
                Timer_10731.Restart();
            }

            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart25_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10731", Timestamp_10731,
                    Timer_10731, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MiddleLayer.MainF.btnStop_Click(null, null);
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart5_FlowRun_1(object sender, EventArgs e)
        {
            return FCResultType.NEXT;

        }

        private FCResultType npFlowChart26_FlowRun(object sender, EventArgs e)
        {


            return FCResultType.NEXT;
        }


        DateTime Timestamp_10723 = DateTime.Now;
        JTimer Timer_10723 = new JTimer();
        private FCResultType npFlowChart5_FlowRun_2(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (MESCheckAllFail)
                    return FCResultType.NEXT;
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                // check ng or not
                bool ng = false;

                var keys = CurrentPCBList.Keys.ToList();

                for (int i = 0; i < keys.Count; i++)
                {
                    int key = keys[i];
                    if (CurrentPCBList[key].Result_A == "NG" ||
                        CurrentPCBList[key].Result_B == "NG")
                    {
                        ng = true;
                        break;
                    }
                }
                if (ng)
                {
                    List<string> _LotID_List = new List<string>();
                    List<string> _PocketID_List = new List<string>();
                    List<string> _ModuleID_List = new List<string>();

                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (CurrentPCBList[keys[i]].Result_A == "NG" ||
                            CurrentPCBList[keys[i]].Result_B == "NG")
                        {
                            int index = keys[i];

                            string lotid = MESLib.CommParas.MesManager.LOTIDs[index];
                            string pocketid = MESLib.CommParas.MesManager.POCKETIDs[index];
                            string moduleid = MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[index];

                            _LotID_List.Add(lotid);
                            _PocketID_List.Add(pocketid);
                            _ModuleID_List.Add(moduleid);
                        }

                    }
                    Timestamp_10723 = DateTime.Now;
                    Timer_10723.Restart();
                    MESLib.CommParas.MesManager.MES_S6F11_10723(_LotID_List,
                        _PocketID_List, _ModuleID_List,
                        MESLib.CommParas.MesManager.PROCID,
                        MESLib.CommParas.MesManager.PRODID);
                }

            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart26_FlowRun_1(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (MESCheckAllFail)
                    return FCResultType.NEXT;
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                // check ng or not
                bool ng = false;

                var keys = CurrentPCBList.Keys.ToList();

                for (int i = 0; i < keys.Count; i++)
                {
                    int key = keys[i];
                    if (CurrentPCBList[key].Result_A == "NG" ||
                        CurrentPCBList[key].Result_B == "NG")
                    {
                        ng = true;
                        break;
                    }
                }
                if (ng)
                {
                    if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10723", Timestamp_10723,
                      Timer_10723, out int ACK, out string data, out bool timeout))
                    {
                        if (ACK == 0)
                        {
                            return FCResultType.NEXT;
                        }
                        else
                        {
                            SysPara.NPShowAlarm("70100");
                            return FCResultType.IDLE;
                        }
                    }
                    else
                    {
                        if (timeout)
                        {
                            // T3 Fail
                            MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                            SysPara.NPShowAlarm("70102");
                            MESLib.CommParas.MesCenter.StopRunning();
                            return FCResultType.IDLE;
                        }
                    }
                    return FCResultType.IDLE;
                }

            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart27_FlowRun(object sender, EventArgs e)
        {
            if (MESLib.CommParas.EnableMes)
            {
                if (MESLib.CommParas.MesManager.PRODID.Trim() == "")
                {
                    SysPara.NPShowAlarm("70108"); return FCResultType.IDLE;
                }
            }

            return FCResultType.NEXT;
        }

        DateTime Timestamp_10702 = DateTime.Now;
        JTimer Timer_10702 = new JTimer();

        private FCResultType npFlowChart28_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                var recipestr = SysPara.RecipeName;

                try
                {
                    //List<string> _LotID_List = new List<string>();
                    List<string> _Pocket_List = new List<string>();
                    //List<string> _Module_List = new List<string>();
                    List<string> _Material_List = new List<string>();

                    var keys = CurrentPCBList.Keys.ToList();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        int index = keys[i];

                        _Pocket_List.Add(MESLib.CommParas.MesManager.POCKETIDs[index]);
                        _Material_List.Add(CurrentPCBList[index].MaterialID);
                    }
                    string _RecipeID = (string)MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");

                    Timestamp_10702 = DateTime.Now;
                    Timer_10702.Restart();
                    Timer_LOT_START_OUTTIME.Restart();
                    MESLib.CommParas.MesManager.LGIT_LOT_START_OK = false;
                    MESLib.CommParas.MesManager.LGIT_LOT_FAIL_OK = false;

                    MESLib.CommParas.MesManager.MES_S6F11_10702(_Material_List,
                        _Pocket_List, _Material_List,
                        MESLib.CommParas.MesManager.PROCID, MESLib.CommParas.MesManager.PRODID,
                        _RecipeID, "1.0.0");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("10702 Error: " + ex.Message);
                    //throw new Exception("10702 Error: " + ex.Message);

                }
            }

            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart29_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10702", Timestamp_10702,
                    Timer_10702, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart30_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.LGIT_LOT_START_OK || MESLib.CommParas.MesManager.LGIT_LOT_FAIL_OK)
                {
                    if (MESLib.CommParas.MesManager.LGIT_LOT_START_OK)
                    {
                        var objstr = JObject.Parse(MESLib.CommParas.MesManager.LGIT_PP_SELECT_STR);

                        foreach (var p in objstr["LOTINFOLIST"].Value<JObject>().Properties())
                        {
                            string name = p.Name;
                            JObject jobj = JObject.Parse(p.Value.ToString());

                            string lotid = jobj["LOTID"].Value<string>();
                            string moduleid = jobj["MODULEID"].Value<string>();
                            string procid = jobj["PROCID"].Value<string>();
                            string pordid = jobj["PRODID"].Value<string>();

                            int index = -1;
                            // find the index
                            foreach (var pair in CurrentPCBList)
                            {
                                int key = pair.Key;
                                var value = pair.Value;
                                string _lotid = value.MaterialID;
                                if (lotid == _lotid)
                                {
                                    index = key;
                                    break;
                                }
                            }

                            if (index == -1)
                            {
                                // Alarm
                                SysPara.NPShowAlarm("70104"); // report and RCMD not match
                                MESLib.CommParas.MesCenter.PauseRunning();
                            }
                            else if (MESLib.CommParas.MesManager.LOTIDs[index] != lotid ||
                                MESLib.CommParas.MesManager.MODULEIDs[index] != moduleid)
                            {
                                // Alarm
                                SysPara.NPShowAlarm("70104"); // report and RCMD not match
                                MESLib.CommParas.MesCenter.PauseRunning();
                            }
                        }

                        return FCResultType.NEXT;
                    }
                    else
                    {
                        //SysPara.NPShowAlarm("7020");
                        ////< A7020 Type = "E" Content = "MES GET LGIT_FAIL" DoStop = "True" />
                        //return FCResultType.IDLE;
                        JObject failinfo = JObject.Parse(MESLib.CommParas.MesManager.LGIT_LOT_FAIL_STR);
                        string[] msg = new string[2];
                        try
                        {
                            msg[0] = failinfo["EQPID"].Value<string>();
                            msg[1] = failinfo["TEXT"].Value<string>();
                        }
                        catch
                        {
                            msg[0] = "EQPID";
                            msg[1] = "LGIT_PP_UPLOAD_FAIL: REASON UNKNOWN";
                        }
                        //DisposeFrm(FrmServerMessage);
                        SysPara.NPShowAlarm("70100");
                        MESLib.Frms.FrmServerMessage FrmServerMessage = new MESLib.Frms.FrmServerMessage(msg[0], msg[1]);
                        FrmServerMessage.fnSetTextMessageNShow();
                        if (FrmServerMessage.dResult == DialogResult.Yes)
                        {
                            return FCResultType.CASE1;
                        }
                        else
                        {
                            MESLib.CommParas.MesCenter.StopRunning();
                            return FCResultType.IDLE;
                        }
                    }
                }
                else
                {
                    if (Timer_LOT_START_OUTTIME.On(60000))
                    {
                        // START OUTTIME
                        MESLib.CommParas.MesManager.MES_S6F11_11002("2");
                        SysPara.NPShowAlarm("7023");
                        Timer_LOT_START_OUTTIME.Restart();
                        return FCResultType.IDLE;
                    }
                    return FCResultType.IDLE;
                }
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart31_FlowRun(object sender, EventArgs e)
        {
            palletResultCopy.Clear();

            if (true)
            {
                // copy the vision results to a new list
                for (int i = 0; i < palletUpResult.Count; i++)
                {
                    palletResultCopy.Add(palletUpResult[i]);
                }

                // tray qr code
                MESLib.MesLog.AddInfo("----------------Prepare----------------");
                MESLib.MesLog.AddInfo("Tray QRCodes: " + MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs._ToString());
                MESLib.MesLog.AddInfo("PalletResultCopy: " + palletResultCopy._ToString<bool>());

            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart32_FlowRun(object sender, EventArgs e)
        {
            MESLib.MesLog.AddInfo($"Start Report Start - {DateTime.Now.ToString("HH:mm:ss:fff")}");

            for (int i = 0; i < SysPara.MESReportResult.Length; i++)
            {
                SysPara.MESReportResult[i] = true;
            }

            if (true)
            {
                MESCheckAllFail = false;
                CurrentPCBList.Clear();

                // compare material catched and the pallet results 0;
                // create current pcb list(used 10701->10710)
                for (int i = 0; i < MaterialID_Array.Length; i++)
                {
                    if (MaterialID_Array[i] != null &&
                        MaterialID_Array[i].Trim() != "" &&
                        MaterialID_Array[i].ToLower() != "error")
                    {
                        SysPara.MESReportResult[i] = true;
                        for (int j = 0; j < palletResultCopy.Count; j++)
                        {
                            if (palletResultCopy[j])
                            {
                                InstallPCBInfo ipi = new InstallPCBInfo();
                                ipi.MaterialID = MaterialID_Array[i];
                                ipi.Result_A = "";
                                ipi.Result_B = "";
                                CurrentPCBList.Add(j, ipi); // every material used, add to current pcb list
                                palletResultCopy[j] = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        SysPara.MESReportResult[i] = false;
                    }
                }

                MESLib.MesLog.AddInfo("----------------Reset Current PCB List----------------");
                MESLib.MesLog.AddInfo("TrayQRCodes:");
                MESLib.MesLog.AddInfo(MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs._ToString());
                MESLib.MesLog.AddInfo("Materials:");
                MESLib.MesLog.AddInfo(MaterialID_Array._ToString());
                MESLib.MesLog.AddInfo("PalletResultCopy:");
                MESLib.MesLog.AddInfo(palletResultCopy._ToString());
                MESLib.MesLog.AddInfo("CurrentPCBList:");
                MESLib.MesLog.AddInfo(CurrentPCBList._ToString());
            }
            return FCResultType.NEXT;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            DataTable DT = RecipeData.Tables["RSet"];
            DT.Rows[0]["NGbuffer1"] = false;
            DT.Rows[0]["NGbuffer2"] = false;
            DT.Rows[0]["NGbuffer3"] = false;
            DT.Rows[0]["NGbuffer4"] = false;
            DT.Rows[0]["NGbuffer5"] = false;
            DT.Rows[0]["NGbuffer6"] = false;
            DT.Rows[0]["NGbuffer7"] = false;
            DT.Rows[0]["NGbuffer8"] = false;
            DT.Rows[0]["NGbuffer9"] = false;
            DT.Rows[0]["NGbuffer10"] = false;
            DT.Rows[0]["NGbuffer11"] = false;
            DT.Rows[0]["NGbuffer12"] = false;
            DT.Rows[0]["NGbuffer13"] = false;
            DT.Rows[0]["NGbuffer14"] = false;
            DT.Rows[0]["NGbuffer15"] = false;
            DT.Rows[0]["NGbuffer16"] = false;
            DT.Rows[0]["NGbuffer17"] = false;
            DT.Rows[0]["NGbuffer18"] = false;
            DT.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));




        }

        private void GantryForm_Load(object sender, EventArgs e)
        {



        }

        private void button27_Click_2(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["FeederPickIndex"] = 0;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (button28.BackColor == Color.Green)
            {

                OB_RobotLight.Off();
                button28.BackColor = Color.Transparent;

            }

            else if (button28.BackColor == Color.Transparent)
            {
                OB_RobotLight.On();
                button28.BackColor = Color.Green;
            }




        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (button34.BackColor == Color.Green)
            {

                OB_RobotLight.Off();
                button34.BackColor = Color.Transparent;

            }

            else if (button34.BackColor == Color.Transparent)
            {
                OB_RobotLight.On();
                button34.BackColor = Color.Green;
            }
        }

        private void NGCCDCheckExpTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private FCResultType npFlowChart33_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                Timestamp_10401 = DateTime.Now;
                Timer_10401.Restart();
                MESLib.CommParas.MesManager.MES_S6F11_10401(((byte)MESLib.ProcessState.SETUP));
                return FCResultType.NEXT;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart34_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10401", Timestamp_10401,
                    Timer_10401, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart36_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10401", Timestamp_10401,
                    Timer_10401, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        SysPara.NPShowAlarm("70102");
                        MESLib.CommParas.MesCenter.StopRunning();
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart35_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                Timestamp_10401 = DateTime.Now;
                Timer_10401.Restart();
                MESLib.CommParas.MesManager.MES_S6F11_10401(((byte)MESLib.ProcessState.READY));
                return FCResultType.NEXT;
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart37_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.EnableMes)
            {
                if (!SysPara.R1ScanResult)
                    return FCResultType.NEXT;
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        DateTime Timestamp_10711 = DateTime.Now;
        JTimer Timer_10711 = new JTimer();
        private FCResultType npFlowChart37_FlowRun_1(object sender, EventArgs e)
        {
            MESLib.MesLog.AddInfo($"Complete Report Start - {DateTime.Now.ToString("HH:mm:ss:fff")}");
            if (SysPara.EnableMes)
            {
                if (MESCheckAllFail)
                    return FCResultType.NEXT;
                if (CurrentPCBList.Count == 0)
                    return FCResultType.NEXT;
                Timestamp_10711 = DateTime.Now;

                List<string> _Pocket_List = new List<string>();
                List<string> _Module_List = new List<string>();
                List<string> _LotID_List = new List<string>();

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                string filename = "";
                filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                string filepath = @"D:\GMES\APDLog";
                filepath = System.IO.Path.Combine(filepath, DateTime.Now.ToString("yyyyMMdd"));


                var keys = CurrentPCBList.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    int index = keys[i];

                    string lotid = MESLib.CommParas.MesManager.LOTIDs[index];
                    string pocketid = MESLib.CommParas.MesManager.POCKETIDs[index];
                    //string moduleid = MESLib.CommParas.MesManager.MODULEIDs[index];
                    string palletcode = MESLib.CommParas.MesManager.TRAY_POSITION_QRCODEs[index];

                    _Pocket_List.Add(pocketid);
                    _Module_List.Add(palletcode);
                    _LotID_List.Add(lotid);
                }
                MESLib.MesLog.AddInfo("10711");
                // check file path
                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory(filepath);
                }
                string csv_title = "Pallet_BCR,PCB_BCR,C/T";
                using (System.IO.StreamWriter sw = new StreamWriter(System.IO.Path.Combine(filepath, filename), true))
                {
                    sw.WriteLine(csv_title);
                }

                // create parameters
                string Pallet_BCR = "";
                string PCB_BCR = "";
                string ct = "";
                string _ct = (processData.swCycleTime.ElapsedMilliseconds / 1000.0).ToString("#.0");

                for (int i = 0; i < MESLib.CommParas.MesManager.POCKETIDs.Length; i++)
                {
                    string pocketid = MESLib.CommParas.MesManager.POCKETIDs[i];

                    if (!_Pocket_List.Contains(pocketid))
                    {
                        Pallet_BCR += "";
                        PCB_BCR += "";
                        ct += "";
                    }
                    else
                    {
                        int index = _Pocket_List.IndexOf(pocketid);
                        Pallet_BCR += _Module_List[index];
                        PCB_BCR += _LotID_List[index];
                        ct += _ct;
                        // csv file parameters                
                        using (System.IO.StreamWriter sw = new StreamWriter(System.IO.Path.Combine(filepath, filename), true))
                        {
                            sw.WriteLine(string.Format("{0},{1},{2}",
                                _Module_List[index],
                                _LotID_List[index],
                                _ct));
                        }
                    }
                    if (i != MESLib.CommParas.MesManager.POCKETIDs.Length - 1)
                    {
                        Pallet_BCR += ",";
                        PCB_BCR += ",";
                        ct += ",";
                    }
                }
                parameters.Add("Pallet_BCR", Pallet_BCR);
                parameters.Add("PCB_BCR", PCB_BCR);
                parameters.Add("C/T", _ct);

                Timestamp_10711 = DateTime.Now;
                Timer_10711.Restart();

                MESLib.MesLog.AddInfo("10711");
                MESLib.MesLog.AddInfo("_Pocket_List: " + _Pocket_List._ToString());
                MESLib.MesLog.AddInfo("_Module_List: " + _Module_List._ToString());
                MESLib.MesLog.AddInfo("_LotID_List: " + _LotID_List._ToString());


                MESLib.CommParas.MesManager.MES_S6F11_10711(_LotID_List, _Pocket_List,
                    _Module_List, MESLib.CommParas.MesManager.PRODID,
                    MESLib.CommParas.MesManager.PROCID,
                    parameters, filename, filepath, "", "");
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart38_FlowRun(object sender, EventArgs e)
        {
            if (MESLib.CommParas.EnableMes)
            {
                if (MESCheckAllFail)
                    return FCResultType.NEXT;
                if (SysPara.MesDebug)
                {
                    return FCResultType.NEXT;
                }
                if (MESLib.CommParas.MesManager.MES_S6F11_ACK_WithTimeout("10711", Timestamp_10711,
                    Timer_10711, out int ACK, out string data, out bool timeout))
                {
                    if (ACK == 0)
                    {
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        SysPara.NPShowAlarm("70100");
                        return FCResultType.IDLE;
                    }
                }
                else
                {
                    if (timeout)
                    {
                        // T3 Fail
                        MESLib.CommParas.MesManager.MES_S6F11_11001("2");
                        MESLib.CommParas.MesCenter.StopRunning();
                        SysPara.NPShowAlarm("70102");
                        return FCResultType.IDLE;
                    }
                }
                return FCResultType.IDLE;
            }
            return FCResultType.NEXT;
        }

        private void button45_Click_1(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["PalletMinValueX"] = (double)num_minPX.Value;
            dt.Rows[0]["PalletMinValueY"] = (double)num_minPY.Value;
            dt.Rows[0]["PalletMaxValueX"] = (double)num_maxPX.Value;
            dt.Rows[0]["PalletMaxValueY"] = (double)num_maxPY.Value;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

        }

        private void button64_Click_1(object sender, EventArgs e)
        {
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["FeederMinValueX"] = (double)num_minFX.Value;
            dt.Rows[0]["FeederMinValueY"] = (double)num_minFY.Value;
            dt.Rows[0]["FeederMaxValueX"] = (double)num_maxFX.Value;
            dt.Rows[0]["FeederMaxValueY"] = (double)num_maxFY.Value;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

        }

        private void num_maxFY_ValueChanged(object sender, EventArgs e)
        {

        }

        private FCResultType npFlowChart39_FlowRun(object sender, EventArgs e)
        {
            SysPara.WaitNextPallet = false;
            throwingjudge = false;
            CCDAlogrithmStatus = false;
            AssmbleCCDAlogStatus = false;
            isPickProduceOK = true;
            curPalletCode = "";
            curR1HaveNum = 0;
            SysPara.RIniRet = false;
            SysPara.JAGStarytEvent.Reset();
            SysPara.OverPressureab = false;
            SysPara.OverPressurebc = false;
            SysPara.Que_ProductCode_PickPos.Clear();
            SysPara.Que_pallet_Code.Clear();
            SysPara.Que_Product_pallet_Code.Clear();
            SysPara.R1GrabCompeletStatus = false;            //R1 Grab feeder product compelet flag
            SysPara.ScanCount = 0;                            //feeder scan count
            SysPara.R1ScanResult = false;                    //scan status
            SysPara.R1AssembStatus = false;                  //Notice robot to assemb
            MiddleLayer.ConveyorF.UpConveyorAutoFlow.NotifyAssembly = false;
            SysPara.ProductCount = 12;                        //assemb count
            SysPara.FeederProductCount = 42;                        //feeder product count
            SysPara.R1AssembResult = false;                  //assemb result
            SysPara.R1AssembCompeletStatus = false;      //R1 Assemb compelet flag
            MiddleLayer.ConveyorF.UpConveyorAutoFlow.AssembCompeletStatus = false;
            SysPara.PressureCompelet = false;
            SysPara.RobotPressureCompelet = false;
            scannerReuslt.Clear();
            CCDReuslt.Clear();
            queCCDPosition.Clear();
            scanCount = 0;
            RunTM.Restart();
            curProductCount = 0;
            haveSendCodeToMES = true;
            palletUpResult.Clear();
            palletDownResult.Clear();
            InitPickIndex(GetRecipeValue("RSet", "FeederPickIndex"));
            MesTestList = new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true };
            for (int i = 0; i < PCBCode.Length; i++)
            {
                PCBCode[i] = "NA";
            }
            InitTM.Restart();


            for (int i = 0; i < MaterialID_Array_Copy.Length; i++)
            {
                MaterialID_Array_Copy[i] = "";
            }
            CurrentPCBList.Clear();
            gripperIndex = 1;
            MESCheckAllFail = false;
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart40_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Goto Safety");
            if (Robot.WriteToEpson(pos[2], pos[3], pos[4], 30000))
            {
                InitTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2]
               + "Pos：" + pos[3]
               + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else
            {
                if (InitTM.On(SysPara.TimeOutSec))
                {

                    SysPara.NPShowAlarm("1635");
                }
                return FCResultType.IDLE;
            }
        }

        private FCResultType npFlowChart48_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                OB_FeederSafetySignal.On();
                return FCResultType.CASE1;
            }
            else if (ret == 0)
            {
                MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                if (Robot.ErorrCode == "E9000" ||
                    Robot.ErorrCode == "E9001" ||
                    Robot.ErorrCode == "E9002")
                {
                    return FCResultType.NEXT;
                }
                else { SysPara.NPShowAlarm("1635"); return FCResultType.IDLE; }

            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635"); return FCResultType.IDLE;
            }        //R1 timeout
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType npFlowChart41_FlowRun(object sender, EventArgs e)
        {
            gripper = "00";
            switch (Robot.ErorrCode)
            {
                case "E9000":
                    gripper = "01";
                    break;
                case "E9001":
                    gripper = "02";
                    break;
                case "E9002":
                    gripper = "03";
                    break;
                default:
                    break;
            }
            if (!GetRecipeValue("RSet", "NGbuffer1"))
            {
                SysPara.ngPos = "001";
            }
            else if (!GetRecipeValue("RSet", "NGbuffer2"))
            {
                SysPara.ngPos = "002";
            }
            else if (!GetRecipeValue("RSet", "NGbuffer3"))
            {
                SysPara.ngPos = "003";
            }
            else if (!GetRecipeValue("RSet", "NGbuffer4"))
            {
                SysPara.ngPos = "004";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer5"))
            {
                SysPara.ngPos = "005";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer6"))
            {
                SysPara.ngPos = "006";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer7"))
            {
                SysPara.ngPos = "007";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer8"))
            {
                SysPara.ngPos = "008";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer9"))
            {
                SysPara.ngPos = "009";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer10"))
            {
                SysPara.ngPos = "010";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer11"))
            {
                SysPara.ngPos = "011";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer12"))
            {
                SysPara.ngPos = "012";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer13"))
            {
                SysPara.ngPos = "013";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer14"))
            {
                SysPara.ngPos = "014";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer15"))
            {
                SysPara.ngPos = "015";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer16"))
            {
                SysPara.ngPos = "016";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer17"))
            {
                SysPara.ngPos = "017";
                return FCResultType.NEXT;
            }
            else if (!GetRecipeValue("RSet", "NGbuffer18"))
            {
                SysPara.ngPos = "018";
                return FCResultType.NEXT;
            }
            else
            {
                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "NG had full", SysPara.bEnableGeneralSaveLog);
                SysPara.NPShowAlarm("0006");
                reintento.Dispose();
                reintento = new Reintento();
                reintento.fnSetTextMessageNShow("NG tray is full!!! sure has been replaced?", true, false, true);
                flowChart72.Enabled = true;
                flowChart72.BackColor = Color.Red;
                if (reintento.dResult == DialogResult.Yes)
                {
                    DataTable dt = RecipeData.Tables["RSet"];
                    dt.Rows[0]["NGbuffer1"] = false;
                    dt.Rows[0]["NGbuffer2"] = false;
                    dt.Rows[0]["NGbuffer3"] = false;
                    dt.Rows[0]["NGbuffer4"] = false;
                    dt.Rows[0]["NGbuffer5"] = false;
                    dt.Rows[0]["NGbuffer6"] = false;
                    dt.Rows[0]["NGbuffer7"] = false;
                    dt.Rows[0]["NGbuffer8"] = false;
                    dt.Rows[0]["NGbuffer9"] = false;
                    dt.Rows[0]["NGbuffer10"] = false;
                    dt.Rows[0]["NGbuffer11"] = false;
                    dt.Rows[0]["NGbuffer12"] = false;
                    dt.Rows[0]["NGbuffer13"] = false;
                    dt.Rows[0]["NGbuffer14"] = false;
                    dt.Rows[0]["NGbuffer15"] = false;
                    dt.Rows[0]["NGbuffer16"] = false;
                    dt.Rows[0]["NGbuffer17"] = false;
                    dt.Rows[0]["NGbuffer18"] = false;
                    dt.AcceptChanges();
                    this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                    SysPara.ngPos = "001";
                    return FCResultType.NEXT;
                }
                else
                {
                    DelayMs(500);
                    return FCResultType.IDLE;
                }
                //return FCResultType.IDLE;
            }

            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart42_FlowRun(object sender, EventArgs e)
        {
            if (Robot.WriteToEpson("017", SysPara.ngPos, "FF", 20000))
            {
                MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing position cammer", SysPara.bEnableGeneralSaveLog);
                InitTM.Restart();
                return FCResultType.NEXT;
            }
            else
            {
                return FCResultType.IDLE;
            }
        }

        private FCResultType npFlowChart43_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                OB_RobotLight.On();
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing  cammer position ok", SysPara.bEnableGeneralSaveLog); 
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[1], SysPara.bEnableGeneralSaveLog);
                return FCResultType.IDLE;
            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {
                //MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot to throwing  cammer position timeout", SysPara.bEnableGeneralSaveLog);

                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType npFlowChart44_FlowRun(object sender, EventArgs e)
        {
            SysPara.visionPos = new VisionPos();
            try
            {
                double exposure = GetSettingValue("MSet", "NGCCDCheckExpTime");
                if (H1_NGCCDCheck.TakePicture(exposure))         //CCD take picture
                {

                    RefreshDifferentThreadUI(cogRecDisp_H5_Recipe, () =>
                    {
                        cogRecDisp_H5_Recipe.Image = H1_NGCCDCheck.InputImage;
                        cogRecDisp_H5_Recipe.Fit();
                    });

                    //if (!MiddleLayer.SystemF.GetSettingValue("PSet", "DisableVision"))
                    //{
                    H1_NGCCDCheck.RunTB();
                    if (Convert.ToBoolean(H1_NGCCDCheck.TB.Outputs["IsHaveProduct"].Value) == false)
                    {
                        MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot NG throwing position1 not have product", SysPara.bEnableGeneralSaveLog);
                        OB_RobotLight.Off();
                        return FCResultType.NEXT;
                    }
                    else
                    {
                        MiddleLayer.LogF.AddLog(LogForm.LogType.Production, "robot NG throwing position1 have product!!!need clear", SysPara.bEnableGeneralSaveLog);

                        string bufferIndex = "NGbuffer1";
                        switch (SysPara.ngPos)
                        {
                            case "001":
                                bufferIndex = "NGbuffer1";
                                break;
                            case "002":
                                bufferIndex = "NGbuffer2";
                                break;
                            case "003":
                                bufferIndex = "NGbuffer3";
                                break;
                            case "004":
                                bufferIndex = "NGbuffer4";
                                break;
                            case "005":
                                bufferIndex = "NGbuffer5";
                                break;
                            case "006":
                                bufferIndex = "NGbuffer6";
                                break;
                            case "007":
                                bufferIndex = "NGbuffer7";
                                break;
                            case "008":
                                bufferIndex = "NGbuffer8";
                                break;
                            case "009":
                                bufferIndex = "NGbuffer9";
                                break;
                            case "010":
                                bufferIndex = "NGbuffer10";
                                break;
                            case "011":
                                bufferIndex = "NGbuffer11";
                                break;
                            case "012":
                                bufferIndex = "NGbuffer12";
                                break;
                            case "013":
                                bufferIndex = "NGbuffer13";
                                break;
                            case "014":
                                bufferIndex = "NGbuffer14";
                                break;
                            case "015":
                                bufferIndex = "NGbuffer15";
                                break;
                            case "016":
                                bufferIndex = "NGbuffer16";
                                break;
                            case "017":
                                bufferIndex = "NGbuffer17";
                                break;
                            case "018":
                                bufferIndex = "NGbuffer18";
                                break;
                            default:
                                break;
                        }
                        DataTable dt = RecipeData.Tables["RSet"];
                        dt.Rows[0][bufferIndex] = true;
                        dt.AcceptChanges();
                        this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                        return FCResultType.CASE1;
                    }
                }
                else
                {
                    CCDAlogrithmStatus = false;

                    SysPara.NPShowAlarm("1204");        //CCD fail
                    return FCResultType.CASE1;
                }
            }
            catch
            {
                CCDAlogrithmStatus = false;

                SysPara.NPShowAlarm("1204");        //CCD fail
                return FCResultType.CASE1;
            }
        }

        private FCResultType npFlowChart45_FlowRun(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "NG Position Assemb");
            if (Robot.WriteToEpson(pos[2], SysPara.ngPos, gripper, 30000))
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2]
                + "Pos：" + SysPara.ngPos
                + "AppIndex：" + gripper, SysPara.bEnableGeneralSaveLog);
                InitTM.Restart();
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        private FCResultType npFlowChart46_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1) { return FCResultType.NEXT; }
            else if (ret == 0)
            {
                return FCResultType.IDLE;
            }
            else if (InitTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout
            else { return FCResultType.IDLE; }
        }

        private FCResultType npFlowChart47_FlowRun(object sender, EventArgs e)
        {
            string bufferIndex = "NGbuffer1";
            switch (SysPara.ngPos)
            {
                case "001":
                    bufferIndex = "NGbuffer1";
                    break;
                case "002":
                    bufferIndex = "NGbuffer2";
                    break;
                case "003":
                    bufferIndex = "NGbuffer3";
                    break;
                case "004":
                    bufferIndex = "NGbuffer4";
                    break;
                case "005":
                    bufferIndex = "NGbuffer5";
                    break;
                case "006":
                    bufferIndex = "NGbuffer6";
                    break;
                case "007":
                    bufferIndex = "NGbuffer7";
                    break;
                case "008":
                    bufferIndex = "NGbuffer8";
                    break;
                case "009":
                    bufferIndex = "NGbuffer9";
                    break;
                case "010":
                    bufferIndex = "NGbuffer10";
                    break;
                case "011":
                    bufferIndex = "NGbuffer11";
                    break;
                case "012":
                    bufferIndex = "NGbuffer12";
                    break;
                case "013":
                    bufferIndex = "NGbuffer13";
                    break;
                case "014":
                    bufferIndex = "NGbuffer14";
                    break;
                case "015":
                    bufferIndex = "NGbuffer15";
                    break;
                case "016":
                    bufferIndex = "NGbuffer16";
                    break;
                case "017":
                    bufferIndex = "NGbuffer17";
                    break;
                case "018":
                    bufferIndex = "NGbuffer18";
                    break;
                default:
                    break;
            }
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0][bufferIndex] = true;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            return FCResultType.CASE1;
        }

        private FCResultType npFlowChart49_FlowRun(object sender, EventArgs e)
        {
            //MOD@@ Guillermo Carrillo - salto del reinicio rapido
            if (bRestartQuickActive) { return FCResultType.NEXT; }
            GC.Collect();
            string processName = "FilesMonitor"; // 不包括 .exe
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(processName);

            if (processes.Length > 0)
            {
                Console.WriteLine($"Program: {processName} existed");
            }
            else
            {
                string exePath = @"D:\CurProgram\FilesMonitor\FilesMonitor.exe";
                string workingDirectory = Path.GetDirectoryName(exePath);

                try
                {
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = exePath,
                        WorkingDirectory = workingDirectory,
                        UseShellExecute = false
                    };

                    System.Diagnostics.Process.Start(startInfo);
                    Console.WriteLine($"Program: {processName} started");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Start Program Error: {ex.Message}");
                }
            }
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart50_FlowRun(object sender, EventArgs e)
        {
                return FCResultType.NEXT;
        }

        private FCResultType npFlowChart51_FlowRun(object sender, EventArgs e)
        {
            //MessageBox.Show((SysPara.Que_Product_pallet_Code.Count + 1).ToString());
            //string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Convery Pallet AssembPosition");
            ////int GripperIndex = gripperIndex; //InvertOrder(SysPara.Que_ProductCode_PickPos.Count);
            //Thread.Sleep(50);
            //int position = SysPara.Que_Product_pallet_Code.Count + 1;
            //if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", position)))
            //{
            //    RunTM.Restart();
            //    MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2] + "Pos：" + string.Format("{0:d3}", (scanCount + 1) + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog));
            return FCResultType.NEXT;
            //}
            //return FCResultType.IDLE;
            //string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Convery Pallet ScanPosition");
            //if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", 1))) //, pos[4], 30000)))
            //{
            //    RunTM.Restart();
            //    MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2] + "Pos：" + string.Format("{0:d3}", (scanCount + 1) + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog));
            //    return FCResultType.NEXT;
            //}
            //else { return FCResultType.IDLE; }
        }

        private FCResultType npFlowChart52_FlowRun(object sender, EventArgs e)
        {
            MESLib.MesLog.AddInfo($"Start Report End - {DateTime.Now.ToString("HH:mm:ss:fff")}");
            return FCResultType.NEXT;
            //int ret = Robot.ReadFromEpson();
            //if (ret == 1)
            //{
            //    RunTM.Restart();
            //    //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
            //    return FCResultType.NEXT;
            //}
            //else if (ret == 0)
            //{

            //    //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
            //    return FCResultType.IDLE;
            //}
            //else if (RunTM.On(SysPara.TimeOutSec))
            //{

            //    SysPara.NPShowAlarm("1635");
            //    return FCResultType.IDLE;
            //}        //R1 timeout
            //else { return FCResultType.IDLE; }
        }

        private FCResultType npFlowChart53_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType npFlowChart54_FlowRun(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1)
            {
                RunTM.Restart();
                //MiddleLayer.LogF.AddLog(LogType.Production, GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else if (ret == 0)
            {

                //Log.log.Write(GetDataByAnnotation(dgv_EpsonErrInfo, Robot.ErorrCode)[3], Color.Red);
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R1 timeout
            else { return FCResultType.IDLE; }
        }

        bool GotoMESCheck = false;
        private FCResultType npFlowChart55_FlowRun(object sender, EventArgs e)
        {
            if(GotoMESCheck)
            {
                GotoMESCheck = false;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType npFlowChart56_FlowRun(object sender, EventArgs e)
        {
            if (GotoMESCheck)
            {
                GotoMESCheck = false;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }
        private FCResultType npFlowChart57_FlowRun(object sender, EventArgs e)
        {
            MESFinishReport = true;
            return FCResultType.NEXT;
        }
        bool GotoPick = false;
        private FCResultType npFlowChart59_FlowRun(object sender, EventArgs e)
        {
            if (GotoPick)
            {
                GotoPick = false;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }
        bool MESFinishReport = false;
        string[] MaterialID_Array_Copy = new string[3];
        private FCResultType npFlowChart58_FlowRun(object sender, EventArgs e)
        {
            if(MESFinishReport)
            {
                MESFinishReport = false;
                // copy material array
                for(int i = 0;i< MaterialID_Array.Length;i++)
                {
                    MaterialID_Array_Copy[i] = MaterialID_Array[i];
                    MaterialID_Array[i] = ""; // reseet it here
                }
                // check is last one or not
                bool pallectallset = true;

                for (int i = 0; i < palletResultCopy.Count; i++)
                {
                    if (palletResultCopy[i])
                    {
                        pallectallset = false;
                        break;
                    }
                }
                // if it is last product, wait till finished.
                if(pallectallset)
                {
                    GotoPick = false;
                }
                else
                {
                    GotoPick = true;
                }
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType npFlowChart60_FlowRun(object sender, EventArgs e)
        {
            GotoMESCheck = true;
            // check is last product
            bool pallectallset = true;
            for (int i = 0; i < palletResultCopy.Count; i++)
            {
                if (palletResultCopy[i])
                {
                    pallectallset = false;
                    break;
                }
            }
            if(pallectallset)
            {
                GotoPick = true;
            }

            return FCResultType.NEXT;
        }
        //R1 Goto scan feeder 

        private FCResultType flowChart13_FlowRun_1(object sender, EventArgs e)
        {
            string[] pos = GetDataByAnnotation(dgv_EpsonPosition, "Feeder product ScanPosition");
            if (Robot.WriteToEpson(pos[2], string.Format("{0:d3}", SysPara.ScanCount), pos[4], 30000))
            {

                DataTable dt = RecipeData.Tables["RSet"];
                dt.Rows[0]["FeederPickIndex"] = SysPara.ScanCount;
                dt.AcceptChanges();
                this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));

                RunTM.Restart();
                MiddleLayer.LogF.AddLog(LogType.Production, "Command：" + pos[2] + "Pos：" + string.Format("{0:d3}", SysPara.ScanCount) + "AppIndex：" + pos[4], SysPara.bEnableGeneralSaveLog);
                return FCResultType.NEXT;
            }
            else { return FCResultType.IDLE; }
        }

        //Wait R1 on ScanPos

        private FCResultType flowChart14_FlowRun_1(object sender, EventArgs e)
        {
            int ret = Robot.ReadFromEpson();
            if (ret == 1) { return FCResultType.NEXT; }
            else if (ret == 0)
            {
                RunTM.Restart();
                return FCResultType.IDLE;
            }
            else if (RunTM.On(SysPara.TimeOutSec))
            {
                SysPara.NPShowAlarm("1635");
                return FCResultType.IDLE;
            }        //R timeout
            else { return FCResultType.IDLE; }
        }

        //TriggerScan
        private FCResultType flowChart15_FlowRun_1(object sender, EventArgs e)
        {
            SysPara.QRCode1 = "Null";
            ProductCode_PickPos newCode;
            if (MiddleLayer.ProcessF.GetSettingValue("PSet", "disableCheckPCBcodes"))
            {
                SysPara.QRCode1 = "QRCodeTset";
                SysPara.R1ScanResult = true;
                return FCResultType.NEXT;
            }
            if (TrigerBarcode1InFlow(RunTM))
            {
                string[] code = new string[4];
                code = SysPara.QRCode1.Split(',');
                for (int i = 0; i < code.Length; i++)
                {
                    if (code[i] != "ERROR")
                    {
                        QRCode.Add(code[i]);
                        SysPara.QRCode1 = code[i];
                        SysPara.R1ScanResult = true;

                        scannerReuslt.Add(SysPara.R1ScanResult);
                        QRCode.Add(SysPara.QRCode1);
                        //MOD@@ Guillermo Carrillo - guarda de indice: CCDReuslt vs MaterialID_Array
                        if (CCDReuslt.Count < MaterialID_Array.Length)
                        {
                            MaterialID_Array[CCDReuslt.Count] = SysPara.QRCode1;
                        }
                        newCode = new ProductCode_PickPos
                        {
                            Code = SysPara.QRCode1,
                            PosIndex = SysPara.ScanCount
                        };
                        SysPara.Que_ProductCode_PickPos.Enqueue(newCode);
                        return FCResultType.NEXT;
                    }
                }
                if (RunTM.On(3000))
                {
                    if (SysPara.QRCode1 == "ERROR,ERROR,ERROR,ERROR")
                    {
                        SysPara.QRCode1 = "NA";
                    }
                    QRCode.Add(SysPara.QRCode1);
                    SysPara.R1ScanResult = false;

                    scannerReuslt.Add(SysPara.R1ScanResult);
                    QRCode.Add(SysPara.QRCode1);
                    //MOD@@ Guillermo Carrillo - guarda de indice: CCDReuslt vs MaterialID_Array
                    if (CCDReuslt.Count < MaterialID_Array.Length)
                    {
                        MaterialID_Array[CCDReuslt.Count] = SysPara.QRCode1;
                    }
                    newCode = new ProductCode_PickPos
                    {
                        Code = SysPara.QRCode1,
                        PosIndex = SysPara.ScanCount
                    };
                    SysPara.Que_ProductCode_PickPos.Enqueue(newCode);

                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            else
            {
                if (RunTM.On(5000))
                {
                    SysPara.R1ScanResult = false;

                    scannerReuslt.Add(SysPara.R1ScanResult);
                    QRCode.Add(SysPara.QRCode1);
                    //MOD@@ Guillermo Carrillo - guarda de indice: CCDReuslt vs MaterialID_Array
                    if (CCDReuslt.Count < MaterialID_Array.Length)
                    {
                        MaterialID_Array[CCDReuslt.Count] = SysPara.QRCode1;
                    }
                    newCode = new ProductCode_PickPos
                    {
                        Code = SysPara.QRCode1,
                        PosIndex = SysPara.ScanCount
                    };
                    SysPara.Que_ProductCode_PickPos.Enqueue(newCode);
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
        }

        private FCResultType npFlowChart54_FlowRun_1(object sender, EventArgs e)
        {
            try
            {
                SaveImage image = new SaveImage();
                image.Image = new Bitmap(cogRecDisp_H5_Recipe.CreateContentBitmap(CogDisplayContentBitmapConstants.Display));
                image.Timer = DateTime.Now.ToString("yyyyMMddHHmmss");
                image.PCBQRCode = SysPara.QRCode1;
                image.PalletQRCode = "NA";
                FeederImages.Add(image);
                return FCResultType.NEXT;
            }
            catch (Exception)
            {
                return FCResultType.IDLE;
            }
            
        }

        public void InitPickIndex(int index)
        {
            int temp = 0;
            if (index % 3 != 0)
            {
                temp = (index / 3) * 3;
            }
            else { temp = index; }
            DataTable dt = RecipeData.Tables["RSet"];
            dt.Rows[0]["FeederPickIndex"] = temp;
            dt.AcceptChanges();
            this.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
        }

        private void SendDataToMes()
        {
            haveSendCodeToMES = false;
            int productCount = 0;
            //if (((Int16)GetRecipeValue("RSet", "PaaletProductCount") - SysPara.Que_Product_pallet_Code.Count) / 3 != 0)
            //{
            //    productCount = 3;
            //}
            //else
            //{
            //    if ((Int16)GetRecipeValue("RSet", "PaaletProductCount")%3==0)
            //    {
            //        productCount = 3;
            //    }
            //    else
            //    {
            //        productCount = (Int16)GetRecipeValue("RSet", "PaaletProductCount") % 3;
            //    }

            //}
            if (((Int16)GetRecipeValue("RSet", "PaaletProductCount") - SysPara.Que_Product_pallet_Code.Count) > 0)
            {
                productCount = 3;
            }
            else
            {
                if ((Int16)GetRecipeValue("RSet", "PaaletProductCount") % 3 == 0)
                {
                    productCount = 3;
                }
                else
                {
                    productCount = (Int16)GetRecipeValue("RSet", "PaaletProductCount") % 3;
                }
            }
            List<Product_Pallet_Code> dataToMES = new List<Product_Pallet_Code>();
            dataToMES = SysPara.Que_Product_pallet_Code.ToList();
            dataToMES = dataToMES.GetRange(dataToMES.Count - productCount, productCount);
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    for (int i = 0; i < dataToMES.Count; i++)
                    {
                        //MiddleLayer.MesF.fn_UpdatePCBQR(dataToMES[i].ProdcutCode, dataToMES[i].PalletCode);
                        //MiddleLayer.ConveyorF.WriteMesByOne(dataToMES[i].ProdcutCode, dataToMES.Count - productCount + i, dataToMES[i], dataToMES[i].PressureResult);
                    }
                    haveSendCodeToMES = true;
                }
                catch (Exception ex)
                {

                }
            });
            for (int i = 0; i < dataToMES.Count; i++)
            {
                MiddleLayer.ConveyorF.WriteMesByOne(dataToMES[i].ProdcutCode, dataToMES.Count - productCount + i, dataToMES[i], dataToMES[i].PressureResult);
            }
        }

        #region Read preasure
        ReadCOM PressReadCOM = new ReadCOM();

        private void btnConnectCOM1_Click(object sender, EventArgs e)
        {
            //PressReadCOM.ConnectCom3();
            string comStr = cmbComPort.SelectedItem.ToString();
            string paraStr = txtBortRate.Text + "," + txtDataBits.Text + "," +
                txtParity.Text + "," + txtStopBit.Text;
            if (PressReadCOM.ConnectCom1(comStr, paraStr))
            //    if (PressReadCOM.ConnectCom1())
            {
                this.btnReadCOM1_Click(this, null);
                DataTable dt = RecipeData.Tables["RSet"];
                dt.Rows[0]["COMIndex"] = cmbComPort.SelectedIndex;
                dt.AcceptChanges();
                MiddleLayer.SystemF.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
                this.btnConnectCOM1.BackColor = Color.Green;
            }
            else
                this.btnConnectCOM1.BackColor = Color.Red;
        }

        private void btnDisconnectCOM1_Click(object sender, EventArgs e)
        {
            PressReadCOM.DisconnectCom1();
            this.btnConnectCOM1.BackColor = this.btnDisconnectCOM1.BackColor;
        }

        private void btnReadCOM1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "1":
                    this.txtCOM1_Value.Text = PressReadCOM.ReadContent_COM1();
                    break;
                case "2":
                    this.txtCOM1_Value.Text = PressReadCOM.ReadContent_COM2();
                    break;
                case "3":
                    this.txtCOM1_Value.Text = PressReadCOM.ReadContent_COM3();
                    break;
                default:
                    break;
            }

        }

        private void btnAutoCom1_Pressure_Click(object sender, EventArgs e)
        {
            btnAutoCom1_Pressure.Enabled = false;
            switch (comboBox1.SelectedItem)
            {
                case "1":
                    PressReadCOM.ContinuedRead_COM1();
                    //.dgvData_AutoCom1_Pressure.DataSource = ReadCOM.dtTable_Com1;
                    break;
                case "2":
                    PressReadCOM.ContinuedRead_COM2();
                    //this.dgvData_AutoCom2_Pressure.DataSource = ReadCOM.dtTable_Com2;
                    break;
                case "3":
                    PressReadCOM.ContinuedRead_COM3();
                    //this.dgvData_AutoCom3_Pressure.DataSource = ReadCOM.dtTable_Com3;
                    break;
                default:
                    break;
            }
        }

        private void btnStopAuto_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "1":
                    PressReadCOM.COM1_ClearZero();
                    break;
                case "2":
                    PressReadCOM.COM2_ClearZero();
                    break;
                case "3":
                    PressReadCOM.COM3_ClearZero();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region FFU
        FFUCom ffu = new FFUCom();

        public bool ConnectFFU(string com, string para)
        {
            if (ffu.Connect(com, para))
            {
                return true;
            }
            else { return false; }
        }
        //Connect FFU
        private void button11_Click_1(object sender, EventArgs e)
        {
            string comStr = MiddleLayer.GantryF.GetSettingValue("MSet", "FFUComPort");
            string paraStr = txb_FFURate.Text + "," + txb_FFUDataBit.Text + "," +
                txb_FFUEvenOrOrder.Text + "," + txb_FFUStopBIt.Text;
            if (ConnectFFU(comStr, paraStr)) { this.button11.BackColor = Color.Green; }
            else { this.button11.BackColor = Color.Red; }
        }
        //Disconnect FFU
        private void button10_Click(object sender, EventArgs e)
        {
            ffu.DisConnect();
            this.button11.BackColor = Color.White;
        }
        //Open ffu by maxspeed
        private void button20_Click(object sender, EventArgs e)
        {
            if (ffu.OpenFFUMaxSpeed())
            {
                button20.BackColor = Color.Green;
            }
            else { button20.BackColor = Color.Red; }
        }
        //Close ffu
        private void button9_Click(object sender, EventArgs e)
        {
            if (ffu.SetFFUSpeed(0))
            {
                button23.BackColor = Color.Green;
            }
            else { button23.BackColor = Color.Red; }
        }
        //Set speed
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (ffu.SetFFUSpeed((int)nud_FFUSpeed.Value))
            {
                button23.BackColor = Color.Green;
            }
            else { button23.BackColor = Color.Red; }
        }

        private void tabPage19_Enter(object sender, EventArgs e)
        {
            tm_ReadFFUSpeed.Start();
        }

        private void tabPage19_Leave(object sender, EventArgs e)
        {
            tm_ReadFFUSpeed.Stop();
        }

        private void tm_ReadFFUSpeed_Tick(object sender, EventArgs e)
        {
            if (ffu.ConnectStatus)
            {
                RefreshDifferentThreadUI(MiddleLayer.GantryF.txb_CurFFUSpeed, () =>
                {
                    MiddleLayer.GantryF.txb_CurFFUSpeed.Text = ffu.GetFFUSpeed().ToString();
                    Thread.Sleep(100);
                });
            }
        }
        #endregion


        #region preasure
        ReadCOM newReadCOM = new ReadCOM();
        object objchart1 = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_index"></param>
        /// <param name="_Pressure"></param>
        /// <param name="_Type"></param>
        public void Chart1Add_Pressure_RubberSheet(int _index, double _Pressure, string _Type = "")
        {
            //Task.Factory.StartNew(() =>
            //{

            lock (objchart1)
            {
                RefreshDifferentThreadUI(this.chart1, () =>
                {
                    if (this.chart1.ChartAreas[0].AxisX.Maximum < _index)
                        this.chart1.ChartAreas[0].AxisX.Maximum = _index;
                    this.chart1.Series[0].Points.AddXY(_index, _Pressure);
                });

                if (_Type == "Over")
                {
                    RefreshDifferentThreadUI(this.chart1, () =>
                    {
                        this.chart1.Series[0].LegendText = "Min: 0 \r\nMax: " + _Pressure;
                    });
                    RefreshDifferentThreadUI(this.dgvData_AutoCom1_Pressure, () =>
                    {
                        this.dgvData_AutoCom1_Pressure.DataSource = ReadCOM.dtTable_Com1;
                    });
                    RefreshDifferentThreadUI(this.dgvData_AutoCom1_Pressure, () =>
                    {
                        this.dgvData_AutoCom1_Pressure.DataSource = ReadCOM.dtTable_Com1;
                    });
                    //SysPara.PreasureData1.Time = DateTime.Now.ToString("hh-mm-ss");
                    //SysPara.PreasureData1.MatCode = SysPara.QRCode1;
                    //SysPara.PreasureData1.PreasureDirver = 1;
                    //SysPara.PreasureData1.Preasure = this.chart1.Series[0].Points.FindMaxByValue().YValues[0]; //max
                    //SysPara.PreasureData1.PreasureMax = GetRecipeValue("RSet", "PressMax");
                    //SysPara.PreasureData1.PreasureMin = GetRecipeValue("RSet", "PressMin");
                    //if (_Pressure >= GetRecipeValue("RSet", "PressMax"))
                    //{
                    //    SysPara.OverPressure = true;
                    //    SysPara.PreasureData1.Result = "NG";
                    //}
                    //else { SysPara.PreasureData1.Result = "OK"; }
                    //processData.fnUpdateResult(SysPara.PreasureData1);
                    SysPara.PressureDirver = 1;
                    SysPara.PressureData = this.chart1.Series[0].Points.FindMaxByValue().YValues[0];
                    SysPara.PressureCompelet = true;
                    RefreshDifferentThreadUI(this.btnAutoCom1_Pressure, () =>
                    {
                        this.btnAutoCom1_Pressure.Enabled = true;
                    });
                }

            }
            //this.chart1.Series[0].Points.Add(_Pressure,_time);
            //});
        }
        /// <summary>
        /// 
        /// </summary>
        public void Chart1Clear_Pressure_RubberSheet()
        {
            Task.Factory.StartNew(() =>
            {
                //if (this.chart1.Series[0].ChartType!=null)
                try
                {
                    if (this.chart1 != null)
                        if (this.chart1.Series.Count > 0)
                            if (this.chart1.Series[0] != null)
                                if (this.chart1.Series[0].Points.Count > 0)
                                    this.chart1.Series[0].Points.Clear();
                }
                catch (Exception ex)
                { }
            });
        }



        object objchart2 = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_index"></param>
        /// <param name="_Pressure"></param>
        /// <param name="_Type"></param>
        public void Chart2Add_Pressure_RubberSheet(int _index, double _Pressure, string _Type = "")
        {
            //Task.Factory.StartNew(() =>
            //{
            lock (objchart2)
            {
                RefreshDifferentThreadUI(this.chart2, () =>
                {
                    if (this.chart2.ChartAreas[0].AxisX.Maximum < _index)
                        this.chart2.ChartAreas[0].AxisX.Maximum = _index;
                    this.chart2.Series[0].Points.AddXY(_index, _Pressure);
                });

                if (_Type == "Over")
                {
                    RefreshDifferentThreadUI(this.chart2, () =>
                    {
                        this.chart2.Series[0].LegendText = "Min: 0 \r\nMax: " + _Pressure;
                    });
                    RefreshDifferentThreadUI(this.dgvData_AutoCom2_Pressure, () =>
                    {
                        this.dgvData_AutoCom2_Pressure.DataSource = ReadCOM.dtTable_Com2;
                    });
                    //SysPara.PreasureData2.Time = DateTime.Now.ToString("hh-mm-ss");
                    //SysPara.PreasureData2.MatCode = SysPara.QRCode1;
                    //SysPara.PreasureData2.PreasureDirver = 2;
                    //SysPara.PreasureData2.Preasure = this.chart2.Series[0].Points.FindMaxByValue().YValues[0]; 
                    //SysPara.PreasureData2.PreasureMax = GetRecipeValue("RSet", "PressMax");
                    //SysPara.PreasureData2.PreasureMin = GetRecipeValue("RSet", "PressMin");
                    //if (_Pressure >= GetRecipeValue("RSet", "PressMax"))
                    //{
                    //    SysPara.OverPressure = true;
                    //    SysPara.PreasureData2.Result = "NG";
                    //}
                    //else { SysPara.PreasureData2.Result = "OK"; }
                    //processData.fnUpdateResult(SysPara.PreasureData2);
                    SysPara.PressureDirver = 2;
                    SysPara.PressureData = this.chart2.Series[0].Points.FindMaxByValue().YValues[0];
                    SysPara.PressureCompelet = true;
                    RefreshDifferentThreadUI(this.btnAutoCom1_Pressure, () =>
                    {
                        this.btnAutoCom1_Pressure.Enabled = true;
                    });
                }

            }
            //this.chart1.Series[0].Points.Add(_Pressure,_time);
            //});
        }
        /// <summary>
        /// 
        /// </summary>
        public void Chart2Clear_Pressure_RubberSheet()
        {
            Task.Factory.StartNew(() =>
            {
                //if (this.chart1.Series[0].ChartType!=null)
                try
                {
                    if (this.chart2 != null)
                        if (this.chart2.Series.Count > 0)
                            if (this.chart2.Series[0] != null)
                                if (this.chart2.Series[0].Points.Count > 0)
                                    this.chart2.Series[0].Points.Clear();
                }
                catch (Exception ex)
                { }
            });
        }


        object objchart3 = new object();




        /// <summary>
        /// 
        /// </summary>
        /// <param name="_index"></param>
        /// <param name="_Pressure"></param>
        /// <param name="_Type"></param>
        public void Chart3Add_Pressure_RubberSheet(int _index, double _Pressure, string _Type = "")
        {
            //Task.Factory.StartNew(() =>
            //{
            lock (objchart2)
            {
                RefreshDifferentThreadUI(this.chart3, () =>
                {
                    if (this.chart3.ChartAreas[0].AxisX.Maximum < _index)
                        this.chart3.ChartAreas[0].AxisX.Maximum = _index;
                    this.chart3.Series[0].Points.AddXY(_index, _Pressure);
                });

                if (_Type == "Over")
                {
                    RefreshDifferentThreadUI(this.chart3, () =>
                    {
                        this.chart3.Series[0].LegendText = "Min: 0 \r\nMax: " + _Pressure;
                    });
                    RefreshDifferentThreadUI(this.dgvData_AutoCom3_Pressure, () =>
                    {
                        this.dgvData_AutoCom3_Pressure.DataSource = ReadCOM.dtTable_Com3;
                    });
                    //SysPara.PreasureData3.Time = DateTime.Now.ToString("hh-mm-ss");
                    //SysPara.PreasureData3.MatCode = SysPara.QRCode1;
                    //SysPara.PreasureData3.PreasureDirver = 3;
                    //SysPara.PreasureData3.Preasure = this.chart3.Series[0].Points.FindMaxByValue().YValues[0]; 
                    //SysPara.PreasureData3.PreasureMax = GetRecipeValue("RSet", "PressMax");
                    //SysPara.PreasureData3.PreasureMin = GetRecipeValue("RSet", "PressMin");
                    //if (_Pressure >= GetRecipeValue("RSet", "PressMax"))
                    //{
                    //    SysPara.OverPressure = true;
                    //    SysPara.PreasureData3.Result = "NG";
                    //}
                    //else { SysPara.PreasureData3.Result = "OK"; }
                    //processData.fnUpdateResult(SysPara.PreasureData3);
                    SysPara.PressureDirver = 3;
                    SysPara.PressureData = this.chart3.Series[0].Points.FindMaxByValue().YValues[0];
                    SysPara.PressureCompelet = true;
                    RefreshDifferentThreadUI(this.btnAutoCom1_Pressure, () =>
                    {
                        this.btnAutoCom1_Pressure.Enabled = true;
                    });
                }

            }
            //this.chart1.Series[0].Points.Add(_Pressure,_time);
            //});
        }
        /// <summary>
        /// 
        /// </summary>
        public void Chart3Clear_Pressure_RubberSheet()
        {
            Task.Factory.StartNew(() =>
            {
                //if (this.chart1.Series[0].ChartType!=null)
                try
                {
                    if (this.chart3 != null)
                        if (this.chart3.Series.Count > 0)
                            if (this.chart3.Series[0] != null)
                                if (this.chart3.Series[0].Points.Count > 0)
                                    this.chart3.Series[0].Points.Clear();
                }
                catch (Exception ex)
                { }
            });
        }
        #endregion

        #region Robot speed
        private void label115_DoubleClick(object sender, EventArgs e)
        {
            if (MiddleLayer.GantryF.Robot.ConnectStatus)
            {
                string[] pos = MiddleLayer.GantryF.GetDataByAnnotation(MiddleLayer.GantryF.dgv_EpsonPosition, "Read Robot Speed");
                if (MiddleLayer.GantryF.Robot.WriteToEpson(pos[2], string.Format("{0:d3}", SysPara.ScanCount), pos[4], 30000))
                {
                    Thread.Sleep(50);
                    int ret = MiddleLayer.GantryF.Robot.ReadFromEpson();
                    if (ret == 105) { trackBar2.Value = Convert.ToInt32(string.Format("{0:d2}", MiddleLayer.GantryF.Robot.Speed)); }
                    else if (ret == -105)
                    {
                        MiddleLayer.LogF.AddLog(LogType.Production, "Read robot speed fail", SysPara.bEnableGeneralSaveLog);

                        //Log.log.Write("读取机器人速度失败", Color.Red);
                    }
                }
                else
                {
                    MiddleLayer.LogF.AddLog(LogType.Production, "Read robot speed fail", SysPara.bEnableGeneralSaveLog);

                    //Log.log.Write("读取机器人速度失败", Color.Red); 
                }
            }
        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            if (MiddleLayer.GantryF.Robot.ConnectStatus)
            {
                string[] pos = MiddleLayer.GantryF.GetDataByAnnotation(MiddleLayer.GantryF.dgv_EpsonPosition, "Write Robot Speed");
                MiddleLayer.GantryF.Robot.SetSpeed(pos[2], pos[3], pos[4], 30000, string.Format("{0:d7}", trackBar2.Value));

            }
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            lbR2SpeedRatio.Text = trackBar2.Value.ToString();
        }
        #endregion

        #region Save datagridview
        //Save all tables position
        public void SaveAxisPosPara()
        {
            string epsonPositionPath = Path.Combine(Application.StartupPath, @"ModuleData\EpsonPosition.xml");
            dgv_EpsonPosition.SaveXmlFileFromPath(epsonPositionPath);
            string epsonErrInfoPath = Path.Combine(Application.StartupPath, @"ModuleData\EpsonErrInfo.xml");
            dgv_EpsonErrInfo.SaveXmlFileFromPath(epsonErrInfoPath);
            string techRobotDischargePos = Path.Combine(Application.StartupPath, @"ModuleData\TechRobotDischargePos.xml");
            dgv_DischargeTechPos.SaveXmlFileFromPath(techRobotDischargePos);
            string RobotPositionInfoPath = Path.Combine(Application.StartupPath, @"ModuleData\RobotTeachPosInfo.xml");
            dgv_RobotPosition.SaveXmlFileFromPath(RobotPositionInfoPath);
        }
        //Load all tables position
        public void ReadAxisPara()
        {
            string epsonPositionPath = Path.Combine(Application.StartupPath, @"ModuleData\EpsonPosition.xml");
            dgv_EpsonPosition.LoadXmlFileFromPath(epsonPositionPath);
            string epsonErrInfoPath = Path.Combine(Application.StartupPath, @"ModuleData\EpsonErrInfo.xml");
            dgv_EpsonErrInfo.LoadXmlFileFromPath(epsonErrInfoPath);
            string techRobotDischargePos = Path.Combine(Application.StartupPath, @"ModuleData\techRobotDischargePos.xml");
            dgv_DischargeTechPos.LoadXmlFileFromPath(techRobotDischargePos);
            string RobotPositionInfoPath = Path.Combine(Application.StartupPath, @"ModuleData\RobotTeachPosInfo.xml");
            dgv_RobotPosition.LoadXmlFileFromPath(RobotPositionInfoPath);
        }
        #endregion
        #endregion
    }
}

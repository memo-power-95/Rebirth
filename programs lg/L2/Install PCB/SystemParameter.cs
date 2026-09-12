using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Alpha
{
    public class SysPara
    {
        // Mes debug flag
        public static bool MesDebug = false;
        public static bool WaitNextPallet = false; // record whether last step need to go to CASE2 bratch.

        public static bool[] MESReportResult = new bool[3]; // record report results
        //System Parameter
        public static string sIniFile = @"C:\Acura\MachineSetup.ini";
        public static string ProgramDir = System.IO.Directory.GetCurrentDirectory();
        public static bool Simulation;
        public static string ProjectName;
        public static string RecipeName;
        public static bool bExpertMode;
        public static bool bEnableGeneralSaveLog;
        public static int iQualityCheckCounter = 0;

        //
        public static bool bDoubleMarkingValidation = false;
        public static bool bDryCycle = false;
        public static bool bDryCycleNotUnit = false;
        public static bool bBypassMode = false;
        public static bool bUseSMEMA = false;
        public static bool bSkipLaserMarking = false;
        public static bool bFlowValidationMode = false;
        public static bool bTakeBoardOutFlipped = false;
        public static bool bValidateLaserMarking = false;
        public static bool bDisableDoor = false;
        public static bool bCheckPCBcodes = false;

        //System Path
        public static string SystemDataDirectory;
        public static string LogDirectory;
        public static string VisionFileDirectory;
        public static string AlarmTableDirectory;
        public static string SettingDataDirectory;
        public static string RecipeDataDirectory;
        public static string MESDirectory;
        public static string LanguageDataDirectory;
        public static string IOPortDirectory;
        public static string AcuraIOTDirectory;
        public static string MESDataDirectory;
        public static bool bOB_UpCvyMtRev = false;
        public static bool bOB_UpCvyMtFwd = false;

        public static bool bOB_SM_UpBoardReady = false;
        //public static bool bOB_SMMachineReady = false;
        public static bool bOB_SM_UpFaliBoard = false;

        

        // MESDATA
        public static string CurrentName;
        public static string PCName;
        public static string StationName;
        public static string DName;
        public static string RName;

        public static string MPName;
        public static string MCIDName;
        public static string LineName;
        public static string STXT;
        //Barcode Info
        public static string gBarcodeInfo;


        //Login Info

        //Temporal, quitar

        public static PermissionType UserPermission = PermissionType.Operator;
        public static string UserName = "Operador";
        //public static PermissionType UserPermission = PermissionType.Administrator;
        //public static string UserName = "Admin";


        //Language
        public static LanguageType LanguageShow = LanguageType.English;
        public static List<ComponentTextInfo>[] ComponentLangurageList;

        //Flow Control
        public static bool IsMaintenanceMode = false;
        public static bool bSaftyReady = false;
        public static bool SystemRun = false;
        public static RunMode SystemMode = RunMode.IDLE;
        public static bool SystemInitialOk = false;
        public static bool RIniRet = false;
        public static bool useDryCycleMode = false;
        public static bool useConveyorBypass = false;
        public static bool useLeftSMEMA = true;
        public static bool useRightSMEMA = false;
        public static bool conveyorEnabled = true;

        //Operation Time Record
        public static int ScanTime_Work = 0;
        public static int ScanTime_AlwaysRun = 0;
        public static int ScanTime_RefreshIOS = 0;


        public static ulong OperationSecond = 0;
        public static ulong RunSecond = 0;
        public static ulong StopSecond = 0;
        public static DateTime StartWorkTM;
        public static DateTime EndWorkTM;
        public static int TimeOutSec = 10000;       //Timeout
        public static object StatusLock = new object();
        //Module Handshake
        public static SystemParameter SystemPara;
        public static MESParameter MESPara;

        public static MainParameter MainPara;

        public static CvIntApp interCvApp = new CvIntApp();
        public static string QRCode1 = "";
        public static string PalletCode1 = "";
        public static string PalletCode2 = "";
        public static string PalletCode3 = "";
        public static ResultData ResultData = new ResultData();        //Preassure Result1
        //public static PreasureData PreasureData2 = new PreasureData();        //Preassure Result2
        //public static PreasureData PreasureData3 = new PreasureData();        //Preassure Result3
        public static string CsvHeader = "";

        public static string PressureDataDirectory = Directory.GetCurrentDirectory() + "\\PressureData";
        public static bool OverPressure = false;
        public static bool OverPressureab = false;
        public static bool OverPressurebc = false;
        public static bool PressureCompelet = false;
        public static bool RobotPressureCompelet = false;
        public static double PressureData = 0;
        public static int PressureDirver = 0;

        public static string ngPos = "";

        //Product code+pickIndex
        public static Queue<ProductCode_PickPos> Que_ProductCode_PickPos = new Queue<ProductCode_PickPos>();
        //Conveyor signal flow scan reuslt queue
        public static Queue<string> Que_pallet_Code = new Queue<string>();
        //prodcutcode + assembPosition code
        public static Queue<Product_Pallet_Code> Que_Product_pallet_Code = new Queue<Product_Pallet_Code>();
        public static bool DischargeFlag = true;
        public static int EachPalletActualCount = 0;
        public static int DischargeCount = 0;

        public static ProductCode_PickPos pickPos = new ProductCode_PickPos();
        public static Product_Pallet_Code ProductPalletCode = new Product_Pallet_Code();
        public static VisionPos visionPos = new VisionPos();        //Vision data

        public static bool R1GrabCompeletStatus = false;            //R1 Grab feeder product compelet flag
        public static int ScanCount = 0;                            //feeder scanCount
        public static bool R1ScanResult = false;                    //scan status
        public static bool R1AssembStatus = false;                  //notice robot to assemb
        public static int ProductCount = 12;                        //assemb Totalcount
        public static int FeederProductCount = 42;                        //feeder product count
        public static bool R1AssembResult = false;                  //assemb result
        public static bool R1AssembCompeletStatus = false;      //R1 Assemb compelet flag
        public static bool TempAssembCompeletStatus = false;
        public static AutoResetEvent JAGStarytEvent = new AutoResetEvent(false);//notice feeder compelet

        //Convery
        public static bool bOB_SMMachineReady = false;
        public static bool bOB_SMForntMachineNg = false;
        public static bool IsEnglish = false;
        public static bool AlarmNow = false;

        //CFX
        public static string cfx_PalletCode = "";
        public static List<int> cfx_FaliList = new List<int>();
        public static List<int> cfx_AbortList = new List<int>();

        //Door
        public static bool LeftDoorAlarmEnable = false;
        public static bool RightDoorAlarmEnable = false;

        public static bool bCableDetectEnable = true;
        public static Thread CableDetectTh;

        public static bool EnableMes = false;
        public static bool SYS_STOP_MODE = true; // MES need to know the STOP mode of system and enable the buttons refer to MES
        public static bool LG_IT_CONNECT = false;
        public static DateTime IdleStart = DateTime.Now;
        public static DateTime IdleEnd = DateTime.Now;
        public static int PalletProductCount = 0;

        public static bool AutoManualState = false;//  Auto:true   Manual:false

        public static string strPickup = "_PickUp_";
        public static string strDischarge = "_Discharge_";

        public static void NPShowAlarm(string code)
        {
            //  Auto:true   Manual:false
            if (SysPara.AutoManualState)
                MESLib.CommFuns.MES_ShowAlarm(code);
            else
                MESLib.CommFuns.MES_ShowAlarm("9" + code);
        }
        public static void NPShowAlarm(string code, string des)
        {
            //  Auto:true   Manual:false
            if (SysPara.AutoManualState)
                MESLib.CommFuns.MES_ShowAlarm(code, "[Auto]" + des);
            else
                MESLib.CommFuns.MES_ShowAlarm("9" + code, "[Manual]" + des);
        }
    }


    public struct ResultData
    {
        public string Time;
        public string ProductCode;
        public string PalletCode;
        public int PreasureDirver;
        public double Preasure;
        public double PreasureMax;
        public double PreasureMin;
        public string abResult;
        public string bcResult;
    }

    public struct ProductCode_PickPos
    {
        public string Code;
        public double PosIndex;
        public bool CCDResult;
    }

    public struct Product_Pallet_Code
    {
        public string ProdcutCode;
        public string PalletCode;
        public string data;
        public bool PressureResult;
    }
    public struct Pos4D
    {
        public double x;
        public double y;
        public double z;
        public double u;
    }
    public struct VisionPos
    {
        public double x;
        public double y;
        public double u;
    }
    public struct CalibrationData
    {
        public double PixelX;
        public double PixelY;
        public double MotorPosX;
        public double MotorPosY;
    }

    public struct ComponentTextInfo
    {
        public string FormName;
        public string ComponentName;
        public string ComponentText;
        public Control Component;
    }

    public struct IOPortInfo
    {
        public string FormName;
        public string ComponentName;
        public string Port;
    }

    public enum PermissionType
    {
        Operator = 0,
        Maintenance,
        Administrator,
        None,
    }

    public enum LanguageType
    {
        Chinese = 0,
        English,
    }

    public enum RunMode
    {
        IDLE = 0,
        AUTO,
        HOME,
    }

    public struct MainParameter
    {
        public bool bDarkModeActive;
        public bool bDarkModeButtonEnabled;
    }

    /// <summary>
    /// Enumeracion de los diversos estados en que se pueden encontrar los modulos
    /// </summary>
    public enum status
    {
        /// <summary>
        /// No hay ningun estado seleccionado
        /// </summary>
        None = 0,
        /// <summary>
        /// En espera
        /// </summary>
        Idle,
        /// <summary>
        /// Ocupado / En proceso
        /// </summary>
        Busy,
        /// <summary>
        /// Comienza el modulo de inicializacion
        /// </summary>
        InitialStart,
        /// <summary>
        ///  Esta inicializando
        /// </summary>
        Initializing,
        /// <summary>
        /// La inicialización finalizo
        /// </summary>
        InitialDone,
        /// <summary>
        /// Cargando Unidades en el Slider
        /// </summary>
        Loading,
        /// <summary>
        ///  Comienza el proceso automatico
        /// </summary>
        ProcessStart,
        /// <summary>
        /// El modulo esta en proceso
        /// </summary>
        Processing,
        /// <summary>
        ///  El proceso actual del modulo termino
        /// </summary>
        ProcessDone,
        /// <summary>
        /// Unidad lista para procesar
        /// </summary>
        ReadyToProcess,
        /// <summary>
        ///  El modulo en gral esta en error
        /// </summary>
        Error
    }



    #region Declare module parameter

    public struct SystemParameter
    {
        /// <summary>
        /// Almacena el estado de las seguridades de la maquina
        /// </summary>
        public bool bSaftyReady;

        /// <summary>
        /// Realiza el cambio de estado cuando el equipo esta en modo mantenimiento
        /// </summary>
        public bool IsMaintenanceMode;
        // <summary>
        /// Guarda valor actual de piezas pasadas
        /// </summary>
        public string sCurrentUnits;



    }
    public class MESParameter
    {
        public bool WriteUnitTars;
        public bool WriteUnitTarsOk;
        public string serialNumber;
        public string partNumber;
        public char revision;
        public DateTime startDatetime;
        public DateTime endDateTime;
        public List<KeyValuePair<string, string>> fails;
        public bool status;
        public List<Measurement> measurements;

        public MESParameter()
        {
            fails = new List<KeyValuePair<string, string>>();
            measurements = new List<Measurement>();
            status = true;
        }
    }

    public struct Measurement
    {
        public string measureLabel;
        public string measureData;
        public string measureMessage;

        public Measurement(string measureLabel, string measureData, string measureMessage)
        {
            this.measureLabel = measureLabel;
            this.measureData = measureData;
            this.measureMessage = measureMessage;
        }
    }


    #endregion




    public struct FastenData
    {
        public string Point;
        public double Torque;
        public double Angle;
        //public double RundownAngle;
        // public bool PostViewTorque;
        public string Result;
        // public ResultCode Result;
        //public DateTime FasternTime;
    }

    public struct FastenData2
    {
        public string Point2;
        public double Torque2;
        public double Angle2;
        //public double RundownAngle;
        // public bool PostViewTorque;
        public string Result2;
        // public ResultCode Result;
        //public DateTime FasternTime;
    }

    public class CvIntApp
    {
        public bool bLoadBoardFinish = false;
        public bool bUnloadBoardFinish = false;
        public bool bCanUnloadBoard = false;
        public bool bCanLoadBoard = false;
        public bool bXyHomeFinish = false;
        public bool bCanFlip = false;
        public bool bFlipFinish = false;
        public bool bCvInitCheckFinish = false;
        

        public void Reset()
        {
            bLoadBoardFinish = false;
            bUnloadBoardFinish = false;
            bCanUnloadBoard = false;
            bCanLoadBoard = false;
            bXyHomeFinish = false;
            bCanFlip = false;
            bFlipFinish = false;
            bCvInitCheckFinish = false;
        }

       
    }
    public class InstallPCBInfo
    {
        public string MaterialID { set; get; }
        public string Result_A { set; get; }
        public string Result_B { set; get; }
    }

    public static class ExpandMethod
    {
        public static string _ToString<T>(this List<T> list)
        {
            string str = "";
            if (list == null)
                return "";
            else if (list.Count == 0)
                return "";
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != 0)
                        str += ", ";
                    str += list[i].ToString();
                }
            }
            return str;
        }
        public static string _ToString<T>(this T[] array)
        {
            if (array == null)
                return "";
            if (array.Length == 0)
                return "";
            string str = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (i != 0)
                    str += ", ";
                str += array[i].ToString();
            }

            return str;
        }

        public static string _ToString(this Dictionary<int, InstallPCBInfo> pcbinfos)
        {
            if (pcbinfos == null)
                return "";
            if (pcbinfos.Count == 0)
                return "";
            string str = "";
            foreach (var p in pcbinfos)
            {
                str += p.Key.ToString() + ":" + string.Format("{0}, {1}, {2}", p.Value.MaterialID,
                    p.Value.Result_A, p.Value.Result_B);
            }
            return str;
        }

    }
}

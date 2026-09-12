using Alpha.Classes;
using Alpha.Forms;
using Alpha.FunctionForms;
using Alpha.MENUForms;
using Alpha.ModuleForms;
using AcuraLibrary;
using AcuraLibrary.Forms;
using NPSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp2;
using Alpha._0.ModuleForms;
using Alpha._0;
using System.Threading;
using Alpha._0.Classes;

namespace Alpha
{
    class MiddleLayer
    {
        //===================Module Form===================
        public static SystemForm SystemF;
        public static ProcessForm ProcessF;
        public static ScannerForm ScannerF;
        public static VisionProRAC VisionProRACF;
        public static ConveyorForm ConveyorF;
        public static GantryForm GantryF;
        public static IXSensorForm IXSensorF;
        //public static LaserMarkingForm LaserMarkingF;
        //public static MesForm Mes_F;
        public static MesForm MesF2;
        public static LotForm LotF;
        public static ProcessDataForm ProcessDataF;

        //===================Function Form=================
        public static LoadingMarqueeForm LoadingMarqueeF;
        public static SignalTowerForm SignalTowerF;
        public static MotorJogForm MotorJogF;
        public static LogForm LogF;
        public static MESForm MesF;
        public static AlarmForm AlarmF;
        public static Reintento VisionRetryF;
        public static Reintento IXHeightRetry;
        public static Reintento MainRetry;
        public static MensajeForm MensajeF;
        public static OptionChoiceForm RobotDialogF;
        public static SwitchLanguage switchLanguage;
        public static CFXHandler cfxHandler = new CFXHandler();

        //===================MENU Form==================
        public static MainForm MainF;
        public static MachineStatusForm MachineStatusF;
        public static MachineSetupForm MachineSetupF;
        public static RecipeEditorForm RecipeEditorF;
        public static ProductionSettingForm ProductionSettingF;
        public static MaintenanceForm MaintenanceF;
        public static FlowChartForm FlowChartF;
        public static UserSettingForm UserSettingF;
        public static About_Form AboutF;
        public static ExceptionReportForm ExReportF;
        //=================================================
        public static List<dynamic> lstForm = new List<dynamic>();
        public static FlowControl FlowCtrl;


        //Clases
        public static Alpha.Classes.ModeColors ModeColors = new Alpha.Classes.ModeColors();
        //=================== Hardwarwe Inventory Instance ==================

        public static void InitialProject()
        {
            #region Load Ini File
            LoadingMarqueeF.SetCaption("Archivo de configuracion");
            fnLoadIniFile();
            #endregion

            #region Load Alarm Table
            LoadingMarqueeF.SetCaption("Tabla de alarmas");
            SDKKernal.LoadAlarmTable(string.Format("{0}\\{1}.xml", SysPara.AlarmTableDirectory, SysPara.LanguageShow.ToString()));
            #endregion

            //Start Checking if it must run on simulation
            SDKKernal.SetSimulation(SysPara.Simulation);

            //About Form Must be Initialize first to capture all module information 
            AboutF = CreateForm(AboutF, "About_Form");
            //========================Form Create===========================================================================================
            MainF = CreateForm(MainF, "MainForm");
            SignalTowerF = CreateForm(SignalTowerF, "SignalTowerForm");
            //Necesary Create first ExceptionLog before Log Form beacuse it import panel
            ExReportF = CreateForm(ExReportF, "ExceptionReportForm");
            LogF = CreateForm(LogF, "LogForm");
            MesF = CreateForm(MesF, "MESForm");
            VisionRetryF = CreateForm(VisionRetryF, "Reintento");
            IXHeightRetry = CreateForm(IXHeightRetry, "Reintento");
            MainRetry = CreateForm(MainRetry, "Reintento");
            MensajeF = CreateForm(MensajeF, "MensajeForm");

            //==============================================================================================================================

            //=======================Module Create==========================================================================================

            ProcessF = CreateForm(ProcessF, "ProcessForm");
            SystemF = CreateForm(SystemF, "SystemForm");
            //EpsonRobotF = CreateForm(EpsonRobotF, "EpsonRobot");
            ConveyorF = CreateForm(ConveyorF, "ConveyorForm");
            GantryF = CreateForm(GantryF, "GantryForm");
            //VisionProRACF = CreateForm(VisionProRACF, "VisionProRAC");
            ScannerF = CreateForm(ScannerF, "ScannerForm");
            //IXSensorF = CreateForm(IXSensorF, "IXSensorForm");
            ProcessDataF = CreateForm(ProcessDataF, "ProcessDataForm");
            MesF2 = CreateForm(MesF2, "MesForm");
            //IXSensorF = CreateForm(IXSensorF, "IXSensorForm");
            LotF = CreateForm(LotF, "LotForm");
            AlarmF = CreateForm(AlarmF, "AlarmForm");
            //==============================================================================================================================

            //=====================Complex Form Create======================================================================================
            MachineStatusF = CreateForm(MachineStatusF, "MachineStatusForm");
            MachineSetupF = CreateForm(MachineSetupF, "MachineSetupForm");
            RecipeEditorF = CreateForm(RecipeEditorF, "RecipeEditorForm");
            ProductionSettingF = CreateForm(ProductionSettingF, "ProductionSettingForm");
            MaintenanceF = CreateForm(MaintenanceF, "MaintenanceForm");
            FlowChartF = CreateForm(FlowChartF, "FlowChartForm");
            UserSettingF = CreateForm(UserSettingF, "UserSettingForm");
            MotorJogF = CreateForm(MotorJogF, "MotorJogForm");
            //==============================================================================================================================

            LoadingMarqueeF.SetCaption("Permiso de usuarios");
            SysPara.UserPermission = PermissionType.Administrator;
            SwitchPermission(SysPara.UserPermission);
            LoadingMarqueeF.SetCaption("Creando interfaz grafica");
            SDKKernal.SetSimulation(SysPara.Simulation);
            SDKKernal.InitializeComponent();

            #region Acura Colors
            //Initialize with DarkMode Color
            foreach (ModuleBaseForm module in ModuleManager.ModuleList)
            {
                foreach (Control control in module.Controls)
                {
                    MainForm.LabelsDarkMode(control);
                }
            }
            #endregion Acura Colors

            #region MODBUS Declaration
            if (!SysPara.Simulation)
            {
                //Robot Epson Main
                //SDKPara.ModbusDevices["192.168.0.1"].DiscreteInputsIndex = 511;//Inputs
                //SDKPara.ModbusDevices["192.168.0.1"].DiscreteInputsQuantity = 80;
                //SDKPara.ModbusDevices["192.168.0.1"].CoilsIndex = 511;//Outputs
                //SDKPara.ModbusDevices["192.168.0.1"].CoilsQuantity = 64;

            }
            #endregion MODBUS Declaration

            LoadingMarqueeF.SetCaption($"Ultima receta {SysPara.RecipeName}");
            OpenRecipe(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            LoadingMarqueeF.SetCaption("Iniciando control de flujos");
            
            LoadingMarqueeF.SetCaption("Encender Motores");
            ServoOn();
            LoadingMarqueeF.SetCaption("Finalizando");
            AfterInitSDK();
            FlowCtrl = new FlowControl();
            FlowCtrl.StartThread();
        }

        public static void DisposeProject()
        {
            cfxHandler.CloseEndpoint();
            ServoOff();
            foreach (ModuleBaseForm module in ModuleManager.ModuleList)
                module.ModuleDispose();

            FlowCtrl.StopThread();
            FlowCtrl = null;
        }


        private static void InitialLanguageCallback(Control cl, string FormName, ref List<ComponentTextInfo> ComponentLangurageList, ref List<ComponentTextInfo> XmlDataList)
        {
            foreach (Control control in cl.Controls)
            {
                Type ControlType = control.GetType();
                bool bNeedAdded = false;
                bNeedAdded |= (ControlType == typeof(Form));
                bNeedAdded |= (ControlType == typeof(Label));
                bNeedAdded |= (ControlType == typeof(NPMotor));
                bNeedAdded |= (ControlType == typeof(NPInput));
                bNeedAdded |= (ControlType == typeof(NPOutput));
                bNeedAdded |= (ControlType == typeof(NPFlowChart));
                bNeedAdded |= (ControlType == typeof(GroupBox));
                bNeedAdded |= (ControlType == typeof(CheckBox));
                bNeedAdded |= (ControlType == typeof(CheckedListBox));
                bNeedAdded |= (ControlType == typeof(Button));
                if (control.Name != "" && bNeedAdded)
                {
                    ComponentTextInfo AddComLan = new ComponentTextInfo();
                    int Index = XmlDataList.FindIndex(ComLan => (ComLan.FormName == FormName && ComLan.ComponentName == control.Name));
                    if (Index >= 0)
                    {
                        AddComLan.FormName = FormName;
                        AddComLan.ComponentName = XmlDataList[Index].ComponentName;
                        AddComLan.ComponentText = XmlDataList[Index].ComponentText;
                        AddComLan.Component = control;
                    }
                    else
                    {
                        AddComLan.FormName = FormName;
                        AddComLan.ComponentName = control.Name;
                        AddComLan.ComponentText = control.Text;
                        AddComLan.Component = control;
                    }
                    ComponentLangurageList.Add(AddComLan);
                }
                if (control.HasChildren)
                    InitialLanguageCallback(control, FormName, ref ComponentLangurageList, ref XmlDataList);
            }
        }

        public static void SwitchPermission(PermissionType Permission)
        {
            SysPara.UserPermission = Permission;
            SDKPara.bShowFlowchartMenu = Permission == PermissionType.Administrator;
            MainF.SwitchPermission(Permission);
            SysPara.bExpertMode = SysPara.UserPermission == PermissionType.Administrator;
        }

        private static void InitialIOPortData()
        {
            List<IOPortInfo> ComponentIOPortList = new List<IOPortInfo>();
            List<IOPortInfo> XmlDataList = new List<IOPortInfo>();
            #region Load from xml file
            string sFilePath = string.Format("{0}\\IOPort.xml", SysPara.IOPortDirectory);
            if (File.Exists(sFilePath))
            {
                XmlDocument ReadDoc = new XmlDocument();
                ReadDoc.Load(sFilePath);
                XmlElement Element = (XmlElement)ReadDoc.SelectSingleNode("PortData");
                if (Element != null)
                {
                    XmlNodeList FormData = Element.ChildNodes;
                    for (int j = 0; j < FormData.Count; j++)
                    {
                        XmlNodeList ComponentData = FormData[j].ChildNodes;
                        for (int k = 0; k < ComponentData.Count; k++)
                        {
                            IOPortInfo PortInfo = new IOPortInfo();
                            PortInfo.FormName = FormData[j].Name;
                            PortInfo.ComponentName = ComponentData[k].Name;
                            PortInfo.Port = ((XmlElement)ComponentData[k]).GetAttribute("Port");
                            XmlDataList.Add(PortInfo);
                        }
                    }
                }
            }
            #endregion
            #region compare all component IOPort data and write to list
            //AllForm
            foreach (Control Form in lstForm)
                InitialIOPortCallback(Form, Form.Name, ref ComponentIOPortList, ref XmlDataList);
            #endregion
            #region Write to xml file
            XmlDocument WriteDoc = new XmlDocument();
            XmlElement FirstElement = XMLExpand.GetElement(WriteDoc, "PortData");
            for (int j = 0; j < ComponentIOPortList.Count; j++)
            {
                XmlElement eSetting = XMLExpand.GetElement(WriteDoc, "PortData/" + ComponentIOPortList[j].FormName + "/" + ComponentIOPortList[j].ComponentName);
                eSetting.SetAttribute("Port", ComponentIOPortList[j].Port);
            }
            if (!Directory.Exists(SysPara.IOPortDirectory))
                Directory.CreateDirectory(SysPara.IOPortDirectory);
            XMLExpand.WriteUnicodeXML(WriteDoc, sFilePath);
            #endregion
        }

        private static void InitialIOPortCallback(Control cl, string FormName, ref List<IOPortInfo> ComponentIOPortList, ref List<IOPortInfo> XmlDataList)
        {
            foreach (dynamic control in cl.Controls)
            {
                Type ControlType = control.GetType();
                bool bNeedAdded = false;
                bNeedAdded |= (ControlType == typeof(NPMotor));
                bNeedAdded |= (ControlType == typeof(NPInput));
                bNeedAdded |= (ControlType == typeof(NPOutput));
                if (control.Name != "" && bNeedAdded)
                {
                    IOPortInfo AddComLan = new IOPortInfo();
                    int Index = XmlDataList.FindIndex(ComLan => (ComLan.FormName == FormName && ComLan.ComponentName == control.Name));
                    if (Index >= 0)
                    {
                        AddComLan.FormName = FormName;
                        AddComLan.ComponentName = XmlDataList[Index].ComponentName;
                        AddComLan.Port = XmlDataList[Index].Port;
                        control.Port = XmlDataList[Index].Port;
                    }
                    else
                    {
                        AddComLan.FormName = FormName;
                        AddComLan.ComponentName = control.Name;
                        AddComLan.Port = control.Port;
                    }
                    ComponentIOPortList.Add(AddComLan);
                }
                if (control.HasChildren)
                    InitialIOPortCallback(control, FormName, ref ComponentIOPortList, ref XmlDataList);
            }
        }

        private static dynamic CreateForm(dynamic FormAddress, string FormName)
        {
            LoadingMarqueeF.SetCaption(FormName);

            dynamic dmic = (FormAddress == null) ? null : FormAddress;
            Assembly Assembly = Assembly.GetExecutingAssembly();
            foreach (Type ObjType in Assembly.GetTypes())
                if (ObjType.Name == FormName)
                {
                    if (dmic == null)
                        dmic = Activator.CreateInstance(ObjType, null);
                    lstForm.Add(dmic);
                    if (ObjType.IsSubclassOf(typeof(ModuleBaseForm)))
                        dmic.ModuleInitialize(dmic.Text);
                    break;
                }
            if (AboutF != null)
            {
                MiddleLayer.AboutF.fnAppendModule(dmic);
            }
            return dmic;
        }

        public static void StartRun()
        {
            SysPara.AutoManualState = true;
            if (!SysPara.SystemRun)
                if ((SysPara.SystemMode == RunMode.AUTO) || (SysPara.SystemMode == RunMode.HOME))
                {
                    if (SysPara.bOB_UpCvyMtRev)
                        MiddleLayer.ConveyorF.OB_UpCvyMtRev.On();
                    else
                        MiddleLayer.ConveyorF.OB_UpCvyMtRev.Off();

                    if (SysPara.bOB_UpCvyMtFwd)
                        MiddleLayer.ConveyorF.OB_UpCvyMtFwd.On();
                    else
                        MiddleLayer.ConveyorF.OB_UpCvyMtFwd.Off();

                    if (SysPara.bOB_SM_UpBoardReady)
                        MiddleLayer.ConveyorF.OB_SM_UpBoardReady.On();
                    else
                        MiddleLayer.ConveyorF.OB_SM_UpBoardReady.Off();
                    if (SysPara.bOB_SMMachineReady)
                        MiddleLayer.ConveyorF.OB_SMMachineReady.On();
                    else
                        MiddleLayer.ConveyorF.OB_SMMachineReady.Off();
                    if (SysPara.bOB_SM_UpFaliBoard)
                        MiddleLayer.ConveyorF.OB_SM_UpFaliBoard.On();
                    else
                        MiddleLayer.ConveyorF.OB_SM_UpFaliBoard.Off();

                    MotorAlarmReset();
                    SDKKernal.ClearAllAlarm();

                    foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                        Module.StartRun();

                    SysPara.SystemRun = true;
                }
        }

        public static void StopRun()
        {
            SysPara.AutoManualState = false;
            if (MiddleLayer.ConveyorF.OB_UpCvyMtRev.GetState())
                SysPara.bOB_UpCvyMtRev = true;
            else
                SysPara.bOB_UpCvyMtRev = false;

            if (MiddleLayer.ConveyorF.OB_UpCvyMtFwd.GetState())
                SysPara.bOB_UpCvyMtFwd = true;
            else
                SysPara.bOB_UpCvyMtFwd = false;

            if (MiddleLayer.ConveyorF.OB_SM_UpBoardReady.GetState())
                SysPara.bOB_SM_UpBoardReady = true;
            else
                SysPara.bOB_SM_UpBoardReady = false;

            if (MiddleLayer.ConveyorF.OB_SMMachineReady.GetState())
                SysPara.bOB_SMMachineReady = true;
            else
                SysPara.bOB_SMMachineReady = false;

            if (MiddleLayer.ConveyorF.OB_SM_UpFaliBoard.GetState())
                SysPara.bOB_SM_UpFaliBoard = true;
            else
                SysPara.bOB_SM_UpFaliBoard = false;

            MiddleLayer.ConveyorF.OB_SM_UpBoardReady.Off();
            MiddleLayer.ConveyorF.OB_SMMachineReady.Off();
            MiddleLayer.ConveyorF.OB_SM_UpFaliBoard.Off();

            MiddleLayer.ConveyorF.OB_UpCvyMtRev.Off();
            //补充机器人暂停
            MiddleLayer.GantryF.OB_RobotStart.Off();
            Thread.Sleep(100);
            MiddleLayer.GantryF.OB_RobotPause.On();
            Thread.Sleep(100);
            MiddleLayer.GantryF.OB_RobotPause.Off();
            //    MiddleLayer.GantryF.OB_IX2.Off();
            //    MiddleLayer.GantryF.bSplindeVelocity = false;
            SysPara.SystemRun = false;
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                Module.StopRun();

            StopAllMotor();
        }

        public static void AlwaysRun()
        {
            if (SDKPara.Arm.DoStop)
            {
                SDKPara.Arm.DoStop = false;
                StopRun();
            }
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.AlwaysRun();
                }
                catch (Exception ex)
                {

                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50007", "An unpredictable error occurred on AlwaysRun() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
        }

        public static void CheckMotorProtected()
        {
            if (SysPara.Simulation)
                return;

            AxisIOState IOState;
            foreach (ControlBaseInterface control in SDKPara.ControlList)
            {
                if (control is NPMotor)
                {
                    IOState = ((NPMotor)control).GetAxisIOState();

                    if (IOState.ALM)
                        SysPara.NPShowAlarm("50023", $"NPMotor {control.Name} alarmado");

                    if (IOState.EMG)
                        SysPara.NPShowAlarm("50027", $"NPMotor {control.Name} con señal EMG activa");

                    if (IOState.SLN || IOState.SLP)
                        SysPara.NPShowAlarm("50025", $"NPMotor {control.Name} llegó al sensor de límite");

                    if (!IOState.SVON && SysPara.SystemInitialOk && control.Name != "MTR_Z")
                    {
                        SysPara.NPShowAlarm("50028", $"NPMotor ${control.Name} apagado");
                        //SysPara.SystemInitialOk = false;
                    }
                }
            }
        }

        public static void InitialParameterReset()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.InitialParameterReset();
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50010", "An unpredictable error occurred on ExecuteReset() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
        }

        public static void InitialReset()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.InitialReset();
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50011", "An unpredictable error occurred on InitialReset() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
        }
        /// <summary>
        /// Function that replace the functions like the bool variable called bFisrtRun on always run. 
        /// The goal is to execute this function after all the forms get initiailzed.
        /// </summary>
        public static void AfterInitSDK()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.AfterInitSDK();
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50005", "An unpredictable error occurred on AfterInitSDK() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
        }
        public static void ServoOn()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.ServoOn();
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50008", "An unpredictable error occurred on ServoOn() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
        }

        public static void ServoOff()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    //Module.ServoOff();
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50009", "An unpredictable error occurred on ServoOff() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
        }

        public static void SetSpeed(int SpeedRate)
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.SetSpeed(SpeedRate);
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50009", "An unpredictable error occurred on ServoOff() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
        }

        public static bool Initial()
        {
            bool bInitailOk = true;
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.Initial();
                }
                catch (Exception ex)
                {
                   
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50012", "An unpredictable error occurred on Initial() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
            return bInitailOk;
        }

        public static bool GetInitialOk()
        {
            bool bInitialOk = true;
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                bInitialOk &= Module.GetInitialOk();
            return bInitialOk;
        }

        public static void RunReset()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.RunReset();
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50013", "An unpredictable error occurred on RunReset() ! ModuleName=\"" + Module.Name + "\"");
                    StopRun();
                }
            }
        }

        public static void Run()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.Run();
                }
                catch (Exception ex)
                {
                    MiddleLayer.ExReportF.fnAddException(ex);
                    SysPara.NPShowAlarm("50014", "An unpredictable error occurred on Run() ! ModuleName=\"" + Module.Name + "\" " + ex.Message);
                    StopRun();
                }
            }
        }

        public static void MotorAlarmReset()
        {
            foreach (ControlBaseInterface control in SDKPara.ControlList)
                if (control is NPMotor)
                {
                    if (((NPMotor)control).GetAxisIOState().ALM)
                    {
                        ((NPMotor)control).AlarmReset();
                    }
                     //((NPMotor)control).ServoOn();
                }
        }

        public static bool OpenRecipe(string RecipePath)
        {
            bool bOpenSuccess = false;
            try
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].ReadRecipeData(RecipePath);
                SysPara.RecipeDataDirectory = Path.GetDirectoryName(RecipePath);
                SysPara.RecipeName = Path.GetFileNameWithoutExtension(RecipePath);
                OpenVision();
                MesF.ReadMESData();
                MesF.updateMES();
                bOpenSuccess = true;
            }
            catch (Exception ex)
            {
                MiddleLayer.ExReportF.fnAddException(ex);
                string sMsgBox = ex.Message;
                MethodBase MB = MethodBase.GetCurrentMethod();

                string sTittle = $"System Exception {MB.ReflectedType.Name} {MB.Name}";
                MessageBox.Show(new Form { TopMost = true }, sMsgBox, sTittle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                SysPara.RecipeDataDirectory = ".\\ModuleData\\RecipeData";
                SysPara.RecipeName = "Recipe";
                bOpenSuccess = false;
            }
            IniFile IniFile = new IniFile(".\\MachineSetup.ini");
            IniFile.WriteString("MachineSetup", "RecipeName", SysPara.RecipeName);
            IniFile.WriteString("PathSetup", "RecipeDirectory", SysPara.RecipeDataDirectory.Replace(System.IO.Directory.GetCurrentDirectory() + "\\", ".\\"));
            return bOpenSuccess;
        }
        public static void OpenVision()
        {
            for (int i = 0; i < VproAlogrithm.VList.Count; i++)
            {
                string path = string.Format(@"{0}\{1}\{2}.vpp", SysPara.VisionFileDirectory, VproAlogrithm.VList[i].GetType().Name, SysPara.RecipeName);
                VproAlogrithm.VList[i].LoadTB(string.Format(@"{0}\{1}\{2}.vpp", SysPara.VisionFileDirectory, VproAlogrithm.VList[i].GetType().Name, SysPara.RecipeName));
            }
        }
        public static void StopAllMotor()
        {
            foreach (ControlBaseInterface control in SDKPara.ControlList)
                if (control is NPMotor)
                { ((NPMotor)control).Stop();
                    //if (SysPara.SystemMode == RunMode.AUTO)
                    //{
                    //    ((NPMotor)control).ServoOff();
                    //}

                }
        }
        public static bool fnInitCompolet(out OMRON.Compolet.CIPCompolet64.NXCompolet _compolet, string IP = null, int port = -1)
        {
            try
            {
                OMRON.Compolet.CIPCompolet64.NXCompolet compolet = new OMRON.Compolet.CIPCompolet64.NXCompolet(null);
                //Actualmetne Requiere la declaracion de la IP en System Form
                string compoletIP = SystemF?.GetSettingValue("PSet", "CompoletIP") ?? "";
                int compoletPort = SystemF?.GetSettingValue("PSet", "CompoletPort") ?? -1;
                _compolet = compolet;
                return (compoletIP != "" && compoletPort != -1) ? fnConnectCompolet(compolet, compoletIP, compoletPort) : false;

            }
            catch (Exception ex)
            {
                _compolet = null;
                ExReportF?.fnAddException(ex);
                return false;
            }

        }

        public static bool fnConnectCompolet(OMRON.Compolet.CIPCompolet64.NXCompolet compolet, string ip = null, int? port = null)
        {
            if (SysPara.Simulation)
                return true;

            string compoletIP = ip ?? SystemF?.GetSettingValue("PSet", "CompoletIP");
            int compoletPort = port ?? SystemF?.GetSettingValue("PSet", "CompoletPort");

            try
            {
                if (!string.IsNullOrEmpty(compoletIP) && compoletPort != 0)
                {
                    compolet.LocalPort = compoletPort;
                    compolet.PeerAddress = compoletIP;
                    compolet.Active = true;
                    compolet.ReceiveTimeLimit = 1250;
                    return compolet.IsConnected;
                }
            }
            catch (Exception ex)
            {
                ExReportF?.fnAddException(ex);
            }

            //SDKPara.Arm.ReportAlarm("4000", AcuraIOT.ErrorGroup.PLC, AcuraIOT.ErrorSubGroup.Connection, AcuraIOT.ErrorType.K, $"Compolet no pudo crear una conexion con {ip ?? ""}:{(port != null ? port.ToString() : "")}");
            return false;
        }
        public static void RemoveAllStops()
        {
            foreach (ModuleBaseForm module in ModuleManager.ModuleList)
            {
                try
                {
                    module.RemoveStop();

                }
                catch (Exception ex)
                {
                    SysPara.NPShowAlarm("50019", "Error inesperado en RemoveAllStops() ! ModuleName=\"" + module.Name + "\" " + ex.Message);
                    StopRun();
                }
            }
        }

        public static async Task ResetAlarms(int delay)
        {
            foreach (ModuleBaseForm module in ModuleManager.ModuleList)
                module.TurnOnResetAlarms();

            await Task.Delay(delay);

            foreach (ModuleBaseForm module in ModuleManager.ModuleList)
                module.TurnOffResetAlarms();
        }

        public static void StopAllMotion()
        {
            foreach (ModuleBaseForm module in ModuleManager.ModuleList)
            {
                try
                {
                    module.StopMotion();

                }
                catch (Exception ex)
                {
                    SysPara.NPShowAlarm("50022", "Error inesperado en StopAllMotion() ! ModuleName=\"" + module.Name + "\" " + ex.Message);
                    StopRun();
                }
            }
        }


        public static void AfterJogForm()
        {
            foreach (ModuleBaseForm module in ModuleManager.ModuleList)
            {
                try
                {
                    module.AfterJogForm();

                }
                catch (Exception ex)
                {
                    SysPara.NPShowAlarm("50029", "Error inesperado en AfterJogForm() ! ModuleName=\"" + module.Name + "\" " + ex.Message);
                    StopRun();
                }
            }
        }

        public static void BeforeJogForm()
        {
            foreach (ModuleBaseForm module in ModuleManager.ModuleList)
            {
                try
                {
                    module.BeforeJogForm();

                }
                catch (Exception ex)
                {
                    SysPara.NPShowAlarm("50030", "Error inesperado en BeforeJogForm() ! ModuleName=\"" + module.Name + "\" " + ex.Message);
                    StopRun();
                }
            }
        }
        public static void fnLoadIniFile()
        {
            IniFile iniFile = new IniFile(SysPara.sIniFile);
            SysPara.ProjectName = iniFile.ReadString("MachineSetup", "ProjectName", "Alpha.0");
            SysPara.RecipeName = iniFile.ReadString("MachineSetup", "RecipeName", "Recipe");
            SysPara.Simulation = iniFile.ReadBoolen("LogicSetup", "Simulation", false);
            SysPara.iQualityCheckCounter = iniFile.ReadInteger("LogicSetup", "QualityCheckCounter", 0);
            SysPara.LogDirectory = iniFile.ReadString("PathSetup", "LogFileDirectory", ".\\LogFile");
            SysPara.VisionFileDirectory = iniFile.ReadString("PathSetup", "VisionDataDirectory", ".\\VisionData");
            SysPara.AlarmTableDirectory = iniFile.ReadString("PathSetup", "AlarmTableDirectory", ".\\AlarmTable");
            SysPara.SettingDataDirectory = iniFile.ReadString("PathSetup", "SettingDataDirectory", ".\\ModuleData\\SettingData");
            SysPara.RecipeDataDirectory = iniFile.ReadString("PathSetup", "RecipeDirectory", ".\\ModuleData\\RecipeData");
            SysPara.MESDataDirectory = iniFile.ReadString("PathSetup", "MESDirectory", ".\\ModuleData\\MESData");
            SysPara.MESDirectory = iniFile.ReadString("PathSetup", "MESDirectory", ".\\ModuleData\\MESData");
            SysPara.IOPortDirectory = iniFile.ReadString("PathSetup", "IOPortDirectory", ".\\ModuleData");
            SysPara.LanguageDataDirectory = iniFile.ReadString("PathSetup", "LanguageDirectory", ".\\LanguageData");
            SysPara.SystemDataDirectory = Path.Combine(iniFile.ReadString("PathSetup", "SystemDataDirectory", ""), "SystemData.mdb");
            SysPara.AcuraIOTDirectory = Path.Combine(iniFile.ReadString("PathSetup", "AcuraIOTDirectory", ""), "AcuraCloudServices.ini");
            SysPara.bEnableGeneralSaveLog = iniFile.ReadBoolen("LogicSetup", "SaveLog", true);
            ModuleManager.SettingDataDirectory = SysPara.SettingDataDirectory;
        }
    }
}

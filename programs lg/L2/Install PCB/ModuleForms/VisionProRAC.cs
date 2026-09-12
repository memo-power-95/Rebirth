using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ImageProcessing;
using NPSDK;
using AcuraLibrary.Forms;
using Alpha.FunctionForms;
using AcuraLibrary;
using System.IO;
using System.Linq;
using AsyncAwaitBestPractices;
using System.Collections.Generic;
using Alpha.Classes;

namespace Alpha.ModuleForms
{
    public partial class VisionProRAC : ModuleBaseForm
    {
        #region ENUMS
        public enum VisionProgramType
        {
            RECIPE,
            PRODUCTION
        }

        public enum ColorLight
        {
            RED,
            GREEN,
            BLUE,
            WHITE,
            OFF

        }
        public enum LampSelection
        {
            GantryUp,
            GantryDown

        }
        #endregion ENUMS

        #region STRUCTS
        public struct VisionResult
        {
            public double X;
            public double Y;
            public double U;
            public bool ResultBool;
            public double ResultDouble;
            public string Description;
            public void Reset()
            {
                X = 0;
                Y = 0;
                U = 0;
                ResultBool = false;
                ResultDouble = 0;
                Description = "";
            }
        }
        #endregion STRUCTS

        #region VARIABLES

        //Ventana Modal
        ModalVProRac dialogModal = new ModalVProRac();

        //VPRO
        private string sCogCameraTool_Path = "";
        private string sImgFlipTool_Path = "";
        private CogAcqFifoTool CogCameraTool; //摄像头配置
        public CogIPOneImageTool ImgFlipTool; //CAMERA FLIP TOOL  相机翻转
        public CogToolBlock TB_RecipeUpVPP; //Recipe PROGRAM TOOL  配方程序
        public CogToolBlock TB_RecipeDwVPP; //Recipe PROGRAM TOOL  配方程序
        private CogToolBlock TB_ProductionVPP; //Production PROGRAM TOOL  生产程序
        public CogToolBlock RunTB; //TOOL RESERVATED FOR RUNNING      运行保留的工具
        public CogToolBlock CalibTB;
        public string TB_VPP_RecipeUp_Path;               //上相机配方地址
        public string TB_VPP_RecipeDw_Path;               //下相机配方地址

        public string camera;
        public double[] _lfXShift = { -5, 0, 5, -5, 0, 5, -5, 0, 5 };
        public double[] _lfYShift = { -5, -5, -5, 0, 0, 0, 5, 5, 5 };
        public bool bTeachResult = false;
        public double Teach_Pick_X = 0;
        public double Teach_Pick_Y = 0;
        //private string cameraUpName = "CamUp";//上相机名称
        //private string cameraDwName = "CamDw";//下相机名称

        private string TB_VPP_Recipe_Tool;                //配方Tool
        private static string sProductionProgramName = "ProductionSettings";
        private string TB_VPP_Production_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sProductionProgramName + ".vpp");
        public int nRadiusImageDisplay = 300;

        //Live Image
        private JTimer JT_LiveImage = new JTimer();

        //Interface
        //private VisionResult Result_VPP = new VisionResult();
        //private VisionResult LastOffset = new VisionResult();
        CogRecordDisplay cogRecordDisplay_RecipeEditor;
        CogRecordDisplay cogRecordDisplay_MachineState;

        //public bool bVisionEnabled { get { return GetData(EnableVision); } }
        public bool bVisionEnabled2 { get { return GetData(EnableVisionGantry); } }


        List<Task<(bool, string)>> lTaskConnect = new List<Task<(bool, string)>>();
        private cSmartAlarm cAlarm = SmartAlarm.cAlarm;

        //RecipeChange
        private string sLastRecipe = "";

        //Light control
        public bool LightIsConnect = false;
        private int _Channel = 1;
        private int _Channel2 = 4;
        private string _SerialNumber = "";
        private string _ComPortName = "COM1";
        private OPTControllerAPI OPTController = null;
        private bool LampUpConnected = false;
        #endregion VARIABLES

        public VisionProRAC()
        {
            #region Tab
            plMaintenance.Enabled = true;
            plProductionSetting.Enabled = true;
            plFlowAuto.Enabled = false;
            plFlowInitial.Enabled = true;
            plMachineStatus.Enabled = true;
            plMotorControl.Enabled = false;
            plRecipeEditor.Enabled = true;
            plMotionSetup.Enabled = false;
            #endregion
            InitializeComponent();

            #region DataGridView
            foreach (DataGridViewColumn column in dgvPosInspection.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            foreach (DataGridViewColumn column in dataGridView_CameraSettings.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            #endregion DataGridView

            #region CogRecordsResult
            button_Production_TeachSelected.Enabled = false;
            button_Production_TeachSelected.Visible = false;
            cogRecordDisplay_RecipeEditor = new Cognex.VisionPro.CogRecordDisplay();
            cogRecordDisplay_MachineState = new Cognex.VisionPro.CogRecordDisplay();
            plMachineStatus.Controls.Add(cogRecordDisplay_MachineState);
            panel_Image.Controls.Add(cogRecordDisplay_RecipeEditor);
            cogRecordDisplay_RecipeEditor.Dock = DockStyle.Fill;
            cogRecordDisplay_MachineState.Dock = DockStyle.Fill;
            #endregion CogRecordResult

            #region SmartAlarm
            cAlarm.AddRange(fc_In_VisionConnectionStart);
            #endregion SmartAlarm
            //
            #region LightControl
            OPTController = new OPTControllerAPI();
            LightConnect();
            fnControlLights(LampSelection.GantryUp, ColorLight.OFF);
            fnControlLights(LampSelection.GantryDown, ColorLight.OFF);
            //LightConnect();
            #endregion
        }

        #region CONFIG INITIAL
        public override void StartRun()
        {
            if (Ctrl_CamLiveCaptureWorker.IsBusy)
            {
                try
                {
                    Ctrl_CamLiveCaptureWorker.CancelAsync();
                    btnGrabImage.Enabled = true;
                    btnEditVPRO.Enabled = true;
                    btnExVision.Enabled = true;
                    btnLiveImage.BackgroundImage = Properties.Resources.Play;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fallo al terminar Live Image.\n" + ex.ToString());
                }
            }
        }
        public override void StopRun()
        {
            //if (Ctrl_CamLiveCaptureWorker.IsBusy)
            //{
            //    try
            //    {
            //        Ctrl_CamLiveCaptureWorker.CancelAsync();

            //        if (Image.InvokeRequired)
            //        {
            //            btnGrabImage.BeginInvoke((MethodInvoker)(() =>
            //            btnGrabImage.Enabled = true
            //            ));
            //            btnEditVPRO.BeginInvoke((MethodInvoker)(() =>
            //            btnEditVPRO.Enabled = true
            //            ));
            //            btnExVision.BeginInvoke((MethodInvoker)(() =>
            //            btnExVision.Enabled = true
            //            ));
            //            btnLiveImage.BeginInvoke((MethodInvoker)(() =>
            //            btnLiveImage.BackgroundImage = Properties.Resources.Play
            //            ));
            //        }
            //        else
            //        {
            //            btnGrabImage.Enabled = true;
            //            btnEditVPRO.Enabled = true;
            //            btnExVision.Enabled = true;
            //            btnLiveImage.BackgroundImage = Properties.Resources.Play;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Fallo al terminar Live Image.\n" + ex.ToString());
            //    }
            //}
        }
        public override void AlwaysRun()
        {
            #region Recipe Change
            if (SysPara.RecipeName != sLastRecipe)
            {
                sLastRecipe = SysPara.RecipeName;
                LoadVPP().SafeFireAndForget(null,false);
            }
            #endregion Recipe Change
        }

        public override void InitialReset()
        {
            fc_In_VisionConnectionStart.TaskReset();
        }

        public override void Initial()
        {
            fc_In_VisionConnectionStart.TaskRun();
        }
        public override void ModuleInitialize(string ModuleName)
        {
            base.ModuleInitialize(ModuleName);

            try
            {
                int iTotalRows = PCameraSettings.Rows.Count;
                if (iTotalRows > 0)
                {
                    //DataRow row = PCameraSettings.Rows[0];
                    //string sCamera = row[CameraVPPName].ToString();
                    //DataRow row1 = PCameraSettings.Rows[1];
                    //string sCamera1 = row1[CameraVPPName].ToString();
                    //string sTool = row[ImageToolVPPName].ToString();
                    //sCogCameraTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sCamera + ".VPP");
                    //sImgFlipTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sTool + ".VPP");
                    //CogCameraTool = (CogAcqFifoTool)CogSerializer.LoadObjectFromFile(sCogCameraTool_Path);
                    //ImgFlipTool = (CogIPOneImageTool)CogSerializer.LoadObjectFromFile(sImgFlipTool_Path);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public override void ModuleDispose()
        {
            cogRecordDisplay_RecipeEditor.Dispose();
            cogRecordDisplay_MachineState.Dispose();
            dialogModal.Hide();
            dialogModal.Dispose();
            fnControlLights(LampSelection.GantryUp, ColorLight.OFF);
            fnControlLights(LampSelection.GantryDown, ColorLight.OFF);
            LightDisConnect();


            try//DISCONNECT VPRO
            {
                CogFrameGrabbers framegrabbers = new CogFrameGrabbers();

                foreach (ICogFrameGrabber fg in framegrabbers)
                    fg.Disconnect(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            CogFrameGrabbers fs = new CogFrameGrabbers();
            for (int i = 0; i < fs.Count; i++)
                fs[i].Disconnect(true);

            //if (CogCameraTool != null)
            //    CogCameraTool.Dispose();

            Application.Exit();
        }
        #endregion CONFIG INITIAL

        #region GENERAL FUNCTIONS

        #region System


        private int getTableRowNumberByName(DataColumn _DataColumn, string sRowName)
        {
            DataTable dtTable = _DataColumn.Table;
            int iTotalPrograms = dtTable.Rows.Count;

            for (int i = 0; i < iTotalPrograms; i++)
            {
                DataRow row = dtTable.Rows[i];
                string sName = row[_DataColumn.ColumnName].ToString();

                if (sName == sRowName)
                    return i;
            }

            return -1;
        }
        #endregion System

        #region Vision
        /// <summary>
        /// LoadVPP
        /// </summary>
        private async Task<bool> LoadVPP()
        {
            try
            {
                TB_VPP_RecipeUp_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", SysPara.RecipeName/* + "Up*/+".vpp");
                //TB_VPP_RecipeDw_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", SysPara.RecipeName + "Dw.vpp");
                TB_RecipeUpVPP = await Task.Run(() => (CogToolBlock)CogSerializer.LoadObjectFromFile(TB_VPP_RecipeUp_Path)).ConfigureAwait(false);
                //TB_RecipeDwVPP = await Task.Run(() => (CogToolBlock)CogSerializer.LoadObjectFromFile(TB_VPP_RecipeDw_Path)).ConfigureAwait(false);
                TB_ProductionVPP = await Task.Run(() => (CogToolBlock)CogSerializer.LoadObjectFromFile(TB_VPP_Production_Path)).ConfigureAwait(false);
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)(() =>
                    {
                        lb_VPPName.Text = SysPara.RecipeName + ".vpp";
                        lb_VPPName.BackColor = Color.DarkGreen;
                        label_ProductionVPPName.Text = sProductionProgramName + ".vpp";
                        label_ProductionVPPName.BackColor = Color.DarkCyan;
                    }));
                }
                else
                {
                    lb_VPPName.Text = SysPara.RecipeName + ".vpp";
                    lb_VPPName.BackColor = Color.DarkGreen;
                    label_ProductionVPPName.Text = sProductionProgramName + ".vpp";
                    label_ProductionVPPName.BackColor = Color.DarkCyan;
                }
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }


        private async Task<VisionResult?> fnRunVisionTest(VisionProgramType visionProgramType, string Cam, int CamPosition)
        {
            try
            {
                if (visionProgramType == VisionProgramType.RECIPE)
                {
                    //if (Cam == "Up")
                    //{
                        //TB_RecipeUpVPP.Inputs["CamPosition"].Value = CamPosition;
                        RunTB = (CogToolBlock)TB_RecipeUpVPP.Tools[TB_VPP_Recipe_Tool];
                    //}
                    //if (Cam == "Dw")
                    //{
                    //    TB_RecipeDwVPP.Inputs["CamPosition"].Value = CamPosition;
                    //    RunTB = (CogToolBlock)TB_RecipeDwVPP.Tools[TB_VPP_Recipe_Tool];
                    //}
                }
                else
                {
                    TB_ProductionVPP.Inputs["InputImage"].Value = (CogImage8Grey)ImgFlipTool.OutputImage;
                    RunTB = (CogToolBlock)TB_ProductionVPP.Tools[TB_VPP_Recipe_Tool];
                }
                RunTB.Run();
                //await Task.Run(() => RunTB.Run()).ConfigureAwait(false);

                if (RunTB.RunStatus.Result == CogToolResultConstants.Accept)
                {
                    VisionResult result = new VisionResult();
                    //if (CamPosition == 1)
                    //{
                      
                        result.X = (double)RunTB.Outputs["X"].Value;
                        result.Y = (double)RunTB.Outputs["Y"].Value;
                        result.U = (double)RunTB.Outputs["Angle"].Value;
                    result.ResultBool = (bool)RunTB.Outputs["ResultBool"].Value;
                    //}
                    //else
                    //{
                    //    result.X = (double)RunTB.Outputs["X"].Value;
                    //    result.Y = (double)RunTB.Outputs["Y"].Value;
                    //    result.ResultBool = (bool)RunTB.Outputs["Result"].Value;
                    //}
                    if (result.ResultBool)
                        return result;
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Get Lamp, 0 no light, 1 led light, 2 cell light
        /// </summary>
        public NPOutput getLamp(int iSelection)
        {
            switch (iSelection)
            {
                case 0:
                    return null;
                case 1:
                    return null;
                case 2:
                    return null;
                default:
                    return null;
            }
        }
        private double GetDegree(double rad)
        {
            double dTempU1 = (rad / Math.PI) * 180;
            return dTempU1;
        }
        private double GetRad(double Degree)
        {
            double dTempU1 = Degree * Math.PI / 180;
            return dTempU1;
        }
        /// <summary>
        /// Take an image from a camera.
        /// </summary>
        /// <param name="RecordDisplay">Where you want to show the image</param>
        /// <param name="sLamp">BL20 Light NPOutput in String</param>
        /// <param name="bDraw">Select if you want to draw the guide lines</param>
        /// <param name="bFit">If image must auto fit</param>
        public async Task GrabOneImage(bool bDraw, bool bFit)
        {
            try
            {
                string sProgram = "";

                //if (cmbCamera.InvokeRequired)
                //{
                //    cmbCamera.BeginInvoke((MethodInvoker)(() =>
                //    sProgram = cmbCamera.SelectedItem?.ToString()
                //    ));
                //}
                //else
                    sProgram = cmbCamera.SelectedItem?.ToString();
               //sProgram = "Up";
                int iSearch = getTableRowNumberByName(CameraVPPName, sProgram);

                if (iSearch > -1)
                {
                    DataRow row = PCameraSettings.Rows[iSearch];
                    string sCamera = row[CameraVPPName].ToString();
                    string sTool = row[ImageToolVPPName].ToString();
                    sCogCameraTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sCamera + ".VPP");
                    sImgFlipTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sTool + ".VPP");

                    if (CogCameraTool == null)
                        CogCameraTool = await Task.Run(() => (CogAcqFifoTool)CogSerializer.LoadObjectFromFile(sCogCameraTool_Path)).ConfigureAwait(false);

                    if (ImgFlipTool == null)
                        ImgFlipTool = await Task.Run(() => (CogIPOneImageTool)CogSerializer.LoadObjectFromFile(sImgFlipTool_Path)).ConfigureAwait(false);

                    //ClearCogRecordDisplay(cogRecordDisplay_RecipeEditor);
                    //ClearCogRecordDisplay(cogRecordDisplay_MachineState);

                    CogCameraTool.Operator.Flush();
                    await Task.Run(() => CogCameraTool.Run()).ConfigureAwait(false);

                    ImgFlipTool.InputImage = (CogImage8Grey)CogCameraTool.OutputImage;
                    await Task.Run(() => ImgFlipTool.Run()).ConfigureAwait(false);

                    cogRecordDisplay_RecipeEditor.Image = ImgFlipTool.OutputImage;
                    cogRecordDisplay_RecipeEditor.Image = (CogImage8Grey)ImgFlipTool.OutputImage;
                    cogRecordDisplay_MachineState.Image = ImgFlipTool.OutputImage;
                    cogRecordDisplay_MachineState.Image = (CogImage8Grey)ImgFlipTool.OutputImage;

                    if (bFit)
                    {
                        cogRecordDisplay_RecipeEditor.Fit();
                        cogRecordDisplay_MachineState.Fit();
                    }

                    #region Draw
                    if (bDraw && ImgFlipTool.OutputImage != null)
                    {
                        CogLine CogLineX = new CogLine();
                        CogLineX.Color = CogColorConstants.Yellow;
                        CogLineX.X = 0;
                        CogLineX.Y = ImgFlipTool.OutputImage.Height / 2;
                        CogLine CogLineY = new CogLine();
                        CogLineY.Color = CogColorConstants.Yellow;
                        CogLineY.Rotation = GetRad(90);
                        CogLineY.X = ImgFlipTool.OutputImage.Width / 2;
                        CogLineY.Y = 0;
                        CogCircle CogCircleCenter = new CogCircle();
                        CogCircleCenter.Color = CogColorConstants.Green;
                        CogCircleCenter.CenterX = ImgFlipTool.OutputImage.Width / 2;
                        CogCircleCenter.CenterY = ImgFlipTool.OutputImage.Height / 2;

                        if (dialogModal.Visible)
                            nRadiusImageDisplay = (int)dialogModal.nUP_CircleDiam.Value;

                        CogCircleCenter.Radius = nRadiusImageDisplay;
                        cogRecordDisplay_RecipeEditor.InteractiveGraphics.Add(CogLineX, "", true);
                        cogRecordDisplay_RecipeEditor.InteractiveGraphics.Add(CogLineY, "", true);
                        cogRecordDisplay_RecipeEditor.InteractiveGraphics.Add(CogCircleCenter, "", true);
                        cogRecordDisplay_MachineState.InteractiveGraphics.Add(CogLineX, "", true);
                        cogRecordDisplay_MachineState.InteractiveGraphics.Add(CogLineY, "", true);
                        cogRecordDisplay_MachineState.InteractiveGraphics.Add(CogCircleCenter, "", true);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la imagen de la camara:\n" + ex.ToString());
            }
        }
        public void fnUpdateRadius2Display(int nNewRadius)
        {
            nRadiusImageDisplay = nNewRadius;
        }
        /// <summary>
        /// Stop live display, clear interactive graphics and clear static graphics of a cogRecordDisplay.
        /// </summary>
        private void ClearCogRecordDisplay(CogRecordDisplay xcogRecordDisplay)
        {
            xcogRecordDisplay.StopLiveDisplay();
            xcogRecordDisplay.InteractiveGraphics.Clear();
            xcogRecordDisplay.StaticGraphics.Clear();
            xcogRecordDisplay.ClearImage8GreyColorMap();
            xcogRecordDisplay.ClearImage16GreyColorMap();
            xcogRecordDisplay.ClearImage16RangeColorMap();
        }

        public ICogRecord getImageFromSubRecord(CogToolBlock CogToolBlock_TB, int iPosition)
        {
            var LastRunRecord = CogToolBlock_TB.CreateLastRunRecord();
            var LastRunRecordCreated = CogToolBlock_TB.CreateLastRunRecord();
            return LastRunRecord.SubRecords.Count > 0 ? LastRunRecord.SubRecords[iPosition] : null;
        }


        private string fnGetVPPName(VisionProgramType VPT, string sProgramSelection)
        {
            DataSet _DataSet = (VPT == VisionProgramType.RECIPE) ? RecipeData : SettingData;
            DataTable _DataTable = (VPT == VisionProgramType.RECIPE) ? RVisionDataTable : PVisionDataTable;
            DataColumn _DataColumn_Name = (VPT == VisionProgramType.RECIPE) ? PName : PSName;
            DataColumn _DataColumn_VPPName = (VPT == VisionProgramType.RECIPE) ? VPPName : PSVPPName;
            int iSearch = getTableRowNumberByName(_DataColumn_Name, sProgramSelection);

            if (iSearch > -1)
            {
                DataRow row = _DataTable.Rows[iSearch];
                string sVPPName = row[_DataColumn_VPPName].ToString();
                return sVPPName;
            }
            else
                MessageBox.Show("No se encontro el programa " + sProgramSelection + " dado de alta en la tabla '" + _DataTable.TableName + "'");

            return null;
        }

        /// <summary>
        /// Select program of the dropdown to inspect inside the main ToolBlock and runs.
        /// </summary>
        public async Task<VisionResult?> DoVisionPositionShot(VisionProgramType VPT, string sProgramSelection, NPOutput sLamp, bool DrawResult, bool bInputImageNeeded, string Cam, int CamPosition = 1)
        {
            string vppName = fnGetVPPName(VPT, sProgramSelection);

            if (vppName != null)
                TB_VPP_Recipe_Tool = vppName;
            else
                return null;

            if (Cam == "Up")
            {
                // LightTurnOn();
                fnControlLights(LampSelection.GantryUp, ColorLight.RED);
            }
            else
            {
                // DwLightTurnOn();
                fnControlLights(LampSelection.GantryDown, ColorLight.WHITE);
            }

            //Trigger Camera current values
            VisionResult? result = await fnRunVisionTest(VPT, Cam, CamPosition).ConfigureAwait(false);
            fnClearImages();
            if (Cam == "Up")
            {
                fnControlLights(LampSelection.GantryUp, ColorLight.OFF);
                //LightTurnOff();
            }
            else
            {
                //DwLightTurnOff();
                fnControlLights(LampSelection.GantryDown, ColorLight.OFF);
            }
            if (result != null)
            {
                fnUpdateLabels(VPT, result.Value);
                //count offset values
                VisionResult? LastOFfset = GetResultCalculation(VPT, result.Value, sProgramSelection);
                fnShowImages(0);

                #region Draw Results
                if (DrawResult && LastOFfset != null)
                    fndrawResult(LastOFfset.Value);
                #endregion

                return LastOFfset;
            }
            else
                fnShowImages(0);

            return null;
        }

        private void fnClearImages()
        {
            try
            {
                cogRecordDisplay_RecipeEditor.Image = null;
                cogRecordDisplay_RecipeEditor.StaticGraphics.Clear();
                cogRecordDisplay_RecipeEditor.InteractiveGraphics.Clear();
            }
            catch (Exception)
            {

            }

            try
            {
                cogRecordDisplay_MachineState.Image = null;
                cogRecordDisplay_MachineState.StaticGraphics.Clear();
                cogRecordDisplay_MachineState.InteractiveGraphics.Clear();
            }
            catch (Exception)
            {

            }
        }

        private void fnUpdateLabels(VisionProgramType VPT, VisionResult Result_VPP)
        {
            if (VPT == VisionProgramType.RECIPE)
            {
                if (lbInspectionX.InvokeRequired)
                {
                    lbInspectionX.BeginInvoke((MethodInvoker)(() =>
                    {
                        lbInspectionX.Text = Convert.ToString(Math.Round(Result_VPP.X, 6));
                        lbInspectionY.Text = Convert.ToString(Math.Round(Result_VPP.Y, 6));
                        lbInspectionU.Text = Convert.ToString(Math.Round(Result_VPP.U, 6));
                    }));
                }
                else
                {
                    lbInspectionX.Text = Convert.ToString(Math.Round(Result_VPP.X, 6));
                    lbInspectionY.Text = Convert.ToString(Math.Round(Result_VPP.Y, 6));
                    lbInspectionU.Text = Convert.ToString(Math.Round(Result_VPP.U, 6));
                }
            }
            else
            {
                if (label_Production_ResultPixelsX.InvokeRequired)
                {
                    label_Production_ResultPixelsX.BeginInvoke((MethodInvoker)(() =>
                    {
                        label_Production_ResultPixelsX.Text = Convert.ToString(Math.Round(Result_VPP.X, 6));
                        label_Production_ResultPixelsY.Text = Convert.ToString(Math.Round(Result_VPP.Y, 6));
                        label_Production_ResultDegU.Text = Convert.ToString(Math.Round(Result_VPP.U, 6));
                    }));
                }
                else
                {
                    label_Production_ResultPixelsX.Text = Convert.ToString(Math.Round(Result_VPP.X, 6));
                    label_Production_ResultPixelsY.Text = Convert.ToString(Math.Round(Result_VPP.Y, 6));
                    label_Production_ResultDegU.Text = Convert.ToString(Math.Round(Result_VPP.U, 6));
                }
            }
        }

        private void fnShowImages(int iImageIndex = 0)
        {
            try
            {
                cogRecordDisplay_RecipeEditor.Record = getImageFromSubRecord(RunTB, iImageIndex);
                cogRecordDisplay_RecipeEditor.AutoFit = true;
                cogRecordDisplay_RecipeEditor.Fit();

            }
            catch (Exception)
            {

            }

            try
            {
                cogRecordDisplay_MachineState.Record = getImageFromSubRecord(RunTB, iImageIndex);
                cogRecordDisplay_MachineState.AutoFit = true;
                cogRecordDisplay_MachineState.Fit();
            }
            catch (Exception)
            {

            }
        }



        private void fnShowImages1(int iImageIndex = 0)
        {
            try
            {
                cogRecordDisplay_RecipeEditor.Record = getImageFromSubRecord(CalibTB, iImageIndex);
                cogRecordDisplay_RecipeEditor.AutoFit = true;
                cogRecordDisplay_RecipeEditor.Fit();

            }
            catch (Exception)
            {

            }

            try
            {
                cogRecordDisplay_MachineState.Record = getImageFromSubRecord(CalibTB, iImageIndex);
                cogRecordDisplay_MachineState.AutoFit = true;
                cogRecordDisplay_MachineState.Fit();
            }
            catch (Exception)
            {

            }
        }
        private void fndrawResult(VisionResult LastOffset)
        {


            try
            {
                CogGraphicLabel clX = new CogGraphicLabel();
                clX.X = 400;
                clX.Y = cogRecordDisplay_RecipeEditor.Image.Height - 250;
                clX.Text = (LastOffset.X >= 0) ? "X: " + string.Format("{0:+00000.000}", LastOffset.X) : "X: " + string.Format("{0:-00000.000}", LastOffset.X);
                cogRecordDisplay_RecipeEditor.InteractiveGraphics.Add(clX, "", true);

                CogGraphicLabel clY = new CogGraphicLabel();
                clY.X = 400;
                clY.Y = cogRecordDisplay_RecipeEditor.Image.Height - 150;
                clY.Text = (LastOffset.Y >= 0) ? "Y: " + string.Format("{0:+00000.000}", LastOffset.Y) : "Y: " + string.Format("{0:-00000.000}", LastOffset.Y);
                cogRecordDisplay_RecipeEditor.InteractiveGraphics.Add(clY, "", true);

                CogGraphicLabel clU = new CogGraphicLabel();
                clU.X = 400;
                clU.Y = cogRecordDisplay_RecipeEditor.Image.Height - 50;
                clU.Text = (LastOffset.U >= 0) ? "U: " + string.Format("{0:+00000.000}", LastOffset.U) : "U: " + string.Format("{0:-00000.000}", LastOffset.U);
                cogRecordDisplay_RecipeEditor.InteractiveGraphics.Add(clU, "", true);
            }
            catch (Exception)
            {

            }

            try
            {
                CogGraphicLabel clX = new CogGraphicLabel();
                clX.X = 400;
                clX.Y = cogRecordDisplay_MachineState.Image.Height - 250;
                clX.Text = (LastOffset.X >= 0) ? "X: " + string.Format("{0:+00000.000}", LastOffset.X) : "X: " + string.Format("{0:-00000.000}", LastOffset.X);
                cogRecordDisplay_MachineState.InteractiveGraphics.Add(clX, "", true);

                CogGraphicLabel clY = new CogGraphicLabel();
                clY.X = 400;
                clY.Y = cogRecordDisplay_MachineState.Image.Height - 150;
                clY.Text = (LastOffset.Y >= 0) ? "Y: " + string.Format("{0:+00000.000}", LastOffset.Y) : "Y: " + string.Format("{0:-00000.000}", LastOffset.Y);
                cogRecordDisplay_MachineState.InteractiveGraphics.Add(clY, "", true);

                CogGraphicLabel clU = new CogGraphicLabel();
                clU.X = 400;
                clU.Y = cogRecordDisplay_MachineState.Image.Height - 50;
                clU.Text = (LastOffset.U >= 0) ? "U: " + string.Format("{0:+00000.000}", LastOffset.U) : "U: " + string.Format("{0:-00000.000}", LastOffset.U);
                cogRecordDisplay_MachineState.InteractiveGraphics.Add(clU, "", true);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Calculate offset by inspection position.
        /// </summary>
        private VisionResult? GetResultCalculation(VisionProgramType VPT, VisionResult Result_VPP, string sProgramSelection)
        {
            #region Configuration
            DataSet _DataSet = (VPT == VisionProgramType.RECIPE) ? RecipeData : SettingData;
            DataTable _DataTable = (VPT == VisionProgramType.RECIPE) ? RVisionDataTable : PVisionDataTable;
            DataColumn _DataColumn_Name = (VPT == VisionProgramType.RECIPE) ? PName : PSName;
            DataColumn _DataColumn_PxXmmX = (VPT == VisionProgramType.RECIPE) ? PxXmmX : PSPxXmmX;
            DataColumn _DataColumn_PxXmmY = (VPT == VisionProgramType.RECIPE) ? PxXmmY : PSPxXmmY;
            DataColumn _DataColumn_TeachPxX = (VPT == VisionProgramType.RECIPE) ? TeachPxX : PSTeachPxX;
            DataColumn _DataColumn_TeachPxY = (VPT == VisionProgramType.RECIPE) ? TeachPxY : PSTeachPxY;
            DataColumn _DataColumn_TeachPxU = (VPT == VisionProgramType.RECIPE) ? TeachPxU : PSTeachPxU;
            DataColumn _DataColumn_OffsetmmX = (VPT == VisionProgramType.RECIPE) ? OffsetmmX : PSOffsetmmX;
            DataColumn _DataColumn_OffsetmmY = (VPT == VisionProgramType.RECIPE) ? OffsetmmY : PSOffsetmmY;
            DataColumn _DataColumn_OffsetmmU = (VPT == VisionProgramType.RECIPE) ? OffsetmmU : PSOffsetmmU;
            DataColumn _DataColumn_TotalmmX = (VPT == VisionProgramType.RECIPE) ? TotalmmX : PSTotalmmX;
            DataColumn _DataColumn_TotalmmY = (VPT == VisionProgramType.RECIPE) ? TotalmmY : PSTotalmmY;
            DataColumn _DataColumn_TotalmmU = (VPT == VisionProgramType.RECIPE) ? TotalmmU : PSTotalmmU;
            DataColumn _DataColumn_ResultBool = (VPT == VisionProgramType.RECIPE) ? ResultBool : PSResultBool;
            DataColumn _DataColumn_ResultDouble = (VPT == VisionProgramType.RECIPE) ? ResultDouble : PSResultDouble;
            DataColumn _DataColumn_ResultString = (VPT == VisionProgramType.RECIPE) ? ResultString : PSResultString;
            #endregion Configuration

            int iSearch = getTableRowNumberByName(_DataColumn_Name, sProgramSelection);

            if (iSearch > -1)
            {
                DataRow row = _DataTable.Rows[iSearch];
                double dResolutionX = (double)row[_DataColumn_PxXmmX];
                double dResolutionY = (double)row[_DataColumn_PxXmmY];
                double dTeachX = (double)row[_DataColumn_TeachPxX];
                double dTeachY = (double)row[_DataColumn_TeachPxY];
                double dTeachU = (double)row[_DataColumn_TeachPxU];

                row[_DataColumn_TotalmmX] = Math.Round((((Math.Round(Result_VPP.X, 5) - dTeachX) / dResolutionX) * 1) + (double)row[_DataColumn_OffsetmmX], 4);
                row[_DataColumn_TotalmmY] = Math.Round((((Math.Round(Result_VPP.Y, 5) - dTeachY) / dResolutionY) * 1) + (double)row[_DataColumn_OffsetmmY], 4);
                row[_DataColumn_TotalmmU] = Math.Round((Math.Round(Result_VPP.U, 5) - dTeachU) + (double)row[_DataColumn_OffsetmmU], 4);
                row[_DataColumn_ResultBool] = Result_VPP.ResultBool;
                //row[_DataColumn_ResultDouble] = Result_VPP.ResultDouble;
                //row[_DataColumn_ResultString] = Result_VPP.Description;

                VisionResult LastOffset = new VisionResult();
                LastOffset.X = (double)row[_DataColumn_TotalmmX];
                LastOffset.Y = (double)row[_DataColumn_TotalmmY];
                LastOffset.U = (double)row[_DataColumn_TotalmmU];
                LastOffset.ResultBool = (bool)row[_DataColumn_ResultBool];
                //LastOffset.ResultDouble = (double)row[_DataColumn_ResultDouble];
                //LastOffset.Description = row[_DataColumn_ResultString].ToString();
                fn_PrintLogVision("Inspection " + sProgramSelection, LastOffset);

                if (VPT == VisionProgramType.RECIPE)
                    WriteRecipeData(Path.Combine(SysPara.RecipeDataDirectory, SysPara.RecipeName + ".xml"));
                else
                    WriteSettingData();

                return LastOffset;
            }
            else
                MessageBox.Show("No se encontro el programa " + sProgramSelection + " dado de alta en la tabla '" + _DataTable.TableName + "'");

            return null;
        }
        private void fn_PrintLogVision(string preString, VisionResult p1)
        {
            string strLog = preString + " | ";
            strLog += (" X: " + p1.X.ToString("f5"));
            strLog += (", Y: " + p1.Y.ToString("f5"));
            strLog += (", U: " + p1.Y.ToString("f3"));
            //MiddleLayer.LogF.AddLog(FunctionForms.LogForm.LogType.VISION, strLog);
        }
        /// <summary>
        /// Get vision result by name
        /// </summary>
        public VisionResult getVisionResult(VisionProgramType VPT, string sInspectionName)
        {
            #region Configuration
            DataSet _DataSet = (VPT == VisionProgramType.RECIPE) ? RecipeData : SettingData;
            DataTable _DataTable = (VPT == VisionProgramType.RECIPE) ? RVisionDataTable : PVisionDataTable;
            DataColumn _DataColumn_Name = (VPT == VisionProgramType.RECIPE) ? PName : PSName;
            DataColumn _DataColumn_TotalmmX = (VPT == VisionProgramType.RECIPE) ? TotalmmX : PSTotalmmX;
            DataColumn _DataColumn_TotalmmY = (VPT == VisionProgramType.RECIPE) ? TotalmmY : PSTotalmmY;
            DataColumn _DataColumn_TotalmmU = (VPT == VisionProgramType.RECIPE) ? TotalmmU : PSTotalmmU;
            DataColumn _DataColumn_ResultBool = (VPT == VisionProgramType.RECIPE) ? ResultBool : PSResultBool;
            DataColumn _DataColumn_ResultDouble = (VPT == VisionProgramType.RECIPE) ? ResultDouble : PSResultDouble;
            DataColumn _DataColumn_ResultString = (VPT == VisionProgramType.RECIPE) ? ResultString : PSResultString;
            #endregion Configuration
            VisionResult Result = new VisionResult();
            int iTotalPrograms = _DataTable.Rows.Count;
            int iSearch = -1;

            for (int i = 0; i < iTotalPrograms; i++)
            {
                DataRow rowi = _DataTable.Rows[i];
                string sName = rowi[_DataColumn_Name].ToString();

                if (sName == sInspectionName)
                {
                    iSearch = i;
                    break;
                }
            }

            if (iSearch > -1)
            {
                DataRow row = _DataTable.Rows[iSearch];
                Result.X = (double)row[_DataColumn_TotalmmX];
                Result.Y = (double)row[_DataColumn_TotalmmY];
                Result.U = (double)row[_DataColumn_TotalmmU];
                Result.ResultBool = (bool)row[_DataColumn_ResultBool];
                Result.ResultDouble = (double)row[_DataColumn_ResultDouble];
                Result.Description = row[_DataColumn_ResultString].ToString();
            }

            return Result;
        }

        public string fnGetVppName(VisionProgramType VPT, int iProgramIndex)
        {
            if (iProgramIndex == 1000)
                return "Programa no válido";

            DataSet _DataSet = (VPT == VisionProgramType.RECIPE) ? RecipeData : SettingData;
            DataTable _DataTable = (VPT == VisionProgramType.RECIPE) ? RVisionDataTable : PVisionDataTable;
            DataColumn _DataColumn_Name = (VPT == VisionProgramType.RECIPE) ? PName : PSName;
            DataRow rowi = _DataTable.Rows[iProgramIndex];
            string sName = rowi[_DataColumn_Name].ToString();
            return sName;
        }
        #endregion Vision

        #region Images
        /// <summary>
        /// Save an image to Path
        /// </summary>
        public async void fnSaveImage(ICogImage _Image, string sName)
        {
            if (_Image != null)
            {
                string sPath = GetSettingValue(PSet.TableName, ImageSavePath.ColumnName);

                if (!Directory.Exists(sPath))
                    Directory.CreateDirectory(sPath);

                string sDir_Path = Path.Combine(sPath, sName + ".jpeg");

                await Task.Run(() =>
                {
                    try
                    {
                        _Image.ToBitmap().Save(sDir_Path);
                    }
                    catch (Exception ex)
                    {

                    }
                }).ConfigureAwait(false);
                //Deletes OldFiles keppeing just las 120 items
                //fn_CleanFiles(); 
            }

        }
        public async void fn_CleanFiles()
        {
            string sPath = GetSettingValue(PSet.TableName, ImageSavePath.ColumnName);

            await Task.Run(() =>
            {
                var directory = new DirectoryInfo(sPath);
                var query = directory.GetFiles("*.jpeg", SearchOption.AllDirectories);
                int nArchivosADejar = 120; //Total Files

                foreach (var file in query.OrderByDescending(file => file.CreationTime).Skip(nArchivosADejar))
                {
                    file.Delete();
                }
            });
        }
        private void buttonLocalPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Please select the Unit Save folder";

            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
                SettingData.Tables[PSet.TableName].Rows[0][ImageSavePath.ColumnName] = dialog.SelectedPath;
        }
        #endregion Images

        #region Results
        public void clearAllRecipeResults()
        {
            int iTotalRows = RecipeData.Tables[RVisionDataTable.TableName].Rows.Count;

            try
            {
                for (int i = 0; i < iTotalRows - 1; i++)
                {
                    DataRow row = RecipeData.Tables[RVisionDataTable.TableName].Rows[i];
                    row[ResultBool.ColumnName] = false;
                    row[ResultDouble.ColumnName] = 0;
                    row[ResultString.ColumnName] = "";
                }

                WriteRecipeData(Path.Combine(SysPara.RecipeDataDirectory, SysPara.RecipeName + ".xml"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void clearAllProductionResults()
        {
            int iTotalRows = SettingData.Tables[PVisionDataTable.TableName].Rows.Count;

            try
            {
                for (int i = 0; i < iTotalRows - 1; i++)
                {
                    DataRow row = SettingData.Tables[PVisionDataTable.TableName].Rows[i];
                    row[PSResultBool.ColumnName] = false;
                    row[PSResultDouble.ColumnName] = 0;
                    row[PSResultString.ColumnName] = "";
                }

                WriteSettingData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion Results

        #region GUI
        /// <summary>
        /// Refresh the ComboBox's content
        /// </summary>
        /// <param name="_ComboBox">Selected ComboBox</param>
        /// <param name="_DataSet">Selected DataSet</param>
        /// <param name="_DataTable">Selected DataTable from DataSet</param>
        /// <param name="_DataColumn">Selected DataColumn from DataTable</param>
        /// <param name="bClear">True if ComboBox must be cleared before adding the data</param>
        private void doRefreshComboBoxList(ComboBox _ComboBox, DataSet _DataSet, DataTable _DataTable, DataColumn _DataColumn, bool bClear)
        {
            try
            {
                int iSelectedIndex = _ComboBox.SelectedIndex;

                if (bClear)
                    _ComboBox.Items.Clear();

                int iTotalData = _DataTable.Rows.Count;

                if (iTotalData > 0)
                {
                    for (int i = 0; i < iTotalData; i++)
                    {
                        DataRow row = _DataTable.Rows[i];
                        string sData = row[_DataColumn].ToString();
                        _ComboBox.Items.Add(sData);
                    }

                    if (_ComboBox.Items.Count > 0 && iSelectedIndex > -1 && iSelectedIndex < _ComboBox.Items.Count)
                    {
                        _ComboBox.SelectedIndex = iSelectedIndex;
                        _ComboBox.Text = _ComboBox.SelectedItem.ToString();
                        return;
                    }
                    else
                        _ComboBox.SelectedIndex = -1;
                }

                _ComboBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Refresh ComboBox:\n" + ex.ToString());
            }
        }
        private async void comboBox_Camera_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sProgram = cmbCamera.SelectedItem.ToString();
            int iSearch = getTableRowNumberByName(CameraVPPName, sProgram);

            if (iSearch > -1)
            {
                try
                {
                    //DataRow row = PCameraSettings.Rows[iSearch];
                    //string sCamera = row[CameraVPPName].ToString();
                    //string sTool = row[ImageToolVPPName].ToString();
                    //sCogCameraTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sCamera + ".VPP");
                    //sImgFlipTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sTool + ".VPP");
                    //CogCameraTool = await Task.Run(() => (CogAcqFifoTool)CogSerializer.LoadObjectFromFile(sCogCameraTool_Path)).ConfigureAwait(false);
                    //ImgFlipTool = await Task.Run(() => (CogIPOneImageTool)CogSerializer.LoadObjectFromFile(sImgFlipTool_Path)).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se puede cargar configuracion de la camara:\n" + ex.ToString());
                }
            }
        }
        #endregion GUI

        private async Task<(bool, string)> fnCameraConection(ICogFrameGrabber camera)
        {
            bool bResult = false;
            return await Task.Run(() =>
            {
                try
                {
                    var ok = camera.AvailableVideoFormats; //Si no detecta camara no se muestran los formatos disponibles de video y entra excepcion
                    bResult = true;
                }
                catch
                {
                    bResult = false;
                }
                finally
                {
                    camera.Disconnect(true);
                }
                return (bResult, $"{camera.Name} IP:{camera.OwnedGigEAccess.CurrentIPAddress}");
            });
        }
        #endregion GENERAL FUNCTIONS

        #region Initialize

        private FCResultType fc_In_VisionConnectionStart_FlowRun(object sender, EventArgs e)
        {
            SDKPara.Arm.ClearAlarm(cAlarm.I(fc_In_VisionConnectionStart));

            lTaskConnect.Clear();
            
            if (!MiddleLayer.VisionProRACF.bVisionEnabled2)
            {
                string _alarm = "Vision Gantry Deshabilitada desde Producttion Settings...";
                SDKPara.Arm.ShowAlarm(cAlarm.I(fc_In_VisionConnectionStart), _alarm);
                return FCResultType.CASE1;
            }


            return FCResultType.NEXT;
        }

        private FCResultType fc_In_VisionConnectionConnect_FlowRun(object sender, EventArgs e)
        {
            CogFrameGrabbers cameras = new CogFrameGrabbers();
            if (cameras.Count == 0)
            {
                return FCResultType.NEXT;
            }
            for (int i = 0; i < cameras.Count; i++)
            {
                ICogFrameGrabber cam = cameras[i]; //Se crea un grabber por camara detectada
                lTaskConnect.Add(fnCameraConection(cam)); //Se ejecuta tarea de conexion
            }
            return FCResultType.NEXT;
        }
        private FCResultType fc_In_VisionConnectionOk_FlowRun(object sender, EventArgs e)
        {
            bool bAllCamerasOk = true;

            if (lTaskConnect.Count == 0) //Si no se detecto ninguna camara
            {
                SDKKernal.ShowAlarm(cAlarm.E(fc_In_VisionConnectionStart), "No se detecto ninguna camara  conectada");
                return FCResultType.IDLE;
            }
            Task t = Task.WhenAll(lTaskConnect);
            if (t.IsCompleted)
            {
                for (int i = 0; i < lTaskConnect.Count; i++)
                {
                    if (!lTaskConnect[i].Result.Item1)
                    {
                        string sCamName = lTaskConnect[i].Result.Item2;
                        SDKKernal.ShowAlarm(cAlarm.E(fc_In_VisionConnectionStart), $"Falla al conectar camara {sCamName}");
                        bAllCamerasOk = false;
                    }

                }
                if (!bAllCamerasOk)
                    return FCResultType.IDLE;

                //if (cmbCamera.Items.Count < lTaskConnect.Count)
                //{
                //    SDKKernal.ShowAlarm(cAlarm.E(fc_In_VisionConnectionStart), $"Error conexión Camaras detectadas:{lTaskConnect.Count} y en Acura existen {cmbCamera.Items.Count} Camaras registradas");
                //    return FCResultType.IDLE;
                //}
                SDKKernal.ClearAlarm(cAlarm.E(fc_In_VisionConnectionStart));
                return FCResultType.NEXT;
            }

            return FCResultType.IDLE;

        }

        private FCResultType fc_In_VisionConnectionFinish_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = true;
            return FCResultType.IDLE;
        }
        #endregion Initialize

        #region PRODUCTION SETTINGS INTERFACE
        private async void button_Production_RunVision_Click(object sender, EventArgs e)
        {
            bool bCogAdquireImage = false;

            if (comboBox_Production_Light.SelectedIndex > -1 && comboBox_Production_Program.SelectedIndex > -1)
                await DoVisionPositionShot(VisionProgramType.PRODUCTION, comboBox_Production_Program.SelectedItem.ToString(), getLamp(comboBox_Production_Light.SelectedIndex), true, bCogAdquireImage, cmbCamera.Text);
        }
        private void button_Production_TeachSelected_Click(object sender, EventArgs e)
        {
            int iTotalPrograms = SettingData.Tables[PVisionDataTable.TableName].Rows.Count;
            int iSearch = -1;
            string sProgram = comboBox_Production_Program.SelectedItem.ToString();

            for (int i = 0; i < iTotalPrograms; i++)
            {
                DataRow row = PVisionDataTable.Rows[i];
                string sName = row[PSName].ToString();

                if (sName == sProgram)
                {
                    iSearch = i;
                    break;
                }
            }

            if (iSearch > -1)
            {
                DataRow row = PVisionDataTable.Rows[iSearch];
                row[PSTeachPxX] = Convert.ToDouble(label_Production_ResultPixelsX.Text);
                row[PSTeachPxY] = Convert.ToDouble(label_Production_ResultPixelsY.Text);
                row[PSTeachPxU] = Convert.ToDouble(label_Production_ResultDegU.Text);
                dataGridView_VisionData.Rows[iSearch].Cells[pSTeachPxXDataGridViewTextBoxColumn.Name].Style.BackColor = Color.LightGreen;
                dgvPosInspection.Rows[iSearch].Cells[pSTeachPxYDataGridViewTextBoxColumn.Name].Style.BackColor = Color.LightGreen;
                dgvPosInspection.Rows[iSearch].Cells[pSTeachPxUDataGridViewTextBoxColumn.Name].Style.BackColor = Color.LightGreen;
                MessageBox.Show("Teach " + sProgram);
            }
            else
                MessageBox.Show("No se encontro el programa " + sProgram + " dado de alta en la tabla '" + PVisionDataTable.TableName + "'");
        }
        private void tabPage_StaticPrograms_Enter(object sender, EventArgs e)
        {
            doRefreshComboBoxList(comboBox_Production_Program, SettingData, PVisionDataTable, PSName, true);
            RefreshCamList();

            if (panel_Image.Enabled)
            {
                panel_Image.Parent = panel_ProductionSettingImage;
                tableLayoutPanel3.Width = 56;
                panel33.Height = 26;
                panel33.Width = 824;
                cmbCamera.Height = 24;
                cmbCamera.Width = 164;
                panel_ProductionSettingImage.Refresh();
                tableLayoutPanel3.Width = 56;
                panel33.Height = 26;
                panel33.Width = 824;
                cmbCamera.Height = 24;
                cmbCamera.Width = 164;
            }
        }
        #endregion PRODUCTION SETTINGS INTERFACE

        #region RECIPE INTERFACE
        private void btnLive_Click(object sender, EventArgs e)
        {

            if (!Ctrl_CamLiveCaptureWorker.IsBusy)
            {
                try
                {
                    if (cmbCamera.Text == "Up")
                    {
                        fnControlLights(LampSelection.GantryUp, ColorLight.RED);
                    }
                    else
                    {
                        fnControlLights(LampSelection.GantryDown, ColorLight.WHITE);
                    }
                    Ctrl_CamLiveCaptureWorker.RunWorkerAsync(1);
                    btnGrabImage.Enabled = false;
                    btnGrabImage.Enabled = false;
                    btnEditVPRO.Enabled = false;
                    btnExVision.Enabled = false;
                    btnLiveImage.BackgroundImage = Properties.Resources.stop;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fallo al iniciar Live Image.\n" + ex.ToString());
                }
            }
            else
            {
                try
                {
                    if (cmbCamera.Text == "Up")
                    {
                        fnControlLights(LampSelection.GantryUp, ColorLight.OFF);
                    }
                    else
                    {
                        fnControlLights(LampSelection.GantryDown, ColorLight.OFF);
                    }
                    Ctrl_CamLiveCaptureWorker.CancelAsync();
                    btnGrabImage.Enabled = true;
                    btnEditVPRO.Enabled = true;
                    btnExVision.Enabled = true;
                    btnLiveImage.BackgroundImage = Properties.Resources.Play;
                    ClearCogRecordDisplay(cogRecordDisplay_RecipeEditor);
                    ClearCogRecordDisplay(cogRecordDisplay_MachineState);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fallo al terminar Live Image.\n" + ex.ToString());
                }
            }
        }

        private async void btnGrab_Click(object sender, EventArgs e)
        {
            #region Configuration
            int iSelecteditem = 0;

            switch (panel_Image.Parent.Name)
            {
                case "panel_RecipeEditorImage":
                    iSelecteditem = cbLights.SelectedIndex;
                    break;
                case "panel_ProductionSettingImage":
                    iSelecteditem = comboBox_Production_Light.SelectedIndex;
                    break;
                default:
                    iSelecteditem = 0;
                    break;
            }
            #endregion Configuration
            //NPOutput OBLamp = getLamp(iSelecteditem);
            LightTurnOn();
            //OBLamp?.On();

            try
            {
                //await GrabOneImage(true, false);
                //cogIPOneImageEditV21.Subject.InputImage = (CogImage8Grey)CogCameraTool.OutputImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede obtener snapshot");
            }

            //OBLamp?.Off();
            LightTurnOff();
        }
        private async void btnReloadTool_Click(object sender, EventArgs e)
        {
            await LoadVPP();
            //int iTotalRows = SettingData.Tables[PCameraSettings.TableName].Rows.Count;

            //if (iTotalRows > 0)
            //{
            //    DataRow row = SettingData.Tables[PCameraSettings.TableName].Rows[0];
            //    string sCamera = row[CameraVPPName.ColumnName].ToString();
            //    string sTool = row[ImageToolVPPName.ColumnName].ToString();
            //    sCogCameraTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sCamera + ".VPP");
            //    sImgFlipTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sTool + ".VPP");
            //    CogCameraTool = await Task.Run(() => (CogAcqFifoTool)CogSerializer.LoadObjectFromFile(sCogCameraTool_Path)).ConfigureAwait(false);
            //    ImgFlipTool = await Task.Run(() => (CogIPOneImageTool)CogSerializer.LoadObjectFromFile(sImgFlipTool_Path)).ConfigureAwait(false);
            //}
        }
        private async void btnEditVPRO_Click(object sender, EventArgs e)
        {
            #region Configuration
            int iSelecteditem = 0;
            VisionProgramType VPT = VisionProgramType.RECIPE;

            switch (panel_Image.Parent.Name)
            {
                case "panel_RecipeEditorImage":
                    iSelecteditem = cbLights.SelectedIndex;
                    VPT = VisionProgramType.RECIPE;
                    break;
                case "panel_ProductionSettingImage":
                    iSelecteditem = comboBox_Production_Light.SelectedIndex;
                    VPT = VisionProgramType.PRODUCTION;
                    break;
                default:
                    iSelecteditem = 0;
                    VPT = VisionProgramType.RECIPE;
                    break;
            }

            #endregion Configuration
            //NPOutput OBLamp = getLamp(iSelecteditem);
            //OBLamp?.On();

            try//TRYING TO READ SELECTED VPP IN THE FOLDER
            {
                await LoadVPP();
                int iTotalCameras = PCameraSettings.Rows.Count;
                int iSearch = -1;
                string sProgram = cmbCamera.SelectedItem.ToString();

                for (int i = 0; i < iTotalCameras; i++)
                {
                    DataRow rowi = PCameraSettings.Rows[i];
                    string sName = rowi[CameraVPPName].ToString();

                    if (sName == sProgram)
                    {
                        iSearch = i;
                        break;
                    }
                }

                if (iSearch > -1)
                {
                    //DataRow row = PCameraSettings.Rows[iSearch];
                    //string sCamera = row[CameraVPPName].ToString();
                    //string sTool = row[ImageToolVPPName].ToString();
                    //sCogCameraTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sCamera + ".VPP");
                    //sImgFlipTool_Path = Path.Combine(SysPara.VisionFileDirectory, "VPP", sTool + ".VPP");
                    //CogCameraTool = await Task.Run(() => (CogAcqFifoTool)CogSerializer.LoadObjectFromFile(sCogCameraTool_Path));
                    //ImgFlipTool = await Task.Run(() => (CogIPOneImageTool)CogSerializer.LoadObjectFromFile(sImgFlipTool_Path));

                    //if (VPT == VisionProgramType.RECIPE)
                    //    TB_RecipeUpVPP.Inputs["InputImage"].Value = (CogImage8Grey)ImgFlipTool.OutputImage;
                    //else
                    //    TB_ProductionVPP.Inputs["InputImage"].Value = (CogImage8Grey)ImgFlipTool.OutputImage;
                }
            }
            catch (Exception ex)
            {
                if (VPT == VisionProgramType.RECIPE)
                    MessageBox.Show("No se puede leer programa VPP de la siguiente ruta:\n" + TB_VPP_RecipeUp_Path + TB_VPP_RecipeDw_Path + "\n" + ex.ToString());
                else
                    MessageBox.Show("No se puede leer programa VPP de la siguiente ruta:\n" + TB_VPP_Production_Path + "\n" + ex.ToString());

                return;
            }

            if (VPT == VisionProgramType.RECIPE)
            {
                if (cmbCamera.Text == "Up")
                {
                    fnControlLights(LampSelection.GantryUp, ColorLight.RED);
                    if (TB_RecipeUpVPP != null)
                    {
                        if (cogRecordDisplay_RecipeEditor.LiveDisplayRunning)
                            cogRecordDisplay_RecipeEditor.StopLiveDisplay();

                        Edit_TB_Form EditForm = new Edit_TB_Form(TB_RecipeUpVPP, TB_VPP_RecipeUp_Path);
                        EditForm.ShowDialog();
                        TB_RecipeUpVPP = EditForm.EditTB;
                        EditForm.Dispose();
                    }
                    else
                        MessageBox.Show("El ToolBlock de la siguiente ruta regreso 'Null':\n" + TB_VPP_RecipeUp_Path);
                }
                if (cmbCamera.Text == "Dw")
                {
                    fnControlLights(LampSelection.GantryDown, ColorLight.WHITE);
                    if (TB_RecipeUpVPP != null)
                    {
                        if (cogRecordDisplay_RecipeEditor.LiveDisplayRunning)
                            cogRecordDisplay_RecipeEditor.StopLiveDisplay();

                        Edit_TB_Form EditForm = new Edit_TB_Form(TB_RecipeUpVPP, TB_VPP_RecipeUp_Path);
                        EditForm.ShowDialog();
                        TB_RecipeUpVPP = EditForm.EditTB;
                        EditForm.Dispose();
                    }
                    else
                        MessageBox.Show("El ToolBlock de la siguiente ruta regreso 'Null':\n" + TB_VPP_RecipeDw_Path);
                }

            }
            else
            {
                if (TB_ProductionVPP != null)
                {
                    if (cogRecordDisplay_RecipeEditor.LiveDisplayRunning)
                        cogRecordDisplay_RecipeEditor.StopLiveDisplay();

                    Edit_TB_Form EditForm = new Edit_TB_Form(TB_ProductionVPP, TB_VPP_Production_Path);
                    EditForm.ShowDialog();
                    TB_ProductionVPP = EditForm.EditTB;
                    EditForm.Dispose();
                }
                else
                    MessageBox.Show("El ToolBlock de la siguiente ruta regreso 'Null':\n" + TB_VPP_Production_Path);
            }

            //OBLamp?.Off();
            fnControlLights(LampSelection.GantryUp, ColorLight.OFF);
            fnControlLights(LampSelection.GantryDown, ColorLight.OFF);
        }
        private async void btnExVision_Click(object sender, EventArgs e)
        {
            bool bCogAdquireImage = false;
            int CamPosition;

            if (cbLights.SelectedIndex > -1 && cbSelectProcess.SelectedIndex > -1)
            {
                //if (cbSelectProcess.SelectedIndex == 0 || cbSelectProcess.SelectedIndex == 2)
                //{
                //    CamPosition = 1;
                //}
                //else
                //{
                //    CamPosition = 2;
                //}
                await DoVisionPositionShot(VisionProgramType.RECIPE, cbSelectProcess.SelectedItem.ToString(), getLamp(cbLights.SelectedIndex), true, bCogAdquireImage, cmbCamera.Text /*CamPosition*/);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado una configuracion valida");
            }

        }

        public override void IntoRecipeEditor()
        {
            RefreshCamList();
        }

        private void RefreshCamList()
        {
            cmbCamera.Items.Clear();
            int iTotalPrograms = PCameraSettings.Rows.Count;

            if (iTotalPrograms > 0)
            {
                for (int i = 0; i < iTotalPrograms; i++)
                {
                    DataRow row = PCameraSettings.Rows[i];
                    string sName = row[CameraVPPName].ToString();
                    cmbCamera.Items.Add(sName);
                }

                cmbCamera.SelectedIndex = 0;
                cmbCamera.Text = cmbCamera.SelectedItem.ToString();
            }
            else
                cmbCamera.Text = "";
        }
        private void Image_Enter(object sender, EventArgs e)
        {
            doRefreshComboBoxList(cbSelectProcess, RecipeData, RVisionDataTable, PName, true);
            RefreshCamList();

            if (panel_Image.Enabled)
            {
                panel_Image.Parent = panel_RecipeEditorImage;
                tableLayoutPanel3.Width = 56;
                panel33.Height = 26;
                panel33.Width = 824;
                cmbCamera.Height = 24;
                cmbCamera.Width = 164;
                panel_RecipeEditorImage.Refresh();
                tableLayoutPanel3.Width = 56;
                panel33.Height = 26;
                panel33.Width = 824;
                cmbCamera.Height = 24;
                cmbCamera.Width = 164;
            }
        }
        /// <summary>
        /// Get the values of last inspection and teach the selected program
        /// </summary>
        private void btnTeachSelectedProgram_Click(object sender, EventArgs e)
        {
            int iTotalPrograms = RVisionDataTable.Rows.Count;
            int iSearch = -1;
            string sProgram = cbSelectProcess.SelectedItem.ToString();

            for (int i = 0; i < iTotalPrograms; i++)
            {
                DataRow rowi = RVisionDataTable.Rows[i];
                string sName = rowi[PName].ToString();
                if (sName == sProgram)
                    iSearch = i;
            }

            if (iSearch > -1)
            {
                DataRow row = RVisionDataTable.Rows[iSearch];
                row[TeachPxX] = Convert.ToDouble(lbInspectionX.Text);
                row[TeachPxY] = Convert.ToDouble(lbInspectionY.Text);
                row[TeachPxU] = Convert.ToDouble(lbInspectionU.Text);
                dgvPosInspection.Rows[iSearch].Cells[teachPxXDataGridViewTextBoxColumn.Name].Style.BackColor = Color.LightGreen;
                dgvPosInspection.Rows[iSearch].Cells[teachPxYDataGridViewTextBoxColumn.Name].Style.BackColor = Color.LightGreen;
                dgvPosInspection.Rows[iSearch].Cells[teachPxUDataGridViewTextBoxColumn.Name].Style.BackColor = Color.LightGreen;
                MessageBox.Show("Teach " + sProgram);
            }
            else
                MessageBox.Show("No se encontro el programa " + sProgram + " dado de alta en la tabla de programas");
        }
        #endregion RECIPE INTERFACE

        #region Indirect Components
        private void Ctrl_CamLiveCaptureWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            e.Result = StartLiveCapture((int)e.Argument, bw, e);
        }
        private object StartLiveCapture(int argument, BackgroundWorker bw, DoWorkEventArgs e)
        {
            #region Configuration
            int iSelecteditem = 0;
            switch (panel_Image.Parent.Name)
            {
                case "panel_RecipeEditorImage":
                    if (cbLights.InvokeRequired)
                    {
                        cbLights.BeginInvoke((MethodInvoker)(() =>
                        iSelecteditem = cbLights.SelectedIndex
                        ));
                    }
                    else
                        iSelecteditem = cbLights.SelectedIndex;

                    break;
                case "panel_ProductionSettingImage":
                    if (comboBox_Production_Light.InvokeRequired)
                    {
                        comboBox_Production_Light.BeginInvoke((MethodInvoker)(() =>
                        iSelecteditem = comboBox_Production_Light.SelectedIndex
                        ));
                    }
                    else
                        iSelecteditem = comboBox_Production_Light.SelectedIndex;

                    break;
                default:
                    iSelecteditem = 0;
                    break;
            }


            #endregion Configuration
            if (JT_LiveImage.On(100))
            {
                //NPOutput OBLamp = getLamp(iSelecteditem);
                //OBLamp?.On();
                
                GrabOneImage(true, false).GetAwaiter().GetResult();

                if (bw.CancellationPending)
                {
                    //OBLamp?.Off();
                    e.Cancel = true;

                    if (btnGrabImage.InvokeRequired)
                    {
                        btnGrabImage.BeginInvoke((MethodInvoker)(() =>
                        btnGrabImage.Enabled = true
                        ));
                    }
                    else
                        btnGrabImage.Enabled = true;

                    if (btnLiveImage.InvokeRequired)
                    {
                        btnLiveImage.BeginInvoke((MethodInvoker)(() =>
                        btnLiveImage.BackgroundImage = Properties.Resources.Play
                        ));
                    }
                    else
                        btnLiveImage.BackgroundImage = Properties.Resources.Play;
                }
                else
                    StartLiveCapture(1, bw, e);
                return 1;
            }
            else
            {
                JT_LiveImage.Restart();
                return 1;
            }
        }
        public void fn_DialogoCamera()
        {
            dialogModal.fnInheritFromPanel(panel_Image, panel_RecipeEditorImage);
        }
        private void lb_VPPName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            fn_DialogoCamera();
        }



        #endregion

        #region LightControl





        private bool LightConnect()
        {
            long lRet = -1;
            lRet = OPTController.InitSerialPort(_ComPortName);

            if (0 != lRet)
            {
                LampUpConnected = false;
                return true;
            }
            else
            {
                LampUpConnected = true;

                return false;
            }

        }
        private void LightDisConnect()
        {

            long lRet = -1;
            lRet = OPTController.ReleaseSerialPort();
            if (0 != lRet)
            {
                //textBox_Status.Text = "Failed to release serial port";
                LampUpConnected = true;
            }
            else
                LampUpConnected = false;

        }
        private void LightTurnOn()
        {
            
          
        }

        private void UpLightWhiteTurnOn()
        {
           
        }

        private void UpLightBlueTurnOn()
        {
           

        }
        private void UpLightRedTurnOn()
        {
            

        }
        private void UpLightGreenTurnOn()
        {
           
        }

        private void DwLightTurnOn()
        {
            
        }

        private void LightTurnOff()
        {
           
        }

        private void UPLightRedTurnOff()
        {
            
        }
        private void DwLightTurnOff()
        {
            
        }
        #endregion

        #region Event
        private void btnLightOn_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryUp, ColorLight.RED);
        }
        private void btnLightOff_Click(object sender, EventArgs e)
        {
            UPLightRedTurnOff();

            // LightTurnOff();
        }

        private void btnDwLightOn_Click(object sender, EventArgs e)
        {
            DwLightTurnOn();
        }

        private void btnDwLightOff_Click(object sender, EventArgs e)
        {
            DwLightTurnOff();
        }


        #endregion

        public void Calib_Timer_Tick(object sender, EventArgs e)
        {
            //MiddleLayer.GantryF.CalibTimerTick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Calib_Timer.Enabled)
            {
                //MiddleLayer.GantryF.iCalibStep = 0;
                camera = cmbCamera.Text;
                Calib_Timer.Enabled = true;
            }

        }


        public void VisionRunCalib()
        {
            string strCalibName = "Calibrate";

            if (this.cmbCamera.Text == "Up")
            {

                CalibTB = (CogToolBlock)TB_RecipeUpVPP.Tools[strCalibName];
            }
            else
            {

                CalibTB = (CogToolBlock)TB_RecipeDwVPP.Tools[strCalibName];
            }
            CalibTB.Run();
            fnShowImages1(0);
            bTeachResult = false;
            bool tbResult = (bool)CalibTB.Outputs["Result"].Value;
            if (tbResult)
            {
                bTeachResult = true;
                Teach_Pick_X = (double)CalibTB.Outputs["X"].Value;
                Teach_Pick_Y = (double)CalibTB.Outputs["Y"].Value;
            }
            else
            {
                bTeachResult = false;
            }
        }

        private void btnUPLightGreenOn_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryUp, ColorLight.GREEN);
        }

        private void btnUPLightGreenOff_Click(object sender, EventArgs e)
        {

        }

        private void btnUPLightBlueOn_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryUp, ColorLight.BLUE);
        }

        private void btnUPLightBlueOff_Click(object sender, EventArgs e)
        {

        }

        private void btnUPLightWhiteOn_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryUp, ColorLight.WHITE);
        }

        private void btnUPLightWhiteOff_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryUp, ColorLight.OFF);
        }


        public void fnControlLights(LampSelection LampSelection, ColorLight ColorLight)
        {
            switch (LampSelection)
            {
                case LampSelection.GantryUp:
                    {
                        switch (ColorLight)
                        {
                            case ColorLight.RED:

                                {
                                    OPTController?.TurnOffChannel(_Channel);
                                    OPTController?.TurnOffChannel(_Channel + 1);
                                    OPTController?.TurnOnChannel(_Channel + 2);
                                    break;
                                }
                            case ColorLight.GREEN:

                                {
                                    OPTController?.TurnOnChannel(_Channel);
                                    OPTController?.TurnOffChannel(_Channel + 1);
                                    OPTController?.TurnOffChannel(_Channel + 2);
                                   
                                    break;
                                }
                            case ColorLight.BLUE:

                                {
                                    OPTController?.TurnOffChannel(_Channel);
                                    OPTController?.TurnOnChannel(_Channel + 1);
                                    OPTController?.TurnOffChannel(_Channel + 2);
                                    
                                    break;
                                }
                            case ColorLight.WHITE:

                                {
                                    OPTController?.TurnOnChannel(_Channel);
                                    OPTController?.TurnOnChannel(_Channel + 1);
                                    OPTController?.TurnOnChannel(_Channel + 2);
                                    break;
                                }
                            case ColorLight.OFF:

                                {
                                    OPTController?.TurnOffChannel(_Channel);
                                    OPTController?.TurnOffChannel(_Channel + 1);
                                    OPTController?.TurnOffChannel(_Channel + 2);
                                    break;
                                }
                        }
                        break;
                    }

                case LampSelection.GantryDown:
                    {
                        switch (ColorLight)
                        {
                            case ColorLight.RED:

                                {
                                    OPTController?.TurnOnChannel(_Channel2);
                                    OPTController?.TurnOffChannel(_Channel2 + 1);
                                    OPTController?.TurnOffChannel(_Channel2 + 2);
                                    break;
                                }
                            case ColorLight.GREEN:

                                {
                                    OPTController?.TurnOffChannel(_Channel2);
                                    OPTController?.TurnOnChannel(_Channel2 + 1);
                                    OPTController?.TurnOffChannel(_Channel2 + 2);

                                    break;
                                }
                            case ColorLight.BLUE:

                                {
                                    OPTController?.TurnOffChannel(_Channel2);
                                    OPTController?.TurnOffChannel(_Channel2 + 1);
                                    OPTController?.TurnOnChannel(_Channel2 + 2);
                                   
                                   
                                    break;
                                }
                            case ColorLight.WHITE:

                                {
                                    OPTController?.TurnOnChannel(_Channel2);
                                    OPTController?.TurnOnChannel(_Channel2 + 1);
                                    OPTController?.TurnOnChannel(_Channel2 + 2);
                                    break;
                                }
                            case ColorLight.OFF:

                                {
                                    OPTController?.TurnOffChannel(_Channel2);
                                    OPTController?.TurnOffChannel(_Channel2 + 1);
                                    OPTController?.TurnOffChannel(_Channel2 + 2);
                                    
                                    break;
                                }
                        }
                        
                    }
                    break;
            }
        }

        private void btnDWLightRedOn_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryDown, ColorLight.RED);
        }

        private void btnDWLightGreenOn_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryDown, ColorLight.GREEN);
        }

        private void btnDWLightBlueOn_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryDown, ColorLight.BLUE);
        }

        private void btnDWLightWhiteOn_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryDown, ColorLight.WHITE);
        }

        private void btnDWLightWhiteOff_Click(object sender, EventArgs e)
        {
            fnControlLights(LampSelection.GantryDown, ColorLight.OFF);
        }
    }


}
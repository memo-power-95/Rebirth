using Alpha.Classes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static Alpha.FunctionForms.LogForm;
//using static Alpha.Enums;
using tstCkBox;
using System.Data;
using Alpha._0.Classes;
using NPSDK;
using LogManager;

namespace Alpha.FunctionForms
{
    public partial class ProcessDataForm : Form
    {
        //Variables
        private DateTime dt_StartUnit = DateTime.Now;
        private DateTime dt_StartUnitUTC = DateTime.UtcNow;
        public Stopwatch swCycleTime = new Stopwatch();
        private Stopwatch swTaktTime = new Stopwatch();
        public uint nJidokaLimit { get; set; } = 0;
        public bool bJidokaEnabled { get; set; }
        public bool IsUnitProcesing { get;  set; }
        //Contadores de Proceso
        public ulong nCountGoodUnits { get; private set; } = 0;
        public ulong nTotalUnits { get; private set; } = 0;
        public ulong nJidokaCount { get; private set; } = 0;

        public ulong nPalletCount { get; private set; } = 0;

        public bool bEnableLOG { get; set; }
        public bool bEnableIOT { get; set; }
        public bool Jidoka_Alarmed { get { return nJidokaCount >= nJidokaLimit && bJidokaEnabled; } }
        public string CycleTime, StopTime, lbl_C;
        public enum ProcessState
        {
            Pasa,
            Falla,
            NA
        }
        public ProcessDataForm()
        {
            InitializeComponent();
            swCycleTime.Stop();
            swTaktTime.Stop();
        }
        public void fnTimersRunning(bool bStop)
        {

            if (!IsUnitProcesing)
                return;
            if (bStop)
            {
                swTaktTime.Start();
                swCycleTime.Start();
            }
            else
            {
                swTaktTime.Stop();
                swCycleTime.Stop();
            }
        }


        public Color ColorTheme
        {
            set { fnSetColor(value); }
        }

        public string sSerialNumber
        {
            set { lbl_Serial.SetText(value); }
            get { return lbl_Serial.Text; }
        }
        public string sTopName
        {
            set { lbl_TopName.SetText(value); }
            get { return lbl_TopName.Text; }
        }
        public string sModelo
        {
            get { return lbl_Model.Text; }
            set { lbl_Model.SetText(value); }
        }
        //public string sTitleModelo
        //{
        //    get { return lbl_TitleModel.Text; }
        //    set { lbl_TitleModel.SetText(value); }
        //}
        public string sTitleStatus
        {
            get { return lbl_TitleStatus.Text; }
            set { lbl_TitleStatus.SetText(value); }
        }
        public string Status
        {
            get { return lbl_Status.Text; }
            set { lbl_Status.SetText(value); }
        }
        public DateTime DateTimeStart
        {
            get { return dt_StartUnit; }
            private set
            {
                dt_StartUnit = value;
                dt_StartUnitUTC = DateTime.UtcNow;
            }
        }

        public void fnSetCycleTime()
        {
            lbl_CycleTime.SetText((swCycleTime.ElapsedMilliseconds / 1000.0).ToString("#.0"));
        }
        public void fnIndicateStartingUnit()
        {
            IsUnitProcesing = true;
            swCycleTime.Restart();
            swTaktTime.Start();
            lbl_Status.SetText("WAIT");
            lbl_Status.SetForeColor(Color.Black);
            //MiddleLayer.MesF.StartDateTime = DateTime.Now;
            dt_StartUnit = DateTime.Now;
            dt_StartUnitUTC = DateTime.UtcNow;
        }
        public void fnIndicateCancelUnit()
        {
            IsUnitProcesing = false;
            swCycleTime.Stop();
            lbl_Status.SetText("-");
            lbl_Status.SetForeColor(Color.Black);
        }
        //public void fnUpdateScrewResultTable(string torque, string angle)
        //{

        //    if (dgvProcessResults.InvokeRequired)
        //    {

        //    dgvProcessResults.BeginInvoke((MethodInvoker)(() => fnUpdateScrewResultTable(torque, angle)));
        //    }            
        //    else
        //    {

        //        DataGridViewRow row = dgvProcessResults.Rows[MiddleLayer.GantryF.uProcessIndex - 1];                
        //        row.Cells[dcTorque.Name].Value = torque + " Lb-in";
        //        row.Cells[dcAngle.Name].Value = angle + "°";
        //    }
        //}
        public void fnUpdateResult(ResultData resultData)
        {
            fnUpdateResult(resultData, 0);
            //string str = null;
            //if (dgvProcessResults.InvokeRequired)
            //    dgvProcessResults.BeginInvoke((MethodInvoker)(() => fnUpdateResult(resultData)));
            //else
            //{
            //    int index = dgvProcessResults.Rows.Add();
            //    DataGridViewRow row = dgvProcessResults.Rows[index];
            //    row.Cells[dcTime.Name].Value = resultData.Time.ToString();
            //    row.Cells[dcProductCode.Name].Value = resultData.ProductCode.ToString();
            //    row.Cells[dcPalletCode.Name].Value = resultData.PalletCode.ToString();
            //    row.Cells[dcPreasureDirver.Name].Value = resultData.PreasureDirver.ToString();
            //    row.Cells[dcPreasure.Name].Value = resultData.Preasure.ToString();
            //    row.Cells[dcPreasureLimit.Name].Value = resultData.PreasureMax.ToString();
            //    row.Cells[dc_abResult.Name].Value = resultData.abResult.ToString();
            //    row.Cells[dc_bcResult.Name].Value = resultData.bcResult;

            //    Color c1 = new Color();
            //    Color c2 = new Color();
            //    if (resultData.abResult == "OK")
            //        c1 = Color.Green;
            //    else if (resultData.abResult == "NG")
            //        c1 = Color.Red;
            //    else
            //        c1 = Color.Gray;

            //    if (resultData.bcResult == "OK")
            //        c2 = Color.Green;
            //    else if (resultData.bcResult == "NG")
            //        c2 = Color.Red;
            //    else
            //        c2 = Color.Gray;

            //    row.Cells[dc_abResult.Name].Style.BackColor = c1;
            //    row.Cells[dc_bcResult.Name].Style.BackColor = c2;
            //    str += resultData.Time.ToString() + "," + resultData.ProductCode.ToString() + "," +
            //        resultData.PalletCode.ToString() + "," +
            //         resultData.PreasureDirver.ToString() + "," + resultData.Preasure.ToString("f3") + "," +
            //        resultData.PreasureMax.ToString() + "," + resultData.abResult + "," + resultData.bcResult;
            //    SaveFile.SaveData(MiddleLayer.GantryF.GetSettingValue("PSet", "SaveImagePath"), SysPara.CsvHeader, str);
            //}
        }
        public void fnUpdateResult(ResultData resultData, int index)
        {
            //if (resultData.abResult == "NG")
            //    return;
            string str = null;
            if (dgvProcessResults.InvokeRequired)
                dgvProcessResults.BeginInvoke((MethodInvoker)(() => fnUpdateResult(resultData, index)));
            else
            {
                //int index = dgvProcessResults.Rows.Add();
                //DataGridViewRow row = dgvProcessResults.Rows[index];
                DataGridViewRow row;
                try
                {
                    dgvProcessResults.Rows.Insert(index, 1);
                    row = dgvProcessResults.Rows[index];
                }
                catch
                {
                    dgvProcessResults.Rows.Insert(0, 1);
                    row = dgvProcessResults.Rows[0];
                }

                row.Cells[dcTime.Name].Value = resultData.Time.ToString();
                row.Cells[dcProductCode.Name].Value = resultData.ProductCode.ToString();
                row.Cells[dcPalletCode.Name].Value = resultData.PalletCode.ToString();
                row.Cells[dcPreasureDirver.Name].Value = resultData.PreasureDirver.ToString();
                row.Cells[dcPreasure.Name].Value = resultData.Preasure.ToString();
                row.Cells[dcPreasureLimit.Name].Value = resultData.PreasureMax.ToString();
                row.Cells[dc_abResult.Name].Value = resultData.abResult.ToString();
                row.Cells[dc_bcResult.Name].Value = resultData.bcResult;

                Color c1 = new Color();
                Color c2 = new Color();
                if (resultData.abResult == "OK")
                    c1 = Color.Green;
                else if (resultData.abResult == "NG")
                    c1 = Color.Red;
                else
                    c1 = Color.Gray;

                if (resultData.bcResult == "OK")
                    c2 = Color.Green;
                else if (resultData.bcResult == "NG")
                    c2 = Color.Red;
                else
                    c2 = Color.Gray;

                row.Cells[dc_abResult.Name].Style.BackColor = c1;
                row.Cells[dc_bcResult.Name].Style.BackColor = c2;
                str += resultData.Time.ToString() + "," + resultData.ProductCode.ToString() + "," +
                    resultData.PalletCode.ToString() + "," +
                     resultData.PreasureDirver.ToString() + "," + resultData.Preasure.ToString("f3") + "," +
                    resultData.PreasureMax.ToString() + "," + resultData.abResult + "," + resultData.bcResult;
                SaveFile.SaveData(@"D:\EVMS\TP\LOG" , SysPara.CsvHeader, str);
            }
        }
        /// <summary>
        /// Remove result display from datagrid view
        /// 2023.1.27, zenan
        /// </summary>
        /// <param name="index">item index</param>
        public void fnRemoveResultAt(int index)
        {
            if (dgvProcessResults.InvokeRequired)
                dgvProcessResults.BeginInvoke((MethodInvoker)(() => fnRemoveResultAt(index)));
            else
            {
                if (dgvProcessResults == null || dgvProcessResults.Rows.Count < index)
                {
                    // do nothing
                }
                else
                {
                    try
                    {
                        dgvProcessResults.Rows.RemoveAt(index);
                    }
                    catch
                    { }
                }
                //for (int i = 0; i < MiddleLayer.GantryF.iProcessQty; i++)
                //{
                //    dgvProcessResults.Rows.Add();
                //    DataGridViewRow row = dgvProcessResults.Rows[i];
                //    row.Cells[dcScrewNumber.Name].Value = (i + 1).ToString();
                //}
            }
        }
        /// <summary>
        /// Remove last item from datagrid view
        /// 2023.1.27, zenan
        /// </summary>
        public void fnRemoveLastResult()
        {
            if (dgvProcessResults != null)
                fnRemoveResultAt(dgvProcessResults.Rows.Count - 1);
        }
        /// <summary>
        /// Get current rows count
        /// 2023.1.27, zenan
        /// </summary>
        public int fnRowsCount()
        {
            if (dgvProcessResults == null)
                return 0;
            else
                return dgvProcessResults.Rows.Count;
        }

        #region Table results
        public void fnInitilizeResultTable()
        {
            if (dgvProcessResults.InvokeRequired)
                dgvProcessResults.BeginInvoke((MethodInvoker)(() => fnInitilizeResultTable()));
            else
            {
                dgvProcessResults.Rows.Clear();
                //for (int i = 0; i < MiddleLayer.GantryF.iProcessQty; i++)
                //{
                //    dgvProcessResults.Rows.Add();
                //    DataGridViewRow row = dgvProcessResults.Rows[i];
                //    row.Cells[dcScrewNumber.Name].Value = (i + 1).ToString();
                //}
            }
        }

        //public void fnUpdateVisionResultTable(ProcessState state)
        //{
        //    if (dgvProcessResults.InvokeRequired)
        //        dgvProcessResults.BeginInvoke((MethodInvoker)(() => fnUpdateVisionResultTable(state)));
        //    else
        //    {
        //        DataGridViewRow row = dgvProcessResults.Rows[MiddleLayer.GantryF.uProcessIndex-1];
        //        row.Cells[dcVision.Name].Value = state.ToString(); ;

        //    }
        //}


        #endregion Table results

        public void fnShowSerial(string sSrialNumber)
        {
            lbl_Serial.SetText(sSrialNumber);
        }


        public void fnIndicateFinishedUnit()
        {
            lbl_TackT.SetText(swTaktTime.Elapsed.ToString("mm\\:ss\\.ff"));
            swTaktTime.Restart();
            swCycleTime.Stop();
            IsUnitProcesing = false;
            if (bEnableIOT)
                MiddleLayer.ProcessF.tmrTaktTime.Restart();
        }
        public void fnAddCountUnit(bool bPass, string sSerialNumber = "NOSERIAL", string defect = "", uint nTotalToCount = 1)
        {
            lbl_Serial.SetText(sSerialNumber);

            UpdateCount(bPass, nTotalToCount);
            lbl_Status.SetText(bPass ? "OK" : "NOK");
            lbl_Status.SetForeColor(bPass ? Color.Green : Color.Red);

            if (!bPass)
            {
                nJidokaCount++;
                lbl_TotalJidokaUnits.SetText(nJidokaCount.ToString());

                if (nJidokaCount >= nJidokaLimit)
                    lbl_TotalJidokaUnits.SetForeColor(Color.Red);
            }
            else
                fnResetJIDOKA();

            string _sModel = lbl_Model.Text != "" ? lbl_Model.Text : SysPara.RecipeName;
            string _sSerial = sSerialNumber != "NOSERIAL" ? sSerialNumber : lbl_Serial.Text;
            string _sStatus = (bPass ? "P" : "F");

            if (bEnableLOG)
                MiddleLayer.LogF.AddLog(LogType.Production, $"{_sModel};{_sSerial};{_sStatus};", true);

            if (bEnableIOT)
                AcuraIOT.AcuraCloudServices.AsyncIOTBoardHistory(_sSerial, _sModel, dt_StartUnitUTC, DateTime.UtcNow, _sStatus, defect, "");

           
        }
        public void fnResetJIDOKA()
        {
            lbl_TotalJidokaUnits.SetText("0");
            nJidokaCount = 0;
            lbl_TotalJidokaUnits.SetForeColor(Color.DarkOrange);
        }
        public void fnResetGENERAL()
        {
            lbl_Unit_Pass.SetText("0");
            lbl_Unit_Fail.SetText("0");
            lbl_Unit_ProductTotal.SetText("0");
            lbl_Unit_PalletTotal.SetText("0");
            nCountGoodUnits = 0;
            nTotalUnits = 0;
            nPalletCount = 0;
            fnResetJIDOKA();
        }
        private void fnSetColor(Color cl)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)(() => fnSetColor(cl)));
            else
            {
                //lbl_TopName.BackColor = cl;
                //lbl_TitleSerial.BackColor = cl;
                //lbl_TitleStatus.BackColor = cl;
                //lbl_TitleModel.BackColor = cl;
                //lbl_TitleCycleTime.BackColor = cl;
                //lbl_TitleTackTime.BackColor = cl;
                //lbl_TitleFPY.BackColor = cl;
                //lbl_TittleJidoka.BackColor = cl;
                //lbl_TitlePass.BackColor = cl;
                //lbl_TitleTotal.BackColor = cl;
            }
        }
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            fnResetJIDOKA();
        }
        private void btn_ResetGeneral_Click(object sender, EventArgs e)
        {
            fnResetGENERAL();
        }
        private void UpdateCount(bool bPass, uint UnitsCountToAdd = 1)
        {
            nTotalUnits += UnitsCountToAdd;
            lbl_Unit_ProductTotal.SetText(nTotalUnits.ToString());

            if (bPass)
            {
                nCountGoodUnits += UnitsCountToAdd;
                lbl_Unit_Pass.SetText(nCountGoodUnits.ToString());
                try
                {
                    LogManager.ResultHelper.WriteCount();
                }
                catch (Exception)
                { }
            }
            else
            {
                ulong nBads = nTotalUnits - nCountGoodUnits;
                lbl_Unit_Fail.SetText(nBads.ToString());
                try
                {
                    LogManager.ResultHelper.WriteCount(false);
                }
                catch (Exception)
                { }
            }

            float f_FPY = _CalcFPY(nCountGoodUnits, nTotalUnits);
            lbl_FPY.SetText($"{f_FPY.ToString("0.0")} %");
            DataTable dt = MiddleLayer.ProcessF.RecipeData.Tables["RSet"];
            dt.Rows[0]["OKCount"] = nCountGoodUnits;
            dt.Rows[0]["TotalCount"] = nTotalUnits;
            dt.AcceptChanges();
            MiddleLayer.ProcessF.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
        }

        public void InitCount()
        {
            nTotalUnits = MiddleLayer.ProcessF.GetRecipeValue("Rset", "TotalCount");
            nCountGoodUnits = MiddleLayer.ProcessF.GetRecipeValue("Rset", "OKCount");
            nPalletCount = MiddleLayer.ProcessF.GetRecipeValue("Rset", "PalletCount");
            ulong nBads = nTotalUnits - nCountGoodUnits;
            lbl_Unit_ProductTotal.SetText(nTotalUnits.ToString());
            lbl_Unit_PalletTotal.SetText(nPalletCount.ToString());
            lbl_Unit_Pass.SetText(nCountGoodUnits.ToString());
            lbl_Unit_Fail.SetText(nBads.ToString());
            float f_FPY = _CalcFPY(nCountGoodUnits, nTotalUnits);
            lbl_FPY.SetText($"{f_FPY.ToString("0.0")} %");
            TotalOKNumber = nCountGoodUnits;
            NGCount = lbl_Unit_Fail.Text;
            TotalOKNumber = nCountGoodUnits;
            NGCount = lbl_Unit_Fail.Text;
        }

        public void UpdatePalletCount(uint UnitsCountToAdd = 1)
        {
            nPalletCount += UnitsCountToAdd;
            lbl_Unit_PalletTotal.SetText(nPalletCount.ToString());
            DataTable dt = MiddleLayer.ProcessF.RecipeData.Tables["RSet"];
            dt.Rows[0]["PalletCount"] = nPalletCount;
            dt.AcceptChanges();
            MiddleLayer.ProcessF.WriteRecipeData(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
        }

        public static float _CalcFPY(ulong nGoodCount, ulong nTotalCount)
        {
            if (nTotalCount == 0)
                nTotalCount = 1;

            float nFPY = ((float)nGoodCount / (float)nTotalCount);
            if(nGoodCount==0 && nTotalCount==1)
            {
                nFPY = 1;
            }

            return nFPY * 100;
        }

        private void btnFeeder1_Click(object sender, EventArgs e)
        {
            //if (MiddleLayer.GantryF.bFeeder1Running == true)
            //{
            //    MiddleLayer.GantryF.bFeeder1Running = false ;
            //    btnFeeder1.BackColor = Color.LightGray;
            //}
            //else
            //{
            //    MiddleLayer.GantryF.bFeeder1Running = true;
            //    btnFeeder1.BackColor = Color.LightGreen;
            //}

        }

        private void btnFeeder2_Click(object sender, EventArgs e)
        {
            //if (MiddleLayer.GantryF.bFeeder2Running == true)
            //{
            //    MiddleLayer.GantryF.bFeeder2Running = false;
            //    btnFeeder2.BackColor = Color.LightGray;
            //}
            //else
            //{
            //    MiddleLayer.GantryF.bFeeder2Running = true;
            //    btnFeeder2.BackColor = Color.LightGreen;
            //}
        }

        private void btnFeeder3_Click(object sender, EventArgs e)
        {
            //if (MiddleLayer.GantryF.bFeeder3Running == true)
            //{
            //    MiddleLayer.GantryF.bFeeder3Running = false;
            //    btnFeeder3.BackColor = Color.LightGray;
            //}
            //else
            //{
            //    MiddleLayer.GantryF.bFeeder3Running = true;
            //    btnFeeder3.BackColor = Color.LightGreen;
            //}
        }

        private void lbl_TackT_Click(object sender, EventArgs e)
        {

        }

        private void btn_ResetJidoka_Click(object sender, EventArgs e)
        {

        }
        public ulong TotalOKNumber = 0;
        public string NGCount = "0";

        private void lbl_CycleTime_Click(object sender, EventArgs e)
        {

        }

        private void dgvProcessResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //lbl_Model.Text = SysPara.RecipeName + " Modelo de PCB: " + (string)MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");
            lbl_Model.Text = " Modelo de PCB: " + (string)MiddleLayer.SystemF.GetSettingValue("PSet", "modelItem");

            #region 生产图
            if (TotalOKNumber != nCountGoodUnits)
            {

                hoursProductShow1.kPointAdd(DateTime.Now, (int)(nCountGoodUnits - TotalOKNumber), true);
                hoursProductShow1.AllTimeDataShow(DateTime.Now);


            }

            if (NGCount != lbl_Unit_Fail.Text)
            {
                hoursProductShow1.kPointAdd(DateTime.Now, (int)(Convert.ToInt32(lbl_Unit_Fail.Text) - Convert.ToInt32(NGCount)), false);
                hoursProductShow1.AllTimeDataShow(DateTime.Now);

            }

            TotalOKNumber = nCountGoodUnits;
            NGCount = lbl_Unit_Fail.Text;
            #endregion
            timer1.Enabled = true;
        }

        private void ProcessDataForm_Load(object sender, EventArgs e)
        {

        }
    }
}

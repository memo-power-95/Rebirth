using Acura3.Classes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static Acura3.FunctionForms.LogForm;
using tstCkBox;

namespace Acura3.FunctionForms
{
    public partial class ProcessDataForm : Form
    {
        //Variables
        private DateTime dt_StartUnit = DateTime.Now;
        private DateTime dt_StartUnitUTC = DateTime.UtcNow;
        private Stopwatch stw_CycleTime = new Stopwatch();
        private Stopwatch stw_TackTime = new Stopwatch();
        public uint nJidokaLimit { get; set; } = 0;
        public bool bJidokaEnabled { get; set; }
        public bool IsUnitProcesing { get; private set; }
        //Contadores de Proceso
        public ulong nCountGoodUnits { get; private set; } = 0;
        public ulong nTotalUnits { get; private set; } = 0;
        public ulong nJidokaCount { get; private set; } = 0;
        public bool bEnableLOG { get; set; }
        public bool bEnableIOT { get; set; }
        public ProcessDataForm()
        {
            InitializeComponent();
            stw_CycleTime.Stop();
            stw_TackTime.Stop();
        }
        public void fnTimersRunning(bool bStop)
        {
            if (bStop)
            {
                stw_CycleTime.Stop();
                stw_CycleTime.Stop();
            }
            else
            {
                stw_CycleTime.Start();
                stw_CycleTime.Start();
            }
        }
    
        public bool Jidoka_Alarmed { get { return nJidokaCount >= nJidokaLimit && bJidokaEnabled; } }

        public Color ColorTheme
        {
            set { fnSetColor(value); }
        }

        public string sSerialNumber
        {
            set { lblSerial1.SetText(value); }
            get { return lblSerial1.Text; }
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
        public string sTitleModelo
        {
            get { return lbl_TitleModel.Text; }
            set { lbl_TitleModel.SetText(value); }
        }
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
            lbl_CycleTime.SetText(stw_CycleTime.Elapsed.ToString("mm\\:ss\\.ff"));
        }
        public void fnIndicateStartingUnit()
        {
            IsUnitProcesing = true;
            stw_CycleTime.Restart();
            lbl_Status.SetText("WAIT");
            lbl_Status.SetForeColor(Color.Black);
            dt_StartUnit = DateTime.Now;
            dt_StartUnitUTC = DateTime.UtcNow;
        }
        public void fnIndicateCancelUnit()
        {
            IsUnitProcesing = false;
            stw_CycleTime.Stop();
            lbl_Status.SetText("-");
            lbl_Status.SetForeColor(Color.Black);
        }

        public void fnResetTaktTime()
        {
            lbl_TackT.SetText(stw_TackTime.Elapsed.ToString("mm\\:ss\\.ff"));
            stw_TackTime.Restart();
            stw_CycleTime.Stop();
        }
        public void fnSetSerial(int _ScanNum, string[] _Seriales)
        {
            if (_ScanNum == 0)
            {

                lblSerial1.Text = _Seriales[0];
                lblSerial2.Text = _Seriales[1];
                //fnAddSerialToTableResults(1, _serial);
            }
            else if (_ScanNum == 1)
            {
                lblSerial3.Text = _Seriales[0];
                lblSerial4.Text = _Seriales[1];
                //fnAddSerialToTableResults(2, _serial);
            }
          
        }
        public void fnAddCountUnit(bool bPass, string sSerialNumber="NOSERIAL", string defect = "", bool bEndTackTime = true, uint nTotalToCount = 1)
        {
            lblSerial1.SetText(sSerialNumber);
            
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

            if (bEndTackTime)
            {
                stw_CycleTime.Stop();
                lbl_TackT.SetText(stw_TackTime.Elapsed.ToString("mm\\:ss\\.ff"));
                stw_TackTime.Restart();
            }

            string _sModel = lbl_Model.Text != "" ? lbl_Model.Text : SysPara.RecipeName;
            string _sSerial = sSerialNumber != "NOSERIAL" ? sSerialNumber : lblSerial1.Text;
            string _sStatus = (bPass ? "P" : "F");
            
            if (bEnableLOG)
                MiddleLayer.LogF.AddLog(LogType.Production, $"{_sModel};{_sSerial};{_sStatus};", true);

            if (bEnableIOT)
                AcuraIOT.AcuraCloudServices.AsyncIOTBoardHistory(_sSerial, _sModel, dt_StartUnitUTC, DateTime.UtcNow, _sStatus, defect, "");

            IsUnitProcesing = false;
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
            lbl_Unit_Total.SetText("0");
            nCountGoodUnits = 0;
            nTotalUnits = 0;
            fnResetJIDOKA();
        }
        private void fnSetColor(Color cl)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)(() => fnSetColor(cl)));
            else
            {
                lbl_TopName.BackColor = cl;
                lbl_TitleSerial.BackColor = cl;
                lbl_TitleStatus.BackColor = cl;
                lbl_TitleModel.BackColor = cl;
                lbl_TitleCycleTime.BackColor = cl;
                lbl_TitleTackTime.BackColor = cl;
                lbl_TitleFPY.BackColor = cl;
                lbl_TittleJidoka.BackColor = cl;
                lbl_TitlePass.BackColor = cl;
                lbl_TitleTotal.BackColor = cl;
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
            lbl_Unit_Total.SetText(nTotalUnits.ToString());

            if (bPass)
            {
                nCountGoodUnits += UnitsCountToAdd;
                lbl_Unit_Pass.SetText(nCountGoodUnits.ToString());
            }
            else
            {
                ulong nBads = nTotalUnits - nCountGoodUnits;
                lbl_Unit_Fail.SetText(nBads.ToString());
            }

            float f_FPY = _CalcFPY(nCountGoodUnits, nTotalUnits);
            lbl_FPY.SetText($"{f_FPY.ToString("0.0")} %");
        }
        public static float _CalcFPY(ulong nGoodCount, ulong nTotalCount)
        {
            if (nTotalCount == 0)
                nTotalCount = 1;

            float nFPY = ((float)nGoodCount / (float)nTotalCount);
            return nFPY * 100;
        }
    }
}

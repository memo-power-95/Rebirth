using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcuraLibrary.Forms;
using Alpha.Classes;
using System.Diagnostics;
using System.Drawing;
using static Alpha.FunctionForms.LogForm;
//using NPSDK.Enums;
using NPSDK;
using System.IO;
using Alpha._0.Classes;
using Alpha.FunctionForms;
using AsyncAwaitBestPractices;
using Keyence.AutoID.SDK;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Windows.Threading;

namespace Alpha._0.ModuleForms
{
    public partial class MesForm : ModuleBaseForm
    {

        public bool stoprunb, EndStopRun, EndStopRun1;
        private bool IsFirstLoad = true;
        private string SelectedModelId = "";
        private Size ModuleControllerSize = new Size(531, 429);
        public MesForm()
        {
            plMaintenance.Enabled = false;
            plFlowAuto.Enabled = false;
            plFlowInitial.Enabled = true;
            plMachineStatus.Enabled = true;
            plProductionSetting.Enabled = true;
            plMotorControl.Enabled = false;
            plRecipeEditor.Enabled = false;
            plMotionSetup.Enabled = false;

            InitializeComponent();

        }

        //public object swCycleTime { get; private set; }
        private void FakeLoad()
        {
            if (MESLib.CommParas.Frm_GEM == null)
                MESLib.CommParas.Frm_GEM = new MESLib.Controls.GEM_Winform();
            MESLib.CommParas.Frm_GEM.Dock = DockStyle.Fill;
            Panel_GEM.Controls.Add(MESLib.CommParas.Frm_GEM);
            if (MESLib.CommParas.Frm_ECV == null)
                MESLib.CommParas.Frm_ECV = new MESLib.Controls.ECV_Winform();
            MESLib.CommParas.Frm_ECV.Dock = DockStyle.Fill;
            Panel_ECV.Controls.Add(MESLib.CommParas.Frm_ECV);



            MESLib.CommParas.MesCenter.LoadParameters();


            SysPara.EnableMes = MiddleLayer.MesF2.GetSettingValue("PSet", "Enable_Mes");
            MESLib.CommParas.EnableMes = SysPara.EnableMes;

            string IP = GetSettingValue("PSet", "Mes_IP");
            string Port = GetSettingValue("PSet", "Mes_Port");

            MESLib.CommParas.IP = IP;
            MESLib.CommParas.Port = Port;
        }
        private void WritePSetValue<T>(string name, T value)
        {
            DataTable dt = SettingData.Tables["PSet"];
            dt.Rows[0][name] = value;
            dt.AcceptChanges();
            WriteSettingData();
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsFirstLoad)
            {
                FakeLoad();
                IsFirstLoad = false;
            }

            timer1.Enabled = true;
        }

        private void MesForm_Load(object sender, EventArgs e)
        {
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int equipmode = 0;
            if (rabFullAutoMode.Checked)
            {
                equipmode = Convert.ToInt32(rabFullAutoMode.Tag.ToString());
                MESLib.CommParas.MesManager.EquipmentOperationMode_PreviousMode = MESLib.CommParas.MesManager.EquipmentOperationMode_CurrentMode;
                MESLib.CommParas.MesManager.EquipmentOperationMode_CurrentMode = MESLib.EquipmentOperationMode.FullAutoMode;
            }
            else if (rabBypassMode.Checked)
            {
                equipmode = Convert.ToInt32(rabBypassMode.Tag.ToString());
                MESLib.CommParas.MesManager.EquipmentOperationMode_PreviousMode = MESLib.CommParas.MesManager.EquipmentOperationMode_CurrentMode;
                MESLib.CommParas.MesManager.EquipmentOperationMode_CurrentMode = MESLib.EquipmentOperationMode.BypassMode;
            }
            else if (rabDryRunningMode.Checked)
            {
                equipmode = Convert.ToInt32(rabDryRunningMode.Tag.ToString());
                MESLib.CommParas.MesManager.EquipmentOperationMode_PreviousMode = MESLib.CommParas.MesManager.EquipmentOperationMode_CurrentMode;
                MESLib.CommParas.MesManager.EquipmentOperationMode_CurrentMode = MESLib.EquipmentOperationMode.DryRunningMode;
            }
            else if (rabManualMode.Checked)
            {
                // check offline state
                if (MESLib.CommParas.MesManager._connector != null)
                {
                    if ((MESLib.ControlState)(Convert.ToInt32(MESLib.CommParas.MesManager._connector.SV.CurrentControlState.content)) != MESLib.ControlState.Equipment_Offline)
                    {
                        MessageBox.Show("Please Offline first!");
                        return;
                    }
                }

                equipmode = Convert.ToInt32(rabManualMode.Tag.ToString());
                MESLib.CommParas.MesManager.EquipmentOperationMode_PreviousMode = MESLib.CommParas.MesManager.EquipmentOperationMode_CurrentMode;
                MESLib.CommParas.MesManager.EquipmentOperationMode_CurrentMode = MESLib.EquipmentOperationMode.ManualMode;
            }

            MESLib.CommParas.MesManager.S6F11_10301(equipmode);
        }
    }
}

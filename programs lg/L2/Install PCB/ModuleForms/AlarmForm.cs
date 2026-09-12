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
using System.Windows.Forms;

namespace Alpha.ModuleForms
{
    public partial class AlarmForm : ModuleBaseForm
    {
        bool IsFirstLoad = true;
        public AlarmForm()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsFirstLoad)
            {
                FakeLoad();
                IsFirstLoad = false;
            }
        }
        private void FakeLoad()
        {
            if (MESLib.CommParas.Frm_Logs == null)
                MESLib.CommParas.Frm_Logs = new MESLib.Controls.Logs_Winform();
            MESLib.CommParas.Frm_Logs.Dock = DockStyle.Fill;

            Panel_Log.Controls.Add(MESLib.CommParas.Frm_Logs);
        }
    }
}

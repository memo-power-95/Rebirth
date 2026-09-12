using Acura3.Classes;
using AcuraLibrary.Forms;
using JabilSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acura3.ModuleForms
{
    public partial class LaserMarkingForm : ModuleBaseForm
    {

        #region ------------------------------- Variables
        Socket laserMarkerSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        const string MARKING_CONFIRMATION = "WX,OK";
        const string MARKING_START = "WX,StartMarking\r\n";
        const string MARKING_ERRORCLEAR = "WX,ErrorClear\r\n";
        const string MARKING_READERROR = "RX,ERROR\r\n";
        public int LaserMarkErrorResetCount { get; private set; }
        JTimer tmrMarking = new JTimer();
        public int iSerialNumbersQuantity { get { return GetData(dcRSet_SerialNumbersQuantity); } }
        public bool bValidateMarking { get { return GetData(dcRSet_ValidateMarking); } }
        public DataColumn _dcSerialNumberIndex { get { return dcMarkingData_SerialNumberIndex; } }
        BindingList<string> listLaserMarkingHistory = new BindingList<string>();
        Task<bool> taskConnection = null;
        Task<(bool, LaserMarkingSequence)> taskClearError = null;
        LaserMarkingSequence lmsClearErrorStep = LaserMarkingSequence.Connection;
        cSmartAlarm cAlm = SmartAlarm.cAlarm;
        DataRow[] markingData = null;
        #endregion

        #region ----------------------- Enum

        public enum LaserMarkingSequence
        {
            Connection,
            GetMarkingDataFromTable,
            WriteProgramNumber,
            WriteOffset,
            WriteSerialNumbers,
            ReadProgramNumber,
            ReadOffset,
            ReadSerialNumbers,
            StartMarking,
            Delay,
            CheckError,
            ClearError,
            CriticalError,
        }

        #endregion

        public LaserMarkingForm()
        {
            InitializeComponent();
            lbLaserMarkingHistory.DataSource = listLaserMarkingHistory;
            cAlm.AddRange(fcInitial_LaserMarking_Start);
        }

        /************************************************************************************
         * General functions
         ************************************************************************************/

        #region General functions

        private void fnAddLaserMarkingRecord(string command)
        {
            listLaserMarkingHistory.Add(command);

            if (listLaserMarkingHistory.Count > 20)
                listLaserMarkingHistory.RemoveAt(0);
        }
        public void fnGeneratedSerialNumbersListviewColumns()
        {
            if (lvGeneratedSerialNumbers.Columns.Count == 0)
                lvGeneratedSerialNumbers.Columns.Add("z", 310);
        }
        public void fnScannedSerialNumbersListviewColumns()
        {
            if (lvScannedSerialNumbers.Columns.Count == 0)
            {
                lvScannedSerialNumbers.Columns.Add("y", 270);
                lvScannedSerialNumbers.Columns.Add("z", 40);
            }
        }
        public void fnClearListViews()
        {
            if (plMachineStatus.InvokeRequired)
            {
                plMachineStatus.BeginInvoke((MethodInvoker)(() =>
                {
                    lvGeneratedSerialNumbers.Clear();
                    lvScannedSerialNumbers.Clear();
                }));
            }
            else
            {
                lvGeneratedSerialNumbers.Clear();
                lvScannedSerialNumbers.Clear();
            }
        }
        public void fnAddGeneratedSerialNumbers(string[] sSerialNumbers)
        {
            if (lvGeneratedSerialNumbers.InvokeRequired)
            {
                lvGeneratedSerialNumbers.BeginInvoke((MethodInvoker)(() =>
                {
                    lvGeneratedSerialNumbers.Clear();

                    foreach (string sSerialNumber in sSerialNumbers)
                        lvGeneratedSerialNumbers.Items.Add(sSerialNumber);
                }));
            }
            else
            {
                lvGeneratedSerialNumbers.Clear();

                foreach (string sSerialNumber in sSerialNumbers)
                    lvGeneratedSerialNumbers.Items.Add(sSerialNumber);
            }
        }
        public void fnAddScannedSerialNumber(string sSerialNumber, bool bStatus, char cQuality = '\0')
        {
            if (lvScannedSerialNumbers.InvokeRequired)
            {
                lvScannedSerialNumbers.BeginInvoke((MethodInvoker)(() =>
                {
                    ListViewItem newItem = lvScannedSerialNumbers.Items.Add(sSerialNumber);
                    //lvScannedSerialNumbers.Items[lvScannedSerialNumbers.Items.Count - 1].BackColor = bStatus ? Color.LightGreen : Color.Red;

                    if (cQuality != '\0')
                        newItem.SubItems.Add(cQuality.ToString());
                }));
            }
            else
            {
                ListViewItem newItem = lvScannedSerialNumbers.Items.Add(sSerialNumber);
                //lvScannedSerialNumbers.Items[lvScannedSerialNumbers.Items.Count - 1].BackColor = bStatus ? Color.LightGreen : Color.Red;

                if (cQuality != '\0')
                    newItem.SubItems.Add(cQuality.ToString());
            }
        }
        private IPEndPoint fnGetLaserMarkerEndPoint()
        {
            string strIP = GetData(dcPSet_IP);
            int iPort = GetData(dcPSet_Port);
            return new IPEndPoint(IPAddress.Parse(strIP), iPort);
        }
        public async Task<bool> fnConnectLaserMarker()
        {
            IPEndPoint endPoint = fnGetLaserMarkerEndPoint();

            if (laserMarkerSocket != null)
            {
                laserMarkerSocket.Close();
                await Task.Delay(100);
            }

            if (laserMarkerSocket == null || !laserMarkerSocket.Connected)
            {
                laserMarkerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                laserMarkerSocket.ReceiveTimeout = 500;
                laserMarkerSocket.SendTimeout = 500;

                await Task.Run(() =>
                {
                    try
                    {
                        laserMarkerSocket.Connect(endPoint);
                    }
                    catch (Exception ex)
                    {

                    }

                }).ConfigureAwait(false);
            }

            if (laserMarkerSocket.Connected)
            {
                fnAddLaserMarkingRecord("Conexión: exitosa");
                return true;
            }

            fnAddLaserMarkingRecord("Conexión: fallida");
            return false;
        }
        private async Task<bool> fnSendCommandWithAck(string command, int iSendTimeout = 5000, int iReceiveTimeout = 5000)
        {
            laserMarkerSocket.SendTimeout = iSendTimeout;
            laserMarkerSocket.ReceiveTimeout = iReceiveTimeout;
            await Task.Run(() => laserMarkerSocket.Send(Encoding.ASCII.GetBytes(command))).ConfigureAwait(false);
            fnAddLaserMarkingRecord("Enviado: " + command);
            byte[] arrDataReceived = new byte[1024];
            await Task.Run(() => laserMarkerSocket.Receive(arrDataReceived, arrDataReceived.Length, 0)).ConfigureAwait(false);
            string strReceived = Encoding.ASCII.GetString(arrDataReceived);
            fnAddLaserMarkingRecord("Recibido: " + strReceived);
            string[] strData = strReceived.Split('\r');
            return strData[strData.Length - 2] == MARKING_CONFIRMATION;
        }
        private async Task<bool> fnSendSerialNumbersWithAck(DataRow[] rowsMarkingData, int iSendTimeout = 1000, int iReceiveTimeout = 1000)
        {
            laserMarkerSocket.SendTimeout = iSendTimeout;
            laserMarkerSocket.ReceiveTimeout = iReceiveTimeout;
            StringBuilder sb = new StringBuilder();

            sb.Append("WX");

            foreach (var Row in rowsMarkingData)
            {
                string PartN  = ""; string SiteCode = "";
                int blockNumb = (int)Row[dcMarkingData_BlockNumber];
                string serialNumber = MiddleLayer.GantryF.dScannedSerialNumbers.Keys.ElementAt((int)Row[dcMarkingData_SerialNumberIndex]);
                SplitPartNAndSiteCode(serialNumber, ref PartN,ref SiteCode);
                //string command = $",BLK={blockNumb},CharacterString={serialNumber}";
                string command = $",BLK={blockNumb},CharacterString={serialNumber},BLK={blockNumb+1},CharacterString={PartN},BLK={blockNumb + 2},CharacterString={SiteCode},";
                sb.Append(command);
            }

            sb.Append("\r\n");

            await Task.Run(() => laserMarkerSocket.Send(Encoding.ASCII.GetBytes(sb.ToString()))).ConfigureAwait(false);
            fnAddLaserMarkingRecord("Enviado: " + sb.ToString());

            byte[] dataReceived = new byte[1024];
            await Task.Run(() => laserMarkerSocket.Receive(dataReceived, dataReceived.Length, 0)).ConfigureAwait(false);

            string strReceived = Encoding.ASCII.GetString(dataReceived);
            fnAddLaserMarkingRecord("Recibido: " + strReceived);

            string[] strData = strReceived.Split('\r');
            return strData[strData.Length - 2] == MARKING_CONFIRMATION;
        }

        private void SplitPartNAndSiteCode(string serialNumber,ref string PartN,ref string SiteCode)
        {
            if (serialNumber.Contains(":"))
            {
                string[] strArr = serialNumber.Split(':');
                string pStr = strArr[0].Substring(1, strArr[0].Length-1);
                string sStr = strArr[1].Substring(1, strArr[1].Length - 1);
                PartN = "(P)" + pStr;
                SiteCode = "(S)" + sStr;
            }
        }

        private async Task<(bool, string[])> fnReadSerialNumbers(DataRow[] rowsMarkingData, int iSendTimeout = 5000, int iReceiveTimeout = 5000)
        {
            laserMarkerSocket.SendTimeout = iSendTimeout;
            laserMarkerSocket.ReceiveTimeout = iReceiveTimeout;

            // Send read command
            StringBuilder sb = new StringBuilder();
            sb.Append("RX");

            foreach (var Row in rowsMarkingData)
            {
                int blockNum = (int)Row[dcMarkingData_BlockNumber];
                string BlkNumber = ",BLK=" + blockNum.ToString();
                string content = ",CharacterString";
                sb.Append(BlkNumber + content);
            }

            sb.Append("\r\n");
            await Task.Run(() => laserMarkerSocket.Send(Encoding.ASCII.GetBytes(sb.ToString()))).ConfigureAwait(false);
            fnAddLaserMarkingRecord("Enviado: " + sb.ToString());

            // Receive data
            byte[] arrDataReceived = new byte[1024];
            await Task.Run(() => laserMarkerSocket.Receive(arrDataReceived, arrDataReceived.Length, 0)).ConfigureAwait(false);
            string strReceived = Encoding.ASCII.GetString(arrDataReceived);
            fnAddLaserMarkingRecord("Recibido: " + strReceived);
            string[] arrData = strReceived.Split('\r');
            string[] recvsplit = arrData[arrData.Length - 2].Split(',');

            return (recvsplit[0] == "RX" && recvsplit[1] == "OK", recvsplit);
        }
        private async Task<(bool, string[])> fnSendCommandWithResults(string command, int iSendTimeout = 5000, int iReceiveTimeout = 5000)
        {
            laserMarkerSocket.SendTimeout = iSendTimeout;
            laserMarkerSocket.ReceiveTimeout = iReceiveTimeout;

            bool bSendOk = await Task.Run(() =>
            {
                try
                {

                    laserMarkerSocket.Send(Encoding.ASCII.GetBytes(command));
                    return true;
                }
                catch (Exception ex)
                {

                }
                return false;
            }).ConfigureAwait(false);

            if (bSendOk)
            {
                fnAddLaserMarkingRecord("Enviado: " + command);
                byte[] dataReceived = new byte[1024];

                bool bReceiveOk = await Task.Run(() =>
                {
                    try
                    {
                        laserMarkerSocket.Receive(dataReceived, SocketFlags.None);
                        return true;
                    }
                    catch (Exception ex)
                    {

                    }

                    return false;
                }).ConfigureAwait(false);

                if (bReceiveOk)
                {
                    string strReceived = Encoding.ASCII.GetString(dataReceived);
                    fnAddLaserMarkingRecord("Recibido: " + strReceived);
                    string[] strData = strReceived.Split('\r');
                    string[] recvsplit = strData[strData.Length - 2].Split(',');
                    return (recvsplit[0] == "RX" && recvsplit[1] == "OK", recvsplit);
                }

            }

            return (false, null);
        }
        public async Task<(bool, LaserMarkingSequence)> fnLaserMarkingSequenceAsync(int itemId, LaserMarkingSequence step, int iPrgNo)
        {
            switch (step)
            {
                case LaserMarkingSequence.Connection:
                    {
                        bool bConnected = await fnConnectLaserMarker().ConfigureAwait(false);

                        if (bConnected)
                        {
                            markingData = null;
                            step = LaserMarkingSequence.GetMarkingDataFromTable;
                        }

                        break;
                    }
                case LaserMarkingSequence.GetMarkingDataFromTable:
                    {
                        markingData = GetDataRows(dcMarkingData_Id, itemId.ToString()).ToArray();
                        step = LaserMarkingSequence.WriteProgramNumber;
                        break;
                    }
                case LaserMarkingSequence.WriteProgramNumber:
                    {
                        bool commandOk = await fnSendCommandWithAck($"WX,ProgramNo={iPrgNo}\r\n").ConfigureAwait(false);

                        if (commandOk)
                            step = LaserMarkingSequence.WriteOffset;

                        break;
                    }
                case LaserMarkingSequence.WriteOffset:
                    {
                        bool commandOk = await fnSendCommandWithAck($"WX,PRG={iPrgNo},ProgramPosition=0,0,0,0,0\r\n").ConfigureAwait(false);

                        if (commandOk)
                            step = LaserMarkingSequence.WriteSerialNumbers;

                        break;
                    }
                case LaserMarkingSequence.WriteSerialNumbers:
                    {
                        if (markingData == null || markingData.Length > 0)
                        {
                            bool bSerialNumbersOk = await fnSendSerialNumbersWithAck(markingData,iSendTimeout:3000, iReceiveTimeout:3000).ConfigureAwait(false);

                            if (bSerialNumbersOk)
                                step = LaserMarkingSequence.ReadProgramNumber;
                        }
                        else
                            step = LaserMarkingSequence.ReadProgramNumber;

                        break;
                    }
                case LaserMarkingSequence.ReadProgramNumber:
                    {
                        (bool bReadOk, string[] strData) = await fnSendCommandWithResults("RX,ProgramNo\r\n").ConfigureAwait(false);

                        if (bReadOk)
                        {
                            if (Convert.ToInt16(strData[2]) == iPrgNo)
                                step = LaserMarkingSequence.ReadOffset;
                            else
                            {
                                LaserMarkErrorResetCount = 0;
                                step = LaserMarkingSequence.CheckError;
                            }
                        }

                        break;
                    }
                case LaserMarkingSequence.ReadOffset:
                    {
                        (bool bReadOk, string[] strData) = await fnSendCommandWithResults($"RX,PRG={iPrgNo},ProgramPosition\r\n").ConfigureAwait(false);

                        if (bReadOk)
                        {
                            if (Convert.ToDouble(strData[4]) == 0 && Convert.ToDouble(strData[5]) == 0)
                                step = LaserMarkingSequence.ReadSerialNumbers;
                            else
                            {
                                LaserMarkErrorResetCount = 0;
                                step = LaserMarkingSequence.CheckError;
                            }
                        }

                        break;
                    }
                case LaserMarkingSequence.ReadSerialNumbers:
                    {
                        if (markingData.Length > 0)
                        {
                            (bool bReadOk, string[] strData) = await fnReadSerialNumbers(markingData).ConfigureAwait(false);

                            if (bReadOk)
                            {
                                int k = 2;

                                foreach (var Row in markingData)
                                {
                                    if (MiddleLayer.GantryF.dScannedSerialNumbers.Keys.ElementAt((int)Row[dcMarkingData_SerialNumberIndex]) != strData[k++])
                                    {
                                        LaserMarkErrorResetCount = 0;
                                        step = LaserMarkingSequence.CheckError;
                                        return (false, step);
                                    }
                                }

                                step = LaserMarkingSequence.StartMarking;
                            }
                        }
                        else
                            step = LaserMarkingSequence.StartMarking;

                        break;
                    }
                case LaserMarkingSequence.StartMarking:
                    {
                        bool bCommandOk = await fnSendCommandWithAck(MARKING_START, 5000, 25000).ConfigureAwait(false);

                        if (bCommandOk)
                        {
                            step = LaserMarkingSequence.Delay;
                            tmrMarking.Restart();
                        }
                        break;
                    }
                case LaserMarkingSequence.Delay:
                    {
                        if (tmrMarking.On(1000))
                        {
                            laserMarkerSocket.Close();
                            step = LaserMarkingSequence.Connection;
                            return (true, step);
                        }
                        break;
                    }
                case LaserMarkingSequence.CheckError:
                    {
                        (bool bReadOk, string[] strData) = await fnSendCommandWithResults(MARKING_READERROR).ConfigureAwait(false);

                        if (bReadOk)
                        {
                            if (strData.Length == 3)
                                step = LaserMarkingSequence.Connection;
                            else
                            {
                                if (LaserMarkErrorResetCount > 3)
                                    step = LaserMarkingSequence.CriticalError;
                                else
                                {
                                    LaserMarkErrorResetCount++;
                                    step = LaserMarkingSequence.ClearError;
                                }
                            }
                        }

                        break;
                    }
                case LaserMarkingSequence.ClearError:
                    {
                        bool bCommandOk = await fnSendCommandWithAck(MARKING_ERRORCLEAR).ConfigureAwait(false);

                        //if (bCommandOk)
                        step = LaserMarkingSequence.CheckError;

                        break;
                    }
                case LaserMarkingSequence.CriticalError:
                    {
                        step = LaserMarkingSequence.Connection;
                        return (false, step);
                    }
                default:
                    return (false, step);
            }

            return (false, step);
        }
        public async Task<(bool, LaserMarkingSequence)> fnResetError(LaserMarkingSequence step)
        {
            switch (step)
            {
                case LaserMarkingSequence.Connection:
                    {
                        await fnConnectLaserMarker().ConfigureAwait(false);
                        LaserMarkErrorResetCount = 0;
                        step = LaserMarkingSequence.CheckError;
                        break;
                    }
                case LaserMarkingSequence.CheckError:
                    {
                        (bool bReadOk, string[] strData) = await fnSendCommandWithResults(MARKING_READERROR).ConfigureAwait(false);

                        if (bReadOk)
                        {
                            if (strData.Length == 3)
                            {
                                step = LaserMarkingSequence.Connection;
                                laserMarkerSocket.Close();
                                return (true, step);
                            }
                            else
                            {
                                if (LaserMarkErrorResetCount > 3)
                                    step = LaserMarkingSequence.CriticalError;
                                else
                                {
                                    LaserMarkErrorResetCount++;
                                    step = LaserMarkingSequence.ClearError;
                                }
                            }
                        }

                        break;
                    }
                case LaserMarkingSequence.ClearError:
                    {
                        bool bCommandOk = await fnSendCommandWithAck(MARKING_ERRORCLEAR).ConfigureAwait(false);

                        //if (bCommandOk)
                        step = LaserMarkingSequence.CheckError;

                        break;
                    }
                case LaserMarkingSequence.CriticalError:
                    {
                        step = LaserMarkingSequence.Connection;
                        return (false, step);
                    }
                default:
                    return (false, step);
            }

            return (false, step);
        }

        #endregion General functions

        /************************************************************************************
         * Overrides
         ************************************************************************************/

        #region Overrides

        public override void InitialReset()
        {
            fcInitial_LaserMarking_Start.TaskReset();
        }

        public override void Initial()
        {
            fcInitial_LaserMarking_Start.TaskRun();
        }

        #endregion Overrides

        /************************************************************************************
         * Events
         ************************************************************************************/

        #region Events



        private async void btnSendCommand_Click(object sender, EventArgs e)
        {
            if (laserMarkerSocket == null || !laserMarkerSocket.Connected)
            {
                MessageBox.Show("Primero debe conectar con la marcadora");
                return;
            }

            btnSendCommand.Enabled = false;

            (bool bStatus, string[] sResult) = await fnSendCommandWithResults(cmbCommands.Text + "\r\n");

            if (bStatus)
                txtAnswer.Text = string.Join(",", sResult);
            else
                txtAnswer.Text = "Error";


            btnSendCommand.Enabled = true;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = false;
            await fnConnectLaserMarker();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = true;
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            listLaserMarkingHistory.Clear();
        }

        private void dgvMarkingData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridView dataGridView = (DataGridView)sender;
            dataGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.LightGreen;
        }



        #endregion Events

        /************************************************************************************
         * fcInitial_LaserMarking
         ************************************************************************************/

        #region fcInitial_LaserMarking

        private JabilSDK.FCResultType fcInitial_LaserMarking_Start_FlowRun(object sender, EventArgs e)
        {
            taskConnection = null;
            taskClearError = null;
            lmsClearErrorStep = LaserMarkingSequence.Connection;
            return JabilSDK.FCResultType.NEXT;
        }
        private JabilSDK.FCResultType fcInitial_LaserMarking_Connect_FlowRun(object sender, EventArgs e)
        {

            //if (taskConnection == null)
            //    taskConnection = fnConnectLaserMarker();
            //else if (taskConnection.IsCompleted)
            //{
            //    if (taskConnection.Result)
                    return JabilSDK.FCResultType.NEXT;
            //    else
            //    {
            //        SDKPara.Arm.ReportAlarm(cAlm.E(fcInitial_LaserMarking_Start), AcuraIOT.ErrorGroup.LaserMarker, AcuraIOT.ErrorSubGroup.Connection, AcuraIOT.ErrorType.E, "Marcadora: Error de conexión");
            //        taskConnection = null;
            //    }
            //}

            //return JabilSDK.FCResultType.IDLE;
        }
        private FCResultType fcInitial_LaserMarking_ResetErrors_FlowRun(object sender, EventArgs e)
        {

            //if (taskClearError == null)
            //    taskClearError = fnResetError(lmsClearErrorStep);
            //else if (taskClearError.IsCompleted)
            //{
            //    lmsClearErrorStep = taskClearError.Result.Item2;

            //    if (taskClearError.Result.Item1)
            //    {
            //        SDKPara.Arm.ReportClearAlarm(cAlm.E(fcInitial_LaserMarking_Start));
                    return FCResultType.NEXT;
            //    }

            //    taskClearError = null;
            //}

            //if (fcInitial_LaserMarking_Start.tmrTimeOut.On(5000))
            //    SDKPara.Arm.ReportAlarm(cAlm.E(fcInitial_LaserMarking_Start), AcuraIOT.ErrorGroup.LaserMarker, AcuraIOT.ErrorSubGroup.Alarm, AcuraIOT.ErrorType.E, "Marcadora laser: No se pudo limpiar errores");

            //return FCResultType.IDLE;
        }
        private FCResultType fcInitial_LaserMarking_End_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = true;
            return FCResultType.IDLE;
        }

        #endregion

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

        }
    }
}

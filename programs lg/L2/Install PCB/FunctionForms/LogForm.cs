using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace Alpha.FunctionForms
{
	public partial class LogForm : Form
    {
        public enum LogType
        {
            UserInterface,
            Alarmas,
            Production,
            NGProduct,
            //Slider,
            //Soldering,
            VISION,
            //MachineStatusEvent,
        }
        public struct LogDataStruct
        {
            public LogType Type;
            public string Message;
            public DateTime DateTime;
            public bool bSave;
        }
        private string[] Caption = new string[5]
        {
          "Typo Error,Codigo,Mensaje",
          "Codigo,Mensaje",
          "Modelo,Serial,Status,Comentarios",
          "Modelo,Serial,Status,Comentarios",
          "Modelo,Serial,Status,Comentarios",
        };
        public enum MachineStatusType
        {
            AcuraExecution,
            AcuraShutdown,
            MachineIdle,
            MachineInitialization,
            MachineRun,
            MachineDown,
            WaitingForProducts,
        }
        //private string sSaveLogFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + "//Log";
        private string sSaveLogFilePath = @"D:\EVMS\TP\LOG";
        private List<LogDataStruct> MsgList = new List<LogDataStruct>();
        public DataGridView[] DgvArray;
        public LogForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            DgvArray = new DataGridView[((IEnumerable<string>)Enum.GetNames(typeof(LogType))).Count<string>()];
            tabLogPage.TabPages.Clear();
             int nEnabledCount=0;
            for (int it1 = 0; it1 < ((IEnumerable<string>)Enum.GetNames(typeof(LogType))).Count<string>(); ++it1)
            {
                DataGridView dataGridView = new DataGridView();
                this.DgvArray[it1] = dataGridView;
                dataGridView.Dock = DockStyle.Fill;
                dataGridView.BackgroundColor = Color.DarkGray;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dataGridView.ColumnHeadersHeight = 30;
                dataGridView.ReadOnly = true;
                dataGridView.ScrollBars=ScrollBars.Both;
                string[] strArray = this.Caption[it1].Split(',');
                dataGridView.ColumnCount = strArray.Length + 2;
                for (int it2 = 0; it2 < strArray.Length + 2; ++it2)
                {
                    switch (it2)
                    {
                        case 0:
                            dataGridView.Columns[it2].HeaderText = "Fecha";
                            dataGridView.Columns[it2].MinimumWidth=40;
                            break;
                        case 1:
                            dataGridView.Columns[it2].HeaderText = "Hora";
                            dataGridView.Columns[it2].MinimumWidth=40;

                            break;
                        default:
                            dataGridView.Columns[it2].HeaderText = strArray[it2 - 2];
                            dataGridView.Columns[it2].MinimumWidth=250;
                            break;
                    }
                    dataGridView.Columns[it2].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                 nEnabledCount++;
				
                TabPage tabPage = new TabPage(Enum.GetNames(typeof(LogType))[it1].ToString());
                tabPage.Controls.Add((Control)dataGridView);
                tabLogPage.Controls.Add((Control)tabPage);
            }
			if (MiddleLayer.ExReportF!=null)
			{
				TabPage tbPage = new TabPage("ExceptionsLog");
				tbPage.Controls.Add(MiddleLayer.ExReportF.pn_Exceptions);
				tabLogPage.Controls.Add((Control)tbPage); 
			}
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
                       
            nEnabledCount++;
			TabControl tabControl=tabLogPage;
            Size sParent=tabControl.Parent.Size;
            Size sDividers=tabControl.ItemSize;
            sDividers.Width=(sParent.Width-50)/nEnabledCount;
			sDividers.Width=sDividers.Width<150?169:sDividers.Width;
            tabControl.ItemSize=sDividers;
        }


        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            tmRefresh.Enabled = false;
            try
            {
                while (this.MsgList.Count != 0)
                {
                    LogDataStruct lg=MsgList.First();
                    this.DgvAddLogMsg(this.DgvArray[(int)lg.Type], lg);
                    SaveLogToFile(MsgList.First());
                    MsgList.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                 MiddleLayer.ExReportF.fnAddException(ex);
            }
            tmRefresh.Enabled = true;
        }

       
        public void DgvAddLogMsg(DataGridView Dgv, LogDataStruct lg)
        {
           
            string[] strColums = new string[Dgv.Columns.Count];
			try
			{
                strColums[0] = lg.DateTime.ToString("yyyy/MMM/dd");
                strColums[1] = lg.DateTime.ToString("HH:mm:ss");

                string[] strMessage = lg.Message.Split(';');
                int nLenght = strMessage.Length;
                int nStart = 2;
                for (int i = nStart; i < Dgv.Columns.Count; i++)
                {
                
                    if (nLenght > (i-nStart))
                    {
                        strColums[i] = strMessage[i - nStart];
                        if (i + 1 >= Dgv.Columns.Count)
                        {
                            string Final = "";
                            for (int j = i - nStart; j < strMessage.Length; j++)
                            {
                                Final += strMessage[j];
                            }
                            strColums[i] = Final;
                        }
                    }
                
                }
                Dgv.Rows.Add(strColums);
			}
			catch (Exception ex)
			{
                MiddleLayer.ExReportF.fnAddException(ex);
			}
            
            if (Dgv.Rows.Count <= 100)
                return;
            Dgv.Rows.Remove(Dgv.Rows[0]);
        }

        private async void SaveLogToFile(LogType LogType, string sMsg,DateTime dtNow)
        {
            try
            {
                //// string LogPath = string.Format(@"{0}\{1}\{2}\{3}.csv", sSaveLogFilePath, LogType.ToString(), dtNow.ToString("yyyy"), dtNow.ToString("MMdd"));
                //string LogPath = string.Format(@"{0}\{1}_{2}.csv", sSaveLogFilePath,
                    //"NA_NA_" + dtNow.ToString("yyyyMMdd"), LogType.ToString());

                string MesID = "NA";
                MesID = MiddleLayer.MesF2.GetSettingValue("PSet", "EQPID");
                if (MesID == null || MesID == "")
                    MesID = "NA";

                string LogPath = string.Format(@"{0}\{1}_{2}.csv", sSaveLogFilePath,
                    MesID + "_NA_" + dtNow.ToString("yyyyMMddHH"), LogType.ToString());

                if (!Directory.Exists(Path.GetDirectoryName(LogPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(LogPath));
                bool bFileExists = File.Exists(LogPath);
                if (!bFileExists)
                {
                    StreamWriter sw = new StreamWriter(LogPath, false);
                    sw.Close();
                }
                await Task.Run(()=>{
                    bool bExit=false;
					 while (!bExit)
					 {
                      if(fn_FileCanRead(LogPath))
                      {
                          bExit=true;
							try
							{
                                 File.AppendAllText(LogPath, string.Format("{0},{1}", dtNow.ToString("HH:mm:ss"), sMsg) + Environment.NewLine, Encoding.UTF8);
							}
							catch (Exception ex)
							{
                                MiddleLayer.ExReportF.fnAddException(ex);
							}
                            return;
					  }
                       Thread.Sleep(10);
					 }
                 }).ConfigureAwait(false);
                
            }

            catch (Exception ex)
            {
                MiddleLayer.ExReportF.fnAddException(ex);
            }
        }
         public bool fn_FileCanRead(string PathFile)
        {
            bool bFileCanread = false;
            try
            {
                FileStream fs = new FileStream(PathFile, FileMode.OpenOrCreate, FileAccess.Read);
                if (fs.CanRead && fs.CanWrite)
                {
                    //Console.WriteLine("MyFile.txt can be both written to and read from.");
                }
                else if (fs.CanRead)
                {
                    //Console.WriteLine("MyFile.txt is not writable.");
                }
                else if (!fs.CanRead)
                {
                    //Console.WriteLine("File CanotbeRead.");

                }
                bFileCanread = fs.CanRead; //&& fs.CanSeek && !fs.SafeFileHandle.IsInvalid;
                fs.Close();
                fs.Dispose();
                return bFileCanread;
            }
            catch (Exception ex)
            {

                return false;
            }
           
        }
        private async void SaveLogToFile(LogDataStruct LogData)
        {
           
			if (LogData.bSave)
			{
                await Task.Run(()=>{
                  SaveLogToFile(LogData.Type,LogData.Message,LogData.DateTime);
			    }).ConfigureAwait(false);
               
			}
        }
        /////Example of adding a log
        //private void fnAddLog(string _pos)
        //{
        //   MiddleLayer.LogF.AddLog(LogType.Production,$"{lbl_Model.Text};{lbl_Serial.Text};{(bPass?"P":"F")};Sin Comentarios", true);
        //}
        public void AddLog(LogType logType, string logMessage,bool bSave=false)
        {
            LogDataStruct Log = new LogDataStruct();
            Log.Type = logType;
            Log.Message = logMessage;
            Log.DateTime = DateTime.Now;
            Log.bSave=bSave;
            MsgList.Add(Log);
        }

        public void AddLog(int logType, string logMessage, bool bSave = false)
        {
            LogDataStruct Log = new LogDataStruct();
            Log.Type = (LogType)logType;
            Log.Message = logMessage;
            Log.DateTime = DateTime.Now;
            Log.bSave = bSave;
            MsgList.Add(Log);
        }

        private void tabLogPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabLogPage.SelectedIndex >= this.DgvArray.Length)
                return;
            DgvArray[this.tabLogPage.SelectedIndex].AutoResizeRows();
            DgvArray[this.tabLogPage.SelectedIndex].AutoResizeColumns();
        }
        public delegate void DALM(DataGridView Dgv, string sMsg);
    }
}

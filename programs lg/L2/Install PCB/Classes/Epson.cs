using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
//using NPFanucRobotDLL;
using NPSDK;
using NPClient;
using LogSv;
using System.Drawing;
using static Alpha.FunctionForms.LogForm;

namespace Alpha._0.Classes
{
    public class Epson
    {
        private TCPCLient EpsonRobot = new TCPCLient();
        public double[] ReadRobotXYU = new double[3];
        public string ReadRobotStr = string.Empty;
        public Queue<string> Queue_Recive = new Queue<string>();
        public string IP;
        public int Port;
        public JTimer EpsonTM = new JTimer();
        public bool IsIdle = false;
        private string speed = string.Empty;
        private string errCode = string.Empty;
        private string[] input = new string[16];
        private string[] output = new string[16];

        public bool ConnectStatus
        {
            get { return EpsonRobot.ConnectStatus(); }
        }
        public string ErorrCode
        {
            get
            {
                return errCode;
            }
        }
        public string[] Input
        {
            get { return input; }
        }

        public string[] Output
        {
            get { return output; }
        }
        public string Speed
        {
            get { return speed; }
        }
        public bool ConnectToEpson(string ip, int port)
        {
            IP = ip;
            Port = port;
            try
            {
                if (EpsonRobot.ConnectStatus())
                {
                    return true;
                }
                EpsonRobot.Disconnect();
                bool ret = false;
                ret = EpsonRobot.Connect(IP, Port);
                Thread.Sleep(100);
                return ret;
            }
            catch (Exception e)
            {
                string a = e.ToString();
                return false;
            }
        }

        public void DisConnectEpson()
        {
            if (EpsonRobot.ConnectStatus()) { EpsonRobot.Disconnect(); }
        }

        public void ReConnect()
        {
            Queue_Recive.Clear();
            EpsonRobot.Reconnect(IP, Port);
        }
        public void SetSpeed(string Command, string PosNum = "FFF", string AppNum = "FF", Int32 TimeOut = 2000, string X = "FFFFFFF", string Y = "FFFFFFF", string U = "FFFFFFF")
        {
            Command += "_" + PosNum + "_" + AppNum + "_" + X + "_" + Y + "_" + U + "_a";
            if (Command.Length != 36)
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Robot sent command length is fault,please check", SysPara.bEnableGeneralSaveLog);
            }
            if (EpsonRobot.ConnectStatus())
            {
                EpsonRobot.Sent(Command + "\r\n");
            }
        }
        /// <summary>
        /// write command to robot
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        /// <param name="U">U</param>
        /// <param name="TimeOut"></param>
        /// <returns></returns>
        public bool WriteToEpson(string Command, string PosNum = "FFF", string AppNum = "FF", Int32 TimeOut = 2000, string X = "FFFFFFF", string Y = "FFFFFFF", string U = "FFFFFFF")
        {
            bool bReturn = false;
            Command += "_" + PosNum + "_" + AppNum + "_" + X + "_" + Y + "_" + U + "_a";
            if (Command.Length != 36)
            {
                MiddleLayer.LogF.AddLog(LogType.Production, "Robot sent command length is fault,please check", SysPara.bEnableGeneralSaveLog);
            }
            if (EpsonRobot.ConnectStatus())
            {
                if (!IsIdle)                     //when flow idle ,do no send the same command
                {
                    IsIdle = true;
                    EpsonTM.Restart();
                    if (!EpsonRobot.Sent(Command + "\r\n"))
                    {
                        IsIdle = false;
                        return bReturn;
                    }
                }
                Thread.Sleep(5);
                if (EpsonTM.On(TimeOut))
                {
                    EpsonTM.Restart();
                    IsIdle = false;
                    SysPara.NPShowAlarm("1635");
                }
                if (Queue_Recive.Count > 0)
                {
                    string str = Queue_Recive.Dequeue();
                    if (str == Command)
                    {
                        bReturn = true;
                        IsIdle = false;
                    }
                }
                return bReturn;
            }
            else
            {
                ReConnect();
                SysPara.NPShowAlarm("1634");
                return bReturn;
            }
        }

        /// <summary>
        /// Read from robot command
        /// </summary>
        /// <returns>
        /// -1---------read fail
        /// 1----------read success
        /// 0----------error code
        /// 12---------read position success
        /// -12--------read position fail
        /// 102--------read output IO success
        /// -102-------read output IO fail
        /// 103--------read input IO success
        /// -103-------read input IO fail
        /// 
        /// </returns>
        public int ReadFromEpson()
        {
            if (EpsonRobot.ConnectStatus())
            {
                if (Queue_Recive.Count > 0)
                {
                    ReadRobotStr = Queue_Recive.Dequeue();
                    string[] str = ReadRobotStr.Split('_');
                    if (str[0] == "Null") { return -1; }
                    if (str[0] == "012")         //return position
                    {
                        if (str[str.Length - 2] == "OKOK")
                        {
                            ReadRobotXYU[0] = Convert.ToDouble(str[str.Length - 5]);
                            ReadRobotXYU[1] = Convert.ToDouble(str[str.Length - 4]);
                            ReadRobotXYU[2] = Convert.ToDouble(str[str.Length - 3]);
                            return 12;
                        }
                        else { return -12; }
                    }
                    else if (str[0] == "102")      //return output IO char[]
                    {
                        if (str[str.Length - 2] == "OKOK")
                        {
                            char[] tt = str[str.Length - 3].ToCharArray();
                            for (int i = 0; i < tt.Length; i++)
                            {
                                output[i] = tt[i].ToString();
                            }
                            return 102;
                        }
                        else { return -102; }
                    }
                    else if (str[0] == "103")
                    {
                        if (str[str.Length - 2] == "OKOK")      //return input IO char[]
                        {
                            char[] tt = str[str.Length - 3].ToCharArray();
                            for (int i = 0; i < tt.Length; i++)
                            {
                                Input[i] = tt[i].ToString();
                            }
                            return 103;
                        }
                        else { return -103; }
                    }
                    else if (str[0] == "105")
                    {
                        if (str[str.Length - 2] == "OKOK")      //return speed
                        {
                            speed = str[str.Length - 3];
                            return 105;
                        }
                        else { return -105; }
                    }
                    else if (str[0] == "104")
                    {
                        if (str[str.Length - 2] == "OKOK")      //return speed
                        {
                            speed = str[str.Length - 3];
                            return 104;
                        }
                        else { return -104; }
                    }
                    else
                    {
                        if (str[str.Length - 2] == "OKOK") { return 1; }        //return command
                        else if (str[str.Length - 2].Contains('E')) { errCode = str[str.Length - 2]; return 0; }
                        else { return -1; }
                    }
                }
                else
                {
                    return -1;
                }

            }
            else
            {
                SysPara.NPShowAlarm("1634");
                ReConnect();
                return -1;
            }
        }


        public double[] ReadPositionFromEpson()
        {
            double[] xyu = new double[3];
            lock (SysPara.StatusLock)
            {
                if (EpsonRobot.ConnectStatus())
                {
                    if (Queue_Recive.Count > 0)
                    {
                        ReadRobotStr = Queue_Recive.Dequeue();
                        string[] str = ReadRobotStr.Split('_');
                        if (str[0] == "Null") { return xyu; }
                        if (str[0] == "012")
                        {
                            if (str[str.Length - 2] == "OKOK")
                            {
                                ReadRobotXYU[0] = Convert.ToDouble(str[str.Length - 5]);
                                ReadRobotXYU[1] = Convert.ToDouble(str[str.Length - 4]);
                                ReadRobotXYU[2] = Convert.ToDouble(str[str.Length - 3]);
                                return ReadRobotXYU;
                            }
                            else { return xyu; }
                        }
                        if (str[0] == "015")
                        {
                            if (str[str.Length - 2] == "OKOK")
                            {
                                ReadRobotXYU[0] = Convert.ToDouble(str[str.Length - 5]);
                                ReadRobotXYU[1] = Convert.ToDouble(str[str.Length - 4]);
                                ReadRobotXYU[2] = Convert.ToDouble(str[str.Length - 3]);
                                return ReadRobotXYU;
                            }
                            else { return xyu; }
                        }
                    }
                    else
                    {
                        return xyu;
                    }
                    return xyu;
                }
                else
                {
                    ReConnect();
                    return xyu;
                }
            }
        }

        public void Recive()
        {
            if (EpsonRobot.ConnectStatus())
            {
                string[] str = EpsonRobot.Receive().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (str.Length > 0)
                {
                    if (str[0] != "Null")
                    {
                        foreach (var item in str)
                        {
                            if (!Queue_Recive.Contains(item)) { Queue_Recive.Enqueue(item); }
                            string[] errCode = item.Split('_');
                            if (errCode[errCode.Length - 2].Contains('E'))
                            {
                                MiddleLayer.LogF.AddLog(LogType.Production, errCode[errCode.Length - 2], SysPara.bEnableGeneralSaveLog);

                                //Log.log.Write(errCode[errCode.Length - 2],Color.Red);
                                SDKKernal.ShowAlarm(errCode[errCode.Length - 2].Split('E')[1]);
                            }
                        }
                    }
                }
                else
                {
                    ReConnect();
                }
            }
            else
            {
                SysPara.NPShowAlarm("1634");
                ReConnect();
            }
        }

    }
}

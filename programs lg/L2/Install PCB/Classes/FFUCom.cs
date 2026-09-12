using LogSv;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Alpha.FunctionForms.LogForm;

namespace Alpha._0.Classes
{
    class FFUCom
    {
        NPSerialProt FFUCOM = new NPSerialProt();

        public static string com = "";
        public static string para = "";
        private double FFUSpeedRate = 250.0 / 1350.0;       //速度转化比

        public bool connectStatus = false;
        public bool ConnectStatus
        {
            get
            {
                if (FFUCOM != null) { return FFUCOM.ConnectStates(); }
                else { return false; }
            }
        }

        //断开连接
        public bool Connect(string comStr,string paraStr)
        {
            com = comStr;
            para = paraStr;
            try
            {
                if (FFUCOM.ConnectComPressL(comStr, paraStr))
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                //MiddleLayer.LogF.AddLog(LogType.Production, ex.ToString(), SysPara.bEnableGeneralSaveLog);

                //Log.log.Write(ex.ToString(), Color.Red);
                return false;
            }
        }
        //断开连接
        public void DisConnect()
        {
            if (FFUCOM.ConnectStates())
            {
                FFUCOM.Dispose();
            }
        }
        
        //打开FFU（最大风速）
        public bool OpenFFUMaxSpeed()
        {
            if (!FFUCOM.ConnectStates())
                Connect(com, para);
            bool b1 = FFUCOM.Write_StringToHex("35 41 01 FA 70");
            Thread.Sleep(100);
            string strMid = FFUCOM.readStingByHex();
            if (strMid == "11 41 01 AE")
            {
                return true;               
            }
            else { return false; }
        }
        //获取风速
        public double GetFFUSpeed()
        {
            try
            {
                if (!FFUCOM.ConnectStates())
                    Connect(com, para);
                bool b1 = FFUCOM.Write_StringToHex("15 21 01 CA");
                Thread.Sleep(100);
                string strMid = FFUCOM.readStingByHex();
                string[] command = strMid.Split(' ');
                double speed = 0;
                if (command.Length >= 4){ speed = To16Convert10(command[3]) / FFUSpeedRate; }
                return speed;
            }
            catch (Exception ex)
            {
                MiddleLayer.LogF.AddLog(LogType.Production, ex.ToString(), SysPara.bEnableGeneralSaveLog);

                //Log.log.Write(ex.ToString(),Color.Red);
                return 0.0 ;
            }
           
        }
        /// <summary>
        /// 设置风速
        /// </summary>
        /// <param name="speed"></param>
        public bool SetFFUSpeed(Int32 speed)
        {
            if (!FFUCOM.ConnectStates())
                Connect(com, para);
            string speedcmd = ((int)(speed * FFUSpeedRate)).ToString("X2");
            string basecmd = "35 41 01 "+ speedcmd;
            string checkCode = GetCheckCode(basecmd);
            bool b1 = FFUCOM.Write_StringToHex(basecmd + " " + checkCode);
            Thread.Sleep(100);
            string strMid = FFUCOM.readStingByHex();
            if (strMid == "11 41 01 AE")
            {
                return true;
            }
            else { return false; }


        }


        #region 十六进制字符串转十进制
        /// <summary>
        /// 十六进制字符串转十进制
        /// </summary>
        /// <param name="str">十六进制字符</param>
        /// <returns></returns>
        static int To16Convert10(string str)
        {
            int res = 0;

            try
            {
                str = str.Trim().Replace(" ", "");//移除空字符
                //方法1
                res = int.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch (Exception e)
            {
                res = 0;
            }

            return res;

        }
        #endregion

        #region 校验码计算
        public string GetCheckCode(string command)
        {
            command += " FF";
            byte[] cmd = strToHexByte(command);
            byte x;
            x = 0;
            for (int i = 0; i < cmd.Length; i++)
            {
                x ^= cmd[i];
            }
            return string.Format("{0:X2} ", x);
        }
#endregion
        public byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        

    }
}

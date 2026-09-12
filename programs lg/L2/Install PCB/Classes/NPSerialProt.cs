using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha._0.Classes
{
    public class NPSerialProt
    {

        //create an Serial Port object        
        private SerialPort sp = new SerialPort();

        /// <summary>
        ///  连接串口
        /// </summary>
        /// <param name="strPortName"></param>
        /// <param name="iRate"></param>
        /// <param name="iDataBits"></param>
        /// <param name="iParity"></param>奇偶校验   没有奇偶None = 0,   奇校验Odd = 1,  偶校验Even = 2,奇偶校验位设置为 1  Mark = 3,  奇偶校验位设置为 0   Space = 4 
        /// <param name="iStopBits"></param>停止位    没有停止位None = 0, 一个停止位 One = 1,使用两个停止位 Two = 2,使用 1.5 停止位 OnePointFive = 3
        /// <returns></returns>
        public bool Connect(string strPortName, int iRate, int iDataBits, int iParity, int iStopBits)
        {
            try
            {
                sp.Dispose();
            }
            catch (Exception)
            {  }

            //奇偶校验   没有奇偶None = 0,   奇校验Odd = 1,  偶校验Even = 2,奇偶校验位设置为 1  Mark = 3,  奇偶校验位设置为 0   Space = 4 
            Parity par = Parity.None;
            switch (iParity)
            {
                case 0:
                    par = Parity.None;
                    break;
                case 1:
                    par = Parity.Odd;
                    break;
                case 2:
                    par = Parity.Even;
                    break;
                case 3:
                    par = Parity.Mark;
                    break;
                case 4:
                    par = Parity.Space;
                    break;
                default:

                    break;
            }

            // 没有停止位None = 0, 一个停止位 One = 1,使用两个停止位 Two = 2,使用 1.5 停止位 OnePointFive = 3   
            StopBits sb = StopBits.None;
            switch (iStopBits)
            {
                case 0:
                    sb = StopBits.None;
                    break;
                case 1:
                    sb = StopBits.One;
                    break;
                case 2:
                    sb = StopBits.Two;
                    break;
                case 3:
                    sb = StopBits.OnePointFive;
                    break;
                default:
                    break;
            }

            sp.PortName = strPortName;
            sp.BaudRate = iRate;         //波特率
            sp.DataBits = iDataBits;     //数据位
            sp.Parity = par;     // 奇偶校验
            sp.StopBits = sb;  // 停止

            try
            {
                sp.Open();
                return sp.IsOpen;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            try
            {
                sp.Dispose();
            }
            catch (Exception)
            {  }
        }
        /// <summary>
        /// 字符串写串口
        /// </summary>
        /// <param name="strWrite"></param>
        /// <returns></returns>
        public bool Write(string strWrite)
        {
            try
            {
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                sp.Write(strWrite);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// HEX写入
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public bool Write(byte[] cmd)
        {
            try
            {
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                sp.Write(cmd, 0, cmd.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// HEX写入
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public bool Write_StringToHex(string cmd)
        {
            try
            {
                byte[] b = strToHexByte(cmd);
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                sp.Write(b, 0, b.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ConnectStates()
        {
            try
            {
                return sp.IsOpen;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <returns></returns>
        public string readSting()
        {
            try
            {
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();// 显示汉字与字符
                Byte[] readBytes = new Byte[sp.BytesToRead];
                sp.Read(readBytes, 0, readBytes.Length);
                string decodedString = utf8.GetString(readBytes);
                return decodedString;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 读取字符串16进制格式
        /// </summary>
        /// <returns></returns>
        public string readStingByHex()
        {
            try
            {
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();// 显示汉字与字符
                Byte[] readBytes = new Byte[sp.BytesToRead];
                sp.Read(readBytes, 0, readBytes.Length);               
                string decodedString = ToHexStrFromByte(readBytes);
                return decodedString;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 读串口字符串数据
        /// </summary>
        /// <returns></returns>
        public string ReadStr()
        {
            try
            {
                string indata = sp.ReadExisting();
                return indata;
            }
            catch (Exception ex)
            {
                return "Err";
            }
        }
        /// <summary>
        /// 读取16进制字符数组
        /// </summary>
        /// <returns></returns>
        public byte[] readChar()
        {
            try
            {
                int iBufferSize = sp.ReadBufferSize;
                byte[] buf = new byte[iBufferSize];//声明一个临时数组存储当前来的串口数据  (byte型 数据)
                sp.Read(buf, 0, iBufferSize);//读取缓冲数据
                sp.DiscardInBuffer();
                return buf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ClearInBuffer()
        {
            try
            {
                sp.DiscardInBuffer();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ClearOutBuffer()
        {
            try
            {
                sp.DiscardOutBuffer();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ConnectComPressL(string strPortName, string CommunicationPara)
        {
            try
            {
                //string strPortName = GetSettingValue("PSet", "ProtNomL");
                //string CommunicationPara = GetSettingValue("PSet", "CommunicationParaL");
                string[] condition = { "," };
                string[] result = CommunicationPara.Split(condition, StringSplitOptions.RemoveEmptyEntries);
                int iRate = Convert.ToInt32(result[0]);
                int iDataBits = Convert.ToInt32(result[1]);
                int iParity = Convert.ToInt32(result[2]);
                int iStopBits = Convert.ToInt32(result[3]);
                return Connect(strPortName, iRate, iDataBits, iParity, iStopBits);//PSet.Com奇偶校验   没有奇偶None = 0,   奇校验Odd = 1,
            }
            catch (Exception)
            {
                return false;
            }
        }

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

        /// <summary>
        /// 字节数组转16进制字符串：空格分隔
        /// </summary>
        /// <param name="byteDatas"></param>
        /// <returns></returns>
        public string ToHexStrFromByte(byte[] byteDatas)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < byteDatas.Length; i++)
            {
                builder.Append(string.Format("{0:X2} ", byteDatas[i]));
            }
            return builder.ToString().Trim();
        }
    }
}

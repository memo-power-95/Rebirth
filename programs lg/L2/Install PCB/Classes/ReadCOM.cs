using Alpha._0.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alpha._0
{
    public class ReadCOM
    {
        System.Timers.Timer Timer_Com1 = new System.Timers.Timer();
        System.Timers.Timer Timer_Com2 = new System.Timers.Timer();
        System.Timers.Timer Timer_Com3 = new System.Timers.Timer();



        NPSerialProt SPCom1 = new NPSerialProt();
        NPSerialProt SPCom2 = new NPSerialProt();
        public static DataTable dtTable_Com1 = new DataTable();
        public static DataTable dtTable_Com2 = new DataTable();
        public static DataTable dtTable_Com3 = new DataTable();
        #region COM1
        public bool ConnectCom1(string comStr, string comParaStr)
        {
            try
            {
                bool b0 = SPCom1.ConnectComPressL(comStr, comParaStr);
                bool ret = SPCom1.ConnectStates();
                bool b1 = SPCom1.Write(":001connect\r\n");
                Thread.Sleep(200);
                string strMid = SPCom1.readSting();
                if (strMid.Contains("OK"))
                {
                    b1 = true;
                }
                else
                {
                    b1 = false;
                }
                return b0 & b1;
            }
            catch (Exception ex)
            {
               
                return false;
            }
            
        }

        public bool ConnectCom1()
        {
            bool b0 = SPCom1.ConnectComPressL(MiddleLayer.GantryF.GetSettingValue("MSet", "ComPort"), "9600,8,0,1");
            bool b1 = SPCom1.Write(":001connect\r\n");
            return b0 & b1;
        }
        public void DisconnectCom1()
        {
            if (SPCom1.ConnectStates())
                SPCom1.Dispose();
        }
        /// <summary>
        /// 读取COM1内容
        /// </summary>
        /// <returns></returns>
        public string ReadContent_COM1()
        {
            try
            {
                string strContent = "";
                if (!SPCom1.ConnectStates())
                    ConnectCom1();
                bool b1 = SPCom1.Write(":001RDGROSS=0" + "\r\n");//:001RDGROSS="READ[OD][OA]"   获取称重值
                Thread.Sleep(100);
                string strMid = SPCom1.readSting();
                if (strMid != null)
                {
                    if (strMid.Length > 0)
                    {
                        strMid = strMid.Trim().Replace("\r\n", "").Trim();

                        string r1 = strMid;
                        r1 = r1.Replace("+", "-");
                        string[] strArray = r1.Split('-');
                        if (strArray.Length > 0)
                        {
                            r1 = strArray[strArray.Length - 1];
                            r1 = r1.Replace("?", "");
                        }
                        strContent = r1.Trim('.');
                        if (strContent.Contains(','))
                        {
                            strContent = strContent.Split(',')[1];
                        }

                    }
                }

                return strContent;
            }
            catch (Exception)
            {
                return "0";
                throw;
            }
            
        }

        public void COM1_ClearZero()
        {
            bool b1 = SPCom1.Write(":001CLSZERO=0" + "\r\n");
            Thread.Sleep(100);
        }


        /// <summary>
        /// COM1当前读取到的压力值
        /// </summary>
        public double _CurrentCom1_Pressure = 0;
        /// <summary>
        /// X2轴运行的状态
        /// </summary>
        public bool _AxisX2_RunComplete = true;
        /// <summary>
        /// Com1监测是否结束
        /// </summary>
        public bool _Com1Read_Complete = false;
        /// <summary>
        /// 连续读取COM1压力值
        /// </summary>
        public void ContinuedRead_COM1()
        {
            if (!SPCom1.ConnectStates())
                ConnectCom1();
            MiddleLayer.GantryF.Chart1Clear_Pressure_RubberSheet();

            dtTable_Com1 = new DataTable();
            dtTable_Com1.Columns.Add("Index", typeof(int));
            dtTable_Com1.Columns.Add("Pressure", typeof(double));
            dtTable_Com1.Columns.Add("Times", typeof(int));
            MiddleLayer.GantryF.RunTM.Restart();
            COM1_ClearZero();
            Task.Factory.StartNew(() =>
            {
                #region MyRegion
                _Com1Read_Complete = false;
            while (!_Com1Read_Complete)
            {
                    string strPressure = ReadContent_COM1();
                    //string strPressure = "0";
                    if (strPressure.Trim().Length > 0)
                {
                    double _Pressure;
                    if (double.TryParse(strPressure, out _Pressure))
                    {
                        //if (_CurrentCom1_Pressure != _Pressure || _AxisX2_RunComplete)
                        //{
                        _CurrentCom1_Pressure = _Pressure;

                        double _PressureN = _Pressure / 100;
                        int _time = Convert.ToInt32((dtTable_Com1.Rows.Count + 1) * Timer_Com1.Interval);
                        DataRow dtRow = dtTable_Com1.NewRow();
                        dtRow["Index"] = dtTable_Com1.Rows.Count + 1;
                        dtRow["Pressure"] = Math.Round(_PressureN, 3);
                        dtRow["Times"] = _time;
                        dtTable_Com1.Rows.Add(dtRow);
                        MiddleLayer.GantryF.Chart1Add_Pressure_RubberSheet(dtTable_Com1.Rows.Count + 1, _PressureN);
                        //if (MiddleLayer.GantryF.RunTM.IsOn(10000))
                        {
                            if (SysPara.RobotPressureCompelet || MiddleLayer.GantryF.RunTM.On(10000))
                            {
                                strPressure = ReadContent_COM1();
                                if (strPressure.Trim().Length > 0)
                                {
                                    //if (double.TryParse(strPressure, out _Pressure))
                                    //{
                                    //if (_CurrentCom1_Pressure != _Pressure)
                                    //{
                                    //_PressureN = _Pressure / 100 * 9.8;
                                    _time = Convert.ToInt32((dtTable_Com1.Rows.Count + 1) * Timer_Com1.Interval);
                                    dtRow = dtTable_Com1.NewRow();
                                    dtRow["Index"] = dtTable_Com1.Rows.Count + 1;
                                    dtRow["Pressure"] = Math.Round(_PressureN, 3);
                                    dtRow["Times"] = _time;
                                    dtTable_Com1.Rows.Add(dtRow);
                                    MiddleLayer.GantryF.Chart1Add_Pressure_RubberSheet(dtTable_Com1.Rows.Count + 1, _PressureN, "Over");
                                    //}
                                    //}
                                }
                                _Com1Read_Complete = true;
                                COM1_ClearZero();
                            }
                        }
                        //}
                    }
                }
                    Thread.Sleep(100);
            }
                #endregion
            });
        }
        #endregion





        #region COM2
        public bool ConnectCom2()
        {
            return SPCom2.ConnectComPressL("COM2", "9600,7,2,1");
        }
        public void DisconnectCom2()
        {
            if (SPCom2.ConnectStates())
                SPCom2.Dispose();
        }
        /// <summary>
        /// 读取COM2内容
        /// </summary>
        /// <returns></returns>
        public string ReadContent_COM2()
        {
            try
            {
                string strContent = "";
                if (!SPCom1.ConnectStates())
                    ConnectCom1();
                bool b1 = SPCom1.Write(":002RDGROSS=0" + "\r\n");//:001RDGROSS= READ[OD][OA]  获取称重值
                Thread.Sleep(100);
                string strMid = SPCom1.readSting();
                if (strMid.Length > 0)
                {
                    strMid = strMid.Trim().Replace("\r\n", "").Trim();

                    string r1 = strMid;
                    r1 = r1.Replace("+", "-");
                    string[] strArray = r1.Split('-');
                    if (strArray.Length > 0)
                    {
                        r1 = strArray[strArray.Length - 1];
                        r1 = r1.Replace("?", "");
                    }
                    strContent = r1.Trim('.');
                    if (strContent.Contains(','))
                    {
                        strContent = strContent.Split(',')[1];
                    }
                }
                return strContent;
            }
            catch (Exception ex)
            {
                return "0";
            }
            
        }

        public void COM2_ClearZero()
        {
            bool b1 = SPCom1.Write(":002CLSZERO=0" + "\r\n");
        }
        /// <summary>
        /// COM1当前读取到的压力值
        /// </summary>
        public double _CurrentCom2_Pressure = 0;
        /// <summary>
        /// X2轴运行的状态
        /// </summary>
        public bool _Com2Read_Complete = false;
        /// <summary>
        /// Com1监测是否结束
        /// </summary>
        public bool _AxisZ2_RunComplete = true;
        /// <summary>
        /// 连续读取COM1压力值
        /// </summary>
        public void ContinuedRead_COM2()
        {
            if (!SPCom1.ConnectStates())
                ConnectCom1();
            MiddleLayer.GantryF.Chart2Clear_Pressure_RubberSheet();

            dtTable_Com2 = new DataTable();
            dtTable_Com2.Columns.Add("Index", typeof(int));
            dtTable_Com2.Columns.Add("Pressure", typeof(double));
            dtTable_Com2.Columns.Add("Times", typeof(int));
            MiddleLayer.GantryF.RunTM.Restart();
            COM2_ClearZero();
            Task.Factory.StartNew(() =>
            {
                #region MyRegion
                _Com2Read_Complete = false;
            while (!_Com2Read_Complete)
            {
                string strPressure = ReadContent_COM2();
                if (strPressure.Trim().Length > 0)
                {
                    double _Pressure;
                    if (double.TryParse(strPressure, out _Pressure))
                    {
                        //if (_CurrentCom2_Pressure != _Pressure || _AxisZ2_RunComplete)
                        //{
                        _CurrentCom2_Pressure = _Pressure;

                        double _PressureN = _Pressure / 100;
                        int _time = Convert.ToInt32((dtTable_Com2.Rows.Count + 1) * Timer_Com2.Interval);
                        DataRow dtRow = dtTable_Com2.NewRow();
                        dtRow["Index"] = dtTable_Com2.Rows.Count + 1;
                        dtRow["Pressure"] = Math.Round(_PressureN, 3);
                        dtRow["Times"] = _time;
                        dtTable_Com2.Rows.Add(dtRow);
                        MiddleLayer.GantryF.Chart2Add_Pressure_RubberSheet(dtTable_Com2.Rows.Count + 1, _PressureN);
                        //if (MiddleLayer.GantryF.RunTM.IsOn(10000))
                        {
                            if (SysPara.RobotPressureCompelet || MiddleLayer.GantryF.RunTM.On(10000))
                            {
                                strPressure = ReadContent_COM2();
                                if (strPressure.Trim().Length > 0)
                                {
                                    //if (double.TryParse(strPressure, out _Pressure))
                                    //{
                                    //if (_CurrentCom2_Pressure != _Pressure)
                                    //{
                                    //_PressureN = _Pressure / 100 * 9.8;
                                    _time = Convert.ToInt32((dtTable_Com2.Rows.Count + 1) * Timer_Com1.Interval);
                                    dtRow = dtTable_Com2.NewRow();
                                    dtRow["Index"] = dtTable_Com2.Rows.Count + 1;
                                    dtRow["Pressure"] = Math.Round(_PressureN, 3);
                                    dtRow["Times"] = _time;
                                    dtTable_Com2.Rows.Add(dtRow);
                                    MiddleLayer.GantryF.Chart2Add_Pressure_RubberSheet(dtTable_Com2.Rows.Count + 1, _PressureN, "Over");
                                    //}
                                    //}
                                }
                                _Com2Read_Complete = true;
                                COM2_ClearZero();
                            }
                        }
                        //}
                    }
                }
            }
                #endregion
            });
        }
        #endregion

        #region COM3
        public bool ConnectCom3()
        {
            return SPCom1.ConnectComPressL("COM8", "9600,7,2,1");
        }
        public void DisconnectCom3()
        {
            if (SPCom2.ConnectStates())
                SPCom2.Dispose();
        }
        /// <summary>
        /// 读取COM2内容
        /// </summary>
        /// <returns></returns>
        public string ReadContent_COM3()
        {
            try
            {
                string strContent = "";
                if (!SPCom1.ConnectStates())
                    ConnectCom1();
                bool b1 = SPCom1.Write(":003RDGROSS=0" + "\r\n");//:001RDGROSS= READ[OD][OA]  获取称重值
                Thread.Sleep(100);
                string strMid = SPCom1.readSting();
                if (strMid.Length > 0)
                {
                    strMid = strMid.Trim().Replace("\r\n", "").Trim();

                    string r1 = strMid;
                    r1 = r1.Replace("+", "-");
                    string[] strArray = r1.Split('-');
                    if (strArray.Length > 0)
                    {
                        r1 = strArray[strArray.Length - 1];
                        r1 = r1.Replace("?", "");
                    }
                    strContent = r1.Trim('.');
                    if (strContent.Contains(','))
                    {
                        strContent = strContent.Split(',')[1];
                    }
                }
                return strContent;
            }
            catch (Exception ex)
            {
                return "0";
            }
            
        }

        public void COM3_ClearZero()
        {
            bool b1 = SPCom1.Write(":003CLSZERO=0" + "\r\n");
        }
        /// <summary>
        /// COM1当前读取到的压力值
        /// </summary>
        public double _CurrentCom3_Pressure = 0;
        /// <summary>
        /// X2轴运行的状态
        /// </summary>
        public bool _Com3Read_Complete = false;
        /// <summary>
        /// Com1监测是否结束
        /// </summary>
        public bool _AxisZ3_RunComplete = true;
        /// <summary>
        /// 连续读取COM1压力值
        /// </summary>
        public void ContinuedRead_COM3()
        {
            if (!SPCom1.ConnectStates())
                ConnectCom1();
            MiddleLayer.GantryF.Chart3Clear_Pressure_RubberSheet();


            dtTable_Com3 = new DataTable();
                dtTable_Com3.Columns.Add("Index", typeof(int));
                dtTable_Com3.Columns.Add("Pressure", typeof(double));
                dtTable_Com3.Columns.Add("Times", typeof(int));
           
            
            MiddleLayer.GantryF.RunTM.Restart();
            COM3_ClearZero();
            Task.Factory.StartNew(() =>
            {
                #region MyRegion
                _Com3Read_Complete = false;
            while (!_Com3Read_Complete)
            {
                string strPressure = ReadContent_COM3();
                if (strPressure.Trim().Length > 0)
                {
                    double _Pressure;
                    if (double.TryParse(strPressure, out _Pressure))
                    {
                        //if (_CurrentCom2_Pressure != _Pressure || _AxisZ2_RunComplete)
                        //{
                        _CurrentCom3_Pressure = _Pressure;

                        double _PressureN = _Pressure / 100;
                        int _time = Convert.ToInt32((dtTable_Com3.Rows.Count + 1) * Timer_Com3.Interval);
                        DataRow dtRow = dtTable_Com3.NewRow();
                        dtRow["Index"] = dtTable_Com3.Rows.Count + 1;
                        dtRow["Pressure"] = Math.Round(_PressureN, 3);
                        dtRow["Times"] = _time;
                        dtTable_Com3.Rows.Add(dtRow);
                        MiddleLayer.GantryF.Chart3Add_Pressure_RubberSheet(dtTable_Com3.Rows.Count + 1, _PressureN);
                        //if (MiddleLayer.GantryF.RunTM.IsOn(10000))
                        {
                            if (SysPara.RobotPressureCompelet || MiddleLayer.GantryF.RunTM.On(10000))
                            {
                                
                                strPressure = ReadContent_COM3();
                                if (strPressure.Trim().Length > 0)
                                {
                                    //if (double.TryParse(strPressure, out _Pressure))
                                    //{
                                    //if (_CurrentCom2_Pressure != _Pressure)
                                    //{
                                    //_PressureN = _Pressure / 100 * 9.8;
                                    _time = Convert.ToInt32((dtTable_Com3.Rows.Count + 1) * Timer_Com3.Interval);
                                    dtRow = dtTable_Com3.NewRow();
                                    dtRow["Index"] = dtTable_Com3.Rows.Count + 1;
                                    dtRow["Pressure"] = Math.Round(_PressureN, 3);
                                    dtRow["Times"] = _time;
                                    dtTable_Com3.Rows.Add(dtRow);
                                    MiddleLayer.GantryF.Chart3Add_Pressure_RubberSheet(dtTable_Com3.Rows.Count + 1, _PressureN, "Over");
                                    //}
                                    //}
                                }
                                _Com3Read_Complete = true;
                                COM3_ClearZero();
                            }
                        }
                        //}
                    }
                }
            }
                #endregion
            });
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro.Display;

/// <summary>
/// Version 1.1 20191221 增加图片保存
/// Version 1.2 20191221 增加文件夹删除超期文件和文件夹功能
/// Version 1.3 20200810 增加保存路径+生产数据列首字字符串外部设置参数+流程Log
/// Version 1.4 20200810 增加设备运行状态Log，Error，Pause，Run，Stop
/// Version 1.5 20200817  保存生产数据/tar数据传入数据改为数组格式
/// Version 1.6 20200819  在生产数据文件里，查找某个特定列的特定数据，并反馈查找列数据+保存数据和Tar增加自定义保存基路径下文件夹名称
/// Version 1.7 20200819  增加Recipe和Setting参数改变保存修改数据
/// </summary>

namespace Alpha._0.Classes
{
    class SaveFile
    {

        /// <summary>
        /// TarFile上传路径
        /// </summary>
        public static string strTarPath = "C://tar";
        /// <summary>
        /// 本地文件数据保存路径
        /// </summary>
        public static string strFileRotePath = "D://DataSave";
        /// <summary>
        /// 保存生产数据列首字符串，不同列名称用英文逗号,间隔
        /// </summary>
        public static string strDataColumnName = "";//"SaveTime,Station,PosX,TimesY,ResultZ,XOffset,YOffset,AngOffset"
        /// <summary>
        /// 保存csv文件，分列字符串用英文逗号间隔
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fullPath"></param>
        public static void SaveCSV(string str, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            sw.WriteLine(str);
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 保存csv文件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fullPath"></param>
        public static void SaveCSV(string[] str, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            string strMid = "";
            for (int i = 0; i < str.Length; i++)
            {
                strMid += str[i] + ",";
            }
            sw.WriteLine(strMid);
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 保存报错信息；保存文件的根目录如D：/aaa/11.txt
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="sSaveData"></param>
        public static void SaveErr(string sSaveData)
        {
            //数据记录
            string sfileName = strFileRotePath + "/Err";
            string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            SaveCSV(DateTime.Now.ToString("HH:mm:ss") + "," + sSaveData, strFileName);
        }
        /// <summary>
        /// 用户保存 
        /// </summary>
        /// <param name="sUser"></param>
        public static void SaveUser(string sUser)
        {
            //数据记录
            string sfileName = strFileRotePath + "/User";
            string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            SaveCSV(DateTime.Now.ToString("HH:mm:ss") + "," + sUser, strFileName);
        }
        /// <summary>
        /// 保存生产数据 不同数组编号不同列数据
        /// </summary>
        /// <param name="sSaveData"></param>
        public static void SaveData(string[] sSaveData)
        {
            //数据记录
            string sfileName = strFileRotePath + "/ProductData";
            string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

            if (!Directory.Exists(sfileName))
            {
                Directory.CreateDirectory(sfileName);
            }
            if (!File.Exists(strFileName))
            {
                SaveCSV("时间,输出状态", strFileName);//列首名称
            }
            string strMid = "";
            string time = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            string state = "OK";//状态
            strMid = time + "," + state + ",";
            //strMid = state + ",";
            for (int i = 0; i < sSaveData.Length; i++)
            {
                strMid += sSaveData[i] + ",";
            }
            SaveCSV(strMid, strFileName);
        }
        public static void SaveData3(string[] sSaveData)
        {
            //数据记录
            string sfileName = strFileRotePath + "/TryRun";
            string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

            if (!Directory.Exists(sfileName))
            {
                Directory.CreateDirectory(sfileName);
            }
            if (!File.Exists(strFileName))
            {
                SaveCSV("Time", strFileName);//列首名称
            }
            string strMid = "";
            //string time = DateTime.Now.ToString("HHmmss");
            //string state = "NG";//扫码状态
            //strMid = time + "," + state + ",";
            //strMid = state + ",";
            for (int i = 0; i < sSaveData.Length; i++)
            {
                strMid += sSaveData[i] + ",";
            }
            SaveCSV(strMid, strFileName);
        }
        public static void SaveData1(string[] sSaveData)
        {
            //数据记录
            string sfileName = strFileRotePath + "/backup";
            string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

            if (!Directory.Exists(sfileName))
            {
                Directory.CreateDirectory(sfileName);
            }
            if (!File.Exists(strFileName))
            {
                SaveCSV("扫码时间,扫码状态,条码内容", strFileName);//列首名称
            }
            string strMid = "";
            string time = DateTime.Now.ToString("HHmmss");
            string state = "OK";//扫码状态
            strMid = time + "," + state + ",";
            //strMid = state + ",";
            for (int i = 0; i < sSaveData.Length; i++)
            {
                strMid += sSaveData[i] + ",";
            }
            SaveCSV(strMid, strFileName);
        }
        public static void SaveData2(string[] sSaveData)
        {
            //数据记录
            string sfileName = strFileRotePath + "/backup";
            string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

            if (!Directory.Exists(sfileName))
            {
                Directory.CreateDirectory(sfileName);
            }
            if (!File.Exists(strFileName))
            {
                SaveCSV("扫码时间,扫码状态,条码内容", strFileName);//列首名称
            }
            string strMid = "";
            string time = DateTime.Now.ToString("HHmmss");
            string state = "NG";//扫码状态
            strMid = time + "," + state + ",";
            //strMid = state + ",";
            for (int i = 0; i < sSaveData.Length; i++)
            {
                strMid += sSaveData[i] + ",";
            }
            SaveCSV(strMid, strFileName);
        }
        /// <summary>
        /// 保存生产数据 不同数组编号不同列数据,自定义保存数据的文件夹路径
        /// </summary>
        /// <param name="sSaveData"></param>
        public static void SaveData(string FileName, string header, string sSaveData)
        {
            //数据记录
            string sfileName = FileName;
            string MesID = "NA";
            MesID = MiddleLayer.MesF2.GetSettingValue("PSet", "EQPID");
            if (MesID == null || MesID == "")
                MesID = "NA";
            string strFileName = sfileName + "\\" + MesID + "_NA_" + DateTime.Now.ToString("yyyyMMddHH") + "_UIDataLog.csv";

            if (!Directory.Exists(sfileName))
            {
                Directory.CreateDirectory(sfileName);
            }
            if (!File.Exists(strFileName))
            {
                SaveCSV(header, strFileName);//列首名称
            }
            //string strMid = "";
            //for (int i = 0; i < sSaveData.Length; i++)
            //{
            //    strMid += sSaveData[i] + ",";
            //}
            SaveCSV(sSaveData, strFileName);
        }
        /// <summary>
        /// 保存tar文件 "C:\\tar\\aa.tar" 不同传入输入列为不同行数据
        /// </summary>
        /// <param name="sFullFilePath"></param>
        /// <param name="sSaveData"></param>
        /// <returns></returns>
        public static void SaveTar(string strTarName, string[] sSaveData)
        {
            //本地存储Tar文件夹
            string sfileName = strFileRotePath + "/Tar/" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!Directory.Exists(sfileName))
            {
                Directory.CreateDirectory(sfileName);
            }
            //tar上传文件夹
            if (!Directory.Exists(strTarPath))
            {
                Directory.CreateDirectory(strTarPath);
            }

            string sFullFilePath;
            FileStream fs;
            StreamWriter sw;
            for (int j = 0; j < 2; j++)
            {
                if (j == 0)
                {
                    sFullFilePath = sfileName + "/" + DateTime.Now.ToString("HHmmss") + "-" + strTarName;
                }
                else
                {
                    sFullFilePath = strTarPath + "/" + strTarName;
                }
                fs = new FileStream(sFullFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                for (int i = 0; i < sSaveData.Length; i++)
                {
                    sw.WriteLine(sSaveData[i]);
                }
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 保存tar文件 "C:\\tar\\aa.tar" 不同传入输入列为不同行数据,本地保存路径文件夹名称
        /// </summary>
        /// <param name="sFullFilePath"></param>
        /// <param name="sSaveData"></param>
        /// <returns></returns>
        public static void SaveTar(string strLocalFileName, string strTarName, string[] sSaveData)
        {
            //本地存储Tar文件夹
            string sfileName = strLocalFileName + "//" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!Directory.Exists(sfileName))
            {
                Directory.CreateDirectory(sfileName);
            }
            //tar上传文件夹
            if (!Directory.Exists(strTarPath))
            {
                Directory.CreateDirectory(strTarPath);
            }

            string sFullFilePath;
            FileStream fs;
            StreamWriter sw;
            for (int j = 0; j < 2; j++)
            {
                if (j == 0)
                {
                    sFullFilePath = sfileName + "/" + DateTime.Now.ToString("HHmmss") + "-" + strTarName;
                }
                else
                {
                    sFullFilePath = strTarPath + "/" + strTarName;
                }
                fs = new FileStream(sFullFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                for (int i = 0; i < sSaveData.Length; i++)
                {
                    sw.WriteLine(sSaveData[i]);
                }
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>  
        /// 保存CogDisplay控件的内容到指定文件路径+图片名称
        /// </summary>  
        /// <param name="displayControl">控件对象</param>  
        /// <param name="filePath">文件路径</param>  
        //public static bool SaveImage(CogDisplay displayControl, string fileName)//"Picture.png"  
        //{
        //    try
        //    {
        //        string strFileName = strFileRotePath+"/Picture/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + fileName;
        //        if (displayControl.Image == null)
        //            return true;
        //        string directoryName = Path.GetDirectoryName(strFileName);
        //        if (!Directory.Exists(directoryName))
        //        {
        //            Directory.CreateDirectory(directoryName);
        //        }
        //        displayControl.CreateContentBitmap(Cognex.VisionPro.Display.CogDisplayContentBitmapConstants.Image).Save(strFileName);
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //}
        /// <summary>
        /// 删除设定文件夹内的文件夹和文件（创建时间超过设定天数）
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="iSaveDays"></param>
        public static void DeleteFile(string strFile, int iSaveDays)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(strFile);//字符串转路径
                FileSystemInfo[] fileInfo = dir.GetFileSystemInfos();//获取路径下的所有文件信息


                foreach (FileSystemInfo info in fileInfo)//遍历文件
                {
                    TimeSpan spand = DateTime.Now - info.CreationTime.Date;//文件距离当前时间差

                    int s = spand.Days;
                    if (s > iSaveDays)//文件创建时间是否超过设定天数
                    {
                        if (info is DirectoryInfo)//判断是否是文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(info.FullName);
                            subdir.Delete(true);//删除文件
                        }
                        else//文件删除文件
                        {
                            File.Delete(info.FullName);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public static void DeleteFile1(string strFile, int iSaveDays)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(strFile);//字符串转路径
                FileSystemInfo[] fileInfo = dir.GetFileSystemInfos();//获取路径下的所有文件信息
                int SaveDays = 21;

                foreach (FileSystemInfo info in fileInfo)//遍历文件
                {
                    TimeSpan spand = DateTime.Now - info.CreationTime.Date;//文件距离当前时间差
                    if (info.Extension == ".bmp")
                    {
                        SaveDays = iSaveDays;
                    }
                    else if (info.Extension == ".csv")
                    {
                        SaveDays = 365;
                    }
                    int s = spand.Days;
                    if (s > SaveDays)//文件创建时间是否超过设定天数
                    {
                        if (info is DirectoryInfo)//判断是否是文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(info.FullName);
                            subdir.Delete(true);//删除文件
                        }
                        else//文件删除文件
                        {
                            File.Delete(info.FullName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public static bool DriveFreeSpace(double totalSize)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Fixed && drive.Name == @"D:\")
                {
                    //long total = drive.TotalSize / (1024 * 1024 * 1024);
                    long free = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                    if (free < totalSize)
                    {
                        return true;
                    }
                }
            }
            return false;
        }




        private static string[] strOlderLog = new string[255];
        private static object lockobject = new object();
        /// <summary>
        /// 保存流程Log，如果想分列存储请将保存字符串列用英文逗号,间隔，最多255个
        /// </summary>
        /// <param name="sSaveLog"></param>      
        public static void SaveLog(int iFlowNo, string sSaveLog)
        {
            Task.Factory.StartNew(() =>
            {
                lock (lockobject)
                {
                    if (strOlderLog[iFlowNo] != sSaveLog)
                    {
                        strOlderLog[iFlowNo] = sSaveLog;
                        //数据记录
                        string sfileName = strFileRotePath + "/FlowLog";
                        string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

                        if (!Directory.Exists(sfileName))
                        {
                            Directory.CreateDirectory(sfileName);
                        }
                        if (!File.Exists(strFileName))
                        {
                            SaveCSV(strDataColumnName, strFileName);//列首名称
                        }
                        SaveCSV(DateTime.Now.ToString("HH:mm:ss:fff") + "," + sSaveLog, strFileName);
                    }
                }
            });
        }

        /// <summary>
        /// 保存设备状态信息，Error，Pause，Stop，Run
        /// </summary>


        /// <summary>
        /// 保存参数修改数据，用户，参数名，原来数据内容，最新的数据内容跟
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="ParaName"></param>
        /// <param name="strOldValue"></param>
        /// <param name="strNewValue"></param>
        public static void SaveSettingChange(string strUser, string ParaName, string strOldValue, string strNewValue)
        {
            Task.Factory.StartNew(() =>
            {
                lock (lockobject)
                {
                    //数据记录
                    string sfileName = strFileRotePath + "/SettingChangeLog";
                    string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
                    if (!Directory.Exists(sfileName))
                    {
                        Directory.CreateDirectory(sfileName);
                    }
                    if (!File.Exists(strFileName))
                    {
                        SaveCSV(strDataColumnName, strFileName);//列首名称
                    }
                    SaveCSV(DateTime.Now.ToString("HH:mm:ss:fff") + "," + strUser + "," + ParaName + ",Old:" + strOldValue + ",New:" + strNewValue, strFileName);
                }
            });
        }

        /// <summary>
        /// 在生产数据文件里查询指定天数csv文件，并查找指定列的内容，并返回特定列的信息
        /// </summary>
        public static List<string[]> listTotalData = new List<string[]>();//数组List，相当于可以无限扩大的二维数组。
        public static string strSearchFile = strFileRotePath + "/ProductData";//默认保存路径
        /// <summary>
        /// 到指定路径下面查找iDays天前的数据并存入list Total DATa列表内
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="iDays"></param>
        /// <returns></returns>
        public static void readCsv(int iDays)
        {
            listTotalData.Clear();
            DateTime dtNow = DateTime.Now;
            for (int i = 0; i <= iDays; i++)
            {
                string strFileMid = strSearchFile + "//" + dtNow.AddDays(-1 * i).ToString("yyyy-MM-dd") + ".csv";
                //没有对应日期文件
                if (!File.Exists(strFileMid))
                {
                    continue;
                }
                StreamReader reader = new StreamReader(strFileMid);
                string line = "";
                List<string[]> listStrArr = new List<string[]>();//数组List，相当于可以无限扩大的二维数组。
                line = reader.ReadLine();//读取一行数据
                while (line != null)
                {
                    if (line != "")
                    {
                        listStrArr.Add(line.Split(','));//将文件内容分割成数组
                    }
                    line = reader.ReadLine();
                }
                reader.Dispose();
                // listTotalData = listA.Concat(listB).ToList<int>();Concat          //保存重复项
                listTotalData = listStrArr.Union(listTotalData).ToList<string[]>();          //剔除重复项
            }
        }
        /// <summary>
        /// 在所有数据里面查找列iCodeColumn编号内容等于strBarcode，返回对应行所有数据
        /// </summary>
        /// <param name="iCodeColumn"></param>
        /// <param name="strBarcode"></param>
        /// <param name="iWeightColumn"></param>
        /// <returns></returns>
        public static string SearchCode(int iCodeColumn, string strBarcode, int iWeightColumn)
        {
            if (listTotalData.Count == 0)
            {
                return "Err donot find the barcode no data";
            }

            List<string[]> listSearch = listTotalData;
            listSearch.Reverse();
            string sMid;
            int iMid;

            //bool bFindIT=false;
            //for (int i = 0; i < listSearch.Count; i++)
            //{
            //    iMid = i;
            //    sMid = listSearch[i][iCodeColumn];
            //    sMid = sMid.Trim();
            //    if (sMid== strBarcode.Trim())
            //    {
            //        bFindIT = true;
            //        break;
            //    }
            //}
            //iMid = 0;
            iMid = listSearch.FindIndex(x => x[iCodeColumn].Trim() == strBarcode.Trim());//返回列编号
            if (iMid == -1)
            {
                return "Err donot find the barcode";
            }
            else
            {
                sMid = listSearch[iMid][iWeightColumn];
                return sMid;
            }

        }
        /// <summary>
        /// 增加查询数据，数组形式
        /// </summary>
        /// <param name="strData"></param>
        public static void ListAddData(string[] strData)
        {
            listTotalData.Add(strData);
        }




        /// <summary>
        /// Sava Parameter Change
        /// </summary>
        /// <param name="ModifyData"></param>
        public static void SaveRecipeChange(string ModifyData)
        {
            string strFileName = strFileRotePath + "//ParameterChange" + "//" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            FileInfo fi = new FileInfo(strFileName);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            if (!File.Exists(strFileName))
            {
                File.AppendAllText(strFileName, "DateTime" + "," + "User" + "," + "Recipe/Setting" + "," + "Parameter Name" + "," + "Original Value" + "," + "Update Value" + "," + "\r\n");

            }
            SaveCSV(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + SysPara.UserName + "," + ModifyData, strFileName);
        }


        public static void SaveMES(string sBarcode, string Data, bool tp, string sTime)
        {
            SysPara.CurrentName = MiddleLayer.GantryF.GetRecipeValue("RSet", "CurrentN");
            SysPara.StationName = MiddleLayer.GantryF.GetRecipeValue("RSet", "StationN");
            SysPara.PCName = MiddleLayer.GantryF.GetRecipeValue("RSet", "PCN");
            SysPara.DName = MiddleLayer.GantryF.GetRecipeValue("RSet", "DN");
            SysPara.RName = MiddleLayer.GantryF.GetRecipeValue("RSet", "RN");
            SysPara.MPName = "MProgram Name:" + MiddleLayer.GantryF.GetRecipeValue("RSet", "MPN");
            SysPara.MCIDName = "MCamera ID:" + MiddleLayer.GantryF.GetRecipeValue("RSet", "MCID");
            SysPara.LineName = MiddleLayer.GantryF.GetRecipeValue("RSet", "LineN");
            SysPara.STXT = MiddleLayer.GantryF.GetRecipeValue("RSet", "STXT");
            string TPFP;
            string sFileName = "D://TARS";
            string sFileNamePerson = "D://DataSave/ProductData";
            string strFileName = sFileName + "/" + sBarcode.Replace("\r", "").Replace("\n", "").Replace(":", "") + ".txt";//可以利用barcode区分开不同产品放到一个文件夹里
            string strFileNamePerson = sFileNamePerson + "/" + sBarcode.Replace("\r", "").Replace("\n", "").Replace(":", "") + ".txt";
            if (!Directory.Exists(sFileName))//创建上传MES文件夹
            {
                Directory.CreateDirectory(sFileName);
            }
            if (!Directory.Exists(sFileNamePerson))//创建本地数据保存文件夹
            {
                Directory.CreateDirectory(sFileNamePerson);
            }
            if (tp)
            {
                TPFP = "TP";
            }
            else
            {
                TPFP = "TF";
            }
            //  string sAppend ="S"+ sBarcode + "\r\n" + "CRenault Nissan" + "\r\n" + "Ncnhuanissan003" + "\r\n" + "PAutoScrew" + "\r\n" + "nXA0253680E" + "\r\n" + "DN/A" + "\r\n" + "RN/A" + "\r\n" + "nN/A" + "\r\n" + "rN/A" + "\r\n" + "WN/A" + "\r\n" + TPFP + "\r\n" + "ONISSAN" + "\r\n" + "L018A" + "\r\n" + "p11" + "\r\n" +
            //  sTime + "\r\n" + Data;
            // string sAppendPeson = "S" + sBarcode + "\r\n" + "CRenault Nissan" + "\r\n" + "Ncnhuanissan003" + "\r\n" + "PAutoScrew" + "\r\n" + "nXA0253680E"+ "\r\n" + "DN/A" + "\r\n" + "RN/A" + "\r\n" + "nN/A" + "\r\n" + "rN/A" + "\r\n" + "WN/A" + "\r\n" + TPFP + "\r\n" + "ONISSAN" + "\r\n" + "L018A" + "\r\n" + "p11" + "\r\n" +
            //  sTime + "\r\n" + Data;

            string sAppend = "S" + sBarcode.Replace("\r", "").Replace("\n", "") + "\r\n" + SysPara.CurrentName + "\r\n" + SysPara.StationName + "\r\n" + SysPara.PCName + "\r\n" + SysPara.DName + "\r\n" + SysPara.RName + "\r\n" + SysPara.MPName + "\r\n" + SysPara.MCIDName + "\r\n" + TPFP + "\r\n" + SysPara.LineName + "\r\n" + sTime;
            string sAppendPeson = "S" + sBarcode.Replace("\r", "").Replace("\n", "") + "\r\n" + SysPara.CurrentName + "\r\n" + SysPara.StationName + "\r\n" + SysPara.PCName + "\r\n" + SysPara.DName + "\r\n" + SysPara.RName + "\r\n" + SysPara.MPName + "\r\n" + SysPara.MCIDName + "\r\n" + TPFP + "\r\n" + SysPara.LineName + "\r\n" + sTime + "\r\n" + SysPara.STXT + "\r\n" + Data;
            File.AppendAllText(strFileName, sAppend);
            File.AppendAllText(strFileNamePerson, sAppendPeson);

        }


        /// <summary>

        /// 向远程文件夹保存本地内容，或者从远程文件夹下载文件到本地

        /// </summary>

        /// <param name="src">要保存的文件的路径，如果保存文件到共享文件夹，这个路径就是本地文件路径如：@"D:\1.avi"</param>

        /// <param name="dst">保存文件的路径，不含名称及扩展名</param>

        /// <param name="fileName">保存文件的名称以及扩展名</param>

        public static void Transport(string src, string dst, string fileName)

        {

            FileStream inFileStream = new FileStream(src, FileMode.Open);

            if (!Directory.Exists(dst))

            {

                Directory.CreateDirectory(dst);

            }

            dst = dst + fileName;

            if (!File.Exists(dst))

            {

                FileStream outFileStream = new FileStream(dst, FileMode.Create, FileAccess.Write);

                byte[] buf = new byte[inFileStream.Length];

                int byteCount;

                while ((byteCount = inFileStream.Read(buf, 0, buf.Length)) > 0)

                {

                    outFileStream.Write(buf, 0, byteCount);

                }

                inFileStream.Flush();

                inFileStream.Close();

                outFileStream.Flush();

                outFileStream.Close();

            }

        }

    }

}


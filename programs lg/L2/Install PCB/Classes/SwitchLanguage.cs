using AcuraLibrary;
using AcuraLibrary.Forms;
using NPSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Alpha._0.Classes
{
    public class SwitchLanguage
    {
        private static bool m_IsEnglish = false;
        public static List<LanguageName> LanguageList = new List<LanguageName>();
        public static List<LanguageName> OrderToolList = new List<LanguageName>();
        public static List<LanguageName> OrderToolStripList = new List<LanguageName>();
        public static List<ToolStripItem> toolStripList = new List<ToolStripItem>();
        public static List<Panel> PanelList = new List<Panel>();
        public static List<Control> toolList = new List<Control>();
        #region switchLanguage

        public SwitchLanguage()
        {
            GetAllControl();
            //getAllText();
            InitLanguage(SysPara.IsEnglish);
        }
        private void InitLanguage(bool isEnglish)
        {
            m_IsEnglish = isEnglish;
            int i = 0;
            int j = 0;
            LanguageList.Clear();
            string sFilePath = string.Format("{0}\\{1}.xml", SysPara.LanguageDataDirectory, "Language");
            if (File.Exists(sFilePath))
            {
                XmlDocument ReadDoc = new XmlDocument();
                ReadDoc.Load(sFilePath);
                XmlElement Element = (XmlElement)ReadDoc.SelectSingleNode("Chinese");
                if (Element != null)
                {
                    XmlNodeList FormData = Element.ChildNodes;
                    XmlNodeList ComponentData = FormData[0].ChildNodes;
                    for (int k = 0; k < ComponentData.Count; k++)
                    {
                        LanguageName CmpTextInfo = new LanguageName();
                        CmpTextInfo.English = ((XmlElement)ComponentData[k]).GetAttribute("ComponentText").Split(',')[1];
                        CmpTextInfo.Chinese = ((XmlElement)ComponentData[k]).GetAttribute("ComponentText").Split(',')[0];
                        LanguageList.Add(CmpTextInfo);
                    }
                }
            }
            foreach (Control item in toolList)
            {
                LanguageName tempTool = new LanguageName();
                if (m_IsEnglish)
                {
                    int index = LanguageList.FindIndex(c => c.Chinese == item.Text || c.English == item.Text);
                    if (index >= 0)
                    {
                        item.Text = LanguageList[index].English;
                        tempTool.index = i;
                        tempTool.Chinese = LanguageList[index].Chinese;
                        tempTool.English = LanguageList[index].English;
                        OrderToolList.Add(tempTool);
                    }
                }
                else
                {
                    int index = LanguageList.FindIndex(c => c.English == item.Text || c.Chinese == item.Text);
                    if (index >= 0)
                    {
                        item.Text = LanguageList[index].Chinese;
                        tempTool.index = i;
                        tempTool.Chinese = LanguageList[index].Chinese;
                        tempTool.English = LanguageList[index].English;
                        OrderToolList.Add(tempTool);
                    }
                }               
                i++;
            }
            foreach (ToolStripItem item in toolStripList)
            {

                LanguageName tempToolStrip = new LanguageName();
                if (m_IsEnglish)
                {
                    int index = LanguageList.FindIndex(c => c.Chinese == item.Text || c.English == item.Text);
                    if (index >= 0)
                    {
                        item.Text = LanguageList[index].English;
                        tempToolStrip.index = j;
                        tempToolStrip.Chinese = LanguageList[index].Chinese;
                        tempToolStrip.English = LanguageList[index].English;
                        OrderToolStripList.Add(tempToolStrip);
                    }
                }
                else
                {

                    int index = LanguageList.FindIndex(c => c.English == item.Text || c.Chinese == item.Text);
                    if (index >= 0)
                    {
                        item.Text = LanguageList[index].Chinese;
                        tempToolStrip.index = j;
                        tempToolStrip.Chinese = LanguageList[index].Chinese;
                        tempToolStrip.English = LanguageList[index].English;
                        OrderToolStripList.Add(tempToolStrip);
                    }
                }
                j++;                 
            }
            //if (isEnglish) 
            //{
            //    MiddleLayer.MainF.lbModifyDate.Text = "Modify Date : " + Directory.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy/MM/dd");
            //    MiddleLayer.MainF.lbProgramVer.Text = "Software Version : " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();

            //    JSDK.Alarm.Initial(string.Format("{0}\\{1}.xml", SysPara.AlarmTableDirectory, "English")); 
            //}
            //else 
            //{ 
            //    MiddleLayer.MainF.lbModifyDate.Text = "修改日期 : " + Directory.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy/MM/dd");
            //    MiddleLayer.MainF.lbProgramVer.Text = "软件版本 : " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
            //    JSDK.Alarm.Initial(string.Format("{0}\\{1}.xml", SysPara.AlarmTableDirectory, "Chinese")); 
            //}
            //JSDK.Alarm.Clear();
        }

        public void AutoSwitchLanguage(bool isEnglish)
        {
            for (int i = 0; i < OrderToolList.Count; i++)
            {
                if (isEnglish) { toolList[OrderToolList[i].index].Text = OrderToolList[i].English; }
                else { toolList[OrderToolList[i].index].Text = OrderToolList[i].Chinese; }
            }
            for (int i = 0; i < OrderToolStripList.Count; i++)
            {
                if (isEnglish) { toolStripList[OrderToolStripList[i].index].Text = OrderToolStripList[i].English; }
                else { toolStripList[OrderToolStripList[i].index].Text = OrderToolStripList[i].Chinese; }
            }
            //if (isEnglish) 
            //{
            //    MiddleLayer.MainF.lbModifyDate.Text = "Modify Date : " + Directory.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy/MM/dd");
            //    MiddleLayer.MainF.lbProgramVer.Text = "Software Version : " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
            //    JSDK.Alarm.Initial(string.Format("{0}\\{1}.xml", SysPara.AlarmTableDirectory, "English")); }
            //else 
            //{
            //    MiddleLayer.MainF.lbModifyDate.Text = "修改日期 : " + Directory.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy/MM/dd");
            //    MiddleLayer.MainF.lbProgramVer.Text = "软件版本 : " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
            //    JSDK.Alarm.Initial(string.Format("{0}\\{1}.xml", SysPara.AlarmTableDirectory, "Chinese")); 
            //}
            //JSDK.Alarm.Clear();
        }

        public static void GetControls1(Control fatherControl, bool isEnglish)
        {
            Control.ControlCollection sonControls = fatherControl.Controls;
            //遍历所有控件
            foreach (Control control in sonControls)
            {
                if (control is ToolStrip)
                {
                    ToolStrip toolStrip = (ToolStrip)control;
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        toolStripList.Add(item);
                    }
                }
                else
                {
                    toolList.Add(control);
                }

                if (control.Controls != null)
                {
                    GetControls1(control, isEnglish);
                }
            }
        }

        public void GetAllControl()
        {
            toolList.Clear();
            toolStripList.Clear();
            GetControls1(MiddleLayer.MainF, m_IsEnglish);
            GetControls1(MiddleLayer.MachineStatusF, m_IsEnglish);
            GetControls1(MiddleLayer.MachineSetupF, m_IsEnglish);
            GetControls1(MiddleLayer.RecipeEditorF, m_IsEnglish);
            GetControls1(MiddleLayer.ProductionSettingF, m_IsEnglish);
            GetControls1(MiddleLayer.MaintenanceF, m_IsEnglish);
            GetControls1(MiddleLayer.FlowChartF, m_IsEnglish);
            GetControls1(MiddleLayer.UserSettingF, m_IsEnglish);
           
            GetControls1(MiddleLayer.SystemF, m_IsEnglish);
            
            GetControls1(MiddleLayer.GantryF, m_IsEnglish);
            GetControls1(MiddleLayer.ConveyorF, m_IsEnglish);
            GetControls1(MiddleLayer.SignalTowerF, m_IsEnglish);
            GetControls1(MiddleLayer.MotorJogF, m_IsEnglish);
            

           

        }


        public void getAllText()
        {
            string xmlCode = "";
            //使用字典为了方便去重
            Dictionary<string, string> dicCode = new Dictionary<string, string>();
            foreach (ToolStripItem itemToolStripItem in toolStripList)
            {
                string strItem = itemToolStripItem.Text;
                if (strItem.Trim().Length > 0)
                {
                    if (!dicCode.ContainsKey(strItem))//去重判断
                    {
                        dicCode.Add(strItem, strItem);
                    }
                }
            }
            foreach (Control itemControl in toolList)
            {
                string strItem = itemControl.Text;
                #region 字符串筛选
                //忽略TextBox控件
                if (itemControl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                    continue;
                if (double.TryParse(strItem, out double dNum))//忽略数字
                    continue;
                if (strItem.IndexOf(" : ") > -1)//Software Version : 1.0.0.0 不去翻译了
                    continue;
                #endregion
                if (strItem.Trim().Length > 0)
                {
                    if (!dicCode.ContainsKey(strItem))//去重判断
                    {
                        dicCode.Add(strItem, strItem);
                    }
                }
            }
            foreach (KeyValuePair<string, string> item in dicCode)
            {
                xmlCode += item.Key + "\n";
            }

            TextWriter tw = new StreamWriter(new BufferedStream(new FileStream(
                    "D:\\ZHLanguage" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".cs", FileMode.Create, FileAccess.Write)),
                    System.Text.Encoding.GetEncoding("gb2312")); ;
            tw.Write(xmlCode);
            tw.Flush();
            tw.Close();
        }

        #endregion
    }

    public struct LanguageName
    {
        public int index;
        public string Chinese;
        public string English;

    }
}

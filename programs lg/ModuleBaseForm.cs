
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPSDK;

namespace AcuraLibrary.Forms
{
    public partial class ModuleBaseForm : Form
    {
       
        public ModuleBaseForm()
        {
            InitializeComponent();
            ModuleManager.ModuleList.Add(this);
        }

        #region Variables
        protected JTimer tmr_AlwaysRun = new JTimer();
        protected JTimer tmr_FCMain = new JTimer();
        protected JTimer tmr_Aux = new JTimer();

        protected JTimer RunTM = new JTimer();
        protected bool bInitialOk { get; set; }
        protected string _ModuleName { get; set; }
        #endregion
        #region Functions
        public virtual void ModuleInitialize(string ModuleName)
        {
            plMaintenance.AutoScroll = true;
            _ModuleName = ModuleName;
            for (int i = 0; i < RecipeData.Tables.Count; i++)
                RecipeData.Tables[i].Namespace = this.Text;
            ReadSettingData();
        }
        public virtual void ModuleDispose() { }
        //continue scan function
        public virtual void AlwaysRun() { }

        //initial function
        public virtual void InitialReset() { }
        public virtual void Initial() { bInitialOk = true; }
        public bool GetInitialOk() { return bInitialOk; }

        //running function
        public virtual void ServoOn() { }
        public virtual void ServoOff() { }
        public virtual void RunReset() { }
        public virtual void Run() { }
        public virtual void StartRun() { }
        public virtual void StopRun() { }
        public virtual void SetSpeed(int SpeedRate) { }
        public virtual void RemoveStop() { }
        public virtual void StopMotion() { }

        public virtual void TurnOnResetAlarms() { }
        public virtual void TurnOffResetAlarms() { }


        //page contron function
        public virtual void IntoRecipeEditor() { }
        public virtual void ExitRecipeEditor() { }
        public virtual void IntoProductionSettings() { }
        public virtual void ExitProductionSettings() { }


        //btn click function
        /// <summary>
        /// Function that execute something before getting into recipe form
        /// </summary>
        public virtual void BeforRecipeEditor() { }
        /// <summary>
        /// Function that execute something after getting exit recipe form
        /// </summary>
        public virtual void AfterRecipeEditor() { }
        /// <summary>
        /// Function that execute something after init the Acura
        /// </summary>
        public virtual void AfterInitSDK() { }
        /// <summary>
        /// Function that execute something before opening the NPMotor Jog Form
        /// </summary>
        public virtual void BeforeJogForm() { }
        /// <summary>
        /// Function that execute something after closing the NPMotor Jog Form
        /// </summary>
        public virtual void AfterJogForm() { }
        /// <summary>
        /// Function that execute something after reaading the model of recipe
        /// </summary>
        public virtual void AfterOpenRecipe(string oldRecipe, string newRecipe) { }



        public class DivisionPOS
        {
            public string DivisionID;
            public double PosX;
            public double PosY;
            public string DivisionName;
        }

        //function
        public void InitialParameterReset()
        {
            bInitialOk = false;
        }

        public void ReadSettingData()
        {
            SettingData.Clear();
            string sSettingDataPath = String.Format("{0}\\{1}.xml", ModuleManager.SettingDataDirectory, _ModuleName);
            if (File.Exists(sSettingDataPath))
                SettingData.ReadXml(sSettingDataPath);
            for (int i = 0; i < SettingData.Tables.Count; i++)
                if (SettingData.Tables[i].Rows.Count == 0)
                {
                    DataRow NewRow = SettingData.Tables[i].NewRow();
                    SettingData.Tables[i].Rows.Add(NewRow);
                }
            SettingData.AcceptChanges();
        }

        public void WriteSettingData()
        {
            string sSettingDataPath = String.Format("{0}\\{1}.xml", ModuleManager.SettingDataDirectory, _ModuleName);
            FileInfo fileInfo = new FileInfo(sSettingDataPath);
            if (fileInfo.Directory.Exists == false)
                fileInfo.Directory.Create();
            SettingData.AcceptChanges();
            SettingData.WriteXml(sSettingDataPath);
        }

        public void WriteSettingData1(string attribute, string value)
        {
            string sSettingDataPath = String.Format("{0}\\{1}.xml", ModuleManager.SettingDataDirectory, _ModuleName);
            UpdateEx(sSettingDataPath, "PSet", attribute, value);
        }


        /// <summary>
        /// 更新二级节点的值
        /// </summary>
        /// <param name="path">文件全称路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="attribute">属性名称</param>
        /// <param name="value">需要修改的值</param>
        /**************************************************
        * 使用示列: 
        * XmlHelper.Insert(path, "ProductSet", "bEnableCarA", "false")
        ************************************************/
        public static void UpdateEx(string path, string nodeName, string attribute, string value)
        {
            try
            {
                if (File.Exists(path))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(path);
                    XmlNode xn = xmlDocument.DocumentElement;
                    foreach (XmlNode node in xn.ChildNodes)
                    {
                        if (node.Name == nodeName)
                        {
                            node[attribute].InnerText = value;
                        }
                    }
                    xmlDocument.Save(path);
                }
                else
                {
                    MessageBox.Show("[" + path + "]文件不存在！");
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }



        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }







        public void ReadRecipeData(string RecipePath)
        {
            if (File.Exists(RecipePath))
            {
                DataSet ds = new DataSet();
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                {
                    if (ModuleManager.ModuleList[i]._ModuleName == _ModuleName)
                        ModuleManager.ModuleList[i].RecipeData.Clear();
                    ds.Merge(ModuleManager.ModuleList[i].RecipeData);
                }
                ds.ReadXml(RecipePath);
                for (int i = 0; i < ds.Tables.Count; i++)
                    if (ds.Tables[i].Namespace == _ModuleName)
                    {
                        try
                        {
                            DataTable dt = ds.Tables[i].Copy();
                            RecipeData.Tables[ds.Tables[i].TableName].Clear();
                            RecipeData.Merge(dt);
                        }
                        catch (Exception) { }
                    }
            }
            for (int i = 0; i < RecipeData.Tables.Count; i++)
                if (RecipeData.Tables[i].Rows.Count == 0)
                {
                    DataRow NewRow = RecipeData.Tables[i].NewRow();                    
                    RecipeData.Tables[i].Rows.Add(NewRow);
                }
            RecipeData.AcceptChanges();
        }

        public void WriteRecipeData(string RecipePath)
        {
            FileInfo fiTmp1 = new FileInfo(RecipePath);
            if (fiTmp1.Directory.Exists == false)
                fiTmp1.Directory.Create();
            RecipeData.AcceptChanges();

            DataSet ds = new DataSet();
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ds.Merge(ModuleManager.ModuleList[i].RecipeData);
            ds.WriteXml(RecipePath);
        }

        private dynamic GetDefaultValue(Type tp)
        {
            dynamic dmc = null;

            switch (Type.GetTypeCode(tp))
            {
                case TypeCode.Boolean:
                    dmc = false;
                    break;
                case TypeCode.String:
                    dmc = string.Empty;
                    break;
                case TypeCode.DateTime:
                    dmc = DateTime.MinValue;
                    break;
                case TypeCode.Char:
                    dmc = char.MinValue;
                    break;
                case TypeCode.Byte:
                    dmc = byte.MinValue;
                    break;
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    dmc = uint.MinValue;
                    break;
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    dmc = 0;
                    break;
            }

            return dmc;
        }
        #region New DataSet Process 
        public async Task<bool> WriteDataSet(DataSet dt,string basePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    string absPath = Path.Combine(basePath, dt.DataSetName, ".xml");
                    FileInfo _file = new FileInfo(absPath);
                    if (!_file.Directory.Exists)
                        _file.Directory.Create();
                    dt.AcceptChanges();
                    dt.WriteXml(absPath);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }).ConfigureAwait(false);
        }
        public async Task<bool> ReadDataSet(DataSet _dt, string basePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    string absPath = Path.Combine(basePath, _dt.DataSetName, ".xml");
                    if (!File.Exists(basePath))
                        return false;
                    DataSet ds = new DataSet();
                    ds.ReadXml(basePath);
                    _dt.Clear();
                    _dt.Merge(ds);
                    _dt.AcceptChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }).ConfigureAwait(false);
        }
        #endregion
        public dynamic GetSettingValue(string TableName, string ColumnName, int RowIndex = 0)
        {
            dynamic Value = null;
            if (SettingData.Tables[TableName].Columns.IndexOf(ColumnName) >= 0)
            {
                Value = SettingData.Tables[TableName].Rows[RowIndex][ColumnName, DataRowVersion.Original];
                if (Value.ToString() == "")
                {
                    SDKKernal.ShowAlarm("30010", "Setting data was not set value, ColumnName=\"" + ColumnName + "\"");
                    Value = GetDefaultValue(SettingData.Tables[TableName].Columns[ColumnName].DataType);
                }
            }
            else
                SDKKernal.ShowAlarm("30009", "Setting data was not found the column, ColumnName=\"" + ColumnName + "\"");
            return Value;
        }
        /// <summary>
        /// OVerload to recive automatic a data table and datacolumn
        /// </summary>
        public dynamic GetSettingValue(System.Data.DataTable dt,System.Data.DataColumn cl, int index=0)
        {
            return  GetSettingValue(dt.TableName,cl.ColumnName,index);
		}
        /// <summary>
        /// Overflow to read any data from a data column
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public dynamic GetData(DataColumn cl,int index=0)
        {
            System.Data.DataTable dt_Table=cl.Table;
            dynamic Value = null;
          
            Value = dt_Table.Rows[index][cl.ColumnName, DataRowVersion.Original];

            if (Value.ToString() == "")
            {
                SDKKernal.ShowAlarm("30011", $"Data from table: {dt_Table.TableName} was not set value, ColumnName: { cl.ColumnName } returning default value ");
                Value = GetDefaultValue(cl.DataType);
            }
            return Value;
		}

        /// <summary>
        /// Overflow to read any data from a data column
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public IEnumerable<DataRow> GetDataRows(DataColumn cl, string filter = "")
        {
            DataTable dt_Table = cl.Table;

            foreach (DataRow row in dt_Table.Rows)
            {
                if (filter == "" || row[cl].ToString() == filter)
                    yield return row;
            }
        }

        /// <summary>
        /// Overflow to read any data from a data column
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataRow GetDataRow(DataColumn cl, string filter)
        {
            DataTable dt_Table = cl.Table;

            foreach (DataRow row in dt_Table.Rows)
            {
                string Test = row[cl].ToString();
                if (row[cl].ToString() == filter)
                    return row;
            }

            return null;
        }

        /// <summary>
        /// OVerload to recive automatic a data table and datacolumn
        /// </summary>
        public dynamic GetRecipeValue(System.Data.DataTable dt,System.Data.DataColumn cl, int index=0)
        {
            return  GetRecipeValue(dt.TableName,cl.ColumnName,index);
		}
        public dynamic GetRecipeValue(string TableName, string ColumnName, int RowIndex = 0)
        {
            dynamic Value = null;
            if (RecipeData.Tables[TableName].Columns.IndexOf(ColumnName) >= 0)
            {
                Value = RecipeData.Tables[TableName].Rows[RowIndex][ColumnName, DataRowVersion.Original];
                if (Value.ToString() == "")
                {
                    SDKKernal.ShowAlarm("30008", "Recipe data was not set value, ColumnName=\"" + ColumnName + "\"");
                    Value = GetDefaultValue(RecipeData.Tables[TableName].Columns[ColumnName].DataType);
                }
            }
            else
                SDKKernal.ShowAlarm("30007", "Recipe data was not found the column, ColumnName=\"" + ColumnName + "\"");
            return Value;
        }
        public IEnumerable<DataRow> GetSettingDataRow(string tableName, string columnName, string value)
        {
            return SettingData.Tables[tableName].AsEnumerable().Where(row => row[columnName].ToString() == value);
        }
        #endregion Functions

        private void plMaintenance_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

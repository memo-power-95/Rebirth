using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Classes
{
	public  static  class DataSaver
	{
		public static bool fnSaveData(dynamic data, string sFullPath)
		{
			try
			{
				if (!Directory.Exists(Path.GetDirectoryName(sFullPath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(sFullPath));
				}
				FileStream fs = new FileStream (sFullPath, FileMode.OpenOrCreate);
				BinaryFormatter binFor=new BinaryFormatter();
				binFor.Serialize(fs,data);
				fs.Close();
				return true;
			}
			catch (Exception ex)
			{
				MiddleLayer.ExReportF.fnAddException(ex);
			}
			return false;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sFullPath"></param>
		/// <param name="bPass"></param>
		/// <param name="bDeleteAfterRead"></param>
		/// <returns>Do not forget to cast to your object type, example myData=(DataClass)thisreturn</returns>
		public static dynamic fnReadData(string sFullPath, ref bool bPass, bool bDeleteAfterRead=false)
		{
			dynamic data=null;
			try
			{				
				if (!Directory.Exists(Path.GetDirectoryName(sFullPath))|| !File.Exists(sFullPath))
				{	
					return false;
				}
				FileStream fs = new FileStream (sFullPath, FileMode.OpenOrCreate);
				BinaryFormatter binFor=new BinaryFormatter();
				data = binFor.Deserialize(fs);
				fs.Close();
				if (bDeleteAfterRead)
				{
					fnDeleteFile(sFullPath);
				}
				return data;
			}
			catch (Exception ex)
			{
				MiddleLayer.ExReportF.fnAddException(ex);
			}
			return data;
		}
		public static bool fnDeleteFile(string sFullPath)
		{
			try
			{
				if (!Directory.Exists(Path.GetDirectoryName(sFullPath))|| !File.Exists(sFullPath))
				{	
					return false;
				}
				File.Delete(sFullPath);
				return true;
			}
			catch (Exception ex)
			{
				MiddleLayer.ExReportF.fnAddException(ex);
			}
			return false;
		}
	}
}

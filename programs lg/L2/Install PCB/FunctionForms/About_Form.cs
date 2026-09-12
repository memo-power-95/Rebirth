using System;
using System.Drawing;
using System.Windows.Forms;
using Alpha.Classes;
using PanelAbout;

namespace Alpha.FunctionForms
{
	public partial class About_Form : Form
	{
		#region Version and Type Control Name
		/// <summary>
		/// Example of Version Control 
		///	Link at \\mxgdlm4nsifsn02\!General\BUILDING 4\RACDocuments\1.MANAGEMENT\2.TEAMS\4.SOFTWARE DESIGN\CONCEPTS\Modularizacion\Standarization\ControlVersionAndTemplates.pdf
		///	Documentation Defined By Regional Automation Center of Mexico
		///	4 Cuadrants :
		///		1--KERNEL 
		///			mKernel_Version
		///			mKC_Version
		///			mCO_Version
		///			Note:Could be typed as any of this options.
		///		2--Auxiliar
		///			mAuxiliar_Version
		///			mAX_Version
		///			mMS_Version
		///			Note:Could be typed as any of this options.
		///		3--Componentes Primarios
		///			mComponentPrimary_Version
		///			mCPrimary_Version
		///			mCP_Version
		///			Note:Could be typed as any of this options.
		///		4--Componentes Primarios
		///			mComponentSecundary_Version
		///			mCSecundary_Version
		///			mCS_Version
		///			Note:Could be typed as any of this options.
		///	Must to be added on Accesible Name of your designer view of FORM 
		///		Example
		///		ProcessForm 
		///			Accesible Name: mCO_1.0.0
		///			Accesible Description: Add anything that does the module
		///	Version Control Suggested is as Follow
		///		V1.0.0.0
		///		VA.B.C.D
		///			A: Increment on Change of Dependencie of DLLS
		///			B: Increment on Change of Depencencies of Acura Core (Mainform,etc)
		///			C: Increment on Change when high impact was made (bug fixed, critical function)
		///			D: Increment on Change when low impact was made (visualization, fuction)
		/// </summary> 
		#endregion
		public enum eTypeModule
		{
			//When you see this dont get afraid 
			//Is an overload of differet types of naming for the same type module
			//It Works
			CORE=0,
			MCO=0,
			MKERNEL=0,
			MKC=0,
			MISC=1,
			MMS=1,
			MAUXILIAR=1,
			MAX=1,
			CORE_COMPONETS=2,
			MCC=2,
			MCOMPONENTPRIMARY=2,
			MCPRIMARY=2,
			MCP=2,
			MODULE_COMPONENT=3,
			MMC=3,
			MCOMPONENTSECUNDARY=3,
			MCSECUNDARY=3,
			MCS=3,
		}
		public About_Form()
		{
			InitializeComponent();
			PanelAbout.pn_ModuleAbout pnAbout=new pn_ModuleAbout();
			pnAbout.fnSetDataToControl(this.ProductName,this.ProductVersion,"ACURA form",null);
			flpn_Core.Controls.Add(pnAbout);
		}
		public void fnAppendModule(Form fModule)
		{

			string sAccesibleName= (fModule!=null&&fModule.AccessibleName!=null)?fModule.AccessibleName:"";
			string sAccesibleDesc= (fModule != null && fModule.AccessibleDescription!=null)?fModule.AccessibleDescription:"";
			string sModuleName= (fModule != null && fModule.Name!=null)?fModule.Name:"";
			Icon ico=null;
			bool bUseIcon=sAccesibleName!="";
			ico=bUseIcon?fModule.Icon:null;//10134
			
			
			string [] sArray=(sAccesibleName.ToUpper()).Split('_');
			string sType="";
			string sVersion="";
			foreach (string sIt in sArray)
			{
				string It=sIt.Trim();
				if (It.StartsWith("M"))
				{
					sType=It;
				}
				else
				{
					sVersion+=It;
				}
			}
			fnAppendModule(sType,sModuleName,sVersion,sAccesibleDesc,ico);
		}
		public void fnAppendModule(string sTypeModule,string sModuleName, string sModuleVersion,string sDescripcion, Image img=null)
		{
			Bitmap theBitmap = new Bitmap(img, new Size(img.Width, img.Width));
			IntPtr Hicon = theBitmap.GetHicon();// Get an Hicon for myBitmap.
			Icon newIcon = Icon.FromHandle(Hicon);// Create a new icon from the handle.
			fnAppendModule(sTypeModule,sModuleName,sModuleVersion,sDescripcion,newIcon);
		}
		public void fnAppendModule(string sTypeModule,string sModuleName, string sModuleVersion, string sDescripcion,Icon ico=null)
		{
			eTypeModule moduleType=eTypeModule.MISC;
			bool bFindend=Enum.TryParse(sTypeModule,out moduleType);
			moduleType=!bFindend?eTypeModule.MISC:moduleType;
			FlowLayoutPanel flpn;
			switch (moduleType)
			{
				case eTypeModule.CORE:
					flpn=flpn_Core;
					break;
				
				case eTypeModule.CORE_COMPONETS:
					flpn=flpn_CoreComponents;
					break;
				case eTypeModule.MODULE_COMPONENT:
					flpn=flpn_ModuleComponents;
					break;
				case eTypeModule.MISC:
				default:
					//Default is Same As misc
					flpn=flpn_Misc;
					break;
			}
			PanelAbout.pn_ModuleAbout pnAbout=new pn_ModuleAbout();
			pnAbout.fnSetDataToControl(sModuleName,sModuleVersion,sDescripcion,ico!=null?ico.ToBitmap():null);
			flpn.Controls.Add(pnAbout);
		
		}	
		private void About_Form_Paint(object sender, PaintEventArgs e)
		{
			pn_ModuleAbout pnModule=new pn_ModuleAbout();
			Color cl=MiddleLayer.ModeColors.MouseOver;
			foreach (var item in flpn_Core.Controls)
			{
				if(item.GetType()==pnModule.GetType())
				{
					((pn_ModuleAbout)item).color=cl;
				}
			}
			foreach (var item in flpn_CoreComponents.Controls)
			{
				if(item.GetType()==pnModule.GetType())
				{
					((pn_ModuleAbout)item).color=cl;
				}
			}
			foreach (var item in flpn_Misc.Controls)
			{
				if(item.GetType()==pnModule.GetType())
				{
					((pn_ModuleAbout)item).color=cl;
				}
			}
			foreach (var item in flpn_ModuleComponents.Controls)
			{
				if(item.GetType()==pnModule.GetType())
				{
					((pn_ModuleAbout)item).color=cl;
				}
			}
			lbl_Core.SetBackColor(MiddleLayer.ModeColors.Background);
			lbl_CoreComponents.SetBackColor(MiddleLayer.ModeColors.Background);
			lbl_ModuleComponents.SetBackColor(MiddleLayer.ModeColors.Background);
			lbl_Misc.SetBackColor(MiddleLayer.ModeColors.Background);
		}
	}
}

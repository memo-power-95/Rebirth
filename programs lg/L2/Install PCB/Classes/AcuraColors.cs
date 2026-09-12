using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Alpha.Classes
{
	//public class AcuraColors
	//{
	//	public static Color Background = Color.FromArgb(46, 46, 46);
	//	public static Color Selected = Color.FromArgb(70, 70, 70);
	//	public static Color MouseOver = Color.FromArgb(70, 70, 70);
	//	public static Color Disable = Color.LightGray;
	//}
	public class ModeColors
    {
        Dictionary<string, ColorsBlock> d_AcuraModes=new Dictionary<string,ColorsBlock>();
        public  Color Background = Color.FromArgb(46, 46, 46);
        public  Color Selected = Color.FromArgb(70, 70, 70);
        public  Color MouseOver = Color.FromArgb(70, 70, 70);
        public  Color Disable = Color.LightGray;
        //ColorsBlock cl_Blocks;
		//d_AcuraModes.Add("Normal",cl_Blocks);
        public ModeColors()
        {
             fnAddModeColor("DARK",Color.FromArgb(45, 45, 45),Color.FromArgb(70, 70, 70),Color.FromArgb(70, 70, 70),Color.LightGray);
             fnAddModeColor("NORMAL",Color.FromArgb(0, 180, 230),Color.FromArgb(0, 130, 180),Color.FromArgb(0, 230, 255),Color.SkyBlue);
             fnSetCurrentMode("NORMAL");
		}
        public void fnAddModeColor(string sKey, Color forBackgroud,Color forSelected,Color forMouseOver,Color forDisable)
        {
            ColorsBlock clMode=new ColorsBlock();
            clMode.fnSetColors(forBackgroud, forSelected, forMouseOver, forDisable);
            fnAddModeColor(sKey,clMode);
		}
         public void fnAddModeColor(string sKey, ColorsBlock clBlock)
        {
            bool bContains=d_AcuraModes.ContainsKey(sKey);
			if (bContains)
			{
                d_AcuraModes.Remove(sKey);
			}
            d_AcuraModes.Add(sKey,clBlock);
              
		}
        public void fnSetCurrentMode(string sKey)
        {
            bool bContains=d_AcuraModes.ContainsKey(sKey);
			if (bContains)
			{   
                ColorsBlock cl=d_AcuraModes[sKey];
                Background=cl.Background;
                Selected=cl.Selected;
                MouseOver=cl.MouseOver;
                Disable=cl.Disable;
			}
		}
	}
    public struct ColorsBlock
    {
        public  Color Background;
        public  Color Selected;
        public  Color MouseOver;
        public  Color Disable;

        public void fnSetColors(Color forBackgroud,Color forSelected,Color forMouseOver,Color forDisable)
        {
            Background=forBackgroud!=null?forBackgroud:Background;
            Selected=forSelected!=null?forSelected:Background;
            MouseOver=forMouseOver!=null?forMouseOver:Background;
            Disable=forDisable!=null?forDisable:Background;
		}
    }
}

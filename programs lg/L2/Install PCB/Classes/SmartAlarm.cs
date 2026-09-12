using System;
using System.Collections.Generic;
using NPSDK;
using System.Windows.Forms;

namespace Alpha.Classes
{
    /// <summary>
    /// BUILT FOR ACURA 2.0
    /// </summary>
    public class SmartAlarm
    {
        //ALARM
        /// <summary>
        /// Clase para smart alarms de alarmas genericas por NPFlowChart
        /// </summary>
        public static cSmartAlarm cAlarm = new cSmartAlarm();
        
        /// <summary>
        /// Enumeracion de los distintos tipos de alarmas que pudieran existir
        /// </summary>
        public static mNameAlm mNameA;
    }

    public class cSmartAlarm
    {
        private List<string> FormList = new List<string>();
        private List<int> AlarmIndexBaseList = new List<int>();
        
        /// <summary>
        /// The alarmbase es el id de alarma donde va a comenzar
        /// </summary>
        private const int ALARMBASE = 10000;
        /// <summary>
        /// The ntypes es la cantidad de numeros de alarma que hay
        /// </summary>
        private const int NTYPES = 6;
        /// <summary>
        /// Enum especifico para los tipos de alarmas
        /// </summary>
        public enum AType
        {
            I = 0,
            W = 1,
            K = 2,
            E = 3,
            K_Stop = 4,
            E_Stop = 5
        }
        /// <summary>
        /// Funcion que aparte un rango de alarmas genericas para el NPFlowChart indicado
        /// </summary>
        /// <param name="fc">The fc.</param>
        public void AddRange(NPFlowChart fc,Control cntl=null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            sParent = sParent == "" ? (fc.Parent != null ? fc.Parent.Name + "_" : "") : sParent;
            FormList.Add($"{sParent}{fc.Name}");
            AlarmIndexBaseList.Add(ALARMBASE + (AlarmIndexBaseList.Count * NTYPES));
        }
         /// <summary>
        /// Funcion que aparte un rango de alarmas genericas para idName indicado
        /// </summary>
        /// <param name="fc">The fc.</param>
        public void AddRange(string  sModuleAlarmsID, Control cntl=null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            FormList.Add($"{sParent}{sModuleAlarmsID}");
            AlarmIndexBaseList.Add(ALARMBASE + (AlarmIndexBaseList.Count * NTYPES));
        }
        private string  GetID(string sNameId,AType aTp)
        {
            int id=FormList.IndexOf(sNameId);
			if (id!=-1)
			{
			    return (Convert.ToString(AlarmIndexBaseList[id] + aTp));
                
			}
            return "E999"+aTp.ToString();
		}
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo I (information)  para el modulo del flowchar especifico</para>
        /// <para>esta NO PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El flowchar principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string I(NPFlowChart fc,Control cntl=null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            sParent = sParent == "" ? (fc.Parent != null ? fc.Parent.Name + "_" : "") : sParent;
            return GetID($"{sParent}{fc.Name}",AType.I);
            //return (Convert.ToString(AlarmIndexBaseList[FormList.IndexOf($"{sParent}{fc.Name}")] + AType.I));
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo W (warning)  para el modulo del flowchar especifico</para>
        /// <para>esta NO PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string W(NPFlowChart fc,Control cntl=null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            sParent = sParent == "" ? (fc.Parent != null ? fc.Parent.Name + "_" : "") : sParent;
            return GetID($"{sParent}{fc.Name}",AType.W);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo K (quien sabe que significa si tienes alguna idea comparte la info)  para el modulo del flowchar especifico,</para>
        /// <para>esta NO PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string K(NPFlowChart fc,Control cntl=null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            sParent = sParent == "" ? (fc.Parent != null ? fc.Parent.Name + "_" : "") : sParent;
            return GetID($"{sParent}{fc.Name}",AType.K);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo E (Emergency)  para el modulo del flowchar especifico</para>
        /// <para>esta NO PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string E(NPFlowChart fc,Control cntl=null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            sParent=sParent==""?(fc.Parent != null?fc.Parent.Name + "_" : ""):sParent;
           return GetID($"{sParent}{fc.Name}",AType.E);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo K (no idea)  para el modulo del flowchar especifico</para>
        /// <para>esta SI PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string K_Stop(NPFlowChart fc,Control cntl=null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            sParent = sParent == "" ? (fc.Parent != null ? fc.Parent.Name + "_" : "") : sParent;
            return GetID($"{sParent}{fc.Name}",AType.K_Stop);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo E (Emergnecy)  para el modulo del flowchar especifico</para>
        /// <para>esta SI PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string E_Stop(NPFlowChart fc,Control cntl=null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            sParent=sParent==""?(fc.Parent!=null?fc.Parent.Name + "_" : ""):sParent;
           return GetID($"{sParent}{fc.Name}",AType.E_Stop);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo I (information)  para el modulo del flowchar especifico</para>
        /// <para>esta NO PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El flowchar principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string I(string  sModuleAlarmsID,Control cntl)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
             return GetID($"{sParent}{sModuleAlarmsID}",AType.I);
            //return (Convert.ToString(AlarmIndexBaseList[FormList.IndexOf($"{sParent}{sModuleAlarmsID}")] + AType.I));
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo W (warning)  para el modulo del flowchar especifico</para>
        /// <para>esta NO PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string W(string  sModuleAlarmsID,Control cntl = null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            return GetID($"{sParent}{sModuleAlarmsID}",AType.W);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo K (quien sabe que significa si tienes alguna idea comparte la info)  para el modulo del flowchar especifico,</para>
        /// <para>esta NO PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string K(string  sModuleAlarmsID,Control cntl = null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            return GetID($"{sParent}{sModuleAlarmsID}",AType.K);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo E (Emergency)  para el modulo del flowchar especifico</para>
        /// <para>esta NO PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string E(string  sModuleAlarmsID,Control cntl)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            return GetID($"{sParent}{sModuleAlarmsID}",AType.E);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo K (no idea)  para el modulo del flowchar especifico</para>
        /// <para>esta SI PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string K_Stop(string  sModuleAlarmsID,Control cntl = null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            return GetID($"{sParent}{sModuleAlarmsID}",AType.K_Stop);
        }
        /// <summary>
        /// <para>Funcion especifica para obtener el id correspondiente a la alarma tipo E (Emergnecy)  para el modulo del flowchar especifico</para>
        /// <para>esta SI PAUSA el proceso. </para>
        /// </summary>
        /// <param name="fc">El NPFlowChart principar para el rango de alarmas.</param>
        /// <returns></returns>
        public string E_Stop(string  sModuleAlarmsID,Control cntl = null)
        {
            string sParent=cntl!=null?cntl.Name+"_":"";
            return GetID($"{sParent}{sModuleAlarmsID}",AType.E_Stop);
        }
        /// <summary>
        /// Limpian las alarmas correspondientes a este modulo
        /// </summary>
        /// <param name="fc"></param>
        public void ClearModuleAlarm(NPFlowChart fc,Control cntl=null)
        {
            SDKPara.Arm.ReportClearAlarm(I(fc,cntl));
            SDKPara.Arm.ReportClearAlarm(W(fc,cntl));
            SDKPara.Arm.ReportClearAlarm(K(fc,cntl));
            SDKPara.Arm.ReportClearAlarm(E(fc,cntl));
        }
        /// Limpian las alarmas correspondientes a este modulo
        /// </summary>
        /// <param name="fc"></param>
        public void ClearModuleAlarm(string sModuleAlarmsID,Control cntl=null)
        {
            SDKPara.Arm.ReportClearAlarm(I(sModuleAlarmsID,cntl));
            SDKPara.Arm.ReportClearAlarm(W(sModuleAlarmsID,cntl));
            SDKPara.Arm.ReportClearAlarm(K(sModuleAlarmsID,cntl));
            SDKPara.Arm.ReportClearAlarm(E(sModuleAlarmsID,cntl));
        }

    }

    public struct mNameAlm
    {
        public const string INDEX = "Index: ";
        public const string ROBOT = "Robot: ";
        public const string VISION = "Vision: ";
        public const string SLIDER = "Slider: ";
        public const string SLIDERLFT = "Slider Izquierdo: ";
        public const string SLIDERRGT = "Slider Derecho: ";
        public const string MES = "Mes: ";
        public const string System = "System: ";
        public const string Scanner = "Scanner: ";
        public const string Modu1 = "Modu1: ";
        public const string BOWL = "Bowl: ";
        public const string NPMotor = "NPMotor: ";
        public const string MECHANISM = "Mecanismo: ";
        public const string WEBCAM = "Webcam: ";
        public const string INSPECT = "Inspect: ";
        public const string SEQUENCER = "Secuenciador: ";
        public const string LASERMARKER = "LaserMarker: ";
        public const string CONVEYORPROCESS = "Conveyor Process: ";
        public const string GANTRY = "Gantry: ";
        public const string CONVEYORBUFFER = "Conveyor Buffer: ";
        public const string CONVEYORESCANER = "Conveyor Escaner: ";
        public const string CONVEYOR = "Conveyor: ";
        public const string RETURNCONVEYOR1 = "Conveyor Retorno 1: ";
        public const string RETURNCONVEYOR2 = "Conveyor Retorno 2: ";
        public const string KeyenceXG = "Keyence XG: ";
        public const string FLIPPER = "Volteador: ";
    }
}

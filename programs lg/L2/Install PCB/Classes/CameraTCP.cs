using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
//using DALSA.SaperaLT.SapClassBasic;

namespace TestCamera
{
    static class CameraTCP
    {
        public static byte[] SendString = new byte[] { };
        static IPAddress[] iPs;
        static Socket socketConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte BUFFER_SIZE = 50;
        private static string HOST = "localhost";
        private static int PORT = 5021;
        public static int TOTAL_CRASH_SOCKET = 0;

        #region Camera connection methods
        /// <summary>
        /// Verifica si existe una conexión activa con una camara, retorna true o false.
        /// </summary>
        /// <returns></returns>
        public static bool IsConnected() {
            return socketConnection.Connected;
        }

        /// <summary>
        /// Se conecta con una camara por medio de TCP, se requiere conocer la IP y numero de puerto de la camara.
        /// </summary>
        /// <param name="HostAddress"></param>
        /// <param name="PortNumber"></param>
        /// <returns></returns>
        public static bool Connect(string HostAddress, int PortNumber) {
            iPs = Dns.GetHostAddresses(HostAddress);
            try {
                socketConnection.Connect(HostAddress, PortNumber);
            } catch (Exception e) {
                Console.WriteLine(e.GetType().FullName);
                Console.WriteLine(e.Message);
            }
            return IsConnected();
        }

        /// <summary>
        /// Se desconecta del server de la camara
        /// </summary>
        public static void DisConnect() {
            try {
                if (socketConnection.Connected) {
                    socketConnection.Disconnect(true);
                }
            }catch(Exception es) {
                Console.WriteLine(es.ToString());
            }
        }

        /// <summary>
        /// Se cierra la comunicacion con la camara
        /// </summary>
        public static void CloseCamera() {

            socketConnection.DuplicateAndClose(1);
            socketConnection.Dispose();
        }
        #endregion

        #region Camera commands methods
        /// <summary>
        /// Funcion generica para enviar un comando por protocolo TCP y regresa la respuesta de la camara.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string SendCommand(string command) {
            byte[] buffer = new byte[BUFFER_SIZE];string result = "None";
            socketConnection.ReceiveBufferSize = BUFFER_SIZE;
            Array.Clear(SendString, 0, SendString.Length);
            SendString = Encoding.ASCII.GetBytes($"{ command }\n");
            try {
                socketConnection.Send(SendString);
                socketConnection.Receive(buffer);
                //Catch Empty array before Socket crash
                bool IsEmptyResponse = buffer.All(single => single == 0);
                if (IsEmptyResponse) {
                    Console.WriteLine("\nSocket before crash");
                    TOTAL_CRASH_SOCKET++;
                    socketConnection.Shutdown(SocketShutdown.Both);
                    socketConnection.Close(100);
                    socketConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Connect(HOST, PORT);
                    SendCommand(command);
                }
            } catch (Exception e) {
                Console.WriteLine(e.GetType().FullName);
                Console.WriteLine(e.Message);
            }
            try {
                result = Encoding.UTF8.GetString(buffer);
            } catch(Exception es) {
                Console.WriteLine("SendCommand Exception: "+es.ToString());
            }
            return result;
        }

        /// <summary>
        /// Regresa un entero que representa el total de soluciones que la camara cuenta actualmente.
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfSolutions() {
            String NoOfSolution = "-1";
            try {
                 NoOfSolution = SendCommand("ns");
                 NoOfSolution = TrimAllWithInplaceCharArray(NoOfSolution);
                 Console.WriteLine("Response number of solution from Camera: " + NoOfSolution.Trim().Replace(" ",string.Empty)+" Leght: "+NoOfSolution.Length);
                 return Convert.ToInt32(NoOfSolution);
            } catch(Exception es) {
                Console.WriteLine("Get Number of Solution Exception: \n" + es.ToString());
                throw;
            }
        }

        public static string TrimAllWithInplaceCharArray(string str)
        {
            var len = str.Length;
            var src = str.ToCharArray();
            int dstIdx = 0;
            for (int i = 0; i < len; i++) {
                var ch = src[i];
                switch (ch) {

                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':

                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':

                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':

                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':

                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                    case '\u0000':
                        continue;

                    default:
                        src[dstIdx++] = ch;
                        break;
                }
            }
            return new string(src, 0, dstIdx);
        }

        /// <summary>
        /// Cambia a la solución con el ID en la cual se hara la inspeccion,  
        /// </summary>
        /// <param name="SolutionID"></param>
        public static void TriggerSolution(int SolutionID) {
            string TriggerSolution = Convert.ToString(SolutionID);
            TriggerSolution = $"ss { SolutionID }";
            SendCommand(TriggerSolution);
        }

        /// <summary>
        /// Manda un trigger a la camara para que toma una nueva foto.
        /// Se toman dos fotos.
        /// </summary>
        public static void TriggerCamera() {
            string TriggerCamera = "gen 0";
            SendCommand(TriggerCamera);
        }

        /// <summary>
        /// Reinicia los contadores de estadisticas del server de la camara.
        /// </summary>
        public static void Reset() {
            SendCommand("rs");
        }

        /// <summary>
        /// Detiene la inspeccion actual de la camara.
        /// </summary>
        public static void StopInspection() {
            SendCommand("stop");
        }

        /// <summary>
        /// Evalua el resultado general de la solución de inspección, retorna PASS o FAIL.
        /// Evalua la solucion dos veces.
        /// </summary>
        /// <returns></returns>
        public static string EvalResult() {
            string response = "";
            response = SendCommand("eval Result.0");
            return response.Contains("1.0000") ? "PASS" : "FAIL";
        }

        public static string EvalCamera(string barcodeName) {
            return SendCommand("eval " + barcodeName);
        }

        /// <summary>
        /// Evalua la variable SerialNumber en la solucion.
        /// </summary>
        /// <returns></returns>
        public static string EvalSerialNumber() {
            return SendCommand("eval SerialNumber");
        }

        /// <summary>
        /// Evalue la variable Family en la solucion.
        /// </summary>
        /// <returns></returns>
        public static string EvalFamily() {
            string family = SendCommand("eval Family");
            return family;
        }
        /// <summary>
        /// Evalue la variable Objeto en la solucion.
        /// </summary>
        /// <returns></returns>
        public static string EvalObject(string sObject)
        {
            if (!CameraTCP.IsConnected())
                CameraTCP.Connect("localhost", 5021);
            CameraTCP.Reset();

            string sResponse = SendCommand("eval " + sObject);
            return sResponse;
        }

        /// <summary>
        /// Corre una solucion: verifica la conexión, cambia a la solución correspondiente
        /// se toma una nueva foto y se realiza la inspección de la solución, retorna el 
        /// resultado general de la solución PASS o FAIL.
        /// </summary>
        /// <param name="solutionID"></param>
        /// <returns></returns>
        public static string RunSolution(int solutionID) {
            string result = "";
            try {
                //TriggerSolution(solutionID);
                SendCommand("start");
                TriggerCamera();
                result = EvalResult();
                StopInspection();
            }catch(Exception es) {
                Console.WriteLine("Run Solution exception: " + es.ToString());
            }
            return result;
        }

        public static string RunSolution(string objName) {
            string result = "";
            try {
                TriggerSolution(0);
                SendCommand("start");
                TriggerCamera();
                result = EvalCamera(objName);
                StopInspection();
            } catch (Exception es) {
                Console.WriteLine("Run Solution exception: " + es.ToString());
            }
            string x = "";
            return x = result.Replace(objName,string.Empty).Replace("\"", "").Substring(12).Trim();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EasyModbus;
using System.Collections;


namespace Acura3.Clases
{

    /// <summary>
    /// JAG_MBClient es un cliente Modbus para la comunicacion con robots epson desde el acura. 
    /// </summary>
    public class JAG_MBClient
    {
        private ModbusClient mb_EpsonClient = null;
        public string sIP = "";
        public int nPort = 0;
        bool bMBBussy;

        #region mbDefinitionEpson

        /// <summary>
        ///direcciones, Establece la direccion de Modbus a la que va escribir, Ojo  en robots epson la posicion que escribimos es una direccion posterior a la establecida. La 31 de acura es la 32 del robot.
        /// </summary>
        public static class direcciones
        {
            public const short ReadTask = 0;
            public const short Task = 35; //Epson 32 
            public const short OperationMode = 32;   //33          
            public const short ProgramNumber = 33;
            public const short Punto = 35;
            public const short dPallet = 36;

            public const short ProcesAux = 38;
            public const short MultiWordStart = 39; //1-3 valid - 4 Full PCB Scrap
            public const short TrayLevel = 40; //Alto 101-105 Bajo 106 Vacio	

        }
        /// <summary>
        /// Commandos es un diccionario definido de comandos para se ejecutados en el robot. Se Define este diccionario para los commandos en la capa de SEQUENCE. 
        /// </summary>
        public static class Commandos
        {
            //Define Integer For ModBus Comands
            public const short cInit = 99; // Mandar a inicializar robot
                                           //public const short cPick = 10; // Tomar tarjeta Comm
                                           //public const short cFrontToServo = 40; // Mandar al frente del cervo
                                           //public const short cCommHoldOnNest = 41; // Cuando cierro gripper
                                           //public const short cCheckPCB = 50; // Lectura Paso
                                           //public const short cToVacumm = 60; // Mandar Comm al vacio
                                           //public const short cRechazoPCB = 70; // lectura de scanner falló
                                           //public const short cRejectTray = 47; // Rechazar charola
                                           //public const short cRejectReady = 66; // Mensaje para decirle al robot que el sistema de rechazo de totes ya está en posición
                                           //public const short cCommReceived = 62; // El vacio ya tiene la Comm
                                           //public const short cCommWithVacumm = 64; // La Comm ya está en el vacío
                                           ////Auxiliares
                                           //public const short aGripperCerrado = 13;
                                           //public const short aGripperAbierto = 15;


            public const short Program1 = 1;
            public const short Program2 = 2;
            public const short Program3 = 3;
            public const short Program4 = 4;
            public const short Program5 = 5;

            public const short Task1 = 10;
            public const short Task2 = 20;
            public const short Task3 = 30;
            public const short Task4 = 40;
            public const short Task5 = 50;
            public const short Task6 = 60;


            public const short NormalMode = 30;
            public const short DryCycleMode = 330;

            public const short cClear = 0; // Limpiar último valor
        }

        /// <summary>
        /// Respuestas es un diccionario definido para las respuestas a comandos del robot. Se define este diccionario para respuesta en la capa de SEQUENCE. 
        /// </summary>
        public static class Respuestas   //31  Sequence,32  Feeder,33  Error ,34  StatusApp ,35  ResetMatrix      
        {
            public const short rInitOK = 99; // Inicialización del robot acabó




            public const short rRequestVacumm = 61; // Robot ya va a ir a poner la comm al vacio
            public const short rRobotgripperOpened = 63; // Gripper de robot abierto cuando deja Comm en placa de vacio
            public const short rRobotOutPressArea = 65; // Robot fuera del area de prensa
                                                        //public const short rPCBDelivered = 20; // 

            public const short rCommOnNest = 40;  // Com está en alineador
            public const short rRobotToServo = 41; // Robot está enfrente del servo esperando
            public const short rGripperOpened = 49; // Gripper del robot abierto en alineador

            public const short rPCBRechazada = 70; // PCB Rechazada
            public const short rRechazoCharola = 70; // Iniciando secuencia de rechazo de charola
            public const short rCharolaRechazada = 71; // Charola rechazada

            public const short rCommListaParaRetirar = 51; // Robot cerró gripper y está listo para retirar comm del alineador
            public const short rCharolaEnRutinaDRechazo = 47; // Charola en rutina de rechazo
            public const short rCharolaEnPosicionRechazo = 48; // Charola en posicion de rechazo
                                                               //Auxiliares
            public const short aRobotWithCom = 100; //ROBOT CON TARjETA acura abre gripper de alineacion
            public const short aGripperRobotCerrado = 14;

        }

        /// <summary>
        ///SalidasDigitales, Establece la direccion de Modbus a nivel COIL a la que va escribir, 
        ///Ojo  en robots epson la posicion que escribimos es una direccion posterior a la establecida. 
        ///La 511 de acura es la 512 del robot.
        /// </summary>
        public static class SalidasDigitales
        {
            public const short StartCycle = 14;

            public const short Start = 511; //512
            public const short Stop = 518;
            public const short Pause = 519;
            public const short Continue = 520;
            public const short Reset = 521;
            public const short Shutdown = 522;
            public const short PowerLow = 512;
            public const short PowerHigh = 525;
            public const short Recover = 524;
            public const short ResetAlarm = 523;
            public const short ModoMtto = 600;

            public const short ExtResSet = 637;
            public const short ExtCmdSet = 638;

        }

        ///// <summary>
        /////EntradasDigitales, Establece la direccion de Modbus a nivel COIL a la que va Leer, 
        /////Ojo  en robots epson la posicion que escribimos es una direccion posterior a la establecida. 
        /////La 511 de acura es la 512 del robot.
        ///// </summary>
        public static class EntradasDigitales
        {
            public const short StartCycle = 14;

            //    OJO: para esta aplicacion los registros estan desfazados 4 palabras
            //    al leer en la 511 como primer bit de acura, se esta leyendo en el
            //    bit 576 salida del Robot


            //public const short Ready = 511; //512

            public const short Running = 512;
            public const short Paused = 513;
            public const short Error = 514;
            public const short AtHome = 515;
            public const short EStopOn = 516;
            public const short SError = 517;
            public const short Warning = 518;
            public const short AutoMode = 575; // Se cambio una salida digital para agregar el trigger al robot 

            public const short ExtRestSet = 600;
        }

        private const short nQtyRegisters = 10;
        /// <summary>
        /// _mbWriteData Es el arreglo actual de escritura en modbus al cliente. 
        /// </summary>
        private int[] _mbWriteData = { };
        /// <summary>
        /// _mbWriteDataDiff Es el arreglo de respaldo de escritura en modbus al cliente, 
        /// con el que compara si es un comando nuevo antes de escribir.  
        /// </summary>
        private int[] _mbWriteDataDiff = { };
        /// <summary>
        /// _mbReadData Es el arreglo de lectura en tiempo real al registro del cliente.  
        /// </summary>
        public int[] _mbReadData = { };

        /// <summary>
        /// Es para especificar cuando esta conectado
        /// </summary>
        bool _bModBusRunning = false;
        /// <summary>
        /// Termina el hilo de estritura y lectura cuando es True
        /// </summary>
        bool _bMBKillThread = false;
        #endregion

        #region PublicFunctions
        public JAG_MBClient()
        {
            Builder();
        }
        public JAG_MBClient(string IP, int PORT)
        {
            sIP = IP;
            nPort = PORT;
            Builder();
        }
        private void Builder()
        {
            if (!_bModBusRunning && sIP != "" && nPort != 0)
            {
                if (mb_EpsonClient != null && mb_EpsonClient.Connected)
                {
                    return;
                }
                _bMBKillThread = true;
                _mbWriteData = new int[nQtyRegisters];
                _mbWriteDataDiff = new int[nQtyRegisters];
                _mbReadData = new int[nQtyRegisters];
                mb_EpsonClient = new ModbusClient(sIP, nPort);
                try
                {
                    mb_EpsonClient.Connect();
                }
                catch (Exception)
                {

                }
                for (ushort mbP = 0; mbP < nQtyRegisters; mbP++)
                {
                    _mbWriteDataDiff[mbP] = 1;
                    sWrite(31 + mbP, 0);
                }
                mb_EpsonClient.ConnectionTimeout = 3000;
                bMBBussy = false;
                tModbus();
            }
        }
        /// <summary>
        /// Devuelve un true si esta conectado. 
        /// </summary>
        /// <returns></returns>
        public bool isConected()
        {
            return mb_EpsonClient.Connected;
        }
        /// <summary>
        /// Desconecta el cliente modbus
        /// </summary>
        public void Dispose()
        {
            if (isConected())
            {
                _bMBKillThread = true;
                for (ushort mbP = 0; mbP < nQtyRegisters; mbP++)
                {
                    sWrite((int)direcciones.OperationMode + mbP, 0);
                }
                if (mb_EpsonClient != null)
                    mb_EpsonClient.Disconnect();
            }

        }
        /// <summary>
        /// Fucion de Lectura para Modbus, devuelve el valor del array de la posicion indicada. 
        /// Usar el diccionario de Pos
        /// </summary>
        /// <param name="nDir">Valor de la posicion Usar Dictionario DPos</param>
        /// <returns></returns>
        public int Read(int nDir)
        {
            nDir = nDir - 36;
            if (nDir <= nQtyRegisters)
            {
                return _mbReadData[nDir];
            }
            return 1000;//Devuelve 1000 de valor falso
        }
        /// <summary>
        /// Fucion de Escritura para Modbus, Escribe en el array local el comando. 
        /// </summary>
        /// <param name="nArrayPos">Valor de la posicion Usar Dictionario DPos</param>
        /// <param name="nCMDWrite">Valor de escritura o comando para escribir en cliente</param>
        private void Write(int nArrayPos, int nCMDWrite)
        {
            if (nArrayPos > nQtyRegisters)
            {
                nArrayPos = nArrayPos - 31;
            }
            _mbWriteData[nArrayPos] = nCMDWrite;

        }

        public void sWrite(int nArrayPos, int nCMDWrite)
        {
            if (nArrayPos >= (int)direcciones.OperationMode)
            {
                bMBBussy = true;
                try
                {
                    mb_EpsonClient.WriteSingleRegister(nArrayPos, nCMDWrite);
                }
                catch (Exception)
                {

                }
                bMBBussy = false;          
            }
        }

        /// <summary>
        /// Escribe multiples registros, de momento usado solo para pruebas
        /// Para enviar comandos con sus parametros al robot
        /// 02 Mar 20
        /// </summary>
        /// <param name="nArrayPos"></param>
        /// <param name="nCMDWrite"></param>
        public void multiWrite(int nArrayPos, int[] nCMDWrite)
        {
            if (nArrayPos >= 31)
            {
                bMBBussy = true;
                try
                {
                    mb_EpsonClient.WriteMultipleRegisters(nArrayPos, nCMDWrite);
                }
                catch (Exception)
                {

                }
                bMBBussy = false;
                //     _mbWriteData[nArrayPos - (int)direcciones.Sequence] = nCMDWrite;
            }
        }

        public void coilWrite(int nArrayPos, bool bSET)
        {
            bMBBussy = true;
            try
            {
                mb_EpsonClient.WriteSingleCoil(nArrayPos, bSET);
            }
            catch (Exception)
            {

            }
            bMBBussy = false;
        }
        public bool coilRead(int nArrayPos)
        {
            bool b = false;          
            if (nArrayPos >= 511)
            {
                try
                {
                    bMBBussy = true;
                    bMBBussy = false;
                    var bools = new BitArray(new int[] { _mbReadData[0] }).Cast<bool>().ToArray();
                    b = bools[nArrayPos - 511];//511 es la primera posicion de una bool en modbus
                }
                catch (Exception)
                {

                }
            }
            return b;
        }
        #endregion

        #region PrivateFunctions
        /// <summary>
        /// Hilo de escritura y lectura para modbus
        /// </summary>
        private async void tModbus()
        {
            await Task.Run(() =>
            {
                _bModBusRunning = true;
                _bMBKillThread = false;
                while (!_bMBKillThread)
                {
                    if (mb_EpsonClient.Connected && !_bMBKillThread && !bMBBussy)
                    {
                        //Lectura
                        #region Reader
                        try
                        {
                            _mbReadData = mb_EpsonClient.ReadInputRegisters(31, nQtyRegisters); //Leer apartir de registro 31 las siguientes 5 palabras
                        }
                        catch (Exception)
                        {
                        }
                        //Limpieza de escritura para sequence si la respuesta del sequnece es igual de el cmd+1 borra el comando escrito. 

                        #endregion
                        //Escritura
                        #region Writter
                        //for (int i = 0; i < nQtyRegisters&& !_bMBKillThread; i++)
                        //{
                        //    if (_mbWriteData[i] != _mbWriteDataDiff[i])
                        //    {
                        //        try
                        //        {
                        //            mb_EpsonClient.WriteSingleRegister(i + 31, _mbWriteData[i]);
                        //        }
                        //        catch (Exception)
                        //        {
                        //        }
                        //        _mbWriteDataDiff[i] = _mbWriteData[i];
                        //    }
                        //}
                        //if (_mbWriteData[DPos.Sequence] == _mbReadData[DPos.Sequence] + 1)
                        //{
                        //    _mbWriteData[DPos.Sequence] = 0;
                        //}
                        //if (15 == _mbReadData[DPos.Sequence] || 13 == _mbReadData[DPos.Sequence])
                        //{
                        //    _mbWriteData[DPos.Sequence] = 0;
                        //}
                        #endregion
                    }
                }
                _bModBusRunning = false;
            });
        }
        #endregion

    }

}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NPSDK;

namespace Alpha
{

    public class FlowControl
    {
        public bool bStopWork = false;
        Thread FlowControlThread = null;
        Thread IOSControlThread = null;
        Thread AlwaysRUnThreads = null;

        public void StartThread()
        {
            bStopWork = false;
            FlowControlThread = new Thread(DoWork);
            FlowControlThread.Start();
            IOSControlThread=new Thread(RefreshIOS);
            IOSControlThread.Start();
            AlwaysRUnThreads=new Thread(AlwaysRUN);
            AlwaysRUnThreads.Start();
        }

        public void StopThread()
        {
            bool Stoped=true;
            
            if (IOSControlThread!=null)
			{
                 IOSControlThread.Join(150);

				 if (IOSControlThread.IsAlive)
				    IOSControlThread.Suspend();

                 Stoped &= IOSControlThread.ThreadState != ThreadState.Running;
			}

            if (AlwaysRUnThreads!=null)
			{
                 AlwaysRUnThreads.Join(150);

				if (AlwaysRUnThreads.IsAlive)
					AlwaysRUnThreads.Suspend(); 

                 Stoped &= AlwaysRUnThreads.ThreadState != ThreadState.Running;
			}
            
            if (FlowControlThread != null)
            {
                FlowControlThread.Join(150);
				
                if (FlowControlThread.IsAlive)
					FlowControlThread.Suspend(); 
                 
                Stoped &= AlwaysRUnThreads.ThreadState != ThreadState.Running;
            }
			
            bStopWork = Stoped;
        }
        
        public void DoWork()
        {
            int ScanTick = 0;
            Int64 LastSecond = 0;
            Int64 TempSecond = 0;
            GetTickCountEx tick = new GetTickCountEx();

            while (!bStopWork)
            {
                Execute();
                Thread.Sleep(1);
                ScanTick++;
                TempSecond = tick.Value;

                if ((TempSecond - LastSecond) >= 1000)
                {
                    LastSecond = TempSecond;

                    if (SysPara.SystemMode == RunMode.AUTO)
                    {
                        SysPara.OperationSecond++;

                        if (SysPara.SystemRun)
                            SysPara.RunSecond++;
                        else
                            SysPara.StopSecond++;
                    }

                    SysPara.ScanTime_Work = ScanTick;
                    ScanTick = 0;
                }
            }
        }
        public  void RefreshIOS()
        {
			//if(SysPara.Simulation)
   //         {
   //             SysPara.ScanTime_RefreshIOS =0;    
   //             return;
			//}

            int ScanTick = 0;
            Int64 LastSecond = 0;
            Int64 TempSecond = 0;
            GetTickCountEx tick = new GetTickCountEx();

            while (!bStopWork)
            {
                DO_RefreshIOS();
                Thread.Sleep(1);
                ScanTick++;
                TempSecond = tick.Value;

                if ((TempSecond - LastSecond) >= 1000)
                {
                    LastSecond = TempSecond;

                    if (SysPara.SystemMode == RunMode.AUTO)
                    {
                        SysPara.OperationSecond++;

                        if (SysPara.SystemRun)
                            SysPara.RunSecond++;
                        else
                            SysPara.StopSecond++;
                    }

                    SysPara.ScanTime_RefreshIOS = ScanTick;
                    ScanTick = 0;
                }
            }
        }
        public void AlwaysRUN()
        {
            int ScanTick = 0;
            Int64 LastSecond = 0;
            Int64 TempSecond = 0;
            GetTickCountEx tick = new GetTickCountEx();

            while (!bStopWork)
            {
                DO_AlwaysRUN();
                Thread.Sleep(1);
                ScanTick++;
                TempSecond = tick.Value;

                if ((TempSecond - LastSecond) >= 1000)
                {
                    LastSecond = TempSecond;

                    if (SysPara.SystemMode == RunMode.AUTO)
                    {
                        SysPara.OperationSecond++;
                        if (SysPara.SystemRun)
                            SysPara.RunSecond++;
                        else
                            SysPara.StopSecond++;
                    }

                    SysPara.ScanTime_AlwaysRun = ScanTick;
                    ScanTick = 0;
                }
            }
        }
        private void Execute()
        {
			
            if (SysPara.SystemRun)
            {
                if (SysPara.SystemMode == RunMode.AUTO)
                {
                    try { ExecuteRun(); }
                    catch (Exception) { SysPara.NPShowAlarm("50014"); }
                }
                else if (SysPara.SystemMode == RunMode.HOME)
                {
                    try { ExecuteInitial(); }
                    catch (Exception) { SysPara.NPShowAlarm("50012"); }
                }
            }
            
        }
        private void DO_RefreshIOS()
        {
            try { MiddleLayer.CheckMotorProtected(); }
            catch (Exception ex) { SysPara.NPShowAlarm("50016", ex.Message); }

            //try
            //{
            //    SDKKernal.RefreshIO();
            //    if(MiddleLayer.EpsonRobotF.bWriteRegister)
            //    {
            //        if(SDKPara.ModbusDevices.ContainsKey(MiddleLayer.EpsonRobotF.sRobotIP) && SDKPara.ModbusDevices[MiddleLayer.EpsonRobotF.sRobotIP].modbusClient.Connected)
            //        {
            //            MiddleLayer.EpsonRobotF.bWriteRegister = false;

            //            int iFirstRegister = MiddleLayer.EpsonRobotF.Registers.Keys.First();
            //            int[] iValues = MiddleLayer.EpsonRobotF.Registers.Values.ToArray();
            //            SDKPara.ModbusDevices[MiddleLayer.EpsonRobotF.sRobotIP].modbusClient.WriteMultipleRegisters(iFirstRegister, iValues);

            //        }
            //    }

            //}
            //catch(Exception ex)
            //{
            //    SysPara.NPShowAlarm("2018", ex.Message);
            //}
            try { SDKKernal.RefreshIO(); }
            catch (Exception ex) { SysPara.NPShowAlarm("50015", ex.Message); }

        }
         private void DO_AlwaysRUN()
        {
                    try { MiddleLayer.AlwaysRun(); }
                catch (Exception) { SysPara.NPShowAlarm("50007"); }
        }
        #region Initial
        private int iInitialTask = 0;
        public void InitialReset()
        {
            iInitialTask = 0;
            SysPara.SystemMode = RunMode.HOME;
            SysPara.SystemInitialOk = false;
        }

        private void ExecuteInitial()
        {
            switch (iInitialTask)
            {
                case 0:
                    MiddleLayer.InitialParameterReset();
                    iInitialTask++;
                    break;
                case 1:
                    MiddleLayer.InitialReset();
                  
                    iInitialTask++;
                    break;
                case 2:
                    MiddleLayer.ServoOn();
                    iInitialTask++;
                    break;
                case 3:
                    iInitialTask++;
                    break;
                case 4:
                    MiddleLayer.Initial();
                    if (MiddleLayer.GetInitialOk())
                        iInitialTask++;
                    break;
                case 5:
                    SysPara.SystemInitialOk = true;
                    MiddleLayer.StopRun();
                    SysPara.SystemMode = RunMode.IDLE;   
                    iInitialTask++;
                    break;
                case 6:
                    break;
            }
        }
        #endregion

        #region Run
        private int iRunTask = 0;
        public void RunReset()
        {
            iRunTask = 0;
            SysPara.SystemMode = RunMode.AUTO;
        }

        private void ExecuteRun()
        {
            if (SysPara.SystemInitialOk)
            {
                switch (iRunTask)
                {
                    case 0:
                        iRunTask++;
                        break;
                    case 1:
                        MiddleLayer.RunReset();
                        iRunTask++;
                        break;
                    case 2:
                        MiddleLayer.Run();
                        break;
                }
            }
        }
        #endregion
    }
}

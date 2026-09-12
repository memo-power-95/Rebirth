using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace Alpha
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Boolean bCreatedNew;
            Mutex m = new Mutex(false, "myUniqueName", out bCreatedNew);
            if (!bCreatedNew)
            {
                 string sMsgBox="Program has been run";
                  MethodBase MB = MethodBase.GetCurrentMethod();
                string sTittle=$"Error {MB.ReflectedType.Name} {MB.Name}";
                MessageBox.Show(new Form { TopMost = true }, sMsgBox,sTittle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MiddleLayer.LoadingMarqueeF = new LoadingMarqueeForm();
            MiddleLayer.LoadingMarqueeF.Show();
            Thread LoadingMarqueeT = new Thread(MiddleLayer.LoadingMarqueeF.RefreshUI);
            LoadingMarqueeT.Start();
            MiddleLayer.InitialProject(); //Initial Project
            MiddleLayer.LoadingMarqueeF.StopRefresh = true;
            LoadingMarqueeT.Join();
            MiddleLayer.LoadingMarqueeF.Close();

            Application.Run(MiddleLayer.MainF); //Start Project
            MiddleLayer.DisposeProject(); //Dispose Project
        }
     
    }
}

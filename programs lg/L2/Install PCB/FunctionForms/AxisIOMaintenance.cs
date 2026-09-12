using System;
using System.Drawing;
using System.Windows.Forms;

namespace Alpha.FunctionForms.AxisMotorForms
{
	public partial class AxisIOMaintenance : UserControl
    {
        #region ------------------------------------VARIABLES
        private NPSDK.NPMotor AxisMotor;

        //TIMERS
        private JTimer tmr_fcAxisIO_Refresh = new JTimer();
        #endregion ---------------------------------VARIABLES
        public AxisIOMaintenance(ref NPSDK.NPMotor refAxisMotor)
        {
            InitializeComponent();
            AxisMotor = refAxisMotor;
            lb_AxisIO.Text = AxisMotor.Name;
            timerRefreshIO.Enabled = true;
        }


        #region --------------------------------------------------------Functions

        private void timerRefreshIO_Tick(object sender, EventArgs e)
        {
            timerRefreshIO.Enabled = false;
            try
            {
                NPSDK.MotionState MState = AxisMotor.GetMotionState();
                NPSDK.AxisIOState IOState = AxisMotor.GetAxisIOState();
                panel_HMV.BackColor = MState.HMV ? Color.Red : SystemColors.ScrollBar;
                panel_SMV.BackColor = MState.SMV ? Color.Red : SystemColors.ScrollBar;
                panel_ALM.BackColor = IOState.ALM ? Color.Red : SystemColors.ScrollBar;
                panel_EMG.BackColor = IOState.EMG ? Color.Red : SystemColors.ScrollBar;
                panel_MEL.BackColor = IOState.MEL ? Color.Red : SystemColors.ScrollBar;
                panel_PEL.BackColor = IOState.PEL ? Color.Red : SystemColors.ScrollBar;
                panel_SVON.BackColor = IOState.SVON ? Color.Red : SystemColors.ScrollBar;
                panel_ORG.BackColor = IOState.ORG ? Color.Red : SystemColors.ScrollBar;
            }
            catch (Exception ex) 
            {
                MiddleLayer.ExReportF.fnAddException(ex);
			}
            timerRefreshIO.Enabled = true;
        }
        #endregion -----------------------------------------------------Functions

    }
}

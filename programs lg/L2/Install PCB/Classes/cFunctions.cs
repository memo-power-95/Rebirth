using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPSDK;
using System.Windows.Forms;



namespace Alpha.Classes
{
    /// <summary>
    /// Class to put a dictionary of functions that helps working with UI updating info colors, or to show process status, Yield and Jidoka things.
    /// </summary>
	public static class cFunctions
    {

        #region -------------------------------------------------------- Extension UI Invokers Functions
        /// <summary>
        /// Resetea los colores de las celdas en el  datagrid view que tenia cambios
        /// </summary>
        /// <param name="grid"></param>
        public static void fnResetGridColor(this DataGridView grid)
        {
            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (col.DefaultCellStyle.BackColor != Color.Empty)
                    col.DefaultCellStyle.BackColor = Color.Empty;
            }
        }

        public static void nUpDownValue(this NumericUpDown ob, decimal Value)
        {

            if (ob.InvokeRequired)
            {
                ob.BeginInvoke((MethodInvoker)(() =>

                ob.nUpDownValue(Value)
                ));
            }
            else
            {
                ob.Value = Value;
            }
        }


        public static void SetText(this Object ob, string sText)
        {
            Control cntl_OB = ((Control)ob);
            if (cntl_OB != null)
            {
                if (cntl_OB.InvokeRequired)
                {
                    cntl_OB.BeginInvoke((MethodInvoker)(() =>

                    ob.SetText(sText)
                    ));
                }
                else
                {
                    ((Control)ob).Text = sText;
                }
            }
        }
        /// <summary>
		/// Functions the set text. Puts somthing to a control
		/// </summary>
		/// <param name="ob">The object</param>
		/// <param name="sText">The s text.</param>
		public static void SetBackColor(this Object ob, Color cl)
        {
            Control cntl_OB = ((Control)ob);
            if (cntl_OB != null)
            {
                if (cntl_OB.InvokeRequired)
                {
                    cntl_OB.BeginInvoke((MethodInvoker)(() =>

                    ob.SetBackColor(cl)
                    ));
                }
                else
                {
                    ((Control)ob).BackColor = cl;
                }
            }
        }
        /// <summary>
        /// Functions the set text. Puts somthing to a control
        /// </summary>
        /// <param name="ob">The object</param>
        /// <param name="sText">The s text.</param>
        public static void SetForeColor(this Object ob, Color cl)
        {
            Control cntl_OB = ((Control)ob);
            if (cntl_OB != null)
            {
                if (cntl_OB.InvokeRequired)
                {
                    cntl_OB.BeginInvoke((MethodInvoker)(() =>
                    ob.SetForeColor(cl)
                    ));
                }
                else
                {
                    ((Control)ob).ForeColor = cl;
                }
            }
        }
        /// <summary>
        /// Functions the set text. Puts somthing to a control
        /// </summary>
        /// <param name="ob">The object</param>
        /// <param name="sText">The s text.</param>
        public static void SetFocus(this Object ob)
        {
            Control cntl_OB = ((Control)ob);
            if (cntl_OB != null)
            {
                if (cntl_OB.InvokeRequired)
                {
                    cntl_OB.BeginInvoke((MethodInvoker)(() =>
                    ob.SetFocus()
                    ));
                }
                else
                {
                    ((Control)ob).Focus();
                }
            }
        }
        /// <summary>
        /// Functions the item enabled. Puts somthing to a control
        /// </summary>
        /// <param name="ob">The object</param>
        /// <param name="sText">The s text.</param>
        public static void SetEnable(this Object ob, bool bEnable)
        {
            Control cntl_OB = ((Control)ob);
            if (cntl_OB != null)
            {
                if (cntl_OB.InvokeRequired)
                {
                    cntl_OB.BeginInvoke((MethodInvoker)(() =>
                    ob.SetEnable(bEnable)
                    ));
                }
                else
                {
                    ((Control)ob).Enabled = bEnable;
                }
            }
        }
        /// <summary>
        /// Functions the item enabled. Puts somthing to a control
        /// </summary>
        /// <param name="ob">The object</param>
        /// <param name="sText">The s text.</param>
        public static void SetVisible(this Object ob, bool bEnable)
        {
            Control cntl_OB = ((Control)ob);
            if (cntl_OB != null)
            {
                if (cntl_OB.InvokeRequired)
                {
                    cntl_OB.BeginInvoke((MethodInvoker)(() =>
                    ob.SetVisible(bEnable)
                    ));
                }
                else
                {
                    ((Control)ob).Visible = bEnable;
                }
            }
        }
        /// <summary>
        /// Functions the item backgorund image. Puts somthing to a control
        /// </summary>
        /// <param name="ob">The object</param>
        /// <param name="sText">The s text.</param>
        public static void SetBackGroundImage(this Object ob, Image img)
        {
            Control cntl_OB = ((Control)ob);
            if (cntl_OB != null)
            {
                if (cntl_OB.InvokeRequired)
                {
                    cntl_OB.BeginInvoke((MethodInvoker)(() =>
                    ob.SetBackGroundImage(img)
                    ));
                }
                else
                {
                    ((Control)ob).BackgroundImage = img;
                }
            }
        }
        public static void fnLayoutControlAdd(this TableLayoutPanel tb, Control Value, int column = 0, int row = 0)
        {
            if (tb.InvokeRequired)
            {
                tb.BeginInvoke((MethodInvoker)(() =>

                tb.fnLayoutControlAdd(Value, column, row)
                ));
            }
            else
            {
                tb.Controls.Add(Value, column, row);
            }
        }
        public static void fnControlAdd(this TabControl tb, Control Value)
        {
            if (tb.InvokeRequired)
            {
                tb.BeginInvoke((MethodInvoker)(() =>
                fnControlAdd(tb, Value)
                ));
            }
            else
            {
                tb.Controls.Add(Value);
            }
        }
        public static void fnControlAdd(this Panel tb, Control Value)
        {
            if (tb.InvokeRequired)
            {
                tb.BeginInvoke((MethodInvoker)(() =>
                fnControlAdd(tb, Value)
                ));
            }
            else
            {
                tb.Controls.Add(Value);
            }
        }
        public static void fnControlAdd(this FlowLayoutPanel tb, Control Value)
        {
            if (tb.InvokeRequired)
            {
                tb.BeginInvoke((MethodInvoker)(() =>
                fnControlAdd(tb, Value)
                ));
            }
            else
            {
                tb.Controls.Add(Value);
            }
        }
        public static void fnControlAdd(this GroupBox tb, Control Value)
        {
            if (tb.InvokeRequired)
            {
                tb.BeginInvoke((MethodInvoker)(() =>
                fnControlAdd(tb, Value)
                ));
            }
            else
            {
                tb.Controls.Add(Value);
            }
        }
        public static void ProgressBar_SetValue(this ProgressBar pg, int Value)
        {
            if (pg.InvokeRequired)
            {
                pg.BeginInvoke((MethodInvoker)(() =>

                pg.ProgressBar_SetValue(Value)
                ));
            }
            else
            {
                pg.Value = Value > 100 ? 100 : Value;
            }
        }

        public static void ComboBox_AddItem(this ComboBox cb, string sText)
        {
            if (sText != null)
                if (cb.InvokeRequired)
                {
                    cb.BeginInvoke((MethodInvoker)(() =>
                    cb.ComboBox_AddItem(sText)
                    ));
                }
                else
                {
                    cb.Items.Add(sText);
                }
        }
        public static void ComboBox_AddItems(this ComboBox cb, List<string> aText)
        {
            if (aText != null && aText.Count > 0)
                if (cb.InvokeRequired)
                {
                    cb.BeginInvoke((MethodInvoker)(() =>
                    cb.ComboBox_AddItems(aText)
                    ));
                }
                else
                {
                    cb.Items.AddRange(aText.ToArray());
                }
        }
        public static void ComboBox_Clear(this ComboBox cb)
        {
            if (cb.InvokeRequired)
            {
                cb.BeginInvoke((MethodInvoker)(() =>
                cb.ComboBox_Clear()
                ));
            }
            else
            {
                cb.Items.Clear();
            }
        }
        public static void ComboBox_SetSelectedIndex(this ComboBox cb, int index)
        {
            if (cb.InvokeRequired)
            {
                cb.BeginInvoke((MethodInvoker)(() =>
                cb.ComboBox_SetSelectedIndex(index)
                ));
            }
            else
            {
                cb.SelectedIndex = index;
            }
        }
        public static void ComboBox_SetSelectedItem(this ComboBox cb, Object ob)
        {
            if (cb.InvokeRequired)
            {
                cb.BeginInvoke((MethodInvoker)(() =>
                cb.ComboBox_SetSelectedItem(ob)
                ));
            }
            else
            {
                cb.SelectedItem = ob;
            }
        }

        public static void ClickVirtual(this Button bt)
        {
            if (bt.InvokeRequired)
            {
                bt.BeginInvoke((MethodInvoker)(() =>
                bt.ClickVirtual()
                ));
            }
            else
            {

                bt.PerformClick();
            }
        }
        public static void ClickVirtual(this RadioButton rd)
        {
            if (rd.InvokeRequired)
            {
                rd.BeginInvoke((MethodInvoker)(() =>
                rd.ClickVirtual()
                ));
            }
            else
            {
                rd.PerformClick();
            }
        }

        public static void NumericUpDown_SetValue(this NumericUpDown nud, decimal Value)
        {
            if (nud.InvokeRequired)
            {
                nud.BeginInvoke((MethodInvoker)(() =>
                nud.NumericUpDown_SetValue(Value)
                ));
            }
            else
            {

                nud.Value = Value;
            }
        }

        #endregion

        #region ---------------------------------NPMotor Beta Functions
        /// <summary>
        /// Function Sobrecargada para ir a una posicion y checar la posicion actual del servo
        /// </summary>
        /// <param name="mt">Objeto NPMotor</param>
        /// <param name="pos">Posicion a la que se va ir </param>
        /// <param name="tolerance">Tolerancia de la posicion pude ser 0.2</param>
        /// <returns></returns>
        public static bool fnMotor_GotoPosAndCheckPos(this NPSDK.NPMotor mt, double pos, double tolerance)
        {
            double Diff = Math.Abs((mt.GetEncPos()) - pos);
            bool bArrive = mt.Goto(pos);
            bool bGoto = bArrive && (Diff <= tolerance);
            return bGoto;
        }
        public static bool fnMotor_GotoPosAndCheckPosSlider(this NPSDK.NPMotor mt, double pos, double tolerance)
        {
            double Diff = Math.Abs((mt.GetEncPos()) - pos);
            bool bArrive = mt.Goto(pos);
            bool bGoto = bArrive && (Diff <= tolerance);
            return bGoto;
        }
        /// <summary>
        /// Devuelve true si existe algun error o limite de carrera
        /// </summary>
        /// <param name="mt"></param>
        /// <returns></returns>
        public static bool fnMotor_ErrorStates(this NPSDK.NPMotor mt)
        {
            return (mt.GetAxisIOState().ALM || mt.GetAxisIOState().EMG || mt.GetAxisIOState().PEL || mt.GetAxisIOState().MEL);
        }
        public static bool fnMotor_SetVelocity(this NPSDK.NPMotor mt, double dWorkSpeed, double dMaxSpeed = 0, double dAceleracion = 0, double dDesaceleracion = 0)
        {
            try
            {
                mt.WorkSpeed = dWorkSpeed;
                mt.MaxSpeed = dMaxSpeed != 0 ? dMaxSpeed : mt.MaxSpeed;
                mt.Tacc = dAceleracion != 0 ? dAceleracion : mt.Tacc;
                mt.Tdec = dDesaceleracion != 0 ? dDesaceleracion : mt.Tdec;
                return true;
            }
            catch (Exception ex)
            {
                MiddleLayer.ExReportF.fnAddException(ex);
            }
            return false;
        }

       public static void MotorJogMouseDown(this NPMotor NPMotor, Enums.Jog jogMode)
        {
            if (MiddleLayer.MotorJogF.IsJogMode)
            {
                if (Enums.Jog.Positive == jogMode)
                    NPMotor.JogP();
                else
                    NPMotor.JogN();

            }

            else
            {
                double dCurrentPosition = NPMotor.GetPos();
                double dRelativeDistance = Math.Abs(MiddleLayer.MotorJogF.Distance);

                double dTargetPosition = Enums.Jog.Positive == jogMode ? dCurrentPosition + dRelativeDistance : dCurrentPosition - dRelativeDistance;
                NPMotor.Goto(dTargetPosition);
            }
        }
        public static void MotorJogMouseUp(this NPMotor NPMotor)
        {
            if (MiddleLayer.MotorJogF.IsJogMode)
            {
                NPMotor.Stop();
            }
        }
        #endregion -------------------------------NPMotor Functions



        #region Math Functions for Vision ETC
        //VISION
        /// <summary>
        /// <para>Funcion que rota un punto X Y </para>
        /// <para>Tomando de punto central otro punto en X Y</para>
        /// </summary>
        /// <param name="dXc">Es la referencia X del punto central al giro</param>
        /// <param name="dYc">Es la referencia Y del punto central al giro</param>
        /// <param name="dX">Es la referencia X que se va girar respecto al centro</param>
        /// <param name="dY">>Es la referencia X que se va girar respecto al centro</param>
        /// <param name="dTETA">Es el angulo en grados que se va rotar</param>
        public static void RotatePoints(double dXc, double dYc, ref double dX, ref double dY, double dTETA)
        {

            //Punto a rotar Xp,Yp apartir de centro  Xc ,Yc
            //Grados a girar a°

            //Se traslada p y c a origenes y se llama punto Xo y Yo
            //Xo=Xp-Xc
            //Yo=Yp-Yc
            double Xo = dX - dXc;
            double Yo = dY - dYc;
            double dRadians = Math.PI * dTETA / 180.0;
            //Xr=Xo cos(a°)-Yo sin(a°)
            //Yr=Xo sin(a°)+Yo cos(a°)
            double Xr = Xo * Math.Cos(dRadians) - Yo * Math.Sin(dRadians);
            double Yr = Xo * Math.Sin(dRadians) + Yo * Math.Cos(dRadians);



            //Xr y Yr es el punto rotado respecto a origen
            //se recupera referencia en base a centro quedando 
            //(Xf,Yf)=(Xc,Yc)+(Xr,Yr) 
            dX = dXc + Xr;
            dY = dYc + Yr;



            //Resultado Final 
        }


        public static double GetDegressFrom2Point(double x1, double y1, double x2, double y2 , bool bIsComplement)
        {
            double Result=0;
            //Result=Math.Atan((x2-x1)/(y2-y1));//justthis
            Result=Math.Atan2((y2-y1),(x2-x1));

            //Resultado en Radianes 
            //Lo convertimos a Grados
            Result=Result*180 / Math.PI;

            if (bIsComplement&&Math.Abs(Result)>90)
            {
                Result = Result+(Result>0?-90:90);
            }
            return Result;
        }
         public static double GetDegressFromCatetos(double x1, double y1, double x2, double y2 , bool bIsComplement)
        {
            double Result=0;
            Result=Math.Atan((x2-x1)/(y2-y1));
            //Result=Math.Atan2((y2-y1),(x2-x1));
            //Result=Math.Atan2((y2-y1),(x2-x1));
            //Resultado en Radianes 
            //Lo convertimos a Grados
            Result=Result*180 / Math.PI;

            //if (bIsComplement)
            //{
            //    Result = 90 - Result;
            //}
            return Result;
        }
        /// <summary>
        /// <para>Funcion que rota un punto X Y </para>
        /// <para>Tomando de punto central otro punto en X Y</para>
        /// </summary>
        /// <param name="dXc">Es la referencia X del punto central al giro</param>
        /// <param name="dYc">Es la referencia Y del punto central al giro</param>
        /// <param name="dX">Es la referencia X que se va girar respecto al centro</param>
        /// <param name="dY">>Es la referencia X que se va girar respecto al centro</param>
        /// <param name="dTETA">Es el angulo en grados que se va rotar</param>
        public static void fnRotatePointsPolar(double dXc, double dYc, ref double dX, ref double dY, double dTETA)
        {

            //Punto a rotar Xp,Yp apartir de centro  Xc ,Yc
            //Grados a girar a°

            //Se traslada p y c a origenes y se llama punto Xo y Yo
            //Xo=Xp-Xc
            //Yo=Yp-Yc
            double Xo = dX - dXc;
            double Yo = dY - dYc;
            double dRadians = Math.PI * dTETA / 180.0;
            //Xr=Xo cos(a°)-Yo sin(a°)
            //Yr=Xo sin(a°)+Yo cos(a°)
            double dVectorAng=Math.Atan2(Yo,Xo);
            double dFinalAngle=dVectorAng-dRadians;
            double dMagnitud=Math.Sqrt(Math.Pow(Xo,2)+Math.Pow(Yo,2));
            double Xr = dMagnitud*Math.Cos(dFinalAngle);
            double Yr = dMagnitud*Math.Sin(dFinalAngle);
            //Xr y Yr es el punto rotado respecto a origen
            //se recupera referencia en base a centro quedando 
            //(Xf,Yf)=(Xc,Yc)+(Xr,Yr) 
            dX = dXc + Xr;
            dY = dYc + Yr;



            //Resultado Final 
        }
        /// <summary>
        /// <para>Funcion que rota un punto X Y </para>
        /// <para>Tomando de punto central otro punto en X Y</para>
        /// </summary>
        /// <param name="dXc">Es la referencia X del punto central al giro</param>
        /// <param name="dYc">Es la referencia Y del punto central al giro</param>
        /// <param name="dX">Es la referencia X que se va girar respecto al centro</param>
        /// <param name="dY">>Es la referencia X que se va girar respecto al centro</param>
        /// <param name="dTETA">Es el angulo en grados que se va rotar</param>
        public static void fnRotatePoints(double dXc, double dYc, ref double dX, ref double dY, double dTETA)
        {

            //Punto a rotar Xp,Yp apartir de centro  Xc ,Yc
            //Grados a girar a°

            //Se traslada p y c a origenes y se llama punto Xo y Yo
            //Xo=Xp-Xc
            //Yo=Yp-Yc
            double Xo = dX - dXc;
            double Yo = dY - dYc;
            double dRadians = Math.PI * dTETA / 180.0;  
            //Xr=Xo cos(a°)-Yo sin(a°)
            //Yr=Xo sin(a°)+Yo cos(a°)
            double Xr = Xo * Math.Cos(dRadians) - Yo * Math.Sin(dRadians);
            double Yr = Xo * Math.Sin(dRadians) + Yo * Math.Cos(dRadians);

            //Xr y Yr es el punto rotado respecto a origen
            //se recupera referencia en base a centro quedando 
            //(Xf,Yf)=(Xc,Yc)+(Xr,Yr) 
            dX = dXc + Xr;
            dY = dYc + Yr;



            //Resultado Final 
        }
		#endregion

		#region --------------------------Production  Functions  Jidoka and Counters
         /// <summary>
            /// Functions the update status specified. Actualiza el Status de lo que corre
            /// </summary>
            /// <param name="pnl">The PNL.</param>
            /// <param name="tb_Status">The tb status.</param>
            public async static void UpdateStatus_Specified(NPSDK.NPFlowChart fc, TextBox tb_Status)
            {
                await Task.Run(() => {
                    try
                    {
                        NPSDK.NPFlowChart current =fc.WorkFlow;
                        string sProcess=current?.Text;
                        while (current?.SubFlowChart!=null)
                        {
                            sProcess+=">";
                           current=current.WorkFlow;
                        }
                        SetText(tb_Status,sProcess);
                    }
                    catch (Exception ex)
                    {
                        MiddleLayer.ExReportF.fnAddException(ex);
                    }
                    return;
                }).ConfigureAwait(false);
            }
            /// <summary>
            /// Function that updates a TextBox to Show the specified last runing NPFlowChart on the auto or home process. This option was develop to show the status on the machine status of the acura form.
            /// </summary>
            /// <param name="fcAuto"> Main NPFlowChart to start tracking when runing auto</param>
            /// <param name="fcInit">Main NPFlowChart to start tracking when runing home</param>
            /// <param name="tbText">TextBox that show status</param>
            public static void UpdateStatus(NPSDK.NPFlowChart fcAuto,NPSDK.NPFlowChart fcInit,TextBox tbText)
            {
                #region Obtiene el Estado del Sistema Auto o Home o Sale de Funcion
                string sIsAutoOrHome = "";
                ///Obtenemos el stado del sistema
                if (SysPara.SystemMode == RunMode.AUTO)
                {
                    sIsAutoOrHome = "AUTO";
                }
                else if (SysPara.SystemMode == RunMode.HOME)
                {
                    sIsAutoOrHome = "HOME";
                }
                else if (SysPara.SystemMode == RunMode.IDLE)
                {
                    return;
                }
                #endregion

                UpdateStatus_Specified(sIsAutoOrHome == "AUTO" ? fcAuto : fcInit, tbText);

            }

            /// <summary>
            /// Funcion para contabilizar una unidad y sumarla a buenas y malas, calcula FPY
            /// </summary>
            public static void AddCountUnit(TextBox tbGood,TextBox tbTotal,TextBox tbFPY,ProgressBar pgFPY,bool bPass)
            {
                fnCounterAdd(tbTotal);
                if (bPass)
                {
                    fnCounterAdd(tbGood);
                }
                int nTotal=Convert.ToInt32(tbTotal.Text);
                int nGood = Convert.ToInt32(tbGood.Text);
                float f_FPY=CalcFPY(nGood,nTotal);
                tbFPY.SetText(Convert.ToInt32(f_FPY).ToString());
                pgFPY.ProgressBar_SetValue(Convert.ToInt16(f_FPY));
            }
            /// <summary>
            /// Funcion para contabilizar una unidad y sumarla a buenas y malas, calcula FPY
            /// </summary>
            public static void AddCountUnit(Label lblGood, Label lblBad, Label lblTotal, Label lblFPY, bool bPass)
            {
                fnCounterAdd(lblTotal);
                if (bPass)
                    fnCounterAdd(lblGood);
                else
                    fnCounterAdd(lblBad);
                int nTotal=Convert.ToInt32(lblTotal.Text);
                int nGood = Convert.ToInt32(lblGood.Text);
                float f_FPY=CalcFPY(nGood,nTotal);
                lblFPY.SetText($"{Convert.ToInt32(f_FPY).ToString()} %");
            }
            
            /// <summary>
            /// Functions the set cycle time. Pone el Tiempo Cyclo 
            /// </summary>
            public static void SetCycleTime(this Object ob,JTimer timer)
            {
                string sText = string.Format("{0:0.0}", ((double)timer.GetValue() / (double)1000)) + " s";
                ob.SetText(sText);
                //if(ob.GetType()==typeof(Label))
                //    ((Label)ob).SetText(sText);
                //else if (ob.GetType()==typeof(TextBox))
                //    ((TextBox)ob).SetText(sText);
                timer.Restart();
            }

            /// <summary>
            /// Funciona para calcular el FPY
            /// </summary>
            public static float CalcFPY(int nGoodCount, int nTotalCount)
            {
                if (nTotalCount == 0)
                {
                    nTotalCount = 1;
                }
                float nFPY = ((float)nGoodCount / (float)nTotalCount);
                return nFPY = nFPY * 100;
            }
            
            /// <summary>
            /// Functions the counter add. Incrementa el numero de unidades
            /// </summary>
            /// <param name="timer">The timer.</param>
            /// <param name="tb">The tb.</param>
            public static  void fnCounterAdd(Control tb)
            {
                decimal nCount ;
                if (!Decimal.TryParse(tb.Text,out nCount))
                {
                    nCount=0;
                }
                SetText(tb, (nCount+1).ToString());
            }
            public static void ResetCount(TextBox tbCounter,Label lblInfo)
            {
                tbCounter.SetText("0");
                lblInfo.SetText("Contando desde: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            }
            public static void ResetCount(TextBox tbCounter,TextBox tbCounterTotal,TextBox tbFPY,ProgressBar pg,Label lblInfo)
            {
                tbCounter.SetText("0");
                tbCounterTotal.SetText("0");
                tbFPY.SetText("0");
                pg.ProgressBar_SetValue(0);
                lblInfo.SetText("Contando desde: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            }
		public static bool JidokaAddCount(TextBox tbCount, TextBox tbLimit,bool bPass, int Limit,bool bHabilitado,bool bFallasConsecutivas)
		{
            bool bAlarma=false;
			bool JidokaEnable=bHabilitado;
			if (JidokaEnable)
			{
				bool bConsecutivas=bFallasConsecutivas;
				if (bConsecutivas&&bPass)
				{
                    tbCount.SetText("0");
				}
			    if (!bPass)
			    {
                    fnCounterAdd(tbCount);
                    if (Convert.ToInt32(tbCount.Text)>=Limit)
                    {
                        bAlarma=true;
                    }
			    }
            }
            else
            {
                 tbCount.SetText("0");
            }
			
            tbLimit.SetText(Limit.ToString("f0"));
            return bAlarma;
		}
		#endregion -----------------------Jidoka

		#region Draw Invokers Functions 
          public static void fnDrawLine(this Panel pn, Color cl, PointF init, PointF end, PointF Min, PointF Max, float pWdith)
		    {
                    
			       if (pn.InvokeRequired)
                    {
                        pn.BeginInvoke((MethodInvoker)(() =>
					     pn.fnDrawLine(cl,init,end,Min,Max,pWdith)
                        ));
                    }
                    else
                    {
					    using(Graphics g = pn.CreateGraphics())
					    {
						  int panelX=pn.Size.Width-40;//40 es el marco de 20 mm
					      int panelY=pn.Height-40;//40 es el marco de 20 mm
                          float RangoX=Max.X-Min.X;
                          float RangoY=Max.Y-Min.Y;
                          double factorX=panelX/RangoX;
                          double factorY=panelY/RangoY;
                          init.X=(int)((init.X-Min.X)*factorX)+20;
                          init.Y=(int)((init.Y-Min.Y)*factorY)+20;
                          end.X=(int)((end.X-Min.X)*factorX)+20;
                          end.Y=(int)((end.Y-Min.Y)*factorY)+20;
					      var pPen = new Pen(cl, pWdith);
                          g.DrawLine(pPen,init,end);
					    }
                    }
			
		    }
             public static void fnDrawLine(this Panel pn, Color cl, PointF init, PointF end, PointF Min, PointF Max, float pWdith, Bitmap bmp)
		    {
                    
			       if (pn.InvokeRequired)
                    {
                        pn.BeginInvoke((MethodInvoker)(() =>
					     pn.fnDrawLine(cl,init,end,Min,Max,pWdith)
                        ));
                    }
                    else
                    {
					    using(Graphics g = Graphics.FromImage(bmp))
					    {
						  int panelX=pn.Size.Width-40;//40 es el marco de 20 mm
					      int panelY=pn.Height-40;//40 es el marco de 20 mm
                          float RangoX=Max.X-Min.X;
                          float RangoY=Max.Y-Min.Y;
                          double factorX=panelX/RangoX;
                          double factorY=panelY/RangoY;
                          init.X=(int)((init.X-Min.X)*factorX)+20;
                          init.Y=(int)((init.Y-Min.Y)*factorY)+20;
                          end.X=(int)((end.X-Min.X)*factorX)+20;
                          end.Y=(int)((end.Y-Min.Y)*factorY)+20;
					      var pPen = new Pen(cl, pWdith);
                          g.DrawLine(pPen,init,end);
					    }
                    }
			
		    }
           public static void fnDrawCircle(this Panel pn, Color cl, PointF init, PointF end, PointF Min, PointF Max, float pWdith , Bitmap bmp)
		    {
                    
			       if (pn.InvokeRequired)
                    {
                        pn.BeginInvoke((MethodInvoker)(() =>
					     pn.fnDrawLine(cl,init,end,Min,Max,pWdith)
                        ));
                    }
                    else
                    {
					    using(Graphics g = Graphics.FromImage(bmp))
					    {
						  int panelX=pn.Size.Width-40;//40 es el marco de 20 mm
					      int panelY=pn.Height-40;//40 es el marco de 20 mm
                          float RangoX=Max.X-Min.X;
                          float RangoY=Max.Y-Min.Y;
                          double factorX=panelX/RangoX;
                          double factorY=panelY/RangoY;
                          init.X=(int)((init.X-Min.X)*factorX)+20;
                          init.Y=(int)((init.Y-Min.Y)*factorY)+20;
                          end.X=(int)((end.X-Min.X)*factorX)+20;
                          end.Y=(int)((end.Y-Min.Y)*factorY)+20;
                          Random rd=new Random();
                          int centerX=rd.Next(0,panelX);
                          int centerY=rd.Next(0,panelY);

                          int radius=4;
					      var pPen = new Pen(Color.Blue, pWdith);
                          g.DrawEllipse(pPen, centerX - radius, centerY - radius,radius + radius, radius + radius);
                          
                          g.FillEllipse(Brushes.Blue, centerX - radius, centerY - radius, radius + radius, radius + radius);
					    }
                    }
			
		    }
            public static void fnDrawCircle(this Panel pn, PointF center, PointF Min, PointF Max , Bitmap bmp)
		    {
                    
					using(Graphics g = Graphics.FromImage(bmp))
					{
						int panelX=pn.Size.Width-40;//40 es el marco de 20 mm
					    int panelY=pn.Height-40;//40 es el marco de 20 mm
                        float RangoX=Max.X-Min.X;
                        float RangoY=Max.Y-Min.Y;
                        double factorX=panelX/RangoX;
                        double factorY=panelY/RangoY;
                        float centerX=(float)(center.X);
                        float centerY=(float)(center.Y);
                        centerX = (centerX > Min.X) ? centerX : Min.X-1;
                        centerX = (centerX < Max.X) ? centerX : Max.X+1;
                        centerY = (centerY > Min.Y) ? centerY : Min.Y-1;
                        centerY = (centerY < Max.Y) ? centerY : Max.Y+1;
                        if (centerX>Min.X&&centerX<Max.X)
                        {
                             centerX=(float)((centerX-Min.X)*factorX)+20;
                        }
                        else
                        {
                            centerX=centerX<Min.X?5:centerX;
                            centerX=centerX>Max.X?panelX+25:centerX;
                        }
                        if (centerY>Min.Y&&centerY<Max.Y)
                        {
                            centerY=(float)((centerY-Min.Y)*factorY)+20;
                        }
                        else
                        {
                            centerY=centerY<Min.Y?5:centerY;
                            centerY=centerY>Max.Y?panelY+25:centerY;

                        }
                        int radius=4;
					    var pPen = new Pen(Color.Blue, 2);
                        RectangleF rec=new RectangleF(new PointF((float)centerX,(float)centerY),new Size((radius + radius),(radius + radius))) ;
                        g.DrawEllipse(pPen,rec);
                        g.FillEllipse(Brushes.Blue,rec);
					}
			
		    }
             public static void DrawImage(this Panel pn, Bitmap bmp)
            {
               
                 if (pn.InvokeRequired)
                {
                    pn.BeginInvoke((MethodInvoker)(() =>
                    pn.DrawImage(bmp)
                    ));
                }
                else
                {
                    using(Graphics g = pn.CreateGraphics())
					{
                        g.DrawImage(bmp, Point.Empty);
                    }
                }
            }

        #endregion
       
      

    }

    /// <summary>
    /// Sorter For list By ColumnIndex
    /// </summary>
    /// 
    #region Structs
    //Structura de Datos Especifica para posiciones X Y Z 
    public struct PosDataType
    {
        public double X;
        public double Y;
        public double U;
        public double Z;
    }

    #endregion
    class ListViewItemComparer : System.Collections.IComparer
    {
        private int _nCol = 0;

        public ListViewItemComparer(int Column2Order)
        {
            _nCol = Column2Order;
        }
        public int Compare(object it1, object it2)
        {
            return String.Compare(((ListViewItem)it1).SubItems[_nCol].Text, ((ListViewItem)it2).SubItems[_nCol].Text);
        }
    }

}

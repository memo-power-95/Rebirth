using System;
using System.Collections.Generic;

namespace Acura3._0.Classes
{
    public enum State
    {
        None = 0,
        Cargando,
        Escaneando,
        En_FVT,
        En_Flipper,
        Espera_Pick,
        Espera_Pick_FVT,
        Espera_Place,
        Espera_Descarga,
        Error,
    }
    
    class Pallet
    {

        #region ------------------------------------ Enums

     

        #endregion

        #region ------------------------------------ Variables

        private int ID = 0;
        
        public List<string> sSerialCodeList = new List<string>();

        public State state { get; set; }        

        public DateTime StartTime { get; set; }

        public int ActualPosition { get; set; }

        #endregion

        #region ------------------------------------ Constants

        private const string CodeOnFail = "NoSerialNumber";

        #endregion


        public Pallet(int _id)
        {
            this.ID = _id;
            this.state = State.None;
            this.StartTime =  DateTime.Now;
            this.ActualPosition = 0;
        }

    }
}

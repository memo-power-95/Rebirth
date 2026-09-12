using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acura3.Classes
{
    public static class Enums
    {
        public enum CylinderState
        {
            Extend,
            Retract
        }

        public enum ConveyorState
        {
            Forward,
            Reverse,
            Stop
        }

        public enum UpDownState
        {
            Up,
            Down
        }

        public enum Side
        {
            Left,
            Right
        }

        public enum Jog
        {
            Positive,
            Negative
        }
    }
}

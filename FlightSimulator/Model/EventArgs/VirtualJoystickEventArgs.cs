using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class used to make way for binding aileron and elevator values
/// </summary>
namespace FlightSimulator.Model.EventArgs
{
    public class VirtualJoystickEventArgs
    {
        public double Aileron { get; set; }
        public double Elevator { get; set; }
    }
}

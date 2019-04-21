using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class CommandCenterUCModel
    {
        private double myAileron = 0;
        private double myElevator = 0;
        private double throttle = 0;
        private double rudder = 0;

        NetworkConnection myNetwork;
        public CommandCenterUCModel(NetworkConnection network)
        {
            myNetwork = network;
        }

        public void Write(double aileron, double elevator) {

            if (Math.Abs(myAileron - aileron) > 0.05)
            {
                myAileron = aileron;
                myNetwork.Write("set controls/flight/aileron " + aileron.ToString() + "\r\n");
            }
            if (Math.Abs(myElevator - elevator) > 0.05)
            {
                myElevator = elevator;
                myNetwork.Write("set controls/flight/elevator " + elevator.ToString() + "\r\n");
            }


            //myNetwork.Write("set controls/flight/elevator " + elevator.ToString() + "\r\n");
            // for sliders:
            //myNetwork.Write("set controls/flight/rudder " + rudder.ToString() + "\r\n");
            //myNetwork.Write("set controls/engines/engine/throttle " + throttle.ToString() + "\r\n");
        }
    }
}

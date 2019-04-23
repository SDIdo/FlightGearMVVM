using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class CommandCenterUCModel
    {
        private double myAileron = 0;
        private double myElevator = 0;
        private double myThrottle = 0;
        private double myRudder = 0;

        NetworkConnection myNetwork;
        public CommandCenterUCModel(NetworkConnection network)
        {
            myNetwork = network;
        }

        public void WriteFromJoystick(double aileron, double elevator)
        {

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
        }
        public void WriteFromSliders(double rudder, double throttle)
        {
            if (Math.Abs(myThrottle - throttle) > 0.1)
            {
                myThrottle = throttle;
                myNetwork.Write("set controls/engines/current-engine/throttle " + throttle.ToString() + "\r\n");
            }
            if (Math.Abs(myRudder - rudder) > 0.05)
            {
                myRudder = rudder;
                myNetwork.Write("set controls/flight/rudder " + rudder.ToString() + "\r\n");

            }
        }
        public void WriteFromAutoPilot(string data)
        {
            string[] commands = data.Split('\n');
            int size = commands.Length;
            string parsedCommand;
            for (int i = 0; i < size ; ++i) {
                if (i == size - 1)
                {
                    parsedCommand = commands[i] + "\r\n";
                } else
                {
                    parsedCommand = commands[i] + "\n";
                }
                Thread.Sleep(2000);
                new Thread(() =>
                {
                    myNetwork.Write(parsedCommand);
                }).Start();
            } 
        }
    }
}

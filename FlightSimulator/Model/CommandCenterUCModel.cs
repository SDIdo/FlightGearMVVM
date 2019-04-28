using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Model for command control operations
/// </summary>
namespace FlightSimulator.Model
{
    public class CommandCenterUCModel
    {
        static readonly int initVal = 0;
        static readonly double threshold = 0.05;
        static readonly double throttleGaps = 0.1;
        static readonly int writtenCommandDelay = 2000;
        static readonly string aileronCommand = "set controls/flight/aileron ";
        static readonly string elevatorCommand = "set controls/flight/elevator ";
        static readonly string throttleCommand = "set controls/engines/current-engine/throttle ";
        static readonly string rudderCommand = "set controls/flight/rudder ";
        static readonly string suffix = "\r\n";
        static readonly char newline = '\n';
        static readonly string newlineStr = "\n";
        private double myAileron = initVal;
        private double myElevator = initVal;
        private double myThrottle = initVal;
        private double myRudder = initVal;

        NetworkConnection myNetwork;
        /// <summary>
        /// ctor for this command center model
        /// </summary>
        /// <param name="network">helper manages the communication</param>
        public CommandCenterUCModel(NetworkConnection network)
        {
            myNetwork = network;
        }
        /// <summary>
        /// sends joystick's aileron and elevator commands to the server
        /// </summary>
        /// <param name="aileron">used to control the trailing edge of each wing</param>
        /// <param name="elevator">controls the aircraft's pitch</param>
        public void WriteFromJoystick(double aileron, double elevator)
        {

            if (Math.Abs(myAileron - aileron) > threshold)
            {
                myAileron = aileron;
                myNetwork.Write(aileronCommand + aileron.ToString() + suffix);
            }
            if (Math.Abs(myElevator - elevator) > threshold)
            {
                myElevator = elevator;
                myNetwork.Write(elevatorCommand + elevator.ToString() + suffix);
            }
        }
        /// <summary>
        /// sends slider's rudder and throttle commands to the server
        /// </summary>
        /// <param name="rudder">used to steer the aircraft</param>
        /// <param name="throttle">manages fluid flow, determines force of engine</param>
        public void WriteFromSliders(double rudder, double throttle)
        {
            if (Math.Abs(myThrottle - throttle) > throttleGaps)
            {
                myThrottle = throttle;
                myNetwork.Write(throttleCommand + throttle.ToString() + suffix);
            }
            if (Math.Abs(myRudder - rudder) > threshold)
            {
                myRudder = rudder;
                myNetwork.Write(rudderCommand + rudder.ToString() + suffix);
            }
        }
        /// <summary>
        /// sends a written autopilot command to the server
        /// </summary>
        /// <param name="data">the command</param>
        public void WriteFromAutoPilot(string data)
        {
            string[] commands = data.Split(newline);
            int size = commands.Length;
            string parsedCommand;
            for (int i = 0; i < size ; ++i) {
                if (i == size - 1)
                {
                    parsedCommand = commands[i] + suffix;
                } else
                {
                    parsedCommand = commands[i] + newlineStr;
                }
                new Thread(() =>
                {
                    Thread.Sleep(writtenCommandDelay);
                    myNetwork.Write(parsedCommand);
                }).Start();
            } 
        }
    }
}
using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class CommandCenterUCVM
    {
        private double throttle;
        public double Throttle
        {
            get { return throttle; }
            set {
                throttle = value;
                this.myModel.Write(throttle, rudder);
            }
        }
        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set {
                rudder = value;
                this.myModel.Write(throttle, rudder);
            }
        }

        CommandCenterUCModel myModel;
        public CommandCenterUCVM(CommandCenterUCModel model)
        {
            myModel = model;
        }

        public void Write(double aileron, double elevator)
        {
            new Thread( ()=> {
            myModel.Write(aileron, elevator);
        }).Start();
            
        }

        //private double aileron;
        //private double elevator;
        //public double Aileron {
        //    get { return myM.Aileron; }
        //    set { aileron = value; }
        //    }
        
        //public double Elevator
        //{
        //    get { return myM.Elevator; }
        //    set { elevator = value; }
        //}

    }
}

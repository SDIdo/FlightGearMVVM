using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FlightSimulator.ViewModels
{
    public class CommandCenterUCVM : BaseNotify
    {
        private bool isPink;

        public bool IsPink
        {
            get { return  isPink; }
            set {  isPink = value;
                NotifyPropertyChanged("IsPink");
            }
        }



        private string autopilotCommandText;

        public string AutopilotCommandText
        {
            get { return autopilotCommandText; }
            set
            {
                autopilotCommandText = value;
                NotifyPropertyChanged(autopilotCommandText); //Signalling to the text box
                IsPink = true;
            }
        }


        private double throttle;
        public double Throttle
        {
            get { return throttle; }
            set {
                throttle = value;
                new Thread(() =>
                {
                    this.myModel.WriteFromSliders(rudder, throttle);
                }).Start();
            }
        }
        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set {
                rudder = value;
                new Thread(() =>
                {
                    this.myModel.WriteFromSliders(rudder, throttle);
                }).Start();
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
            myModel.WriteFromJoystick(aileron, elevator);
        }).Start();
            
        }


        public void WriteFromAutopilot(string a)
        {
                myModel.WriteFromAutoPilot(a);
        }

        #region Commands
        #region AutopilotOKCommand
        private ICommand _autopilotOKCommand;
        public ICommand AutopilotOKCommand
        {
            get
            {
                return _autopilotOKCommand ?? (_autopilotOKCommand = new CommandHandler(() => OnAutopilotOK()));
            }
        }
        private void OnAutopilotOK()
        {
            myModel.WriteFromAutoPilot(autopilotCommandText);
            IsPink = false;
        }
        #endregion


        #region AutopilotClearCommand
        private ICommand _autopilotClearCommand;
        public ICommand AutopilotClearCommand
        {
            get
            {
                return _autopilotClearCommand ?? (_autopilotClearCommand = new CommandHandler(() => OnAutopilotClear()));
            }
        }
        private void OnAutopilotClear()
        {
            AutopilotCommandText = "";
            IsPink = false;
        }
        #endregion

        #endregion

    }
}

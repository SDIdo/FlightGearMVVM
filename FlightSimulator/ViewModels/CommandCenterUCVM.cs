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

/// <summary>
/// View model of operations - automatic pilot and manual joystick
/// </summary>
namespace FlightSimulator.ViewModels
{
    public class CommandCenterUCVM : BaseNotify
    {
        private bool isNew;

        public bool IsNew
        {
            get { return  isNew; }
            set {  isNew = value;
                NotifyPropertyChanged("IsNew");
            }
        }

        private string autopilotCommandText;

        public string AutopilotCommandText
        {
            get { return autopilotCommandText; }
            set
            {
                autopilotCommandText = value;
                NotifyPropertyChanged(autopilotCommandText); //Signalling to the autopilot
                if(autopilotCommandText != "")
                {
                    IsNew = true;
                } else
                {
                    IsNew = false;
                }
            }
        }
        private double aileron;
        public double Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                new Thread(() =>
                {
                    this.myModel.WriteFromJoystick(aileron, elevator);
                }).Start();
            }
        }

        private double elevator;
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                new Thread(() =>
                {
                    this.myModel.WriteFromJoystick(aileron, elevator);
                }).Start();
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
        /// <summary>
        /// ctor for this view model
        /// </summary>
        /// <param name="model">a required model</param>
        public CommandCenterUCVM(CommandCenterUCModel model)
        {
            myModel = model;
        }
        /// <summary>
        /// Send from the joystick to be processed to the server
        /// </summary>
        /// <param name="aileron">the aileron value</param>
        /// <param name="elevator">the elevator value</param>
        public void SendFromJoystick(double aileron, double elevator)
        {
            new Thread( ()=> {
            myModel.WriteFromJoystick(aileron, elevator);
        }).Start();
            
        }
        /// <summary>
        /// Send from the autopilot to be processed to the server
        /// </summary>
        /// <param name="commands">one or more commands to send</param>
        public void SendFromAutopilot(string commands)
        {
                myModel.WriteFromAutoPilot(commands);
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
        /// <summary>
        /// Send command/s to the server
        /// </summary>
        private void OnAutopilotOK()
        {
            myModel.WriteFromAutoPilot(autopilotCommandText);
            IsNew = false;
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
        /// <summary>
        /// Upon pressing the clear button color white and clean text block
        /// </summary>
        private void OnAutopilotClear()
        {
            AutopilotCommandText = "";
            IsNew = false;
        }
        #endregion
        #endregion
    }
}

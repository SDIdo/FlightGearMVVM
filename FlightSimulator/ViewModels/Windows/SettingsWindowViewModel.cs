using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
/// <summary>
/// view model for binding communication values with the settings view
/// </summary>
namespace FlightSimulator.ViewModels.Windows
{

    public class SettingsWindowViewModel : BaseNotify
    {
        private ISettingsModel model;
        /// <summary>
        /// ctor for this view model
        /// </summary>
        /// <param name="model">a required settings model</param>
        public SettingsWindowViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        public string FlightServerIP
        {
            get { return model.FlightServerIP; }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        public int FlightCommandPort
        {
            get { return model.FlightCommandPort; }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        public int FlightInfoPort
        {
            get { return model.FlightInfoPort; }
            set
            {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }
        /// <summary>
        /// Model will save current chosen communication values
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }
        /// <summary>
        /// Model will reload current chosen communication values
        /// </summary>
        public void ReloadSettings()
        {
            model.ReloadSettings();
        }

        #region Commands
        #region ClickCommand
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }
        /// <summary>
        ///  Upon clicking OK model will save current chosen communication values
        /// </summary>
        private void OnClick()
        {
            model.SaveSettings();
        }
        #endregion

        #region CancelCommand
        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }
        /// <summary>
        /// Upon cancel click model shall not save chosen communication values
        /// </summary>
        private void OnCancel()
        {
            model.ReloadSettings();
        }
        #endregion
        #endregion
    }
}


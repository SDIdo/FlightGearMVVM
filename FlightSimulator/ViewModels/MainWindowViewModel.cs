using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

/// <summary>
/// reponsible for between main view and main model
/// </summary>
namespace FlightSimulator.ViewModels
{   
    class MainWindowViewModel : BaseNotify
    {
        private MainWindowModel myModel;
        /// <summary>
        /// Set a main model at ctor
        /// </summary>
        /// <param name="model">a required model for main</param>
        public MainWindowViewModel(MainWindowModel model)
        {
            this.myModel = model;
        }

        #region Commands
        #region ClickCommand
        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => OnClickSettings()));
            }
        }
        /// <summary>
        /// Opens up the setting window, on command
        /// </summary>
        private void OnClickSettings()
        {
            Views.Windows.Settings settings = new Views.Windows.Settings();
            settings.ShowDialog();
        }
        #endregion


        #region ConnectCommand
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => OnConnectClick()));
            }
        }
        /// <summary>
        /// start up the connect function in the model
        /// </summary>
        private void OnConnectClick()
        {
            myModel.Connect();
        }
        #endregion


        #region DisconnectCommand
        private ICommand _disconnectCommand;
        public ICommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand ?? (_disconnectCommand = new CommandHandler(() => OnDisconnectClick()));
            }
        }
        /// <summary>
        /// Start up the disconnect function in the model
        /// </summary>
        private void OnDisconnectClick()
        {
            myModel.Disconnect();
        }
        #endregion
        #endregion
    }
}

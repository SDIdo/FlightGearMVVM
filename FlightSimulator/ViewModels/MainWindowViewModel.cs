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

namespace FlightSimulator.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private MainWindowModel myModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

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
        private void OnConnectClick()
        {

            myModel.Connect();
            //FlightBoardModel fbm = new FlightBoardModel(new MyTelnetClient());
            //fbm.Connect("localhost", 5400, 5402);


            //MyTelnetClient tel = new MyTelnetClient();
            //tel.Connect("localhost", 5400, 5402);
            //FlightBoardModel fbm = new FlightBoardModel(tel);
            //MessageBox.Show("Don't Click! it's a TEST");
        }
        #endregion
        #endregion
    }
}

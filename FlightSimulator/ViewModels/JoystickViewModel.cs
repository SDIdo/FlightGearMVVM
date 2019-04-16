using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class JoystickViewModel : INotifyPropertyChanged    //assuming one sided.
    {
        public event PropertyChangedEventHandler PropertyChanged;
        JoystickModel myModel = new JoystickModel(new MyTelnetClient());

        private ICommand _sendCommand;
        public ICommand SendCommand
        {
            get
            {
                return _sendCommand ?? (_sendCommand = new CommandHandler(() => Send()));
            }
        }
        private void Send()
        {
            MessageBox.Show("OnClick");
            model.write();
        }
    }
}

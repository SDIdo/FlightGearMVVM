using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightSimulator.Model;
using System.Windows;

namespace FlightSimulator.Model
{
    class ConnectModel
    {
        public Interface.ISettingsModel mySettingsModel;
        public ConnectModel(Interface.ISettingsModel settingsModel)
        {
            mySettingsModel = settingsModel;
        }
        void show()
        {
            MessageBox.Show(mySettingsModel.FlightCommandPort.ToString());
            Console.WriteLine(mySettingsModel.FlightCommandPort.ToString());
        }
    }
    
}

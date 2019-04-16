using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightSimulator.Model;
using System.Windows;

namespace FlightSimulator.Model
{
    public class ConnectModel      //Obsolete
    {
        public Interface.ISettingsModel mySettingsModel;
        public ConnectModel(Interface.ISettingsModel settingsModel)
        {
            mySettingsModel = settingsModel;
        }
    }
    
}

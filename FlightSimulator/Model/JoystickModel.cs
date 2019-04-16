using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class JoystickModel
    {
        MyTelnetClient myTel;
        public JoystickModel(MyTelnetClient tel)
        {
            myTel = tel;
        }
        public sendMsgToSimulator(string aileron, string elevator)
        {
            MessageBox.Show("Hello");
        }
    }
}

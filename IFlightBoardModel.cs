using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface IFlightBoardModel : INotifyPropertyChanged
    {
        //  Connection to the flight board
        void connect(string ip, int port);
        //void disconnect();
        //void start();

        //  properties
        double Lat { get; set; }
        double Lon { get; set; }

        // activate communication
        //void recieve();
        //void Transmit();
    }

}

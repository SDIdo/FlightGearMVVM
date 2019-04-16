using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public interface ITelnetClient : INotifyPropertyChanged
    {

        void Connect(string ip, int serverPort, int clientPort);
        void Write(string command);
        void Read();
        void Disconnect();
        void Start();
    }
}

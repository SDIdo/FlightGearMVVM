using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class MyTelnetClient : ITelnetClient
    {
        public void connect(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public void disconnect()
        {
            throw new NotImplementedException();
        }

        public string read()
        {
            throw new NotImplementedException();
        }

        public void write(string command)
        {
            throw new NotImplementedException();
        }
    }
}

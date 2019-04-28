using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{/// <summary>
/// All network classes must implement this interface.
/// </summary>
    public interface ITelnetClient : INotifyPropertyChanged
    {
        /// <summary>
        /// Connects using a network stream to given args.
        /// </summary>
        /// <param name="ip">the ip adress</param>
        /// <param name="serverPort">a port to send data to</param>
        /// <param name="clientPort">a port in which data is being sent from</param>
        void Connect(string ip, int serverPort, int clientPort);
        /// <summary>
        /// Send a command by writing data to a network stream
        /// </summary>
        /// <param name="command">data to send to the server</param>
        void Write(string command);
        /// <summary>
        /// Listen or read data from a connected source or a client
        /// </summary>
        void Read();
        /// <summary>
        /// Before exit it's adviced to disconnect from a server and from being a server.
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Start reading or writing on a thread.
        /// </summary>
        void Start();
    }
}

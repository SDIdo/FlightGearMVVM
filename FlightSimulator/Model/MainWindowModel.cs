using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FlightSimulator.Model.Interface;

/// <summary>
/// Model for communication settings options
/// </summary>
namespace FlightSimulator.Model
{
    public class MainWindowModel
    {
        static readonly string localHost = "localhost";
        static readonly string personalIp = "127.0.0.1";
        NetworkConnection myTelnet;
        /// <summary>
        /// ctor for main window model
        /// </summary>
        /// <param name="telnet">a required network connection class</param>
        public MainWindowModel(NetworkConnection telnet)
        {
            this.myTelnet = telnet;
        }
        /// <summary>
        /// Prepare self a server and reach an outside server using Network Connection
        /// </summary>
        public void Connect()
        {
            string inOutIp = Properties.Settings.Default.FlightServerIP;
            int sendPort = Properties.Settings.Default.FlightCommandPort;
            int receivePort = Properties.Settings.Default.FlightInfoPort;
            if (inOutIp == personalIp)
            {
                inOutIp = localHost;
            }
            myTelnet.Connect(inOutIp, receivePort, sendPort);
        }
        /// <summary>
        /// calls Network connection method to disconnect
        /// </summary>
        public void Disconnect()
        {
            myTelnet.Disconnect();
        }
    }
}

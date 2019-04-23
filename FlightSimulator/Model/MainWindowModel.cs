using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model
{
    public class MainWindowModel
    {
        NetworkConnection myTelnet;
        public MainWindowModel(NetworkConnection telnet)
        {
            this.myTelnet = telnet;
        }


        public void Connect()
        {
            string ip = Properties.Settings.Default.FlightServerIP;
            int sendPort = Properties.Settings.Default.FlightCommandPort;
            int receivePort = Properties.Settings.Default.FlightInfoPort;
            MessageBox.Show("connect with port:" + receivePort.ToString());
            MessageBox.Show("connect with ip:" + ip);
            //string ipName = Dns.GetHostByAddress(ip).HostName;
            if (ip == "127.0.0.1")  // @TODO: later get host by address.
            {
                ip = "localhost";
            }
            myTelnet.Connect(ip, receivePort, sendPort);
        }
        public void Disconnect()
        {
            MessageBox.Show("in Main Model Disconnected");
            myTelnet.Disconnect();
        }
    }
}

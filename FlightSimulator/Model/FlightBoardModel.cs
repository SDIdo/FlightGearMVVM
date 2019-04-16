using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.Model
{
    public class FlightBoardModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ITelnetClient telnetClient;

        public FlightBoardModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.telnetClient.PropertyChanged += this.ConvertInfoLine;
        }

        public void ConvertInfoLine(object sender, PropertyChangedEventArgs e)
        {
            string[] infoArray = e.PropertyName.Split(',');
            string lon = infoArray[1];
            string lat = infoArray[2];
            MessageBox.Show("lon: " + lon + "lat: " + lat); //instead of this.. draw on the flightboard.
            int numLon = 0;
            int numLat = 0;
            Int32.TryParse(lon, out numLon);
            Int32.TryParse(lat, out numLat);
            
            Lon = numLon;
            Lat = numLat;   //Sending to be drawn..
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lon"));
            MessageBox.Show("lon,lat after conversion: " + Lon + ", " + Lat);
        }

        //The properties implementation
        private double lat;
        private double lon;
        
        public double Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }
        public double Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Connect(string ip, int infoPort, int commandPort)
        {
            this.telnetClient.Connect(ip, infoPort, commandPort);
        }

        public void Disconnect()
        {
            this.telnetClient.Disconnect();
        }

        public void Start()
        {
            this.telnetClient.Start();
        }

        public void Write(string command)
        {
            this.telnetClient.Write(command);
        }

    }
}

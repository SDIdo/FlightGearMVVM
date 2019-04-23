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

        NetworkConnection myNetwork;

        //public FlightBoardModel(ImyNetwork inputmyNetwork)
        //{
        //    this.myNetwork = inputmyNetwork;
        //    this.myNetwork.PropertyChanged += this.ConvertInfoLine;
        //}

        public FlightBoardModel(NetworkConnection network)
        {
            this.myNetwork = network;
            this.myNetwork.PropertyChanged += this.ConvertInfoLine;
            //myNetwork.Connect("localhost", 5400, 5402);
        }

        public void ConvertInfoLine(object sender, PropertyChangedEventArgs e)
        {
            string[] infoArray = e.PropertyName.Split(',');
            string lon = infoArray[1];
            string lat = infoArray[2];
            //MessageBox.Show("lon: " + lon + "lat: " + lat); //instead of this.. draw on the flightboard.
            double numLon = 0;
            double numLat = 0;
            Double.TryParse(lon, out numLon);
            Double.TryParse(lat, out numLat);
            
            Lon = numLon;
            Lat = numLat;   //Sending to be drawn..
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
                //MessageBox.Show("Entered set lat from the Model");
                NotifyPropertyChanged("Lat");
            }
        }
        public double Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                //MessageBox.Show("Entered set lon from the Model");
                NotifyPropertyChanged("Lon");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            //MessageBox.Show("In FlightBoard propertyChanged func");
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Disconnect()
        {
            this.myNetwork.Disconnect();
        }

        public void Start()
        {
            this.myNetwork.Start();
        }

        public void Write(string command)
        {
            this.myNetwork.Write(command);
        }

    }
}

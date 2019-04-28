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

/// <summary>
/// model for the grid representing lat-lon flight
/// </summary>
namespace FlightSimulator.Model
{
    public class FlightBoardModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        NetworkConnection myNetwork;
        /// <summary>
        /// ctor for this model
        /// </summary>
        /// <param name="network">a required NetworkConnection class</param>
        public FlightBoardModel(NetworkConnection network)
        {
            this.myNetwork = network;
            this.myNetwork.PropertyChanged += this.LonLatFromData;
        }
        /// <summary>
        /// gets lon and lat from the input from the server
        /// </summary>
        /// <param name="sender">the server</param>
        /// <param name="e">the input</param>
        public void LonLatFromData(object sender, PropertyChangedEventArgs e)
        {
            string msg = e.PropertyName;
            string[] infoArray = e.PropertyName.Split(',');

            string lon = infoArray[0];  //location of lon is 0
            string lat = infoArray[1];  //location of lat is 1
            double numLon = Double.Parse(lon);
            double numLat = Double.Parse(lat);

            Lon = numLon;
            Lat = numLat;
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

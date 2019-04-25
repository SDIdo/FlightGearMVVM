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

        public FlightBoardModel(NetworkConnection network)
        {
            this.myNetwork = network;
            this.myNetwork.PropertyChanged += this.ConvertInfoLine;
        }

        public void ConvertInfoLine(object sender, PropertyChangedEventArgs e)
        {
            string msg = e.PropertyName;
            string[] infoArray = e.PropertyName.Split(',');

            string lon = infoArray[0];
            string lat = infoArray[1];
            //MessageBox.Show("lon: " + lon + "lat: " + lat); //instead of this.. draw on the flightboard.
            double numLon = Double.Parse(lon);
            double numLat = Double.Parse(lat);
            
            Console.WriteLine("Before sending");
            Console.WriteLine("lon is: " + numLon);
            Console.WriteLine("lat is: " + numLat);
            Lon = numLon;
            Lat = numLat;   //Sending to be drawn..
            //string msg = e.PropertyName;
            //Console.WriteLine("$$$$$$$$$$$$$$$$$" + msg);
            //int index = msg.IndexOf(',');
            //string sub = msg.Substring(0, index);
            //Lon = Double.Parse(sub);
            //sub = msg.Substring(index + 1);
            //index = sub.IndexOf(',');
            //sub = sub.Substring(0, index);
            //Lat = Double.Parse(sub);
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

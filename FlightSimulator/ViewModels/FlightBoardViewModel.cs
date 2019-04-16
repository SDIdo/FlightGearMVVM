using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.ViewModels
{

    public class FlightBoardViewModel : INotifyPropertyChanged
    {
        private double lon;
        private double lat;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropetyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public IFlightBoardModel myModel;

        public FlightBoardViewModel(IFlightBoardModel model)    //doesnt even enter to these registered funcs
        {
            myModel = model;
            myModel.PropertyChanged += ModelToView;

        }

        void ModelToView(object sender, PropertyChangedEventArgs e)
        {
            MessageBox.Show("HAHA!!");
            OnPropetyChanged(e.PropertyName);
        }

        
        public double Lon
        {
            set
            {
                this.lon = value;
                OnPropetyChanged("Lon");
            }
            get
            {
                return myModel.Lon;
            }
        }

        public double Lat
        {
            set
            {
                this.lat = value;
                OnPropetyChanged("Lat");
            }
            get
            {
                return myModel.Lat;
            }
        }
    }
}

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
        private FlightBoardModel myModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public FlightBoardViewModel(FlightBoardModel model)    //doesnt even enter to these registered funcs
        {
            myModel = model;
            myModel.PropertyChanged += delegate (object o, PropertyChangedEventArgs e)
            {
                this.PropertyChanged?.Invoke(o, e);
            };
        }


        public double Lon
        {
            get {
                return myModel.Lon;
            }
        }

        public double Lat
        {
            get
            {
                return myModel.Lat;
            }
        }
    }
}

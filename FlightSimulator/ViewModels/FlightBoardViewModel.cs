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

/// <summary>
/// View model for FlightBoard implements the INotifyPropertyChanged
/// </summary>
namespace FlightSimulator.ViewModels
{

    public class FlightBoardViewModel : INotifyPropertyChanged
    {
        private FlightBoardModel myModel;
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// update all who are registered to be notified about a changed property
        /// </summary>
        /// <param name="propName">the changed property name</param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        /// ctor for the view model
        /// </summary>
        /// <param name="model">a required model</param>
        public FlightBoardViewModel(FlightBoardModel model)
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

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

    public class FlightBoardViewModel : Observable
    {
        private IFlightBoardModel model;
        private FlightBoard view;



        public FlightBoardViewModel(IFlightBoardModel model, FlightBoard view)    //doesnt even enter to these registered funcs
        {
            this.model = model;
            this.view = view;

            //model.PropertyChanged += view.Vm_PropertyChanged;

            //model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            //{
            //    this.OnPropetyChanged(e.PropertyName);
            //};
        }
        //public void NotifyPropertyChanged(string propName)
        //{
        //    if (this.PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(propName)); //will go
        //                                                                            //to the plane?
        //}

        public double Lon
        {
            get;
        }

        public double Lat
        {
            get;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{

    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        ObservableDataSource<Point> planeLocations = null;

        public FlightBoard()
        {
            InitializeComponent();
            FlightBoardViewModel flightBoardViewModel = new FlightBoardViewModel(new FlightBoardModel(new MyTelnetClient()));
            flightBoardViewModel.PropertyChanged += Vm_PropertyChanged;

            this.DataContext = flightBoardViewModel;
            //RoutedEventArgs e = new RoutedEventArgs();
            //UserControl_Loaded(this, e);

            //FlightBoardViewModel viewModel = new FlightBoardViewModel(null, null);

            //DataContext = viewModel;
            //Vm_PropertyChanged(this, new PropertyChangedEventArgs("Lon"));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e) //WAS PRIVATE! CHANGE BACK WHEN THINGS SETTLE.
        {
            MessageBox.Show("Has entered VM_PropertyChanged!");

            if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
            {
                //for (int i = 2; i >= 0; --i)
                //{
                //    Point p1 = new Point(1, 1);
                //    planeLocations.AppendAsync(Dispatcher, p1);
                //    p1 = new Point(1, -1);
                //    planeLocations.AppendAsync(Dispatcher, p1);
                //    p1 = new Point(8, 2);
                //    planeLocations.AppendAsync(Dispatcher, p1);
                //}

                double lat = ((FlightBoardViewModel)sender).Lat;
                double lon = ((FlightBoardViewModel)sender).Lon;
                MessageBox.Show("Took Notice!: " + lon + ", " + lat);
                Point p1 = new Point(lat, lon);
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }
    }
}


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
        FlightBoardViewModel myViewModel;
        public FlightBoard()
        {
            InitializeComponent();

            //RoutedEventArgs e = new RoutedEventArgs();
            //UserControl_Loaded(this, e);

            //FlightBoardViewModel viewModel = new FlightBoardViewModel(null, null);

            //DataContext = viewModel;
            //Vm_PropertyChanged(this, new PropertyChangedEventArgs("Lon"));
        }
        public void SetVM(FlightBoardViewModel viewModel)
        {
            myViewModel = viewModel;
            myViewModel.PropertyChanged += Vm_PropertyChanged;
        }

            private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        public void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e) //WAS PRIVATE! CHANGE BACK WHEN THINGS SETTLE.
        {
            //MessageBox.Show("Has entered VM_PropertyChanged!");

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

                //double lat = ((FlightBoardModel)sender).Lat;    //Scat later change
                //double lon = ((FlightBoardModel)sender).Lon;

                var _fbView = sender as FlightBoardModel;
                Console.WriteLine("Took Notice!: " + _fbView.Lon + ", " + _fbView.Lat);
                Point p1 = new Point(_fbView.Lon, _fbView.Lat);
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }
    }
}


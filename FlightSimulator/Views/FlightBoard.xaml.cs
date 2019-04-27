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

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
            {

                var flightBoardView = sender as FlightBoardModel;
                Console.WriteLine("Took Notice!: " + flightBoardView.Lon + ", " + flightBoardView.Lat);
                Point p1 = new Point(flightBoardView.Lat, flightBoardView.Lon);
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }
    }
}


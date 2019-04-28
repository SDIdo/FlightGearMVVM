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
    /// Interaction logic for FlightBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        static readonly int initVal = 0;
        ObservableDataSource<Point> planeLocations = null;
        FlightBoardViewModel myViewModel;
        public FlightBoard()
        {
            InitializeComponent();
        }
        public void SetVM(FlightBoardViewModel viewModel)
        {
            myViewModel = viewModel;
            myViewModel.PropertyChanged += Vm_PropertyChanged;
        }
        /// <summary>
        /// Initialize the grid
        /// </summary>
        /// <param name="sender">initializer</param>
        /// <param name="e">arguments of initializer</param>
            private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
        }
        /// <summary>
        /// Update lat-lon information graphically on the board
        /// </summary>
        /// <param name="sender">initializer</param>
        /// <param name="e">arguments of initializer</param>
        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Point p1 = new Point(initVal, initVal);
            if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
            {
                var flightBoardView = sender as FlightBoardModel;
                p1.X = flightBoardView.Lat;
                p1.Y = flightBoardView.Lon;
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }
    }
}


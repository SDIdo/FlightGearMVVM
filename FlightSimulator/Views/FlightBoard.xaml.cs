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

        FlightBoardViewModel vm;
        public FlightBoard()
        {
            InitializeComponent();
            //RoutedEventArgs e = new RoutedEventArgs();
            //UserControl_Loaded(this, e);

            vm = new FlightBoardViewModel(new FlightBoardModel(new MyTelnetClient()));


            vm.PropertyChanged += Vm_PropertyChanged;      //register to an event
            DataContext = vm;

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
            
            //if(e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
            //{
                MessageBox.Show("Has entered VM_Property");
                for (int i = 2; i >= 0; --i)
                {
                    Point p1 = new Point(1, 1);            // Fill here!
                    planeLocations.AppendAsync(Dispatcher, p1);
                    p1 = new Point(1, -1);            // Fill here!
                    planeLocations.AppendAsync(Dispatcher, p1);
                    p1 = new Point(8, 2);            // Fill here!
                    planeLocations.AppendAsync(Dispatcher, p1);
                }
                //double lat = ((FlightBoardModel)sender).Lat;    //Should be taken from the view Model! Change when settle
                //double lon = ((FlightBoardModel)sender).Lon;
                //MessageBox.Show("Took Notice!: " + lon + ", " + lat);
                //Point p1 = new Point(lat, lon);
                //planeLocations.AppendAsync(Dispatcher, p1);
            }
        }
    }
//}


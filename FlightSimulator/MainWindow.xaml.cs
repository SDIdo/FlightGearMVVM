using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using FlightSimulator.Properties;

using FlightSimulator.ViewModels.Windows;
using FlightSimulator.Model;
using FlightSimulator.ViewModels;

namespace FlightSimulator
{
    /// <summary>
    /// Initialize components and main classes. 
    /// Has also a registration to the closing event for nice exiting upon x click.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NetworkConnection generalTelnet = new NetworkConnection();
            MainWindowViewModel myViewModel = new MainWindowViewModel(new MainWindowModel(generalTelnet));
            this.DataContext = myViewModel;

            FlightBoardViewModel flightBoardViewModel = new FlightBoardViewModel(new FlightBoardModel(generalTelnet));
            this.flightBoardView.SetVM(flightBoardViewModel);
            this.flightBoardView.DataContext = flightBoardViewModel;

            CommandCenterUCVM ccucvm = new CommandCenterUCVM(new CommandCenterUCModel(generalTelnet));
            this.commandCenterUCView.SetVM(ccucvm);
        }
    }
}

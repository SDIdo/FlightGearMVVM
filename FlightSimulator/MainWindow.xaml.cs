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

using FlightSimulator.ViewModels.Windows;
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using FlightSimulator.Views;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static MyTelnetClient telnet = new MyTelnetClient();
        FlightBoardModel flightBoardModel = new FlightBoardModel(telnet);   //later improve style
        SettingsWindowViewModel settingsVM;
        


        public MainWindow()
        {
            InitializeComponent();  //Starts each view class 
            SettingsWindowViewModel settingsVM = new SettingsWindowViewModel(new ApplicationSettingsModel());
            //joystick.DataContext = joystickVM;

            //joystickVM = new JoystickViewModel(new JoystickModel(telnet));
            //FlightBoardViewModel fbvm = new FlightBoardViewModel(flightBoardModel);
            //fbvm.Lon = 3;




            ////SCAT
            //MyTelnetClient telNetClient = new MyTelnetClient();
            //FlightBoardModel flightboardmodel = new FlightBoardModel(telNetClient);
            //FlightBoardViewModel flightboardviewmodel = new FlightBoardViewModel(flightboardmodel);
            //FlightBoard flightboard = new FlightBoard();    //won't effect anything.. the view initializes
            //                                                //those classes to itself

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Views.Windows.Settings settings = new Views.Windows.Settings();
            settings.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            flightBoardModel.connect("127.0.0.1", 5402);    //scatting connection
        }

        private void Get_Click(object sender, RoutedEventArgs e)
        {
            flightBoardModel.receive("127.0.0.1", 5402);
        }
    }
}

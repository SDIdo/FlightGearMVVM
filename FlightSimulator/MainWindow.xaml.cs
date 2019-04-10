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


namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingsWindowViewModel settingsVM;
        public MainWindow()
        {
            InitializeComponent();
            settingsVM = new SettingsWindowViewModel(new ApplicationSettingsModel());
            DataContext = this;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Views.Windows.Settings settings = new Views.Windows.Settings();
            int x = Properties.Settings.Default.FlightCommandPort;  //5100
            int y = Properties.Settings.Default.FlightInfoPort;     //5200
            string z = Properties.Settings.Default.FlightServerIP;  //127.0.0.1

            MessageBox.Show(z);
            MessageBox.Show(x.ToString());
            MessageBox.Show(y.ToString());
            settings.ShowDialog();
        }
    }
}

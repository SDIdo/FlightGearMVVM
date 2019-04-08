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

namespace FlightSimulator.Views.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.Windows.SettingsWindowViewModel(new Model.ApplicationSettingsModel());    //
            //FlightServerIP.Text = "127";
            //FlightInfoPort.Text = "225";
            //FlightCommandPort.Text = "777";
        }
        //private void ApplyButton_Click(object sender, RoutedEventArgs e)
        //{

        //    ViewModels.Windows.SettingsWindowViewModel settingsVM = new ViewModels.Windows.SettingsWindowViewModel(new Model.ApplicationSettingsModel());
        //    //settingsVM.NotifyPropertyChanged("FlightServerIP");
        //    settingsVM.FlightServerIP = FlightServerIP.Text;
        //    MessageBox.Show($"The description is : {FlightServerIP.Text}");
        //}
    }
}

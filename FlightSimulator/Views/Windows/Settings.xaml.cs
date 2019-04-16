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
            InitializeComponent();      //They all need to be of the same Model.. somehow to insert it into settings
            ViewModels.Windows.SettingsWindowViewModel vm = new ViewModels.Windows.SettingsWindowViewModel(new Model.ApplicationSettingsModel());    //
            this.DataContext = vm;
            vm.PropertyChanged += Test;
        }

        void Test(object a, PropertyChangedEventArgs e)
        {
            MessageBox.Show("Hello");
        }
    }
}

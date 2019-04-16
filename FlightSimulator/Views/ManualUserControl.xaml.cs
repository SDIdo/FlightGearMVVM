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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for ManualUserControl.xaml
    /// </summary>
    public partial class ManualUserControl : UserControl
    {
        public ManualUserControl()
        {
            InitializeComponent();
            this.DataContext = JoystickView; // Data context is the Joystick in the view.
        }

        private void RudderSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RudderValueText.Text = Math.Round(RudderSlider.Value, 3).ToString();
        }

        private void ThrottleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ThrottleValueText.Text = Math.Round(ThrottleSlider.Value, 3).ToString();
        }
    }
}

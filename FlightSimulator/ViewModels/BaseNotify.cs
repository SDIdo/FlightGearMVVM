using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Implementation of INotify with changed property parameter as a string
/// </summary>
namespace FlightSimulator.ViewModels
{
    public abstract class BaseNotify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// update all who are registered to be notified about a changed property
        /// </summary>
        /// <param name="propName">the changed property name</param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

}

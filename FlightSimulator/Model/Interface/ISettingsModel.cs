using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Settings containers must implement this interface
/// </summary>
namespace FlightSimulator.Model.Interface
{
    public interface ISettingsModel
    {
        string FlightServerIP { get; set; }          // The IP Of the Flight Server
        int FlightInfoPort { get; set; }           // The Port of the Flight Server
        int FlightCommandPort { get; set; }           // The Port of the Flight Server
        /// <summary>
        /// Save the settings
        /// </summary>
        void SaveSettings();
        /// <summary>
        /// reload the settings
        /// </summary>
        void ReloadSettings();
    }
}

using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Contains and responsible for the application settings
/// </summary>
namespace FlightSimulator.Model
{
    public class ApplicationSettingsModel : ISettingsModel
    {
        #region Singleton
        private static ISettingsModel m_Instance = null;
        /// <summary>
        /// ctor for this settings container
        /// </summary>
        public static ISettingsModel Instance
        {
            get
            {
                if(m_Instance == null)
                {
                    m_Instance = new ApplicationSettingsModel();
                }
                return m_Instance;
            }
        }
        #endregion
        public string FlightServerIP
        {
            get { return Properties.Settings.Default.FlightServerIP; }
            set { Properties.Settings.Default.FlightServerIP = value; }
        }
        public int FlightCommandPort
        {
            get { return Properties.Settings.Default.FlightCommandPort; }
            set { Properties.Settings.Default.FlightCommandPort = value; }
        }

        public int FlightInfoPort
        {
            get { return Properties.Settings.Default.FlightInfoPort; }
            set { Properties.Settings.Default.FlightInfoPort = value; }
        }
        /// <summary>
        /// saves the current values
        /// </summary>
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// reloads the values
        /// </summary>
        public void ReloadSettings()
        {
            Properties.Settings.Default.Reload();
        }
    }
}

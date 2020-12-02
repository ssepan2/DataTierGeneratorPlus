using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Ssepan.Utility;

namespace DataTierGeneratorPlus
{
	/// <summary>
    /// The manager for the persisted Settings. This is a sub-component of the Model.
	/// </summary>
    public static class SettingsController 
    {
        #region declarations
        private static Settings _Settings;
        private static String _Filename;
        private static Boolean _SkipSettingsCheck;
        #endregion declarations

        #region constructors
        //init static members
        static SettingsController()
        {
            New();
        }

        #endregion constructors

        #region PropertyChanged handlers
        static void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(String.Format("Settings.{0} was changed: ", e.PropertyName /*, (sender as Settings).SomeProperty*/));
                GeneratorController.Refresh();

            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
        }
        #endregion PropertyChanged handlers

        #region Properties
        public static Settings ModelSettings
        {
            get { return _Settings; }
            set
            {
                if (_Settings != null)
                {
                    _Settings.PropertyChanged -= new PropertyChangedEventHandler(Settings_PropertyChanged);
                }
                _Settings = value;
                if (_Settings != null)
                {
                    _Settings.PropertyChanged += new PropertyChangedEventHandler(Settings_PropertyChanged);
                }
            }
        }

        public static String Filename
        {
            get { return _Filename; }
            set { _Filename = value; }
        }

        public static Boolean Dirty
        {
            get 
            {
                //skip if form initializing
                if (!_SkipSettingsCheck)
                {
                    return ModelSettings.Dirty;
                }
                else
                {
                    return false;
                }
            }
            //set { _Dirty = value; }
        }

        public static Boolean SkipSettingsCheck
        {
            get { return _SkipSettingsCheck; }
            set { _SkipSettingsCheck = value; }
        }
        #endregion Properties

        #region static methods
        #region SettingIO
        /// <summary>
        /// New settings
        /// </summary>
        /// <returns></returns>
        public static Boolean New()
        {
            Boolean returnValue = default(Boolean);
            try
            {
                //create new object
                ModelSettings = new Settings();
                Filename = Settings.FILE_NEW;

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                //throw ex;
            }
            return returnValue;
        }

        /// <summary>
        /// Open settings.
        /// </summary>
        /// <returns></returns>
        public static Boolean Open()
        {
            Boolean returnValue = default(Boolean);

            try
            {
                //read from XML file
                Settings.LoadXml(ModelSettings, Filename);

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }

            return returnValue;
        }

        /// <summary>
        /// Save settings.
        /// </summary>
        /// <returns></returns>
        public static Boolean Save()
        {
            Boolean returnValue = default(Boolean);

            try
            {
                //write to XML file
                Settings.PersistXml(ModelSettings, Filename);

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }
             
            return returnValue;
        }
        #endregion SettingIO
        #endregion

    }
}

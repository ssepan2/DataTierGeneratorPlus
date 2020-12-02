using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Ssepan.Application;
using Ssepan.Utility;

namespace DataTierGeneratorPlusLibrary
{
	/// <summary>
    /// The manager for the persisted Settings. This is a sub-component of the Controller.
	/// </summary>
    public class SettingsController :
        SettingsControllerBase
    {
        #region constructors
        /// <summary>
        /// Init AsStatic properties
        /// </summary>
        static SettingsController()
        {
            try
            {
                AsStatic = new SettingsController();
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        #endregion constructors

        #region Properties
        public new static SettingsController AsStatic
        {
            get { return SettingsControllerBase.AsStatic as SettingsController; }
            set
            {
                SettingsControllerBase.AsStatic = value;
            }
        }
        #endregion Properties

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Ssepan.Application.WinForms;
using Ssepan.Utility;

namespace DataTierGeneratorPlus
{
    public class GeneratorController
    {
        public static GeneratorController controller;

        static GeneratorController()
        {
            controller = new GeneratorController();
        }

        /// <summary>
        /// New settings
        /// </summary>
        public static void New()
        {
            try
            {
                if (SettingsController.New())
                {
                    Refresh();
                }
                else
                {
                    throw new ApplicationException(String.Format("Unable to get New Settings.\r\nPath: {0}", SettingsController.Filename));
                }
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
        }

        /// <summary>
        /// Open settings
        /// </summary>
        public static void Open()
        {
            try
            {
                if (SettingsController.Open())
                {
                    Refresh();
                }
                else
                {
                    throw new ApplicationException(String.Format("Unable to open Settings.\r\nPath: {0}", SettingsController.Filename));
                }
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
        }

        /// <summary>
        /// Save settings
        /// </summary>
        public static void Save()
        {
            try
            {
                if (SettingsController.Save())
                {
                    Refresh();
                }
                else
                {
                    throw new ApplicationException(String.Format("Unable to save Settings.\r\nPath: {0}", SettingsController.Filename));
                }
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
        }

        /// <summary>
        /// The Controller method for assigning a complete Settings object.
        /// </summary>
        public static void Refresh()
        {
            try
            {
                //calculate changes
                //n/a

                //notify observers
                GeneratorModel.model.OnNotify(new EventArgs());
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
        }

        /// <summary>
        /// The Controller method for assigning  viewer size.
        /// </summary>
        public static void SetViewerSize(Size objSize)
        {
            try
            {
                SettingsController.ModelSettings.Size = objSize;
                Refresh();
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
        }

        /// <summary>
        /// The Controller method for assigning  viewer location.
        /// </summary>
        public static void SetViewerLocation(Point objLocation)
        {
            try
            {
                SettingsController.ModelSettings.Location = objLocation;
                Refresh();
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
        }

        /// <summary>
        /// Display property grid dialog. Changes to properties are reflected immediately.
        /// </summary>
        public static void ShowProperties()
        {
            try
            {
                //PropertiesViewer pv = new PropertiesViewer(SettingsController.ModelSettings, Refresh);
                PropertyDialog pv = new PropertyDialog(SettingsController.ModelSettings, Refresh);
#if debug
                pv.Show();//dialog properties grid validation does refresh
#else
                pv.ShowDialog();//dialog properties grid validation does refresh
                pv.Dispose();
#endif
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
        }

        /// <summary>
        /// Check registry for file association; if missing and allowed, register file type.
        /// </summary>
        public static Boolean ValidateFileAssociation()
        {
            Boolean returnValue = default(Boolean);

            try
            {
                //TODO:instantiate generator as property of Program
                returnValue = GeneratorModel.model.IsFileAssociation(Application.ExecutablePath, Settings.FILE_TYPE_EXTENSION, false);//TODO:..., ..., SettingsController.ModelSettings.AllowRegisterFileAssociation
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }

            return returnValue;
        }
    }
}

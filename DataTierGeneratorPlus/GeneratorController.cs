using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Ssepan.Application;
using Ssepan.Application.WinForms;
using Ssepan.Utility;

namespace DataTierGeneratorPlus
{
    public class GeneratorController :
        ModelControllerBase
    {
        #region constructors
        static GeneratorController()
        {
            try
            {
                AsStatic = new GeneratorController();

            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw;
            }
        }
        #endregion constructors

        #region Properties
        public new static GeneratorController AsStatic
        {
            get { return ModelControllerBase.AsStatic as GeneratorController; }
            set
            {
                ModelControllerBase.AsStatic = value;
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// The Controller method for triggering update notifications to the model.
        /// Method must override so that sub class can perform it's own method, 
        /// and call base method when it is done.
        /// </summary>
        public override void Refresh()
        {
            try
            {
                //calculate changes
                //n/a

                //call base to trigger OnNotify
                base.Refresh();
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw;
            }
        }

        /// <summary>
        /// The Controller method for assigning  viewer size.
        /// </summary>
        public void SetViewerSize(Size objSize)
        {
            try
            {
                Settings.AsStatic.Size = objSize;
                Refresh();
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw;
            }
        }

        /// <summary>
        /// The Controller method for assigning  viewer location.
        /// </summary>
        public void SetViewerLocation(Point objLocation)
        {
            try
            {
                Settings.AsStatic.Location = objLocation;
                Refresh();
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw;
            }
        }
        #endregion Methods
    }
}

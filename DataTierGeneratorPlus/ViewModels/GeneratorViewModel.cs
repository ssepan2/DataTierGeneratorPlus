using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DataTierGeneratorPlusLibrary;
using DataTierGeneratorPlusLibrary.MVC;
using Ssepan.Application;
using Ssepan.Application.WinForms;
using Ssepan.Io;
using Ssepan.Utility;

namespace DataTierGeneratorPlus
{
    /// <summary>
    /// Note: it is not necessary to make this subclass take type parameters.
    /// </summary>
    public class GeneratorViewModel :
        FormsViewModel<Bitmap, GeneratorSettings, GeneratorModel, GeneratorViewer>
    {
        #region Declarations
        protected static FileDialogInfo _dataFileDialogInfo = default(FileDialogInfo);

        #region Commands
        //public ICommand FileNewCommand { get; private set; }
        //public ICommand FileOpenCommand { get; private set; }
        //public ICommand FileSaveCommand { get; private set; }
        //public ICommand FileSaveAsCommand { get; private set; }
        //public ICommand FilePrintCommand { get; private set; }
        //public ICommand FileExitCommand { get; private set; }
        //public ICommand EditCopyToClipboardCommand { get; private set; }
        //public ICommand EditPropertiesCommand { get; private set; }
        //public ICommand ViewPreviousMonthCommand { get; private set; }
        //public ICommand ViewPreviousWeekCommand { get; private set; }
        //public ICommand ViewNextWeekCommand { get; private set; }
        //public ICommand ViewNextMonthCommand { get; private set; }
        //public ICommand HelpAboutCommand { get; private set; }
        #endregion Commands
        #endregion Declarations

        #region Constructors
        public GeneratorViewModel() { }//Note: not called, but need to be present to compile--SJS

        public GeneratorViewModel
        (
            PropertyChangedEventHandler propertyChangedEventHandlerDelegate,
            Dictionary<String, Bitmap> actionIconImages,
            FileDialogInfo settingsFileDialogInfo,
            FileDialogInfo dataFileDialogInfo //this param must be assigned in this ctor
        ) :
            base(propertyChangedEventHandlerDelegate, actionIconImages, settingsFileDialogInfo)
        {
            try
            {
                _dataFileDialogInfo = dataFileDialogInfo;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyChangedEventHandlerDelegate"></param>
        /// <param name="actionIconImages"></param>
        /// <param name="settingsFileDialogInfo"></param>
        /// <param name="view"></param>
        public GeneratorViewModel
        (
            PropertyChangedEventHandler propertyChangedEventHandlerDelegate,
            Dictionary<String, Bitmap> actionIconImages,
            FileDialogInfo settingsFileDialogInfo,
            FileDialogInfo dataFileDialogInfo, //this param must be assigned in this ctor
            GeneratorViewer view
        ) :
            base(propertyChangedEventHandlerDelegate, actionIconImages, settingsFileDialogInfo, view)
        {
            try
            {
                _dataFileDialogInfo = dataFileDialogInfo;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }
        #endregion Constructors

        #region Properties
        private Boolean _IsLoadInvalidated = true;
        public Boolean IsLoadInvalidated
        {
            get { return _IsLoadInvalidated; }
            set 
            { 
                _IsLoadInvalidated = value; 
            }
        }
        private Boolean _PreviousEnabledStateLoad = default(Boolean);
        public Boolean PreviousEnabledStateLoad
        {
            get { return _PreviousEnabledStateLoad; }
            set { _PreviousEnabledStateLoad = value; }
        }

        private Boolean _PreviousEnabledStateGenerate = default(Boolean);
        public Boolean PreviousEnabledStateGenerate
        {
            get { return _PreviousEnabledStateGenerate; }
            set { _PreviousEnabledStateGenerate = value; }
        }
        #endregion Properties

        #region Methods
        ///// <summary>
        ///// model specific, not generioc
        ///// </summary>
        //public void DoSomething()
        //{
        //    StatusMessage = String.Empty;
        //    ErrorMessage = String.Empty;

        //    try
        //    {
        //        StartProgressBar
        //        (
        //            "Doing something...",
        //            null,
        //            null, //_actionIconImages["Xxx"],
        //            true,
        //            33
        //        );

        //        ModelController<MVCModel>.Model.SomeBoolean = !ModelController<MVCModel>.Model.SomeBoolean;
        //        ModelController<MVCModel>.Model.SomeInt += 1;
        //        ModelController<MVCModel>.Model.SomeString = DateTime.Now.ToString();

        //        //SettingsController<MVCSettings>.Settings.SomeBoolean = true;
        //        //SettingsController<MVCSettings>.Settings.SomeInt += 1;
        //        //SettingsController<MVCSettings>.Settings.SomeString = "test";

        //        ModelController<MVCModel>.Model.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);

        //        StopProgressBar(null, String.Format("{0}", ex.Message));
        //    }
        //    finally
        //    {
        //        StopProgressBar("Did something.");
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        internal void LoadTables
        (
            BackgroundWorker worker
        )
        {
            try
            {
                StartProgressBar("Loading...", null, View.menuEditLoad.Image as Bitmap, false, 0);

                //save buttons
                ButtonsEnabled(false);

                //// Declare Tuple object to pass multiple params to DoWork method.
                //var arguments =
                //    Tuple.Create<SomeType /*someValue*/>
                //    (
                //        tables
                //    );

                //Load table metadata
                worker.RunWorkerAsync(/*arguments*/);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                StopProgressBar(null, String.Format("{0}", ex.Message));

                IsLoadInvalidated = true;
                ButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="tables"></param>
        internal void GenerateFromTables
        (
            BackgroundWorker worker,
            List<Table> tables
        )
        {
            try
            {
                StartProgressBar("Generating...", null, View.menuEditGenerate.Image as Bitmap, false, 0);

                //save buttons
                ButtonsEnabled(false);

                // Declare Tuple object to pass multiple params to DoWork method.
                var arguments =
                    Tuple.Create<List<Table> /*tableList*/>
                    (
                        tables
                    );

                // Generate the SQL and .Net code
                worker.RunWorkerAsync(arguments);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                StopProgressBar(null, String.Format("{0}", ex.Message));

                ButtonsEnabled(true);
            }
        }

        internal void OutputPathFolderSelect()
        {
            StatusMessage = String.Empty;
            ErrorMessage = String.Empty;

            try
            {
                StartProgressBar
                (
                    "Selecting output path...",
                    null,
                    _actionIconImages["Save"],
                    true,
                    33
                );
                _dataFileDialogInfo.Filename = View.OutputPathTextBox.Text;
                if (FileDialogInfo.GetPathForSave(_dataFileDialogInfo))
                {
                    View.OutputPathTextBox.Text = _dataFileDialogInfo.Filename;

                    StopProgressBar("Output path selection completed.");
                }
                else
                {
                    StopProgressBar("Output path selection cancelled.");
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);

                StopProgressBar(null, String.Format("{0}", ex.Message));
            }
        }

        /// <summary>
        /// button state.
        /// </summary>
        internal void ButtonsEnabled
        (
            Boolean enabledFlag
        )
        {
            try
            {
                PreviousEnabledStateLoad = ButtonEnabled(enabledFlag, PreviousEnabledStateLoad, View.loadButton, View.menuEditLoad, View.tsbEditLoad);
                PreviousEnabledStateGenerate = ButtonEnabled(enabledFlag, PreviousEnabledStateGenerate, View.generateButton, View.menuEditGenerate, View.tsbEditGenerate);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }
        #endregion Methods
    }
}

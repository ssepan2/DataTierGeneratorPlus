//#define USE_CONFIG_FILEPATH

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DataTierGeneratorPlusLibrary;
using Ssepan.Application;
using Ssepan.Application.WinForms;
using Ssepan.DataBinding;
using Ssepan.Io;
using Ssepan.Utility;

namespace DataTierGeneratorPlus
{
    /// <summary>
    /// This is the View.
    /// Form used to collect the connection information for the code we're going to generate.
    /// </summary>
    public partial class GeneratorViewer :
        Form,
        INotifyPropertyChanged
    {
        #region Declarations
        protected Boolean disposed;

        private Boolean _ValueChangedProgrammatically;

        //cancellation hook
        Action cancelDelegate = null;
        protected GeneratorViewModel ViewModel = default(GeneratorViewModel);

        //private Settings model;
        //private Boolean IsLoadInvalidated = true;
        //use state field to remember, because buttons may use different criteria to determine when they are enabled, 
        //and one may be enabled while the other is not. Also, they need to be remembered during background processing and used when finished.
        //private Boolean PreviousEnabledStateLoad = false;
        //private Boolean PreviousEnabledStateGenerate = false;
        //private static FileDialogInfo settingsFileDialogInfo = default(FileDialogInfo);
        #endregion Declarations

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public GeneratorViewer(String[] args) 
        {
            try
            {
                InitializeComponent();

                ////(re)define default output delegate
                //ConsoleApplication.defaultOutputDelegate = ConsoleApplication.messageBoxWrapperOutputDelegate;

                //subscribe to notifications
                this.PropertyChanged += ModelPropertyChangedEventHandlerDelegate;

                InitViewModel();

                BindSizeAndLocation();
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }
        #endregion Constructors

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            try
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);

                throw;
            }
        }
        #endregion INotifyPropertyChanged

        #region PropertyChangedEventHandlerDelegate
        /// <summary>
        /// Note: property changes update UI manually.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModelPropertyChangedEventHandlerDelegate
        (
            Object sender,
            PropertyChangedEventArgs e
        )
        {
            try
            {
                if (e.PropertyName == "IsChanged")
                {
                    //ConsoleApplication.defaultOutputDelegate(String.Format("{0}", e.PropertyName));
                    ApplySettings();
                }
                //Status Bar
                else if (e.PropertyName == "ActionIconIsVisible")
                {
                    StatusBarActionIcon.Visible = (ViewModel.ActionIconIsVisible);
                }
                else if (e.PropertyName == "ActionIconImage")
                {
                    StatusBarActionIcon.Image = (ViewModel != null ? ViewModel.ActionIconImage : (Image)null);
                }
                else if (e.PropertyName == "StatusMessage")
                {
                    //replace default action by setting control property
                    StatusBarStatusMessage.Text = ViewModel.StatusMessage;
                    //e = new PropertyChangedEventArgs(e.PropertyName + ".handled");

                    //ConsoleApplication.defaultOutputDelegate(String.Format("{0}", StatusMessage));
                }
                else if (e.PropertyName == "ErrorMessage")
                {
                    //replace default action by setting control property
                    StatusBarErrorMessage.Text = ViewModel.ErrorMessage;
                    //e = new PropertyChangedEventArgs(e.PropertyName + ".handled");

                    //ConsoleApplication.defaultOutputDelegate(String.Format("{0}", ErrorMessage));
                }
                else if (e.PropertyName == "ErrorMessageToolTipText")
                {
                    StatusBarErrorMessage.ToolTipText = ViewModel.ErrorMessageToolTipText;
                }
                else if (e.PropertyName == "ProgressBarValue")
                {
                    StatusBarProgressBar.Value = ViewModel.ProgressBarValue;
                }
                else if (e.PropertyName == "ProgressBarMaximum")
                {
                    StatusBarProgressBar.Maximum = ViewModel.ProgressBarMaximum;
                }
                else if (e.PropertyName == "ProgressBarMinimum")
                {
                    StatusBarProgressBar.Minimum = ViewModel.ProgressBarMinimum;
                }
                else if (e.PropertyName == "ProgressBarStep")
                {
                    StatusBarProgressBar.Step = ViewModel.ProgressBarStep;
                }
                else if (e.PropertyName == "ProgressBarIsMarquee")
                {
                    StatusBarProgressBar.Style = (ViewModel.ProgressBarIsMarquee ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks);
                }
                else if (e.PropertyName == "ProgressBarIsVisible")
                {
                    StatusBarProgressBar.Visible = (ViewModel.ProgressBarIsVisible);
                }
                else if (e.PropertyName == "DirtyIconIsVisible")
                {
                    StatusBarDirtyMessage.Visible = (ViewModel.DirtyIconIsVisible);
                }
                else if (e.PropertyName == "DirtyIconImage")
                {
                    StatusBarDirtyMessage.Image = ViewModel.DirtyIconImage;
                }
                //model bindings -- additional actions besides two-way change notifications
                else if (e.PropertyName == "Server")
                {
                    ViewModel.IsLoadInvalidated = true;

                    ValidateButtons();
                }
                else if (e.PropertyName == "Database")
                {
                    ViewModel.IsLoadInvalidated = true;

                    ValidateButtons();
                }
                else if (e.PropertyName == "SQLAuthentication")
                {
                    if (GeneratorController<GeneratorModel>.Model.SQLAuthentication)
                    {
                        lblWindowsAuthentication.Enabled = false;
                        lblSqlAuthentication.Enabled = true;
                        loginNameLabel.Enabled = true;
                        loginNameTextBox.Enabled = true;
                        loginNameTextBox.BackColor = SystemColors.Window;

                        passwordLabel.Enabled = true;
                        passwordTextBox.Enabled = true;
                        passwordTextBox.BackColor = SystemColors.Window;
                    }
                    else
                    {
                        lblWindowsAuthentication.Enabled = true;
                        lblSqlAuthentication.Enabled = false;
                        loginNameLabel.Enabled = false;
                        loginNameTextBox.Enabled = false;
                        loginNameTextBox.BackColor = SystemColors.InactiveBorder;

                        passwordLabel.Enabled = false;
                        passwordTextBox.Enabled = false;
                        passwordTextBox.BackColor = SystemColors.InactiveBorder;
                    }

                    ViewModel.IsLoadInvalidated = true;

                    ValidateButtons();
                }
                else if (e.PropertyName == "SQLLogin")
                {
                    ViewModel.IsLoadInvalidated = true;

                    ValidateButtons();
                }
                else if (e.PropertyName == "Password")
                {
                    ViewModel.IsLoadInvalidated = true;

                    ValidateButtons();
                }
                else if (e.PropertyName == "GrantUser")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "SPPrefix")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "CreateMultipleFiles")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "Namespace")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "ClassSuffix")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "Language")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "AutoSelectOnLoad")
                {
                    ViewModel.IsLoadInvalidated = true;

                    ValidateButtons();
                }
                else if (e.PropertyName == "FetchTableDetailsWithLoad")
                {
                    ViewModel.IsLoadInvalidated = true;

                    ValidateButtons();
                }
                else if (e.PropertyName == "DBMS")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "SQLHelper")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "OutputPath")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "GenerateCustomClassTemplate")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "GenerateDataLayerClasses")
                {
                    this.csGroupBox.Enabled = GeneratorController<GeneratorModel>.Model.GenerateDataLayerClasses;

                    ValidateButtons();
                }
                else if (e.PropertyName == "GenerateStoredProcedures")
                {
                    this.sqlGroupBox.Enabled = GeneratorController<GeneratorModel>.Model.GenerateStoredProcedures;

                    ValidateButtons();
                }
                else if (e.PropertyName == "GenerateWcfLayerClasses")
                {
                    this.wcfGroupBox.Enabled = GeneratorController<GeneratorModel>.Model.GenerateWcfLayerClasses; 

                    ValidateButtons();
                }
                else if (e.PropertyName == "GenerateWcfLayerServerComponents")
                {
                    ValidateButtons();
                }
                else if (e.PropertyName == "GenerateWcfLayerClientHelpers")
                {
                    ValidateButtons();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Note: handle settings property changes manually.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SettingsPropertyChangedEventHandlerDelegate
        (
            Object sender,
            PropertyChangedEventArgs e
        )
        {
            try
            {
                if (e.PropertyName == "Dirty")
                {
                    //apply settings that don't have databindings
                    ViewModel.DirtyIconIsVisible = (SettingsController<GeneratorSettings>.Settings.Dirty);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }
        #endregion PropertyChangedEventHandlerDelegate

        #region Properties
        private String _ViewName = Program.APP_NAME;
        public String ViewName
        {
            get { return _ViewName; }
            set
            {
                _ViewName = value;
                OnPropertyChanged("ViewName");
            }
        }
        #endregion Properties

        #region Events
        #region Form Events
        /// <summary>
        /// Process Form key presses.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override Boolean ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Boolean returnValue = default(Boolean);
            try
            {
                // Implement the Escape / Cancel keystroke
                if (keyData == Keys.Cancel || keyData == Keys.Escape)
                {
                    //if a long-running cancellable-action has registered 
                    //an escapable-event, trigger it
                    InvokeActionCancel();

                    // This keystroke was handled, 
                    //don't pass to the control with the focus
                    returnValue = true;
                }
                else
                {
                    returnValue = base.ProcessCmdKey(ref msg, keyData);
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
            return returnValue;
        }

        private void GeneratorViewer_Load(Object sender, System.EventArgs e)
        {
            try
            {
                ViewModel.StatusMessage = String.Format("{0} starting...", ViewName);

                ViewModel.StatusMessage = String.Format("{0} started.", ViewName);

                _Run();
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                ViewModel.StatusMessage = String.Empty;

                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }

        private void GeneratorViewer_FormClosing(Object sender, FormClosingEventArgs e)
        {
            try
            {
                ViewModel.StatusMessage = String.Format("{0} completing...", ViewName);

                DisposeSettings();

                ViewModel.StatusMessage = String.Format("{0} completed.", ViewName);
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message.ToString();
                ViewModel.StatusMessage = "";

                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
            finally
            {
                ViewModel = null;
            }
        }

        /// <summary>
        /// form KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneratorViewer_KeyDown(Object sender, KeyEventArgs e)
        {
            String statusMessage = String.Empty;
            String errorMessage = default(String);

            try
            {
                //use Esc to Cancel and active background processes.
                //if (e.KeyValue == Convert.ToChar(27))
                if (e.KeyCode == Keys.Escape)
                {
                    if (this.backgroundWorkerGenerate.IsBusy)
                    {
                        backgroundWorkerGenerate.CancelAsync();
                    }
                    if (this.backgroundWorkerLoad.IsBusy)
                    {
                        backgroundWorkerLoad.CancelAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);

                ViewModel.StopProgressBar(null, String.Format("{0}", ex.Message));
            }
            finally
            {
                ViewModel.StopProgressBar(statusMessage, errorMessage);
            }
        }

        #endregion Form Events

        #region Menu Events
        private void menuFileNew_Click(Object sender, EventArgs e)
        {
            ViewModel.FileNew();
        }

        private void menuFileOpen_Click(Object sender, EventArgs e)
        {
            ViewModel.FileOpen();
        }

        private void menuFileSave_Click(Object sender, EventArgs e)
        {
            ViewModel.FileSave();
        }

        private void menuFileSaveAs_Click(Object sender, EventArgs e)
        {
            ViewModel.FileSaveAs();
        }

        private void menuFileExit_Click(Object sender, EventArgs e)
        {
            ViewModel.FileExit();
        }

        private void menuEditProperties_Click(Object sender, EventArgs e)
        {
            ViewModel.EditProperties();
        }

        private void menuEditCopyToClipboard_Click(Object sender, EventArgs e)
        {
            ViewModel.EditCopy();
        }

        private void menuHelpAbout_Click(Object sender, EventArgs e)
        {
            ViewModel.HelpAbout<AssemblyInfo>();
        }

        #endregion MenuEvents

        #region Control Events

        /// <summary>
        /// output folder button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputPathFolderSelectButton_Click(Object sender, EventArgs e)
        {
            ViewModel.OutputPathFolderSelect();
        }

        #region Settings Events

        #endregion Settings Events

        #region LoadButton

        /// <summary>
        /// ValidateButtons method.
        /// </summary>
        private void ValidateButtons()
        {
            try
            {
                ValidateLoadButton();
                ValidateGenerateButton();

            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        /// <summary>
        /// ValidateLoadButton method.
        /// </summary>
        private void ValidateLoadButton()
        {
            try
            {
                Boolean buttonEnabledFlag = true;

                if (serverTextBox.Text.Length == 0)
                {
                    this.errorProvider1.SetError(this.serverTextBox, "Server is required.");
                    buttonEnabledFlag = false;
                }
                else
                {
                    this.errorProvider1.SetError(this.serverTextBox, "");
                }

                if (databaseTextBox.Text.Length == 0)
                {
                    this.errorProvider1.SetError(this.databaseTextBox, "Database is required.");
                    buttonEnabledFlag = false;
                }
                else
                {
                    this.errorProvider1.SetError(this.databaseTextBox, "");
                }

                if (sqlServerAuthenticationRadioButton.Checked)
                {
                    if (loginNameTextBox.Text.Length == 0)
                    {
                        this.errorProvider1.SetError(this.loginNameTextBox, "Login is required.");
                        buttonEnabledFlag = false;
                    }
                    else
                    {
                        this.errorProvider1.SetError(this.loginNameTextBox, "");
                    }

                    if (passwordTextBox.Text.Length == 0)
                    {
                        this.errorProvider1.SetError(this.passwordTextBox, "Password is required.");
                        buttonEnabledFlag = false;
                    }
                    else
                    {
                        this.errorProvider1.SetError(this.passwordTextBox, "");
                    }
                }
                else
                { 
                    this.errorProvider1.SetError(this.loginNameTextBox, String.Empty);
                    this.errorProvider1.SetError(this.passwordTextBox, String.Empty);
                }

                ViewModel.PreviousEnabledStateLoad = ViewModel.ButtonEnabled(buttonEnabledFlag, ViewModel.PreviousEnabledStateLoad, loadButton, menuEditLoad, tsbEditLoad);

            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        #endregion LoadButton

        #region BackgroundWorkerLoad

        /// <summary>
        /// Click event for background Load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadButton_Click(Object sender, System.EventArgs e)
        {
            ViewModel.LoadTables(backgroundWorkerLoad);
        }

        /// <summary>
        /// Handle DoWork event for Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerLoad_DoWork(Object sender, DoWorkEventArgs e)
        {
            ViewModel.BackgroundWorker_DoWork<List<Table>>
            (
                sender as BackgroundWorker,
                e,
                (
                    BackgroundWorker worker,
                    DoWorkEventArgs ea,
                    ref String errorMessage
                ) =>
                {
                    List<Table> result = default(List<Table>);

                    //run process
                    result =
                        GeneratorController<GeneratorModel>.LoadTableListInBackground
                        (   //arguments,
                            GeneratorController<GeneratorModel>.Model,
                            ref errorMessage,
                            worker,
                            ea
                        );

                    return result;
                },
                "No table list was returned."
            );
        }

        /// <summary>
        /// Handle ProgressChanged event for Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerLoad_ProgressChanged(Object sender, ProgressChangedEventArgs e)
        {
            ViewModel.BackgroundWorker_ProgressChanged("Loading...", e.UserState, e.ProgressPercentage);
        }

        /// <summary>
        /// Handle RunWorkerComplete event for Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerLoad_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e)
        {
            ViewModel.BackgroundWorker_RunWorkerCompleted
            (
                "Loaded.",
                sender as BackgroundWorker,
                e,
                (error) =>
                {
                    // Show the error message
                    ViewModel.StopProgressBar(null, error.Message);

                    ViewModel.IsLoadInvalidated = true;
                },
                null,
                () =>
                {
                    List<Table> tableList;

                    tableList = (List<Table>)e.Result;

                    // Generate the list of tables
                    if (tableList.Count > 0)
                    {
                        //display list of tables used to generate code
                        this.dataGridView1.DataSource = tableList;
                    }

                    //restore buttons
                    ViewModel.ButtonsEnabled(false);

                    //re-evaluate buttons
                    ValidateButtons();
                }
            );
        }

        #endregion BackgroundWorkerLoad

        #region GenerateButton

        /// <summary>
        /// Validate Generate model
        /// </summary>
        private void ValidateGenerateButton()
        {
            String statusMessage = String.Empty;
            String errorMessage = default(String);

            try
            {
                Boolean buttonEnabledFlag = true;

                if (ViewModel.IsLoadInvalidated)
                {
                    buttonEnabledFlag = false;

                    if (serverTextBox.Text.Length == 0)
                    {
                        this.errorProvider1.SetError(this.serverTextBox, "Server is required.");
                        buttonEnabledFlag = false;
                    }
                    else
                    {
                        this.errorProvider1.SetError(this.serverTextBox, "");
                    }

                    if (databaseTextBox.Text.Length == 0)
                    {
                        this.errorProvider1.SetError(this.databaseTextBox, "Database is required.");
                        buttonEnabledFlag = false;
                    }
                    else
                    {
                        this.errorProvider1.SetError(this.databaseTextBox, "");
                    }

                    if (sqlServerAuthenticationRadioButton.Checked)
                    {
                        if (loginNameTextBox.Text.Length == 0)
                        {
                            this.errorProvider1.SetError(this.loginNameTextBox, "Login is required.");
                            buttonEnabledFlag = false;
                        }
                        else
                        {
                            this.errorProvider1.SetError(this.loginNameTextBox, "");
                        }

                        if (passwordTextBox.Text.Length == 0)
                        {
                            this.errorProvider1.SetError(this.passwordTextBox, "Password is required.");
                            buttonEnabledFlag = false;
                        }
                        else
                        {
                            this.errorProvider1.SetError(this.passwordTextBox, "");
                        }
                    }

                }

                ViewModel.PreviousEnabledStateGenerate = ViewModel.ButtonEnabled(buttonEnabledFlag, ViewModel.PreviousEnabledStateGenerate, generateButton, menuEditGenerate, tsbEditGenerate);

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
            finally
            {
                ViewModel.StopProgressBar(statusMessage, errorMessage);
            }
        }

        #endregion GenerateButton

        #region BackgroundWorkerGenerate

        /// <summary>
        /// Handle Click event for Generate button
        /// </summary>
        private void GenerateButton_Click(Object sender, System.EventArgs e)
        {
            ViewModel.GenerateFromTables(backgroundWorkerGenerate, (List<Table>)this.dataGridView1.DataSource);
        }

        /// <summary>
        /// Handle DoWork event for Generate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerGenerate_DoWork(Object sender, DoWorkEventArgs e)
        {
            ViewModel.BackgroundWorker_DoWork<Boolean>
            (
                sender as BackgroundWorker,
                e,
                (
                    BackgroundWorker worker,
                    DoWorkEventArgs ea,
                    ref String errorMessage
                ) =>
                {
                    Boolean result = default(Boolean);

                    // Get Tuple object passed from RunWorkerAsync() method
                    Tuple<List<Table> /*tableList*/> arguments =
                        e.Argument as Tuple<List<Table> /*tableList*/>;

                    //run process
                    result =
                        GeneratorController<GeneratorModel>.GenerateInBackground
                        (   //arguments,
                            GeneratorController<GeneratorModel>.Model,
                            arguments.Item1,
                            ref errorMessage,
                            worker,
                            ea
                        );

                    return result;
                },
                "Unable to generate."
            );
        }

        /// <summary>
        /// Handle ProgressChanged event for Generate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerGenerate_ProgressChanged(Object sender, ProgressChangedEventArgs e)
        {
            ViewModel.BackgroundWorker_ProgressChanged("Generating...", e.UserState, e.ProgressPercentage);
        }

        /// <summary>
        /// Handle RunWorkerCompleted event for Generate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerGenerate_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e)
        {
            ViewModel.BackgroundWorker_RunWorkerCompleted
            (
                "Generated.",
                sender as BackgroundWorker,
                e,
                null,
                null,
                () =>
                {
                    //restore buttons
                    ViewModel.ButtonsEnabled(false);

                    //re-evaluate buttons
                    ValidateButtons();
                }
            );
        }
        
        #endregion BackgroundWorkerGenerate
        #endregion Control Events
        #endregion Events

        #region Methods
        #region FormAppBase
        protected void InitViewModel()
        {
            try
            {
                //subscribe view to model notifications
                ModelController<GeneratorModel>.Model.PropertyChanged += ModelPropertyChangedEventHandlerDelegate;
                //subscribe view to settings notifications
                SettingsController<GeneratorSettings>.DefaultHandler = SettingsPropertyChangedEventHandlerDelegate;

                FileDialogInfo settingsFileDialogInfo =
                    new FileDialogInfo
                    (
                        SettingsController<GeneratorSettings>.FILE_NEW,
                        null,
                        null,
                        /*SettingsController<GeneratorSettings>.Settings*/GeneratorSettings.FileTypeExtension,
                        /*SettingsController<GeneratorSettings>.Settings*/GeneratorSettings.FileTypeDescription,
                        /*SettingsController<GeneratorSettings>.Settings*/GeneratorSettings.FileTypeName,
                        new String[] 
                        { 
                            "XML files (*.xml)|*.xml", 
                            "All files (*.*)|*.*" 
                        },
                        false,
                        default(Environment.SpecialFolder),
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal).WithTrailingSeparator()
                    );

                //set dialog caption
                settingsFileDialogInfo.Title = this.Text;

                FileDialogInfo textDataFileDialogInfo =
                    new FileDialogInfo
                    (
                        "(new)",
                        null,
                        null,
                        "txt",
                        "Text files",
                        "txtfile",
                        new String[] 
                        { 
                            "Data files (*.dat)|*.dat", 
                            "All files (*.*)|*.*" 
                        },
                        false,
                        default(Environment.SpecialFolder)
                    );
                //set dialog caption
                textDataFileDialogInfo.Title = this.Text;

                //class to handle standard behaviors
                ViewModelController<Bitmap, GeneratorViewModel>.New
                (
                    ViewName,
                    new GeneratorViewModel
                    (
                        this.ModelPropertyChangedEventHandlerDelegate,
                        new Dictionary<String, Bitmap>() 
                        { 
                            { "New", Properties.Resources.New }, 
                            { "Save", Properties.Resources.Save },
                            { "Open", Properties.Resources.Open },
                            { "Print", Properties.Resources.Print },
                            { "Copy", Properties.Resources.Copy },
                            { "Properties", Properties.Resources.Properties },
                            { "Load", Properties.Resources.Load },
                            { "Generate", Properties.Resources.Generate }
                        },
                        settingsFileDialogInfo,
                        textDataFileDialogInfo,
                        this
                    )
                );
                ViewModel = ViewModelController<Bitmap, GeneratorViewModel>.ViewModel[ViewName];

                BindFormUi();

                //Init config parameters
                if (!LoadParameters())
                {
                    throw new Exception(String.Format("Unable to load config file parameter(s)."));
                }

                //DEBUG:filename coming in is being converted/passed as DOS 8.3 format equivalent
                //Load
                if ((SettingsController<GeneratorSettings>.FilePath == null) || (SettingsController<GeneratorSettings>.Filename == SettingsController<GeneratorSettings>.FILE_NEW))
                {
                    //NEW
                    ViewModel.FileNew();
                }
                else
                {
                    //OPEN
                    ViewModel.FileOpen(false);
                }

#if debug
            //debug view
            menuEditProperties_Click(sender, e);
#endif

                //Display dirty state
                ModelController<GeneratorModel>.Model.Refresh();
            }
            catch (Exception ex)
            {
                if (ViewModel != null)
                {
                    ViewModel.ErrorMessage = ex.Message;
                }
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }

        protected void DisposeSettings()
        {
            //save user and application model 
            Properties.Settings.Default.Save();

            if (SettingsController<GeneratorSettings>.Settings.Dirty)
            {
                //prompt before saving
                DialogResult dialogResult = MessageBox.Show("Save changes?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        {
                            //SAVE
                            ViewModel.FileSave();

                            break;
                        }
                    case DialogResult.No:
                        {
                            break;
                        }
                    default:
                        {
                            throw new InvalidEnumArgumentException();
                        }
                }
            }

            //unsubscribe from model notifications
            ModelController<GeneratorModel>.Model.PropertyChanged -= ModelPropertyChangedEventHandlerDelegate;
        }

        protected void _Run()
        {
            //MessageBox.Show("running", "MVC", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        #endregion FormAppBase

        #region Utility
        /// <summary>
        /// Bind static Model controls to Model Controller
        /// </summary>
        private void BindFormUi()
        {
            try
            {
                //Form
                ControlBindings.BindListControlToListOfString(ddlLanguage, GeneratorModel.LanguageList);
                ControlBindings.BindListControlToListOfString(ddlDBMS, GeneratorModel.DbmsList);
                ControlBindings.BindListControlToListOfString(ddlSqlHelper, GeneratorModel.SQLHelperList);

                //Controls
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        /// <summary>
        /// Bind Model controls to Model Controller
        /// </summary>
        private void BindModelUi()
        {
            try
            {

                //Settings
                //serverTextBox.DataBindings.Clear();
                //serverTextBox.DataBindings.Add("Text", Settings.AsStatic, "Server", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(serverTextBox, ModelController<GeneratorModel>.Model, "Server");

                //databaseTextBox.DataBindings.Clear();
                //databaseTextBox.DataBindings.Add("Text", Settings.AsStatic, "Database", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(databaseTextBox, ModelController<GeneratorModel>.Model, "Database");

                //sqlServerAuthenticationRadioButton.DataBindings.Clear();
                //sqlServerAuthenticationRadioButton.DataBindings.Add("Checked", Settings.AsStatic, "SQLAuthentication", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(sqlServerAuthenticationRadioButton, ModelController<GeneratorModel>.Model, "SQLAuthentication", "Checked");

                //loginNameTextBox.DataBindings.Clear();
                //loginNameTextBox.DataBindings.Add("Text", Settings.AsStatic, "SQLLogin", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(loginNameTextBox, ModelController<GeneratorModel>.Model, "SQLLogin");

                //passwordTextBox.DataBindings.Clear();
                //passwordTextBox.DataBindings.Add("Text", Settings.AsStatic, "Password", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(passwordTextBox, ModelController<GeneratorModel>.Model, "Password");

                //grantUserTextBox.DataBindings.Clear();
                //grantUserTextBox.DataBindings.Add("Text", Settings.AsStatic, "GrantUser", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(grantUserTextBox, ModelController<GeneratorModel>.Model, "GrantUser");

                //storedProcedurePrefixTextBox.DataBindings.Clear();
                //storedProcedurePrefixTextBox.DataBindings.Add("Text", Settings.AsStatic, "SPPrefix", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(storedProcedurePrefixTextBox, ModelController<GeneratorModel>.Model, "SPPrefix");

                //multipleFilesCheckBox.DataBindings.Clear();
                //multipleFilesCheckBox.DataBindings.Add("Checked", Settings.AsStatic, "CreateMultipleFiles", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(multipleFilesCheckBox, ModelController<GeneratorModel>.Model, "CreateMultipleFiles", "Checked");

                //namespaceTextBox.DataBindings.Clear();
                //namespaceTextBox.DataBindings.Add("Text", Settings.AsStatic, "Namespace", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(namespaceTextBox, ModelController<GeneratorModel>.Model, "Namespace");

                //txtClassSuffix.DataBindings.Clear();
                //txtClassSuffix.DataBindings.Add("Text", Settings.AsStatic, "ClassSuffix", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(txtClassSuffix, ModelController<GeneratorModel>.Model, "ClassSuffix");

                //ddlLanguage.DataBindings.Clear();
                //ddlLanguage.DataBindings.Add("SelectedValue", Settings.AsStatic, "Language", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<ComboBox, GeneratorModel>(ddlLanguage, ModelController<GeneratorModel>.Model, "Language", "SelectedValue");

                //chkAutoSelectOnLoad.DataBindings.Clear();
                //chkAutoSelectOnLoad.DataBindings.Add("Checked", Settings.AsStatic, "AutoSelectOnLoad", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(chkAutoSelectOnLoad, ModelController<GeneratorModel>.Model, "AutoSelectOnLoad", "Checked");

                //chkFetchTableDetailsWithLoad.DataBindings.Clear();
                //chkFetchTableDetailsWithLoad.DataBindings.Add("Checked", Settings.AsStatic, "FetchTableDetailsWithLoad", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(chkFetchTableDetailsWithLoad, ModelController<GeneratorModel>.Model, "FetchTableDetailsWithLoad", "Checked");

                //ddlDBMS.DataBindings.Clear();
                //ddlDBMS.DataBindings.Add("SelectedValue", Settings.AsStatic, "DBMS", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<ComboBox, GeneratorModel>(ddlDBMS, ModelController<GeneratorModel>.Model, "DBMS", "SelectedValue");

                //ddlSqlHelper.DataBindings.Clear();
                //ddlSqlHelper.DataBindings.Add("SelectedValue", Settings.AsStatic, "SQLHelper", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<ComboBox, GeneratorModel>(ddlSqlHelper, ModelController<GeneratorModel>.Model, "SQLHelper", "SelectedValue");

                //OutputPathTextBox.DataBindings.Clear();
                //OutputPathTextBox.DataBindings.Add("Text", Settings.AsStatic, "OutputPath", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<TextBox, GeneratorModel>(OutputPathTextBox, ModelController<GeneratorModel>.Model, "OutputPath");

                //GenerateCustomClassTemplateCheckBox.DataBindings.Clear();
                //GenerateCustomClassTemplateCheckBox.DataBindings.Add("Checked", Settings.AsStatic, "GenerateCustomClassTemplate", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(GenerateCustomClassTemplateCheckBox, ModelController<GeneratorModel>.Model, "GenerateCustomClassTemplate", "Checked");

                //GenerateDataLayerClasses.DataBindings.Clear();
                //GenerateDataLayerClasses.DataBindings.Add("Checked", Settings.AsStatic, "GenerateDataLayerClasses", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(GenerateDataLayerClassesCheckBox, ModelController<GeneratorModel>.Model, "GenerateDataLayerClasses", "Checked");

                //GenerateStoredProceduresCheckBox.DataBindings.Clear();
                //GenerateStoredProceduresCheckBox.DataBindings.Add("Checked", Settings.AsStatic, "GenerateStoredProcedures", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(GenerateStoredProceduresCheckBox, ModelController<GeneratorModel>.Model, "GenerateStoredProcedures", "Checked");

                //GenerateWcfLayerClassesCheckBox.DataBindings.Clear();
                //GenerateWcfLayerClassesCheckBox.DataBindings.Add("Checked", Settings.AsStatic, "GenerateWcfLayerClasses", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(GenerateWcfLayerClassesCheckBox, ModelController<GeneratorModel>.Model, "GenerateWcfLayerClasses", "Checked");

                //GenerateWcfLayerServerComponentsCheckBox.DataBindings.Clear();
                //GenerateWcfLayerServerComponentsCheckBox.DataBindings.Add("Checked", Settings.AsStatic, "GenerateWcfLayerServerComponents", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(GenerateWcfLayerServerComponentsCheckBox, ModelController<GeneratorModel>.Model, "GenerateWcfLayerServerComponents", "Checked");

                //GenerateWcfLayerClientHelpersCheckBox.DataBindings.Clear();
                //GenerateWcfLayerClientHelpersCheckBox.DataBindings.Add("Checked", Settings.AsStatic, "GenerateWcfLayerClientHelpers", false, DataSourceUpdateMode.OnPropertyChanged);
                BindField<CheckBox, GeneratorModel>(GenerateWcfLayerClientHelpersCheckBox, ModelController<GeneratorModel>.Model, "GenerateWcfLayerClientHelpers", "Checked");
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        private void BindField<TControl, TModel>
        (
            TControl fieldControl,
            TModel model,
            String modelPropertyName,
            String controlPropertyName = "Text",
            Boolean formattingEnabled = false,
            DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged,
            Boolean reBind = true
        )
            where TControl : Control
        {
            try
            {
                fieldControl.DataBindings.Clear();
                if (reBind)
                {
                    fieldControl.DataBindings.Add(controlPropertyName, model, modelPropertyName, formattingEnabled, dataSourceUpdateMode);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Apply Settings to viewer.
        /// </summary>
        private void ApplySettings()
        {
            try
            {
                _ValueChangedProgrammatically = true;

                //apply model that have databindings
                BindModelUi();

                //apply model that shouldn't use databindings

                //apply model that can't use databindings
                Text = Path.GetFileName(SettingsController<GeneratorSettings>.Filename) + " - " + ViewName;

                //apply model that don't have databindings
                ViewModel.DirtyIconIsVisible = (SettingsController<GeneratorSettings>.Settings.Dirty);

                _ValueChangedProgrammatically = false;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        /// <summary>
        /// Set function button and menu to enable value, and cancel button to opposite.
        /// For now, do only disabling here and leave enabling based on biz logic 
        ///  to be triggered by refresh?
        /// </summary>
        /// <param name="functionButton"></param>
        /// <param name="functionMenu"></param>
        /// <param name="cancelButton"></param>
        /// <param name="enable"></param>
        private void SetFunctionControlsEnable
        (
            Button functionButton,
            ToolStripButton functionToolbarButton,
            ToolStripMenuItem functionMenu,
            Button cancelButton,
            Boolean enable
        )
        {
            try
            {
                //stand-alone button
                if (functionButton != null)
                {
                    functionButton.Enabled = enable;
                }

                //toolbar button
                if (functionToolbarButton != null)
                {
                    functionToolbarButton.Enabled = enable;
                }

                //menu item
                if (functionMenu != null)
                {
                    functionMenu.Enabled = enable;
                }

                //stand-alone cancel button
                if (cancelButton != null)
                {
                    cancelButton.Enabled = !enable;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Invoke any delegate that has been registered 
        ///  to cancel a long-running background process.
        /// </summary>
        private void InvokeActionCancel()
        {
            try
            {
                //execute cancellation hook
                if (cancelDelegate != null)
                {
                    cancelDelegate();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Load from app config; override with command line if present
        /// </summary>
        /// <returns></returns>
        private Boolean LoadParameters()
        {
            Boolean returnValue = default(Boolean);
#if USE_CONFIG_FILEPATH
            String _settingsFilename = default(String);
#endif

            try
            {
                if ((Program.Filename != null) && (Program.Filename != SettingsController<GeneratorSettings>.FILE_NEW))
                {
                    //got filename from command line
                    //DEBUG:filename coming in is being converted/passed as DOS 8.3 format equivalent
                    if (!RegistryAccess.ValidateFileAssociation(Application.ExecutablePath, "." + /*SettingsController<GeneratorSettings>.Settings*/GeneratorSettings.FileTypeExtension))
                    {
                        throw new ApplicationException(String.Format("Settings filename not associated: '{0}'.\nCheck filename on command line.", Program.Filename));
                    }
                    //it passed; use value from command line
                }
                else
                {
#if USE_CONFIG_FILEPATH
                    //get filename from app.config
                    if (!Configuration.ReadString("SettingsFilename", out _settingsFilename))
                    {
                        throw new ApplicationException(String.Format("Unable to load SettingsFilename: {0}", "SettingsFilename"));
                    }
                    if ((_settingsFilename == null) || (_settingsFilename == SettingsController<GeneratorSettings>.FILE_NEW))
                    {
                        throw new ApplicationException(String.Format("Settings filename not set: '{0}'.\nCheck SettingsFilename in app config file.", _settingsFilename));
                    }
                    //use with the supplied path
                    SettingsController<GeneratorSettings>.Filename = _settingsFilename;

                    if (Path.GetDirectoryName(_settingsFilename) == String.Empty)
                    {
                        //supply default path if missing
                        SettingsController<GeneratorSettings>.Pathname = Environment.GetFolderPath(Environment.SpecialFolder.Personal).WithTrailingSeparator();
                    }
#endif
                }

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                //throw;
            }
            return returnValue;
        }

        private void BindSizeAndLocation()
        {
            //Note:Size must be done after InitializeComponent(); do Location this way as well.--SJS
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::DataTierGeneratorPlus.Properties.Settings.Default, "Location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::DataTierGeneratorPlus.Properties.Settings.Default, "Size", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ClientSize = global::DataTierGeneratorPlus.Properties.Settings.Default.Size;
            this.Location = global::DataTierGeneratorPlus.Properties.Settings.Default.Location;
        }
        #endregion Utility
        #endregion Methods

    }
}
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using Ssepan.Application.WinForms;
using Ssepan.DataBinding;
using Ssepan.Io;
using Ssepan.Patterns;
using Ssepan.Utility;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataTierGeneratorPlus
{
    /// <summary>
    /// Form used to collect the connection information for the code we're going to generate.
    /// </summary>
    public partial class GeneratorViewer : Window
    {
        #region declarations

        //private Settings settings;
        private Boolean IsLoadInvalidated = true;
        private Boolean _ValueChangedProgrammatically;
        private String _OriginalFormTitle;
        private String[] _CommandLineArgs = null;
        //use state field to remember, because buttons may use different criteria to determine when they are enabled, 
        //and one may be enabled while the other is not. Also, they need to be remembered during background processing and used when finished.
        private Boolean LoadButtonState = false;
        private Boolean GenerateButtonState = false;
        private static FileDialogInfo settingsFileDialogInfo = new FileDialogInfo(Settings.FILE_NEW, null, null, Settings.FILE_TYPE_EXTENSION, Settings.FILE_TYPE_DESCRIPTION, Settings.FILE_TYPE_NAME, new String[] { "XML files (*.xml)|*.xml", "All files (*.*)|*.*" }, false, Environment.SpecialFolder.Personal);

        private enum GenerateParameterListIndex
        {
            GenerateParameterListIndexSettings = 0,
            GenerateParameterListIndexTableList = 1,
        }
        #endregion declarations
        
        #region Properties
        protected String OriginalFormTitle
        {
            get { return _OriginalFormTitle; }
            set { _OriginalFormTitle = value; }
        }
        #endregion Properties

        #region constructors
        public GeneratorViewer(String[] args)
        {
            try
            {
                InitializeComponent();
                _CommandLineArgs = args;

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
        #endregion constructors

        #region IEventObserver Members

        public void NotifyHandler(Object sender, EventArgs e)
        {
            try
            {
                this.ApplySettings();

                //calculate changes
                //Program.somemodel.dosomething();
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

        #endregion IEventObserver Members

        #region FormEvents

        /// <summary>
        /// Load event.
        /// </summary>
        private void GeneratorViewer_Load(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";
            String fileName = String.Empty;

            try
            {
                Boolean isValidFileAssociation;

                //subscribe to notifications
                if (GeneratorModel.model != null)
                {
                    GeneratorModel.model.Notify += this.NotifyHandler;
                }

                SettingsController.SkipSettingsCheck = true;

                settingsFileDialogInfo.Title = this.Title;

                BindFormUi();

                OriginalFormTitle = this.Title;

                fileName = SettingsController.Filename;

                isValidFileAssociation = GeneratorController.ValidateFileAssociation();
                if (isValidFileAssociation)
                {
                    //check each parameter for the file name (take first)
                    foreach (String param in _CommandLineArgs)
                    {
                        fileName = param;
                        break;
                    }
                }

                //DEBUG:filename coming in is being converted/passed as DOS 8.3 format equivalent
                //if ... && (fileName.Contains("." + Settings.FILE_TYPE_EXTENSION)))
                //Load
                if ((fileName == null) || (fileName == Settings.FILE_NEW))
                {
                    //NEW
                    StartProgressBar("New...", "", this.menuFileNew.Image);
                    GeneratorController.New();
                    statusMessage = "New.";
                }
                else
                {
                    SettingsController.Filename = fileName;

                    //OPEN
                    settingsFileDialogInfo.Filename = SettingsController.Filename;
                    if (FileDialogInfo.GetPathForLoad(false, settingsFileDialogInfo))
                    {
                        SettingsController.Filename = settingsFileDialogInfo.Filename;

                        StartProgressBar("Opening...", "", this.menuFileOpen.Image);
                        GeneratorController.Open();
                        statusMessage = "Opened.";
                    }
                    else
                    {
                        statusMessage = "Open cancelled.";
                    }
                }

                #if debug
                //debug view
                menuEditProperties_Click(sender, e);
                #endif

                SettingsController.SkipSettingsCheck = false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GeneratorViewer_Closing(Object sender, CancelEventArgs e)
        {
            //String statusMessage = "";
            //String errorMessage = "";

            try
            {
                e.Cancel = false;
            }
            catch (Exception ex)
            {
                //errorMessage = ex.Message.ToString();
                //statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                //throw ex;
            }
            //finally
            //{
            //    StopProgressBar(statusMessage, errorMessage);
            //}
        }

        /// <summary>
        /// Closing event.
        /// </summary>
        private void GeneratorViewer_FormClosing(Object sender, FormClosingEventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                //SAVE
                if (SettingsController.Dirty)
                {
                    //prompt before saving
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Save changes?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (dialogResult)
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                        {
                            settingsFileDialogInfo.Filename = SettingsController.Filename;
                            if (FileDialogInfo.GetPathForSave(false, settingsFileDialogInfo))
                            {
                                SettingsController.Filename = settingsFileDialogInfo.Filename;

                                StartProgressBar("Saving...", "", this.menuFileSave.Image);
                                GeneratorController.Save();
                                statusMessage = "Saved.";
                            }
                            else
                            {
                                statusMessage = "Save cancelled.";
                            }

                            break;
                        }
                        case System.Windows.Forms.DialogResult.No:
                        {
                            break;
                        }
                        default:
                        {
                            throw new InvalidEnumArgumentException();
                        }
                    }
                }

                //unsubscribe from notifications
                if (GeneratorModel.model != null)
                {
                    GeneratorModel.model.Notify -= this.NotifyHandler;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GeneratorViewer_Move(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                //If the user changed the position in the view...
                if (!_ValueChangedProgrammatically)
                {
                    //...tell the controller about it.
                    GeneratorController.SetViewerLocation(this.Location);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GeneratorViewer_Resize(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                //If the user changed the size in the view...
                if (!_ValueChangedProgrammatically)
                {
                    //...tell the controller about it.
                    GeneratorController.SetViewerSize(this.Size);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// form KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneratorViewer_KeyDown(Object sender, System.Windows.Input.KeyEventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

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

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// output folder button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputPathFolderSelectButton_Click(Object sender, EventArgs e)
        {
            try
            {
                if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.OutputPathTextBox.Text = this.folderBrowserDialog1.SelectedPath;
                }

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

        #endregion FormEvents

        #region MenuEvents

        private void menuFileNew_Click(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                //SAVE
                if (SettingsController.Dirty)
                {
                    //prompt before saving
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Save changes?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (dialogResult)
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                        {
                            settingsFileDialogInfo.Filename = SettingsController.Filename;
                            if (FileDialogInfo.GetPathForSave(false, settingsFileDialogInfo))
                            {
                                SettingsController.Filename = settingsFileDialogInfo.Filename;

                                StartProgressBar("Saving...", "", this.menuFileSave.Image);
                                GeneratorController.Save();
                                statusMessage = "Saved.";
                            }
                            else
                            {
                                statusMessage = "Save cancelled.";
                            }

                            break;
                        }
                        case System.Windows.Forms.DialogResult.No:
                        {
                            break;
                        }
                        default:
                        {
                            throw new InvalidEnumArgumentException();
                        }
                    }
                }

                //clear grid on New or Open
                this.dataGridView1.DataSource = null;

                //NEW
                StartProgressBar("New...", "", this.menuFileNew.Image);
                GeneratorController.New();
                statusMessage = "New.";

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void menuFileOpen_Click(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                //SAVE
                if (SettingsController.Dirty)
                {

                    //prompt before saving
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Save changes?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (dialogResult)
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                        {
                            settingsFileDialogInfo.Filename = SettingsController.Filename;
                            if (FileDialogInfo.GetPathForSave(false, settingsFileDialogInfo))
                            {
                                SettingsController.Filename = settingsFileDialogInfo.Filename;

                                StartProgressBar("Saving...", "", this.menuFileSave.Image);
                                GeneratorController.Save();
                                statusMessage = "Saved.";
                            }
                            else
                            {
                                statusMessage = "Save cancelled.";
                            }

                            break;
                        }
                        case System.Windows.Forms.DialogResult.No:
                        {
                            break;
                        }
                        default:
                        {
                            throw new InvalidEnumArgumentException();
                        }
                    }
                }

                //clear grid on New or Open
                this.dataGridView1.DataSource = null;

                //OPEN
                settingsFileDialogInfo.Filename = SettingsController.Filename;
                if (FileDialogInfo.GetPathForLoad(false, settingsFileDialogInfo))
                {
                    SettingsController.Filename = settingsFileDialogInfo.Filename;

                    StartProgressBar("Opening...", "", this.menuFileOpen.Image);
                    GeneratorController.Open();
                    statusMessage = "Opened.";
                }
                else
                {
                    statusMessage = "Open cancelled.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void menuFileSave_Click(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                //SAVE
                settingsFileDialogInfo.Filename = SettingsController.Filename;
                if (FileDialogInfo.GetPathForSave(false, settingsFileDialogInfo))
                {
                    SettingsController.Filename = settingsFileDialogInfo.Filename;

                    StartProgressBar("Saving...", "", this.menuFileSave.Image);
                    GeneratorController.Save();
                    statusMessage = "Saved.";
                }
                else
                {
                    statusMessage = "Save cancelled.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void menuFileSaveAs_Click(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                //SAVE
                settingsFileDialogInfo.Filename = SettingsController.Filename;
                if (FileDialogInfo.GetPathForSave(true, settingsFileDialogInfo))
                {
                    SettingsController.Filename = settingsFileDialogInfo.Filename;

                    StartProgressBar("Saving...", "", this.menuFileSave.Image);
                    GeneratorController.Save();
                    statusMessage = "Saved.";
                }
                else
                {
                    statusMessage = "Save cancelled.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void menuFileExit_Click(Object sender, EventArgs e)
        {
            //String statusMessage = "";
            //String errorMessage = "";

            try
            {
                this.Close();
                Application.Exit();

            }
            catch (Exception ex)
            {
                //errorMessage = ex.Message.ToString();
                //statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                //StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void menuEditProperties_Click(object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ShowProperties();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void menuHelpAbout_Click(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                // Open the About form in Dialog Mode
                AboutDialog frm = new AboutDialog(new AssemblyInfo());
                frm.ShowDialog(this);
                frm.Dispose();

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        #endregion MenuEvents

        #region Settings Events

        /// <summary>
        /// required by Load and (Generate if Fetch Table Details On Load not checked)
        /// </summary>
        private void ServerTextBox_TextChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.IsLoadInvalidated = true;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// required by Load and (Generate if Fetch Table Details On Load not checked)
        /// </summary>
        private void DatabaseTextBox_TextChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.IsLoadInvalidated = true;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// CheckChanged event.
        /// </summary>
        private void SqlServerAuthenticationRadioButton_CheckedChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                if (((CheckBox)sender).Checked)
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

                this.IsLoadInvalidated = true;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// required by Load and (Generate if Fetch Table Details On Load not checked)
        /// </summary>
        private void LoginNameTextBox_TextChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.IsLoadInvalidated = true;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// required by Load and (Generate if Fetch Table Details On Load not checked)
        /// </summary>
        private void passwordTextBox_TextChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.IsLoadInvalidated = true;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// CheckedChanged event.
        /// </summary>
        private void chkFetchTableDetailsWithLoad_CheckedChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.IsLoadInvalidated = true;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// CheckedChanged event.
        /// </summary>
        private void chkAutoSelectOnLoad_CheckedChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.IsLoadInvalidated = true;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// TextChanged event.
        /// </summary>
        private void namespaceTextBox_TextChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void grantUserTextBox_TextChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void storedProcedurePrefixTextBox_TextChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void multipleFilesCheckBox_CheckedChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void txtClassSuffix_TextChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GenerateCustomClassTemplateCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void OutputPathTextBox_TextChanged(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void ddlLanguage_SelectedIndexChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void ddlDBMS_SelectedIndexChanged(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void ddlSqlHelper_SelectedIndexChanged(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GenerateDataLayerClassesCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.csGroupBox.Enabled = ((CheckBox)sender).Checked;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GenerateStoredProceduresCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.sqlGroupBox.Enabled = ((CheckBox)sender).Checked;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GenerateWcfLayerClassesCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                this.wcfGroupBox.Enabled = ((CheckBox)sender).Checked;

                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GenerateWcfLayerServerComponentsCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        private void GenerateWcfLayerClientHelpersCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                ValidateButtons();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

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
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw ex;
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

                LoadEnabled(buttonEnabledFlag);

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
        /// LoadEnabled method.
        /// </summary>
        private void LoadEnabled(Boolean enabledFlag)
        {
            try
            {
                this.loadButton.Enabled = enabledFlag;
                this.menuEditLoad.Enabled = enabledFlag;
                this.tsbEditLoad.Enabled = enabledFlag;

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

        #endregion LoadButton

        #region BackgroundWorkerLoad

        /// <summary>
        /// Click event for background Load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadButton_Click(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                //clear or set messages
                StartProgressBar("Loading...", errorMessage, this.menuEditLoad.Image, ProgressBarStyle.Blocks, 0);

                //save buttons
                LoadButtonState = loadButton.Enabled;
                LoadEnabled(false);

                GenerateButtonState = generateButton.Enabled;
                GenerateEnabled(false);

                ////read controls into settings and  Generate the SQL and .Net code
                //this.backgroundWorkerLoad.RunWorkerAsync(ScrapeSettings());
                this.backgroundWorkerLoad.RunWorkerAsync(SettingsController.ModelSettings);

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                StopProgressBar(statusMessage, errorMessage);
                    
                this.IsLoadInvalidated = true;
            }
        }

        /// <summary>
        /// Handle DoWork event for Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerLoad_DoWork(Object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                //run process
                e.Result = GeneratorModel.LoadTableListInBackground((Settings)e.Argument, worker, e);
                if (e.Result == null)
                {
                    throw new Exception("No table list was returned.");
                }
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                //re-throw and let RunWorkerCompleted event handle and report error.
                throw ex;
            }
        }

        /// <summary>
        /// Handle ProgressChanged event for Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerLoad_ProgressChanged(Object sender, ProgressChangedEventArgs e)
        {
            try
            {
                UpdateProgressBar("Loading..." + e.ProgressPercentage.ToString(), e.ProgressPercentage);

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
        /// Handle RunWorkerComplete event for Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerLoad_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";
            
            try
            {
                // First, handle the case where an exception was thrown.
                if (e.Error != null)
                {
                    // Show the error message
                    errorMessage = e.Error.Message.ToString();
                    statusMessage = "";

                    this.IsLoadInvalidated = true;
                }
                else if (e.Cancelled)
                {
                    // Handle the case where the user cancelled the operation.
                    errorMessage = "Cancelled.";
                    statusMessage = "";
                }
                else
                {
                    // Operation completed successfully. 
                    // So display the result.

                    ArrayList tableList;
                    BackgroundWorker worker = sender as BackgroundWorker;

                    tableList = (ArrayList)e.Result;

                    // Generate the list of tables
                    if (tableList.Count > 0)
                    {
                        //display list of tables used to generate code
                        this.dataGridView1.DataSource = tableList;
                    }

                    // Inform the user we're done
                    statusMessage = "Loaded.";

                    this.IsLoadInvalidated = false;

                }
                // Do post completion operations, like enabling the controls etc.      
                //restore buttons

                LoadEnabled(LoadButtonState);
                GenerateEnabled(GenerateButtonState);

                //re-evaluate buttons
                ValidateButtons();

                this.Activate();

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        #endregion BackgroundWorkerLoad

        #region GenerateButton

        /// <summary>
        /// Validate Generate settings
        /// </summary>
        private void ValidateGenerateButton()
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                Boolean buttonEnabledFlag = true;

                if (this.IsLoadInvalidated)
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

                GenerateEnabled(buttonEnabledFlag);

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";

                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// Set Enabled state of Generate feature
        /// </summary>
        private void GenerateEnabled(Boolean enabledFlag)
        {
            try
            {
                this.generateButton.Enabled = enabledFlag;
                this.menuEditGenerate.Enabled = enabledFlag;
                this.tsbEditGenerate.Enabled = enabledFlag;

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

        #endregion GenerateButton

        #region BackgroundWorkerGenerate

        /// <summary>
        /// Handle Click event for Generate button
        /// </summary>
        private void GenerateButton_Click(Object sender, System.EventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";
            try
            {
                //clear or set mesages
                StartProgressBar("Generating...", errorMessage, this.menuEditGenerate.Image, ProgressBarStyle.Blocks, 0);

                //save buttons
                LoadButtonState = loadButton.Enabled;
                LoadEnabled(false);

                GenerateButtonState = generateButton.Enabled;
                GenerateEnabled(false);

                //create arraylist to hold 2 parameters
                ArrayList parameterList = new ArrayList();

                //load parameter list
                ////read controls into settings, and add as index 0
                parameterList.Add(SettingsController.ModelSettings);
                //retrieve list of tables used to generate code, and add as index 1
                parameterList.Add((ArrayList)this.dataGridView1.DataSource);

                // Generate the SQL and .Net code
                this.backgroundWorkerGenerate.RunWorkerAsync(parameterList);

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message.ToString();
                statusMessage = "";
                
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                StopProgressBar(statusMessage, errorMessage);
            }
        }

        /// <summary>
        /// Handle DoWork event for Generate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerGenerate_DoWork(Object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                ArrayList parameterList = (ArrayList)e.Argument;

                //extract parameters and run process
                if (!GeneratorModel.GenerateInBackground((Settings)parameterList[(int)GenerateParameterListIndex.GenerateParameterListIndexSettings], (ArrayList)parameterList[(int)GenerateParameterListIndex.GenerateParameterListIndexTableList], worker, e))
                {
                    throw new ApplicationException("Unable to Generate In Background.");
                }

                //no return value; 
                e.Result = null;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                //re-throw and let RunWorkerCompleted event handle and report error.
                throw ex;
            }
        }

        /// <summary>
        /// Handle ProgressChanged event for Generate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerGenerate_ProgressChanged(Object sender, ProgressChangedEventArgs e)
        {
            try
            {
                UpdateProgressBar("Generating..." + e.ProgressPercentage.ToString() + "%", e.ProgressPercentage);

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
        /// Handle RunWorkerCompleted event for Generate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerGenerate_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e)
        {
            String statusMessage = "";
            String errorMessage = "";

            try
            {
                // First, handle the case where an exception was thrown.
                if (e.Error != null)
                {
                    // Show the error message
                    errorMessage = e.Error.Message.ToString();
                    statusMessage = "";
                }
                else if (e.Cancelled)
                {
                    // Handle the case where the user cancelled the operation.
                    errorMessage = "Cancelled.";
                    statusMessage = "";
                }
                else
                {
                    // Operation completed successfully. 
                    // So display the result.
                    BackgroundWorker worker = sender as BackgroundWorker;

                    //no return result.

                    // Inform the user we're done
                    statusMessage = "Generated.";
                }

                // Do post completion operations, like enabling the controls etc.        

                //restore buttons
                LoadEnabled(LoadButtonState);
                GenerateEnabled(GenerateButtonState);

                this.Activate();

            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
            }
            finally
            {
                StopProgressBar(statusMessage, errorMessage);
            }
        }
        
        #endregion BackgroundWorkerGenerate

        #region Utility
        /// <summary>
        /// Display property grid dialog. Changes to properties are reflected immediately.
        /// </summary>
        public static void ShowProperties()
        {
            try
            {
                PropertyDialog pv = new PropertyDialog(SettingsController.ModelSettings, GeneratorController.Refresh);
#if debug
                pv.Show();//dialog properties grid validaPropertiesViewertion does refresh
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
            }
        }

        /// <summary>
        /// Bind Settings controls to SettingsController
        /// </summary>
        private void BindFormUi()
        {
            try
            {
                //Form
                ControlBindings.BindListControlToListOfString(ddlLanguage, Settings.LanguageList);
                ControlBindings.BindListControlToListOfString(ddlDBMS, Settings.DbmsList);
                ControlBindings.BindListControlToListOfString(ddlSqlHelper, Settings.SQLHelperList);
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

        /// <summary>
        /// Bind Settings controls to SettingsController
        /// </summary>
        private void BindSettingsUi()
        {
            try
            {

                //Settings
                serverTextBox.DataBindings.Clear();
                serverTextBox.DataBindings.Add("Text", SettingsController.ModelSettings, "Server", false, DataSourceUpdateMode.OnPropertyChanged);

                databaseTextBox.DataBindings.Clear();
                databaseTextBox.DataBindings.Add("Text", SettingsController.ModelSettings, "Database", false, DataSourceUpdateMode.OnPropertyChanged);

                sqlServerAuthenticationRadioButton.DataBindings.Clear();
                sqlServerAuthenticationRadioButton.DataBindings.Add("Checked", SettingsController.ModelSettings, "SQLAuthentication", false, DataSourceUpdateMode.OnPropertyChanged);

                loginNameTextBox.DataBindings.Clear();
                loginNameTextBox.DataBindings.Add("Text", SettingsController.ModelSettings, "SQLLogin", false, DataSourceUpdateMode.OnPropertyChanged);

                passwordTextBox.DataBindings.Clear();
                passwordTextBox.DataBindings.Add("Text", SettingsController.ModelSettings, "Password", false, DataSourceUpdateMode.OnPropertyChanged);

                grantUserTextBox.DataBindings.Clear();
                grantUserTextBox.DataBindings.Add("Text", SettingsController.ModelSettings, "GrantUser", false, DataSourceUpdateMode.OnPropertyChanged);

                storedProcedurePrefixTextBox.DataBindings.Clear();
                storedProcedurePrefixTextBox.DataBindings.Add("Text", SettingsController.ModelSettings, "SPPrefix", false, DataSourceUpdateMode.OnPropertyChanged);

                multipleFilesCheckBox.DataBindings.Clear();
                multipleFilesCheckBox.DataBindings.Add("Checked", SettingsController.ModelSettings, "CreateMultipleFiles", false, DataSourceUpdateMode.OnPropertyChanged);

                namespaceTextBox.DataBindings.Clear();
                namespaceTextBox.DataBindings.Add("Text", SettingsController.ModelSettings, "Namespace", false, DataSourceUpdateMode.OnPropertyChanged);

                txtClassSuffix.DataBindings.Clear();
                txtClassSuffix.DataBindings.Add("Text", SettingsController.ModelSettings, "ClassSuffix", false, DataSourceUpdateMode.OnPropertyChanged);

                ddlLanguage.DataBindings.Clear();
                ddlLanguage.DataBindings.Add("SelectedValue", SettingsController.ModelSettings, "Language", false, DataSourceUpdateMode.OnPropertyChanged);

                chkAutoSelectOnLoad.DataBindings.Clear();
                chkAutoSelectOnLoad.DataBindings.Add("Checked", SettingsController.ModelSettings, "AutoSelectOnLoad", false, DataSourceUpdateMode.OnPropertyChanged);

                chkFetchTableDetailsWithLoad.DataBindings.Clear();
                chkFetchTableDetailsWithLoad.DataBindings.Add("Checked", SettingsController.ModelSettings, "FetchTableDetailsWithLoad", false, DataSourceUpdateMode.OnPropertyChanged);

                ddlDBMS.DataBindings.Clear();
                ddlDBMS.DataBindings.Add("SelectedValue", SettingsController.ModelSettings, "DBMS", false, DataSourceUpdateMode.OnPropertyChanged);

                ddlSqlHelper.DataBindings.Clear();
                ddlSqlHelper.DataBindings.Add("SelectedValue", SettingsController.ModelSettings, "SQLHelper", false, DataSourceUpdateMode.OnPropertyChanged);

                OutputPathTextBox.DataBindings.Clear();
                OutputPathTextBox.DataBindings.Add("Text", SettingsController.ModelSettings, "OutputPath", false, DataSourceUpdateMode.OnPropertyChanged);

                GenerateCustomClassTemplateCheckBox.DataBindings.Clear();
                GenerateCustomClassTemplateCheckBox.DataBindings.Add("Checked", SettingsController.ModelSettings, "GenerateCustomClassTemplate", false, DataSourceUpdateMode.OnPropertyChanged);

                GenerateDataLayerClassesCheckBox.DataBindings.Clear();
                GenerateDataLayerClassesCheckBox.DataBindings.Add("Checked", SettingsController.ModelSettings, "GenerateDataLayerClassesCheckBox", false, DataSourceUpdateMode.OnPropertyChanged);

                GenerateStoredProceduresCheckBox.DataBindings.Clear();
                GenerateStoredProceduresCheckBox.DataBindings.Add("Checked", SettingsController.ModelSettings, "GenerateStoredProcedures", false, DataSourceUpdateMode.OnPropertyChanged);

                GenerateWcfLayerClassesCheckBox.DataBindings.Clear();
                GenerateWcfLayerClassesCheckBox.DataBindings.Add("Checked", SettingsController.ModelSettings, "GenerateWcfLayerClasses", false, DataSourceUpdateMode.OnPropertyChanged);

                GenerateWcfLayerServerComponentsCheckBox.DataBindings.Clear();
                GenerateWcfLayerServerComponentsCheckBox.DataBindings.Add("Checked", SettingsController.ModelSettings, "GenerateWcfLayerServerComponents", false, DataSourceUpdateMode.OnPropertyChanged);

                GenerateWcfLayerClientHelpersCheckBox.DataBindings.Clear();
                GenerateWcfLayerClientHelpersCheckBox.DataBindings.Add("Checked", SettingsController.ModelSettings, "GenerateWcfLayerClientHelpers", false, DataSourceUpdateMode.OnPropertyChanged);
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
        /// Show and start progress bar.
        /// </summary>
        /// <param name="statusMessage"></param>
        /// <param name="errorMessage"></param>
        /// <param name="objImage"></param>
        private void StartProgressBar(String statusMessage, String errorMessage, System.Windows.Controls.Image objImage)
        {
            try
            {
                this.StatusBarStatusMessage.Text = statusMessage;
                this.StatusBarErrorMessage.Text = errorMessage;
                //this.StatusBarErrorMessage.ToolTipText = errorMessage;

                this.StatusBarProgressBar.Visible = true;

                this.StatusBarActionIcon.Image = objImage;
                this.StatusBarActionIcon.Visible = true;

                //give the app time to draw the eye-candy, even if its only for an instant
                Application.DoEvents();
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
        /// Show and start progress bar; with style and value.
        /// </summary>
        /// <param name="statusMessage"></param>
        /// <param name="errorMessage"></param>
        /// <param name="objImage"></param>
        /// <param name="progressBarStyle"></param>
        /// <param name="progressBarValue"></param>
        private void StartProgressBar(String statusMessage, String errorMessage, System.Windows.Controls.Image objImage, ProgressBarStyle progressBarStyle, Int32 progressBarValue)
        {
            try
            {
                this.StatusBarStatusMessage.Text = statusMessage;
                this.StatusBarErrorMessage.Text = errorMessage;
                //this.StatusBarErrorMessage.ToolTipText = errorMessage;

                this.StatusBarProgressBar.Style = progressBarStyle;//set to blocks if actual percentage was used.
                this.StatusBarProgressBar.Value = progressBarValue;//set to value if percentage used.
                //if Style is not Marquee, then we are marking either a count or percentage
                if (progressBarValue > this.StatusBarProgressBar.Maximum)
                {
                    this.StatusBarProgressBar.Step = 1;
                    this.StatusBarProgressBar.Value = 1;
                }
                this.StatusBarProgressBar.Visible = true;

                this.StatusBarActionIcon.Image = objImage;
                this.StatusBarActionIcon.Visible = true;

                //give the app time to draw the eye-candy, even if its only for an instant
                Application.DoEvents();
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
        /// Update progress bar.
        /// </summary>
        /// <param name="statusMessage"></param>
        /// <param name="progressBarValue"></param>
        private void UpdateProgressBar(String statusMessage, Int32 progressBarValue)
        {
            try
            {
                this.StatusBarStatusMessage.Text = statusMessage;
                //this.StatusBarErrorMessage.Text = errorMessage;
                //this.StatusBarErrorMessage.ToolTipText = errorMessage;

                //if Style is not Marquee, then we are marking either a count or percentage
                //if we are simply counting, the progress bar will periodically need to adjust the Maximum.
                if (progressBarValue > this.StatusBarProgressBar.Maximum)
                {
                    this.StatusBarProgressBar.Maximum = this.StatusBarProgressBar.Maximum * 2;
                }
                this.StatusBarProgressBar.Value = progressBarValue;

                //give the app time to draw the eye-candy, even if its only for an instant
                Application.DoEvents();
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
        /// Stop and hide progress bar.
        /// </summary>
        /// <param name="statusMessage"></param>
        /// <param name="errorMessage"></param>
        private void StopProgressBar(String statusMessage, String errorMessage)
        {
            try
            {
                this.StatusBarStatusMessage.Text = statusMessage;
                this.StatusBarErrorMessage.Text = errorMessage;
                //this.StatusBarErrorMessage.ToolTipText = errorMessage;

                this.StatusBarProgressBar.Visible = false;
                this.StatusBarProgressBar.Style = ProgressBarStyle.Marquee;//reset back to marquee (default) in case actual percentage was used
                this.StatusBarProgressBar.Maximum = 100;//ditto
                this.StatusBarProgressBar.Step = 10;//ditto
                this.StatusBarProgressBar.Value = 30;//ditto

                this.StatusBarActionIcon.Visible = false;
                this.StatusBarActionIcon.Image = null;

                //give the app time to draw the eye-candy, even if its only for an instant
                Application.DoEvents();
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
        /// Apply anotherSettings to viewer.
        /// </summary>
        private void ApplySettings()
        {
            try
            {
                _ValueChangedProgrammatically = true;

                //apply settings that have databindings
                BindSettingsUi();

                //apply settings that shouldn't use databindings
                this.Size = SettingsController.ModelSettings.Size;
                this.Location = SettingsController.ModelSettings.Location;

                //apply settings that can't use databindings
                this.Text = Path.GetFileName(SettingsController.Filename) + " - " + _OriginalFormTitle;

                //apply settings that don't have databindings
                this.StatusBarDirtyMessage.Visible = (SettingsController.Dirty);

                _ValueChangedProgrammatically = false;
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
        #endregion Utility

    }
}

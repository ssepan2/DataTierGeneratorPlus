using System;

namespace DataTierGeneratorPlus
{
    partial class GeneratorViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(Boolean disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneratorViewer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.authenticationGroupBox = new System.Windows.Forms.GroupBox();
            this.lblSqlAuthentication = new System.Windows.Forms.Label();
            this.lblWindowsAuthentication = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.loginNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginNameLabel = new System.Windows.Forms.Label();
            this.sqlServerAuthenticationRadioButton = new System.Windows.Forms.CheckBox();
            this.sqlGroupBox = new System.Windows.Forms.GroupBox();
            this.storedProcedurePrefixTextBox = new System.Windows.Forms.TextBox();
            this.storedProcedurePrefixLabel = new System.Windows.Forms.Label();
            this.grantUserTextBox = new System.Windows.Forms.TextBox();
            this.grantUserLabel = new System.Windows.Forms.Label();
            this.multipleFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.csGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlLanguage = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ddlSqlHelper = new System.Windows.Forms.ComboBox();
            this.GenerateCustomClassTemplateCheckBox = new System.Windows.Forms.CheckBox();
            this.txtClassSuffix = new System.Windows.Forms.TextBox();
            this.lblClassSuffix = new System.Windows.Forms.Label();
            this.namespaceTextBox = new System.Windows.Forms.TextBox();
            this.namespaceLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LoadGroupBox = new System.Windows.Forms.GroupBox();
            this.chkAutoSelectOnLoad = new System.Windows.Forms.CheckBox();
            this.chkFetchTableDetailsWithLoad = new System.Windows.Forms.CheckBox();
            this.DBMS = new System.Windows.Forms.GroupBox();
            this.databaseTextBox = new System.Windows.Forms.TextBox();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.ddlDBMS = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OutputPathFolderSelectButton = new System.Windows.Forms.Button();
            this.OutputPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GenerateStoredProceduresCheckBox = new System.Windows.Forms.CheckBox();
            this.wcfGroupBox = new System.Windows.Forms.GroupBox();
            this.GenerateWcfLayerServerComponentsCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateWcfLayerClientHelpersCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerateWcfLayerClassesCheckBox = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusBarStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBarErrorMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBarProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusBarActionIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBarDirtyMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFileNew = new System.Windows.Forms.ToolStripButton();
            this.tsbFileOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbFileSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEditLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbEditGenerate = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorkerGenerate = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerLoad = new System.ComponentModel.BackgroundWorker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.loadButton = new System.Windows.Forms.Button();
            this.generateButton = new System.Windows.Forms.Button();
            this.GenerateDataLayerClassesCheckBox = new System.Windows.Forms.CheckBox();
            this.authenticationGroupBox.SuspendLayout();
            this.sqlGroupBox.SuspendLayout();
            this.csGroupBox.SuspendLayout();
            this.LoadGroupBox.SuspendLayout();
            this.DBMS.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.wcfGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // authenticationGroupBox
            // 
            this.authenticationGroupBox.Controls.Add(this.lblSqlAuthentication);
            this.authenticationGroupBox.Controls.Add(this.lblWindowsAuthentication);
            this.authenticationGroupBox.Controls.Add(this.passwordTextBox);
            this.authenticationGroupBox.Controls.Add(this.loginNameTextBox);
            this.authenticationGroupBox.Controls.Add(this.passwordLabel);
            this.authenticationGroupBox.Controls.Add(this.loginNameLabel);
            this.authenticationGroupBox.Controls.Add(this.sqlServerAuthenticationRadioButton);
            resources.ApplyResources(this.authenticationGroupBox, "authenticationGroupBox");
            this.authenticationGroupBox.Name = "authenticationGroupBox";
            this.authenticationGroupBox.TabStop = false;
            this.toolTip1.SetToolTip(this.authenticationGroupBox, resources.GetString("authenticationGroupBox.ToolTip"));
            // 
            // lblSqlAuthentication
            // 
            resources.ApplyResources(this.lblSqlAuthentication, "lblSqlAuthentication");
            this.lblSqlAuthentication.Name = "lblSqlAuthentication";
            // 
            // lblWindowsAuthentication
            // 
            resources.ApplyResources(this.lblWindowsAuthentication, "lblWindowsAuthentication");
            this.lblWindowsAuthentication.Name = "lblWindowsAuthentication";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.passwordTextBox.Name = "passwordTextBox";
            // 
            // loginNameTextBox
            // 
            this.loginNameTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.loginNameTextBox, "loginNameTextBox");
            this.loginNameTextBox.Name = "loginNameTextBox";
            // 
            // passwordLabel
            // 
            resources.ApplyResources(this.passwordLabel, "passwordLabel");
            this.passwordLabel.Name = "passwordLabel";
            // 
            // loginNameLabel
            // 
            resources.ApplyResources(this.loginNameLabel, "loginNameLabel");
            this.loginNameLabel.Name = "loginNameLabel";
            // 
            // sqlServerAuthenticationRadioButton
            // 
            resources.ApplyResources(this.sqlServerAuthenticationRadioButton, "sqlServerAuthenticationRadioButton");
            this.sqlServerAuthenticationRadioButton.Name = "sqlServerAuthenticationRadioButton";
            // 
            // sqlGroupBox
            // 
            resources.ApplyResources(this.sqlGroupBox, "sqlGroupBox");
            this.sqlGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.sqlGroupBox.Controls.Add(this.storedProcedurePrefixTextBox);
            this.sqlGroupBox.Controls.Add(this.storedProcedurePrefixLabel);
            this.sqlGroupBox.Controls.Add(this.grantUserTextBox);
            this.sqlGroupBox.Controls.Add(this.grantUserLabel);
            this.sqlGroupBox.Controls.Add(this.multipleFilesCheckBox);
            this.sqlGroupBox.Name = "sqlGroupBox";
            this.sqlGroupBox.TabStop = false;
            this.toolTip1.SetToolTip(this.sqlGroupBox, resources.GetString("sqlGroupBox.ToolTip"));
            // 
            // storedProcedurePrefixTextBox
            // 
            resources.ApplyResources(this.storedProcedurePrefixTextBox, "storedProcedurePrefixTextBox");
            this.storedProcedurePrefixTextBox.Name = "storedProcedurePrefixTextBox";
            this.toolTip1.SetToolTip(this.storedProcedurePrefixTextBox, resources.GetString("storedProcedurePrefixTextBox.ToolTip"));
            // 
            // storedProcedurePrefixLabel
            // 
            resources.ApplyResources(this.storedProcedurePrefixLabel, "storedProcedurePrefixLabel");
            this.storedProcedurePrefixLabel.Name = "storedProcedurePrefixLabel";
            // 
            // grantUserTextBox
            // 
            resources.ApplyResources(this.grantUserTextBox, "grantUserTextBox");
            this.grantUserTextBox.Name = "grantUserTextBox";
            this.toolTip1.SetToolTip(this.grantUserTextBox, resources.GetString("grantUserTextBox.ToolTip"));
            // 
            // grantUserLabel
            // 
            resources.ApplyResources(this.grantUserLabel, "grantUserLabel");
            this.grantUserLabel.Name = "grantUserLabel";
            // 
            // multipleFilesCheckBox
            // 
            resources.ApplyResources(this.multipleFilesCheckBox, "multipleFilesCheckBox");
            this.multipleFilesCheckBox.Name = "multipleFilesCheckBox";
            this.toolTip1.SetToolTip(this.multipleFilesCheckBox, resources.GetString("multipleFilesCheckBox.ToolTip"));
            // 
            // csGroupBox
            // 
            resources.ApplyResources(this.csGroupBox, "csGroupBox");
            this.csGroupBox.Controls.Add(this.label4);
            this.csGroupBox.Controls.Add(this.ddlLanguage);
            this.csGroupBox.Controls.Add(this.label3);
            this.csGroupBox.Controls.Add(this.ddlSqlHelper);
            this.csGroupBox.Controls.Add(this.GenerateCustomClassTemplateCheckBox);
            this.csGroupBox.Controls.Add(this.txtClassSuffix);
            this.csGroupBox.Controls.Add(this.lblClassSuffix);
            this.csGroupBox.Controls.Add(this.namespaceTextBox);
            this.csGroupBox.Controls.Add(this.namespaceLabel);
            this.csGroupBox.Name = "csGroupBox";
            this.csGroupBox.TabStop = false;
            this.toolTip1.SetToolTip(this.csGroupBox, resources.GetString("csGroupBox.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ddlLanguage
            // 
            this.ddlLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ddlLanguage, "ddlLanguage");
            this.ddlLanguage.Name = "ddlLanguage";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ddlSqlHelper
            // 
            this.ddlSqlHelper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ddlSqlHelper, "ddlSqlHelper");
            this.ddlSqlHelper.Name = "ddlSqlHelper";
            // 
            // GenerateCustomClassTemplateCheckBox
            // 
            resources.ApplyResources(this.GenerateCustomClassTemplateCheckBox, "GenerateCustomClassTemplateCheckBox");
            this.GenerateCustomClassTemplateCheckBox.Name = "GenerateCustomClassTemplateCheckBox";
            this.toolTip1.SetToolTip(this.GenerateCustomClassTemplateCheckBox, resources.GetString("GenerateCustomClassTemplateCheckBox.ToolTip"));
            // 
            // txtClassSuffix
            // 
            resources.ApplyResources(this.txtClassSuffix, "txtClassSuffix");
            this.txtClassSuffix.Name = "txtClassSuffix";
            this.toolTip1.SetToolTip(this.txtClassSuffix, resources.GetString("txtClassSuffix.ToolTip"));
            // 
            // lblClassSuffix
            // 
            resources.ApplyResources(this.lblClassSuffix, "lblClassSuffix");
            this.lblClassSuffix.Name = "lblClassSuffix";
            // 
            // namespaceTextBox
            // 
            resources.ApplyResources(this.namespaceTextBox, "namespaceTextBox");
            this.namespaceTextBox.Name = "namespaceTextBox";
            this.toolTip1.SetToolTip(this.namespaceTextBox, resources.GetString("namespaceTextBox.ToolTip"));
            // 
            // namespaceLabel
            // 
            resources.ApplyResources(this.namespaceLabel, "namespaceLabel");
            this.namespaceLabel.Name = "namespaceLabel";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // LoadGroupBox
            // 
            this.LoadGroupBox.Controls.Add(this.chkAutoSelectOnLoad);
            this.LoadGroupBox.Controls.Add(this.chkFetchTableDetailsWithLoad);
            resources.ApplyResources(this.LoadGroupBox, "LoadGroupBox");
            this.LoadGroupBox.Name = "LoadGroupBox";
            this.LoadGroupBox.TabStop = false;
            this.toolTip1.SetToolTip(this.LoadGroupBox, resources.GetString("LoadGroupBox.ToolTip"));
            // 
            // chkAutoSelectOnLoad
            // 
            resources.ApplyResources(this.chkAutoSelectOnLoad, "chkAutoSelectOnLoad");
            this.chkAutoSelectOnLoad.Name = "chkAutoSelectOnLoad";
            this.toolTip1.SetToolTip(this.chkAutoSelectOnLoad, resources.GetString("chkAutoSelectOnLoad.ToolTip"));
            // 
            // chkFetchTableDetailsWithLoad
            // 
            resources.ApplyResources(this.chkFetchTableDetailsWithLoad, "chkFetchTableDetailsWithLoad");
            this.chkFetchTableDetailsWithLoad.Name = "chkFetchTableDetailsWithLoad";
            this.toolTip1.SetToolTip(this.chkFetchTableDetailsWithLoad, resources.GetString("chkFetchTableDetailsWithLoad.ToolTip"));
            // 
            // DBMS
            // 
            this.DBMS.Controls.Add(this.databaseTextBox);
            this.DBMS.Controls.Add(this.serverTextBox);
            this.DBMS.Controls.Add(this.databaseLabel);
            this.DBMS.Controls.Add(this.label2);
            this.DBMS.Controls.Add(this.serverLabel);
            this.DBMS.Controls.Add(this.ddlDBMS);
            resources.ApplyResources(this.DBMS, "DBMS");
            this.DBMS.Name = "DBMS";
            this.DBMS.TabStop = false;
            this.toolTip1.SetToolTip(this.DBMS, resources.GetString("DBMS.ToolTip"));
            // 
            // databaseTextBox
            // 
            resources.ApplyResources(this.databaseTextBox, "databaseTextBox");
            this.databaseTextBox.Name = "databaseTextBox";
            // 
            // serverTextBox
            // 
            resources.ApplyResources(this.serverTextBox, "serverTextBox");
            this.serverTextBox.Name = "serverTextBox";
            // 
            // databaseLabel
            // 
            resources.ApplyResources(this.databaseLabel, "databaseLabel");
            this.databaseLabel.Name = "databaseLabel";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // serverLabel
            // 
            resources.ApplyResources(this.serverLabel, "serverLabel");
            this.serverLabel.Name = "serverLabel";
            // 
            // ddlDBMS
            // 
            this.ddlDBMS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ddlDBMS, "ddlDBMS");
            this.ddlDBMS.Name = "ddlDBMS";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.OutputPathFolderSelectButton);
            this.groupBox1.Controls.Add(this.OutputPathTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // OutputPathFolderSelectButton
            // 
            resources.ApplyResources(this.OutputPathFolderSelectButton, "OutputPathFolderSelectButton");
            this.OutputPathFolderSelectButton.Name = "OutputPathFolderSelectButton";
            this.OutputPathFolderSelectButton.UseVisualStyleBackColor = true;
            this.OutputPathFolderSelectButton.Click += new System.EventHandler(this.OutputPathFolderSelectButton_Click);
            // 
            // OutputPathTextBox
            // 
            resources.ApplyResources(this.OutputPathTextBox, "OutputPathTextBox");
            this.OutputPathTextBox.Name = "OutputPathTextBox";
            this.toolTip1.SetToolTip(this.OutputPathTextBox, resources.GetString("OutputPathTextBox.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // GenerateStoredProceduresCheckBox
            // 
            resources.ApplyResources(this.GenerateStoredProceduresCheckBox, "GenerateStoredProceduresCheckBox");
            this.GenerateStoredProceduresCheckBox.Name = "GenerateStoredProceduresCheckBox";
            this.toolTip1.SetToolTip(this.GenerateStoredProceduresCheckBox, resources.GetString("GenerateStoredProceduresCheckBox.ToolTip"));
            // 
            // wcfGroupBox
            // 
            resources.ApplyResources(this.wcfGroupBox, "wcfGroupBox");
            this.wcfGroupBox.Controls.Add(this.GenerateWcfLayerServerComponentsCheckBox);
            this.wcfGroupBox.Controls.Add(this.GenerateWcfLayerClientHelpersCheckBox);
            this.wcfGroupBox.Name = "wcfGroupBox";
            this.wcfGroupBox.TabStop = false;
            this.toolTip1.SetToolTip(this.wcfGroupBox, resources.GetString("wcfGroupBox.ToolTip"));
            // 
            // GenerateWcfLayerServerComponentsCheckBox
            // 
            resources.ApplyResources(this.GenerateWcfLayerServerComponentsCheckBox, "GenerateWcfLayerServerComponentsCheckBox");
            this.GenerateWcfLayerServerComponentsCheckBox.Name = "GenerateWcfLayerServerComponentsCheckBox";
            this.toolTip1.SetToolTip(this.GenerateWcfLayerServerComponentsCheckBox, resources.GetString("GenerateWcfLayerServerComponentsCheckBox.ToolTip"));
            // 
            // GenerateWcfLayerClientHelpersCheckBox
            // 
            resources.ApplyResources(this.GenerateWcfLayerClientHelpersCheckBox, "GenerateWcfLayerClientHelpersCheckBox");
            this.GenerateWcfLayerClientHelpersCheckBox.Name = "GenerateWcfLayerClientHelpersCheckBox";
            this.toolTip1.SetToolTip(this.GenerateWcfLayerClientHelpersCheckBox, resources.GetString("GenerateWcfLayerClientHelpersCheckBox.ToolTip"));
            // 
            // GenerateWcfLayerClassesCheckBox
            // 
            resources.ApplyResources(this.GenerateWcfLayerClassesCheckBox, "GenerateWcfLayerClassesCheckBox");
            this.GenerateWcfLayerClassesCheckBox.Name = "GenerateWcfLayerClassesCheckBox";
            this.toolTip1.SetToolTip(this.GenerateWcfLayerClassesCheckBox, resources.GetString("GenerateWcfLayerClassesCheckBox.ToolTip"));
            // 
            // openFileDialog1
            // 
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "SAVE_16.ICO");
            this.imageList1.Images.SetKeyName(2, "documents_16.ico");
            this.imageList1.Images.SetKeyName(3, "folder-open_16.ico");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuHelp});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNew,
            this.menuFileOpen,
            this.menuFileSave,
            this.menuFileSaveAs,
            this.toolStripSeparator1,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            resources.ApplyResources(this.menuFile, "menuFile");
            // 
            // menuFileNew
            // 
            resources.ApplyResources(this.menuFileNew, "menuFileNew");
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // menuFileOpen
            // 
            resources.ApplyResources(this.menuFileOpen, "menuFileOpen");
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // menuFileSave
            // 
            resources.ApplyResources(this.menuFileSave, "menuFileSave");
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            resources.ApplyResources(this.menuFileSaveAs, "menuFileSaveAs");
            this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            resources.ApplyResources(this.menuFileExit, "menuFileExit");
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditLoad,
            this.menuEditGenerate,
            this.toolStripSeparator2,
            this.menuEditProperties});
            this.menuEdit.Name = "menuEdit";
            resources.ApplyResources(this.menuEdit, "menuEdit");
            // 
            // menuEditLoad
            // 
            resources.ApplyResources(this.menuEditLoad, "menuEditLoad");
            this.menuEditLoad.Image = global::DataTierGeneratorPlus.Properties.Resources.Load;
            this.menuEditLoad.Name = "menuEditLoad";
            this.menuEditLoad.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // menuEditGenerate
            // 
            resources.ApplyResources(this.menuEditGenerate, "menuEditGenerate");
            this.menuEditGenerate.Image = global::DataTierGeneratorPlus.Properties.Resources.Generate;
            this.menuEditGenerate.Name = "menuEditGenerate";
            this.menuEditGenerate.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuEditProperties
            // 
            this.menuEditProperties.Name = "menuEditProperties";
            resources.ApplyResources(this.menuEditProperties, "menuEditProperties");
            this.menuEditProperties.Click += new System.EventHandler(this.menuEditProperties_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            resources.ApplyResources(this.menuHelp, "menuHelp");
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            resources.ApplyResources(this.menuHelpAbout, "menuHelpAbout");
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarStatusMessage,
            this.StatusBarErrorMessage,
            this.StatusBarProgressBar,
            this.StatusBarActionIcon,
            this.StatusBarDirtyMessage});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // StatusBarStatusMessage
            // 
            this.StatusBarStatusMessage.ForeColor = System.Drawing.Color.Green;
            this.StatusBarStatusMessage.Name = "StatusBarStatusMessage";
            resources.ApplyResources(this.StatusBarStatusMessage, "StatusBarStatusMessage");
            // 
            // StatusBarErrorMessage
            // 
            this.StatusBarErrorMessage.AutoToolTip = true;
            this.StatusBarErrorMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusBarErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.StatusBarErrorMessage.Name = "StatusBarErrorMessage";
            resources.ApplyResources(this.StatusBarErrorMessage, "StatusBarErrorMessage");
            this.StatusBarErrorMessage.Spring = true;
            // 
            // StatusBarProgressBar
            // 
            this.StatusBarProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.StatusBarProgressBar.Name = "StatusBarProgressBar";
            resources.ApplyResources(this.StatusBarProgressBar, "StatusBarProgressBar");
            this.StatusBarProgressBar.Value = 10;
            // 
            // StatusBarActionIcon
            // 
            this.StatusBarActionIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.StatusBarActionIcon, "StatusBarActionIcon");
            this.StatusBarActionIcon.Name = "StatusBarActionIcon";
            // 
            // StatusBarDirtyMessage
            // 
            this.StatusBarDirtyMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.StatusBarDirtyMessage, "StatusBarDirtyMessage");
            this.StatusBarDirtyMessage.Name = "StatusBarDirtyMessage";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFileNew,
            this.tsbFileOpen,
            this.tsbFileSave,
            this.toolStripSeparator,
            this.tsbEditLoad,
            this.tsbEditGenerate});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbFileNew
            // 
            this.tsbFileNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbFileNew, "tsbFileNew");
            this.tsbFileNew.Name = "tsbFileNew";
            this.tsbFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // tsbFileOpen
            // 
            this.tsbFileOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbFileOpen, "tsbFileOpen");
            this.tsbFileOpen.Name = "tsbFileOpen";
            this.tsbFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // tsbFileSave
            // 
            this.tsbFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbFileSave, "tsbFileSave");
            this.tsbFileSave.Name = "tsbFileSave";
            this.tsbFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // tsbEditLoad
            // 
            this.tsbEditLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbEditLoad, "tsbEditLoad");
            this.tsbEditLoad.Image = global::DataTierGeneratorPlus.Properties.Resources.Load;
            this.tsbEditLoad.Name = "tsbEditLoad";
            this.tsbEditLoad.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // tsbEditGenerate
            // 
            this.tsbEditGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbEditGenerate, "tsbEditGenerate");
            this.tsbEditGenerate.Image = global::DataTierGeneratorPlus.Properties.Resources.Generate;
            this.tsbEditGenerate.Name = "tsbEditGenerate";
            this.tsbEditGenerate.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // backgroundWorkerGenerate
            // 
            this.backgroundWorkerGenerate.WorkerReportsProgress = true;
            this.backgroundWorkerGenerate.WorkerSupportsCancellation = true;
            this.backgroundWorkerGenerate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGenerate_DoWork);
            this.backgroundWorkerGenerate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerGenerate_ProgressChanged);
            this.backgroundWorkerGenerate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerGenerate_RunWorkerCompleted);
            // 
            // backgroundWorkerLoad
            // 
            this.backgroundWorkerLoad.WorkerReportsProgress = true;
            this.backgroundWorkerLoad.WorkerSupportsCancellation = true;
            this.backgroundWorkerLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoad_DoWork);
            this.backgroundWorkerLoad.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerLoad_ProgressChanged);
            this.backgroundWorkerLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLoad_RunWorkerCompleted);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // loadButton
            // 
            resources.ApplyResources(this.loadButton, "loadButton");
            this.loadButton.Image = global::DataTierGeneratorPlus.Properties.Resources.Load;
            this.loadButton.Name = "loadButton";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // generateButton
            // 
            resources.ApplyResources(this.generateButton, "generateButton");
            this.generateButton.Image = global::DataTierGeneratorPlus.Properties.Resources.Generate;
            this.generateButton.Name = "generateButton";
            this.generateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // GenerateDataLayerClassesCheckBox
            // 
            resources.ApplyResources(this.GenerateDataLayerClassesCheckBox, "GenerateDataLayerClassesCheckBox");
            this.GenerateDataLayerClassesCheckBox.Name = "GenerateDataLayerClassesCheckBox";
            // 
            // GeneratorViewer
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.GenerateDataLayerClassesCheckBox);
            this.Controls.Add(this.GenerateWcfLayerClassesCheckBox);
            this.Controls.Add(this.GenerateStoredProceduresCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.DBMS);
            this.Controls.Add(this.LoadGroupBox);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.authenticationGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.wcfGroupBox);
            this.Controls.Add(this.sqlGroupBox);
            this.Controls.Add(this.csGroupBox);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GeneratorViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneratorViewer_FormClosing);
            this.Load += new System.EventHandler(this.GeneratorViewer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GeneratorViewer_KeyDown);
            this.authenticationGroupBox.ResumeLayout(false);
            this.authenticationGroupBox.PerformLayout();
            this.sqlGroupBox.ResumeLayout(false);
            this.sqlGroupBox.PerformLayout();
            this.csGroupBox.ResumeLayout(false);
            this.csGroupBox.PerformLayout();
            this.LoadGroupBox.ResumeLayout(false);
            this.DBMS.ResumeLayout(false);
            this.DBMS.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.wcfGroupBox.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox authenticationGroupBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox loginNameTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label loginNameLabel;
        private System.Windows.Forms.CheckBox sqlServerAuthenticationRadioButton;
        private System.Windows.Forms.GroupBox sqlGroupBox;
        private System.Windows.Forms.TextBox grantUserTextBox;
        private System.Windows.Forms.Label grantUserLabel;
        private System.Windows.Forms.CheckBox multipleFilesCheckBox;
        private System.Windows.Forms.GroupBox csGroupBox;
        private System.Windows.Forms.TextBox namespaceTextBox;
        private System.Windows.Forms.Label namespaceLabel;
        private System.Windows.Forms.TextBox storedProcedurePrefixTextBox;
        private System.Windows.Forms.Label storedProcedurePrefixLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblClassSuffix;
        private System.Windows.Forms.TextBox txtClassSuffix;
        private System.Windows.Forms.GroupBox LoadGroupBox;
        private System.Windows.Forms.CheckBox chkAutoSelectOnLoad;
        private System.Windows.Forms.CheckBox chkFetchTableDetailsWithLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox DBMS;
        private System.Windows.Forms.ComboBox ddlDBMS;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem menuFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarStatusMessage;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarErrorMessage;
        private System.Windows.Forms.ToolStripProgressBar StatusBarProgressBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFileNew;
        private System.Windows.Forms.ToolStripButton tsbFileOpen;
        private System.Windows.Forms.ToolStripButton tsbFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarActionIcon;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarDirtyMessage;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox GenerateCustomClassTemplateCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OutputPathFolderSelectButton;
        private System.Windows.Forms.TextBox databaseTextBox;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlLanguage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddlSqlHelper;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGenerate;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoad;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox GenerateStoredProceduresCheckBox;
        private System.Windows.Forms.GroupBox wcfGroupBox;
        private System.Windows.Forms.CheckBox GenerateWcfLayerServerComponentsCheckBox;
        private System.Windows.Forms.CheckBox GenerateWcfLayerClientHelpersCheckBox;
        private System.Windows.Forms.Label lblWindowsAuthentication;
        private System.Windows.Forms.Label lblSqlAuthentication;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuEditProperties;
        internal System.Windows.Forms.ToolStripMenuItem menuEditLoad;
        internal System.Windows.Forms.Button generateButton;
        internal System.Windows.Forms.Button loadButton;
        internal System.Windows.Forms.ToolStripMenuItem menuEditGenerate;
        internal System.Windows.Forms.ToolStripButton tsbEditLoad;
        internal System.Windows.Forms.ToolStripButton tsbEditGenerate;
        internal System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.TextBox OutputPathTextBox;
        private System.Windows.Forms.CheckBox GenerateWcfLayerClassesCheckBox;
        private System.Windows.Forms.CheckBox GenerateDataLayerClassesCheckBox;
    }
}
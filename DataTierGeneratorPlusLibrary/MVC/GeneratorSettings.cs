using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Ssepan.Application;
using Ssepan.Utility;

namespace DataTierGeneratorPlusLibrary
{
    /// <summary>
    /// persisted model; run-time model depends on this
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class GeneratorSettings :
        SettingsBase
    {
        #region Declarations
        public new const String FILE_TYPE_EXTENSION = "dtgp"; //"xml";
        public new const String FILE_TYPE_NAME = "DataTierGeneratorPlusLibraryfile";
        public new const String FILE_TYPE_DESCRIPTION = "DataTierGeneratorPlusLibrary Settings File";
        #endregion Declarations

        #region Constructors
        public GeneratorSettings()
        {
            FileTypeExtension = FILE_TYPE_EXTENSION;
            FileTypeName = FILE_TYPE_NAME;
            FileTypeDescription = FILE_TYPE_DESCRIPTION;
            SerializeAs = SerializationFormat.Xml;//default
        }

        //public Settings
        //(
        //    Int32 someInt,
        //    Boolean someBoolean,
        //    String someString
        //) :
        //    this()
        //{
        //    SomeInt = someInt;
        //    SomeBoolean = someBoolean;
        //    SomeString = someString;
        //}
        #endregion Constructors

        #region IDisposable support
        ~GeneratorSettings()
        {
            Dispose(false);
        }

        //inherited; override if additional cleanup needed
        protected override void Dispose(Boolean disposeManagedResources)
        {
            // process only if mananged and unmanaged resources have
            // not been disposed of.
            if (!disposed)
            {
                try
                {
                    //Resources not disposed
                    if (disposeManagedResources)
                    {
                        // dispose managed resources
                    }

                    disposed = true;
                }
                finally
                {
                    // dispose unmanaged resources
                    base.Dispose(disposeManagedResources);
                }
            }
            else
            {
                //Resources already disposed
            }
        }
        #endregion

        #region IEquatable<ISettings>
        /// <summary>
        /// Compare property values of two specified Settings objects.
        /// </summary>
        /// <param name="anotherSettings"></param>
        /// <returns></returns>
        public override Boolean Equals(ISettingsComponent other)
        {
            Boolean returnValue = default(Boolean);
            GeneratorSettings otherSettings = default(GeneratorSettings);

            try
            {
                otherSettings = other as GeneratorSettings;

                if (this == otherSettings)
                {
                    returnValue = true;
                }
                else
                {
                    if (!base.Equals(other))
                    {
                        returnValue = false;
                    }
                    else if (this.AutoSelectOnLoad != otherSettings.AutoSelectOnLoad)
                    {
                        returnValue = false;
                    }
                    else if (this.CreateMultipleFiles != otherSettings.CreateMultipleFiles)
                    {
                        returnValue = false;
                    }
                    else if (this.ClassSuffix != otherSettings.ClassSuffix)
                    {
                        returnValue = false;
                    }
                    else if (this.Database != otherSettings.Database)
                    {
                        returnValue = false;
                    }
                    else if (this.DBMS != otherSettings.DBMS)
                    {
                        returnValue = false;
                    }
                    else if (this.FetchTableDetailsWithLoad != otherSettings.FetchTableDetailsWithLoad)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateCustomClassTemplate != otherSettings.GenerateCustomClassTemplate)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateDataLayerClasses != otherSettings.GenerateDataLayerClasses)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateStoredProcedures != otherSettings.GenerateStoredProcedures)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateWcfLayerClasses != otherSettings.GenerateWcfLayerClasses)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateWcfLayerClientHelpers != otherSettings.GenerateWcfLayerClientHelpers)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateWcfLayerServerComponents != otherSettings.GenerateWcfLayerServerComponents)
                    {
                        returnValue = false;
                    }
                    else if (this.GrantUser != otherSettings.GrantUser)
                    {
                        returnValue = false;
                    }
                    else if (this.Language != otherSettings.Language)
                    {
                        returnValue = false;
                    }
                    else if (this.Namespace != otherSettings.Namespace)
                    {
                        returnValue = false;
                    }
                    else if (this.OutputPath != otherSettings.OutputPath)
                    {
                        returnValue = false;
                    }
                    else if (this.Password != otherSettings.Password)
                    {
                        returnValue = false;
                    }
                    else if (this.Server != otherSettings.Server)
                    {
                        returnValue = false;
                    }
                    else if (this.SPPrefix != otherSettings.SPPrefix)
                    {
                        returnValue = false;
                    }
                    else if (this.SQLAuthentication != otherSettings.SQLAuthentication)
                    {
                        returnValue = false;
                    }
                    else if (this.SQLHelper != otherSettings.SQLHelper)
                    {
                        returnValue = false;
                    }
                    else if (this.SQLLogin != otherSettings.SQLLogin)
                    {
                        returnValue = false;
                    }
                    else
                    {
                        returnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }

            return returnValue;
        }
        #endregion IEquatable<ISettings>

        #region Properties
        [XmlIgnore]
        public override Boolean Dirty
        {
            get
            {
                Boolean returnValue = default(Boolean);

                try
                {
                    if (base.Dirty)
                    {
                        returnValue = true;
                    }
                    else if (_Server != __Server)
                    {
                        returnValue = true;
                    }
                    else if (_Database != __Database)
                    {
                        returnValue = true;
                    }
                    else if (_SQLAuthentication != __SQLAuthentication)
                    {
                        returnValue = true;
                    }
                    else if (_SQLLogin != __SQLLogin)
                    {
                        returnValue = true;
                    }
                    else if (_Password != __Password)
                    {
                        returnValue = true;
                    }
                    else if (_GrantUser != __GrantUser)
                    {
                        returnValue = true;
                    }
                    else if (_SPPrefix != __SPPrefix)
                    {
                        returnValue = true;
                    }
                    else if (_CreateMultipleFiles != __CreateMultipleFiles)
                    {
                        returnValue = true;
                    }
                    else if (_Namespace != __Namespace)
                    {
                        returnValue = true;
                    }
                    else if (_ClassSuffix != __ClassSuffix)
                    {
                        returnValue = true;
                    }
                    else if (_Language != __Language)
                    {
                        returnValue = true;
                    }
                    else if (_SQLHelper != __SQLHelper)
                    {
                        returnValue = true;
                    }
                    else if (_AutoSelectOnLoad != __AutoSelectOnLoad)
                    {
                        returnValue = true;
                    }
                    else if (_FetchTableDetailsWithLoad != __FetchTableDetailsWithLoad)
                    {
                        returnValue = true;
                    }
                    else if (_DBMS != __DBMS)
                    {
                        returnValue = true;
                    }
                    else if (_GenerateCustomClassTemplate != __GenerateCustomClassTemplate)
                    {
                        returnValue = true;
                    }
                    else if (_OutputPath != __OutputPath)
                    {
                        returnValue = true;
                    }
                    else if (_GenerateDataLayerClasses != __GenerateDataLayerClasses)
                    {
                        returnValue = true;
                    }
                    //else if (_Version != __Version)
                    //{
                    //    returnValue = true;
                    //}
                    else if (_GenerateStoredProcedures != __GenerateStoredProcedures)
                    {
                        returnValue = true;
                    }
                    else if (_GenerateWcfLayerClasses != __GenerateWcfLayerClasses)
                    {
                        returnValue = true;
                    }
                    else if (_GenerateWcfLayerServerComponents != __GenerateWcfLayerServerComponents)
                    {
                        returnValue = true;
                    }
                    else if (_GenerateWcfLayerClientHelpers != __GenerateWcfLayerClientHelpers)
                    {
                        returnValue = true;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                    throw;
                }

                return returnValue;
            }
        }

        #region Persisted Properties
        private String __DBMS = "MSSQL"; //String.Empty;
        private String _DBMS = "MSSQL"; //String.Empty;
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Database type to use (i.e. Sql Server, Oracle, etc.)"),
        CategoryAttribute("DBMS"),
        DefaultValueAttribute("MSSQL")]
		public String DBMS
		{
			get	{	return _DBMS;	}
            set
            {
                _DBMS = value;
                this.OnPropertyChanged("DBMS");
            }
        }

        private String __Server = "(local)"; //String.Empty;
        private String _Server = "(local)"; //String.Empty;
        //DefaultValueAttribute(val)]
        [DescriptionAttribute("Database Server name"),
        CategoryAttribute("DBMS"),
        DefaultValueAttribute("(local)")]
        public String Server
		{
			get	{	return _Server;	}
			set	
            {	
                _Server = value;
                this.OnPropertyChanged("Server");
            }
		}

        private String __Database = String.Empty;
        private String _Database = String.Empty;
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Database (catalog) name"),
        CategoryAttribute("DBMS"),
        DefaultValueAttribute(null)]
		public  String Database
		{
			get	{	return _Database;	}
			set	
            {
                _Database = value;
                this.OnPropertyChanged("Database");
            }
		}

        private Boolean __SQLAuthentication = default(Boolean);
        private Boolean _SQLAuthentication = default(Boolean);
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Use SQL Authentication, instead of Windows (default)"),
        CategoryAttribute("Authentication"),
        DefaultValueAttribute(false)]
		public  Boolean SQLAuthentication
		{
			get	{	return _SQLAuthentication;	}
            set
            {
                _SQLAuthentication = value;
                this.OnPropertyChanged("SQLAuthentication");
            }
        }
        
        private String __SQLLogin = String.Empty;
        private String _SQLLogin = String.Empty;
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Database user name"),
        CategoryAttribute("Authentication"),
        DefaultValueAttribute(null)]
		public  String SQLLogin
		{
			get	{	return _SQLLogin;	}
            set
            {
                _SQLLogin = value;
                this.OnPropertyChanged("SQLLogin");
            }
        }

        private String __Password = String.Empty;
        private String _Password = String.Empty;
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Database password"),
        CategoryAttribute("Authentication"),
        DefaultValueAttribute(null)]
		public  String Password
		{
			get	{	return _Password;	}
            set
            {
                _Password = value;
                this.OnPropertyChanged("Password");
            }
        }

        private Boolean __AutoSelectOnLoad = default(Boolean);
        private Boolean _AutoSelectOnLoad = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Automatically select tables as the table-list is loaded."),
        CategoryAttribute("Load"),
        DefaultValueAttribute(false)]
		public  Boolean AutoSelectOnLoad
		{
			get	{	return _AutoSelectOnLoad;	}
            set
            {
                _AutoSelectOnLoad = value;
                this.OnPropertyChanged("AutoSelectOnLoad");
            }
        }

        private Boolean __FetchTableDetailsWithLoad = default(Boolean);
        private Boolean _FetchTableDetailsWithLoad = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Load details about the table (i.e. - keys, columns, etc.) as the table-list is loaded."),
        CategoryAttribute("Load"),
        DefaultValueAttribute(false)]
		public  Boolean FetchTableDetailsWithLoad
		{
			get	{	return _FetchTableDetailsWithLoad;	}
            set
            {
                _FetchTableDetailsWithLoad = value;
                this.OnPropertyChanged("FetchTableDetailsWithLoad");
            }
        }

        private String __OutputPath = String.Empty;
        private String _OutputPath = String.Empty;
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Base path for output"),
        CategoryAttribute("Paths"),
        DefaultValueAttribute(null)]
        public String OutputPath
		{
            get { return _OutputPath; }
            set
            {
                _OutputPath = value;
                this.OnPropertyChanged("OutputPath");
            }
        }

        private Boolean __GenerateStoredProcedures = default(Boolean);
        private Boolean _GenerateStoredProcedures = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate stored procedures"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(false)]
        public Boolean GenerateStoredProcedures
        {
            get { return _GenerateStoredProcedures; }
            set
            {
                _GenerateStoredProcedures = value;
                this.OnPropertyChanged("GenerateStoredProcedures");
            }
        }

        private String __GrantUser = String.Empty;
        private String _GrantUser = String.Empty;
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("User name to be applied to stored procedures' rights"),
        CategoryAttribute("Stored Procedures"),
        DefaultValueAttribute(null)]
		public  String GrantUser
		{
			get	{	return _GrantUser;	}
            set
            {
                _GrantUser = value;
                this.OnPropertyChanged("GrantUser");
            }
        }

        private String __SPPrefix = String.Empty;
        private String _SPPrefix = String.Empty;
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Prefix to be generated before stored procedure names"),
        CategoryAttribute("Stored Procedures"),
        DefaultValueAttribute(null)]
		public  String SPPrefix
		{
			get	{	return _SPPrefix;	}
            set
            {
                _SPPrefix = value;
                this.OnPropertyChanged("SPPrefix");
            }
        }

        private Boolean __CreateMultipleFiles = default(Boolean);
        private Boolean _CreateMultipleFiles = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate one file per stored procedure, instead of one file for all stored procedures per table (default)"),
        CategoryAttribute("Stored Procedures"),
        DefaultValueAttribute(false)]
		public  Boolean CreateMultipleFiles
		{
			get	{	return _CreateMultipleFiles;	}
            set
            {
                _CreateMultipleFiles = value;
                this.OnPropertyChanged("CreateMultipleFiles");
            }
        }

        private Boolean __GenerateDataLayerClasses = default(Boolean);
        private Boolean _GenerateDataLayerClasses = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate data access classes"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(false)]
        public Boolean GenerateDataLayerClasses
		{
            get { return _GenerateDataLayerClasses; }
            set
            {
                _GenerateDataLayerClasses = value;
                this.OnPropertyChanged("GenerateDataLayerClasses");
            }
        }

        private String __Namespace = String.Empty;
        private String _Namespace = String.Empty;
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Namespace to use for generated classes"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(null)]
		public  String Namespace
		{
			get	{	return _Namespace;	}
            set
            {
                _Namespace = value;
                this.OnPropertyChanged("Namespace");
            }
        }

        private String __ClassSuffix = String.Empty;
        private String _ClassSuffix = String.Empty;
        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Suffix to be generated after class names"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(null)]
		public  String ClassSuffix
		{
			get	{	return _ClassSuffix;	}
            set
            {
                _ClassSuffix = value;
                this.OnPropertyChanged("ClassSuffix");
            }
        }

        private String __Language = "C#"; //String.Empty;
        private String _Language = "C#"; //String.Empty;
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute(".Net language that will be generated"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute("C#")]
		public  String Language
		{
			get	{	return _Language;	}
            set
            {
                _Language = value;
                this.OnPropertyChanged("Language");
            }
        }

        private String __SQLHelper = "BuiltIn"; //String.Empty;
        private String _SQLHelper = "BuiltIn"; //String.Empty;
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("SQL helper library to use"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute("BuiltIn")]
		public  String SQLHelper
		{
			get { return _SQLHelper;    }
            set
            {
                _SQLHelper = value;
                this.OnPropertyChanged("SQLHelper");
            }
        }

        private Boolean __GenerateCustomClassTemplate = default(Boolean);
        private Boolean _GenerateCustomClassTemplate = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate empty partial classes for custom code"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(false)]
        public Boolean GenerateCustomClassTemplate
		{
            get { return _GenerateCustomClassTemplate; }
            set
            {
                _GenerateCustomClassTemplate = value;
                this.OnPropertyChanged("GenerateCustomClassTemplate");
            }
        }

        private Boolean __GenerateWcfLayerClasses = default(Boolean);
        private Boolean _GenerateWcfLayerClasses = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate WCF classes"),
        CategoryAttribute("WCF Layers"),
        DefaultValueAttribute(false)]
        public Boolean GenerateWcfLayerClasses
        {
            get { return _GenerateWcfLayerClasses; }
            set
            {
                _GenerateWcfLayerClasses = value;
                this.OnPropertyChanged("GenerateWcfLayerClasses");
            }
        }

        private Boolean __GenerateWcfLayerServerComponents = default(Boolean);
        private Boolean _GenerateWcfLayerServerComponents = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate WCF server classes and configuration"),
        CategoryAttribute("WCF Layers"),
        DefaultValueAttribute(false)]
        public Boolean GenerateWcfLayerServerComponents
        {
            get { return _GenerateWcfLayerServerComponents; }
            set
            {
                _GenerateWcfLayerServerComponents = value;
                this.OnPropertyChanged("GenerateWcfLayerServerComponents");
            }
        }

        private Boolean __GenerateWcfLayerClientHelpers = default(Boolean);
        private Boolean _GenerateWcfLayerClientHelpers = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate WCF client proxy wrapper classes"),
        CategoryAttribute("WCF Layers"),
        DefaultValueAttribute(false)]
        public Boolean GenerateWcfLayerClientHelpers
        {
            get { return _GenerateWcfLayerClientHelpers; }
            set
            {
                _GenerateWcfLayerClientHelpers = value;
                this.OnPropertyChanged("GenerateWcfLayerClientHelpers");
            }
        }

        private String __Version = String.Empty;
        private String _Version = String.Empty;
        //[DataObjectFieldAttribute(false, false, false)]
        [ReadOnly(true)]
        [DescriptionAttribute("Application major version"),
        CategoryAttribute("Misc"),
        DefaultValueAttribute(null)]
        public String Version
        {
            get { return _Version; }
            set
            {
                _Version = value;
                this.OnPropertyChanged("Version");
            }
        }
        #endregion Persisted Properties
        #endregion Properties

        #region Methods
        /// <summary>
        /// Copies property values from source working fields to detination working fields, then optionally syncs destination.
        /// <param name="destinationSettings"></param>
        /// <param name="sync"></param>
        /// </summary>
        public override void CopyTo(ISettingsComponent destination, Boolean sync)
        {
            GeneratorSettings destinationSettings = default(GeneratorSettings);

            try
            {
                destinationSettings = destination as GeneratorSettings;

                destinationSettings.Server = this.Server;
                destinationSettings.Database = this.Database;
                destinationSettings.SQLAuthentication = this.SQLAuthentication;
                destinationSettings.SQLLogin = this.SQLLogin;
                destinationSettings.Password = this.Password;
                destinationSettings.GrantUser = this.GrantUser;
                destinationSettings.SPPrefix = this.SPPrefix;
                destinationSettings.CreateMultipleFiles = this.CreateMultipleFiles;
                destinationSettings.Namespace = this.Namespace;
                destinationSettings.ClassSuffix = this.ClassSuffix;
                //destinationSettings.BaseClassSealed = this.BaseClassSealed;
                destinationSettings.Language = this.Language;
                destinationSettings.SQLHelper = this.SQLHelper;
                destinationSettings.AutoSelectOnLoad = this.AutoSelectOnLoad;
                destinationSettings.FetchTableDetailsWithLoad = this.FetchTableDetailsWithLoad;
                destinationSettings.DBMS = this.DBMS;
                destinationSettings.GenerateCustomClassTemplate = this.GenerateCustomClassTemplate;
                destinationSettings.OutputPath = this.OutputPath;
                destinationSettings.GenerateDataLayerClasses = this.GenerateDataLayerClasses;
                //destinationSettings.Version = this.Version;
                destinationSettings.GenerateStoredProcedures = this.GenerateStoredProcedures;
                destinationSettings.GenerateWcfLayerClasses = this.GenerateWcfLayerClasses;
                destinationSettings.GenerateWcfLayerServerComponents = this.GenerateWcfLayerServerComponents;
                destinationSettings.GenerateWcfLayerClientHelpers = this.GenerateWcfLayerClientHelpers;

                base.CopyTo(destination, sync);//also checks and optionally performs sync
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        /// <summary>
        /// Syncs property values from working fields to reference fields.
        /// </summary>
        public override void Sync()
        {
            try
            {
                __Server = _Server;
                __Database = _Database;
                __SQLAuthentication = _SQLAuthentication;
                __SQLLogin = _SQLLogin;
                __Password = _Password;
                __GrantUser = _GrantUser;
                __SPPrefix = _SPPrefix;
                __CreateMultipleFiles = _CreateMultipleFiles;
                __Namespace = _Namespace;
                __ClassSuffix = _ClassSuffix;
                __Language = _Language;
                __SQLHelper = _SQLHelper;
                __AutoSelectOnLoad = _AutoSelectOnLoad;
                __FetchTableDetailsWithLoad = _FetchTableDetailsWithLoad;
                __DBMS = _DBMS;
                __GenerateCustomClassTemplate = _GenerateCustomClassTemplate;
                __OutputPath = _OutputPath;
                __GenerateDataLayerClasses = _GenerateDataLayerClasses;
                __Version = _Version;
                __GenerateStoredProcedures = _GenerateStoredProcedures;
                __GenerateWcfLayerClasses = _GenerateWcfLayerClasses;
                __GenerateWcfLayerServerComponents = _GenerateWcfLayerServerComponents;
                __GenerateWcfLayerClientHelpers = _GenerateWcfLayerClientHelpers;

                base.Sync();

                if (Dirty)
                {
                    throw new ApplicationException("Sync failed.");
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        //#region private methods
        //private String ApplyDefaultString(String field, String settingName)
        //{
        //    String returnValue = field;
        //    // initialize Settings object 
        //    AppSettingsReader appSettings = default(AppSettingsReader);

        //    try
        //    {
        //        if (field == null || field == String.Empty)
        //        {
        //            appSettings = new System.Configuration.AppSettingsReader();
        //            returnValue = (String)appSettings.GetValue(settingName, typeof(String));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
        //    }
        //    finally
        //    {
        //        appSettings = null;
        //    }
        //    return returnValue;
        //}
        //#endregion private methods
        #endregion Methods
    }
}

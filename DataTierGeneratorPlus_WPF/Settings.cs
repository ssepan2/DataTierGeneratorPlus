using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using Ssepan.Patterns;
using Ssepan.Utility;

namespace DataTierGeneratorPlus
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
    [Serializable()]
    public class Settings :
        IDisposable,
        INotifyPropertyChanged,
        IEquatable<Settings>
    {
        #region declarations
        private Boolean disposed = false;

        public const String FILE_NEW = "(new)";
        public const String FILE_TYPE_EXTENSION = "dtgp"; //"xml";
        public const String FILE_TYPE_NAME = "datatiergeneratorplusfile";
        public const String FILE_TYPE_DESCRIPTION = "DataTierGeneratorPlus Settings File";

        public static List<String> LanguageList = new List<String>();
        public static List<String> DbmsList = new List<String>();
        public static List<String> SQLHelperList = new List<String>();

        #region Customizations
        //LANGUAGE CUSTOMIZATION: 1 of 5 -- define new language ID constant here (recommend String of 2-10 characters)
		public  const String Language_CSharp = "C#";
		public  const String Language_VisualBasic = "VB";
		//public const String Language_JSharp = "J#";
		//public const String Language_CPlusPlus = "C++";
		//public const String Language_Xxxx = "Xxxx";
		
		//LANGUAGE CUSTOMIZATION: 2 of 5 -- write new set of language code modules...
		//
		//XxDatabaseUtility.txt						-	DB Library used for Built-In option.
		//XxGeneratorBuiltIn.cs					-	code generator for Built-In option.  Must inherit from VsGenerator, which implements IVsGenerator interface.
		//XxGeneratorDotNetNuke.cs			-	code generator for DotNetNuke option.  Must inherit from VsGenerator, which implements IVsGenerator interface.
		//XxGeneratorEnterpriseLibrary.cs	-	code generator for Enterprise Library option.  Must inherit from VsGenerator, which implements IVsGenerator interface.
		//XxUtility.cs										-	language-specific utility library.  Must inherit from VsUtility, which implements IVsUtility interface.
		//
		//...and include them in the project.  "Xx" represents artibrary 2 letter language prefix, defined by you. 
	
		//LANGUAGE CUSTOMIZATION: 3 of 5 -- go to VsGeneratorFactory.cs and add a pair of Cases to Switch statement for language.-->
	
		//LANGUAGE CUSTOMIZATION: 4 of 5 -- go to VsUtilityFactory.cs and add a Case to Switch statement for language.-->
		

		//DBMS CUSTOMIZATION: 1 of 6 -- define new DBMS ID constant here (recommend String of 2-10 characters)
		//public const String DBMS_MSAccess = "MSAccess";
		public  const String DBMS_MSSQL = "MSSQL";
		//public const String DBMS_MySQL = "MySQL";
		//public const String DBMS_Oracle = "Oracle"; //http://builder.com.com/5100-6389-1049889.html#Listing%20B
		//public const String DBMS_Sybase = "Sybase";
		//public const String DBMS_Yyyy = "Yyyy";

		//DBMS CUSTOMIZATION: 2 of 6 -- write new set of dbms-specific table- and column-metadata lookup queries...
		//
		//YyyColumnMetadata.sql					-	.
		//YyyTableMetadata.sql						-	.
		//YyyUserCreate.sql						-	.
		//
		//...and include them in the project, with Build Action set to Embedded Resource.  "Yyy" represents artibrary 3 letter dbms prefix, defined by you. 
		
		//DBMS CUSTOMIZATION: 3 of 6 -- write new set of dbms code modules...
		//
		//YyyGenerator.cs							-	code generator.  Must inherit from DbGenerator, which implements IDbGenerator interface.
		//YyyUtility.cs									-	dbms-specific utility library.  Must inherit from DbUtility, which implements IDbUtility interface.
		//
		//...and include them in the project (recommend working from a copy).  "Yyy" represents artibrary 3 letter dbms prefix, defined by you. 
		
		//DBMS CUSTOMIZATION: 4 of 6 -- go to DbGeneratorFactory.cs and add a Case to Switch statement for DBMS.-->
	
		//DBMS CUSTOMIZATION: 5 of 6 -- go to DbUtilityFactory.cs and add a Case to Switch statement for DBMS.-->

        public  const String SQLHelper_BuiltIn = "BuiltIn";
        //public const String SQLHelper_EnterpriseLibrary = "EnterpriseLibrary";
        // public const String SQLHelper_DotNetNuke = "DotNetNuke";
        #endregion Customizations
        #endregion declarations

        #region constructors
        //init static members
        static Settings()
        {
			//LANGUAGE CUSTOMIZATION: 5 of 5 -- add new language ID constant to list 
			LanguageList.Add(Language_CSharp);
            LanguageList.Add(Language_VisualBasic);
            //LanguageList.Add(Language_JSharp);//<--this option commented out until Microsoft decides to make their implementations of J# a little more consistent.--SJS, 9/8/2006.
            //LanguageList.Add(Language_CPlusPlus);//<--this option commented out until output of C++ generator can be corrected by a C++ expert.--SJS, 1/5/2006.
            //LanguageList.Add(Language_Xxxx);
		
			//DBMS CUSTOMIZATION: 6 of 6 -- add new DBMS ID constant to list 
            //DbmsList.Add(DBMS_MSAccess);
            DbmsList.Add(DBMS_MSSQL);
            //DbmsList.Add(DBMS_MySQL);
            //DbmsList.Add(DBMS_Oracle);
            //DbmsList.Add(DBMS_Sybase);
            //DbmsList.Add(DBMS_Yyyy);

            SQLHelperList.Add(SQLHelper_BuiltIn);
            //SQLHelperList.Add(SQLHelper_EnterpriseLibrary);
            //SQLHelperList.Add(SQLHelper_DotNetNuke);
        }

        //init non-static members
		public Settings()
		{
            try
            {
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
       #endregion

        #region IDisposable support
        ~Settings()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            // dispose of the managed and unmanaged resources
            Dispose(true);

            // tell the GC that the Finalize process no longer needs
            // to be run for this object.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposeManagedResources)
        {
            // process only if mananged and unmanaged resources have
            // not been disposed of.
            if (!this.disposed)
            {
                //Resources not disposed
                if (disposeManagedResources)
                {
                    // dispose managed resources
                }
                // dispose unmanaged resources
                disposed = true;
            }
            else
            {
                //Resources already disposed
            }
        }
        #endregion

        #region INotifyPropertyChanged support
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(String propertyName)
        {
            try
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
#if debug
                    Log.Write(
                        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name,
                        Log.FormatEntry(String.Format("PropertyChanged: {0}", propertyName), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name),
                        System.Diagnostics.EventLogEntryType.Information,
                        99);
#endif
                }
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
        }
        #endregion INotifyPropertyChanged support

        #region IEquatable<Settings> Members

        /// <summary>
        /// Compare property values of two specified Settings objects.
        /// </summary>
        /// <param name="anotherSettings"></param>
        /// <returns></returns>
        public Boolean Equals(Settings other)
        {
            Boolean returnValue = default(Boolean);

            try
            {
                if (this == other)
                {
                    throw new ApplicationException(String.Format("Comparing value of settings against itself"));
                }

                if (this.AutoSelectOnLoad != other.AutoSelectOnLoad)
                {
                    returnValue = false;
                }
                else if (this.CreateMultipleFiles != other.CreateMultipleFiles)
                {
                    returnValue = false;
                }
                else if (this.ClassSuffix != other.ClassSuffix)
                {
                    returnValue = false;
                }
                else if (this.Database != other.Database)
                {
                    returnValue = false;
                }
                else if (this.DBMS != other.DBMS)
                {
                    returnValue = false;
                }
                else if (this.FetchTableDetailsWithLoad != other.FetchTableDetailsWithLoad)
                {
                    returnValue = false;
                }
                else if (this.GenerateCustomClassTemplate != other.GenerateCustomClassTemplate)
                {
                    returnValue = false;
                }
                else if (this.GenerateDataLayerClassesCheckBox != other.GenerateDataLayerClassesCheckBox)
                {
                    returnValue = false;
                }
                else if (this.GenerateStoredProcedures != other.GenerateStoredProcedures)
                {
                    returnValue = false;
                }
                else if (this.GenerateWcfLayerClasses != other.GenerateWcfLayerClasses)
                {
                    returnValue = false;
                }
                else if (this.GenerateWcfLayerClientHelpers != other.GenerateWcfLayerClientHelpers)
                {
                    returnValue = false;
                }
                else if (this.GenerateWcfLayerServerComponents != other.GenerateWcfLayerServerComponents)
                {
                    returnValue = false;
                }
                else if (this.GrantUser != other.GrantUser)
                {
                    returnValue = false;
                }
                else if (this.Language != other.Language)
                {
                    returnValue = false;
                }
                else if (this.Namespace != other.Namespace)
                {
                    returnValue = false;
                }
                else if (this.OutputPath != other.OutputPath)
                {
                    returnValue = false;
                }
                else if (this.Password != other.Password)
                {
                    returnValue = false;
                }
                else if (this.Server != other.Server)
                {
                    returnValue = false;
                }
                else if (this.SPPrefix != other.SPPrefix)
                {
                    returnValue = false;
                }
                else if (this.SQLAuthentication != other.SQLAuthentication)
                {
                    returnValue = false;
                }
                else if (this.SQLHelper != other.SQLHelper)
                {
                    returnValue = false;
                }
                else if (this.SQLLogin != other.SQLLogin)
                {
                    returnValue = false;
                }
                else if (this.Size != other.Size)
                {
                    returnValue = false;
                }
                else if (this.Location != other.Location)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
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

        #endregion

        #region Properties
        [XmlIgnore]
        public Boolean Dirty
        {
            get
            {
                Boolean returnValue = default(Boolean);

                try
                {
                    if (this._Server != this.__Server)
                    {
                        returnValue = true;
                    }
                    else if (this._Database != this.__Database)
                    {
                        returnValue = true;
                    }
                    else if (this._SQLAuthentication != this.__SQLAuthentication)
                    {
                        returnValue = true;
                    }
                    else if (this._SQLLogin != this.__SQLLogin)
                    {
                        returnValue = true;
                    }
                    else if (this._Password != this.__Password)
                    {
                        returnValue = true;
                    }
                    else if (this._GrantUser != this.__GrantUser)
                    {
                        returnValue = true;
                    }
                    else if (this._SPPrefix != this.__SPPrefix)
                    {
                        returnValue = true;
                    }
                    else if (this._CreateMultipleFiles != this.__CreateMultipleFiles)
                    {
                        returnValue = true;
                    }
                    else if (this._Namespace != this.__Namespace)
                    {
                        returnValue = true;
                    }
                    else if (this._ClassSuffix != this.__ClassSuffix)
                    {
                        returnValue = true;
                    }
                    else if (this._Language != this.__Language)
                    {
                        returnValue = true;
                    }
                    else if (this._SQLHelper != this.__SQLHelper)
                    {
                        returnValue = true;
                    }
                    else if (this._AutoSelectOnLoad != this.__AutoSelectOnLoad)
                    {
                        returnValue = true;
                    }
                    else if (this._FetchTableDetailsWithLoad != this.__FetchTableDetailsWithLoad)
                    {
                        returnValue = true;
                    }
                    else if (this._DBMS != this.__DBMS)
                    {
                        returnValue = true;
                    }
                    else if (this._GenerateCustomClassTemplate != this.__GenerateCustomClassTemplate)
                    {
                        returnValue = true;
                    }
                    else if (this._OutputPath != this.__OutputPath)
                    {
                        returnValue = true;
                    }
                    else if (this._GenerateDataLayerClassesCheckBox != this.__GenerateDataLayerClassesCheckBox)
                    {
                        returnValue = true;
                    }
                    //else if (this._Version != this.__Version)
                    //{
                    //    returnValue = true;
                    //}
                    else if (this._GenerateStoredProcedures != this.__GenerateStoredProcedures)
                    {
                        returnValue = true;
                    }
                    else if (this._GenerateWcfLayerClasses != this.__GenerateWcfLayerClasses)
                    {
                        returnValue = true;
                    }
                    else if (this._GenerateWcfLayerServerComponents != this.__GenerateWcfLayerServerComponents)
                    {
                        returnValue = true;
                    }
                    else if (this._GenerateWcfLayerClientHelpers != this.__GenerateWcfLayerClientHelpers)
                    {
                        returnValue = true;
                    }
                    else if (this._Size != this.__Size)
                    {
                        returnValue = true;
                    }
                    else if (this._Location != this.__Location)
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
                    Log.Write(
                        ex,
                        System.Reflection.MethodBase.GetCurrentMethod(),
                        System.Diagnostics.EventLogEntryType.Error,
                        99);
                    throw ex;
                }

                return returnValue;
            }
            //set { _Dirty = value; }
        }

        #region Persisted Properties
        private String __DBMS = "MSSQL"; //String.Empty;
        private String _DBMS = "MSSQL"; //String.Empty;
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Database type to use (i.e. Sql Server, Oracle, etc.)"),
        CategoryAttribute("DBMS"),
        DefaultValueAttribute("MSSQL")]
		public  String DBMS
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

        private Boolean __GenerateDataLayerClassesCheckBox = default(Boolean);
        private Boolean _GenerateDataLayerClassesCheckBox = default(Boolean);
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate data access classes"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(false)]
        public Boolean GenerateDataLayerClassesCheckBox
		{
            get { return _GenerateDataLayerClassesCheckBox; }
            set
            {
                _GenerateDataLayerClassesCheckBox = value;
                this.OnPropertyChanged("GenerateDataLayerClassesCheckBox");
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

        /// <summary>
        /// Determines the viewer window size.
        /// </summary>
        private System.Drawing.Size __Size = new Size(546, 769);
        private System.Drawing.Size _Size = new Size(546, 769);
        [DescriptionAttribute("Determines the viewer window size."),
        CategoryAttribute("Appearance"),
        DefaultValueAttribute(typeof(Size), "546, 769")]
        public Size Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                //this.OnPropertyChanged("Size");
            }
        }

        /// <summary>
        /// Determines the viewer window position.
        /// </summary>
        private System.Drawing.Point __Location = new Point(100, 100);
        private System.Drawing.Point _Location = new Point(100, 100);
        [DescriptionAttribute("Determines the viewer window position."),
        CategoryAttribute("Appearance"),
        DefaultValueAttribute(typeof(Point), "100, 100")]
        public Point Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                //this.OnPropertyChanged("Location");
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
                //this.OnPropertyChanged("Version");
            }
        }
        #endregion Persisted Properties
        #endregion Properties

        #region static methods
        #region SettingIO
        public static void LoadXml(Settings settings, String filePath)
        {
            Settings returnValue = default(Settings);

            try
            {
                //XML Serializer of type Settings
                XmlSerializer xs = new XmlSerializer(typeof(Settings));

                //Stream reader for file
                StreamReader sr = new StreamReader(filePath);

                //de-serialize into Settings
                returnValue = (Settings)xs.Deserialize(sr);
                returnValue.Sync();
                returnValue.CopyTo(settings, true);

                //close file
                sr.Close();
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
            finally
            {
                returnValue = null;
            }
        }

        public static void PersistXml(Settings settings, String filePath)
		{
            try
            {
                //XML Serializer of type Settings
                XmlSerializer xs = new XmlSerializer(typeof(Settings));

                //Stream writer for file
                StreamWriter sw = new StreamWriter(filePath);

                //serialize out of Settings
                xs.Serialize(sw, settings);

                //close file
                sw.Close();

                settings.Sync();
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

        #endregion SettingIO
        #endregion static methods

        #region non-static methods
        /// <summary>
        /// Copies property values from source working fields to detination working fields, then optionally syncs destination.
        /// <param name="destinationSettings"></param>
        /// <param name="sync"></param>
        /// </summary>
        public void CopyTo(Settings destinationSettings, Boolean sync)
        {
            try
            {
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
                destinationSettings.GenerateDataLayerClassesCheckBox = this.GenerateDataLayerClassesCheckBox;
                //destinationSettings.Version = this.Version;
                destinationSettings.GenerateStoredProcedures = this.GenerateStoredProcedures;
                destinationSettings.GenerateWcfLayerClasses = this.GenerateWcfLayerClasses;
                destinationSettings.GenerateWcfLayerServerComponents = this.GenerateWcfLayerServerComponents;
                destinationSettings.GenerateWcfLayerClientHelpers = this.GenerateWcfLayerClientHelpers;
                destinationSettings.Size = this.Size;
                destinationSettings.Location = this.Location;

                if (sync)
                {
                    destinationSettings.Sync();
                }
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
        /// Syncs property values from working fields to reference fields.
        /// </summary>
        public void Sync()
        {
            try
            {
                this.__Server = this._Server;
                this.__Database = this._Database;
                this.__SQLAuthentication = this._SQLAuthentication;
                this.__SQLLogin = this._SQLLogin;
                this.__Password = this._Password;
                this.__GrantUser = this._GrantUser;
                this.__SPPrefix = this._SPPrefix;
                this.__CreateMultipleFiles = this._CreateMultipleFiles;
                this.__Namespace = this._Namespace;
                this.__ClassSuffix = this._ClassSuffix;
                this.__Language = this._Language;
                this.__SQLHelper = this._SQLHelper;
                this.__AutoSelectOnLoad = this._AutoSelectOnLoad;
                this.__FetchTableDetailsWithLoad = this._FetchTableDetailsWithLoad;
                this.__DBMS = this._DBMS;
                this.__GenerateCustomClassTemplate = this._GenerateCustomClassTemplate;
                this.__OutputPath = this._OutputPath;
                this.__GenerateDataLayerClassesCheckBox = this._GenerateDataLayerClassesCheckBox;
                this.__Version = this._Version;
                this.__GenerateStoredProcedures = this._GenerateStoredProcedures;
                this.__GenerateWcfLayerClasses = this._GenerateWcfLayerClasses;
                this.__GenerateWcfLayerServerComponents = this._GenerateWcfLayerServerComponents;
                this.__GenerateWcfLayerClientHelpers = this._GenerateWcfLayerClientHelpers;
                this.__Size = this._Size;
                this.__Location = this._Location;

                if (Dirty)
                {
                    throw new ApplicationException("Sync failed.");
                }
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

        #region private methods
        private String ApplyDefaultString(String field, String settingName)
        {
            String returnValue = field;
            // initialize Settings object 
            AppSettingsReader appSettings = default(AppSettingsReader);

            try
            {
                if (field == null || field == String.Empty)
                {
                    appSettings = new System.Configuration.AppSettingsReader();
                    returnValue = (String)appSettings.GetValue(settingName, typeof(String));
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
            finally
            {
                appSettings = null;
            }
            return returnValue;
        }
        #endregion private methods
        #endregion non-static methods
    }
}

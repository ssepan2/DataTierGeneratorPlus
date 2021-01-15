using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using Ssepan.Application;
using Ssepan.Utility;

namespace DataTierGeneratorPlusLibrary 
{
    /// <summary>
    /// run-time model; relies on model
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GeneratorModel : 
        ModelBase
    {
        #region Declarations

        public static List<String> LanguageList = new List<String>();
        public static List<String> DbmsList = new List<String>();
        public static List<String> SQLHelperList = new List<String>();

        #region Customizations
        //LANGUAGE CUSTOMIZATION: 1 of 5 -- define new language ID constant here (recommend String of 2-10 characters)
        public const String Language_CSharp = "C#";
        public const String Language_VisualBasic = "VB";
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
        public const String DBMS_MSSQL = "MSSQL";
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

        public const String SQLHelper_BuiltIn = "BuiltIn";
        //public const String SQLHelper_EnterpriseLibrary = "EnterpriseLibrary";
        // public const String SQLHelper_DotNetNuke = "DotNetNuke";
        #endregion Customizations
        #endregion Declarations

        #region Constructors
        //init static members
        static GeneratorModel()
        {
            try
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
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
        }

        public GeneratorModel() 
        {
            if (SettingsController<GeneratorSettings>.Settings == null)
            {
                SettingsController<GeneratorSettings>.New();
            }
            Debug.Assert(SettingsController<GeneratorSettings>.Settings != null, "SettingsController<GeneratorSettings>.Settings != null");
        }

        //public GeneratorModel
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

        #region IEquatable<IModel>
        /// <summary>
        /// Compare property values of two specified Model objects.
        /// </summary>
        /// <param name="anotherSettings"></param>
        /// <returns></returns>
        public override Boolean Equals(IModelComponent other)
        {
            Boolean returnValue = default(Boolean);
            GeneratorModel otherModel = default(GeneratorModel);

            try
            {
                otherModel = other as GeneratorModel;

                if (this == otherModel)
                {
                    returnValue = true;
                }
                else
                {
                    if (!base.Equals(other))
                    {
                        returnValue = false;
                    }
                    else if (this.AutoSelectOnLoad != otherModel.AutoSelectOnLoad)
                    {
                        returnValue = false;
                    }
                    else if (this.CreateMultipleFiles != otherModel.CreateMultipleFiles)
                    {
                        returnValue = false;
                    }
                    else if (this.ClassSuffix != otherModel.ClassSuffix)
                    {
                        returnValue = false;
                    }
                    else if (this.Database != otherModel.Database)
                    {
                        returnValue = false;
                    }
                    else if (this.DBMS != otherModel.DBMS)
                    {
                        returnValue = false;
                    }
                    else if (this.FetchTableDetailsWithLoad != otherModel.FetchTableDetailsWithLoad)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateCustomClassTemplate != otherModel.GenerateCustomClassTemplate)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateDataLayerClasses != otherModel.GenerateDataLayerClasses)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateStoredProcedures != otherModel.GenerateStoredProcedures)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateWcfLayerClasses != otherModel.GenerateWcfLayerClasses)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateWcfLayerClientHelpers != otherModel.GenerateWcfLayerClientHelpers)
                    {
                        returnValue = false;
                    }
                    else if (this.GenerateWcfLayerServerComponents != otherModel.GenerateWcfLayerServerComponents)
                    {
                        returnValue = false;
                    }
                    else if (this.GrantUser != otherModel.GrantUser)
                    {
                        returnValue = false;
                    }
                    else if (this.Language != otherModel.Language)
                    {
                        returnValue = false;
                    }
                    else if (this.Namespace != otherModel.Namespace)
                    {
                        returnValue = false;
                    }
                    else if (this.OutputPath != otherModel.OutputPath)
                    {
                        returnValue = false;
                    }
                    else if (this.Password != otherModel.Password)
                    {
                        returnValue = false;
                    }
                    else if (this.Server != otherModel.Server)
                    {
                        returnValue = false;
                    }
                    else if (this.SPPrefix != otherModel.SPPrefix)
                    {
                        returnValue = false;
                    }
                    else if (this.SQLAuthentication != otherModel.SQLAuthentication)
                    {
                        returnValue = false;
                    }
                    else if (this.SQLHelper != otherModel.SQLHelper)
                    {
                        returnValue = false;
                    }
                    else if (this.SQLLogin != otherModel.SQLLogin)
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
        #endregion IEquatable<IModel>

        #region Properties
        #region Persisted Properties
        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Database type to use (i.e. Sql Server, Oracle, etc.)"),
        CategoryAttribute("DBMS"),
        DefaultValueAttribute("MSSQL")]
        public String DBMS
        {
            get { return SettingsController<GeneratorSettings>.Settings.DBMS; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.DBMS = value;
                this.OnPropertyChanged("DBMS");
            }
        }

        //DefaultValueAttribute(val)]
        [DescriptionAttribute("Database Server name"),
        CategoryAttribute("DBMS"),
        DefaultValueAttribute("(local)")]
        public String Server
        {
            get { return SettingsController<GeneratorSettings>.Settings.Server; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.Server = value;
                this.OnPropertyChanged("Server");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Database (catalog) name"),
        CategoryAttribute("DBMS"),
        DefaultValueAttribute(null)]
        public String Database
        {
            get { return SettingsController<GeneratorSettings>.Settings.Database; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.Database = value;
                this.OnPropertyChanged("Database");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Use SQL Authentication, instead of Windows (default)"),
        CategoryAttribute("Authentication"),
        DefaultValueAttribute(false)]
        public Boolean SQLAuthentication
        {
            get { return SettingsController<GeneratorSettings>.Settings.SQLAuthentication; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.SQLAuthentication = value;
                this.OnPropertyChanged("SQLAuthentication");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Database user name"),
        CategoryAttribute("Authentication"),
        DefaultValueAttribute(null)]
        public String SQLLogin
        {
            get { return SettingsController<GeneratorSettings>.Settings.SQLLogin; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.SQLLogin = value;
                this.OnPropertyChanged("SQLLogin");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Database password"),
        CategoryAttribute("Authentication"),
        DefaultValueAttribute(null)]
        public String Password
        {
            get { return SettingsController<GeneratorSettings>.Settings.Password; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.Password = value;
                this.OnPropertyChanged("Password");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Automatically select tables as the table-list is loaded."),
        CategoryAttribute("Load"),
        DefaultValueAttribute(false)]
        public Boolean AutoSelectOnLoad
        {
            get { return SettingsController<GeneratorSettings>.Settings.AutoSelectOnLoad; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.AutoSelectOnLoad = value;
                this.OnPropertyChanged("AutoSelectOnLoad");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Load details about the table (i.e. - keys, columns, etc.) as the table-list is loaded."),
        CategoryAttribute("Load"),
        DefaultValueAttribute(false)]
        public Boolean FetchTableDetailsWithLoad
        {
            get { return SettingsController<GeneratorSettings>.Settings.FetchTableDetailsWithLoad; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.FetchTableDetailsWithLoad = value;
                this.OnPropertyChanged("FetchTableDetailsWithLoad");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Base path for output"),
        CategoryAttribute("Paths"),
        DefaultValueAttribute(null)]
        public String OutputPath
        {
            get { return SettingsController<GeneratorSettings>.Settings.OutputPath; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.OutputPath = value;
                this.OnPropertyChanged("OutputPath");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate stored procedures"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(false)]
        public Boolean GenerateStoredProcedures
        {
            get { return SettingsController<GeneratorSettings>.Settings.GenerateStoredProcedures; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.GenerateStoredProcedures = value;
                this.OnPropertyChanged("GenerateStoredProcedures");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("User name to be applied to stored procedures' rights"),
        CategoryAttribute("Stored Procedures"),
        DefaultValueAttribute(null)]
        public String GrantUser
        {
            get { return SettingsController<GeneratorSettings>.Settings.GrantUser; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.GrantUser = value;
                this.OnPropertyChanged("GrantUser");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Prefix to be generated before stored procedure names"),
        CategoryAttribute("Stored Procedures"),
        DefaultValueAttribute(null)]
        public String SPPrefix
        {
            get { return SettingsController<GeneratorSettings>.Settings.SPPrefix; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.SPPrefix = value;
                this.OnPropertyChanged("SPPrefix");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate one file per stored procedure, instead of one file for all stored procedures per table (default)"),
        CategoryAttribute("Stored Procedures"),
        DefaultValueAttribute(false)]
        public Boolean CreateMultipleFiles
        {
            get { return SettingsController<GeneratorSettings>.Settings.CreateMultipleFiles; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.CreateMultipleFiles = value;
                this.OnPropertyChanged("CreateMultipleFiles");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate data access classes"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(false)]
        public Boolean GenerateDataLayerClasses
        {
            get { return SettingsController<GeneratorSettings>.Settings.GenerateDataLayerClasses; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.GenerateDataLayerClasses = value;
                this.OnPropertyChanged("GenerateDataLayerClasses");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Namespace to use for generated classes"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(null)]
        public String Namespace
        {
            get { return SettingsController<GeneratorSettings>.Settings.Namespace; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.Namespace = value;
                this.OnPropertyChanged("Namespace");
            }
        }

        //[DataObjectFieldAttribute(false, false, true)]
        [DescriptionAttribute("Suffix to be generated after class names"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(null)]
        public String ClassSuffix
        {
            get { return SettingsController<GeneratorSettings>.Settings.ClassSuffix; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.ClassSuffix = value;
                this.OnPropertyChanged("ClassSuffix");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute(".Net language that will be generated"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute("C#")]
        public String Language
        {
            get { return SettingsController<GeneratorSettings>.Settings.Language; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.Language = value;
                this.OnPropertyChanged("Language");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("SQL helper library to use"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute("BuiltIn")]
        public String SQLHelper
        {
            get { return SettingsController<GeneratorSettings>.Settings.SQLHelper; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.SQLHelper = value;
                this.OnPropertyChanged("SQLHelper");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate empty partial classes for custom code"),
        CategoryAttribute("Data Layer"),
        DefaultValueAttribute(false)]
        public Boolean GenerateCustomClassTemplate
        {
            get { return SettingsController<GeneratorSettings>.Settings.GenerateCustomClassTemplate; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.GenerateCustomClassTemplate = value;
                this.OnPropertyChanged("GenerateCustomClassTemplate");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate WCF classes"),
        CategoryAttribute("WCF Layers"),
        DefaultValueAttribute(false)]
        public Boolean GenerateWcfLayerClasses
        {
            get { return SettingsController<GeneratorSettings>.Settings.GenerateWcfLayerClasses; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.GenerateWcfLayerClasses = value;
                this.OnPropertyChanged("GenerateWcfLayerClasses");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate WCF server classes and configuration"),
        CategoryAttribute("WCF Layers"),
        DefaultValueAttribute(false)]
        public Boolean GenerateWcfLayerServerComponents
        {
            get { return SettingsController<GeneratorSettings>.Settings.GenerateWcfLayerServerComponents; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.GenerateWcfLayerServerComponents = value;
                this.OnPropertyChanged("GenerateWcfLayerServerComponents");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [DescriptionAttribute("Generate WCF client proxy wrapper classes"),
        CategoryAttribute("WCF Layers"),
        DefaultValueAttribute(false)]
        public Boolean GenerateWcfLayerClientHelpers
        {
            get { return SettingsController<GeneratorSettings>.Settings.GenerateWcfLayerClientHelpers; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.GenerateWcfLayerClientHelpers = value;
                this.OnPropertyChanged("GenerateWcfLayerClientHelpers");
            }
        }

        //[DataObjectFieldAttribute(false, false, false)]
        [ReadOnly(true)]
        [DescriptionAttribute("Application major version"),
        CategoryAttribute("Misc"),
        DefaultValueAttribute(null)]
        public String Version
        {
            get { return SettingsController<GeneratorSettings>.Settings.Version; }
            set
            {
                SettingsController<GeneratorSettings>.Settings.Version = value;
                this.OnPropertyChanged("Version");
            }
        }
        #endregion Persisted Properties

        private static IDbUtility _DatabaseUtility = default(IDbUtility);
        public static IDbUtility DatabaseUtility
        {
            get { return _DatabaseUtility; }
            set { _DatabaseUtility = value; }
        }
        #endregion Properties

        #region Methods

        /// <summary>
        /// Get a default base output path.
        /// </summary>
        /// <returns></returns>
        public static String GetDefaultOutputPath()
        {
            String returnValue = default(String);
            String applicationName = String.Empty;
            try
            {
                applicationName = Path.GetFileNameWithoutExtension(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name);
                returnValue = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationName);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
            }
            return returnValue;
        }
        #endregion Methods

    }
}

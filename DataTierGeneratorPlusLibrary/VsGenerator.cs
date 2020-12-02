using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Ssepan.Utility;

namespace DataTierGeneratorPlusLibrary 
{
	 abstract class VsGenerator : IVsGenerator
	{
        protected const String XML_FILE_EXT = "xml";
        protected const String SVC_FILE_EXT = "svc";
         
         abstract public String CodeFolder
         {
             get;
         }
		
		/// <summary>
		/// Creates a dotNet language data access  class .
		/// </summary>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         public abstract Boolean CreateDatabaseUtilityClass
			 (
             String path,
             GeneratorModel model
             );

         protected abstract String GetDatabaseUtilityClass
             (
            GeneratorModel model
             );
		
		/// <summary>
		/// Creates a dotNet language null handler class.
		/// </summary>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         public abstract Boolean CreateNullHandlerClass
			 (
             String path,
             GeneratorModel model
             );

         protected abstract String GetNullHandlerClass
             (
            GeneratorModel model
             );

         /// <summary>
         /// Creates a dotNet language property comparer class.
         /// </summary>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         public abstract Boolean CreateSortComparerClass
             (
             String path,
             GeneratorModel model
             );

         protected abstract String GetSortComparerClass
             (
            GeneratorModel model
             );

         /// <summary>
         /// Creates a dotNet language searchable, sortable binding list class.
         /// </summary>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         public abstract Boolean CreateBindingListViewClass
             (
             String path,
             GeneratorModel model
             );

         protected abstract String GetBindingListViewClass
             (
            GeneratorModel model
             );
		
		 /// <summary>
		 /// Creates a dotNet language table class for all of the table's columns. Unlike the Table object passed in, the generated class will be a simple set of strongly-typed properties.
		 /// </summary>
		 /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
		 /// <param name="path">Path where the class should be created.</param>
		 /// <param name="model">User model.</param>
         public abstract Boolean CreateTableStructureClass
			 (
			 Table table, 
			 String path, 
			 GeneratorModel model
			 ) ;
		
		/// <summary>
		/// Creates a dotNet language data access  class for all of the table's stored procedures generated automatically.
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
		/// <param name="path">Path where the class should be created.</param>
		/// <param name="model">User model.</param>
         public abstract Boolean CreateDataAccessGeneratedClass
			(
			Table table, 
			String path, 
			GeneratorModel model
			) ;

         protected abstract Boolean CreateInsertMethod
        (
            Table table, 
            StreamWriter streamWriter,
            GeneratorModel model
        ) ;

         protected abstract Boolean CreateInsertMethodTakingInfo
			 (
			 Table table, 
			 StreamWriter streamWriter,
			 GeneratorModel model
			 ) ;

         protected abstract Boolean CreateUpdateMethod
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateUpdateMethodTakingInfo
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateDeleteMethod
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateDeleteMethodTakingInfo
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateDeleteByMethods
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateDeleteByMethodsTakingInfo
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectMethodReturningDataReader
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectMethodReturningDataSet
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectMethodReturningInfo
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectAllMethodReturningDataReader
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectAllMethodReturningDataSet
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectAllMethodReturningInfo
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectByMethodsReturningDataReader
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectByMethodsReturningDataSet
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateSelectByMethodsReturningInfo
			(
			Table table, 
			StreamWriter streamWriter,
			GeneratorModel model
			) ;

         protected abstract Boolean CreateListLoadMethod
             (
             Table table,
            StreamWriter streamWriter,
             GeneratorModel model
             );

         /// <summary>
         /// Creates an empty dotNet language data access  class for all of the table's stored procedures created by hand.
         /// </summary>
         /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         public abstract Boolean CreateDataAccessCustomClass
            (
            Table table,
            String path,
            GeneratorModel model
            );

         /// <summary>
         /// Returns the contents of the Data Access Custom class.
         /// </summary>
         /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
         /// <param name="model">User model.</param>
         /// <returns>A formatted app / web config snippet.</returns>
         protected abstract String GetDataAccessCustomClass
         (
             Table table,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a WCF Service Config System.ServiceModel snippet.
         /// </summary>
        /// <param name="wcfServiceConfigSnippetStringBuilder"></param>
         /// <param name="model"></param>
         public Boolean GetWcfServiceConfigSystemServiceModelSnippet
          (
              ref StringBuilder wcfServiceConfigSnippetStringBuilder,
              GeneratorModel model
          )
         {
             Boolean returnValue = default(Boolean);
             try
             {
                 Boolean IsNameSpace = (model.Namespace.Trim() != String.Empty);

                 wcfServiceConfigSnippetStringBuilder.Append(Utility.GetResource("DataTierGeneratorPlusLibrary.Templates.WcfServiceConfigSystemServiceModel.txt"));

                 //if (IsNameSpace)
                 //{
                 wcfServiceConfigSnippetStringBuilder.Replace("#Namespace#", model.Namespace + ".");
                 //}
                 //else
                 //{
                 //    wcfServiceConfigSnippetStringBuilder.Replace("#Namespace#", "");
                 //}
                 returnValue = true;
             }
             catch (Exception ex)
             {
                 Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
             }
             return returnValue;
         }
         
         /// <summary>
         /// Creates WCF Service Config Service and Behavior snippets.
         /// </summary>
         /// <param name="wcfServiceConfigSnippetStringBuilder"></param>
         /// <param name="table"></param>
         /// <param name="model"></param>
         public Boolean GetWcfServiceConfigServiceAndBehaviorSnippet
          (
              ref StringBuilder wcfServiceConfigSnippetStringBuilder,
              Table table,
              GeneratorModel model
          )
         {
             Boolean returnValue = default(Boolean);
             try
             {
                 Boolean IsNameSpace = (model.Namespace.Trim() != String.Empty);

                 String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Service" + model.ClassSuffix));
                 className = Utility.FilterPathCharacters(className);

                 String interfaceName = "I" + className;

                 wcfServiceConfigSnippetStringBuilder.Replace("#ServiceTag#", Utility.GetResource("DataTierGeneratorPlusLibrary.Templates.WcfServiceConfigService.txt"));
                 wcfServiceConfigSnippetStringBuilder.Replace("#BehaviorTag#", Utility.GetResource("DataTierGeneratorPlusLibrary.Templates.WcfServiceConfigBehavior.txt"));

                 //if (IsNameSpace)
                 //{
                 wcfServiceConfigSnippetStringBuilder.Replace("#Namespace#", model.Namespace + ".");
                 //}
                 //else
                 //{
                 //    returnValue = returnValue.Replace("#Namespace#", "");
                 //}
                 wcfServiceConfigSnippetStringBuilder.Replace("#Service#", className);
                 wcfServiceConfigSnippetStringBuilder.Replace("#Interface#", interfaceName);
                 returnValue = true;
             }
             catch (Exception ex)
             {
                 Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
             }
             return returnValue;
         }
         
         /// <summary>
         /// Write WCF Service Config System.ServiceModel snippet.
         /// </summary>
         /// <param name="wcfServiceConfigSnippetStringBuilder"></param>
         /// <param name="path"></param>
         /// <param name="model"></param>
         public Boolean WriteWcfServiceConfigSystemServiceModelSnippet
          (
             ref StringBuilder wcfServiceConfigSnippetStringBuilder,
             String path,
             GeneratorModel model
          )
         {
             Boolean returnValue = default(Boolean);
             try
             {
                 //clear out the last pair of tags
                 wcfServiceConfigSnippetStringBuilder.Replace("#ServiceTag#", "");
                 wcfServiceConfigSnippetStringBuilder.Replace("#BehaviorTag#", "");

                 using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, "ServiceConfig." + XML_FILE_EXT)))
                 {
                     streamWriter.Write(wcfServiceConfigSnippetStringBuilder.ToString());
                 }
                 returnValue = true;
             }
             catch (Exception ex)
             {
                 Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
             }
             return returnValue;
         }

         /// <summary>
         /// Creates a System ServiceModel Config snippet.
         /// </summary>
         /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         public Boolean CreateWcfServiceSVCFile
         (
             Table table,
             String path,
             GeneratorModel model
         )
         {
             Boolean returnValue = default(Boolean);
             try
             {
                 String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Service" + model.ClassSuffix));
                 className = Utility.FilterPathCharacters(className);

                 using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, className + ".Generated." + SVC_FILE_EXT)))
                 {
                     streamWriter.Write(GetWcfServiceSVCFile(table, model));
                 }
                 returnValue = true;
             }
             catch (Exception ex)
             {
                 Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
             }
             return returnValue;
         }

         protected abstract String GetWcfServiceSVCFile
         (
            Table table,
            GeneratorModel model
         );
        
         /// <summary>
        /// Creates a C# contract class for all of the table's fields .
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="model">User model.</param>
         public abstract Boolean CreateWcfContractAndInterfaceClasses
         (
             Table table,
             String path,
             GeneratorModel model
         );

         protected abstract Boolean CreateInsertServiceInterface
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateInsertServiceInterfaceTakingInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateUpdateServiceInterface
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateUpdateServiceInterfaceTakingInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateDeleteServiceInterface
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateDeleteServiceInterfaceTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            GeneratorModel model
        );

         protected abstract Boolean CreateDeleteByServiceInterfaces
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateDeleteByServiceInterfacesTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            GeneratorModel model
        );

         protected abstract Boolean CreateSelectServiceInterfaceReturningDataSet
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectServiceInterfaceReturningInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectAllServiceInterfaceReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            GeneratorModel model
        );

         protected abstract Boolean CreateSelectAllServiceInterfaceReturningInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectByServiceInterfacesReturningDataSet
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectByServiceInterfacesReturningInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a dotNet language WCF Service class for all of the table's stored procedures generated automatically.
         /// </summary>
         /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         public abstract Boolean CreateWcfServiceGeneratedClass
        (
            Table table,
            String path,
            GeneratorModel model
        );

         protected abstract Boolean CreateInsertService
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateInsertServiceTakingInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateUpdateService
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateUpdateServiceTakingInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateDeleteService
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateDeleteServiceTakingInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateDeleteByServices
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateDeleteByServicesTakingInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectServiceReturningDataSet
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectServiceReturningInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectAllServiceReturningDataSet
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectAllServiceReturningInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectByServicesReturningDataSet
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateSelectByServicesReturningInfo
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

        public abstract Boolean CreateWcfServiceCustomClass
         (
             Table table,
             String path,
             GeneratorModel model
         );

         protected abstract String GetWcfServiceCustomClass
         (
            Table table,
            GeneratorModel model
         );

         /// <summary>
         /// Creates a WCF service extension class.
         /// </summary>
         /// <param name="table"></param>
         /// <param name="path"></param>
         /// <param name="model"></param>
         public abstract Boolean CreateWcfServiceExtensionClass
         (
             Table table,
             String path,
             GeneratorModel model
         );

         protected abstract Boolean CreateToListOfContractServiceExtension
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateToContractServiceExtension
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         protected abstract Boolean CreateToInfoServiceExtension
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a WCF service client extension class.
         /// </summary>
         /// <param name="table"></param>
         /// <param name="path"></param>
         /// <param name="model"></param>
         public abstract Boolean CreateWcfServiceClientExtensionClass
         (
             Table table,
             String path,
             GeneratorModel model
         );

         protected abstract Boolean CreateToListOfContractServiceClientExtension
         (
             Table table,
             StreamWriter streamWriter,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a WCF service client application-code class.
         /// </summary>
         /// <param name="table"></param>
         /// <param name="path"></param>
         /// <param name="model"></param>
         public abstract Boolean CreateWcfServiceClientAppCodeClass
         (
             Table table,
             String path,
             GeneratorModel model
         );

        /// <summary>
        /// Returns the contents of the WCF service application-code class.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="model">User model.</param>
        /// <returns>A formatted app / web config snippet.</returns>
         protected abstract String GetWcfServiceClientAppCodeClass
         (
             Table table,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a String for a method parameter representing the specified column.
         /// </summary>
         /// <param name="column">Object that stores the information for the column the parameter represents.</param>
         /// <returns>String containing parameter information of the specified column for a method call.</returns>
         public abstract String CreateMethodFormalParameter
         (
             Column column
         );

         protected abstract String CreateStoredProcedureActualParameterString
             (
             Column column,
             String columnPrefix
             );

         protected abstract String CreateInfoActualParameterString
          (
             Column column,
             String columnPrefix
         );
	 }
}

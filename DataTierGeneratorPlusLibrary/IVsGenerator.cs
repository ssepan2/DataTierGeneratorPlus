using System;
using System.IO;
using System.Text;
using DataTierGeneratorPlusLibrary.MVC;

namespace DataTierGeneratorPlusLibrary 
{
	 interface IVsGenerator 
	{

         String CodeFolder
         { 
             get;
         }
		
		/// <summary>
		/// Creates a dotNet language data access  class .
		/// </summary>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         Boolean CreateDatabaseUtilityClass
		 (
             String path,
             GeneratorModel model
         );

		
		/// <summary>
		/// Creates a dotNet language null handling class.
		/// </summary>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         Boolean CreateNullHandlerClass
         (
             String path,
             GeneratorModel model
         );

		/// <summary>
		/// Creates a dotNet language property comparer class.
		/// </summary>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         Boolean CreateSortComparerClass
         (
             String path,
             GeneratorModel model
         );

		/// <summary>
		/// Creates a dotNet language searchable, sortable binding list class.
		/// </summary>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         Boolean CreateBindingListViewClass
         (
             String path,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a dotNet language table class for all of the table's columns. Unlike the Table object passed in, the generated class will be a simple set of strongly-typed properties.
         /// </summary>
         /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         Boolean CreateTableStructureClass
         (
             Table table,
             String path,
             GeneratorModel model
         );
		
		/// <summary>
		/// Creates a dotNet language data access  class for all of the table's stored procedures generated automatically.
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
		/// <param name="path">Path where the class should be created.</param>
		/// <param name="model">User model.</param>
         Boolean CreateDataAccessGeneratedClass
		(
			Table table, 
			String path, 
			GeneratorModel model
		) ;
		
		/// <summary>
		/// Creates an empty dotNet language data access  class for all of the table's stored procedures created by hand.
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
		/// <param name="path">Path where the class should be created.</param>
		/// <param name="model">User model.</param>
         Boolean CreateDataAccessCustomClass
		(
			Table table, 
			String path, 
			GeneratorModel model
		) ;

         /// <summary>
         /// Creates a WCF Service Config System.ServiceModel snippet.
         /// </summary>
         /// <param name="wcfServiceConfigSnippetStringBuilder"></param>
         /// <param name="model"></param>
         Boolean GetWcfServiceConfigSystemServiceModelSnippet
         (
             ref StringBuilder wcfServiceConfigSnippetStringBuilder,
             GeneratorModel model
         );
         
         /// <summary>
         /// Creates WCF Service Config Service and Behavior snippets.
         /// </summary>
         /// <param name="wcfServiceConfigSnippetStringBuilder"></param>
         /// <param name="table"></param>
         /// <param name="model"></param>
         Boolean GetWcfServiceConfigServiceAndBehaviorSnippet
         (
             ref StringBuilder wcfServiceConfigSnippetStringBuilder,
             Table table,
             GeneratorModel model
         );
         
         /// <summary>
         /// Write WCF Service Config System.ServiceModel snippet.
         /// </summary>
         /// <param name="wcfServiceConfigSnippetStringBuilder"></param>
         /// <param name="path"></param>
         /// <param name="model"></param>
         Boolean WriteWcfServiceConfigSystemServiceModelSnippet
         (
            ref StringBuilder wcfServiceConfigSnippetStringBuilder,
            String path, 
            GeneratorModel model
         );

         /// <summary>
         /// Creates a WCF Service SVC file.
         /// </summary>
         /// <param name="table"></param>
         /// <param name="path"></param>
         /// <param name="model"></param>
         Boolean CreateWcfServiceSVCFile
         (
             Table table,
             String path,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a C# contract class for all of the table's fields .
         /// </summary>
         /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         Boolean CreateWcfContractAndInterfaceClasses
         (
             Table table,
             String path,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a dotNet language WCF Service class for all of the table's stored procedures generated automatically.
         /// </summary>
         /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
         /// <param name="path">Path where the class should be created.</param>
         /// <param name="model">User model.</param>
         Boolean CreateWcfServiceGeneratedClass
        (
            Table table,
            String path,
            GeneratorModel model
        );

         /// <summary>
         /// Creates a WCF Service Custom class.
         /// </summary>
         /// <param name="table"></param>
         /// <param name="path"></param>
         /// <param name="model"></param>
         Boolean CreateWcfServiceCustomClass
         (
             Table table,
             String path,
             GeneratorModel model
         );
        
        /// <summary>
         /// Creates a WCF service extension class.
         /// </summary>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <param name="model"></param>
         Boolean CreateWcfServiceExtensionClass
         (
             Table table,
             String path,
             GeneratorModel model
         );
        
        /// <summary>
         /// Creates a WCF service client extension class.
         /// </summary>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <param name="model"></param>
         Boolean CreateWcfServiceClientExtensionClass
         (
             Table table,
             String path,
             GeneratorModel model
         );
        
        /// <summary>
         /// Creates a WCF service client application-code class.
         /// </summary>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <param name="model"></param>
         Boolean CreateWcfServiceClientAppCodeClass
         (
             Table table,
             String path,
             GeneratorModel model
         );

         /// <summary>
         /// Creates a String for a method parameter representing the specified column.
         /// </summary>
         /// <param name="column">Object that stores the information for the column the parameter represents.</param>
         /// <returns>String containing parameter information of the specified column for a method call.</returns>
         String CreateMethodFormalParameter
         (
             Column column
         );
		
		 //the rest are private, and so do not belong in interface; maybe in a base class
     }
}

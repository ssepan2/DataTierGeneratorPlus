DataTierGeneratorPlus v6.14


PURPOSE:
To let the developer go directly from a working database design to laying out fields on a form, without the tedious work of writing (or re-writing) the layers in between. 


USAGE:

The tool is designed for, and tested primarily against, Visual Studio 2005/2008/2010 (and the Express Editions) and SQL Server 2005/2008[R2] (and Express Editions). At this time it DOES replace the previous versions (3.x/4.x) that generated code compatible with Visual Studio 2003. See "Known Issues" for any work-arounds and limitations that apply.
In the source version, files are generated under the "bin\debug\<database>\cs\", "bin\debug\<database>\vb\" and "bin\debug\<database>\sql\" folders by default. 
In the binary version, they are generated under <special_folder>\DataTierGeneratorPlus\<database>\<[sql|language]>, where the special folder is currently LocalApplicationData (which maps to <OS_drive>:\users\<user>\AppData\Local). 
In both cases, you can override these.

Data Layer:
The developer designs the SQL tables first, complete with foreign-key relationships. The tool then generates SQL stored procedures for the basic C.R.U.D. operations, along stored procedures for Selecting or Deleting based on any foreign keys that may be defined. 
This layer consists of the following files, where LL is the source language:
~StoredProcedures.Sql -- these are the generated SQL Server scripts to add CRUD stored procedures for all the tables generated. It must be run once (in the database tool of your choice) before the application is run.

Business Layer:
The tool optionally follows up with several .Net classes per table, plus several support classes.  The object can then be bound to the ObjectDataSource, and controls bound to the data source can pick fields at design time. In practice, you refer to the Controller class for a given table and see the generated methods and any custom methods. An Info class is also provided, and both are decorated with DataObject attributes. 
This layer consists of the following files, where TTT is the corresponding table and LL is the source language:
~TTTInfo.LL -- A class that serves as a simple BO and corresponds to one of the tables for which code was generated. It must be compiled into the application.
~TTTController.Generated.LL -- A (partial) class that wraps the data layer execute-calls and provides CRUD functionality taking either fields or the info object and returning either DataReader, DataSet, or BindingListView<Info>. It must be compiled into the application.
~TTTController.Custom.LL -- A partial class that corresponds to TTTController.Generated.LL, and is initially empty. This class can contain custom code that will not be overwritten (if the 'Generate Custom Class Template(s)' option is not checked on subsequent runs of the generator). It must be compiled into the application.
~DatabaseUtility.LL -- this is the SQL Server utility class for connecting to the database and executing stored procedures. It must be compiled into the application.
~BindingListView.LL -- A subclass of generic BindingList<T> that implements IBindingListView and supports filtering and sorting. It must be compiled into the application.
~SortComparer.LL -- A class that implements IComparer<T> and is required by BindingListView. It must be compiled into the application.
~NullHandler.LL -- A class that is used by the Controller class for converting null values between .Net and the database. It must be compiled into the application.

WCF Service Layer: 
The tool adds a WCF web service layer that plugs on top of the Info and Controller classes. This layer consists of Contract and Service classes that correspond to the Info and Controller classes in the business layer.
This layer consists of the following files, where TTT is the corresponding table and LL is the source language:
~TTTContract.LL -- A class that defines the contract and the interface of the service. The Contract defines the service contract corresponds to the Info class, and the interface defines the operation contract. It must be compiled into the service application. 
~TTTService.Generated.svc.LL -- A (partial) class that implements the operation contract interface, and corresponds to the Controller class. It must be compiled into the service application. 
~TTTService.Custom.LL -- A partial class that corresponds to TTTService.Generated.LL, and is initially empty. This class can contain custom code that will not be overwritten (if the 'Generate Custom Class Template(s)' option is not checked on subsequent runs of the generator). It must be compiled into the service application.
~TTTService.Generated.svc -- An XML-formatted file which defines the service.  It must be compiled into the service application.
~TTTServiceConfig.xml -- An XML-formatted config snippet that contains settings that msut be copied and pasted into the config file of the service application.
~TTTServiceExtensions.LL -- A class that provides conversion routines in the form of extension methods. It must be compiled into the service application.

WCF Service client app:
~BindingListView.LL -- A subclass of generic BindingList<T> that implements IBindingListView and supports filtering and sorting. It must be compiled into the application.
~SortComparer.LL -- A class that implements IComparer<T> and is required by BindingListView. It must be compiled into the application.
~TTTServiceClientExtensions.LL -- A class that provides conversion routines in the form of extension methods. It must be compiled into the service *client* application.
~TTTServiceClientApplication.LL -- (Optional) A class that illustrates declaring and calling the service client proxy generated by Visual Studio / svcutil.exe. If used, it must be compiled into the service *client* application.
Additionally, these tasks must be performed manually:
~The service client application must reference the service.
~The service client application must add one Import (VB) or using (C#) statement to the class that references the service *for every service used*, where NNN is the service client applications Namespace: 
Import|using NNN.TTTService[;]

SAMPLES:
There are several sample projects per language, where LL is the language:
~LL_BuiltIn -- This app is the test app for the DAL, BLL, and SVC layers.
~LL_BuiltIn_WcfServiceApp -- This app is the service host.
~The test app uses a pair of sample tables, test_master and test_detail. There is a script to generate the tables, relationship, and data in the SQL folder.
~There are sample settings files in the Settings folder.

HISTORY:

6.14: (CURRENT)
~Updated Ssepan.* to 2.1
~Fixed progress bar and status message logic.
~TODO:move generate methods to controller
~TODO:see DocumentScanner project for BackgroundWorker error message passing

6.13: (RELEASED)
~Refactored Settings / SettingsController, and their bases in Ssepan.Application, to put the static Settings property into the Settings class instead of the SettingsController class. This will make Settings more like SettingsController and the model / controller classes, and hopefully make Settings easier to understand and maintain.
~Projects are using .Net Framework 4.0.
~Using version 2.0 of Ssepan.* libraries, all of which are using .Net Framework 4.0.

6.12:
~Refactor image resources.

6.11:
~Refactor UI File IO logic in menu events to eliminate duplication.
~Refactored IEquality implementation.

6.10:
~Fixed missing display of errors in Catch in model controller, settings.
~Moved common portions of settings I/O into base classes in Ssepan.Application library.
~Modified Sepan.Application to include _ValueChanging flag from sub-class, and to check and set it from the Controller base class Refresh method.

6.9:
~Fixed field initialization of File|New, File|Open.
~Fixed database login field validation so that validation messages/indicators do not show when checkbox is cleared.
~Due to permissions issues in Vista/Windows7, changed app to generate files under user's special application local folders (<drive>:\Users\<profile>\AppData\Local\DataTierGeneratorPlus\). Since this location is not easily found, the application was also changed to open the Explorer window to that location on a successful generation.

6.8:
~Refactored <appnamespace>.frmAbout to Ssepan.Application.WinForms.AboutDialog.
~Refactored Ssepan.Configuration.PropertiesViewer to Ssepan.Application.WinForms.PropertyDialog.

6.7:
~Clean up solution structure and setup project details.
~Associated application with .DTGP file type. The application can still open and save .XML files that were saved in the previous version.
~Included missing sample/test settings files.

6.6:
~Still more internal refactoring -- retro-fitting calls to additional common libraries (Ssepan.Io, Ssepan.DataBinding, Ssepan.Configuration).
~Added property viewer dialog to project so that it's possible to view the Settings object at runtime.
~Using simple databinding more, and relying on MVC observer notifications only where necessary.
~Fixed misnamed Login default value in app.config to SQLLogin.

6.5:
~More internal refactoring; moved generator code and declarations that were common to VB and C# implementations into base class.
~Changed some instances of string concatenation into String.Format calls.

6.4:
~Internal refactoring -- implemented some model-view-controller features and cleaned up business logic.
~Moved 'Dirty' logic into settings object; replaced manual binding between interface and settings with imperative databinding.
~Modified SQL / Windows Authentication interface to support databinding to settings.
~Added size and position to stored settings.

6.3:
~Added more flexible generation options, including WCF services and stored procedures.
~Added more visible error reporting.
~Added missing error trapping and logging in SQL generation class.

6.2:
~Generates sample WCF service client application code to illustrate calling the WCF service client proxy generated by Visual Studio / svcutil.exe.

6.1:
~Generates all WCF service config snippets into single file; eliminate the need for extensive manual cutting / pasting.

6.0:
~Added logging and improved error trapping to application.
~Re-ordered columns in Table grid to be more usable -- selection first, followed by table name and alias.
~Fixed references to generated stored procs in generated code; SelectBy and DeleteBy references had key column names appended to the proc name outside of the brackets.
~Removed extraneous wrapper functions for handling nulls from the generated Controller class, and called the null handler directly.
~Removed extraneous VS Utility factory class, and called the formal-parameter and table-structure methods directly.
~Revisited null handling; used nullable types and limited role of Null Handler class. (Missing Int64 type in null handler class now moot.)
~Moved all templates to Templates subfolder.
~Generates VB code with NameSpace as comments, so it behaves well with the Root NamesSpace.
~Added the generation of a WCF service server-layer to plug on over the Controller / Info layers.

5.0:
~This preview was tested with ASP.Net 2.0 ObjectDataSource and Windows Forms 2.0 BindingSource (Object option) in Visual Studio 2005. The latter behaves differently from the former. Specifically, the WebForms version allows you to specify both the BO and the related CRUD operations. The WinForms version allows you to specify the BO to bind to BindingSource, but it appears that you have to call the CRUD operations yourself. (I am still researching this feature, so I may be mistaken.)
~I implemented a better way to do the Dirty flag for the tool's settings. I changed the 'Dirty' logic from (true-if-field-changed) to (true-if-fields-differ-from-Settings). I now check against the current settings object *when* a field is changed, and only set the flag when the fields differ from it, rather than just setting the flag when a field changes. That allows for cases where the user changes the values back to the current settings. 
~I disabled the J# and C++ features, removed the source files from the project, and archived them in Zip files (included). The J# implementation in both VS2003 and VS2005 appears inconsistent. C++ isn't even an option available for web site projects, and I've never used it, so it wasn't practical to continue to keep it in sync with all the changes when I couldn't test it properly anyway. In the time I had it posted on the web site, and several hundred downloads were done, there was not one single request or comment indicating that either language was needed. I can save myself 50% of the effort in maintaining the 2 languages that I do use. 
~I disabled the EnterpriseLibrary features, removed the source files from the project, and archived them in Zip files (included). The implementation of EnterpriseLibrary for .Net Framework 2.0 appears to use a slightly different syntax when called from an application. At least that is how it appears in the current version of DataTierGenerator on SourceForge. Since I didn't have time to keep up the EnterpriseLibrary option and get the ObjectDataSource feature done (I'm shooting for October 1) I decide to scale the project back to just the built-in sql helper library for now. This saves me another 25% effort, and combined with the language reductions, 75% overall. I believe it is a contributing factor to my having been able to put out a working preview by the middle of September.
~I've created versions of all the Select methods that return DataSet. I've cleaned up the method names to make them more consistent.
~The classes generated have been renamed to be consistent with the DNN DAL standard: xxxController for the class containing the access methods, and xxxInfo for the class containing the table columns. The xxxPlus naming convention (Sql and SqlPlus ) has been dropped; the new naming convenion is ControllerBase and Controller, respectively. The Table class naming convention has changed from a prefix of Table to a suffix of Info. 
~I've made the generated classes compatible with ASP.Net 2.0's ObjectDataSource. So far the basic CRUD works fine, but in order to get Insert/Update/Delete to work when implemented with knowledge of the Info object, I had to pull the connection and transaction parameters out of the calls; to be consistent, I did it with all the calls. I have added 2 new properties to the DatabaseUtility class: ConnectionStringKeyName and TransactionObject. This means that the setting of the connection only needs to be done once in Page_Load(). You must also init the TransactionObject property to null/Nothing. I am aware that this latest change could potentially break the transaction functionality. I believe that the TransactionObject property can still be set in the Inserting/Updating/Deleting events, but for now you should take care to manually clear it AFTER (in the Inserted/Updated/Deleted events.) I will research this further. 9/23/2006, SJS.
~VS2005 Forms standardization: Modified forms to use partial classes for segregating designer code from manual code. Moved Main() into Program class.
~Control replacements: MenuBar --> MenuStrip.  StatusBar --> StatusStrip.  DataGrid --> DataGridView.
~Added a ToolBarStrip with common functions.
~Deprecated the Base-class/sub-class switch, and replaced the generated pairs of base/sub classes with pairs of partial classes.
~Added a setting to allow user to skip the generation of the empty custom-methods partial class. This setting will help the user avoid unpleasant accidents like overwriting the customized partial class when combined with the next note.
~Added a setting to allow the user to override the default output location (/...exe_path/databasename/[sql] | [language]/) with an exact absolute path. This setting combined with the ability to skip the custom partial class allows user to generate code directly into the target project's folders.
~Although the 10/23/2006 release utilized a searchable, sortable sub class of BindingList<T>, I've not yet figured out in WinForm's how to get BindingSource and DataGridView to do updates back to the database using the generated CRUD methods. There also seems to be a problem getting sorting to work  in WinForms with ObjectDataSource and GridView. An implementation of IBindingListView is about the same. It turns out that strongly-typed datsets integrate better than custom business objects in both WinForms and WebForms. 11/12/2006, SJS.
~The generation of .Net access classes is now optional. It now appears that the optimal solution is to use strongly-typed datasets, and since those integrate with stored procedures directly, it is not necessary to generate another datalayer.
~I have tested paging against strongly-typed datasets in WebForms and found it to be satisfactory as well. (I am pretty sure that customer business objects would have required more custom coding).
~I am adding some multi-threading support, in the form of the BackgroundWorker component, to the Load and Generate functions. This implementation supports a simple call to a separate thread, progress-reporting, and cancelling (via ESC key).

4.5:
~After a 7-month hiatus paying bills, I've found the opportunity to spend some time looking for ways to tune and enhance this app. Two key areas that I want to pursue will be moving the app closer to targeting multiple DBMS' and (for ASP.Net 2.0) generating code that will plug directly into ObjectDataSource.
~Cleaned up still more duplication and complicated code left over from the merge of the v2 and Enterprise variations of DataTierGenerator. Most of this cleanup was concentrated in the various Utility classes.
~Moved the IGenerator interface class to a newly created abstract VsGenerator base class, and inherited all language generator classes from the abstract class. All generators are still created by the factory that returns an object that complies with interface, but now there is a base class that also enforces what private methods should be written as well when writing a new language generator.
~Refactored the database generator in a similar fashion to the code generator. The intention is to isolate database-specific generation in a way that will allow alternate DBMS' (i.e. - Oracle, MySQL, Access) to be targeted. The Settings object now supports a DBMS setting, and the form has a DBMS field (with a single choice for now.) The application itself was using SQLClient internally when looking up tables, columns, and keys. This SQL Server assumption was also reflected in the form, and was addressed by using OleDb internally, and by moving the connection strings into an abstracted db utility class. The interface was modified to replace SQL Server specific parts of the interface with a more generic one.  Status: Beta.  9/1/2006, SJS.
~Also refactored the database utility, and the language utility classes to use the factory pattern, and made them accessible as static public fields in the Generator class. Now, the majority of the code written for extensions will take advantage of the compiler to tell developers of new features when they have implemented the expected members.

4.0:
~Refactored application internals with interfaces for generator classes, to simplify task of implementing new languages.  Previous re-factoring in v3.x made it *possible* to add the generation of additional .Net languages; this round makes it *easier* to do so, by limiting the code changes to Settings.cs and VsGeneratorFactory.cs. Simple steps listed in comments in Settings.cs. Status: Beta.   1/3/2006, SJS.
~Modified main form to use DropDownList for languages instead of RadioButtons. This allowed me to isolate the interface from knowledge of specific languages available.  Instead, this information is controlled primarily in Settings.cs. Status: Beta. 2/14/2006, SJS.
~Modified IVsGenerator interface, and related classes, to include a method CreateTableStructureClass, which will generate a simple class containing typed properties for each column in the associated database table. I found myself creating one of these for each table that I intended to build a grid form. The purpose of the class is to support the manner in which I have been implementing Add functionality in web forms with a DataGrids. I have coded and tested the C#, VB, and J# versions, and I have coded the C++ version (but did not finish testing it -- the C++ option is disabled due to C++-specific errors that I cannot resolve).  Status: Beta.  3/12/2006, SJS.

3.7:
~Wrote templates and methods to generate C++ code (see known issues).

3.6:
~Wrote templates and methods to generate J# code.  Status: Beta. 12/28/2005, SJS.

3.5:
~Allow storing and retrieving named configurations, instead of using a single file (settings.xml). 12/16/2005, SJS.
~Wrote templates and methods to generate VB.Net code. Status: Beta. 12/26/2005, SJS.
~Also wrote a companion project that uses all 4 cases. It will serve as the start of a test platform. While far from exhaustive, it hits the basic CRUD operations. It will be available as a separate download. Status: Beta. 12/26/2005, SJS.

3.0:
~All of the functionality described in the "Purpose" section was already present in the two flavors of open-source project that I came across on GotDotNet.com. The two differed only in the fact that one used a custom database utility class from go from the C# methods to the SQL SPs, while the other used the new MS Enterprise Library (aka DAAB). My contribution was to take the 2 flavors and mash them together into my own variety, which I am treating as an open-source project because that’s how I found them. (No, I haven’t bothered to post this back to GotDotNet.com.) 
~Added the subclass feature. The deal with sub-classing the generated C# code is this. One user liked the idea of generating the data tier. But he sometimes needed custom access methods. (For example, UpdatePassword() might not want to update all fields in a User table.) He didn’t like the idea that he had to choose between a) storing his custom sql access methods in a different class than his generated methods, b) risking the custom methods being overwritten if he had to change the db schema and re-generate the data tier procedure, or c) forgo ever re-generating the data tier if he used custom methods. The solution was to generate a sub-class with a suffix of ‘Plus’. All custom data methods are placed in the Plus files. We only copy the base class and the subclass into the project the first time we generate; on subsequent generation we copy just the base class, leaving any custom methods untouched. In practice, this has worked out very well. While you still have to manually change your interface, and would also have to tweak your custom SPs and access methods, it’s easier than hand-coding changes to the routine stuff as well.
~One single set of settings is serialized in and out of a Settings class on app startup/shutdown. It’s a quick way to remember your settings if you are working mostly with a single project at a time. I didn’t feel like dealing with doing named file saves/opens. 
~Added a Datagrid to view and select tables. Included a checkbox to select all/none.
~Refactored the interface visually to recognize that the Load function is only affected by some settings, while the Generate function is affected by all settings.
~Refactored the interface logically to perform field validation and button / menu enabling based on the dependencies above.
~Refactored internal classes to pass a Settings object instead of individual options to dozens of internal methods. This cleaned up the code considerably, and will make it easier to extend this tool for future options (implementation of other .Net languages, addition of option for Oracle sql, etc.).
~Renamed and refactored some of the 'Generation' classes and class source filenames to make it easier in the future to include code generation class files for other .Net languages.
~Added option to delay loading of table details (columns and keys) until the Generation step. This also allowed me to design the tool to load details only for Selected tables. Total execution time, over VPN, on a large database (100+ tables), with small subset being generated (<10 tables), was reduced from 7 minutes to under 30 seconds!
~Added tooltips on fields and field groups.
~Used Winforms 'anchoring' on controls to make form and datagrid resizable. Form can also be Minimized and Maximized.
~Template "CsDatabaseUtility.cs" modified to allow the retrieval of a named connection String setting from the web.config file. Omitting the name will fetch a default setting (ConnectionString) as always. 12/16/2005, SJS.


FIXES:
~Fixed bug when user changes generated language after clicking Load, resulting in some of the code being generated in the language that was set immediately prior to the Load. Fixed, v6.0. 6/1/2009, SJS
~Fixed bug where references to the <table>Info class were in the wrong case. While not a problem in VB, it resulted in C# code that would not compile. Fixed, v6.0. 6/1/2009, SJS
~Fixed a bug that occurred when the user clicked Generate more than 1X after clicking Load, when the ‘Fetch Table Details With Load’ option was un-checked. The error, which was avoidable by always re-Loading before running Generate again, was that the column and key details were duplicated. This resulted in scripts that will not compile in QA, and CS code that would be equally bad. Although it’s unlikely to have been an issue 99% of the time, it was sever enough to warrant a fix ASAP. Severity: High. Ensured that ArrayList is cleared. Fixed. 11/11/2005, SJS
~When form was resized, status bar did not appear to resize horizontally to use space. In fact Status Bar control DID resize, but Panel object within does not.  Severity: Low. Set panel Autosize to Spring. Status: Fixed. 12/16/2005, SJS.
~Fixed a bug in the generation of the C# Enterprise Library code, where the subclass "...Plus" did not inherit from the base class, so referencing the generated methods from the Plus class was impossible. Severity: Medium. Added inheritance clause to Class declaration. Status: Fixed. 12/26/2005, SJS.
~When run on SQL Server 2005, the schema-to-fn_listextendedproperty lookups fail in the ON clause of the JOIN with an error 'Cannot resolve the collation conflict between "Latin1_General_CI_AI" and "SQL_Latin1_General_CP1_CI_AS" in the equal to operation.'. The solution is to add ' COLLATE Latin1_General_CI_AS' to the fn_listextendedproperty.  Status: Fixed. 2005, SJS.
~Fixed the VB XML Comments format; changed from '/// to '''.  Status: Fixed. 9/19/2006, SJS.
~Fixed the constructors generated for all VB classes: was using classname() instead of New(). Status: Fixed. 9/19/2006, SJS.
~Fixed a bug in the C# copy of NullHandler class in HandleAppNull(); was not handling (objField==null). Status: Fixed. 9/23/2006, SJS.
~Fixed a conflict that kept the generated code from working in VB WinForms 2.0. Unlike WebForms projects, which only specify Namespaces in individual class files, WinForms projects have project-level settings. It is worth noting that the C# and VB settings are not only named differently, but they have different behaviors, and the difference can affect class files that explicitly specify a NameSpace. The C# property is called DefaultNamespace, whereas the VB propertty is called RootNamespace. The names give a hint of their different behavior. DefaultNamespace specifies the namespace that a class will belong to in the absence of a NameSpace keyword; the keyword overrides the default. RootNamespace specifies the namespace that a class will belong to in the absence of a NameSpace keyword *and* the parent namespace if a keyword is provided; the keyword appends a child namespace to the default. The end result is that classes with a NameSpace keyword will fit seamlessly into a C# project if the namespaces match, but in VB the classes will nest under the root class whether the namespaces match or not. The impact on our generated classes is that C# WebForms, C# WinForms, and VB WebForms load seamlessly into VS05 projects, whereas VB WinForms will experience difficulty finding the classes. Since this is only one case in four, my solution is to continue to provide the ability to generate the NameSpace keywords on the generated VB code and provide the option to disable the NameSpace keywords in the generated files. I made a modification to not generate the NameSpace code if the Namespace field is blank. Status: Fixed. Target Version: 5.0.  10/11/2006, SJS.


KNOWN ISSUES:
~(Vista/Windows7)Running this app under Vista or Windows 7 requires that the application folder in Program Files have the permissions 'Modify' and 'Write' set for the "Users" group. Otherwise clicking the Generate may result in the message "Unable to Generate in Background", which wraps a more detailed message in the event log indicating that access is denied on the files and folders that the program is generating. 
~(Vista/Windows7)Running this app under Vista or Windows 7 requires that the library that writes to the event log (Ssepan.Utility.dll) have its name added to the list of allowed 'sources'. Rather than do it manually, the one way to be sure to get it right is to simply run the application the first time As Administrator, and the settings will be added. After that you may run it normally. To register additional DLLs for the event log, you can use this trick any time you get an error indicating that you cannot write to it. Or you can manually register DLLs by adding a key called '<filename>.dll' under HKLM\System\CurrentControlSet\services\eventlog\Application\, and adding the string value 'EventMessageFile' with the value like <C>:\<Windows>\Microsoft.NET\Framework\v2.0.50727\EventLogMessages.dll (where the drive letter and Windows folder match your system). Status: work-around. 
~The Generate may fail with message "Unable to Generate in Background". Event log will record the message like "Could not find a part of the path '<path to file being generated>'". This appears to be an intermittent timing issue, but I will investivate further. Workaround is to simply press the Generate button again. Status: Testing. Target Version: unknown.  10/13/2009, SJS.
~When using ASP.Net 2.0 ObjectDataSource with the generated classes, note that while you can select a given field to display or hide, I SUSPECT that you should not eliminate it entirely, as you may encounter errors during inserting or updating. Once I have had a chance to test this more thoroughly, I can confirm or deny this suspicion. I also suspect that by fully impementing the use of Info objects as parameters to the CRUD methods, this point may become moot.
~If you encounter a runtime error during any of the CRUD operations that mentions "Object reference not set to an instance of an object." and it is pointing to the connection object, note that you must set the connection and transaction objects in the Page_Load event. For now, set LL_BuiltIn.DatabaseUtility.ConnectionStringKeyName to "ConnectionString" and set LL_BuiltIn.DatabaseUtility.TransactionObject to null or Nothing.
~If you encounter a runtime error that indicates that the key cannot be found, set the GridView.DataKeyNames property to the key or keys.
~If you encounter a runtime error during Insert/Update/Delete that indicates "ObjectDataSource 'xxx' could not find a non-generic method 'yyy' that has parameters: zzz1, zzz2, original_zzz.", where one of the fields is "original_keyfieldname", edit the indicated DataSourceObject and change it's OldValuesParameterFormatString property from "original_{0}" to "{0}".
~When targeting SQL Server 2005 Express, note that the database server seems to be installed with a named instance by default. As a result, referencing the database server by "(local)" may fail. Check your installed copy to be certain, but the default name seems to be "<machinename>\SQLEXPRESS". Double-check your connection strings.
~When targeting SQL Server 2005 (or later), note that the database server seems to require that you provide a strong password before it will let you proceed with the database server installation. Double-check your connection strings.
~(redacted)In the J# implementation, all calls that pass a DataSet or DataReader back out through the parameter list using the /** @ref */ tag fail to work. This applies to both the Built-In and EnterpriseLibrary options.. This means that the user will want to rely on DatabaseUtililty or EnterpriseLibrary methods that pass back DataSet or DataReader through the return value. Both libraries have methods that return DataReader. Both libraries also expose an ExecuteDataSet function. In the case of the Enterprise library, since the user would have to create an instance of the library, I have generated an additional set of Select methods that return DataSets.  This issue requires someone comfortable with Visual J# and reference parameters.  Status: On Hold. 12/28/2005, SJS.
~(redacted)Wrote templates and methods to generate C++ code, but the generated code does not compile in the test-harness app (distributed separately). The compilation errors center around 1) base-classes cannot be found for a given sub-class, and 2) namespace identifiers are not recognized by other files with the same namespace, even with Using clause. Google shows a lot of chatter about headers files and includes, but I was not able to find the fix. As a result the C++ option remains disabled. This issue requires someone comfortable with Visual C++.  Status: On Hold.  1/2/2006, SJS.
~When using the generated .Net classes with WebForms GridView, sorting will not work. The control seemed to want to pass the sort parameters to the select call, rather than calling the ApplySort() method on the IBindingListView interface.
~When using the generated .Net classes with WinForms DataGridView, it is assumed that the business object will keep track of multiple row changes and provide an Update() method that will reconcile all changed rows with the data store. While strongly-typed datasets implement the logic necessary, custom business objects use interfaces (IList, IBindingList, and IBindingListView) that seem to have gone a different direction.
~When sorting against WebForms, using strongly-typed datasets, the very first click of a column goes from unsorted to sorted ascending, and if it was the key column, it will appear to have no effect.
~Restoring from Maximize loses the size settings. Status: Research. 

POSSIBLE ENHANCEMENTS:
~Move more business logic for settings combinations into Settings object and out of the form. Also, implements MVC. Status: Not Started. Target Version: 7.x?.  6/13/2009, SJS.
~Implement classes compatible with DotNetNuke 4.x. The CRUD classes look like they would need to be generated against a separate database utility (DotNetNuke), instead of being a minor variation. I believe that this can be handled as a new SQL Helper option. I have already refactored the app to allow for this possibility.  I may also need to add a NamespacePrefix at some point, for compatibility with DNN namespace naming conventions. The ClassSuffix setting may be deprecated. Status: Not Started. Target Version: Unknown.  9/13/2006, SJS.
~Associate a file-extension with this application. Status: Research. 


MISCELLANEOUS:
~To group or ungroup files in Visual Studio, without having to manually edit the project file to add a DependentUpon tag, see VSCommands at http://mokosh.co.uk/page/VsCommands.aspx.


Steve Sepan
ssepanus@yahoo.com
7/31/2011

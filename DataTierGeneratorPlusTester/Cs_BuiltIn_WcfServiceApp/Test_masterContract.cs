using System;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
//using System.ComponentModel;

namespace Cs_BuiltIn_WcfServiceApp 
{
	/// <summary>
	/// Class that defines the table operations.
	/// </summary>
	[ServiceContract]
	public interface ITest_masterService 
	{

		/// <summary>
		/// Inserts a record into the [dbo].[test_master] table.
		/// </summary>
		[OperationContract]
		void Insert(Int32 id, String description, String notes, Int32 someint, Int32? someint_nullable, DateTime somedate, DateTime? somedate_nullable, Double somefloat, Double? somefloat_nullable, Boolean somebool, Boolean? somebool_nullable);

		/// <summary>
		/// Inserts a record into the [dbo].[test_master] table.
		/// </summary>
		[OperationContract]
		void InsertInfo(Test_masterContract info);

		/// <summary>
		/// Updates a record in the [dbo].[test_master] table.
		/// </summary>
		[OperationContract]
		void Update(Int32 id, String description, String notes, Int32 someint, Int32? someint_nullable, DateTime somedate, DateTime? somedate_nullable, Double somefloat, Double? somefloat_nullable, Boolean somebool, Boolean? somebool_nullable);

		/// <summary>
		/// Updates a record in the [dbo].[test_master] table.
		/// </summary>
		[OperationContract]
		void UpdateInfo(Test_masterContract info);

		/// <summary>
		/// Deletes a record from the [dbo].[test_master] table by a composite primary key.
		/// </summary>
		[OperationContract]
		void Delete(Int32 id);

		/// <summary>
		/// Deletes a record from the [dbo].[test_master] table by a composite primary key.
		/// </summary>
		[OperationContract]
		void DeleteInfo(Test_masterContract info);

		/// <summary>
		/// Selects a single record from the [dbo].[test_master] table.
		/// </summary>
		[OperationContract]
		DataSet SelectDS(Int32 id);

		/// <summary>
		/// Selects a single record from the [dbo].[test_master] table.
		/// </summary>
		[OperationContract]
		BindingListView<Test_masterContract> SelectInfo(Test_masterContract info);

		/// <summary>
		/// Selects all records from the [dbo].[test_master] table.
		/// </summary>
		[OperationContract]
		DataSet SelectDSAll();

		/// <summary>
		/// Selects all records from the [dbo].[test_master] table.
		/// </summary>
		[OperationContract]
		BindingListView<Test_masterContract> SelectInfoAll();
	}

	/// <summary>
	/// Class that stores table fields.
	/// </summary>
	[DataContract]
	public class Test_masterContract 
	{
		/// <summary>
		/// Default constructor.  
		/// </summary>
		public Test_masterContract() {}

		/// <summary>
		/// Constructor with values.  
		/// </summary>
		public Test_masterContract(
			Int32 id,
			String description,
			String notes,
			Int32 someint,
			Int32? someint_nullable,
			DateTime somedate,
			DateTime? somedate_nullable,
			Double somefloat,
			Double? somefloat_nullable,
			Boolean somebool,
			Boolean? somebool_nullable
		)
		{
			_id = id;
			_description = description;
			_notes = notes;
			_someint = someint;
			_someint_nullable = someint_nullable;
			_somedate = somedate;
			_somedate_nullable = somedate_nullable;
			_somefloat = somefloat;
			_somefloat_nullable = somefloat_nullable;
			_somebool = somebool;
			_somebool_nullable = somebool_nullable;
		}

		/// Private variables for columns in the test_master table.
		private Int32 _id;
		private String _description;
		private String _notes;
		private Int32 _someint;
		private Int32? _someint_nullable;
		private DateTime _somedate;
		private DateTime? _somedate_nullable;
		private Double _somefloat;
		private Double? _somefloat_nullable;
		private Boolean _somebool;
		private Boolean? _somebool_nullable;

		/// Public properties for columns in the test_master table.

		/// <summary>
		/// id  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(true, false, false)]
		public Int32 id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// description  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, false)]
		public String description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// notes  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, true)]
		public String notes
		{
			get { return _notes; }
			set { _notes = value; }
		}

		/// <summary>
		/// someint  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, false)]
		public Int32 someint
		{
			get { return _someint; }
			set { _someint = value; }
		}

		/// <summary>
		/// someint_nullable  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, true)]
		public Int32? someint_nullable
		{
			get { return _someint_nullable; }
			set { _someint_nullable = value; }
		}

		/// <summary>
		/// somedate  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, false)]
		public DateTime somedate
		{
			get { return _somedate; }
			set { _somedate = value; }
		}

		/// <summary>
		/// somedate_nullable  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, true)]
		public DateTime? somedate_nullable
		{
			get { return _somedate_nullable; }
			set { _somedate_nullable = value; }
		}

		/// <summary>
		/// somefloat  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, false)]
		public Double somefloat
		{
			get { return _somefloat; }
			set { _somefloat = value; }
		}

		/// <summary>
		/// somefloat_nullable  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, true)]
		public Double? somefloat_nullable
		{
			get { return _somefloat_nullable; }
			set { _somefloat_nullable = value; }
		}

		/// <summary>
		/// somebool  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, false)]
		public Boolean somebool
		{
			get { return _somebool; }
			set { _somebool = value; }
		}

		/// <summary>
		/// somebool_nullable  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, true)]
		public Boolean? somebool_nullable
		{
			get { return _somebool_nullable; }
			set { _somebool_nullable = value; }
		}
	}
}

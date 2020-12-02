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
	public interface ITest_detailService 
	{

		/// <summary>
		/// Inserts a record into the [dbo].[test_detail] table.
		/// </summary>
		[OperationContract]
		void Insert(Int32 master_id, String id, String description, Int32 qty, Double amt);

		/// <summary>
		/// Inserts a record into the [dbo].[test_detail] table.
		/// </summary>
		[OperationContract]
		void InsertInfo(Test_detailContract info);

		/// <summary>
		/// Updates a record in the [dbo].[test_detail] table.
		/// </summary>
		[OperationContract]
		void Update(Int32 master_id, String id, String description, Int32 qty, Double amt);

		/// <summary>
		/// Updates a record in the [dbo].[test_detail] table.
		/// </summary>
		[OperationContract]
		void UpdateInfo(Test_detailContract info);

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		/// </summary>
		[OperationContract]
		void Delete(Int32 master_id, String id);

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		/// </summary>
		[OperationContract]
		void DeleteInfo(Test_detailContract info);

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[OperationContract]
		void DeleteByMaster_id(Int32 master_id);

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[OperationContract]
		void DeleteInfoByMaster_id(Test_detailContract info);

		/// <summary>
		/// Selects a single record from the [dbo].[test_detail] table.
		/// </summary>
		[OperationContract]
		DataSet SelectDS(Int32 master_id, String id);

		/// <summary>
		/// Selects a single record from the [dbo].[test_detail] table.
		/// </summary>
		[OperationContract]
		BindingListView<Test_detailContract> SelectInfo(Test_detailContract info);

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table.
		/// </summary>
		[OperationContract]
		DataSet SelectDSAll();

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table.
		/// </summary>
		[OperationContract]
		BindingListView<Test_detailContract> SelectInfoAll();

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[OperationContract]
		DataSet SelectDSByMaster_id(Int32 master_id);

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[OperationContract]
		BindingListView<Test_detailContract> SelectInfoByMaster_id(Test_detailContract info);
	}

	/// <summary>
	/// Class that stores table fields.
	/// </summary>
	[DataContract]
	public class Test_detailContract 
	{
		/// <summary>
		/// Default constructor.  
		/// </summary>
		public Test_detailContract() {}

		/// <summary>
		/// Constructor with values.  
		/// </summary>
		public Test_detailContract(
			Int32 master_id,
			String id,
			String description,
			Int32 qty,
			Double amt
		)
		{
			_master_id = master_id;
			_id = id;
			_description = description;
			_qty = qty;
			_amt = amt;
		}

		/// Private variables for columns in the test_detail table.
		private Int32 _master_id;
		private String _id;
		private String _description;
		private Int32 _qty;
		private Double _amt;

		/// Public properties for columns in the test_detail table.

		/// <summary>
		/// master_id  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(true, false, false)]
		public Int32 master_id
		{
			get { return _master_id; }
			set { _master_id = value; }
		}

		/// <summary>
		/// id  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(true, false, false)]
		public String id
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
		/// qty  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, false)]
		public Int32 qty
		{
			get { return _qty; }
			set { _qty = value; }
		}

		/// <summary>
		/// amt  
		/// </summary>
		[DataMember]
		//[DataObjectFieldAttribute(false, false, false)]
		public Double amt
		{
			get { return _amt; }
			set { _amt = value; }
		}
	}
}

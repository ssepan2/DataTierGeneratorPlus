using System;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
//using System.ComponentModel;

namespace Cs_BuiltIn_WcfServiceApp 
{
	//[DataObject(true)]
	public partial class Test_detailService : ITest_detailService
	{
		protected Test_detailService() {}

		/// <summary>
		/// Inserts a record into the [dbo].[test_detail] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Insert, false)]
		public void Insert(Int32 master_id, String id, String description, Int32 qty, Double amt) 
		{
			Test_detailController.Insert 
			(
				master_id,
				id,
				description,
				qty,
				amt
			);
		}

		/// <summary>
		/// Inserts a record into the [dbo].[test_detail] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
		public void InsertInfo(Test_detailContract info) 
		{
			Test_detailController.InsertInfo
			(
				info.ToTest_detailInfo()
			);
		}

		/// <summary>
		/// Updates a record in the [dbo].[test_detail] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Update, false)]
		public void Update(Int32 master_id, String id, String description, Int32 qty, Double amt) 
		{
			Test_detailController.Update
			(
				master_id,
				id,
				description,
				qty,
				amt
			);
		}

		/// <summary>
		/// Updates a record in the [dbo].[test_detail] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
		public void UpdateInfo(Test_detailContract info) 
		{
			Test_detailController.UpdateInfo
			(
				info.ToTest_detailInfo()
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
		public void Delete(Int32 master_id, String id) 
		{
			Test_detailController.Delete
			(
				master_id,
				id
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
		public void DeleteInfo(Test_detailContract info) 
		{
			Test_detailController.DeleteInfo
			(
				info.ToTest_detailInfo()
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
		public void DeleteByMaster_id(Int32 master_id) 
		{
			Test_detailController.DeleteByMaster_id
			(
				master_id
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
		public void DeleteInfoByMaster_id(Test_detailContract info) 
		{
			Test_detailController.DeleteInfoByMaster_id
			(
				info.ToTest_detailInfo()
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_detail] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public DataSet SelectDS(Int32 master_id, String id) 
		{
			return Test_detailController.SelectDS
			(
				master_id,
				id
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_detail] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public BindingListView<Test_detailContract> SelectInfo(Test_detailContract info) 
		{
				return Test_detailController.SelectInfo(info.ToTest_detailInfo()).ToBindingListViewOfContract();
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public DataSet SelectDSAll() 
		{
			return Test_detailController.SelectDSAll();
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public BindingListView<Test_detailContract> SelectInfoAll() 
		{
			return Test_detailController.SelectInfoAll().ToBindingListViewOfContract();
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public DataSet SelectDSByMaster_id(Int32 master_id) 
		{
			return Test_detailController.SelectDSByMaster_id
			(
				master_id
			);
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public BindingListView<Test_detailContract> SelectInfoByMaster_id(Test_detailContract info) 
		{
			return Test_detailController.SelectInfoByMaster_id(info.ToTest_detailInfo()).ToBindingListViewOfContract();
		}
	}
}

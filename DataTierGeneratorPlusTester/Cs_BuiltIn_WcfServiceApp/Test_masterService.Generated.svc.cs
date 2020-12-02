using System;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
//using System.ComponentModel;

namespace Cs_BuiltIn_WcfServiceApp 
{
	//[DataObject(true)]
	public partial class Test_masterService : ITest_masterService
	{
		protected Test_masterService() {}

		/// <summary>
		/// Inserts a record into the [dbo].[test_master] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Insert, false)]
		public void Insert(Int32 id, String description, String notes, Int32 someint, Int32? someint_nullable, DateTime somedate, DateTime? somedate_nullable, Double somefloat, Double? somefloat_nullable, Boolean somebool, Boolean? somebool_nullable) 
		{
			Test_masterController.Insert 
			(
				id,
				description,
				notes,
				someint,
				someint_nullable,
				somedate,
				somedate_nullable,
				somefloat,
				somefloat_nullable,
				somebool,
				somebool_nullable
			);
		}

		/// <summary>
		/// Inserts a record into the [dbo].[test_master] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
		public void InsertInfo(Test_masterContract info) 
		{
			Test_masterController.InsertInfo
			(
				info.ToTest_masterInfo()
			);
		}

		/// <summary>
		/// Updates a record in the [dbo].[test_master] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Update, false)]
		public void Update(Int32 id, String description, String notes, Int32 someint, Int32? someint_nullable, DateTime somedate, DateTime? somedate_nullable, Double somefloat, Double? somefloat_nullable, Boolean somebool, Boolean? somebool_nullable) 
		{
			Test_masterController.Update
			(
				id,
				description,
				notes,
				someint,
				someint_nullable,
				somedate,
				somedate_nullable,
				somefloat,
				somefloat_nullable,
				somebool,
				somebool_nullable
			);
		}

		/// <summary>
		/// Updates a record in the [dbo].[test_master] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
		public void UpdateInfo(Test_masterContract info) 
		{
			Test_masterController.UpdateInfo
			(
				info.ToTest_masterInfo()
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_master] table by a composite primary key.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
		public void Delete(Int32 id) 
		{
			Test_masterController.Delete
			(
				id
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_master] table by a composite primary key.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
		public void DeleteInfo(Test_masterContract info) 
		{
			Test_masterController.DeleteInfo
			(
				info.ToTest_masterInfo()
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_master] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public DataSet SelectDS(Int32 id) 
		{
			return Test_masterController.SelectDS
			(
				id
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_master] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public BindingListView<Test_masterContract> SelectInfo(Test_masterContract info) 
		{
				return Test_masterController.SelectInfo(info.ToTest_masterInfo()).ToBindingListViewOfContract();
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_master] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public DataSet SelectDSAll() 
		{
			return Test_masterController.SelectDSAll();
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_master] table.
		/// </summary>
		//[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public BindingListView<Test_masterContract> SelectInfoAll() 
		{
			return Test_masterController.SelectInfoAll().ToBindingListViewOfContract();
		}
	}
}

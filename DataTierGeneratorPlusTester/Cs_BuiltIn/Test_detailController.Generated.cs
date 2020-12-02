using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Cs_BuiltIn 
{
	 [DataObject(true)]
	public partial class Test_detailController 
	{
		protected Test_detailController() {}

		//Public enums for column positions on the [dbo].[test_detail] table.
		public enum ColumnIndex 
		{
			Master_id = 0,
			Id = 1,
			Description = 2,
			Qty = 3,
			Amt = 4,
		};

		/// <summary>
		/// Inserts a record into the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Insert, false)]
		public static void Insert(Int32 master_id, String id, String description, Int32 qty, Double amt) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailInsert]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)),
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)),
				new SqlParameter("@description", NullHandler.HandleAppNull(description, DBNull.Value)),
				new SqlParameter("@qty", NullHandler.HandleAppNull(qty, DBNull.Value)),
				new SqlParameter("@amt", NullHandler.HandleAppNull(amt, DBNull.Value))
			);
		}

		/// <summary>
		/// Inserts a record into the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
		public static void InsertInfo(Test_detailInfo info) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailInsert]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(info.master_id, DBNull.Value)),
				new SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)),
				new SqlParameter("@description", NullHandler.HandleAppNull(info.description, DBNull.Value)),
				new SqlParameter("@qty", NullHandler.HandleAppNull(info.qty, DBNull.Value)),
				new SqlParameter("@amt", NullHandler.HandleAppNull(info.amt, DBNull.Value))
			);
		}

		/// <summary>
		/// Updates a record in the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Update, false)]
		public static void Update(Int32 master_id, String id, String description, Int32 qty, Double amt) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailUpdate]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)),
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)),
				new SqlParameter("@description", NullHandler.HandleAppNull(description, DBNull.Value)),
				new SqlParameter("@qty", NullHandler.HandleAppNull(qty, DBNull.Value)),
				new SqlParameter("@amt", NullHandler.HandleAppNull(amt, DBNull.Value))
			);
		}

		/// <summary>
		/// Updates a record in the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
		public static void UpdateInfo(Test_detailInfo info) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailUpdate]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(info.master_id, DBNull.Value)),
				new SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)),
				new SqlParameter("@description", NullHandler.HandleAppNull(info.description, DBNull.Value)),
				new SqlParameter("@qty", NullHandler.HandleAppNull(info.qty, DBNull.Value)),
				new SqlParameter("@amt", NullHandler.HandleAppNull(info.amt, DBNull.Value))
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
		public static void Delete(Int32 master_id, String id) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDelete]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)),
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value))
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
		public static void DeleteInfo(Test_detailInfo info) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDelete]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(info.master_id, DBNull.Value)),
				new SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value))
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
		public static void DeleteByMaster_id(Int32 master_id) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDeleteByMaster_id]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value))
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
		public static void DeleteInfoByMaster_id(Test_detailInfo info) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDeleteByMaster_id]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(info.master_id, DBNull.Value))
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static SqlDataReader SelectDR(Int32 master_id, String id) 
		{
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelect]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)),
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value))
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static DataSet SelectDS(Int32 master_id, String id) 
		{
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelect]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)),
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value))
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public static BindingListView<Test_detailInfo> SelectInfo(Test_detailInfo info) 
		{
			using (SqlDataReader dr = SelectDR(info.master_id,info.id)) 
			{
				return LoadListDR(dr);
			}
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static SqlDataReader SelectDRAll() 
		{
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectAll]");
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static DataSet SelectDSAll() 
		{
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectAll]");
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public static BindingListView<Test_detailInfo> SelectInfoAll() 
		{
			using (SqlDataReader dr = SelectDRAll()) 
			{
				return LoadListDR(dr);
			}
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static SqlDataReader SelectDRByMaster_id(Int32 master_id) 
		{
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectByMaster_id]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value))
			);
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static DataSet SelectDSByMaster_id(Int32 master_id) 
		{
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectByMaster_id]",
				new SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value))
			);
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_detail] table by a foreign key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public static BindingListView<Test_detailInfo> SelectInfoByMaster_id(Test_detailInfo info) 
		{
			using (SqlDataReader dr = SelectDRByMaster_id(info.master_id)) 
			{
				return LoadListDR(dr);
			}
		}

		/// <summary>
		/// Loads all records from the [dbo].[test_detail] table into List of Test_detailInfo.
		/// </summary>
		protected static BindingListView<Test_detailInfo> LoadListDR(SqlDataReader dr) 
		{ 
			BindingListView<Test_detailInfo> infoList = new BindingListView<Test_detailInfo>();
			while (dr.Read()) 
			{
				infoList.Add
					(
					new Test_detailInfo
						(
						(Int32)NullHandler.HandleDbNull(dr["master_id"]),
						(String)NullHandler.HandleDbNull(dr["id"]),
						(String)NullHandler.HandleDbNull(dr["description"]),
						(Int32)NullHandler.HandleDbNull(dr["qty"]),
						(Double)NullHandler.HandleDbNull(dr["amt"])
						)
					);
			}
			return infoList;
		}
	}
}

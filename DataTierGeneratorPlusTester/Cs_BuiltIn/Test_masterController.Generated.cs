using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Cs_BuiltIn 
{
	 [DataObject(true)]
	public partial class Test_masterController 
	{
		protected Test_masterController() {}

		//Public enums for column positions on the [dbo].[test_master] table.
		public enum ColumnIndex 
		{
			Id = 0,
			Description = 1,
			Notes = 2,
			Someint = 3,
			Someint_nullable = 4,
			Somedate = 5,
			Somedate_nullable = 6,
			Somefloat = 7,
			Somefloat_nullable = 8,
			Somebool = 9,
			Somebool_nullable = 10,
		};

		/// <summary>
		/// Inserts a record into the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Insert, false)]
		public static void Insert(Int32 id, String description, String notes, Int32 someint, Int32? someint_nullable, DateTime somedate, DateTime? somedate_nullable, Double somefloat, Double? somefloat_nullable, Boolean somebool, Boolean? somebool_nullable) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterInsert]",
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)),
				new SqlParameter("@description", NullHandler.HandleAppNull(description, DBNull.Value)),
				new SqlParameter("@notes", NullHandler.HandleAppNull(notes, DBNull.Value)),
				new SqlParameter("@someint", NullHandler.HandleAppNull(someint, DBNull.Value)),
				new SqlParameter("@someint_nullable", NullHandler.HandleAppNull(someint_nullable, DBNull.Value)),
				new SqlParameter("@somedate", NullHandler.HandleAppNull(somedate, DBNull.Value)),
				new SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(somedate_nullable, DBNull.Value)),
				new SqlParameter("@somefloat", NullHandler.HandleAppNull(somefloat, DBNull.Value)),
				new SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(somefloat_nullable, DBNull.Value)),
				new SqlParameter("@somebool", NullHandler.HandleAppNull(somebool, DBNull.Value)),
				new SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(somebool_nullable, DBNull.Value))
			);
		}

		/// <summary>
		/// Inserts a record into the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
		public static void InsertInfo(Test_masterInfo info) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterInsert]",
				new SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)),
				new SqlParameter("@description", NullHandler.HandleAppNull(info.description, DBNull.Value)),
				new SqlParameter("@notes", NullHandler.HandleAppNull(info.notes, DBNull.Value)),
				new SqlParameter("@someint", NullHandler.HandleAppNull(info.someint, DBNull.Value)),
				new SqlParameter("@someint_nullable", NullHandler.HandleAppNull(info.someint_nullable, DBNull.Value)),
				new SqlParameter("@somedate", NullHandler.HandleAppNull(info.somedate, DBNull.Value)),
				new SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(info.somedate_nullable, DBNull.Value)),
				new SqlParameter("@somefloat", NullHandler.HandleAppNull(info.somefloat, DBNull.Value)),
				new SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(info.somefloat_nullable, DBNull.Value)),
				new SqlParameter("@somebool", NullHandler.HandleAppNull(info.somebool, DBNull.Value)),
				new SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(info.somebool_nullable, DBNull.Value))
			);
		}

		/// <summary>
		/// Updates a record in the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Update, false)]
		public static void Update(Int32 id, String description, String notes, Int32 someint, Int32? someint_nullable, DateTime somedate, DateTime? somedate_nullable, Double somefloat, Double? somefloat_nullable, Boolean somebool, Boolean? somebool_nullable) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterUpdate]",
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)),
				new SqlParameter("@description", NullHandler.HandleAppNull(description, DBNull.Value)),
				new SqlParameter("@notes", NullHandler.HandleAppNull(notes, DBNull.Value)),
				new SqlParameter("@someint", NullHandler.HandleAppNull(someint, DBNull.Value)),
				new SqlParameter("@someint_nullable", NullHandler.HandleAppNull(someint_nullable, DBNull.Value)),
				new SqlParameter("@somedate", NullHandler.HandleAppNull(somedate, DBNull.Value)),
				new SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(somedate_nullable, DBNull.Value)),
				new SqlParameter("@somefloat", NullHandler.HandleAppNull(somefloat, DBNull.Value)),
				new SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(somefloat_nullable, DBNull.Value)),
				new SqlParameter("@somebool", NullHandler.HandleAppNull(somebool, DBNull.Value)),
				new SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(somebool_nullable, DBNull.Value))
			);
		}

		/// <summary>
		/// Updates a record in the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
		public static void UpdateInfo(Test_masterInfo info) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterUpdate]",
				new SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)),
				new SqlParameter("@description", NullHandler.HandleAppNull(info.description, DBNull.Value)),
				new SqlParameter("@notes", NullHandler.HandleAppNull(info.notes, DBNull.Value)),
				new SqlParameter("@someint", NullHandler.HandleAppNull(info.someint, DBNull.Value)),
				new SqlParameter("@someint_nullable", NullHandler.HandleAppNull(info.someint_nullable, DBNull.Value)),
				new SqlParameter("@somedate", NullHandler.HandleAppNull(info.somedate, DBNull.Value)),
				new SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(info.somedate_nullable, DBNull.Value)),
				new SqlParameter("@somefloat", NullHandler.HandleAppNull(info.somefloat, DBNull.Value)),
				new SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(info.somefloat_nullable, DBNull.Value)),
				new SqlParameter("@somebool", NullHandler.HandleAppNull(info.somebool, DBNull.Value)),
				new SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(info.somebool_nullable, DBNull.Value))
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_master] table by a composite primary key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
		public static void Delete(Int32 id) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterDelete]",
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value))
			);
		}

		/// <summary>
		/// Deletes a record from the [dbo].[test_master] table by a composite primary key.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
		public static void DeleteInfo(Test_masterInfo info) 
		{
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterDelete]",
				new SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value))
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static SqlDataReader SelectDR(Int32 id) 
		{
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelect]",
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value))
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static DataSet SelectDS(Int32 id) 
		{
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelect]",
				new SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value))
			);
		}

		/// <summary>
		/// Selects a single record from the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public static BindingListView<Test_masterInfo> SelectInfo(Test_masterInfo info) 
		{
			using (SqlDataReader dr = SelectDR(info.id)) 
			{
				return LoadListDR(dr);
			}
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static SqlDataReader SelectDRAll() 
		{
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelectAll]");
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
		public static DataSet SelectDSAll() 
		{
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelectAll]");
		}

		/// <summary>
		/// Selects all records from the [dbo].[test_master] table.
		/// </summary>
		[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
		public static BindingListView<Test_masterInfo> SelectInfoAll() 
		{
			using (SqlDataReader dr = SelectDRAll()) 
			{
				return LoadListDR(dr);
			}
		}

		/// <summary>
		/// Loads all records from the [dbo].[test_master] table into List of Test_masterInfo.
		/// </summary>
		protected static BindingListView<Test_masterInfo> LoadListDR(SqlDataReader dr) 
		{ 
			BindingListView<Test_masterInfo> infoList = new BindingListView<Test_masterInfo>();
			while (dr.Read()) 
			{
				infoList.Add
					(
					new Test_masterInfo
						(
						(Int32)NullHandler.HandleDbNull(dr["id"]),
						(String)NullHandler.HandleDbNull(dr["description"]),
						(String)NullHandler.HandleDbNull(dr["notes"]),
						(Int32)NullHandler.HandleDbNull(dr["someint"]),
						(Int32?)NullHandler.HandleDbNull(dr["someint_nullable"]),
						(DateTime)NullHandler.HandleDbNull(dr["somedate"]),
						(DateTime?)NullHandler.HandleDbNull(dr["somedate_nullable"]),
						(Double)NullHandler.HandleDbNull(dr["somefloat"]),
						(Double?)NullHandler.HandleDbNull(dr["somefloat_nullable"]),
						(Boolean)NullHandler.HandleDbNull(dr["somebool"]),
						(Boolean?)NullHandler.HandleDbNull(dr["somebool_nullable"])
						)
					);
			}
			return infoList;
		}
	}
}

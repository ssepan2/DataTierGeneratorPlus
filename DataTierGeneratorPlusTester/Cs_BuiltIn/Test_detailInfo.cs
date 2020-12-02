using System;
using System.Collections;
using System.ComponentModel;

namespace Cs_BuiltIn {

	/// <summary>
	/// Class that stores table fields.
	/// </summary>
	public class Test_detailInfo {

		/// <summary>
		/// Default constructor.  
		/// </summary>
		public Test_detailInfo() {}

		/// <summary>
		/// Constructor with values.  
		/// </summary>
		public Test_detailInfo(
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
		[DataObjectFieldAttribute(true, false, false)]
		public Int32 master_id{
			get { return _master_id; }
			set { _master_id = value; }
		}

		/// <summary>
		/// id  
		/// </summary>
		[DataObjectFieldAttribute(true, false, false)]
		public String id{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// description  
		/// </summary>
		[DataObjectFieldAttribute(false, false, false)]
		public String description{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// qty  
		/// </summary>
		[DataObjectFieldAttribute(false, false, false)]
		public Int32 qty{
			get { return _qty; }
			set { _qty = value; }
		}

		/// <summary>
		/// amt  
		/// </summary>
		[DataObjectFieldAttribute(false, false, false)]
		public Double amt{
			get { return _amt; }
			set { _amt = value; }
		}
	}
}

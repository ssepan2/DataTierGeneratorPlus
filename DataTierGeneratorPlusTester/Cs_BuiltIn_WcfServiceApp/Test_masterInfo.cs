using System;
using System.Collections;
using System.ComponentModel;

namespace Cs_BuiltIn_WcfServiceApp {

	/// <summary>
	/// Class that stores table fields.
	/// </summary>
	public class Test_masterInfo {

		/// <summary>
		/// Default constructor.  
		/// </summary>
		public Test_masterInfo() {}

		/// <summary>
		/// Constructor with values.  
		/// </summary>
		public Test_masterInfo(
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
		[DataObjectFieldAttribute(true, false, false)]
		public Int32 id{
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
		/// notes  
		/// </summary>
		[DataObjectFieldAttribute(false, false, true)]
		public String notes{
			get { return _notes; }
			set { _notes = value; }
		}

		/// <summary>
		/// someint  
		/// </summary>
		[DataObjectFieldAttribute(false, false, false)]
		public Int32 someint{
			get { return _someint; }
			set { _someint = value; }
		}

		/// <summary>
		/// someint_nullable  
		/// </summary>
		[DataObjectFieldAttribute(false, false, true)]
		public Int32? someint_nullable{
			get { return _someint_nullable; }
			set { _someint_nullable = value; }
		}

		/// <summary>
		/// somedate  
		/// </summary>
		[DataObjectFieldAttribute(false, false, false)]
		public DateTime somedate{
			get { return _somedate; }
			set { _somedate = value; }
		}

		/// <summary>
		/// somedate_nullable  
		/// </summary>
		[DataObjectFieldAttribute(false, false, true)]
		public DateTime? somedate_nullable{
			get { return _somedate_nullable; }
			set { _somedate_nullable = value; }
		}

		/// <summary>
		/// somefloat  
		/// </summary>
		[DataObjectFieldAttribute(false, false, false)]
		public Double somefloat{
			get { return _somefloat; }
			set { _somefloat = value; }
		}

		/// <summary>
		/// somefloat_nullable  
		/// </summary>
		[DataObjectFieldAttribute(false, false, true)]
		public Double? somefloat_nullable{
			get { return _somefloat_nullable; }
			set { _somefloat_nullable = value; }
		}

		/// <summary>
		/// somebool  
		/// </summary>
		[DataObjectFieldAttribute(false, false, false)]
		public Boolean somebool{
			get { return _somebool; }
			set { _somebool = value; }
		}

		/// <summary>
		/// somebool_nullable  
		/// </summary>
		[DataObjectFieldAttribute(false, false, true)]
		public Boolean? somebool_nullable{
			get { return _somebool_nullable; }
			set { _somebool_nullable = value; }
		}
	}
}

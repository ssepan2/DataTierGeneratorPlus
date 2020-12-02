using System;

namespace DataTierGeneratorPlusLibrary 
{
	/// <summary>
	/// Class that stores information for columns in a database table.
	/// </summary>
	public class Column 
    {

		private String _Name;
		/// <summary>
		/// Name of the column.
		/// </summary>
		public String Name {
			get { return _Name; }
			set { _Name = value; }
		}

		private String _ProgrammaticAlias;
		/// <summary>
		/// Extended name of the column.
		/// </summary>
		public String ProgrammaticAlias {
			get { return _ProgrammaticAlias; }
			set { _ProgrammaticAlias = value; }
		}

		private String _Type;
		/// <summary>
		/// Data type of the column.
		/// </summary>
		public String Type {
			get { return _Type; }
			set { _Type = value; }
		}

		private String _Length;
		/// <summary>
		/// Length in bytes of the column.
		/// </summary>
		public String Length {
			get { return _Length; }
			set { _Length = value; }
		}
		
		private String _Precision;
		/// <summary>
		/// Precision of the column.  Applicable to decimal, float, and numeric data types only.
		/// </summary>
		public String Precision {
			get { return _Precision; }
			set { _Precision = value; }
		}
		
		private String _Scale;
		/// <summary>
		/// Scale of the column.  Applicable to decimal, and numeric data types only.
		/// </summary>
		public String Scale {
			get { return _Scale; }
			set { _Scale = value; }
		}
		
		private Boolean _IsRowGuidCol;
		/// <summary>
		/// Flags the column as a uniqueidentifier column.
		/// </summary>
		public Boolean IsRowGuidCol {
			get { return _IsRowGuidCol; }
			set { _IsRowGuidCol = value; }
		}

		private Boolean _IsIdentity;
		/// <summary>
		/// Flags the column as an identity column.
		/// </summary>
		public Boolean IsIdentity {
			get { return _IsIdentity; }
			set { _IsIdentity = value; }
		}

		private Boolean _IsComputed;
		/// <summary>
		/// Flags the column as being computed.
		/// </summary>
		public Boolean IsComputed {
			get { return _IsComputed; }
			set { _IsComputed = value; }
		}

		private Boolean _IsNullable;
        /// <summary>
		/// Flags the column as being nullable.
		/// </summary>
		public Boolean IsNullable {
			get { return _IsNullable; }
			set { _IsNullable = value; }
		}

  		private Boolean _IsPrimaryKey;//<--under development
        /// <summary>
		/// Flags the column as being nullable.
		/// </summary>
        public Boolean IsPrimaryKey//<--under development
        {
            get { return _IsPrimaryKey; }
            set { _IsPrimaryKey = value; }
        }
	
    }
}

using System;

namespace DataTierGeneratorPlus {
	/// <summary>
	/// Class that stores information for columns in a database table.
	/// </summary>
	public class Column {
		// Private variable used to hold the property values
		private String name;
		private String programmaticAlias;
		private String type;
		private String length;
		private String precision;
		private String scale;
		private Boolean isRowGuidCol;
		private Boolean isIdentity;
		private Boolean isComputed;
		private Boolean isNullable;
		private Boolean isPrimaryKey;//<--under development

		/// <summary>
		/// Name of the column.
		/// </summary>
		public String Name {
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Extended name of the column.
		/// </summary>
		public String ProgrammaticAlias {
			get { return programmaticAlias; }
			set { programmaticAlias = value; }
		}

		/// <summary>
		/// Data type of the column.
		/// </summary>
		public String Type {
			get { return type; }
			set { type = value; }
		}

		/// <summary>
		/// Length in bytes of the column.
		/// </summary>
		public String Length {
			get { return length; }
			set { length = value; }
		}
		
		/// <summary>
		/// Precision of the column.  Applicable to decimal, float, and numeric data types only.
		/// </summary>
		public String Precision {
			get { return precision; }
			set { precision = value; }
		}
		
		/// <summary>
		/// Scale of the column.  Applicable to decimal, and numeric data types only.
		/// </summary>
		public String Scale {
			get { return scale; }
			set { scale = value; }
		}
		
		/// <summary>
		/// Flags the column as a uniqueidentifier column.
		/// </summary>
		public Boolean IsRowGuidCol {
			get { return isRowGuidCol; }
			set { isRowGuidCol = value; }
		}

		/// <summary>
		/// Flags the column as an identity column.
		/// </summary>
		public Boolean IsIdentity {
			get { return isIdentity; }
			set { isIdentity = value; }
		}

		/// <summary>
		/// Flags the column as being computed.
		/// </summary>
		public Boolean IsComputed {
			get { return isComputed; }
			set { isComputed = value; }
		}

        /// <summary>
		/// Flags the column as being nullable.
		/// </summary>
		public Boolean IsNullable {
			get { return isNullable; }
			set { isNullable = value; }
		}

        /// <summary>
		/// Flags the column as being nullable.
		/// </summary>
        public Boolean IsPrimaryKey//<--under development
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
		}
	
    }
}

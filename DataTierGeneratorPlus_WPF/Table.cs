using System;
using System.Collections;

namespace DataTierGeneratorPlus {
	/// <summary>
	/// Class that stores information for tables in a database.
	/// </summary>
	public class Table {
		String catalog;
		String schema;
		String name;
		String programmaticAlias;
		Boolean _isSelected=false;
		ArrayList columns;
		ArrayList primaryKeys;
		Hashtable foreignKeys;
		
		/// <summary>
		/// Default constructor.  All collections are initialized.
		/// </summary>
		public Table() {
			columns = new ArrayList();
			primaryKeys = new ArrayList();
			foreignKeys = new Hashtable();
		}

		/// <summary>
		/// Selected table(s).
		/// </summary>
		public Boolean IsSelected 
		{
			get { return _isSelected; }
			set { _isSelected = value; }
		}

		/// <summary>
		/// Name of the table.
		/// </summary>
		public String Name {
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Represents the name that should be used in code to represent the class.
		/// </summary>
		public String ProgrammaticAlias {
			get { return programmaticAlias; }
			set { programmaticAlias = value; }
		}
		
		/// <summary>
		/// Contains the list of Column instances that define the table.
		/// </summary>
		public ArrayList Columns {
			get { return columns; }
		}

		/// <summary>
        /// Catalog of the table (database).
		/// </summary>
        public String Catalog
        {
            get { return catalog; }
            set { catalog = value; }
		}

		/// <summary>
        /// Schema of the table (owner).
		/// </summary>
        public String Schema
        {
            get { return schema; }
            set { schema = value; }
		}

		/// <summary>
		/// Contains the list of primary key Column instances that define the table.
		/// </summary>
		public ArrayList PrimaryKeys {
			get { return primaryKeys; }
		}

		/// <summary>
		/// Contains the list of Column instances that define the table.  The Hashtable returned 
		/// is keyed on the foreign key name, and the value associated with the key is an 
		/// ArrayList of Column instances that compose the foreign key.
		/// </summary>
		public Hashtable ForeignKeys {
			get { return foreignKeys; }
		}
	}
}

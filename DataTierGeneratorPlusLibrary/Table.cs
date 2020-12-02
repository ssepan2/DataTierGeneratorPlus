using System;
using System.Collections;
using System.Collections.Generic;

namespace DataTierGeneratorPlusLibrary 
{
	/// <summary>
	/// Class that stores information for tables in a database.
	/// </summary>
	public class Table 
    {
		
		/// <summary>
		/// Default constructor.  All collections are initialized.
		/// </summary>
		public Table() {
            _Columns = new List<Column>();
            _PrimaryKeys = new List<Column>();
            //_ForeignKeys = new Hashtable();
            _ForeignKeys = new Dictionary<String, List<Column>>();
		}

		private Boolean _IsSelected=false;
		/// <summary>
		/// Selected table(s).
		/// </summary>
		public Boolean IsSelected 
		{
			get { return _IsSelected; }
			set { _IsSelected = value; }
		}

		private String _Name;
		/// <summary>
		/// Name of the table.
		/// </summary>
		public String Name {
			get { return _Name; }
			set { _Name = value; }
		}

		private String _ProgrammaticAlias;
		/// <summary>
		/// Represents the name that should be used in code to represent the class.
		/// </summary>
		public String ProgrammaticAlias {
			get { return _ProgrammaticAlias; }
			set { _ProgrammaticAlias = value; }
		}

        private List<Column> _Columns;
		/// <summary>
		/// Contains the list of Column instances that define the table.
		/// </summary>
        public List<Column> Columns
        {
			get { return _Columns; }
		}

		private String _Catalog;
		/// <summary>
        /// Catalog of the table (database).
		/// </summary>
        public String Catalog
        {
            get { return _Catalog; }
            set { _Catalog = value; }
		}

		private String _Schema;
		/// <summary>
        /// Schema of the table (owner).
		/// </summary>
        public String Schema
        {
            get { return _Schema; }
            set { _Schema = value; }
		}

        private List<Column> _PrimaryKeys;
		/// <summary>
		/// Contains the list of primary key Column instances that define the table.
		/// </summary>
        public List<Column> PrimaryKeys
        {
			get { return _PrimaryKeys; }
		}

        //private Hashtable _ForeignKeys;
        private Dictionary<String, List<Column>> _ForeignKeys;
        /// <summary>
		/// Contains the list of Column instances that define the table.  The Hashtable returned 
		/// is keyed on the foreign key name, and the value associated with the key is an 
		/// List of Column instances that compose the foreign key.
		/// </summary>
        //public Hashtable ForeignKeys
        public Dictionary<String, List<Column>> ForeignKeys
        {
			get { return _ForeignKeys; }
		}
	}
}

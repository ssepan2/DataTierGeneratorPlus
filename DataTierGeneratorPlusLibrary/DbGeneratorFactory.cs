using System;

namespace DataTierGeneratorPlusLibrary
{
	/// <summary>
	/// DbGeneratorFactory creates the appropriate DBMS generator.
	/// </summary>
	internal class DbGeneratorFactory
	{
		public DbGeneratorFactory()
		{
		}

		internal static IDbGenerator GetGenerator(GeneratorModel model)
		{
			IDbGenerator objGenerator = null;

			//Generate dbms library
            switch (model.DBMS)
			{
				case GeneratorModel.DBMS_MSSQL:
					objGenerator = new SqlGenerator();
					break;
				// DBMS CUSTOMIZATION: 4 of 6 -- add additional case here for additional DBMS class
				//case Settings.DBMS_MSAccess:
				//	objGenerator = new AccGenerator();
				//	break;
				//case Settings.DBMS_Oracle:
				//	objGenerator = new OraGenerator();
				//	break;
				//case Settings.DBMS_MySQL:
				//	objGenerator = new MySGenerator();
				//	break;
				//case Settings.DBMS_Yyyy:
				//	objGenerator = new YyGenerator();
				//	break;
				default:
				{
					throw new IndexOutOfRangeException();
				}
			}

			return objGenerator;
		}
	}
}

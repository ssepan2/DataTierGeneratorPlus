using System;
using DataTierGeneratorPlusLibrary.MVC;

namespace DataTierGeneratorPlusLibrary
{
	/// <summary>
	/// DbUtilityFactory creates the appropriate DBMS utility.
	/// </summary>
	internal class DbUtilityFactory
	{
		public DbUtilityFactory()
		{
		}

		internal static IDbUtility GetUtility(GeneratorModel model)
		{
			IDbUtility objUtility = null;

			//Generate dbms library
            switch (model.DBMS)
			{
				case GeneratorModel.DBMS_MSSQL:
					objUtility = new SqlUtility();
					break;
				// DBMS CUSTOMIZATION: 5 of 6 -- add additional case here for additional DBMS class
				//case Settings.DBMS_MSAccess:
				//	objUtility = new AccUtility();
				//	break;
				//case Settings.DBMS_Oracle:
				//	objUtility = new OraUtility();
				//	break;
				//case Settings.DBMS_MySQL:
				//	objUtility = new MySUtility();
				//	break;
				//case Settings.DBMS_Yyyy:
				//	objUtility = new YyUtility();
				//	break;
				default:
				{
					throw new IndexOutOfRangeException();
				}
			}

			return objUtility;
		}
	}
}

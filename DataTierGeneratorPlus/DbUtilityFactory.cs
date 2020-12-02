using System;

namespace DataTierGeneratorPlus
{
	/// <summary>
	/// DbUtilityFactory creates the appropriate DBMS utility.
	/// </summary>
	internal class DbUtilityFactory
	{
		public DbUtilityFactory()
		{
		}

		internal static IDbUtility GetUtility(Settings settings)
		{
			IDbUtility objUtility = null;

			//Generate dbms library
			switch(settings.DBMS)
			{
				case Settings.DBMS_MSSQL:
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

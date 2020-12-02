using System.Runtime.Serialization;
using System.ServiceModel;

namespace Cs_BuiltIn_WcfServiceApp 
{

	internal static class Test_masterServiceExtensions
	{

		/// <summary>
		/// Converts from List of Test_masterInfo to List of Test_masterContract.
		/// </summary>
		internal static BindingListView<Test_masterContract> ToBindingListViewOfContract(this BindingListView<Test_masterInfo> infoList) 
		{ 
			BindingListView<Test_masterContract> returnValue = new BindingListView<Test_masterContract>();
			foreach (Test_masterInfo info in infoList) 
			{
				returnValue.Add
					(
					info.ToTest_masterContract()
					);
			}
			return returnValue;
		}

		/// <summary>
		/// Converts from Test_masterInfo to Test_masterContract.
		/// </summary>
		internal static Test_masterContract ToTest_masterContract(this Test_masterInfo info) 
		{
			return new Test_masterContract
			(
				info.id,
				info.description,
				info.notes,
				info.someint,
				info.someint_nullable,
				info.somedate,
				info.somedate_nullable,
				info.somefloat,
				info.somefloat_nullable,
				info.somebool,
				info.somebool_nullable
			);
		}

		/// <summary>
		/// Converts from Test_masterContract to Test_masterInfo.
		/// </summary>
		internal static Test_masterInfo ToTest_masterInfo(this Test_masterContract info) 
		{
			return new Test_masterInfo
			(
				info.id,
				info.description,
				info.notes,
				info.someint,
				info.someint_nullable,
				info.somedate,
				info.somedate_nullable,
				info.somefloat,
				info.somefloat_nullable,
				info.somebool,
				info.somebool_nullable
			);
		}
	}
}

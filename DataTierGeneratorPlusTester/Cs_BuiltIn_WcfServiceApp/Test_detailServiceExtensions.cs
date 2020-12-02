using System.Runtime.Serialization;
using System.ServiceModel;

namespace Cs_BuiltIn_WcfServiceApp 
{

	internal static class Test_detailServiceExtensions
	{

		/// <summary>
		/// Converts from List of Test_detailInfo to List of Test_detailContract.
		/// </summary>
		internal static BindingListView<Test_detailContract> ToBindingListViewOfContract(this BindingListView<Test_detailInfo> infoList) 
		{ 
			BindingListView<Test_detailContract> returnValue = new BindingListView<Test_detailContract>();
			foreach (Test_detailInfo info in infoList) 
			{
				returnValue.Add
					(
					info.ToTest_detailContract()
					);
			}
			return returnValue;
		}

		/// <summary>
		/// Converts from Test_detailInfo to Test_detailContract.
		/// </summary>
		internal static Test_detailContract ToTest_detailContract(this Test_detailInfo info) 
		{
			return new Test_detailContract
			(
				info.master_id,
				info.id,
				info.description,
				info.qty,
				info.amt
			);
		}

		/// <summary>
		/// Converts from Test_detailContract to Test_detailInfo.
		/// </summary>
		internal static Test_detailInfo ToTest_detailInfo(this Test_detailContract info) 
		{
			return new Test_detailInfo
			(
				info.master_id,
				info.id,
				info.description,
				info.qty,
				info.amt
			);
		}
	}
}

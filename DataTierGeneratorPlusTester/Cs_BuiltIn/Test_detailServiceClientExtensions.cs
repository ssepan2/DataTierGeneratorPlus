using System.Collections.Generic;
using Cs_BuiltIn.Test_detailService;

namespace Cs_BuiltIn 
{

	internal static class Test_detailServiceClientExtensions
	{

		/// <summary>
		/// Converts from Array of Test_detailContract to List of Test_detailContract.
		/// </summary>
		internal static BindingListView<Test_detailContract> ToBindingListViewOfContract(this Test_detailContract[] infoList) 
		{ 
			BindingListView<Test_detailContract> returnValue = new BindingListView<Test_detailContract>();
			foreach (Test_detailContract info in infoList) 
			{
				returnValue.Add
					(
					info
					);
			}
			return returnValue;
		}
	}
}

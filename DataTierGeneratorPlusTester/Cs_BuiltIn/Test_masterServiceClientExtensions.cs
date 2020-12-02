using System.Collections.Generic;
using Cs_BuiltIn.Test_masterService;

namespace Cs_BuiltIn 
{

	internal static class Test_masterServiceClientExtensions
	{

		/// <summary>
		/// Converts from Array of Test_masterContract to List of Test_masterContract.
		/// </summary>
		internal static BindingListView<Test_masterContract> ToBindingListViewOfContract(this Test_masterContract[] infoList) 
		{ 
			BindingListView<Test_masterContract> returnValue = new BindingListView<Test_masterContract>();
			foreach (Test_masterContract info in infoList) 
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

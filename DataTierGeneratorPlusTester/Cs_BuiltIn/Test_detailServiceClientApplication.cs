using System;
using System.ComponentModel;
using System.Data;
using Cs_BuiltIn.Test_detailService;

namespace Cs_BuiltIn {

    /// <summary>
    /// Service client code (generated by DataTierGeneratorPlus), to call service client proxy reference (generated by Visual Studio / svcutil).
    /// </summary>
    public static class Test_detailServiceClientApplication
      {
        /// <summary>
        /// Sample service client application code
        /// </summary>
        public static void Test()
        {
            BindingListView<Test_detailService.Test_detailContract> masterList = default(BindingListView<Test_detailService.Test_detailContract>);
            //Test_detailService.Test_detailContract masterInfo = default(Test_detailService.Test_detailContract);
            Test_detailService.Test_detailServiceClient test_detailClient = new Test_detailService.Test_detailServiceClient();

            test_detailClient.Open();

            //call service methods here, using opened service client
            masterList = test_detailClient.SelectInfoAll().ToBindingListViewOfContract();

            if ((test_detailClient.State != System.ServiceModel.CommunicationState.Closed))
            {
                test_detailClient.Close();
            }
        }
      }

}
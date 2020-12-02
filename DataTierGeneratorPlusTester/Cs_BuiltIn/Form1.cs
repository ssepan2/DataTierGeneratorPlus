#define DAL
#define BLL
#define WCF
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;//temp
using Cs_BuiltIn.Test_masterService; //<--add
using Cs_BuiltIn.Test_detailService; //<--add

namespace Cs_BuiltIn
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.Label lblOutput;
        private BindingSource bindingSource1;
        private DataGridView dataGridView1;
        private Button cmdTestDal;
        private Button cmdTestBll;
        private Button cmdTestSvc;
        private IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdTestDal = new System.Windows.Forms.Button();
            this.cmdTestBll = new System.Windows.Forms.Button();
            this.cmdTestSvc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.Location = new System.Drawing.Point(16, 9);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(352, 70);
            this.lblConnectionString.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(16, 199);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(360, 122);
            this.lblStatus.TabIndex = 2;
            // 
            // lblOutput
            // 
            this.lblOutput.Location = new System.Drawing.Point(5, 79);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(344, 120);
            this.lblOutput.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 333);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(360, 230);
            this.dataGridView1.TabIndex = 4;
            // 
            // cmdTestDal
            // 
            this.cmdTestDal.Location = new System.Drawing.Point(12, 569);
            this.cmdTestDal.Name = "cmdTestDal";
            this.cmdTestDal.Size = new System.Drawing.Size(75, 23);
            this.cmdTestDal.TabIndex = 5;
            this.cmdTestDal.Text = "Test DAL";
            this.cmdTestDal.UseVisualStyleBackColor = true;
            this.cmdTestDal.Click += new System.EventHandler(this.cmdTestDal_Click);
            // 
            // cmdTestBll
            // 
            this.cmdTestBll.Location = new System.Drawing.Point(93, 568);
            this.cmdTestBll.Name = "cmdTestBll";
            this.cmdTestBll.Size = new System.Drawing.Size(75, 23);
            this.cmdTestBll.TabIndex = 5;
            this.cmdTestBll.Text = "Test BLL";
            this.cmdTestBll.UseVisualStyleBackColor = true;
            this.cmdTestBll.Click += new System.EventHandler(this.cmdTestBll_Click);
            // 
            // cmdTestSvc
            // 
            this.cmdTestSvc.Location = new System.Drawing.Point(174, 569);
            this.cmdTestSvc.Name = "cmdTestSvc";
            this.cmdTestSvc.Size = new System.Drawing.Size(75, 23);
            this.cmdTestSvc.TabIndex = 5;
            this.cmdTestSvc.Text = "Test SVC";
            this.cmdTestSvc.UseVisualStyleBackColor = true;
            this.cmdTestSvc.Click += new System.EventHandler(this.cmdTestSvc_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(425, 603);
            this.Controls.Add(this.cmdTestSvc);
            this.Controls.Add(this.cmdTestBll);
            this.Controls.Add(this.cmdTestDal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblConnectionString);
            this.Name = "Form1";
            this.Text = "C# Built In";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{


		}

        private void cmdTestDal_Click(object sender, EventArgs e)
        {
#if DAL
            DataSet ds = default(DataSet);
            IDataReader dr = default(IDataReader);

            lblConnectionString.Text = "";
            lblStatus.Text = "";
            lblOutput.Text = "";
            dataGridView1.DataSource = bindingSource1;

            //get connection string
            lblStatus.Text += "A";
            string strConnectionString = DatabaseUtility.GetConnectionString();
            lblConnectionString.Text += strConnectionString;
            Application.DoEvents();

            //open/close connection
            lblStatus.Text += "B";
            SqlConnection connection = DatabaseUtility.GetConnection();
            connection.Close();
            Application.DoEvents();

            //select master all (datereader)
            lblStatus.Text += "C";
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelectAll]");
            while ((dr.Read()))
            {
                lblOutput.Text += dr["description"] + ",";
            }
            Application.DoEvents();

            //select master all (dataset)
            lblStatus.Text += "D";
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_masterSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select master by master key (datereader)
            lblStatus.Text += "E";
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelect]", new SqlParameter("@id", 1));
            while ((dr.Read()))
            {
                lblOutput.Text += dr["description"] + ",";
            }
            Application.DoEvents();

            //select master by master key (dataset)
            lblStatus.Text += "F";
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelect]", new SqlParameter("@id", 1));
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //insert master
            lblStatus.Text += "G";
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterInsert]", new SqlParameter("@id", NullHandler.HandleAppNull(123, DBNull.Value)), new SqlParameter("@description", NullHandler.HandleAppNull("abc", DBNull.Value)), new SqlParameter("@notes", NullHandler.HandleAppNull(null, DBNull.Value)), new SqlParameter("@someint", NullHandler.HandleAppNull(1, DBNull.Value)), new SqlParameter("@someint_nullable", NullHandler.HandleAppNull(null, DBNull.Value)), new SqlParameter("@somedate", NullHandler.HandleAppNull(DateTime.Parse("12/26/2005 1:01:01 PM"), DBNull.Value)), new SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(null, DBNull.Value)),
            new SqlParameter("@somefloat", NullHandler.HandleAppNull(1.1, DBNull.Value)), new SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(null, DBNull.Value)), new SqlParameter("@somebool", NullHandler.HandleAppNull(true, DBNull.Value)), new SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(null, DBNull.Value)));
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_masterSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //update master
            lblStatus.Text += "H";
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterUpdate]", new SqlParameter("@id", NullHandler.HandleAppNull(123, DBNull.Value)), new SqlParameter("@description", NullHandler.HandleAppNull("abc", DBNull.Value)), new SqlParameter("@notes", NullHandler.HandleAppNull("Thingamajig", DBNull.Value)), new SqlParameter("@someint", NullHandler.HandleAppNull(1, DBNull.Value)), new SqlParameter("@someint_nullable", NullHandler.HandleAppNull(1, DBNull.Value)), new SqlParameter("@somedate", NullHandler.HandleAppNull(DateTime.Parse("12/26/2005 1:01:01 PM"), DBNull.Value)), new SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(DateTime.Parse("12/26/2005 1:01:01 PM"), DBNull.Value)),
            new SqlParameter("@somefloat", NullHandler.HandleAppNull(1.1, DBNull.Value)), new SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(1.1, DBNull.Value)), new SqlParameter("@somebool", NullHandler.HandleAppNull(true, DBNull.Value)), new SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(true, DBNull.Value)));
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_masterSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //insert detail
            lblStatus.Text += "I";
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailInsert]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), new SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)), new SqlParameter("@description", NullHandler.HandleAppNull("Desc1A", DBNull.Value)), new SqlParameter("@qty", NullHandler.HandleAppNull(1, DBNull.Value)), new SqlParameter("@amt", NullHandler.HandleAppNull(10, DBNull.Value)));
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select detail all (datereader)
            lblStatus.Text += "J";
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectAll]");
            while ((dr.Read()))
            {
                lblOutput.Text += dr["description"] + ",";
            }
            Application.DoEvents();

            //select detail all (dataset)
            lblStatus.Text += "K";
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select detail by detail key (datereader)
            lblStatus.Text += "L";
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelect]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), new SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)));
            while ((dr.Read()))
            {
                lblOutput.Text += dr["description"] + ",";
            }
            Application.DoEvents();

            //select detail by detail key (dataset)
            lblStatus.Text += "M";
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelect]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), new SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)));
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //update detail
            lblStatus.Text += "N";
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailUpdate]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), new SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)), new SqlParameter("@description", NullHandler.HandleAppNull("Desc1A_", DBNull.Value)), new SqlParameter("@qty", NullHandler.HandleAppNull(2, DBNull.Value)), new SqlParameter("@amt", NullHandler.HandleAppNull(20, DBNull.Value)));
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //delete detail
            lblStatus.Text += "K";
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDelete]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), new SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)));
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select detail by master key (datareader)
            lblStatus.Text += "P";
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailInsert]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), new SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)), new SqlParameter("@description", NullHandler.HandleAppNull("Desc1A", DBNull.Value)), new SqlParameter("@qty", NullHandler.HandleAppNull(1, DBNull.Value)), new SqlParameter("@amt", NullHandler.HandleAppNull(10, DBNull.Value)));
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectByMaster_id]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)));
            while ((dr.Read()))
            {
                lblOutput.Text += dr["description"] + ",";
            }
            Application.DoEvents();

            //select detail by master key (dataset)
            lblStatus.Text += "Q";
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectByMaster_id]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)));
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //delete detail by master key
            lblStatus.Text += "R";
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDeleteByMaster_id]", new SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)));
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //delete master
            lblStatus.Text += "S";
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterDelete]", new SqlParameter("@id", NullHandler.HandleAppNull(123, DBNull.Value)));
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_masterSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();
#endif 
        }

        private void cmdTestBll_Click(object sender, EventArgs e)
        {
#if BLL
            DataSet ds = default(DataSet);
            IDataReader dr = default(IDataReader);
            BindingListView<Test_masterInfo> masterList = default(BindingListView<Test_masterInfo>);
            BindingListView<Test_detailInfo> detailList = default(BindingListView<Test_detailInfo>);
            Test_masterInfo masterInfo = default(Test_masterInfo);
            Test_detailInfo detailInfo = default(Test_detailInfo);

            lblConnectionString.Text = "";
            lblStatus.Text = "";
            lblOutput.Text = "";
            dataGridView1.DataSource = bindingSource1;

            //select master all (datereader)
            lblStatus.Text += "A";
            dr = Test_masterController.SelectDRAll();
            while ((dr.Read()))
            {
                lblOutput.Text += dr["description"] + ",";
            }
            Application.DoEvents();

            //select master all (dataset)
            lblStatus.Text += "B";
            ds = Test_masterController.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select master all (info)
            lblStatus.Text += "C";
            masterList = Test_masterController.SelectInfoAll();
            bindingSource1.DataSource = masterList;
            Application.DoEvents();

            //select master by master key (datereader)
            lblStatus.Text += "D";
            dr = Test_masterController.SelectDR(1);
            while ((dr.Read()))
            {
                lblOutput.Text += dr["description"] + ",";
            }
            Application.DoEvents();

            //select master by master key (dataset)
            lblStatus.Text += "E";
            ds = Test_masterController.SelectDS(1);
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select master by master key (info)
            lblStatus.Text += "F";
            masterInfo = new Test_masterInfo();
            masterInfo.id = 123;
            masterList = Test_masterController.SelectInfo(masterInfo);
            bindingSource1.DataSource = masterList;
            Application.DoEvents();

            //insert master
            lblStatus.Text += "G";
            Test_masterController.Insert(123, "abc", null, 1, null, DateTime.Parse("12/26/2005 1:01:01 PM"), null, 1.1, null, true,
            null);
            ds = Test_masterController.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //update master
            lblStatus.Text += "H";
            Test_masterController.Update(123, "abc", "Thingamajig", 1, 1, DateTime.Parse("12/26/2005 1:01:01 PM"), DateTime.Parse("12/26/2005 1:01:01 PM"), 1.1, 1.1, true,
            false);
            ds = Test_masterController.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //insert detail (info)
            lblStatus.Text += "I";
            detailInfo = new Test_detailInfo(123, "1", "DescA", 1, 10);
            Test_detailController.InsertInfo(detailInfo);
            detailInfo = null;
            ds = Test_detailController.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //update detail (info)
            lblStatus.Text += "J";
            detailInfo = new Test_detailInfo(123, "1", "DescA_", 2, 20);
            Test_detailController.UpdateInfo(detailInfo);
            detailInfo = null;
            ds = Test_detailController.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //delete detail (info)
            lblStatus.Text += "K";
            detailInfo = new Test_detailInfo();
            detailInfo.master_id = 123;
            detailInfo.id = "1";
            Test_detailController.DeleteInfo(detailInfo);
            detailInfo = null;
            ds = Test_detailController.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select detail by master key (datareader)
            lblStatus.Text += "L";
            Test_detailController.Insert(123, "1", "Desc1A", 1, 10);
            dr = Test_detailController.SelectDRByMaster_id(123);
            while ((dr.Read()))
            {
                lblOutput.Text += dr["description"] + ",";
            }
            Application.DoEvents();

            //select detail by master key (dataset)
            lblStatus.Text += "M";
            ds = Test_detailController.SelectDSByMaster_id(123);
            ds = Test_detailController.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select detail by master key (info)
            lblStatus.Text += "N";
            detailInfo = new Test_detailInfo();
            detailInfo.master_id = 123;
            detailInfo.id = "1";
            detailList = Test_detailController.SelectInfoByMaster_id(detailInfo);
            detailInfo = null;
            bindingSource1.DataSource = detailList;
            Application.DoEvents();

            //delete detail by master key
            lblStatus.Text += "O";
            Test_detailController.DeleteByMaster_id(123);
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), null, "test_detailSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //detail delete by master key w/ info
            lblStatus.Text += "P";
            Test_detailController.Insert(123, "1", "Desc1A", 1, 10);
            detailInfo = new Test_detailInfo();
            detailInfo.master_id = 123;
            Test_detailController.DeleteInfoByMaster_id(detailInfo);
            detailInfo = null;
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), null, "test_detailSelectAll");
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //delete master
            lblStatus.Text += "Q";
            Test_masterController.Delete(123);
            ds = Test_masterController.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();
#endif
        }

        private void cmdTestSvc_Click(object sender, EventArgs e)
        {
            Test_masterServiceClientApplication.Test();
#if WCF
            DataSet ds = default(DataSet);
            BindingListView<Test_masterService.Test_masterContract> masterList = default(BindingListView<Test_masterService.Test_masterContract>);
            BindingListView<Test_detailService.Test_detailContract> detailList = default(BindingListView<Test_detailService.Test_detailContract>);
            Test_masterService.Test_masterContract masterInfo = default(Test_masterService.Test_masterContract);
            Test_detailService.Test_detailContract detailInfo = default(Test_detailService.Test_detailContract);
            Test_masterService.Test_masterServiceClient test_masterClient = new Test_masterService.Test_masterServiceClient();
            Test_detailService.Test_detailServiceClient test_detailClient = new Test_detailService.Test_detailServiceClient();

            test_masterClient.Open();
            test_detailClient.Open();

            lblConnectionString.Text = "";
            lblStatus.Text = "";
            lblOutput.Text = "";
            dataGridView1.DataSource = bindingSource1;

            //select master all (dataset)
            lblStatus.Text += "A";
            ds = test_masterClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select master all (info)
            lblStatus.Text += "B";
            masterList = test_masterClient.SelectInfoAll().ToBindingListViewOfContract();
            bindingSource1.DataSource = masterList;
            Application.DoEvents();

            //select master by master key (dataset)
            lblStatus.Text += "C";
            ds = test_masterClient.SelectDS(1);
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select master by master key (info)
            lblStatus.Text += "D";
            masterInfo = new Test_masterContract();
            masterInfo.id = 123;
            masterList = test_masterClient.SelectInfo(masterInfo).ToBindingListViewOfContract();
            bindingSource1.DataSource = masterList;
            Application.DoEvents();

            //insert master
            lblStatus.Text += "E";
            test_masterClient.Insert(123, "abc", null, 1, null, DateTime.Parse("12/26/2005 1:01:01 PM"), null, 1.1, null, true,
            null);
            ds = test_masterClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //update master
            lblStatus.Text += "F";
            test_masterClient.Update(123, "abc", "Thingamajig", 1, 1, DateTime.Parse("12/26/2005 1:01:01 PM"), DateTime.Parse("12/26/2005 1:01:01 PM"), 1.1, 1.1, true,
            false);
            ds = test_masterClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //insert detail (info)
            lblStatus.Text += "G";
            detailInfo = new Test_detailContract();
            detailInfo.master_id = 123;
            detailInfo.id = "1";
            detailInfo.description = "DescA";
            detailInfo.qty = 1;
            detailInfo.amt = 10;
            test_detailClient.InsertInfo(detailInfo);
            detailInfo = null;
            ds = test_detailClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //update detail (info)
            lblStatus.Text += "H";
            detailInfo = new Test_detailContract();
            detailInfo.master_id = 123;
            detailInfo.id = "1";
            detailInfo.description = "DescA_";
            detailInfo.qty = 2;
            detailInfo.amt = 20;
            test_detailClient.UpdateInfo(detailInfo);
            detailInfo = null;
            ds = test_detailClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //delete detail (info)
            lblStatus.Text += "I";
            detailInfo = new Test_detailContract();
            detailInfo.master_id = 123;
            detailInfo.id = "1";
            test_detailClient.DeleteInfo(detailInfo);
            detailInfo = null;
            ds = test_detailClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select detail by master key (dataset)
            lblStatus.Text += "J";
            ds = test_detailClient.SelectDSByMaster_id(123);
            ds = test_detailClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //select detail by master key (info)
            lblStatus.Text += "K";
            detailInfo = new Test_detailContract();
            detailInfo.master_id = 123;
            detailInfo.id = "1";
            detailList = test_detailClient.SelectInfoByMaster_id(detailInfo).ToBindingListViewOfContract();
            detailInfo = null;
            bindingSource1.DataSource = detailList;
            Application.DoEvents();

            //delete detail by master key
            lblStatus.Text += "L";
            test_detailClient.DeleteByMaster_id(123);
            ds = test_detailClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //detail delete by master key w/ info
            lblStatus.Text += "M";
            test_detailClient.Insert(123, "1", "Desc1A", 1, 10);
            detailInfo = new Test_detailContract();
            detailInfo.master_id = 123;
            test_detailClient.DeleteInfoByMaster_id(detailInfo);
            detailInfo = null;
            ds = test_detailClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            //delete master
            lblStatus.Text += "N";
            test_masterClient.Delete(123);
            ds = test_masterClient.SelectDSAll();
            bindingSource1.DataSource = ds.Tables[0].DefaultView;
            Application.DoEvents();

            if ((test_detailClient.State != System.ServiceModel.CommunicationState.Closed))
            {
                test_detailClient.Close();
            }

            if ((test_masterClient.State != System.ServiceModel.CommunicationState.Closed))
            {
                test_masterClient.Close();
            }
#endif
        }
	}
}

Imports System.Data
Imports System.Data.SqlClient
Imports VB_BuiltIn.Test_masterService '<--add
Imports VB_BuiltIn.Test_detailService '<--add

Namespace VB_BuiltIn

    Public Class Form1
        Inherits System.Windows.Forms.Form


#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents lblOutput As System.Windows.Forms.Label
        Friend WithEvents lblStatus As System.Windows.Forms.Label
        Friend WithEvents lblConnectionString As System.Windows.Forms.Label
        Private WithEvents dataGridView1 As System.Windows.Forms.DataGridView
        Private WithEvents bindingSource1 As System.Windows.Forms.BindingSource
        Friend WithEvents cmdTestBll As System.Windows.Forms.Button
        Friend WithEvents cmdTestSvc As System.Windows.Forms.Button
        Friend WithEvents cmdTestDal As System.Windows.Forms.Button
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.lblOutput = New System.Windows.Forms.Label
            Me.lblStatus = New System.Windows.Forms.Label
            Me.lblConnectionString = New System.Windows.Forms.Label
            Me.dataGridView1 = New System.Windows.Forms.DataGridView
            Me.bindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
            Me.cmdTestDal = New System.Windows.Forms.Button
            Me.cmdTestBll = New System.Windows.Forms.Button
            Me.cmdTestSvc = New System.Windows.Forms.Button
            CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.bindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblOutput
            '
            Me.lblOutput.Location = New System.Drawing.Point(20, 81)
            Me.lblOutput.Name = "lblOutput"
            Me.lblOutput.Size = New System.Drawing.Size(344, 116)
            Me.lblOutput.TabIndex = 7
            '
            'lblStatus
            '
            Me.lblStatus.Location = New System.Drawing.Point(20, 197)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(360, 123)
            Me.lblStatus.TabIndex = 6
            '
            'lblConnectionString
            '
            Me.lblConnectionString.Location = New System.Drawing.Point(20, 9)
            Me.lblConnectionString.Name = "lblConnectionString"
            Me.lblConnectionString.Size = New System.Drawing.Size(352, 72)
            Me.lblConnectionString.TabIndex = 5
            '
            'dataGridView1
            '
            Me.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dataGridView1.Location = New System.Drawing.Point(12, 323)
            Me.dataGridView1.Name = "dataGridView1"
            Me.dataGridView1.Size = New System.Drawing.Size(360, 240)
            Me.dataGridView1.TabIndex = 8
            '
            'cmdTestDal
            '
            Me.cmdTestDal.Location = New System.Drawing.Point(12, 569)
            Me.cmdTestDal.Name = "cmdTestDal"
            Me.cmdTestDal.Size = New System.Drawing.Size(75, 23)
            Me.cmdTestDal.TabIndex = 9
            Me.cmdTestDal.Text = "Test DAL"
            Me.cmdTestDal.UseVisualStyleBackColor = True
            '
            'cmdTestBll
            '
            Me.cmdTestBll.Location = New System.Drawing.Point(93, 569)
            Me.cmdTestBll.Name = "cmdTestBll"
            Me.cmdTestBll.Size = New System.Drawing.Size(75, 23)
            Me.cmdTestBll.TabIndex = 9
            Me.cmdTestBll.Text = "Test BLL"
            Me.cmdTestBll.UseVisualStyleBackColor = True
            '
            'cmdTestSvc
            '
            Me.cmdTestSvc.Location = New System.Drawing.Point(174, 569)
            Me.cmdTestSvc.Name = "cmdTestSvc"
            Me.cmdTestSvc.Size = New System.Drawing.Size(75, 23)
            Me.cmdTestSvc.TabIndex = 9
            Me.cmdTestSvc.Text = "Test SVC"
            Me.cmdTestSvc.UseVisualStyleBackColor = True
            '
            'Form1
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(400, 600)
            Me.Controls.Add(Me.cmdTestSvc)
            Me.Controls.Add(Me.cmdTestBll)
            Me.Controls.Add(Me.cmdTestDal)
            Me.Controls.Add(Me.dataGridView1)
            Me.Controls.Add(Me.lblOutput)
            Me.Controls.Add(Me.lblStatus)
            Me.Controls.Add(Me.lblConnectionString)
            Me.Name = "Form1"
            Me.Text = "VB Built In"
            CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.bindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        End Sub

        Private Sub cmdTestDal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTestDal.Click
            Dim ds As DataSet
            Dim dr As IDataReader

            lblConnectionString.Text = ""
            lblStatus.Text = ""
            lblOutput.Text = ""
            dataGridView1.DataSource = bindingSource1

            'get connection string
            lblStatus.Text += "A"
            Dim strConnectionString As String = DatabaseUtility.GetConnectionString()
            lblConnectionString.Text += strConnectionString
            Application.DoEvents()

            'open/close connection
            lblStatus.Text += "B"
            Dim connection As SqlConnection = DatabaseUtility.GetConnection()
            connection.Close()
            Application.DoEvents()

            'select master all (datereader)
            lblStatus.Text += "C"
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelectAll]")
            While (dr.Read())
                lblOutput.Text += dr("description") + ","
            End While
            Application.DoEvents()

            'select master all (dataset)
            lblStatus.Text += "D"
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), Nothing, "test_masterSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select master by master key (datereader)
            lblStatus.Text += "E"
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelect]", _
             New SqlParameter("@id", 1) _
            )
            While (dr.Read())
                lblOutput.Text += dr("description") + ","
            End While
            Application.DoEvents()

            'select master by master key (dataset)
            lblStatus.Text += "F"
			ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelect]", _
				New SqlParameter("@id", 1) _
			)
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'insert master
            lblStatus.Text += "G"
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterInsert]", _
             New SqlParameter("@id", NullHandler.HandleAppNull(123, DBNull.Value)), _
             New SqlParameter("@description", NullHandler.HandleAppNull("abc", DBNull.Value)), _
             New SqlParameter("@notes", NullHandler.HandleAppNull(Nothing, DBNull.Value)), _
             New SqlParameter("@someint", NullHandler.HandleAppNull(1, DBNull.Value)), _
             New SqlParameter("@someint_nullable", NullHandler.HandleAppNull(Nothing, DBNull.Value)), _
             New SqlParameter("@somedate", NullHandler.HandleAppNull(DateTime.Parse("12/26/2005 1:01:01 PM"), DBNull.Value)), _
             New SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(Nothing, DBNull.Value)), _
             New SqlParameter("@somefloat", NullHandler.HandleAppNull(1.1, DBNull.Value)), _
             New SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(Nothing, DBNull.Value)), _
             New SqlParameter("@somebool", NullHandler.HandleAppNull(True, DBNull.Value)), _
             New SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(Nothing, DBNull.Value)) _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_masterSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'update master
            lblStatus.Text += "H"
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterUpdate]", _
             New SqlParameter("@id", NullHandler.HandleAppNull(123, DBNull.Value)), _
             New SqlParameter("@description", NullHandler.HandleAppNull("abc", DBNull.Value)), _
             New SqlParameter("@notes", NullHandler.HandleAppNull("Thingamajig", DBNull.Value)), _
             New SqlParameter("@someint", NullHandler.HandleAppNull(1, DBNull.Value)), _
             New SqlParameter("@someint_nullable", NullHandler.HandleAppNull(1, DBNull.Value)), _
             New SqlParameter("@somedate", NullHandler.HandleAppNull(DateTime.Parse("12/26/2005 1:01:01 PM"), DBNull.Value)), _
             New SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(DateTime.Parse("12/26/2005 1:01:01 PM"), DBNull.Value)), _
             New SqlParameter("@somefloat", NullHandler.HandleAppNull(1.1, DBNull.Value)), _
             New SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(1.1, DBNull.Value)), _
             New SqlParameter("@somebool", NullHandler.HandleAppNull(True, DBNull.Value)), _
             New SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(True, DBNull.Value)) _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_masterSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'insert detail
            lblStatus.Text += "I"
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailInsert]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), _
             New SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)), _
             New SqlParameter("@description", NullHandler.HandleAppNull("Desc1A", DBNull.Value)), _
             New SqlParameter("@qty", NullHandler.HandleAppNull(1, DBNull.Value)), _
             New SqlParameter("@amt", NullHandler.HandleAppNull(10, DBNull.Value)) _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select detail all (datereader)
            lblStatus.Text += "J"
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectAll]")
            While (dr.Read())
                lblOutput.Text += dr("description") + ","
            End While
            Application.DoEvents()

            'select detail all (dataset)
            lblStatus.Text += "K"
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select detail by detail key (datereader)
            lblStatus.Text += "L"
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelect]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), _
             New SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)) _
            )
            While (dr.Read())
                lblOutput.Text += dr("description") + ","
            End While
            Application.DoEvents()

            'select detail by detail key (dataset)
            lblStatus.Text += "M"
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelect]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), _
             New SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)) _
            )
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'update detail
            lblStatus.Text += "N"
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailUpdate]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), _
             New SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)), _
             New SqlParameter("@description", NullHandler.HandleAppNull("Desc1A_", DBNull.Value)), _
             New SqlParameter("@qty", NullHandler.HandleAppNull(2, DBNull.Value)), _
             New SqlParameter("@amt", NullHandler.HandleAppNull(20, DBNull.Value)) _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'delete detail
            lblStatus.Text += "K"
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDelete]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), _
             New SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)) _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select detail by master key (datareader)
            lblStatus.Text += "P"
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailInsert]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)), _
             New SqlParameter("@id", NullHandler.HandleAppNull(1, DBNull.Value)), _
             New SqlParameter("@description", NullHandler.HandleAppNull("Desc1A", DBNull.Value)), _
             New SqlParameter("@qty", NullHandler.HandleAppNull(1, DBNull.Value)), _
             New SqlParameter("@amt", NullHandler.HandleAppNull(10, DBNull.Value)) _
            )
            dr = DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectByMaster_id]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)) _
            )
            While (dr.Read())
                lblOutput.Text += dr("description") + ","
            End While
            Application.DoEvents()

            'select detail by master key (dataset)
            lblStatus.Text += "Q"
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectByMaster_id]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)) _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'delete detail by master key
            lblStatus.Text += "R"
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDeleteByMaster_id]", _
             New SqlParameter("@master_id", NullHandler.HandleAppNull(123, DBNull.Value)) _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_detailSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'delete master
            lblStatus.Text += "S"
            DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterDelete]", _
             New SqlParameter("@id", NullHandler.HandleAppNull(123, DBNull.Value)) _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "test_masterSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

        End Sub

        Private Sub cmdTestBll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTestBll.Click
            Dim ds As DataSet
            Dim dr As IDataReader
            Dim masterList As BindingListView(Of Test_masterInfo)
            Dim detailList As BindingListView(Of Test_detailInfo)
            Dim masterInfo As Test_masterInfo
            Dim detailInfo As Test_detailInfo

            lblConnectionString.Text = ""
            lblStatus.Text = ""
            lblOutput.Text = ""
            dataGridView1.DataSource = bindingSource1

            'select master all (datereader)
            lblStatus.Text += "A"
            dr = Test_masterController.SelectDRAll()
            While (dr.Read())
                lblOutput.Text += dr("description") + ","
            End While
            Application.DoEvents()

            'select master all (dataset)
            lblStatus.Text += "B"
            ds = Test_masterController.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select master all (info)
            lblStatus.Text += "C"
            masterList = Test_masterController.SelectInfoAll()
            bindingSource1.DataSource = masterList
            Application.DoEvents()

            'select master by master key (datereader)
            lblStatus.Text += "D"
            dr = Test_masterController.SelectDR( _
             1 _
            )
            While (dr.Read())
                lblOutput.Text += dr("description") + ","
            End While
            Application.DoEvents()

            'select master by master key (dataset)
            lblStatus.Text += "E"
            ds = Test_masterController.SelectDS( _
             1 _
            )
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select master by master key (info)
            lblStatus.Text += "F"
            masterInfo = New Test_masterInfo()
            masterInfo.id = 123
            masterList = Test_masterController.SelectInfo(masterInfo)
            bindingSource1.DataSource = masterList
            Application.DoEvents()

            'insert master
            lblStatus.Text += "G"
            Test_masterController.Insert( _
                123, _
                "abc", _
                Nothing, _
                1, _
                Nothing, _
                DateTime.Parse("12/26/2005 1:01:01 PM"), _
                Nothing, _
                1.1, _
                Nothing, _
                True, _
                Nothing _
            )
            ds = Test_masterController.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'update master
            lblStatus.Text += "H"
            Test_masterController.Update( _
                123, _
                "abc", _
                "Thingamajig", _
                1, _
                1, _
                DateTime.Parse("12/26/2005 1:01:01 PM"), _
                DateTime.Parse("12/26/2005 1:01:01 PM"), _
                1.1, _
                1.1, _
                True, _
                False _
            )
            ds = Test_masterController.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'insert detail (info)
            lblStatus.Text += "I"
            detailInfo = New Test_detailInfo(123, "1", "DescA", 1, 10)
            Test_detailController.InsertInfo(detailInfo)
            detailInfo = Nothing
            ds = Test_detailController.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'update detail (info)
            lblStatus.Text += "J"
            detailInfo = New Test_detailInfo(123, "1", "DescA_", 2, 20)
            Test_detailController.UpdateInfo(detailInfo)
            detailInfo = Nothing
            ds = Test_detailController.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'delete detail (info)
            lblStatus.Text += "K"
            detailInfo = New Test_detailInfo()
            detailInfo.master_id = 123
            detailInfo.id = "1"
            Test_detailController.DeleteInfo(detailInfo)
            detailInfo = Nothing
            ds = Test_detailController.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select detail by master key (datareader)
            lblStatus.Text += "L"
            Test_detailController.Insert( _
             123, _
             "1", _
             "Desc1A", _
             1, _
             10 _
            )
            dr = Test_detailController.SelectDRByMaster_id( _
             123 _
            )
            While (dr.Read())
                lblOutput.Text += dr("description") + ","
            End While
            Application.DoEvents()

            'select detail by master key (dataset)
            lblStatus.Text += "M"
            ds = Test_detailController.SelectDSByMaster_id( _
             123 _
            )
            ds = Test_detailController.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select detail by master key (info)
            lblStatus.Text += "N"
            detailInfo = New Test_detailInfo()
            detailInfo.master_id = 123
            detailInfo.id = "1"
            detailList = Test_detailController.SelectInfoByMaster_id(detailInfo)
            detailInfo = Nothing
            bindingSource1.DataSource = detailList
            Application.DoEvents()

            'delete detail by master key
            lblStatus.Text += "O"
            Test_detailController.DeleteByMaster_id( _
             123 _
            )
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), Nothing, "test_detailSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'detail delete by master key w/ info
            lblStatus.Text += "P"
            Test_detailController.Insert( _
             123, _
             "1", _
             "Desc1A", _
             1, _
             10 _
            )
            detailInfo = New Test_detailInfo()
            detailInfo.master_id = 123
            Test_detailController.DeleteInfoByMaster_id(detailInfo)
            detailInfo = Nothing
            ds = DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), Nothing, "test_detailSelectAll")
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'delete master
            lblStatus.Text += "Q"
            Test_masterController.Delete( _
             123 _
            )
            ds = Test_masterController.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

        End Sub

        Private Sub cmdTestSvc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTestSvc.Click
            Test_masterServiceClientApplication.Test()

            Dim ds As DataSet
            Dim masterList As BindingListView(Of Test_masterService.Test_masterContract)
            Dim detailList As BindingListView(Of Test_detailService.Test_detailContract)
            Dim masterInfo As Test_masterService.Test_masterContract
            Dim detailInfo As Test_detailService.Test_detailContract
            Dim test_masterClient As Test_masterService.Test_masterServiceClient = New Test_masterService.Test_masterServiceClient()
            Dim test_detailClient As Test_detailService.Test_detailServiceClient = New Test_detailService.Test_detailServiceClient()

            test_masterClient.Open()
            test_detailClient.Open()

            lblConnectionString.Text = ""
            lblStatus.Text = ""
            lblOutput.Text = ""
            dataGridView1.DataSource = bindingSource1

            'select master all (dataset)
            lblStatus.Text += "A"
            ds = test_masterClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select master all (info)
            lblStatus.Text += "B"
            masterList = test_masterClient.SelectInfoAll().ToBindingListViewOfContract()
            bindingSource1.DataSource = masterList
            Application.DoEvents()

            'select master by master key (dataset)
            lblStatus.Text += "C"
            ds = test_masterClient.SelectDS( _
             1 _
            )
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select master by master key (info)
            lblStatus.Text += "D"
            masterInfo = New Test_masterContract()
            masterInfo.id = 123
            masterList = test_masterClient.SelectInfo(masterInfo).ToBindingListViewOfContract()
            bindingSource1.DataSource = masterList
            Application.DoEvents()

            'insert master
            lblStatus.Text += "E"
            test_masterClient.Insert( _
                123, _
                "abc", _
                Nothing, _
                1, _
                Nothing, _
                DateTime.Parse("12/26/2005 1:01:01 PM"), _
                Nothing, _
                1.1, _
                Nothing, _
                True, _
                Nothing _
            )
            ds = test_masterClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'update master
            lblStatus.Text += "F"
            test_masterClient.Update( _
                123, _
                "abc", _
                "Thingamajig", _
                1, _
                1, _
                DateTime.Parse("12/26/2005 1:01:01 PM"), _
                DateTime.Parse("12/26/2005 1:01:01 PM"), _
                1.1, _
                1.1, _
                True, _
                False _
            )
            ds = test_masterClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'insert detail (info)
            lblStatus.Text += "G"
            detailInfo = New Test_detailContract()
            detailInfo.master_id = 123
            detailInfo.id = "1"
            detailInfo.description = "DescA"
            detailInfo.qty = 1
            detailInfo.amt = 10
            test_detailClient.InsertInfo(detailInfo)
            detailInfo = Nothing
            ds = test_detailClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'update detail (info)
            lblStatus.Text += "H"
            detailInfo = New Test_detailContract()
            detailInfo.master_id = 123
            detailInfo.id = "1"
            detailInfo.description = "DescA_"
            detailInfo.qty = 2
            detailInfo.amt = 20
            test_detailClient.UpdateInfo(detailInfo)
            detailInfo = Nothing
            ds = test_detailClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'delete detail (info)
            lblStatus.Text += "I"
            detailInfo = New Test_detailContract()
            detailInfo.master_id = 123
            detailInfo.id = "1"
            test_detailClient.DeleteInfo(detailInfo)
            detailInfo = Nothing
            ds = test_detailClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select detail by master key (dataset)
            lblStatus.Text += "J"
            ds = test_detailClient.SelectDSByMaster_id( _
             123 _
            )
            ds = test_detailClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'select detail by master key (info)
            lblStatus.Text += "K"
            detailInfo = New Test_detailContract()
            detailInfo.master_id = 123
            detailInfo.id = "1"
            detailList = test_detailClient.SelectInfoByMaster_id(detailInfo).ToBindingListViewOfContract()
            detailInfo = Nothing
            bindingSource1.DataSource = detailList
            Application.DoEvents()

            'delete detail by master key
            lblStatus.Text += "L"
            test_detailClient.DeleteByMaster_id( _
             123 _
            )
            ds = test_detailClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'detail delete by master key w/ info
            lblStatus.Text += "M"
            test_detailClient.Insert( _
             123, _
             "1", _
             "Desc1A", _
             1, _
             10 _
            )
            detailInfo = New Test_detailContract()
            detailInfo.master_id = 123
            test_detailClient.DeleteInfoByMaster_id(detailInfo)
            detailInfo = Nothing
            ds = test_detailClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            'delete master
            lblStatus.Text += "N"
            test_masterClient.Delete( _
             123 _
            )
            ds = test_masterClient.SelectDSAll()
            bindingSource1.DataSource = ds.Tables(0).DefaultView
            Application.DoEvents()

            If (test_detailClient.State <> System.ServiceModel.CommunicationState.Closed) Then
                test_detailClient.Close()
            End If

            If (test_masterClient.State <> System.ServiceModel.CommunicationState.Closed) Then
                test_masterClient.Close()
            End If

        End Sub
    End Class

End Namespace
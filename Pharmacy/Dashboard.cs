
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Sql;
using System.Collections.Generic;
using Pharmacy.AgentList;
using Pharmacy.Inventory;
using Pharmacy.Product;
using System.IO;
using System.Threading;
using System.Drawing.Printing;

namespace Pharmacy
{
    public partial class dashboard : Form
    {




        // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\kksai\Desktop\Super Shop\Pharmacy\bin\Debug\Database\DB.mdf";Integrated Security=True;Connect Timeout=30
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";
        string imageLoc = "";
        string WholesaleProductID, WholesaleDetailsId, RetailDetailsId;
        DataTable Wdt,Rdt,Productdata;
        int ID;
        UpdateProduct updateProduct = new UpdateProduct();
        AddProduct addpro = new AddProduct();
        AddAgent addAgent = new AddAgent();
        UpdateAgent updateAgent = new UpdateAgent();
        AddInventory inventoryAdd = new AddInventory();
        decimal sum, t, discount, Wquan, Wavailable;
        ReturnGoods GetReturn = new ReturnGoods();
        byte[] image = null;
        ViewInvetory GetViewInvetory = new ViewInvetory();




        public dashboard()
        {
            InitializeComponent();
            this.dashpanel.Visible = true;
            ///////////////////////////////////////////////
            //wholesale
            //////////////////////////////////////////
            SQLCommands sql = new SQLCommands();

            /////////////////////////////
            /////Retail
            ////////////////////////////////
            RproductList.DataSource = sql.FillProductAvl();
            RproductList.Update();
            RproductList.ClearSelection();
            RID.Text = sql.RtopID().ToString();

            ////////////////////////////////////
            ////Debtor
            /////////////////////////////////
            DebtorGrid.DataSource = sql.FillDebtor();
            DebtorGrid.Update();
            DebtorGrid.ClearSelection();

            ///////////////////////////////////////
            //////Profit/Loss
            //////////////////////
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM-dd-yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM-yyyy";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "yyyy";
            dateTimePicker3.ShowUpDown = true;
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker3.Enabled = false;
            dashboarddropdown.SelectedIndex = 0;

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            dashboardbutton.selected = true;
            inventorypanel.Visible = false;
            inventoryviewpanel.Visible = false;
            inventoryaddpanel.Visible = false;
            billretailwholesalepanel.Visible = false;
            RetailChart();
            WholeChart();
            dashboardCharts();
            MonthlyChart();
            YearlyChart();
            WeeklyChart();
            DailyChart();


        }


        /*  private void bunifuImageButton3_Click(object sender, EventArgs e)
          {

              if (inventorypanel.Visible == false)
              {
                  inventorypanel.Visible = true;
                  allpanel.Height = allpanel.Height+inventorypanel.Height;

                  int a =midpanel.Location.Y;
                  int b = midpanel.Location.X;
                  midpanel.Location = new Point(b, a+inventorypanel.Height);
              }
              else
              {
                  inventorypanel.Visible = false;
                  allpanel.Height = allpanel.Height - inventorypanel.Height;

                  int a = midpanel.Location.Y;
                  int b = midpanel.Location.X;
                  midpanel.Location = new Point(b, a -inventorypanel.Height);
              }
          }*/

        private void bunifuImageButton3_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            if (inventorypanel.Visible == false)
            {
                inventorypanel.Visible = true;
                allpanel.Height = allpanel.Height + inventorypanel.Height;

                //int a = productssidepanel.Location.Y;
                //int b = productssidepanel.Location.X;
                //productssidepanel.Location = new Point(b, a + inventorypanel.Height);

                int c = midpanel.Location.Y;
                int d = midpanel.Location.X;
                midpanel.Location = new Point(d, c + inventorypanel.Height);

                //  int f = mid2panel.Location.Y;
                // int g = mid2panel.Location.X;
                // mid2panel.Location = new Point(g, f + billretailwholesalepanel.Height);
            }
            else
            {
                inventorypanel.Visible = false;
                allpanel.Height = allpanel.Height - inventorypanel.Height;

                //int a = productssidepanel.Location.Y;
                //int b = productssidepanel.Location.X;
                //productssidepanel.Location = new Point(b, a - inventorypanel.Height);

                int c = midpanel.Location.Y;
                int d = midpanel.Location.X;
                midpanel.Location = new Point(d, c - inventorypanel.Height);

                // int f = mid2panel.Location.Y;
                //int g = mid2panel.Location.X;
                //mid2panel.Location = new Point(g, f - billretailwholesalepanel.Height);
            }
        }

        private void allpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void home_Click(object sender, EventArgs e)
        {
            this.dashpanel.BringToFront();
            this.dashpanel.Visible = true;
            this.inventoryviewpanel.Visible = false;
            //this.inventoryviewpanel.SendToBack();
            this.inventoryaddpanel.Visible = false;
            //this.inventoryaddpanel.SendToBack();
            this.productseditpanel.Visible = false;
            //this.productspanel.SendToBack();
            dashboarddropdown.SelectedIndex = 0;
            RetailChart();
            WholeChart();
            dashboardCharts();
            MonthlyChart();
            YearlyChart();
            WeeklyChart();
            DailyChart();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            this.agentlistpanel.Visible = true;
            this.agentlistpanel.BringToFront();

            this.billretailpanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.billretailpanel.Visible = true;
            this.debtorpanel.Visible = false;
            this.creditorpanel.Visible = false;
            this.customerspanel.Visible = false;
            this.extracostpanel.Visible = false;
            this.refundpanel.Visible = false;
            this.userspanel.Visible = false;
            this.userzonepanel.Visible = false;
            this.settingspanel.Visible = false;
            this.aboutuspanel.Visible = false;
            fill_agent_datagrifview();
            Wdt = AgentListDataGridView.DataSource as DataTable;
        }

        private void BunifuImageButton3_Click_1(object sender, EventArgs e)
        {

        }


        private void bunifuImageButton3_Click_2(object sender, EventArgs e)
        {

            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                Application.OpenForms[i].WindowState = FormWindowState.Maximized;
            }
        }

        private void inventpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void view_Click(object sender, EventArgs e)
        {

            this.inventoryviewpanel.Visible = true;
            this.inventoryviewpanel.BringToFront();
            this.inventoryaddpanel.Visible = false;
            this.productseditpanel.Visible = false;
            fillStoreDatagridView();
            bunifuCustomDataGrid1.Update();
            bunifuCustomDataGrid1.ClearSelection();
        }

        private void add_Click(object sender, EventArgs e)
        {
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.productseditpanel.Visible = false;
            fill_inventoryDeatils_datagrifview();
            bunifuCustomDataGrid2.Update();
            bunifuCustomDataGrid2.ClearSelection();

        }

        public void bunifuCustomDataGrid2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ID = Convert.ToInt32(bunifuCustomDataGrid2.CurrentRow.Cells[0].Value.ToString());
           

        }

        public void fiilViewInventory()
        {
            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query = "SELECT [ProductName] as 'Product Name',[ProductType] as 'Product Type', [InventoryQuantity] as 'Quantity', [Cost] as 'Cost', [SellingRate] as 'Selling Rate', [BuyingRate] as 'Buying Rate' FROM dbo.InventoryDetails JOIN dbo.Product On dbo.InventoryDetails.ProductId = dbo.Product.ProductId  WHERE [InventoryId]= " + ID + "";

                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                GetViewInvetory.InventoryDetilsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                GetViewInvetory.InventoryDetilsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                GetViewInvetory.InventoryDetilsDataGridView.DataSource = data;
                GetViewInvetory.InventoryDetilsDataGridView.Update();
                GetViewInvetory.InventoryDetilsDataGridView.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        public void fiilReturnInventory()
        {
            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query = "SELECT [InventoryDetailsId] as 'Inventory ID', Product.[ProductId] as 'Product ID', [ProductName] as 'Product Name',[ProductType] as 'Product Type', [InventoryQuantity] as 'Quantity', [Cost] as 'Cost', [SellingRate] as 'Selling Rate', [BuyingRate] as 'Buying Rate' FROM dbo.InventoryDetails JOIN dbo.Product On dbo.InventoryDetails.ProductId = dbo.Product.ProductId  WHERE [InventoryId]= " + ID + "";

                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                GetReturn.InventoryReturnDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                GetReturn.InventoryReturnDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                GetReturn.InventoryReturnDataGridView.DataSource = data;
                GetReturn.InventoryReturnDataGridView.Update();
                GetReturn.InventoryReturnDataGridView.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void productsButton_Click(object sender, EventArgs e)
        {
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.plpanel.Visible = false;
            //////////////////////////////////////////////////////////////////////////

            fill_product_datagrifview();
            productsgridview.Update();
            productsgridview.ClearSelection();
            Productdata = productsgridview.DataSource as DataTable;
        }

        private void productsviewbutton_Click(object sender, EventArgs e)
        {
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
           this.productseditpanel.Visible = false;

        }

        private void productseditbutton_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton6_Click_1(object sender, EventArgs e)
        {
            this.aboutuspanel.Visible = true;
            this.aboutuspanel.BringToFront();

            this.settingspanel.Visible = true;
            this.userzonepanel.Visible = true;
            this.userspanel.Visible = true;
            this.refundpanel.Visible = true;
            this.extracostpanel.Visible = true;
            this.customerspanel.Visible = true;
            this.creditorpanel.Visible = true;
            this.debtorpanel.Visible = true;
            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }



        private void productupdate_Click(object sender, EventArgs e)
        {

        }

        private void productdelete_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = false;
            this.plextracostscards.Visible = false;
        }

        private void profitlossButton4_Click(object sender, EventArgs e)
        {
            this.plpanel.Visible = true;
            this.plpanel.BringToFront();
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = false;
            this.plextracostscards.Visible = false;
            this.billretailpanel.Visible = false;
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            this.plwholesalebillscards.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plextracostscards.Visible = false;
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            this.plextracostscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plretailsbillscards.Visible = true;

        }

        private void billButton5_Click(object sender, EventArgs e)
        {
            if (billretailwholesalepanel.Visible == false)
            {
                billretailwholesalepanel.Visible = true;
                billsidepanel.Height = billsidepanel.Height + billretailwholesalepanel.Height;

                int f = mid2panel.Location.Y;
                int g = mid2panel.Location.X;
                mid2panel.Location = new Point(g, f + billretailwholesalepanel.Height);
            }
            else
            {
                billretailwholesalepanel.Visible = false;
                billsidepanel.Height = billsidepanel.Height - billretailwholesalepanel.Height;

                int f = mid2panel.Location.Y;
                int g = mid2panel.Location.X;
                mid2panel.Location = new Point(g, f - billretailwholesalepanel.Height);
            }
        }

        private void billretailbutton_Click(object sender, EventArgs e)
        {
            this.billretailpanel.Visible = true;
            this.billretailpanel.BringToFront();
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = false;
            this.billwholesalepanel.Visible = false;

            SQLCommands sql = new SQLCommands();
            RproductList.DataSource = sql.FillProductAvl();
            Wdt = RproductList.DataSource as DataTable;
            RproductList.Update();
            RproductList.ClearSelection();
            RID.Text = sql.RtopID().ToString();
        }

        private void billwholesalebutton_Click(object sender, EventArgs e)
        {
            this.billwholesalepanel.Visible = true;
            this.billwholesalepanel.BringToFront();
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = false;
            this.billretailpanel.Visible = true;

            SQLCommands sql = new SQLCommands();
            wholesaleinstockgrid.DataSource = sql.FillProductAvl();
            wholesaleinstockgrid.Update();
            wholesaleinstockgrid.ClearSelection();
            WID.Text = sql.WtopID().ToString();
            Wdt = wholesaleinstockgrid.DataSource as DataTable;
            DataTable dt = sql.fillWcustomerDD();
            WcustomerDD.DisplayMember = "CustomerName";
            WcustomerDD.ValueMember = "CustomerId";
            WcustomerDD.DataSource = dt;
            WcustomerDD.Text = "";

        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {

        }

        private void debtorButton7_Click(object sender, EventArgs e)
        {
            this.debtorpanel.Visible = true;
            this.debtorpanel.BringToFront();

            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;
            this.creditorpanel.Visible = false;
            this.customerspanel.Visible = false;
            this.extracostpanel.Visible = false;
            this.refundpanel.Visible = false;
            this.userspanel.Visible = false;
            this.userzonepanel.Visible = false;
            this.settingspanel.Visible = false;
            this.aboutuspanel.Visible = false;
            SQLCommands sql = new SQLCommands();
            DebtorGrid.DataSource = sql.FillDebtor();
            Wdt = DebtorGrid.DataSource as DataTable;
            DebtorGrid.Update();
            DebtorGrid.ClearSelection();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.creditorpanel.Visible = true;
            this.creditorpanel.BringToFront();

            this.debtorpanel.Visible = true;

            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;
            this.customerspanel.Visible = false;
            this.extracostpanel.Visible = false;
            this.refundpanel.Visible = false;
            this.userspanel.Visible = false;
            this.userzonepanel.Visible = false;
            this.settingspanel.Visible = false;
            this.aboutuspanel.Visible = false;
            fill_Creditor_Load();
            Wdt = bunifuCustomDataGrid3.DataSource as DataTable;
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            this.customerspanel.Visible = true;
            this.customerspanel.BringToFront();

            this.creditorpanel.Visible = true;
            this.debtorpanel.Visible = true;
            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;
            this.extracostpanel.Visible = false;
            this.refundpanel.Visible = false;
            this.userspanel.Visible = false;
            this.userzonepanel.Visible = false;
            this.settingspanel.Visible = false;
            this.aboutuspanel.Visible = false;

            SQLCommands sql = new SQLCommands();
            customersgrid.DataSource = sql.FillCustomers();

            customersgrid.Update();
            customersgrid.ClearSelection();

            CustomerId.Clear();
            CustomerName.Clear();
            CustomerCompany.Clear();
            CustomerPhn.Clear();
            Wdt = customersgrid.DataSource as DataTable;
        }

        private void mini_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            this.extracostpanel.Visible = true;
            this.extracostpanel.BringToFront();

            this.customerspanel.Visible = true;
            this.creditorpanel.Visible = true;
            this.debtorpanel.Visible = true;
            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;
            this.refundpanel.Visible = false;
            this.userspanel.Visible = false;
            this.userzonepanel.Visible = false;
            this.settingspanel.Visible = false;
            this.aboutuspanel.Visible = false;


            SQLCommands sql = new SQLCommands();
            extracostgrid.DataSource = sql.FillCost();
            Wdt = extracostgrid.DataSource as DataTable;
            extracostgrid.Update();
            extracostgrid.ClearSelection();

            CostId.Clear();
            CostDesc.Clear();
            CostAmount.Clear();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            this.userspanel.Visible = true;
            this.userspanel.BringToFront();

            this.refundpanel.Visible = true;
            this.extracostpanel.Visible = true;
            this.customerspanel.Visible = true;
            this.creditorpanel.Visible = true;
            this.debtorpanel.Visible = true;
            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;
            this.userzonepanel.Visible = false;
            this.settingspanel.Visible = false;
            this.aboutuspanel.Visible = false;

            SQLCommands sql = new SQLCommands();
            usersgrid.DataSource = sql.FillUser();
            usersgrid.Update();
            usersgrid.ClearSelection();
            Wdt = usersgrid.DataSource as DataTable;
            UserName.Clear();
            UserPass.Clear();
            UserID.Clear();
            UserType.selectedIndex = 0;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.aboutuspanel.Visible = false;
            this.userspanel.Visible = false;
            this.extracostpanel.Visible = false;
            this.customerspanel.Visible = false;
            this.creditorpanel.Visible = false;
            this.debtorpanel.Visible = false;
            this.billwholesalepanel.Visible = false;
            this.plpanel.Visible = false;
            this.productseditpanel.Visible = false;
            this.inventoryviewpanel.Visible = false;
            this.inventoryaddpanel.Visible = false;
            this.plretailsbillscards.Visible = false;
            this.plwholesalebillscards.Visible = false;
            this.plextracostscards.Visible = false;
            this.agentlistpanel.Visible = false;
            this.billretailpanel.Visible = false;
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        private void productadd_Click(object sender, EventArgs e)
        {

            addpro.ShowDialog();
            fill_product_datagrifview();
            productsgridview.Update();
            productsgridview.ClearSelection();

        }

        public void fill_product_datagrifview()
        {


            try
            {

                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);

                String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' , SKU as 'Barcode', Unit as 'Unit' , MinimumQuantity as  'Minimum Quantity' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                productsgridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                productsgridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                productsgridview.DataSource = data;
                productsgridview.Update();
                productsgridview.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Please Connect To The database");
            }

        }

        private void productupdate_Click_1(object sender, EventArgs e)
        {
            if (updateProduct.productName.Text.Length > 0)
            {
                updateProduct.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please Select A row First");
            }
            fill_product_datagrifview();
            productsgridview.Update();
            productsgridview.ClearSelection();
        }

        private void ProductRefreshButton_Click(object sender, EventArgs e)
        {
            fill_product_datagrifview();
            productsgridview.Update();
            productsgridview.ClearSelection();
        }

        public void productsgridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            updateProduct.productId.Text = productsgridview.CurrentRow.Cells[0].Value.ToString();
            updateProduct.productName.Text = productsgridview.CurrentRow.Cells[1].Value.ToString();
            updateProduct.productType.Text = productsgridview.CurrentRow.Cells[2].Value.ToString();
            updateProduct.buyingRate.Text = productsgridview.CurrentRow.Cells[3].Value.ToString();
            updateProduct.saleRate.Text = productsgridview.CurrentRow.Cells[4].Value.ToString();
            updateProduct.Unit.Text = productsgridview.CurrentRow.Cells[6].Value.ToString();
            updateProduct.MinimumQuantity.Text = productsgridview.CurrentRow.Cells[7].Value.ToString();

        }

        private void productdelete_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in productsgridview.Rows)


            {

                if (row.Selected)
                {
                    try
                    {



                        String query = "DELETE FROM [dbo].[Product] WHERE [ProductId]= " + row.Cells["Product ID"].Value.ToString() + ";";


                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();


                                int k = cmd.ExecuteNonQuery();

                                sqlCon.Close();
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                }

            }

            MessageBox.Show("DELETe sucessfully");
            fill_product_datagrifview();
            productsgridview.Update();
            productsgridview.ClearSelection();



        }

        /// <agent>
        /// ////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void agentAddButtonClick(object sender, EventArgs e)
        {
            addAgent.ShowDialog();
            fill_agent_datagrifview();
            AgentListDataGridView.Update();
            AgentListDataGridView.ClearSelection();



        }

        private void agentUpdateButtonClick(object sender, EventArgs e)
        {
            if (updateAgent.addAgentName.Text.Length > 0)
            {
                updateAgent.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please Select A row First");
            }

            fill_agent_datagrifview();
            AgentListDataGridView.Update();
            AgentListDataGridView.ClearSelection();
        }

        private void agentDeleteButtonClick(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in AgentListDataGridView.Rows)


            {

                if (row.Selected)
                {
                    try
                    {



                        String query = "DELETE FROM [dbo].[Agent] WHERE [AgentId]= " + row.Cells["Agent ID"].Value.ToString() + ";";


                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();


                                int k = cmd.ExecuteNonQuery();

                                sqlCon.Close();
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                }

            }

            MessageBox.Show("DELETe sucessfully");
            fill_agent_datagrifview();
            AgentListDataGridView.Update();
            AgentListDataGridView.ClearSelection();


        }

        private void agentRefreshButtonClick(object sender, EventArgs e)
        {
            fill_agent_datagrifview();
            AgentListDataGridView.Update();
            AgentListDataGridView.ClearSelection();

        }



        public void fill_agent_datagrifview()
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);

                String query = "SELECT [AgentId] AS 'Agent ID', [AgentName] AS 'Agent Name', [CompanyName] AS 'Company Name', [PhoneNumber] AS 'Phone Number' FROM dbo.Agent; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                AgentListDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                AgentListDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                AgentListDataGridView.DataSource = data;
                AgentListDataGridView.Update();
                AgentListDataGridView.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Please Connect To The database");
            }

        }
        public void fill_Ledger()
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);

                String query = "SELECT [LedgerId] AS 'Ledger ID',[CustomerName] AS 'Customer Name', [Description] AS 'Description', [Debit] AS 'Debit', [Credit] AS 'Credit', [Balance] AS 'Balance' FROM dbo.Ledger JOIN  dbo.Customer On dbo.Ledger.CustomerId =dbo.Customer.CustomerId  ;";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                bunifuCustomDataGrid4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                bunifuCustomDataGrid4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bunifuCustomDataGrid4.DataSource = data;
                bunifuCustomDataGrid4.Update();
                bunifuCustomDataGrid4.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Please Connect To The database");
            }

        }

        private void AgentListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            updateAgent.addAgentId.Text = AgentListDataGridView.CurrentRow.Cells[0].Value.ToString();
            updateAgent.addAgentName.Text = AgentListDataGridView.CurrentRow.Cells[1].Value.ToString();
            updateAgent.addCompanyName.Text = AgentListDataGridView.CurrentRow.Cells[2].Value.ToString();
            updateAgent.addPhoneNumber.Text = AgentListDataGridView.CurrentRow.Cells[3].Value.ToString();

        }
        ////////////////////////////////////////////////////////////////////////////////




        private void inventoryaddbutton_Click_1(object sender, EventArgs e)
        {
            inventoryAdd.ShowDialog();
            fill_inventoryDeatils_datagrifview();
            bunifuCustomDataGrid2.Update();
            bunifuCustomDataGrid2.ClearSelection();

        }

        private void inventoryupdatebutton_Click_1(object sender, EventArgs e)
        {

        }

        private void inventoryprint_Click(object sender, EventArgs e)
        {

        }

        public void fill_inventoryDeatils_datagrifview()
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query = @"SELECT [InventoryId] as 'Inventory ID', [AgentName] as 'Agent Name', [TotalBill] as 'Total Bill', [CashPaid] as 'Cash Paid', [Due] as 'Due', [Date] as 'Date',[CompanyName] as 'Company Name',  [PhoneNumber] as 'Phone Number' FROM  dbo.Inventory JOIN dbo.Agent On dbo.Inventory.AgentId = dbo.Agent.AgentId ;";
                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                bunifuCustomDataGrid2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                bunifuCustomDataGrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bunifuCustomDataGrid2.DataSource = data;
                bunifuCustomDataGrid2.Update();
                bunifuCustomDataGrid2.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }





        private void bunifuThinButton232_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();

            string name = UserName.Text;
            string pass = UserPass.Text;
            string type = UserType.selectedValue.ToString();
            if (sql.InsertUser(name, type, pass) == true)
            {
                MessageBox.Show("Success");
            }
            else
                MessageBox.Show("Failed");
            UserName.Clear();
            UserPass.Clear();
            UserType.selectedIndex = 0;

            usersgrid.DataSource = sql.FillUser();
            Wdt = usersgrid.DataSource as DataTable;
            usersgrid.Update();
            usersgrid.ClearSelection();
            //usersgrid.Refresh();

        }

        private void usersgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            usersgrid.DataSource = sql.FillUser();
        }

        private void usersgrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.usersgrid.Rows[e.RowIndex];

                UserID.Text = row.Cells["UserId"].Value.ToString();
                UserName.Text = row.Cells["Username"].Value.ToString();
                if (row.Cells["UserType"].Value.ToString()[0] == 'S')
                {
                    UserType.selectedIndex = 1;
                    // MessageBox.Show("1");
                }
                else if (row.Cells["UserType"].Value.ToString()[0] == 'A')
                {
                    UserType.selectedIndex = 2;
                }
                else
                {
                    UserType.selectedIndex = 3;
                    //MessageBox.Show(UserType.Items[1] + " " + row.Cells["UserType"].Value.ToString()[0])  ;
                }
                UserPass.Text = row.Cells["PassWord"].Value.ToString();
            }
        }

        private void bunifuThinButton231_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (sql.UpdateUser(UserID.Text, UserName.Text, UserType.selectedValue.ToString(), UserPass.Text))
            {
                MessageBox.Show("Success");
            }
            else
                MessageBox.Show("Failed");

            usersgrid.DataSource = sql.FillUser();
            usersgrid.Update();
            usersgrid.ClearSelection();

            UserID.Clear();
            UserName.Clear();
            UserPass.Clear();
            UserType.selectedIndex = 0;
        }

        private void bunifuThinButton230_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            foreach (DataGridViewRow row in usersgrid.Rows)
            {
                if (row.Selected)
                {
                    if (sql.DeleteUser(row.Cells["UserId"].Value.ToString()))
                    {

                    }
                    else
                        MessageBox.Show("Failed");
                }
            }

            usersgrid.DataSource = sql.FillUser();
            usersgrid.Update();
            usersgrid.ClearSelection();

            UserID.Clear();
            UserName.Clear();
            UserPass.Clear();
            UserType.selectedIndex = 0;
        }

        //Delete CompanyAgents

        private void CustomerAdd_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (sql.InsertCustomer(CustomerName.Text, CustomerCompany.Text, CustomerPhn.Text))
            {
                MessageBox.Show("Success");
            }
            else
                MessageBox.Show("Failed");

            customersgrid.DataSource = sql.FillCustomers();

            customersgrid.Update();
            customersgrid.ClearSelection();

            CustomerId.Clear();
            CustomerName.Clear();
            CustomerCompany.Clear();
            CustomerPhn.Clear();
        }

        private void CustomerDelete_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            foreach (DataGridViewRow row in customersgrid.Rows)
            {
                if (row.Selected)
                {
                    if (sql.DeleteCustomer(row.Cells["CustomerId"].Value.ToString()))
                    {

                    }
                    else
                        MessageBox.Show("Failed");
                }
            }

            customersgrid.DataSource = sql.FillCustomers();

            customersgrid.Update();
            customersgrid.ClearSelection();

            CustomerId.Clear();
            CustomerName.Clear();
            CustomerCompany.Clear();
            CustomerPhn.Clear();
        }

        private void customersgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.customersgrid.Rows[e.RowIndex];

                CustomerId.Text = row.Cells["CustomerId"].Value.ToString();
                CustomerName.Text = row.Cells["CustomerName"].Value.ToString();
                CustomerCompany.Text = row.Cells["CompanyName"].Value.ToString();
                CustomerPhn.Text = row.Cells["PhoneNumber"].Value.ToString();

            }
        }

        private void CustomerUpdate_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (sql.UpdateCustomer(CustomerId.Text, CustomerName.Text, CustomerCompany.Text, CustomerPhn.Text))
            {
                MessageBox.Show("Success");
            }
            else
                MessageBox.Show("Failed");

            customersgrid.DataSource = sql.FillCustomers();

            customersgrid.Update();
            customersgrid.ClearSelection();

            CustomerId.Clear();
            CustomerName.Clear();
            CustomerCompany.Clear();
            CustomerPhn.Clear();
        }

        private void ExtracostAdd_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            // if (sql.InsertCost(CostDesc.Text, Convert.ToDecimal(CostAmount.Text)))
            // {
            //      MessageBox.Show("Success");
            //  }
            // else
            MessageBox.Show(sql.InsertCost(CostDesc.Text, Convert.ToDecimal(CostAmount.Text)));


            extracostgrid.DataSource = sql.FillCost();
            Wdt = extracostgrid.DataSource as DataTable;
            extracostgrid.Update();
            extracostgrid.ClearSelection();

            CostId.Clear();
            CostDesc.Clear();
            CostAmount.Clear();
        }

        private void ExtraCostDelete_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            foreach (DataGridViewRow row in extracostgrid.Rows)
            {
                if (row.Selected)
                {
                    if (sql.DeleteCost(row.Cells["CostId"].Value.ToString()))
                    {

                    }
                    else
                        MessageBox.Show("Failed");
                }
            }

            extracostgrid.DataSource = sql.FillCost();

            extracostgrid.Update();
            extracostgrid.ClearSelection();

            CostId.Clear();
            CostDesc.Clear();
            CostAmount.Clear();
        }

        private void extracostgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.extracostgrid.Rows[e.RowIndex];

                CostId.Text = row.Cells["CostId"].Value.ToString();
                CostDesc.Text = row.Cells["CostType"].Value.ToString();
                CostAmount.Text = row.Cells["Amount"].Value.ToString();
            }
        }

        private void ExtraCostUpdate_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (sql.UpdateCost(CostId.Text, CostDesc.Text, Convert.ToDecimal(CostAmount.Text)))
            {
                MessageBox.Show("Success");
            }
            else
                MessageBox.Show("Failed");

            extracostgrid.DataSource = sql.FillCost();

            extracostgrid.Update();
            extracostgrid.ClearSelection();

            CostId.Clear();
            CostDesc.Clear();
            CostAmount.Clear();
        }


        private void wholesalebillinfocards_Paint(object sender, PaintEventArgs e)
        {

        }

        private void midpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void wholesaleinstockgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.wholesaleinstockgrid.Rows[e.RowIndex];

                WProductName.Text = row.Cells["Product Name"].Value.ToString();
                WholesaleProductID = row.Cells["Product ID"].Value.ToString();
                //MessageBox.Show(WholesaleProductID);
                Wavailable = Convert.ToDecimal(row.Cells["Quantity"].Value);



            }
        }

        private void Wsearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Wdt);
            dv.RowFilter = string.Format(@"[Product Name] LIKE '%{0}%'", Wsearch.Text);
            wholesaleinstockgrid.DataSource = dv;

        }

        private void Wquantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Wquantity.Text != "")
                {
                    DataView dv = new DataView(Wdt);
                    dv.RowFilter = string.Format("[Product ID] =" + Convert.ToInt32(WholesaleProductID) + "");
                    decimal a = Convert.ToDecimal(dv[0]["Price"]);
                    Wquan = Convert.ToDecimal(Wquantity.Text);
                    if (Wquan > Wavailable)
                    {
                        MessageBox.Show("Not Enough Products");
                        Wquantity.Clear();
                        WtotalLabel.Text = "0.0";
                    }

                    WtotalLabel.Text = (Wquan * a).ToString();
                }
                else
                    WtotalLabel.Text = "0.0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Only Numbers");
                Wquantity.Clear();
            }
        }

        private void billretailpanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void bunifuThinButton233_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (WProductName.Text != "" && WProductName.Text != null && Wquantity.Text != "" && Wquantity.Text != null)
            {
                DataView dv = new DataView(Wdt);
                dv.RowFilter = string.Format("[Product ID] =" + Convert.ToInt32(WholesaleProductID) + "");
                decimal a = Convert.ToDecimal(dv[0]["Price"]);

                if (sql.InsertWholesale(WID.Text, WholesaleProductID, Wquantity.Text, a.ToString(), WtotalLabel.Text))
                {
                    //  MessageBox.Show("");
                }
                else
                {
                    MessageBox.Show("failed");
                }
                WholesaleAdd.DataSource = sql.Fillwholesalel(sql.WtopID());
                WholesaleAdd.Columns[5].Visible = false;
                // MessageBox.Show(sql.WtopID());
                WholesaleAdd.Update();

                WProductName.Clear();
                Wquantity.Clear();
                WtotalLabel.Text = "0.0";

                sum = 0;
                for (int i = 0; i < WholesaleAdd.Rows.Count; ++i)
                {
                    sum += Convert.ToDecimal(WholesaleAdd.Rows[i].Cells[4].Value);
                }

                Wtotal.Text = sum.ToString();


                // MessageBox.Show(sql.WupdateProduct((decimal)Wquan, WholesaleProductID));
                sql.WupdateProduct((decimal)Wquan, WholesaleProductID);

                wholesaleinstockgrid.DataSource = sql.FillProductAvl();
                wholesaleinstockgrid.Update();
                wholesaleinstockgrid.ClearSelection();
                SkuTextBox.Clear();
            }
            else
                MessageBox.Show("Select Product/ Enter Quantity!");
        }

        private void bunifuThinButton234_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            string s, id;
            id = WID.Text;
            if (Wpaid.Text != "" && Wpaid.Text != null && WcustomerDD.Text != "")
            {
                if (Wdiscount.Text == "")
                {
                    s = "0.0";
                }
                else
                    s = Wdiscount.Text;
                //if (sql.InsertWholesaleMain(WcustomerDD.SelectedValue.ToString(), s, Wtotal.Text, Wpaid.Text, Wdue.Text))
                //{
                //    MessageBox.Show("Done");
                //}
                //else
                //{
                //    MessageBox.Show("Failed");
                //}
                MessageBox.Show(sql.InsertWholesaleMain(Convert.ToInt32(WcustomerDD.SelectedValue), Convert.ToDecimal(s), Convert.ToDecimal(Wtotal.Text), Convert.ToDecimal(Wpaid.Text), Convert.ToDecimal(Wdue.Text)));

                if (Convert.ToDecimal(Wdue.Text) > 0)
                    MessageBox.Show(sql.InsertDebtor(Convert.ToInt32(WcustomerDD.SelectedValue), Convert.ToDecimal(Wdue.Text)));
                AddToLedger();

                string a = "Talukdar halldfkgdfkmbdklfmbkldfmbldf";
                PrintReciept print = new PrintReciept();
                print.ExportDataTableToPdf(@"D:\test.pdf", a, WcustomerDD.SelectedValue.ToString(), s, Wtotal.Text, id, Wpaid.Text, Wdue.Text);
                System.Diagnostics.Process.Start(@"D:\test.pdf");
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;

                WProductName.Clear();
                Wquantity.Clear();
                WtotalLabel.Text = "0.0";

                Wdiscount.Clear();
                Wtotal.Clear();
                Wdue.Text = "0.0";
                Wpaid.Clear();
                WcustomerDD.Text = "";

                //  MessageBox.Show(sql.WtopID());
                WID.Text = sql.WtopID();



                DebtorGrid.DataSource = sql.FillDebtor();
                DebtorGrid.Update();
                DebtorGrid.ClearSelection();

                WholesaleAdd.DataSource = null;
                WholesaleAdd.Update();
            }
            else
                MessageBox.Show("Enter Payment/ Select Customer Name!");

        }

        private void inventoryRefreshButton_Click(object sender, EventArgs e)
        {

        }

        private void inventoryRefreshButton_Click_1(object sender, EventArgs e)
        {
            fill_inventoryDeatils_datagrifview();
            bunifuCustomDataGrid2.Update();
            bunifuCustomDataGrid2.ClearSelection();
        }

        private void bunifuThinButton28_Click_1(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                fiilViewInventory();
                GetViewInvetory.ShowDialog();

            }
            else
            {
                MessageBox.Show("Please Select A row First");
            }


        }

        private void Wpaid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Wpaid.Text != "")
                {
                    t = Convert.ToDecimal(Wtotal.Text) - Convert.ToDecimal(Wpaid.Text);
                    if (t >= 0)
                    {
                        Wdue.Text = t.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Can not pay more!!");
                        Wpaid.Clear();
                        Wdue.Text = "0.00";
                    }
                }

                else
                    Wdue.Text = "0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Only Numbers");
                Wpaid.Clear();

            }


        }

        private void Wdiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Wdiscount.Text != "")
                {
                    discount = sum - (sum * (Convert.ToDecimal(Wdiscount.Text) / (decimal)100));
                    Wtotal.Text = discount.ToString("0.00");
                }
                else
                {
                    Wtotal.Text = sum.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Only Numbers");
                Wtotal.Text = sum.ToString();
            }

        }


        /// <summary>
        /// Retail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void RproductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.RproductList.Rows[e.RowIndex];

                RproductName.Text = row.Cells["Product Name"].Value.ToString();
                WholesaleProductID = row.Cells["Product ID"].Value.ToString();
                //MessageBox.Show(WholesaleProductID);
                Wavailable = Convert.ToDecimal(row.Cells["Quantity"].Value);

            }
        }

        private void Rquantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Rquantity.Text != "")
                {
                    DataView dv = new DataView(Wdt);
                    dv.RowFilter = string.Format("[Product ID] =" + Convert.ToInt32(WholesaleProductID) + "");
                    MessageBox.Show(dv[0]["Price"].ToString());
                    decimal a = Convert.ToDecimal(dv[0]["Price"]);
                    Wquan = Convert.ToDecimal(Rquantity.Text);
                    if (Wquan > Wavailable)
                    {
                        MessageBox.Show("Not Enough Products");
                        Rquantity.Text = "0";
                        Rtotal.Text = "0.0";
                    }

                    Rtotal.Text = (Wquan * a).ToString();
                }
                else
                    Rtotal.Text = "0.0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Rquantity.Clear();
                Rtotal.Text = "0.0";
            }
        }


        private void RetailAdd_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (RproductName.Text != "" && RproductName.Text != null)
            {
                if (Rquantity.Text != "" && Rquantity.Text != null)
                {
                    if (sql.InsertRetail(RID.Text, WholesaleProductID, Rquantity.Text, Rtotal.Text))
                    {
                        //MessageBox.Show("");
                    }
                    else
                    {
                        MessageBox.Show("failed");
                    }
                    RproductAdd.DataSource = sql.FillRetail(sql.RtopID());
                    RproductAdd.Columns[5].Visible = false;
                    // MessageBox.Show(sql.RtopID());
                    RproductAdd.Update();
                    RproductAdd.ClearSelection();

                    RproductName.Clear();
                    Rquantity.Clear();
                    Rtotal.Text = "0.0";

                    sum = 0;
                    for (int i = 0; i < RproductAdd.Rows.Count; ++i)
                    {
                        sum += Convert.ToDecimal(RproductAdd.Rows[i].Cells[4].Value);
                    }

                    RtotalBill.Text = sum.ToString();


                    //MessageBox.Show(sql.WupdateProduct((decimal)Wquan, WholesaleProductID));
                    sql.WupdateProduct((decimal)Wquan, WholesaleProductID);
                    RproductList.DataSource = sql.FillProductAvl();
                    RproductList.Update();
                    Rdt = sql.FillRetail(sql.RtopID());
                    RproductList.ClearSelection();
                    RetailSkuText.Clear();
                }
                else
                    MessageBox.Show("Enter Quantity!");
            }
            else
                MessageBox.Show("Select Name!!");
        }

        public void fillStoreDatagridView()
        {
            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query = "SELECT [ProductName] as 'Product Name', [ProductType] as 'Product Type' ,[Quantity] as 'Available Quantity' FROM dbo.Product ;";

                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                bunifuCustomDataGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                bunifuCustomDataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bunifuCustomDataGrid1.DataSource = data;
                bunifuCustomDataGrid1.Update();
                bunifuCustomDataGrid1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void Rdiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Rdiscount.Text != "")
                {
                    discount = sum - (sum * (Convert.ToDecimal(Rdiscount.Text) / (decimal)100));
                    RtotalBill.Text = discount.ToString();
                }
                else
                {
                    RtotalBill.Text = sum.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Only Numbers");
                Rdiscount.Clear();
                RtotalBill.Text = sum.ToString();
            }
        }

        private void CreditorDataGridViewClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuCustomLabel44.Text = bunifuCustomDataGrid3.CurrentRow.Cells[0].Value.ToString();
            bunifuCustomLabel49.Text = bunifuCustomDataGrid3.CurrentRow.Cells[2].Value.ToString();
        }

        private void CreditorPaidButton(object sender, EventArgs e)
        {



            if (Convert.ToDecimal(bunifuCustomLabel49.Text) > Convert.ToDecimal(CreditorCashPaid.Text))
            {

                try
                {



                    String query = "UPDATE [dbo].[Creditor] SET [CreditorDue] = @due   WHERE [CreditorId] =  " + Convert.ToInt32(bunifuCustomLabel44.Text) + "; ";


                    using (SqlConnection sqlCon = new SqlConnection(conString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                        {
                            sqlCon.Open();
                            Decimal restDue = Convert.ToDecimal(bunifuCustomLabel49.Text) - Convert.ToDecimal(CreditorCashPaid.Text);
                            cmd.Parameters.AddWithValue("@due", restDue);


                            int k = cmd.ExecuteNonQuery();
                            if (k > 0)
                            {
                                MessageBox.Show("Update sucessfully");

                            }
                            else
                            {
                                MessageBox.Show("Not working");
                            }
                            sqlCon.Close();
                            fill_Creditor_Load();
                            bunifuCustomDataGrid3.Update();
                            bunifuCustomDataGrid3.ClearSelection();

                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }


            else if (Convert.ToDecimal(bunifuCustomLabel49.Text) == Convert.ToDecimal(CreditorCashPaid.Text))
            {

                try
                {

                    String query = "DELETE FROM [dbo].[Creditor] WHERE [CreditorId]= " + Convert.ToInt32(bunifuCustomLabel44.Text) + ";";

                    //String query = "UPDATE [dbo].[Creditor] SET [CreditorDue] = @due   WHERE [CreditorId] =  " + Convert.ToInt32(bunifuCustomLabel49.Text) + "; ";


                    using (SqlConnection sqlCon = new SqlConnection(conString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                        {
                            sqlCon.Open();
                            Decimal restDue = Convert.ToDecimal(bunifuCustomLabel49.Text) - Convert.ToDecimal(CreditorCashPaid.Text);
                            cmd.Parameters.AddWithValue("@due", restDue);


                            int k = cmd.ExecuteNonQuery();
                            if (k > 0)
                            {
                                MessageBox.Show("Update sucessfully");

                            }
                            else
                            {
                                MessageBox.Show("Not working");
                            }
                            sqlCon.Close();
                            fill_Creditor_Load();
                            bunifuCustomDataGrid3.Update();
                            bunifuCustomDataGrid3.ClearSelection();

                        }
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }


            else
            {
                MessageBox.Show("You Cant Paid More Then Due");
                CreditorCashPaid.Clear();

            }
            CreditorCashPaid.Clear();
            bunifuCustomLabel44.Text = "";
            bunifuCustomLabel49.Text = "";

        }

        private void bunifuCustomLabel_Click(object sender, EventArgs e)
        {

        }

        private void Rpaid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Rpaid.Text != "")
                {
                    t = Convert.ToDecimal(Rpaid.Text) - Convert.ToDecimal(RtotalBill.Text);                 
                    Rdue.Text = t.ToString();
             
                }

                else
                    Rdue.Text = Rtotal.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Only Numbers");
                Rpaid.Clear();
                Rdue.Text = Rtotal.Text;

            }

        }

        private void RetailPurchase_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (Rpaid.Text != "" && Rpaid.Text != null)
            {
                string s;
                if (Rdiscount.Text == "")
                {
                    s = "0.0";
                }
                else
                    s = Rdiscount.Text;
                //if (sql.InsertWholesaleMain(WcustomerDD.SelectedValue.ToString(), s, Wtotal.Text, Wpaid.Text, Wdue.Text))
                //{
                //    MessageBox.Show("Done");
                //}
                //else
                //{
                //    MessageBox.Show("Failed");
                //}
                if (Convert.ToDecimal(Rdue.Text) >= 0)
                {
                    //MessageBox.Show(sql.InsertRetailMain(Convert.ToDecimal(s), Convert.ToDecimal(RtotalBill.Text), Convert.ToDecimal(Rpaid.Text), Convert.ToDecimal(Rdue.Text)));
                    sql.InsertRetailMain(Convert.ToDecimal(s), Convert.ToDecimal(RtotalBill.Text), Convert.ToDecimal(Rpaid.Text), Convert.ToDecimal(Rdue.Text));
                    RproductName.Clear();
                    Rquantity.Clear();
                    print();
                    Rtotal.Text = "0.0";

                    Rdiscount.Clear();
                    RtotalBill.Clear();
                    Rdue.Text = "0.0";
                    Rpaid.Clear();
                    //WcustomerDD.Text = "";

                    //MessageBox.Show(sql.RtopID());
                    RID.Text = sql.RtopID();
                    RproductAdd.DataSource = null;
                    RproductAdd.Update();
                }
                else
                {
                    MessageBox.Show("Pay Full Amount");
                }
            }
            else
                MessageBox.Show("Enter Payment!");
        }


        private void RetailSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Wdt);
            dv.RowFilter = string.Format(@"[Product Name] LIKE '%{0}%'", RetailSearch.Text);
            RproductList.DataSource = dv;
        }


        ////////////////////////////////
        /////Debtor
        /////////////////
        private void DebtorGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DebtorGrid.Rows[e.RowIndex];

                DebtorID.Text = row.Cells["Debtor ID"].Value.ToString();
                DebtorDue.Text = row.Cells["Due"].Value.ToString();
                sum = Convert.ToDecimal(DebtorDue.Text);
            }
        }


        private void DebtorPaid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DebtorPaid.Text != "")
                {

                    t = sum - Convert.ToDecimal(DebtorPaid.Text);
                    if (t >= 0)
                    {
                        DebtorDue.Text = t.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Can not pay more!!");
                        DebtorPaid.Clear();

                    }
                }

                else
                    DebtorDue.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Only Numbers");
                Rpaid.Clear();

            }
        }


        private void DebtorUpdate_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (Convert.ToDecimal(DebtorDue.Text) == 0)
            {
                if (sql.DeleteDebtor(DebtorID.Text))
                {
                    MessageBox.Show("Deleted");

                }
            }
            else
            {
                if (sql.updateDebtor(Convert.ToDecimal(DebtorDue.Text), DebtorID.Text))
                {
                    MessageBox.Show("Updated");

                }
            }

            DebtorGrid.DataSource = sql.FillDebtor();
            DebtorGrid.Update();
            DebtorGrid.ClearSelection();


            DebtorID.Clear();
            DebtorPaid.Clear();
            DebtorDue.Text = "0.0";
        }


        public void fill_Creditor_Load()
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query = @"SELECT [CreditorId] as 'Creditor ID', [AgentName] as 'Agent Name', [CreditorDue] as 'Due', [Date] as 'Date',[CompanyName] as 'Company Name',  [PhoneNumber] as 'Phone Number' FROM  dbo.Creditor JOIN dbo.Agent On dbo.Creditor.AgentId = dbo.Agent.AgentId ;";
                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                bunifuCustomDataGrid3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                bunifuCustomDataGrid3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bunifuCustomDataGrid3.DataSource = data;
                bunifuCustomDataGrid3.Update();
                bunifuCustomDataGrid3.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }


        //Yo oh ho


        /////////////////////////////////////////////////////////////////////////

        public void ProfitLoss_Retil_Fill(string a, int i)
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query;
                if (i == 1)
                {
                    query = @"SELECT [RetailId] as 'Retail ID', [TotalBill] as 'Total Bill', [Paid] As 'Paid Amount', [Retail].[Date] as 'Date' FROM  dbo.Retail Where Retail.Date = '" + a + "';";

                }
                else
                    query = @"SELECT [RetailId] as 'Retail ID', [TotalBill] as 'Total Bill', [Paid] As 'Paid Amount', [Retail].[Date] as 'Date' FROM  dbo.Retail Where Year(Retail.Date) = '" + a + "';";
                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                plretailsbillsgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                plretailsbillsgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                plretailsbillsgrid.DataSource = data;
                plretailsbillsgrid.Update();
                plretailsbillsgrid.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        public void ProfitLoss_Retil_Fill2(string a, string b)
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query;
                query = @"SELECT [RetailId] as 'Retail ID', [TotalBill] as 'Total Bill', [Paid] As 'Paid Amount', [Retail].[Date] as 'Date' FROM  dbo.Retail Where Year(Retail.Date)='" + b + "' and Month(Retail.Date)='" + a + "';";
                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                plretailsbillsgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                plretailsbillsgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                plretailsbillsgrid.DataSource = data;
                plretailsbillsgrid.Update();
                plretailsbillsgrid.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        public void ProfitLoss_WholeSale_Fill(string a, int i)
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query;
                if (i == 1)
                {
                    query = @"SELECT [WholesaleId] as 'Wholesale ID', [CustomerName] as 'Customer Name', [TotalBill] as 'Total Bill', [Paid] As 'Paid Amount', [Wholesale].[Date] as 'Date' FROM  dbo.Wholesale JOIN dbo.Customer On dbo.Wholesale.CustomerId = dbo.Customer.CustomerId Where Wholesale.Date = '" + a + "';";
                }
                else
                    query = @"SELECT [WholesaleId] as 'Wholesale ID', [CustomerName] as 'Customer Name', [TotalBill] as 'Total Bill', [Paid] As 'Paid Amount', [Wholesale].[Date] as 'Date' FROM  dbo.Wholesale JOIN dbo.Customer On dbo.Wholesale.CustomerId = dbo.Customer.CustomerId Where Year(Wholesale.Date) = '" + a + "';";

                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                plwholesalebillsgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                plwholesalebillsgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                plwholesalebillsgrid.DataSource = data;
                plwholesalebillsgrid.Update();
                plwholesalebillsgrid.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        public void ProfitLoss_WholeSale_Fill2(string a, string b)
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query;

                query = @"SELECT [WholesaleId] as 'Wholesale ID', [CustomerName] as 'Customer Name', [TotalBill] as 'Total Bill', [Paid] As 'Paid Amount', [Wholesale].[Date] as 'Date' FROM  dbo.Wholesale JOIN dbo.Customer On dbo.Wholesale.CustomerId = dbo.Customer.CustomerId Where Year(Wholesale.Date) = '" + b + "' and Month(Wholesale.Date)='" + a + "';";

                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                plwholesalebillsgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                plwholesalebillsgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                plwholesalebillsgrid.DataSource = data;
                plwholesalebillsgrid.Update();
                plwholesalebillsgrid.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void settingspanel_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ProfitLoss_ExtraCost_Fill(string a, int i)
        {


            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query;
                if (i == 1)
                {
                    query = @"SELECT [CostId] as 'Cost ID', [CostType] as 'Cost Type', [Amount] as 'Amount', [Date] As 'Date' FROM  dbo.ExtraCost Where Date ='" + a + "';";
                }
                else
                    query = @"SELECT [CostId] as 'Cost ID', [CostType] as 'Cost Type', [Amount] as 'Amount', [Date] As 'Date' FROM  dbo.ExtraCost Where Year(Date) ='" + a + "' ;";

                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                plextracostsgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                plextracostsgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                plextracostsgrid.DataSource = data;
                plextracostsgrid.Update();
                plextracostsgrid.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }



        public void ProfitLoss_ExtraCost_Fill2(string a, string b)
        {


            try
            {
                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query;
                query = @"SELECT [CostId] as 'Cost ID', [CostType] as 'Cost Type', [Amount] as 'Amount', [Date] As 'Date' FROM  dbo.ExtraCost Where Month(Date) ='" + a + "' and Year(Date)='" + b + "';";


                //String query = "SELECT ProductId AS 'Product ID', ProductName AS 'Product Name', ProductType AS 'Product Type', BuyingRate AS 'Buying Rate', SellingRate AS 'Selling Rate' FROM dbo.Product; ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                plextracostsgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                plextracostsgrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                plextracostsgrid.DataSource = data;
                plextracostsgrid.Update();
                plextracostsgrid.ClearSelection();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void bunifuDropdown4_onItemSelected(object sender, EventArgs e)
        {
            if (bunifuDropdown4.selectedValue.ToString() == "Date")
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
            else if (bunifuDropdown4.selectedValue.ToString() == "Month")
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = false;
            }
            else if (bunifuDropdown4.selectedValue.ToString() == "Year")
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = true;

            }
        }

        private void settingsbutton_Click(object sender, EventArgs e)
        {
            this.settingspanel.Visible = true;
            this.settingspanel.BringToFront();

            this.userzonepanel.Visible = true;
            this.userspanel.Visible = true;
            this.refundpanel.Visible = true;
            this.extracostpanel.Visible = true;
            this.customerspanel.Visible = true;
            this.creditorpanel.Visible = true;
            this.debtorpanel.Visible = true;
            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;

            this.aboutuspanel.Visible = false;

            LoadCompanyDetail();
            //companyDatagridView.Update();
            //companyDatagridView.ClearSelection();
        }

      

        private void userzonebutton_Click(object sender, EventArgs e)
        {
            this.userzonepanel.Visible = true;
            this.userzonepanel.BringToFront();

            this.userspanel.Visible = true;
            this.extracostpanel.Visible = true;
            this.refundpanel.Visible = true;
            this.customerspanel.Visible = true;
            this.creditorpanel.Visible = true;
            this.debtorpanel.Visible = true;
            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;

            this.settingspanel.Visible = false;
            this.aboutuspanel.Visible = false;
            fill_Ledger();
        }

        private void companydetailedit_Click(object sender, EventArgs e)
        {
            name.Enabled = true;
            address.Enabled = true;
            phone.Enabled = true;
            website.Enabled = true;
            email.Enabled = true;
            findLogo.Enabled = true;
        }

        private void refund_Click(object sender, EventArgs e)
        {
            this.refundpanel.Visible = true;
            this.refundpanel.BringToFront();


            this.extracostpanel.Visible = true;
            this.customerspanel.Visible = true;
            this.creditorpanel.Visible = true;
            this.debtorpanel.Visible = true;
            this.billwholesalepanel.Visible = true;
            this.plpanel.Visible = true;
            this.productseditpanel.Visible = true;
            this.inventoryviewpanel.Visible = true;
            this.inventoryaddpanel.Visible = true;
            this.plretailsbillscards.Visible = true;
            this.plwholesalebillscards.Visible = true;
            this.plextracostscards.Visible = true;
            this.agentlistpanel.Visible = true;
            this.billretailpanel.Visible = true;
            this.userspanel.Visible = false;
            this.userzonepanel.Visible = false;
            this.settingspanel.Visible = false;
            this.aboutuspanel.Visible = false;
            SQLCommands sql = new SQLCommands();


            refundwholesaleId.DataSource = sql.CheckWholesaleID();
            refundwholesaleId.DisplayMember = "WholesaleId";
            refundwholesaleId.ValueMember = "CustomerId";
            refundwholesaleId.SelectedIndex = -1;


            refundgrid.DataSource = sql.RefundGriddFill();
            refundgrid.Update();

        }

        private void bunifuCustomLabel28_Click(object sender, EventArgs e)
        {

        }

        private void ProfitLossCheck_Click(object sender, EventArgs e)
        {
            string a;
            string b;

            decimal WcostofGoods, Wrevenue, RcostofGoods, Rrevenue, extraCost, grossProfit;
            SQLCommands sql = new SQLCommands();
            if (dateTimePicker1.Enabled)
            {
                a = dateTimePicker1.Value.ToString();
                a = a.Remove(9);
                //MessageBox.Show(a);
                ProfitLoss_ExtraCost_Fill(a, 1);
                plextracostsgrid.Update();
                plextracostsgrid.ClearSelection();

                ProfitLoss_Retil_Fill(a, 1);
                plretailsbillsgrid.Update();
                plretailsbillsgrid.ClearSelection();

                ProfitLoss_WholeSale_Fill(a, 1);
                plwholesalebillsgrid.Update();
                plwholesalebillsgrid.ClearSelection();

                Wrevenue = sql.SumWRev(a, "", "", 1);
                Rrevenue = sql.SumRRev(a, "", "", 1);
                Revenue.Text = (Wrevenue + Rrevenue).ToString();

                WcostofGoods = sql.SumWCog(a, "", "", 1);
                RcostofGoods = sql.SumRCog(a, "", "", 1);
                //MessageBox.Show(sql.SumWCog(a, "", "", 1));

                CostofGoodsold.Text = (WcostofGoods + RcostofGoods).ToString();

                grossProfit = (Wrevenue + Rrevenue) - (WcostofGoods + RcostofGoods);
                GrossProfit.Text = grossProfit.ToString();
                extraCost = sql.SumExtra(a, "", "", 1);
                ExtraCost.Text = extraCost.ToString();

                NetProfit.Text = (grossProfit - extraCost).ToString();

            }
            else if (dateTimePicker2.Enabled)
            {
                a = dateTimePicker2.Value.ToString("MM");
                b = dateTimePicker2.Value.ToString("yyyy");
                // a = a.Substring(3,a.Length-12);
                // MessageBox.Show(b);
                ProfitLoss_ExtraCost_Fill2(a, b);
                plextracostsgrid.Update();
                plextracostsgrid.ClearSelection();

                ProfitLoss_Retil_Fill2(a, b);
                plretailsbillsgrid.Update();
                plretailsbillsgrid.ClearSelection();

                ProfitLoss_WholeSale_Fill2(a, b);
                plwholesalebillsgrid.Update();
                plwholesalebillsgrid.ClearSelection();

                Wrevenue = sql.SumWRev("", b, a, 2);
                Rrevenue = sql.SumRRev("", b, a, 2);
                Revenue.Text = (Wrevenue + Rrevenue).ToString();

                WcostofGoods = sql.SumWCog("", b, a, 2);
                RcostofGoods = sql.SumRCog("", b, a, 2);
                CostofGoodsold.Text = (WcostofGoods + RcostofGoods).ToString();

                grossProfit = (Wrevenue + Rrevenue) - (WcostofGoods + RcostofGoods);
                GrossProfit.Text = grossProfit.ToString();
                extraCost = sql.SumExtra("", b, a, 2);
                ExtraCost.Text = extraCost.ToString();

                NetProfit.Text = (grossProfit - extraCost).ToString();
            }
            else if (dateTimePicker3.Enabled)
            {
                b = dateTimePicker3.Value.ToString("yyyy");
                ProfitLoss_ExtraCost_Fill(b, 2);
                plextracostsgrid.Update();
                plextracostsgrid.ClearSelection();

                ProfitLoss_Retil_Fill(b, 2);
                plretailsbillsgrid.Update();
                plretailsbillsgrid.ClearSelection();

                ProfitLoss_WholeSale_Fill(b, 2);
                plwholesalebillsgrid.Update();
                plwholesalebillsgrid.ClearSelection();

                Wrevenue = sql.SumWRev("", b, "", 3);
                Rrevenue = sql.SumRRev("", b, "", 3);
                Revenue.Text = (Wrevenue + Rrevenue).ToString();

                WcostofGoods = sql.SumWCog("", b, "", 3);
                RcostofGoods = sql.SumRCog("", b, "", 3);
                CostofGoodsold.Text = (WcostofGoods + RcostofGoods).ToString();

                grossProfit = (Wrevenue + Rrevenue) - (WcostofGoods + RcostofGoods);
                GrossProfit.Text = grossProfit.ToString();
                extraCost = sql.SumExtra("", b, "", 3);
                ExtraCost.Text = extraCost.ToString();

                NetProfit.Text = (grossProfit - extraCost).ToString();
            }

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                fiilReturnInventory();
                GetReturn.ShowDialog();
                GetReturn.InventoryReturnDataGridView.Update();
                GetReturn.InventoryReturnDataGridView.ClearSelection();
                fillStoreDatagridView();
                bunifuCustomDataGrid1.Update();
                bunifuCustomDataGrid1.ClearSelection();
            }
            else
            {
                MessageBox.Show("Please Select A row First");
            }


        }

        private void settingssavebutton_Click(object sender, EventArgs e)
        {
   
            if (name.Enabled)
            {
                if (name.Text != "")
                {

                    try
                    {
                        //String query = "INSERT INTO [dbo].[Company] ( [CompanyName], [CompanyAddress], [CompanyPhone],[CompanyWebsite],[CompanyEmail],[CompanyLogo]) VALUES( @name, @address, @phone, @website,@email,@logo)";
                        String query = @"update [dbo].[Company] set [CompanyName]=@name,[CompanyAddress]=@address, [CompanyPhone]=@phone,[CompanyWebsite]=@website,[CompanyEmail]=@email,[CompanyLogo]=@logo where id=1 IF @@ROWCOUNT = 0 
                    INSERT INTO[dbo].[Company] ( [CompanyName], [CompanyAddress], [CompanyPhone],[CompanyWebsite],[CompanyEmail],[CompanyLogo]) VALUES(@name, @address, @phone, @website, @email, @logo);";

                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();
                                cmd.Parameters.AddWithValue("@name", this.name.Text.Trim());
                                cmd.Parameters.AddWithValue("@address", this.address.Text.Trim());
                                cmd.Parameters.AddWithValue("@phone", this.phone.Text.Trim());
                                cmd.Parameters.AddWithValue("@website", this.website.Text.Trim());
                                cmd.Parameters.AddWithValue("@email", this.email.Text.Trim());
                                //cmd.Parameters.AddWithValue("@logo", image);
                                if (image != null)
                                    cmd.Parameters.AddWithValue("@logo", SqlDbType.Image).Value = image;
                                else
                                {
                                    SqlParameter imageParameter = new SqlParameter("@logo", SqlDbType.Image);
                                    imageParameter.Value = DBNull.Value;
                                    cmd.Parameters.Add(imageParameter);
                                }
                                int k = cmd.ExecuteNonQuery();
                                if (k > 0)
                                {
                                    MessageBox.Show("sucessfully");
                                }
                                else
                                {
                                    MessageBox.Show("Not");
                                }

                                //LoadCompanyDetail();
                                //logo.Image = null;
                                name.Enabled = false;
                                address.Enabled = false;
                                phone.Enabled = false;
                                website.Enabled = false;
                                email.Enabled = false;
                                findLogo.Enabled = false;
                                sqlCon.Close();
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Fill All the data ");
                }
            }
            else
            {
                MessageBox.Show("Click Edit");
            }
        }


        private void findLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg files(*.jpg)|*.jpg|png files(*.png)|*.png|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imageLoc = dialog.FileName.ToString();
                logo.ImageLocation = imageLoc;
                FileStream stream = new FileStream(imageLoc, FileMode.Open, FileAccess.Read);
                BinaryReader binary = new BinaryReader(stream);
                image = binary.ReadBytes((int)stream.Length);
            }

        }



        public void LoadCompanyDetail()
        {
            try
            {

                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                String query;
                query = @"SELECT CompanyName AS 'Compay Name',CompanyAddress AS 'Company Address',CompanyPhone AS 'Company Phone',CompanyWebsite AS 'Company Website',CompanyEmail AS 'Company Email',CompanyLogo AS 'Company Logo' FROM  dbo.Company ;";
                //sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);

                SqlDataReader Reader;
                try
                {
                    sqlConn.Open();
                    Reader = cmd.ExecuteReader();

                    while (Reader.Read())
                    {
                        name.Text = Reader.GetValue(0).ToString();
                        address.Text = Reader.GetValue(1).ToString();
                        phone.Text = Reader.GetValue(2).ToString();
                        website.Text = Reader.GetValue(3).ToString();
                        email.Text = Reader.GetValue(4).ToString();
                        //comboBox4.Text = Reader.GetValue(8).ToString();
                       
                        if (Reader.GetValue(5) == DBNull.Value)
                        {
                            logo.Image = null;
                        
                        }
                        else
                        {
                            byte[] image = (byte[])Reader.GetValue(5);
                            MemoryStream memoryStream = new MemoryStream(image);
                            logo.Image = Image.FromStream(memoryStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                sqlConn.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void EveryDaySellChart_Click(object sender, EventArgs e)
        {


        }


        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (Convert.ToDecimal(refundquantity.Text.ToString()) <= sql.returnquantity(refundwholesaleId.SelectedValue.ToString(), refundproductname.SelectedValue.ToString()))
                MessageBox.Show(sql.FillRefund(refundwholesaleId.Text, refundwholesaleId.SelectedValue.ToString(), refundquantity.Text, refundproductname.SelectedValue.ToString()));
            else
                MessageBox.Show("Quantity Exceeds Buying amount!");

            refundquantity.Clear();
            refundproductname.SelectedIndex = -1;
            refundwholesaleId.SelectedIndex = -1;

            refundgrid.DataSource = sql.RefundGriddFill();
            refundgrid.Update();
        }

        

        private void refundwholesaleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();

            refundproductname.DataSource = sql.fillProductDD(refundwholesaleId.SelectedValue.ToString());
            refundproductname.DisplayMember = "ProductName";
            refundproductname.ValueMember = "ProductId";
            refundproductname.SelectedIndex = -1;
        }

        public void AddToLedger()
        {
            try
            {



                String query = "INSERT INTO [dbo].[Ledger] ( [CustomerId], [Description], [Debit], [Credit],[Balance]) " +
                    "VALUES( @customerId, 'Bill' , @debit, 0.00 , 0.00) ," +
                    "(@customerId, 'Cash Recived' , 0.00, @credit , @balance);";


                using (SqlConnection sqlCon = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCon.Open();
                        cmd.Parameters.AddWithValue("@customerId", Convert.ToInt32(WcustomerDD.SelectedValue));
                        cmd.Parameters.AddWithValue("@debit", Convert.ToDecimal(this.Wtotal.Text.Trim()));
                        cmd.Parameters.AddWithValue("@credit", Convert.ToDecimal(this.Wpaid.Text.Trim()));
                        Decimal balance = Convert.ToDecimal(this.Wtotal.Text.Trim()) - Convert.ToDecimal(this.Wpaid.Text.Trim());
                        cmd.Parameters.AddWithValue("@balance", balance);
                        int k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            MessageBox.Show("Ledger sucessful");


                        }
                        else
                        {
                            MessageBox.Show("Inserted Not Inserted");
                        }
                        sqlCon.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }





        private void SkuTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strgroupids;
            if (e.KeyChar == (char)Keys.Return)
            {
                strgroupids = SkuTextBox.Text;
                strgroupids = strgroupids.Remove(strgroupids.Length - 1);
                DataTable dt = new DataTable();
                SQLCommands sql = new SQLCommands();
                dt = sql.FindProductSku(SkuTextBox.Text);
                if (dt.Rows.Count > 0)
                {
                   // MessageBox.Show("Hooray");
                    WProductName.Text = dt.Rows[0]["ProductName"].ToString();
                    WholesaleProductID = dt.Rows[0]["ProductId"].ToString();
                    Wavailable = Convert.ToDecimal(dt.Rows[0]["Quantity"]);
                }
                else
                    MessageBox.Show("Product Not Found");
                SkuTextBox.Text = strgroupids;
            }
        }


        private void WholesaleRefresh_Click(object sender, EventArgs e)
        {
            SkuTextBox.Clear();
        }
       
        private void WholesaleAdd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
            WholesaleDetailsId = WholesaleAdd.CurrentRow.Cells[5].Value.ToString();
            //MessageBox.Show(WholesaleDetailsId);            
        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (sql.DeleteWholelsaledetails(WholesaleDetailsId))
            {
                MessageBox.Show("Entry Deleted");
            }
            else
                MessageBox.Show("Failed");
            WholesaleAdd.DataSource = sql.Fillwholesalel(sql.WtopID());
            WholesaleAdd.Columns[5].Visible = false;
            WholesaleAdd.Update();
        }


        private void RetailSkuText_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strgroupids;
            if (e.KeyChar == (char)Keys.Return)
            {
                strgroupids = RetailSkuText.Text;
                strgroupids = strgroupids.Remove(strgroupids.Length - 1);
                DataTable dt = new DataTable();
                SQLCommands sql = new SQLCommands();
                dt = sql.FindProductSku(RetailSkuText.Text);
                if (dt.Rows.Count > 0)
                {
                    //MessageBox.Show("Hooray");
                    RproductName.Text = dt.Rows[0]["ProductName"].ToString();
                    WholesaleProductID = dt.Rows[0]["ProductId"].ToString();
                    Wavailable = Convert.ToDecimal(dt.Rows[0]["Quantity"]);
                }
                else
                    MessageBox.Show("Product Not Found");
                RetailSkuText.Text = strgroupids;
            }
        }


        private void WholeChart()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Create a connection to SQL DataBase
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //Select all the records in database
            string command = @"SELECT TOP(5) dbo.Product.ProductName ,SUM(dbo.WholesaleDetails.Quantity)/ (SELECT TOP(5) SUM(dbo.WholesaleDetails.Quantity) from dbo.WholesaleDetails) AS TotalQuantity
FROM dbo.WholesaleDetails
JOIN dbo.Product On dbo.WholesaleDetails.ProductId = dbo.Product.ProductId 
GROUP BY dbo.Product.ProductName
ORDER BY SUM(dbo.WholesaleDetails.Quantity) DESC";
            SqlCommand cmd = new SqlCommand(command, con);
            adapter.SelectCommand = cmd;





            //Retrieve the records from database
            adapter.Fill(table);
            this.WholesalePopulerProdutChart.DataSource = table;

            //Mapping a field with x-value of chart
            this.WholesalePopulerProdutChart.Series["Series1"].XValueMember = "ProductName";
            // this.EveryDaySellChart.Series["Series1"].Points.AddXY("Date", "Total");
            //Mapping a field with y-value of Chart
            this.WholesalePopulerProdutChart.Series["Series1"].YValueMembers = "TotalQuantity";

            //Bind the DataTable with Chart
            this.WholesalePopulerProdutChart.DataBind();

        }
        /// <summary>
        /// MOnthly 
        /// </summary>
        private void MonthlyChart()
        {
            int Year;
            if (YearPickerForMonthChart.SelectedIndex == -1)
            {
                Year = DateTime.Now.Year;
            }
            else
            {
                Year = Convert.ToInt32(YearPickerForMonthChart.Text);
            }
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Create a connection to SQL DataBase
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //Select all the records in database
            string command = @" SELECT SalesYear,Month,
 MonthName , isnull(SUM(TotalSales),0) AS Total from  (
  SELECT YEAR(Date) as SalesYear,
  MONTH(Date) as Month,
 DATENAME(MONTH,Date) as MonthName,
isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Retail 
GROUP BY YEAR(Date),DATENAME(MONTH,Date), MONTH(Date)
Union
  SELECT YEAR(Date) as SalesYear,
	 MONTH(Date) as Month,
         DATENAME(MONTH,Date) as MonthName,
         isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Wholesale 
GROUP BY YEAR(Date),DATENAME(MONTH,Date),MONTH(Date)
)  a Where SalesYear =  "+ Year +  @"
GROUP BY SalesYear, MonthName,Month
Order BY Month asc";
            SqlCommand cmd = new SqlCommand(command, con);
            adapter.SelectCommand = cmd;

            //Retrieve the records from database
            adapter.Fill(table);
            if (table != null && table.Rows.Count > 0)
            {
                this.EveryMonthSellChart.DataSource = table;

                //Mapping a field with x-value of chart
                this.EveryMonthSellChart.Series["Sale"].XValueMember = "MonthName";
                // this.EveryDaySellChart.Series["Series1"].Points.AddXY("Date", "Total");
                //Mapping a field with y-value of Chart
                this.EveryMonthSellChart.Series["Sale"].YValueMembers = "Total";

                //Bind the DataTable with Chart
                this.EveryMonthSellChart.DataBind();
            }
            else
            {
                MessageBox.Show("There Is no Data");
                YearPickerForMonthChart.SelectedIndex = -1;
            }
        }
        private void DailyChart()
        {

            int Year;
            int Month;
            if (yearCombox.SelectedIndex == -1)
            {
                Year = DateTime.Now.Year;
            }
            else
            {
                Year = Convert.ToInt32(yearCombox.Text);
            }
            if (monthCombobox.SelectedIndex == -1)
            {
                Month = DateTime.Now.Month;
            }
            else
            {
                Month = monthCombobox.SelectedIndex +1;
            }
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Create a connection to SQL DataBase
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //Select all the records in database
            string command = @"SELECT SalesYear,Month, Day,isnull(SUM(TotalSales),0) AS Total from  (
  SELECT YEAR(Date) as SalesYear,
  MONTH(Date) as Month,
  Day(Date) as Day,
isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Retail 
GROUP BY YEAR(Date), MONTH(Date), Day(Date)
Union
  SELECT YEAR(Date) as SalesYear,
	 MONTH(Date) as Month,
		 Day(Date) as Day,
         isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Wholesale 
GROUP BY YEAR(Date),MONTH(Date), Day(Date)
)  a WHERE SalesYear = "+Year+@" AND Month = "+ Month + @"
GROUP BY SalesYear,Month,Day
Order BY Day asc";
            SqlCommand cmd = new SqlCommand(command, con);
            adapter.SelectCommand = cmd;

            //Retrieve the records from database
            adapter.Fill(table);
            adapter.Fill(table);
            if (table != null && table.Rows.Count > 0)
            {
                this.EveryDaySellChart.DataSource = table;

                //Mapping a field with x-value of chart
                this.EveryDaySellChart.Series["Sale"].XValueMember = "Day";
                // this.EveryDaySellChart.Series["Sale"].Points.AddXY("Date", "Total");
                //Mapping a field with y-value of Chart
                this.EveryDaySellChart.Series["Sale"].YValueMembers = "Total";

                //Bind the DataTable with Chart
                this.EveryDaySellChart.DataBind();
            }
            else
            {
                MessageBox.Show("There Is no Data");
                yearCombox.SelectedIndex = -1;
                monthCombobox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// MOnthly 
        /// </summary>
        private void YearlyChart()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Create a connection to SQL DataBase
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //Select all the records in database
            string command = @" SELECT SalesYear, isnull(SUM(TotalSales),0) AS Total from  (
  SELECT YEAR(Date) as SalesYear,
  MONTH(Date) as Month,
 DATENAME(MONTH,Date) as MonthName,
isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Retail 
GROUP BY YEAR(Date),DATENAME(MONTH,Date), MONTH(Date)
Union
  SELECT YEAR(Date) as SalesYear,
	 MONTH(Date) as Month,
         DATENAME(MONTH,Date) as MonthName,
         isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Wholesale 
GROUP BY YEAR(Date),DATENAME(MONTH,Date),MONTH(Date)
)  a 
GROUP BY SalesYear
Order BY SalesYear asc";
            SqlCommand cmd = new SqlCommand(command, con);
            adapter.SelectCommand = cmd;

            //Retrieve the records from database
            adapter.Fill(table);
            this.EveryYearSellChart.DataSource = table;

            //Mapping a field with x-value of chart
            this.EveryYearSellChart.Series["Sale"].XValueMember = "SalesYear";
            // this.EveryDaySellChart.Series["Series1"].Points.AddXY("Date", "Total");
            //Mapping a field with y-value of Chart
            this.EveryYearSellChart.Series["Sale"].YValueMembers = "Total";

            //Bind the DataTable with Chart
            this.EveryYearSellChart.DataBind();

        }
        /// <summary>
        /// ///////////////////
        /// 
        /// </summary>
        /// 


        private void WeeklyChart()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Create a connection to SQL DataBase
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //Select all the records in database
            string command = @"SELECT SalesYear,Month, Day,isnull(SUM(TotalSales),0) AS Total from  (
  SELECT YEAR(Date) as SalesYear,
  MONTH(Date) as Month,
  Day(Date) as Day,
isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Retail 
GROUP BY YEAR(Date), MONTH(Date), Day(Date)
Union
  SELECT YEAR(Date) as SalesYear,
	 MONTH(Date) as Month,
		 Day(Date) as Day,
         isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Wholesale 
GROUP BY YEAR(Date),MONTH(Date), Day(Date)
)  a WHERE SalesYear = YEAR(GETDATE()) AND Month = MONTH(GETDATE()) AND  Day BETWEEN DAY(DATEADD(day,-6,GETDATE())) AND DAY(GETDATE()) 
GROUP BY SalesYear,Month,Day
Order BY Day asc";
            SqlCommand cmd = new SqlCommand(command, con);
            adapter.SelectCommand = cmd;

            //Retrieve the records from database
            adapter.Fill(table);
            this.WeeklyDaySellChart.DataSource = table;

            //Mapping a field with x-value of chart
            this.WeeklyDaySellChart.Series["Sale"].XValueMember = "Day";
            // this.EveryDaySellChart.Series["Series1"].Points.AddXY("Date", "Total");
            //Mapping a field with y-value of Chart
            this.WeeklyDaySellChart.Series["Sale"].YValueMembers = "Total";

            //Bind the DataTable with Chart
            this.WeeklyDaySellChart.DataBind();

        }


        private void RetailChart()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Create a connection to SQL DataBase
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //Select all the records in database
            string command = @"SELECT TOP(5) dbo.Product.ProductName ,SUM(dbo.RetailDetails.Quantity)/ (SELECT TOP(5) SUM(dbo.RetailDetails.Quantity) from dbo.RetailDetails) AS TotalQuantity
FROM dbo.RetailDetails
JOIN dbo.Product On dbo.RetailDetails.ProductId = dbo.Product.ProductId 
GROUP BY dbo.Product.ProductName
ORDER BY SUM(dbo.RetailDetails.Quantity) DESC";
            SqlCommand cmd = new SqlCommand(command, con);
            adapter.SelectCommand = cmd;

            //Retrieve the records from database
            adapter.Fill(table);

            this.RetailPopulerProductsChart.DataSource = table;

            //Mapping a field with x-value of chart
            this.RetailPopulerProductsChart.Series["Series1"].XValueMember = "ProductName";
            // this.EveryDaySellChart.Series["Series1"].Points.AddXY("Date", "Total");
            //Mapping a field with y-value of Chart
            this.RetailPopulerProductsChart.Series["Series1"].YValueMembers = "TotalQuantity";

            //Bind the DataTable with Chart
            this.RetailPopulerProductsChart.DataBind();

        }

        private void RproductAdd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RetailDetailsId = RproductAdd.CurrentRow.Cells[5].Value.ToString();
         //   MessageBox.Show(RetailDetailsId);
        }

        private void RetailDelete_Click(object sender, EventArgs e)
        {
            SQLCommands sql = new SQLCommands();
            if (sql.DeleteRetaildetails(RetailDetailsId))
            {
                MessageBox.Show("Entry Deleted");
            }
            else
                MessageBox.Show("Failed");
            RproductAdd.DataSource = sql.FillRetail(sql.RtopID());
            RproductAdd.Columns[5].Visible = false;
            RproductAdd.Update();
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            RetailSkuText.Clear();
        }


        #region POS
        public void print()
        {

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(POSprint);
            pd.PrintController = new StandardPrintController();
            pd.DefaultPageSettings.Margins.Left = 0;
            pd.DefaultPageSettings.Margins.Right = 0;
            pd.DefaultPageSettings.Margins.Top = 0;
            pd.DefaultPageSettings.Margins.Bottom = 0;
            //  pd.DefaultPageSettings.PaperSize = ps;
            pd.Print();
        }

        DataTable MakeDataTable()
        {
            //Create friend table object
            DataTable friend = new DataTable();

            //Define columns
            friend.Columns.Add("Sl. No");
            friend.Columns.Add("Product Name");
            friend.Columns.Add("Quantity");
            friend.Columns.Add("Unit Price");
            friend.Columns.Add("Extended Price");

            //Populate with friends :)
            friend.Rows.Add(1, "HP Pavillion Notebook", 10, 40000, 400000);
            friend.Rows.Add(1, "Asus Zenbook", 10, 50000, 500000);

            return friend;
        }

        public void POSprint(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            DataTable dt = MakeDataTable();

            Font fBody = new Font("Bodoni MT Condensed", 11, FontStyle.Bold);
            Font fBody1 = new Font("Bodoni MT Condensed", 10, FontStyle.Regular);
            Font fBody2 = new Font("Bodoni MT Condensed", 7, FontStyle.Regular);
            Font rs = new Font("Stencil", 9, FontStyle.Bold);
            Font fTType = new Font("", 20, FontStyle.Bold);
            SolidBrush sb = new SolidBrush(Color.Black);
            g.DrawString("M/S MD.BACCHU SARKER RICE AGENCY", fBody, sb, 0, 10);
            // 20  makes the line get down from the previous line.
            g.DrawString("  Haji Abdul Rahim Market, Chondona,\nChowrasta Bazar(Chaul Potti), Gazipur\nProprietor: 01711453878, 01751872421", fBody1, sb, 17, 30);
            // g.DrawString("  Chowrasta Bazar(Chaul Potti), Gazipur", fBody1, sb, 17, 50);
            //g.DrawString("  Proprietor: 01711453878, 01751872421", fBody1, sb, 17, 70);
            g.DrawString("  Shopon: 01957304504", fBody1, sb, 40, 90);
            // g.DrawString("           Phone#01711453878", fBody1, sb, 20, 65);
            g.DrawString("   Date :", fBody1, sb, 10, 110);
            g.DrawString(DateTime.Now.ToShortDateString(), fBody1, sb, 90, 110);
            g.DrawString("   Time :", fBody1, sb, 10, 130);
            g.DrawString(DateTime.Now.ToShortTimeString(), fBody1, sb, 90, 130);

            g.DrawString("   Receipt No:", fBody, sb, 10, 145);
            g.DrawString(RID.Text, fBody, sb, 90, 145);
            g.DrawString("------------------------------------------------------------------", fBody1, sb, 0, 160);
            g.DrawString(" Item Name     Quantity  price/Unit   Total", fBody1, sb, 0, 175);
            g.DrawString("------------------------------------------------------------------", fBody1, sb, 0, 190);





            int SPACEE = 190;
            int ss = 0;

            int ss2 = 0;
            int ss3 = 0;
            int total_item = 1;
            string Item_name = " ";
            double Quantity = 0;
            double unit_price = 1;


            for (int i = 1; i <= Rdt.Rows.Count ; i++)
            {
                SPACEE = SPACEE + 20;
                ss = SPACEE;
                //******    


                //g.DrawString(s , fBody1, sb, 0, SPACEE );

                g.DrawString(Rdt.Rows[i - 1][1].ToString(), fBody2, sb, 0, SPACEE, new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip));
                // g.DrawString(".", fBody2, sb, 2, SPACEE, new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip));
                g.DrawString("", fBody2, sb, 15, SPACEE, new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip));
                g.DrawString(Rdt.Rows[i - 1][2].ToString(), fBody2, sb, 85, SPACEE, new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip));
                g.DrawString("  *  ", fBody2, sb, 95, SPACEE);
                g.DrawString(Rdt.Rows[i - 1][3].ToString(), fBody2, sb, 105, SPACEE);

                g.DrawString(Rdt.Rows[i - 1][4].ToString(), fBody2, sb, 140, SPACEE);
                g.DrawString("Tk", fBody2, sb, 190, SPACEE);


                /*******/

            }

            ss2 = 20 * total_item;
            int sss = ss2 + 210;
            ss3 = sss;

            sum = 0;
            for (int i = 0; i < RproductAdd.Rows.Count; ++i)
            {
                sum += Convert.ToDecimal(RproductAdd.Rows[i].Cells[4].Value);
            }

            g.DrawString("------------------------------------------------------------------", fBody1, sb, 0, sss + 10);
            g.DrawString(" Total Amount:", fBody1, sb, 0, sss + 20);
            g.DrawString(sum.ToString(), fBody1, sb, 110, sss + 20);
            g.DrawString("Tk", fBody1, sb, 165, sss + 20);

            g.DrawString(" Discount:", fBody1, sb, 0, sss + 40);
            g.DrawString(Rdiscount.Text + "%", fBody1, sb, 110, sss + 40);
            //g.DrawString("Tk", fBody1, sb, 165, sss + 40);

            g.DrawString(" Total:", fBody1, sb, 0, sss + 60);
            g.DrawString(RtotalBill.Text, fBody1, sb, 110, sss + 60);
            g.DrawString("Tk", fBody1, sb, 165, sss + 60);

            g.DrawString("Change:", fBody, sb, 0, sss + 80);
            g.DrawString(Rdue.Text, fBody, sb, 110, sss + 80);
            g.DrawString("Tk", fBody, sb, 165, sss + 80);
            g.DrawString("------------------------------------------------------------------", fBody1, sb, 0, sss + 90);
            g.DrawString("Thank You for Your Business.", fBody1, sb, 40, sss + 100);
            g.DrawString("Software Distributed by Perky Rabbit", fBody1, sb, 19, sss + 140);
            g.DrawString("       For Support: ", fBody1, sb, 40, sss + 160);
            g.DrawString("Website: www.perkyrabbit.com ", fBody1, sb, 19, sss + 180);
            g.DrawString("Email: support@perkyrabbit.com ", fBody1, sb, 19, sss + 200);
            g.DrawString("Phone: +88029859491, +8801708518090", fBody1, sb, 17, sss + 220);
            g.DrawString("", fBody1, sb, 40, sss + 240);
        }
        #endregion








        private void dashboardCharts ()
        {
            var today = DateTime.Now.ToString("MM/dd/yyyy");
            try
            {

                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();

                String query = @"Select isnull(SUM(Total),0) AS Bill2  From (
        (SELECT isnull(SUM(TotalBill),0) AS Total
        FROM dbo.Wholesale 
        WHERE Date = '" + today + "') Union All "+
        @"( SELECT  isnull(SUM(TotalBill),0) AS Total 
        FROM dbo.Retail 
        WHERE  Date  = '" + today + "' ) ) bill";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                label2.Text = reader["Bill2"].ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }
            ///////////////
            try
            {

                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();

                String query = @"Select Sum (Total) as Bill From  (
			 (SELECT isnull(SUM(TotalBill),0) AS Total
			  FROM dbo.Wholesale )  
        Union All
			  ( SELECT  isnull(SUM(TotalBill),0) AS Total
			  FROM dbo.Retail ) 
		) Bill";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                label21.Text = reader["Bill"].ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }
            ///////////////
            try
            {

                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();

                String query = @"SELECT  isnull(SUM(Amount),0) AS Amount 
        FROM dbo.ExtraCost 
        WHERE  Date  = '" + today + "' ";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                label3.Text = reader["Amount"].ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }
            ///////////////
            try
            {

                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();

                String query = @"SELECT  isnull(SUM(Amount),0) AS Amount2  FROM dbo.ExtraCost ";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                label23.Text = reader["Amount2"].ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }
            ///////////////
            try
            {

                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();

                String query = @"SELECT  isnull(SUM(DebtorDue),0) AS Due  FROM dbo.Debtor ;";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                label25.Text = reader["Due"].ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }

            ///////////////
            try
            {

                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();

                String query = @"SELECT  isnull(SUM(CreditorDue),0) AS Due  FROM dbo.Creditor ;";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                label14.Text = reader["Due"].ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }



        }

        private void YearPickerForMonthChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonthlyChart();
        }

        private void yearCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DailyChart();
        }

        private void monthCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DailyChart();
            
        }

        private void dashboarddropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (dashboarddropdown.SelectedIndex == 0)
            {
                WeeklyDaySellChartPanel.BringToFront();
                WeeklyDaySellChartPanel.Visible = true;
                EveryDaySellChartPanel.Visible = false;
                EveryMonthSellChartPanel.Visible = false;
                EveryYearSellChartPanel.Visible = false;
                customChartPanel.Visible = false;
            }
            else if (dashboarddropdown.SelectedIndex == 1)
            {
                WeeklyDaySellChartPanel.Visible = true;
                EveryDaySellChartPanel.BringToFront();
                EveryDaySellChartPanel.Visible = true;

                EveryMonthSellChartPanel.Visible = false;
                EveryYearSellChartPanel.Visible = false;
                customChartPanel.Visible = false;
            }
            else if (dashboarddropdown.SelectedIndex == 2)
            {
                EveryDaySellChartPanel.Visible = true;
                WeeklyDaySellChartPanel.Visible = true;
                EveryMonthSellChartPanel.BringToFront();
                EveryMonthSellChartPanel.Visible = true;

                EveryYearSellChartPanel.Visible = false;
                customChartPanel.Visible = false;
            }
            else if (dashboarddropdown.SelectedIndex == 3)
            {
                EveryDaySellChartPanel.Visible = true;
                WeeklyDaySellChartPanel.Visible = true;
                EveryMonthSellChartPanel.Visible = true;
                EveryYearSellChartPanel.BringToFront();
                EveryYearSellChartPanel.Visible = true;
                customChartPanel.Visible = false;


            }
            else if (dashboarddropdown.SelectedIndex == 4)
            {
                EveryDaySellChartPanel.Visible = true;
                WeeklyDaySellChartPanel.Visible = true;
                EveryMonthSellChartPanel.Visible = true;
                EveryYearSellChartPanel.Visible = true;
                customChartPanel.BringToFront();
                customChartPanel.Visible = true;

            }

            }

        private void SetDate_Click(object sender, EventArgs e)
        {
            // int date1 = datePicker1.Value.ToShortDateString.;
            string date1 = datePicker1.Value.ToShortDateString();
            string date2 = datePicker2.Value.ToShortDateString();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Create a connection to SQL DataBase
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //Select all the records in database
            string command = @" SELECT Date,
isnull(SUM(TotalSales),0) AS Total FROM
( SELECT Date,
isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Retail 
	Group by Date
	union
 SELECT Date,
isnull(SUM(TotalBill),0) AS TotalSales
    FROM dbo.Wholesale 
	Group by Date
	) a WHERE Date BETWEEN '"+ date1 + @"' AND '" + date2 + @"'
	Group by Date 
	Order by Date asc";
            SqlCommand cmd = new SqlCommand(command, con);
            adapter.SelectCommand = cmd;

            //Retrieve the records from database
            adapter.Fill(table);
            this.customChart.DataSource = table;

            //Mapping a field with x-value of chart
            this.customChart.Series["Sale"].XValueMember = "Date";
            // this.EveryDaySellChart.Series["Series1"].Points.AddXY("Date", "Total");
            //Mapping a field with y-value of Chart
            this.customChart.Series["Sale"].YValueMembers = "Total";

            //Bind the DataTable with Chart
            this.customChart.DataBind();






        }

        private void productseditpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchProduct_TextChanged(object sender, EventArgs e)
        {           
            DataView dv = new DataView(Productdata);
            dv.RowFilter = string.Format(@"[Product Name] LIKE '%{0}%'", SearchProduct.Text);
            productsgridview.DataSource = dv;

        }




        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Productdata);
            dv.RowFilter = string.Format(@"[Product Type] LIKE '%{0}%'", SearchType.Text);
            productsgridview.DataSource = dv;


        }


        private void SearchAgent_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Wdt);
            dv.RowFilter = string.Format(@"[Agent Name] LIKE '%{0}%'", SearchAgent.Text);
            AgentListDataGridView.DataSource = dv;
        }


        private void DebtorSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Wdt);
            dv.RowFilter = string.Format(@"[Customer Name] LIKE '%{0}%'", DebtorSearch.Text);
            DebtorGrid.DataSource = dv;
        }


        private void CreditorSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Wdt);
            dv.RowFilter = string.Format(@"[Agent Name] LIKE '%{0}%'", CreditorSearch.Text);
            bunifuCustomDataGrid3.DataSource = dv;
        }


        private void CustomerSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Wdt);
            dv.RowFilter = string.Format(@"[Customer Name] LIKE '%{0}%'", CustomerSearch.Text);
            customersgrid.DataSource = dv;
        }

        private void ExtracostSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Wdt);
            dv.RowFilter = string.Format(@"[Cost Type] LIKE '%{0}%'", ExtracostSearch.Text);
            extracostgrid.DataSource = dv;
        }

        private void UserSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(Wdt);
            dv.RowFilter = string.Format(@"[User Name] LIKE '%{0}%'", UserSearch.Text);
            usersgrid.DataSource = dv;
        }


    }
}

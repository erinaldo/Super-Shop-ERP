using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy.Inventory
{
    public partial class AddInventory : Form
    {
      
        Decimal newCost;
        Decimal dueAmount;
        Decimal totalBillAmount;
        static string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

        public AddInventory()
        {
            InitializeComponent();

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void AddInventory_Load(object sender, EventArgs e)
        {

            
            getStoreId();
            getProductName();
            getAgentName();

        }
        public void getProductName()
        {
            SqlConnection sqlCon = new SqlConnection(conString);
            SqlDataReader saReader = null;

            try
            {

                DataTable tb = new DataTable("Product");

                sqlCon.Open();
                string query = "SELECT [ProductId] ,[ProductName] FROM[dbo].[Product];";
                SqlCommand cmd = new SqlCommand(query, sqlCon);


                saReader = cmd.ExecuteReader();
                tb.Load(saReader);

                productName.DisplayMember = "ProductName";
                productName.ValueMember = "ProductId";

                productName.DataSource = tb;
               
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // Close data reader object and database connection
                if (saReader != null)
                    saReader.Close();

                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }




        }
        






        public void getAgentName()
        {
            SqlConnection sqlCon = new SqlConnection(conString);
            SqlDataReader saReader = null;
            try
            {

                DataTable tb = new DataTable("Agent");

                sqlCon.Open();
                string query = "SELECT[AgentId] ,[AgentName] FROM[dbo].[Agent];";
                SqlCommand cmd = new SqlCommand(query, sqlCon);


                saReader = cmd.ExecuteReader();
                tb.Load(saReader);
                agentName.DisplayMember = "AgentName";
                agentName.ValueMember = "AgentId";
                agentName.DataSource = tb;

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // Close data reader object and database connection
                if (saReader != null)
                    saReader.Close();

                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }





        }

        private void productNameChanged(object sender, EventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(conString);
            SqlDataReader saReader = null;


            try
            {


                string query = "SELECT * FROM [dbo].[Product] WHERE [ProductId]= " + Convert.ToInt32(productName.SelectedValue.ToString()) + "  ;";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                sqlCon.Open();
                using (saReader = cmd.ExecuteReader())
                {
                    if (saReader.Read())
                    {
                        productRate.Text = saReader["BuyingRate"].ToString();//column name should be that you want to show on textbox

                    }
                    // saReader.Close();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                // Close data reader object and database connection
                if (saReader != null)
                    saReader.Close();

                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }



        }

        private void agentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(conString);
            SqlDataReader saReader = null;

            try
            {

                string query = "SELECT * FROM [dbo].[Agent] WHERE [AgentId] = " + Convert.ToInt32(agentName.SelectedValue.ToString()) + "   ;";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                sqlCon.Open();

                using (saReader = cmd.ExecuteReader())
                {
                    if (saReader.Read())
                    {
                        companyName.Text = saReader["CompanyName"].ToString();//column name should be that you want to show on textbox

                    }
                    //saReader.Close();
                }
                ////sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // Close data reader object and database connection
                if (saReader != null)
                    saReader.Close();

                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }



        }

        private void addbutton_Click(object sender, EventArgs e)
        {


            if (quantity.Text != "" && productName.Text != "")
            {



                try
                {



                    String query = "INSERT INTO [dbo].[InventoryDetails] ([InventoryId],[ProductId],[InventoryQuantity],[Cost]) VALUES(@invetryId , @productid , @quantity ,@cost)";


                    using (SqlConnection sqlCon = new SqlConnection(conString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                        {
                            sqlCon.Open();
                            cmd.Parameters.AddWithValue("@invetryId", Convert.ToInt32(this.storeId.Text));
                            cmd.Parameters.AddWithValue("@productid", Convert.ToInt32(this.productName.SelectedValue.ToString()));
                            cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(this.quantity.Text));
                            cmd.Parameters.AddWithValue("@cost", Convert.ToDecimal(this.cost.Text));
                            int k = cmd.ExecuteNonQuery();
                            if (k > 0)
                            {
                                productIncrease();
                                MessageBox.Show("Inserted sucessfully");

                                totalBillAmount += Convert.ToDecimal(this.cost.Text);
                                totalBill.Text = totalBillAmount.ToString();
                                agentName.Enabled = false;
                                productName.Text = "";
                                quantity.Clear() ;
                                cost.Text = "";
                                productRate.Text = "";

                              

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


            else
            {
                MessageBox.Show("Fill All the data ");
            }






            inventoryDetailsLoad();
            inventoryDetailsDataGridView.Update();
            inventoryDetailsDataGridView.ClearSelection();
        }

        private void quantity_TextChanged(object sender, EventArgs e)
        {
            double num;
            // bool isNum = Double.TryParse(buyingRate.Text.Trim(), out num);
            try { 
            if (quantity.Text != "")
            {
                newCost = Convert.ToDecimal(productRate.Text.ToString()) * Convert.ToDecimal(quantity.Text.ToString());
                cost.Text = newCost.ToString();
            }
            //else
            //{
            //    //MessageBox.Show("Only Numbers");
            //    //    //quantity.Text = "";
            //    //    quantity.Clear();


            //}
            }
            catch
            {
                MessageBox.Show("Only Numbers");
                //quantity.Text = "";
                quantity.Clear();

            }

        }

        private void cashPaid_TextChanged(object sender, EventArgs e)
        {
            double num;
            // bool isNum = Double.TryParse(buyingRate.Text.Trim(), out num);
            try
            {
                if (cashPaid.Text != "" )
                {
                    string s = cashPaid.Text.ToString();
                    dueAmount = totalBillAmount - Convert.ToDecimal(s);
                    due.Text = dueAmount.ToString();
                }
                //else
                //{
                //    //MessageBox.Show("Only Numbers");
                //    //cashPaid.Clear();

                //}

            }
            catch
            {
                MessageBox.Show("Only Numbers");
                cashPaid.Clear();

            }

        }

        public void inventoryDetailsLoad()
        {

            try
            {



                // string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(conString);
                // INSERT INTO ([InventoryId],[ProductId],[Quantity],[Cost]) VALUES(@invetryId , @productid , @quantity , @cost)
                String query = @"SELECT [InventoryDetailsId] as 'Inventory Details ID',[ProductName] as 'Product Name', [InventoryQuantity] as 'Quantity', [Cost] as 'Cost', [SellingRate] as 'Selling Rate', [BuyingRate] as 'Buying Rate' FROM dbo.InventoryDetails JOIN dbo.Product On dbo.InventoryDetails.ProductId = dbo.Product.ProductId  WHERE [InventoryId]= " + Convert.ToInt32(this.storeId.Text) + "";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);


                DataTable data = new DataTable();
                data.Locale = System.Globalization.CultureInfo.InvariantCulture;
                sqlDataAdapter.Update(data);
                sqlDataAdapter.Fill(data);

                inventoryDetailsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                inventoryDetailsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                inventoryDetailsDataGridView.ColumnHeadersVisible = true;
                inventoryDetailsDataGridView.DataSource = data;
                inventoryDetailsDataGridView.Update();
                inventoryDetailsDataGridView.ClearSelection();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void delete_Click(object sender, EventArgs e)
        {



            foreach (DataGridViewRow row in inventoryDetailsDataGridView.Rows)


            {

                if (row.Selected)
                {
                    try
                    {



                        String query = "DELETE FROM [dbo].[InventoryDetails] WHERE [InventoryDetailsId]= " + row.Cells["Inventory Details ID"].Value.ToString() + ";";


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
            inventoryDetailsLoad();
            inventoryDetailsDataGridView.Update();
            inventoryDetailsDataGridView.ClearSelection();




        }

        private void Purchase_Click(object sender, EventArgs e)
        {


            if (cashPaid.Text != "" && totalBill.Text != "" )
            {

                if (Convert.ToDecimal(totalBill.Text) > Convert.ToDecimal(cashPaid.Text))
                 {
                    try
                    {



                        String query = "INSERT INTO [dbo].[Inventory] ([AgentId],[TotalBill],[CashPaid],[Due],[Date]) VALUES( @agentid , @totalbill ,@cashpaid,@due,@date)";


                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();
                                // cmd.Parameters.AddWithValue("@invetryid", Convert.ToInt32(this.storeId.Text));
                                cmd.Parameters.AddWithValue("@agentid", Convert.ToInt32(this.agentName.SelectedValue.ToString()));
                                cmd.Parameters.AddWithValue("@totalbill", Convert.ToInt32(this.totalBillAmount));
                                cmd.Parameters.AddWithValue("@cashpaid", Convert.ToDecimal(this.cashPaid.Text));
                                cmd.Parameters.AddWithValue("@due", Convert.ToDecimal(this.due.Text));
                                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                                int k = cmd.ExecuteNonQuery();
                                if (k > 0)
                                {

                                    MessageBox.Show("Purchase sucessfully");

                                    
                                    creditor();



                                }
                                else
                                {
                                    MessageBox.Show("Inserted Not Inserted");
                                }
                                sqlCon.Close();
                            }
                        }
                        agentName.Enabled = true;
                        productName.Text = "";
                        agentName.Text = "";
                        productRate.Text = "";
                        companyName.Text = "";
                        quantity.Clear();
                        due.Text = "";
                        cost.Text = "";
                        productRate.Text = "";
                        cashPaid.Text = "";
                        totalBill.Text = "";


                        if (this.inventoryDetailsDataGridView.DataSource != null)
                        {
                            this.inventoryDetailsDataGridView.DataSource = null;
                        }
                        else
                        {
                            this.inventoryDetailsDataGridView.Rows.Clear();
                        }
                        inventoryDetailsDataGridView.DataSource = null;
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                }
                else if ( Convert.ToDecimal(totalBill.Text) == Convert.ToDecimal(cashPaid.Text))

                {
                    try
                    {



                        String query = "INSERT INTO [dbo].[Inventory] ([AgentId],[TotalBill],[CashPaid],[Due],[Date]) VALUES( @agentid , @totalbill ,@cashpaid,@due,@date)";


                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();
                                // cmd.Parameters.AddWithValue("@invetryid", Convert.ToInt32(this.storeId.Text));
                                cmd.Parameters.AddWithValue("@agentid", Convert.ToInt32(this.agentName.SelectedValue.ToString()));
                                cmd.Parameters.AddWithValue("@totalbill", Convert.ToInt32(this.totalBillAmount));
                                cmd.Parameters.AddWithValue("@cashpaid", Convert.ToDecimal(this.cashPaid.Text));
                                cmd.Parameters.AddWithValue("@due", Convert.ToDecimal(this.due.Text));
                                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                                int k = cmd.ExecuteNonQuery();
                                if (k > 0)
                                {

                                    MessageBox.Show("Purchase sucessfully");

                               



                                }
                                else
                                {
                                    MessageBox.Show("Inserted Not Inserted");
                                }
                                sqlCon.Close();
                            }
                        }
                        agentName.Enabled = true;
                        productName.Text = "";
                        agentName.Text = "";
                        productRate.Text = "";
                        companyName.Text = "";
                        quantity.Clear();
                        due.Text = "";
                        cost.Text = "";
                        productRate.Text = "";
                        cashPaid.Text = "";
                        totalBill.Text = "";


                        if (this.inventoryDetailsDataGridView.DataSource != null)
                        {
                            this.inventoryDetailsDataGridView.DataSource = null;
                        }
                        else
                        {
                            this.inventoryDetailsDataGridView.Rows.Clear();
                        }
                        inventoryDetailsDataGridView.DataSource = null;
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }

                else
                {
                    MessageBox.Show("You Cannot Pay More than Total Ammount ");
                    cashPaid.Clear();
                    due.Text= "";
                }
            }


            else
            {
                MessageBox.Show("Fill All the data ");
            }

    
///


        }

        private void Reload_Click(object sender, EventArgs e)
        {
            inventoryDetailsLoad();
            inventoryDetailsDataGridView.Update();
            inventoryDetailsDataGridView.ClearSelection();
        }

        public void productIncrease()          
        {

            Decimal ins = Convert.ToDecimal(this.quantity.Text.ToString());
           

            try
            {

               

                String query = "UPDATE [dbo].[Product] SET  [Quantity]= [Quantity] + " + ins + "   WHERE [ProductId] =  " + Convert.ToInt32(productName.SelectedValue.ToString()) + "; ";


                using (SqlConnection sqlCon = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCon.Open();
                        

                        int k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            
                            MessageBox.Show("Product Updated sucessfully");


                        }
                        else
                        {
                            MessageBox.Show("Product Not Update");
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

        public void creditor()
        {

            try
            {



                String query = "INSERT INTO [dbo].[Creditor] ([CreditorDue],[Date],[AgentId]) VALUES (@CreditorDue , @Date , @AgentId)";


                using (SqlConnection sqlCon = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCon.Open();
                        cmd.Parameters.AddWithValue("@CreditorDue", Convert.ToDecimal(this.due.Text));
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@AgentId", Convert.ToInt32(this.agentName.SelectedValue.ToString()));
                        int k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                           
                            MessageBox.Show("You are on Debt");



                        }
                        else
                        {
                            MessageBox.Show("Debt Not working");
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



        public void getStoreId ()
        {
            SqlConnection sqlCon = new SqlConnection(conString);
            string a;
            SqlCommand Cmd1 = new SqlCommand("SELECT count(*) as InventoryId FROM dbo.[Inventory];", sqlCon);
            SqlCommand Cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.[Inventory]') + IDENT_INCR('dbo.[Inventory]') AS InventoryId;", sqlCon);

            try
            {
                sqlCon.Open();
                SqlDataReader reader = Cmd1.ExecuteReader();
                reader.Read();
                a = reader["InventoryId"].ToString();
                reader.Close();
                if (a == "0")
                {
                    sqlCon.Close();
                    storeId.Text = "1";              }
                else
                {
                    reader = Cmd.ExecuteReader();
                    reader.Read();
                    a = reader["InventoryId"].ToString();
                    sqlCon.Close();
                    storeId.Text = a;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        





    }


}

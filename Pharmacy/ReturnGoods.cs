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

namespace Pharmacy
{
    public partial class ReturnGoods : Form
    {
        Decimal quantity = 0;

        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";
        public ReturnGoods()
        {
            InitializeComponent();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            if (this.InventoryReturnDataGridView.DataSource != null)
            {
                this.InventoryReturnDataGridView.DataSource = null;
            }
            else
            {
                this.InventoryReturnDataGridView.Rows.Clear();
            }
            InventoryReturnDataGridView.DataSource = null;

            Return.Text = "";
        }

        private void InventoryReturnDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            productName.Text = InventoryReturnDataGridView.CurrentRow.Cells[2].Value.ToString();
            productQuantity.Text = InventoryReturnDataGridView.CurrentRow.Cells[4].Value.ToString();

        }




        public void productDecrease()
        {

            int ID = Convert.ToInt32(InventoryReturnDataGridView.CurrentRow.Cells[1].Value.ToString());
            try
            {



                String query = "UPDATE [dbo].[Product] SET  [Quantity]= [Quantity] - " + quantity + "   WHERE [ProductId] =  " + ID + "; ";


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

        private void SaveButton_Click(object sender, EventArgs e)
        {


            if (Return.Text != "")
            {
                double num;
                // bool isNum = Double.TryParse(buyingRate.Text.Trim(), out num);

                if (Double.TryParse(Return.Text.Trim(), out num))
                {


                    try
                    {

                        quantity = Convert.ToDecimal(this.Return.Text.ToString());
                        int intID = Convert.ToInt32(InventoryReturnDataGridView.CurrentRow.Cells[0].Value.ToString());

                        String query = "UPDATE [dbo].[InventoryDetails] SET [InventoryQuantity] = [InventoryQuantity] - @quantity  WHERE [InventoryDetailsId] =  " + intID + "; ";


                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();
                                cmd.Parameters.AddWithValue("@quantity", quantity);


                                int k = cmd.ExecuteNonQuery();
                                if (k > 0)
                                {
                                    MessageBox.Show("Update sucessfully");
                                    productDecrease();

                                }
                                else
                                {
                                    MessageBox.Show(" Not updated");
                                }
                                sqlCon.Close();

                            }
                        }

                        if (this.InventoryReturnDataGridView.DataSource != null)
                        {
                            this.InventoryReturnDataGridView.DataSource = null;
                        }
                        else
                        {
                            this.InventoryReturnDataGridView.Rows.Clear();
                        }
                        InventoryReturnDataGridView.DataSource = null;
                        this.Close();
                        Return.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                }
                else
                {
                    MessageBox.Show("Enter only number");
                    Return.Text = "";
                }

            }


            else
            {
                MessageBox.Show("Fill All the data ");
            }



        }
    }
}

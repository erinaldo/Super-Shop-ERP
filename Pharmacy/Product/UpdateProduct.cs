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
using Pharmacy;

namespace Pharmacy.Product
{
    public partial class UpdateProduct : Form
    {

        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

        public UpdateProduct()
        {
            InitializeComponent();
        }

        private void UpdateProduct_Load(object sender, EventArgs e)
        {
         

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            if (productId.Text != "" && productName.Text != "" && buyingRate.Text != "" && saleRate.Text != "")
            {
                double num;
                bool isNum = Double.TryParse(buyingRate.Text.Trim(), out num);

                if (Double.TryParse(buyingRate.Text.Trim(), out num) && Double.TryParse(saleRate.Text.Trim(), out num))
                {
                    try
                    {



                        String query = "UPDATE [dbo].[Product] SET [ProductName] = @productName , [ProductType] = @productType , [BuyingRate] = @buyingRate , [SellingRate]=  @saleRate WHERE [ProductID] =  "+ Convert.ToInt32(productId.Text) +"; ";


                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();
                                cmd.Parameters.AddWithValue("@productName", this.productName.Text.Trim());
                                cmd.Parameters.AddWithValue("@productType", this.productType.Text.Trim());
                                cmd.Parameters.AddWithValue("@buyingRate", Convert.ToDecimal(this.buyingRate.Text.Trim()));
                                cmd.Parameters.AddWithValue("@saleRate", Convert.ToDecimal(this.saleRate.Text.Trim()));
                                int k = cmd.ExecuteNonQuery();
                                if (k > 0)
                                {
                                    MessageBox.Show("Inserted sucessfully");


                                }
                                else
                                {
                                    MessageBox.Show("Inserted Not Inserted");
                                }
                                sqlCon.Close();
                            }
                        }

                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }



                }
                else
                {
                    MessageBox.Show("Enter Number only");


                    buyingRate.Text = "";
                    saleRate.Text = "";



                }
            }


            else
            {
                MessageBox.Show("Fill All the data ");
            }














           
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

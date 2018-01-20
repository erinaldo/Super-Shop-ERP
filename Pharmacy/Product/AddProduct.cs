using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using Pharmacy;


namespace Pharmacy.Product
{

     
    public partial class AddProduct : Form
    {
        

        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

        int generate;


        public AddProduct()
        {
            InitializeComponent();
        }

         
        

        private void saveButton(object sender, EventArgs e)
        {
           
            if (productId.Text !="" && productName.Text != "" && buyingRate.Text != "" && saleRate.Text != "")
            {
                double num;
               // bool isNum = Double.TryParse(buyingRate.Text.Trim(), out num);

                  if (Double.TryParse(buyingRate.Text.Trim(), out num) && Double.TryParse(saleRate.Text.Trim(), out num))
                {
                    try
                    {



                        String query = "INSERT INTO[dbo].[Product] ( [ProductName], [ProductType], [BuyingRate], [SellingRate],[Quantity],[SKU]) VALUES( @productName, @productType, @buyingRate, @saleRate,@quantity,@sku)";


                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();
                                cmd.Parameters.AddWithValue("@productName", this.productName.Text.Trim());
                                cmd.Parameters.AddWithValue("@productType", this.productType.Text.Trim());
                                cmd.Parameters.AddWithValue("@buyingRate", Convert.ToDecimal(this.buyingRate.Text.Trim()));
                                cmd.Parameters.AddWithValue("@saleRate", Convert.ToDecimal(this.saleRate.Text.Trim()));
                                cmd.Parameters.AddWithValue("@quantity", 0.00);
                                cmd.Parameters.AddWithValue("@sku", SKUTextbox.Text.ToString());
                                int k = cmd.ExecuteNonQuery();
                                if (k > 0)
                                {
                                    MessageBox.Show("Inserted sucessfully");

                                    productId.Text = "";
                                    productName.Text = "";
                                    productType.Text = "";
                                    buyingRate.Text = "";
                                    saleRate.Text = "";
                                    SKUTextbox.Text = "";
                  
                                    
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

        private void AddProduct_Load(object sender, EventArgs e)
        {
            try
            {

                 SqlConnection sqlCon = new SqlConnection(conString);
                    sqlCon.Open();
               
                String query = "SELECT IDENT_CURRENT('dbo.[Product]') + IDENT_INCR('dbo.[Product]') AS ProductId";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                productId.Text = reader["ProductId"].ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Generate(object sender, EventArgs e)
        {
            generate = Convert.ToInt32(productId.Text.ToString()) + 100000000;
            
           
            SKUTextbox.Text = generate.ToString();
        }
    }
}

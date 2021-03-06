﻿using System;
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
using System.IO;

namespace Pharmacy.Product
{

     
    public partial class AddProduct : Form
    {
        Decimal minQuantity;
        Decimal newCost;
        String imageLoc;
        byte[] image = null;
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

        int generate;


        public AddProduct()
        {
            InitializeComponent();
        }

         
        

        private void saveButton(object sender, EventArgs e)
        {
            double num;

            if (productId.Text !="" && productName.Text != "" && buyingRate.Text != "" && saleRate.Text != "")
            {
               
                // bool isNum = Double.TryParse(buyingRate.Text.Trim(), out num);
                
                    if (String.IsNullOrEmpty(MinimumQuantity.Text))
                    {
                        minQuantity = 0.0m;
                    }
                    else
                    {
                    if (Double.TryParse(MinimumQuantity.Text.Trim(), out num))
                    {
                        minQuantity = Convert.ToDecimal(this.MinimumQuantity.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show("Enter Number Only");
                        MinimumQuantity.Text = "";
                    }
                   }
               

                if (Double.TryParse(buyingRate.Text.Trim(), out num) && Double.TryParse(saleRate.Text.Trim(), out num) && UnitCombox.Text != "" && Double.TryParse(vat.Text.Trim(), out num))
                {
                    try
                    {
                        String query = "INSERT INTO[dbo].[Product] ( [ProductName], [ProductType], [BuyingRate], [SellingRate],[Quantity],[SKU],[Unit],[MinimumQuantity],[Vat],[SaleWithVat],[ProductImage]) VALUES( @productName, @productType, @buyingRate, @saleRate,@quantity,@sku,@unit,@minQuantity,@vat,@salewithvat,@image)";


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
                                cmd.Parameters.AddWithValue("@Unit", UnitCombox.Text.ToString());
                                cmd.Parameters.AddWithValue("@minQuantity", minQuantity);
                                cmd.Parameters.AddWithValue("@vat", Convert.ToDecimal(this.vat.Text.Trim()));
                                cmd.Parameters.AddWithValue("@salewithvat", newCost);
                                // cmd.Parameters.AddWithValue("@image", image);
                                // cmd.Parameters.AddWithValue("@image", (image == null) ? (Object) DBNull.Value : image);
                                if (image != null)
                                    cmd.Parameters.AddWithValue("@image", SqlDbType.Image).Value = image;
                                else { 
                                SqlParameter imageParameter = new SqlParameter("@image", SqlDbType.Image);
                                imageParameter.Value = DBNull.Value;
                                cmd.Parameters.Add(imageParameter);
                                }


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
                                    UnitCombox.SelectedIndex = -1;
                                    MinimumQuantity.Text = "";
                                    vat.Text = "";
                                    vatsale.Text = "";
                                    logo.Image = null;
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
                    MinimumQuantity.Text = "";
                 


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

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

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

        private void vatTextChanged(object sender, EventArgs e)
        {
            double num;
            // bool isNum = Double.TryParse(buyingRate.Text.Trim(), out num);
            try
            {
                if (vat.Text != "")
                {
                    
                    newCost = Convert.ToDecimal(saleRate.Text.ToString()) + (Convert.ToDecimal(saleRate.Text.ToString()) * Convert.ToDecimal(vat.Text.ToString()) )/100;
                    vatsale.Text = newCost.ToString();
                }
                else
                {
                    vatsale.Clear();
                }
   
            }
            catch
            {
                MessageBox.Show("Only Numbers");
                //quantity.Text = "";
                vat.Clear();

            }
        }
    }
}

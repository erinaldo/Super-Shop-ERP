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

namespace Pharmacy.AgentList
{
    public partial class UpdateAgent : Form
    {


        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

        public UpdateAgent()
        {
            InitializeComponent();
        }

        private void UpdateAgent_Load(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (addCompanyName.Text != "" && addAgentName.Text != ""  )
            {   
                    try
                    {



                        String query = "UPDATE [dbo].[Agent] SET [AgentName] = @agentName , [CompanyName]= @companyName ,  [PhoneNumber] = @phoneNumber   WHERE [AgentId] =  " + Convert.ToInt32(addAgentId.Text) + "; ";


                        using (SqlConnection sqlCon = new SqlConnection(conString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                            {
                                sqlCon.Open();
                                cmd.Parameters.AddWithValue("@agentName", this.addAgentName.Text.Trim());
                                cmd.Parameters.AddWithValue("@companyName", this.addCompanyName.Text.Trim());
                                cmd.Parameters.AddWithValue("@phoneNumber", this.addPhoneNumber.Text.Trim());

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
                MessageBox.Show("Fill All the data ");
            }



        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class AddAgent : Form
    {
        string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";
        public AddAgent()
        {
            InitializeComponent();
        }

        private void AddAgent_Load(object sender, EventArgs e)
        {
            try
            {

                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();

                String query = "SELECT IDENT_CURRENT('dbo.[Agent]') + IDENT_INCR('dbo.[Agent]') AS AgentId";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                addAgentId.Text = reader["AgentId"].ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }
        }

        private void saveButtonClick(object sender, EventArgs e)
        {


            if (addCompanyName.Text != "" && addAgentName.Text != "" )
            {
                

                
                    try
                    {



                        String query = "INSERT INTO [dbo].[Agent] ( [AgentName], [CompanyName], [PhoneNumber]) VALUES( @agentName, @companyName, @phoneNumber)";


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

                                    addAgentId.Text = "";
                                    addAgentName.Text = "";
                                    addCompanyName.Text = "";
                                    addPhoneNumber.Text = "";
                                    
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

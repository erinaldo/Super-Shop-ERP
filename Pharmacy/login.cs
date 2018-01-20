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

namespace Pharmacy
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            try
            {

                string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\DB.mdf;Integrated Security=True";

                SqlConnection sqlConn = new SqlConnection(con);

                String query = "SELECT * FROM dbo.[User] WHERE Username ='" + Textbox1.Text + "' AND Password ='" + Textbox2.Text + "'";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConn);
                DataTable data = new DataTable();
                sqlDataAdapter.Fill(data);
                if (data.Rows.Count == 1)
                {

                    dashboard dash = new dashboard();
                    dash.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Wrong username/ password !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Connect To The database");
            }
       
            








        }
    }
}

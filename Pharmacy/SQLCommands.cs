using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Pharmacy
{
    class SQLCommands
    {
        static string constng = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        SqlConnection Con = new SqlConnection(constng);
        public bool InsertUser(string Name, string Type, string Pass)
        {

            // create sql connection object.  Be sure to put a valid connection string
            // SqlConnection Con = new SqlConnection("connectionStrings");
            // create command object with SQL query and link to connection object
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[User] " +
        "(Username, UserType, PassWord, Date) " +
                "VALUES(@UserName, @UserType, @PassWord, @Date)",
        Con);

            // create your parameters
            Cmd.Parameters.Add("@Username", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@UserType", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@PassWord", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@Date", System.Data.SqlDbType.DateTime);

            // set values to parameters from textboxes
            Cmd.Parameters["@Username"].Value = Name;
            Cmd.Parameters["@UserType"].Value = Type;
            Cmd.Parameters["@PassWord"].Value = Pass;
            Cmd.Parameters["@Date"].Value = DateTime.Now;
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(string ID, string Name, string Type, string Pass)
        {
            SqlCommand Cmd = new SqlCommand("UPDATE dbo.[User] SET " +
            "Username='" + Name + "' , UserType='" + Type + "' , PassWord='" + Pass + "' , Date='" + DateTime.Now + "' " + "Where UserId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteUser(String ID)
        {
            SqlCommand Cmd = new SqlCommand("DELETE FROM dbo.[User] WHERE UserId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable FillUser()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT UserId as 'ID',Username as 'User Name',UserType as 'User Type',Password,Date FROM dbo.[User]";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }

           // Con.Close();
            return table;
        }


        //================================================================================================
        //================================================================================================
        //================================================================================================

        public bool InsertCom_Agents(string Name, string C_Name, string Phn)
        {

            // create sql connection object.  Be sure to put a valid connection string
            // SqlConnection Con = new SqlConnection("connectionStrings");
            // create command object with SQL query and link to connection object
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[Customer] " +
        "(CustomerName, CompanyName, PhoneNumber, Date) " +
                "VALUES(@CustomerName, @CompanyName, @PhoneNumber, @Date)",
        Con);

            // create your parameters
            Cmd.Parameters.Add("@CustomerName", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@CompanyName", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@PhoneNumber", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@Date", System.Data.SqlDbType.Date);

            // set values to parameters from textboxes
            Cmd.Parameters["@CustomerName"].Value = Name;
            Cmd.Parameters["@CompanyName"].Value = C_Name;
            Cmd.Parameters["@PhoneNumber"].Value = Phn;
            Cmd.Parameters["@Date"].Value = DateTime.Today;
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteC_Agents(String ID)
        {
            SqlCommand Cmd = new SqlCommand("DELETE FROM dbo.[Customer] WHERE CustomerId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateC_Agents(string ID, string Name, string C_Name, string Phn)
        {
            SqlCommand Cmd = new SqlCommand("UPDATE dbo.[Customer] SET " +
            "CustomerName='" + Name + "' , CompanyName='" + C_Name + "' , PhoneNumber='" + Phn + "' , Date='" + DateTime.Today + "' " + "Where CustomerId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public DataTable FillC_Agents()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT * FROM dbo.[Customer]";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }

            Con.Close();
            return table;
        }


        //=====================================================================================================
        //=====================================================================================================

        public bool InsertCustomer(string Name, string C_Name, string Phn)
        {

            // create sql connection object.  Be sure to put a valid connection string
            // SqlConnection Con = new SqlConnection("connectionStrings");
            // create command object with SQL query and link to connection object
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[Customer] " +
        "(CustomerName, CompanyName, PhoneNumber, Date) " +
                "VALUES(@CustomerName, @CompanyName, @PhoneNumber, @Date)",
        Con);

            // create your parameters
            Cmd.Parameters.Add("@CustomerName", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@CompanyName", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@PhoneNumber", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@Date", System.Data.SqlDbType.Date);

            // set values to parameters from textboxes
            Cmd.Parameters["@CustomerName"].Value = Name;
            Cmd.Parameters["@CompanyName"].Value = C_Name;
            Cmd.Parameters["@PhoneNumber"].Value = Phn;
            Cmd.Parameters["@Date"].Value = DateTime.Today;
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteCustomer(String ID)
        {
            SqlCommand Cmd = new SqlCommand("DELETE FROM dbo.[Customer] WHERE CustomerId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCustomer(string ID, string Name, string C_Name, string Phn)
        {
            SqlCommand Cmd = new SqlCommand("UPDATE dbo.[Customer] SET " +
            "CustomerName='" + Name + "' , CompanyName='" + C_Name + "' , PhoneNumber='" + Phn + "' , Date='" + DateTime.Today + "' " + "Where CustomerId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public DataTable FillCustomers()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT CustomerId as 'ID',CustomerName as 'Customer Name',CompanyName as 'Company Name',CustomerType as 'Customer Type',PhoneNumber as 'Phone Number',Date FROM dbo.[Customer]";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }

            Con.Close();
            return table;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        public string InsertCost(string Type, decimal Amount)
        {

            // create sql connection object.  Be sure to put a valid connection string
            // SqlConnection Con = new SqlConnection("connectionStrings");
            // create command object with SQL query and link to connection object
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[ExtraCost] " +
        "(CostType, Amount, Date) " +
                "VALUES(@CostType, @Amount, @Date)",
        Con);

            // create your parameters
            Cmd.Parameters.Add("@CostType", System.Data.SqlDbType.NChar);
            Cmd.Parameters.Add("@Amount", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@Date", System.Data.SqlDbType.Date);

            // set values to parameters from textboxes
            Cmd.Parameters["@CostType"].Value = Type;
            Cmd.Parameters["@Amount"].Value = Amount;
            Cmd.Parameters["@Date"].Value = DateTime.Today;
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public bool UpdateCost(string ID, string type, decimal amount)
        {
            SqlCommand Cmd = new SqlCommand("UPDATE dbo.[ExtraCost] SET " +
            "CostType='" + type + "' , Amount='" + amount + "', Date='" + DateTime.Today + "' " + "Where CostId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteCost(String ID)
        {
            SqlCommand Cmd = new SqlCommand("DELETE FROM dbo.[ExtraCost] WHERE CostId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable FillCost()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT CostId as 'ID',CostType as 'Cost Type',Amount,Date FROM dbo.[ExtraCost]";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }

            Con.Close();
            return table;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Wholesale
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DataTable FillProductAvl()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT ProductId as 'Product ID', ProductName as 'Product Name', ProductType as 'Product Type', SaleWithVat as 'Price', Quantity FROM dbo.[Product]";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }

           
            return table;
        }

        public bool DeleteWholelsaledetails(string ID)
        {
            SqlCommand Cmd = new SqlCommand("DELETE FROM dbo.[WholesaleDetails] WHERE WholesaleDeatilsId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable Fillwholesalel(string a)
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT ROW_NUMBER() OVER(ORDER BY WholesaleDetails.WholesaleDeatilsId) as 'No.', Product.ProductName as 'Product Name', WholesaleDetails.Quantity as Quantity, WholesaleDetails.UnitPrice as 'Unit Price', WholesaleDetails.Price as Total, WholesaleDetails.WholesaleDeatilsId as ID FROM dbo.[WholesaleDetails] INNER JOIN dbo.[Product] ON WholesaleDetails.ProductId = Product.ProductId where WholesaleDetails.WholesaleID =" + a+";";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }


            return table;
        }

        public string WtopID()
        {
            string a;
            SqlCommand Cmd1 = new SqlCommand("SELECT count(*) as ProductId FROM dbo.[Wholesale];", Con); ;
            SqlCommand Cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.[Wholesale]') + IDENT_INCR('dbo.[Wholesale]')   AS ProductId;", Con);
            try
            {
                Con.Open();
                SqlDataReader reader = Cmd1.ExecuteReader();
                reader.Read();
                a = reader["ProductId"].ToString();
                reader.Close();
                if (a == "0")
                {
                    Con.Close();
                    return "1";
                }
                else
                {
                    reader = Cmd.ExecuteReader();                   
                    reader.Read();
                    a = reader["ProductId"].ToString();
                    Con.Close();
                    return a;
                }

                
               

            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public bool InsertWholesale(string WID,string PID, string Quantity,string Unit, string Total)
        {
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[WholesaleDetails] VALUES(" + WID + "," + PID + "," + Quantity + "," + Unit + "," + Total + ");", Con);
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string InsertWholesaleMain(int Cid,decimal dis, decimal tb, decimal paid, decimal due)
        {
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[Wholesale] " +
        "(CustomerId, Discount, TotalBill, Paid, Due, Date) " +
                "VALUES(@CustomerId, @Discount, @TotalBill, @Paid,@Due,@Date)",
        Con);

            // create your parameters
            Cmd.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int);
            Cmd.Parameters.Add("@Discount", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@TotalBill", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@Paid", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@Due", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@Date", System.Data.SqlDbType.Date);

            // set values to parameters from textboxes
            Cmd.Parameters["@CustomerId"].Value = Cid;
            Cmd.Parameters["@Discount"].Value = dis;
            Cmd.Parameters["@TotalBill"].Value = tb;
            Cmd.Parameters["@Paid"].Value = paid;
            Cmd.Parameters["@Due"].Value = due;
            Cmd.Parameters["@Date"].Value = DateTime.Today;
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string WupdateProduct(decimal quan, string ID)
        {
            SqlCommand Cmd = new SqlCommand("update dbo.[Product] set Quantity = (Quantity - "+quan+") where ProductId ="+ ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public DataTable fillWcustomerDD()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT CustomerId,CustomerName FROM dbo.[Customer]";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }

            
            return table;

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////Retail
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string RtopID()
        {
            string a;
            SqlCommand Cmd1 = new SqlCommand("SELECT count(*) as ProductId FROM dbo.[Retail];", Con); ;
            SqlCommand Cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.[Retail]') + IDENT_INCR('dbo.[Retail]')   AS ProductId;", Con);
            try
            {
                Con.Open();
                SqlDataReader reader = Cmd1.ExecuteReader();
                reader.Read();
                a = reader["ProductId"].ToString();
                reader.Close();
                if (a == "0")
                {
                    Con.Close();
                    return "1";
                }
                else
                {
                    reader = Cmd.ExecuteReader();
                    reader.Read();
                    a = reader["ProductId"].ToString();
                    Con.Close();
                    return a;
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public bool InsertRetail(string WID, string PID, string Quantity, string Total)
        {
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[RetailDetails] VALUES(" + WID + "," + PID + "," + Quantity + "," + Total + ");", Con);
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteRetaildetails(string ID)
        {
            SqlCommand Cmd = new SqlCommand("DELETE FROM dbo.[RetailDetails] WHERE RetailDetailsId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable FillRetail(string a)
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT ROW_NUMBER() OVER(ORDER BY RetailDetails.RetailDetailsId) as 'No.', Product.ProductName as 'Product Name', RetailDetails.Quantity as Quantity,Product.SellingRate as 'Unit Price', RetailDetails.Price as Total, RetailDetails.RetailDetailsId as ID FROM dbo.[RetailDetails] INNER JOIN dbo.[Product] ON RetailDetails.ProductId = Product.ProductId where RetailDetails.RetailId =" + a + ";";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }


            return table;
        }

        public string InsertRetailMain(decimal dis, decimal tb, decimal paid, decimal due)
        {
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[Retail] " +
        "(Discount, TotalBill, Paid, Due, Date) " +
                "VALUES(@Discount, @TotalBill, @Paid,@Due,@Date)",
        Con);

            // create your parameters
            
            Cmd.Parameters.Add("@Discount", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@TotalBill", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@Paid", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@Due", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@Date", System.Data.SqlDbType.Date);

            // set values to parameters from textboxes
            
            Cmd.Parameters["@Discount"].Value = dis;
            Cmd.Parameters["@TotalBill"].Value = tb;
            Cmd.Parameters["@Paid"].Value = paid;
            Cmd.Parameters["@Due"].Value = due;
            Cmd.Parameters["@Date"].Value = DateTime.Today;
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return DateTime.Today.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////Debtor
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string InsertDebtor(int ID, decimal due)
        {
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.[Debtor] " +
        "(DebtorDue, CustomerId, Date) " +
                "VALUES(@DebtorDue, @CustomerId,@Date)",
        Con);

            // create your parameters

            Cmd.Parameters.Add("@DebtorDue", System.Data.SqlDbType.Decimal);
            Cmd.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int);
            Cmd.Parameters.Add("@Date", System.Data.SqlDbType.Date);

            // set values to parameters from textboxes

            Cmd.Parameters["@DebtorDue"].Value = due;
            Cmd.Parameters["@CustomerId"].Value = ID;
            Cmd.Parameters["@Date"].Value = DateTime.Today;
            try
            {
                // open sql connection
                Con.Open();

                // execute the query and return number of rows affected, should be one
                int RowsAffected = Cmd.ExecuteNonQuery();

                // close connection when done
                Con.Close();
                return "Debted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public DataTable FillDebtor()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT Debtor.DebtorId as 'Debtor ID',Customer.CustomerName as 'Customer Name',Debtor.DebtorDue as 'Due', Debtor.Date as 'Date' FROM dbo.[Debtor] INNER JOIN dbo.[Customer] ON Debtor.CustomerId = Customer.CustomerId;";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }


            return table;
        }

        public bool updateDebtor(decimal Due, string ID)
        {
            SqlCommand Cmd = new SqlCommand("update dbo.[Debtor] set DebtorDue = " + Due + " where DebtorId =" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteDebtor(String ID)
        {
            SqlCommand Cmd = new SqlCommand("DELETE FROM dbo.[Debtor] WHERE DebtorId=" + ID + ";", Con);

            try
            {
                Con.Open();
                int RowsAffected = Cmd.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////profit Loss
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public decimal SumWCog(string date,string year,string month,int i)
        {
            decimal a;
            SqlConnection Con = new SqlConnection(constng);
            SqlCommand Cmd1 = new SqlCommand();
            if (i == 1)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.BuyingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Wholesale INNER JOIN dbo.WholesaleDetails ON Wholesale.WholesaleId = WholesaleDetails.WholesaleId WHERE Wholesale.Date = '" + date + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }//SqlCommand Cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.[Wholesale]') + IDENT_INCR('dbo.[Wholesale]')   AS ProductId;", Con);

            else if (i == 2)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.BuyingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Wholesale INNER JOIN dbo.WholesaleDetails ON Wholesale.WholesaleId = WholesaleDetails.WholesaleId WHERE YEAR(Wholesale.Date) = '" + year + "' AND MONTH(Wholesale.Date)= '" + month + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }

            else if (i == 3)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.BuyingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Wholesale INNER JOIN dbo.WholesaleDetails ON Wholesale.WholesaleId = WholesaleDetails.WholesaleId WHERE YEAR(Wholesale.Date) = '" + year + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }
            try
            {
                Con.Open();
                SqlDataReader reader = Cmd1.ExecuteReader();
                reader.Read();
                a = Convert.ToDecimal(reader["Profit"].ToString());
                reader.Close();
                Con.Close();

                return a;
            }
            catch (Exception ex)
            {
                 return 0;
               // return ex.ToString();
            }
        }

        public decimal SumWRev(string date, string year, string month, int i)
        {
            decimal a;
            SqlConnection Con = new SqlConnection(constng);
            SqlCommand Cmd1 = new SqlCommand();
            if (i == 1)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.SellingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Wholesale INNER JOIN dbo.WholesaleDetails ON Wholesale.WholesaleId = WholesaleDetails.WholesaleId WHERE Wholesale.Date = '" + date + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }//SqlCommand Cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.[Wholesale]') + IDENT_INCR('dbo.[Wholesale]')   AS ProductId;", Con);

            else if (i == 2)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.SellingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Wholesale INNER JOIN dbo.WholesaleDetails ON Wholesale.WholesaleId = WholesaleDetails.WholesaleId WHERE YEAR(Wholesale.Date) = '" + year + "' AND MONTH(Wholesale.Date)= '"+ month +"') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }

            else if (i == 3)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.SellingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Wholesale INNER JOIN dbo.WholesaleDetails ON Wholesale.WholesaleId = WholesaleDetails.WholesaleId WHERE YEAR(Wholesale.Date) = '" + year + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }

            try
            {
                Con.Open();
                SqlDataReader reader = Cmd1.ExecuteReader();
                reader.Read();
                a = Convert.ToDecimal(reader["Profit"].ToString());
                reader.Close();
                Con.Close();

                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public decimal SumRCog(string date, string year, string month, int i)
        {
            decimal a;
            SqlConnection Con = new SqlConnection(constng);

            SqlCommand Cmd1 = new SqlCommand();
            if (i == 1)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.BuyingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Retail INNER JOIN dbo.RetailDetails ON Retail.RetailId = RetailDetails.RetailId WHERE Retail.Date = '" + date + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }//SqlCommand Cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.[Wholesale]') + IDENT_INCR('dbo.[Wholesale]')   AS ProductId;", Con);

            else if (i == 2)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.BuyingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Retail INNER JOIN dbo.RetailDetails ON Retail.RetailId = RetailDetails.RetailId WHERE YEAR(Retail.Date) = '" + year + "' AND MONTH(Retail.Date)= '" + month + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }

            else if (i == 3)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.BuyingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Retail INNER JOIN dbo.RetailDetails ON Retail.RetailId = RetailDetails.RetailId WHERE YEAR(Retail.Date) = '" + year + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }
            try
            {
                Con.Open();
                SqlDataReader reader = Cmd1.ExecuteReader();
                reader.Read();
                a = Convert.ToDecimal(reader["Profit"].ToString());
                reader.Close();
                Con.Close();

                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public decimal SumRRev(string date, string year, string month, int i)
        {
            decimal a;
            SqlConnection Con = new SqlConnection(constng);

            SqlCommand Cmd1 = new SqlCommand();
            if (i == 1)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.SellingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Retail INNER JOIN dbo.RetailDetails ON Retail.RetailId = RetailDetails.RetailId WHERE Retail.Date = '" + date + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }//SqlCommand Cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.[Wholesale]') + IDENT_INCR('dbo.[Wholesale]')   AS ProductId;", Con);

            else if (i == 2)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.SellingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Retail INNER JOIN dbo.RetailDetails ON Retail.RetailId = RetailDetails.RetailId WHERE YEAR(Retail.Date) = '" + year + "' AND MONTH(Retail.Date)= '" + month + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }

            else if (i == 3)
            {
                Cmd1 = new SqlCommand("SELECT SUM((Product.SellingRate * quan)) AS Profit FROM dbo.Product INNER JOIN (SELECT ProductId,Quantity AS quan FROM dbo.Retail INNER JOIN dbo.RetailDetails ON Retail.RetailId = RetailDetails.RetailId WHERE YEAR(Retail.Date) = '" + year + "') Hal ON Product.ProductId = Hal.ProductId;", Con); ;
            }

            try
            {
                Con.Open();
                SqlDataReader reader = Cmd1.ExecuteReader();
                reader.Read();
                a = Convert.ToDecimal(reader["Profit"].ToString());
                reader.Close();
                Con.Close();

                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public decimal SumExtra(string date, string year, string month,int i)
        {
            decimal a;
            SqlConnection Con = new SqlConnection(constng);

            SqlCommand Cmd = new SqlCommand();

            if (i == 1)
            {
                Cmd = new SqlCommand("SELECT SUM(Amount) as Cost from dbo.ExtraCost Where Date = '" + date + "';", Con);
            }
            else if (i == 2)
            {
                Cmd = new SqlCommand("SELECT SUM(Amount) as Cost from dbo.ExtraCost Where YEAR(Date) = '" + year + "' AND MONTH(Date) = '" + month + "';", Con);
            }
            else if (i == 3)
            {
                Cmd = new SqlCommand("SELECT SUM(Amount) as Cost from dbo.ExtraCost Where YEAR(Date) = '" + year + "';", Con);
            }
            try
            {
                Con.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                reader.Read();
                a = Convert.ToDecimal(reader["Cost"].ToString());
                reader.Close();
                Con.Close();
                return a;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////Refund
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public DataTable CheckWholesaleID()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT WholesaleId,CustomerId FROM dbo.[Wholesale]";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }


            return table;

        }

        public string FillRefund(string CID,string WID,string quantity,string PID)
        {
            SqlConnection Con = new SqlConnection(constng);
            SqlCommand Cmd = new SqlCommand("INSERT INTO dbo.Refund(WholesaleId,ProductId,Quantity,CustomerID,Date) VALUES("+WID+", "+PID+", "+ quantity + ", "+CID+",'"+DateTime.Today.ToString().Remove(9)+"'); ; ", Con);
            try
            {
                Con.Open();
                int k = Cmd.ExecuteNonQuery();
                Con.Close();
                return "";

            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public DataTable fillProductDD(string ID)
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT Product.ProductId as ProductId ,Product.ProductName as ProductName FROM dbo.[Product] INNER JOIN dbo.[WholesaleDetails] ON Product.ProductId = WholesaleDetails.ProductId WHERE WholesaleId = " + ID + ";";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }


            return table;
        }

        public DataTable RefundGriddFill()
        {
            DataTable table = new DataTable();

            //Con.Open();
            string sqlQuery = @"SELECT WholesaleId as 'Wholesale ID', ProductId as 'Product ID',Quantity,CustomerID as 'Customer ID',Date FROM dbo.[Refund]";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }


            return table;
        }

        public decimal returnquantity(string WID,string PID)
        {
            decimal a;
            SqlConnection Con = new SqlConnection(constng);

            SqlCommand Cmd = new SqlCommand();         
            Cmd = new SqlCommand("SELECT Quantity FROM dbo.WholesaleDetails WHERE WholesaleId = "+WID+" and ProductId = "+PID+";", Con);
            
            try
            {
                Con.Open();
                SqlDataReader reader = Cmd.ExecuteReader();
                reader.Read();
                a = Convert.ToDecimal(reader["Quantity"].ToString());
                reader.Close();
                Con.Close();
                return a;

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public string refillQuantity(string PID, string quantity)
        {
            decimal a;
            SqlConnection Con = new SqlConnection(constng);

            SqlCommand Cmd = new SqlCommand();
            Cmd = new SqlCommand("Update dbo.[Product] SET Quantity = Quantity + "+quantity+" WHERE ProductId = "+PID+";", Con);
            try
            {
                Con.Open();
                int k = Cmd.ExecuteNonQuery();
                Con.Close();
                return "";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        ////Barcode
        //////////////////////////////////////////////////////////////
        public DataTable FindProductSku(string sku)
        {
            DataTable dt = new DataTable();
            SqlConnection Con = new SqlConnection(constng);

            string sqlQuery = "SELECT * FROM dbo.[Product] WHERE SKU = '" +sku+ "'" ;

            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(dt);
            }
            return dt;
        }

    }

}


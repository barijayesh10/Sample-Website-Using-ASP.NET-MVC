using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace Test_Sample_Website
{
    public class Class1
    {
        SqlConnection conn = new SqlConnection(@"Data Source=JAYESH-BARI;Initial Catalog=JayCom;Integrated Security=True");
        SqlDataReader rdr; DataTable dataTable;

        public int Register(string user, string pass, string con_pass)
        {
            int a = 0;
            while (a <= 0)
            {
                try
                {
                    rdr = duplicate(user, pass);
                    if (rdr.HasRows == true)
                    {
                        return 1;
                    }
                    else
                    {
                        rdr.Close();
                        string query = "INSERT INTO Client(reg_username, reg_password, reg_con_pssword) VALUES (@reg_username, @reg_password, @reg_con_pssword)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("reg_username", user);
                            cmd.Parameters.AddWithValue("reg_password", pass);
                            cmd.Parameters.AddWithValue("reg_con_pssword", con_pass);
                            try
                            {
                                cmd.CommandTimeout = 0;
                                cmd.ExecuteNonQuery();
                            }
                            catch
                            {
                                return 2;
                            }
                        }
                    }
                }
                catch
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return 0;
        }

        public int Login(string user, string pass)
        {
            int a = 0;
            while (a <= 0)
            {
                try
                {
                    string query = "SELECT * From Client WHERE reg_username='" + user + "' AND reg_password = '" + pass + "'";
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 0;
                        rdr = cmd.ExecuteReader();
                    }
                    a++;
                }
                catch
                {
                    conn.Close();
                }                
            }
            if (rdr.HasRows == true)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public SqlDataReader duplicate(string user, string pass)
        {
            int a = 0;
            while (a <= 0)
            {
                try
                {
                    string query = "SELECT * From Client WHERE reg_username='" + user + "'";
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 0;
                        rdr = cmd.ExecuteReader();
                    }
                    a++;
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
            return rdr;
        }

        public SqlDataReader getcountry()
        {
            int a = 0;
            while (a <= 0)
            {
                try
                {
                    string query = "SELECT * From Country";
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 0;
                        rdr = cmd.ExecuteReader();
                    }
                    a++;
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
            return rdr;
        }

        public DataTable getmember()
        {
            int a = 0;
            while(a <= 0)
            {
                try
                {
                    dataTable = new DataTable();
                    string query = "SELECT * from Client";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    a++;
                }
                catch
                {
                    conn.Close();
                }
            }
            
            return dataTable;
        }

        public int Contact(string name, string email, string number, string message)
        {
            int a = 0;
            while (a <= 0)
            {
                try
                {
                    string query = "INSERT INTO Contact(name, email, number, message) VALUES (@name, @email, @number, @message)";
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("name", name);
                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("number", number);
                        cmd.Parameters.AddWithValue("message", message);
                        try
                        {
                            cmd.CommandTimeout = 0;
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            return 2;
                        }
                    }
                    a++;
                }
                catch
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return 0;
        }
    }
}
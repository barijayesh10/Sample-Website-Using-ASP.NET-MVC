using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Test_Sample_Website
{
    public class Class1
    {
        SqlConnection conn = new SqlConnection(@"Data Source=JAYESH-BARI;Initial Catalog=JayCom;Integrated Security=True");
        SqlDataReader rdr;

        public int Register(string user, string pass, string con_pass)
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
                conn.Close();
                conn.Dispose();
                return 0;
            }
        }

        public int Login(string user, string pass)
        {
            string query = "SELECT * From Client WHERE reg_username='" + user + "' AND reg_password = '" + pass + "'";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 0;
                rdr = cmd.ExecuteReader();
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
            try
            {
                string query = "SELECT * From Client WHERE reg_username='" + user + "'";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 0;
                    rdr = cmd.ExecuteReader();
                }
            }
            catch (Exception)
            {

            }
            return rdr;
        }

        public int Contact(string name, string email, string number, string message)
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
            conn.Close();
            conn.Dispose();
            return 0;
        }
    }
}
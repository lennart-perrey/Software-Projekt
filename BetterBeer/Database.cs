using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
namespace BetterBeer
{
    public class Database
    {
        public static bool checkUser(String login, string password)
        {
            bool res = false;
            SqlConnection conn = new SqlConnection("server=goliath.wi.fh-flensburg.de;database=ws1819_spbier;user=ws1819_spbier;password = kpe_4827");
            conn.Open();

            SqlCommand myCommand = new SqlCommand("SELECT UserId FROM Benutzer where Login='" + login + "' AND password='" + password + "'", conn);
            SqlDataReader dataReader = myCommand.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.GetValue(0) != null)
                {
                    res = true;
                }
            }
            dataReader.Close();


            conn.Close();
            return res;
        }
    }
}

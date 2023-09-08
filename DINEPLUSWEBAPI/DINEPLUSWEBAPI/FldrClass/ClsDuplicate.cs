using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrClass
{
    public class ClsDuplicate
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataReader dr;

        public string CheckMain1(string strGUID)
        {
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand($"SELECT IC FROM tblMain1 WHERE GUID = '{strGUID}'", myconnection);
            dr = mycommand.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                string IC = dr["IC"].ToString();
                return IC;
            }
            dr.Close();
            myconnection.Close();
            return "0";
        }
        public string DeleteMain2(string strIC)
        {
            string connectionString = new ClsGetConnection().PlsConnect();

            using (SqlConnection myconnection = new SqlConnection(connectionString))
            {
                myconnection.Open();

                string sqlStatement = "DELETE FROM tblMain2 WHERE IC = @IC";

                using (SqlCommand mycommand = new SqlCommand(sqlStatement, myconnection))
                {
                    mycommand.Parameters.AddWithValue("@IC", strIC);
                    int rowsAffected = mycommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "0"; 
                    }
                    else
                    {
                        return "1"; 
                    }
                }
            }
        }

        public int CheckRowMain2(string strIC)
        {

            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand($"SELECT TOP 1 RowNum FROM tblMain2 WHERE IC = '{strIC}' ORDER BY RowNum DESC", myconnection);
            dr = mycommand.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                int rownum = int.Parse(dr["RowNum"].ToString()) + 1;
                return rownum;
            }
            dr.Close();
            myconnection.Close();
            return 0;
        }
    }
}

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrClass
{
    public class ClsAutoNumber
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataReader dr;
        public string plsnumber;
        public string VoucherAutoNum(string argvoucher, string argCNCode)
        {
            string pristrNumber;
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand($"SELECT Top 1 DocNum FROM tblMain1 WHERE CNCode='{argCNCode}' AND Voucher = '{argvoucher}' AND (ISNUMERIC(DocNum) = 1) ORDER BY DocNum DESC", myconnection);
            dr = mycommand.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                int intAutoNumber = int.Parse(dr["DocNum"].ToString()) + 1;
                pristrNumber = Convert.ToString(intAutoNumber).PadLeft(7, '0');
                dr.Close();
            }
            else
            {
                pristrNumber = "0000001";
            }
            myconnection.Close();
            return pristrNumber;
        }
        public int GetLastRow(string strIC)
        {
            using (SqlConnection myconnection = new SqlConnection(new ClsGetConnection().PlsConnect()))
            {
                myconnection.Open();

                using (SqlCommand mycommand = new SqlCommand($"SELECT MAX(RowNum) FROM tblMain2 WHERE IC = '{strIC}'", myconnection))
                {
                    int row = Convert.ToInt32(mycommand.ExecuteScalar());
                    return row;
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DINEPLUSWEBAPI.FldrClass;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class FrancisAutoNumController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataReader dr;

        [HttpGet]
        [Route("API/WEBAPI/AutoNumber/GetAutoNumProduct")]
        public string GetProductAutoNum()
        {
            string pristrNumber;
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand($"SELECT Top 1 StockNumber FROM tblEntryProducts ORDER BY StockNumber DESC", myconnection);
            dr = mycommand.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                int intAutoNumber = int.Parse(dr["StockNumber"].ToString()) + 1;
                pristrNumber = Convert.ToString(intAutoNumber).PadLeft(5, '0');
                dr.Close();
            }
            else
            {
                pristrNumber = "00001";
            }
            myconnection.Close();
            return pristrNumber;
        }

        [HttpGet]
        [Route("API/WEBAPI/AutoNumber/GetAutoNumCategory")]
        public string GetCategoryAutoNum()
        {
            string pristrNumber;
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand($"SELECT Top 1 CatCode FROM tblEntryCategory ORDER BY CatCode DESC", myconnection);
            dr = mycommand.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                int intAutoNumber = int.Parse(dr["CatCode"].ToString()) + 1;
                pristrNumber = Convert.ToString(intAutoNumber).PadLeft(2, '0');
                dr.Close();
            }
            else
            {
                pristrNumber = "01";
            }
            myconnection.Close();
            return pristrNumber;
        }

        [HttpGet]
        [Route("API/WEBAPI/AutoNumber/GetAutoNumTable")]
        public string GetTableAutoNum()
        {
            string pristrNumber;
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand($"SELECT Top 1 TableCode FROM tblEntryTables ORDER BY TableCode DESC", myconnection);
            dr = mycommand.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                int intAutoNumber = int.Parse(dr["TableCode"].ToString()) + 1;
                pristrNumber = Convert.ToString(intAutoNumber).PadLeft(2, '0');
                dr.Close();
            }
            else
            {
                pristrNumber = "01";
            }
            myconnection.Close();
            return pristrNumber;
        }

        [HttpGet]
        [Route("API/WEBAPI/AutoNumber/GetAutoNumName")]
        public string GetNameAutoNum()
        {
            string pristrNumber;
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand($"SELECT Top 1 ControlNo FROM tblEntryName ORDER BY ControlNo DESC", myconnection);
            dr = mycommand.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                int intAutoNumber = int.Parse(dr["ControlNo"].ToString()) + 1;
                pristrNumber = Convert.ToString(intAutoNumber).PadLeft(3, '0');
                dr.Close();
            }
            else
            {
                pristrNumber = "001";
            }
            myconnection.Close();
            return pristrNumber;
        }
    }
}

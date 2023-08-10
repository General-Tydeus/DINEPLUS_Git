using DINEPLUSWEBAPI.FldrClass;
using DINEPLUSWEBAPI.FldrModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class InsertController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlCommand mycommand2;
        SqlDataReader dr;
        int latestRowNum;

        [HttpPost]
        [Route("API/DINEPLUSWEBAPI/Voucher/InsertMain2Additional")]
        public string InsertProduct(List<ModeltblMain2> modelMain2)
        {
            try
            {
                if (modelMain2.Count > 0)
                {
                    foreach (var vartblMain2 in modelMain2)
                    {
                        latestRowNum = new ClsAutoNumber().GetLastRow(vartblMain2.IC);

                        myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                        myconnection.Open();

                        string sqlstatement2 = "INSERT INTO tblMain2 (IC, StockNumber,  PIn, POut, UP, Cost, Discount,  OrderTime, RowNum) " +
                                                "Values (@_IC, @_StockNumber, @_PIn, @_POut, @_UP, @_Cost, @_Discount,  @_OrderTime, @_RowNum)";
                        mycommand2 = new SqlCommand(sqlstatement2, myconnection);
                        mycommand2.Parameters.Add("_IC", SqlDbType.VarChar).Value = vartblMain2.IC;
                        mycommand2.Parameters.Add("_StockNumber", SqlDbType.VarChar).Value = vartblMain2.StockNumber;
                        mycommand2.Parameters.Add("_PIn", SqlDbType.Money).Value = vartblMain2.PIn;
                        mycommand2.Parameters.Add("_POut", SqlDbType.Money).Value = vartblMain2.POut;
                        mycommand2.Parameters.Add("_UP", SqlDbType.Money).Value = vartblMain2.UP;
                        mycommand2.Parameters.Add("_Cost", SqlDbType.Money).Value = vartblMain2.Cost;
                        mycommand2.Parameters.Add("_Discount", SqlDbType.Money).Value = vartblMain2.Discount;
                        mycommand2.Parameters.Add("_OrderTime", SqlDbType.VarChar).Value = vartblMain2.OrderTime;
                        mycommand2.Parameters.Add("_RowNum", SqlDbType.Int).Value = latestRowNum + 1;

                        mycommand2.ExecuteNonQuery();
                        myconnection.Close();

                    }
                    return "1"; // Success
                }
                return "2"; // wala unod model
            }
            catch (Exception ex)
            {
                return ex.Message; 
            }
        }

    }
}

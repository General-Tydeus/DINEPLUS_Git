using DINEPLUSWEBAPI.FldrClass;
using DINEPLUSWEBAPI.FldrModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class UpdateController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataReader dr;

        [HttpPut]
        [Route("API/DINEPLUSWEBAPI/UpdateTblStatus")]
        public string VoidVoucher(ModeltblMain1 MdlVoidMain11)
        {
            try
            {
                string SqlStatement = $"UPDATE tblEntryTables SET Status=@_Status, TableDocNum=@_TableDocNum WHERE TableCode = '{MdlVoidMain11.TableCode}'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_Status", SqlDbType.VarChar).Value = MdlVoidMain11.TableDesc;
                mycommand.Parameters.Add("_TableDocNum", SqlDbType.VarChar).Value = MdlVoidMain11.TableDocNum;
                mycommand.ExecuteNonQuery();
                myconnection.Close();
                return "1";
            }
         
             catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPut]
        [Route("API/DINEPLUSWEBAPI/UpdateMain1")]
        public string UpdateMain1(ModeltblMain1 ModeltblMain11)
        {
            try
            {
                string SqlStatement = $"UPDATE tblMain1 SET CashReceived=@_CashReceived, CAmount=@_CAmount, Serve=@_Serve WHERE IC = '{ModeltblMain11.IC}'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_Serve", SqlDbType.Bit).Value = 1;
                mycommand.Parameters.Add("_CashReceived", SqlDbType.Money).Value = ModeltblMain11.CashReceived;
                mycommand.Parameters.Add("_CAmount", SqlDbType.Money).Value = ModeltblMain11.CAmount;
                mycommand.ExecuteNonQuery();
                myconnection.Close();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}

using DINEPLUSWEBAPI.FldrClass;
using DINEPLUSWEBAPI.FldrModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DINEPLUSWEBAPI.FldrModel.ClsModelMain;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class ListController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataReader dr;
        string sqlStatement;
        string SqlSentenceView;

        [HttpGet]
        [Route("API/DINEPLUSWEBAPI/DINEPLUSWEBAPIEntry/GetProductList")]
        public IEnumerable<MdlProduct> GetProductList()
        {
            List<MdlProduct> MdlProductMSSQL = new List<MdlProduct>();

            string sqlStatement = $"SELECT * FROM tblEntryProducts";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                MdlProduct MdlProduct1 = new MdlProduct
                {
                    StockNumber = dr["StockNumber"].ToString(),
                    ProductDesc = dr["ProductDesc"].ToString(),
                    UnitMeasure = dr["UnitMeasure"].ToString(),
                    SellingPrice = double.Parse(dr["SellingPrice"].ToString()),
                    UCost = double.Parse(dr["UCost"].ToString()),
                    Active = (bool)dr["Active"],
            };
                MdlProductMSSQL.Add(MdlProduct1);
            }
            myconnection.Close();
            return MdlProductMSSQL;
        }
        [HttpGet]
        [Route("API/DINEPLUSWEBAPI/ListOthers/UserDetails")]
        public ViewtblDetailsUser GetUserDetails(string strURIUserName)
        {
            ViewtblDetailsUser ViewtblDetailsUser1 = new ViewtblDetailsUser();
            string SqlSentenceView = $"SELECT * FROM tblUser WHERE UserName='{strURIUserName}'";

            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlSentenceView, myconnection);
            dr = mycommand.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                ViewtblDetailsUser1.UserCode = dr["UserCode"].ToString();
                ViewtblDetailsUser1.UserName = dr["UserName"].ToString();
                ViewtblDetailsUser1.GroupCode = dr["GroupCode"].ToString();
                ViewtblDetailsUser1.CNCode = dr["CNCode"].ToString();
                dr.Close();
                myconnection.Close();
                return ViewtblDetailsUser1;
            }
            else
            {
                dr.Close();
                myconnection.Close();
                return ViewtblDetailsUser1;
            }
        }

        [HttpGet]
        [Route("API/DINEPLUSWEBAPI/DINEPLUSWEBAPIEntry/GetTblList")]
        public IEnumerable<MdlTables> GetTblList()
        {

            string sqlStatement = $"SELECT * FROM tblEntryTables";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                MdlTables MdlTables1 = new MdlTables
                {
                    TableCode = dr["TableCode"].ToString(),
                    TableDesc = dr["TableDesc"].ToString(),
                    Status = dr["Status"].ToString(),
                    LongStatus = new ClsStringHelper().LongStats(dr["Status"].ToString())
                };
                yield return MdlTables1;
            }
            myconnection.Close();
        }
    }
}

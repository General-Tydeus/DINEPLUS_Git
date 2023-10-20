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

            string sqlStatement = $"SELECT * FROM tblEntryProducts WHERE Active = 1 Order By ProductDesc ASC";
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
                    CatCode = dr["CatCode"].ToString(),

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

            string sqlStatement = $"SELECT * FROM tblEntryTables WHERE TableCode <> '00'";
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
                    LongStatus = new ClsStringHelper().LongStats(dr["Status"].ToString()),
                    TableDocNum = dr["TableDocNum"].ToString(),

                };
                yield return MdlTables1;
            }
            myconnection.Close();
        }
        [HttpGet]
        [Route("API/DINEPLUSWEBAPI/DINEPLUSWEBAPIEntry/GetCategoryList")]
        public IEnumerable<MdlCategory> GetCategoryList()
        {

            string sqlStatement = $"SELECT * FROM tblEntryCategory WHERE CatCode <> '00' Order By CatDesc ASC";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                MdlCategory MdlCategory1 = new MdlCategory
                {
                    CatCode = dr["CatCode"].ToString(),
                    CatDesc = dr["CatDesc"].ToString(),

                };
                yield return MdlCategory1;
            }
            myconnection.Close();
        }
        [HttpGet]
        [Route("API/DINEPLUSWEBAPI/DINEPLUSWEBAPIEntry/GetUserList")]
        public IEnumerable<MdlUser> GetUserList()
        {

            string sqlStatement = $"SELECT UserCode, UserName FROM tblUser WHERE Active = 1";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                MdlUser MdlCategory1 = new MdlUser
                {
                    UserCode = dr["UserCode"].ToString(),
                    UserName = dr["UserName"].ToString(),

                };
                yield return MdlCategory1;
            }
            myconnection.Close();
        }
        [HttpGet]
        [Route("API/DINEPLUSWEBAPI/DINEPLUSWEBAPIEntry/GetTblOrders")]
        public IEnumerable<ModeltblMain2> GetTblOrders(string strDocnum)
        {
            List<ModeltblMain2> ModeltblMain2MSSQL = new List<ModeltblMain2>();

            string sqlStatement = $"SELECT * FROM ViewOrdersSO WHERE IC = '{strDocnum}'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ModeltblMain2 ModeltblMain21 = new ModeltblMain2
                {
                    StockNumber = dr["StockNumber"].ToString(),
                    ProductDesc = dr["ProductDesc"].ToString(),
                    POut = double.Parse(dr["POut"].ToString()),
                    UP = double.Parse(dr["UP"].ToString()),
                    Cost = double.Parse(dr["Cost"].ToString()),
                    Discount = double.Parse(dr["Discount"].ToString()),
                    OrderTime = dr["OrderTime"].ToString(),
                    Totals = double.Parse(dr["UP"].ToString()) * double.Parse(dr["POut"].ToString()),
                };
                ModeltblMain2MSSQL.Add(ModeltblMain21);
            }
            myconnection.Close();
            return ModeltblMain2MSSQL;

        }
    }
}

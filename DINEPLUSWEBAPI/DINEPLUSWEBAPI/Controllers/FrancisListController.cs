using DINEPLUSWEBAPI.FldrModel;
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
    public class FrancisListController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataReader dr;
        string sqlStatement;
        [HttpGet]
        [Route("API/WEBAPI/Entry/GetCategory")]
        public IEnumerable<ModeltblCategory> GetCategoryList()
        {
            List<ModeltblCategory> ModeltblCategoryMSSQL = new List<ModeltblCategory>();

            string sqlStatement = $"SELECT CatCode, CatDesc FROM tblEntryCategory ORDER BY CatDesc";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ModeltblCategory ModeltblCategory1 = new ModeltblCategory
                {
                    CatCode = dr["CatCode"].ToString(),
                    CatDesc = dr["CatDesc"].ToString(),
                };
                ModeltblCategoryMSSQL.Add(ModeltblCategory1);
            }
            myconnection.Close();
            return ModeltblCategoryMSSQL;
        }

        [HttpGet]
        [Route("API/WEBAPI/Entry/GetProductListForEdit")]
        public IEnumerable<ModeltblProducts> GetProductListForEdit(string strURIParam, string strURIStockNumber)
        {
            //strURIParam= 1=All, 2=Specific
            List<ModeltblProducts> ModeltblProductsMSSQL = new List<ModeltblProducts>();

            if (strURIParam == "1")
            {
                sqlStatement = $"SELECT * FROM ViewtblEntryProducts";
            }
            else if (strURIParam=="2")
            {
                sqlStatement = $"SELECT * FROM ViewtblEntryProducts WHERE StockNumber='{strURIStockNumber}'";
            }
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ModeltblProducts ModeltblProducts1 = new ModeltblProducts
                {
                    StockNumber = dr["StockNumber"].ToString(),
                    ProductDesc = dr["ProductDesc"].ToString(),
                    UnitMeasure = dr["UnitMeasure"].ToString(),
                    CatCode=dr["CatCode"].ToString(),
                    CatDesc=dr["CatDesc"].ToString(),
                    SellingPrice = double.Parse(dr["SellingPrice"].ToString()),
                    UCost = double.Parse(dr["UCost"].ToString()),
                    Active = (bool)dr["Active"],
                };
                ModeltblProductsMSSQL.Add(ModeltblProducts1);
            }
            myconnection.Close();
            return ModeltblProductsMSSQL;
        }

        [HttpGet]
        [Route("API/WEBAPI/Entry/GetTable")]
        public IEnumerable<ModeltblTable> GetTableList()
        {
            List<ModeltblTable> ModeltblTableMSSQL = new List<ModeltblTable>();

            string sqlStatement = $"SELECT TableCode, TableDesc FROM tblEntryTables ORDER BY TableDesc";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ModeltblTable ModeltblTable1 = new ModeltblTable
                {
                    TableCode = dr["TableCode"].ToString(),
                    TableDesc = dr["TableDesc"].ToString(),
                };
                ModeltblTableMSSQL.Add(ModeltblTable1);
            }
            myconnection.Close();
            return ModeltblTableMSSQL;
        }

        [HttpGet]
        [Route("API/WEBAPI/Entry/GetName")]
        public IEnumerable<ModeltblEntryName> GetNameList()
        {
            List<ModeltblEntryName> ModeltblEntryNameMSSQL = new List<ModeltblEntryName>();
            string sqlStatement = $"SELECT ControlNo, CustName, Active FROM tblEntryName ORDER BY CustName";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ModeltblEntryName ModeltblEntryName1 = new ModeltblEntryName
                {
                    ControlNo = dr["ControlNo"].ToString(),
                    CustName = dr["CustName"].ToString(),
                    Active=(bool)dr["Active"],
                };
                ModeltblEntryNameMSSQL.Add(ModeltblEntryName1);
            }
            myconnection.Close();
            return ModeltblEntryNameMSSQL;
        }
    }
}

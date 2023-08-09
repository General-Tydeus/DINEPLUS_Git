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

        [HttpGet]
        [Route("API/WEBAPI/Entry/GetNameForvoucher")]
        public IEnumerable<ModeltblEntryName> GetNameListForVoucher()
        {
            List<ModeltblEntryName> ModeltblEntryNameMSSQL = new List<ModeltblEntryName>();
            string sqlStatement = $"SELECT ControlNo, CustName FROM tblEntryName WHERE Active=1 ORDER BY CustName";
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
                };
                ModeltblEntryNameMSSQL.Add(ModeltblEntryName1);
            }
            myconnection.Close();
            return ModeltblEntryNameMSSQL;
        }

        [HttpGet]
        [Route("API/WEBAPI/Report/GetInventorySummary")]
        public IEnumerable<ModelInvSum> GetInvSummary(string strURIAsOfDate)
        {
            //string strAsOfDate = DTURIAsOfDate.ToString("MM/dd/yyyy");
            List<ModelInvSum> ModelInvSumMSSQL = new List<ModelInvSum>();
            string sqlStatement = $"SELECT StockNumber, ProductDesc, SUM(AlsQty) AS AlsQty, SUM(AlsTotalCost) AS AlsTotalCost" +
                $" FROM ViewInventory WHERE TDate<='{strURIAsOfDate}' GROUP BY ProductDesc, StockNumber ORDER BY ProductDesc";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ModelInvSum ModelInvSum1 = new ModelInvSum
                {
                    StockNumber = dr["StockNumber"].ToString(),
                    ProductDesc = dr["ProductDesc"].ToString(),
                    AlsQty=double.Parse(dr["AlsQty"].ToString()),
                    AlsTotalCost=double.Parse(dr["AlsTotalCost"].ToString()),
                };
                ModelInvSumMSSQL.Add(ModelInvSum1);
            }
            myconnection.Close();
            return ModelInvSumMSSQL;
        }

        [HttpGet]
        [Route("API/WEBAPI/Report/GetCollectionSummary")]
        public IEnumerable<ModelCollection> GetColSummary(string strURIFromDate, string strURIToDate)
        {
            //string strAsOfDate = DTURIAsOfDate.ToString("MM/dd/yyyy");
            List<ModelCollection> ModelCollectionMSSQL = new List<ModelCollection>();
            string sqlStatement = $"SELECT RefDoc, TDate, CAmount" +
                $" FROM ViewCollection WHERE TDate Between '{strURIFromDate}' AND '{strURIToDate}'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ModelCollection ModelCollection1 = new ModelCollection
                {
                    RefDoc = dr["RefDoc"].ToString(),
                    TDate = DateTime.Parse(dr["TDate"].ToString()),
                    CAmount = double.Parse(dr["CAmount"].ToString()),
                };
                ModelCollectionMSSQL.Add(ModelCollection1);
            }
            myconnection.Close();
            return ModelCollectionMSSQL;
        }


        [HttpGet]
        [Route("API/WEBAPI/Report/GetSalesProduct")]
        public IEnumerable<ModelSalesProduct> GetSalesProduct(string strURIFromDate, string strURIToDate)
        {
            List<ModelSalesProduct> ModelSalesProductMSSQL = new List<ModelSalesProduct>();
            string sqlStatement = $"SELECT ProductDesc, SUM(TotalQty) AS TotalQty, SUM(TotalSales) AS TotalSales" +
                $" FROM ViewSalesProduct WHERE TDate Between '{strURIFromDate}' AND '{strURIToDate}' GROUP BY ProductDesc ";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ModelSalesProduct ModelSalesProduct1 = new ModelSalesProduct
                {
                    ProductDesc = dr["ProductDesc"].ToString(),
                    TotalQty = double.Parse(dr["TotalQty"].ToString()),
                    TotalSales = double.Parse(dr["TotalSales"].ToString()),
                };
                ModelSalesProductMSSQL.Add(ModelSalesProduct1);
            }
            myconnection.Close();
            return ModelSalesProductMSSQL;
        }

        [HttpGet]
        [Route("API/WEBAPI/Entry/GetNameOfUser")]
        public IEnumerable<ClsModeltblUser> GetNameOfUser(string strURILoginName)
        {
            List<ClsModeltblUser> ClsModeltblUserMSSQL = new List<ClsModeltblUser>();
            string sqlStatement = $"SELECT UserCode, UserName, CNCode FROM tblUser WHERE Active=1 AND UserName='{strURILoginName}'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlStatement, myconnection);
            dr = mycommand.ExecuteReader();
            while (dr.Read())
            {
                ClsModeltblUser ClsModeltblUser1 = new ClsModeltblUser
                {
                    UserCode = dr["UserCode"].ToString(),
                    UserName = dr["UserName"].ToString(),
                    CNCode=dr["CNCode"].ToString(),
                };
                ClsModeltblUserMSSQL.Add(ClsModeltblUser1);
            }
            myconnection.Close();
            return ClsModeltblUserMSSQL;
        }
        public class ModelInvSum
        {
            public string StockNumber { get; set; }
            public string ProductDesc { get; set; }
            public double AlsQty { get; set; }
            public double AlsTotalCost { get; set; }
        }

        public class ClsModeltblUser
        {
            public string UserCode { get; set; }
            public string UserName { get; set; }
            public string CNCode { get; set; }
        }
    }
}

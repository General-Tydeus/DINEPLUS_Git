using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DINEPLUSWEBAPI.FldrClass;
using System.Data;
using DINEPLUSWEBAPI.FldrModel;
using System.Net;

namespace DINEPLUSWEBAPI.Controllers
{
    
    [ApiController]
    public class FrancisInsertController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        
        [HttpPost]
        [Route("API/WebAPI/Insert/InserttblProduct")]
        public HttpResponseMessage InsertProduct(ModeltblProducts ModeltblProducts1)
        {
            string SqlStatement = "INSERT INTO tblEntryProducts (StockNumber, ProductDesc, UnitMeasure, SellingPrice, UCost, CatCode) Values (@_StockNumber, @_ProductDesc, @_UnitMeasure, @_SellingPrice, @_UCost, @_CatCode) ";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_StockNumber", SqlDbType.VarChar).Value = new FrancisAutoNumController().GetProductAutoNum();
            mycommand.Parameters.Add("_ProductDesc", SqlDbType.VarChar).Value = ModeltblProducts1.ProductDesc;
            mycommand.Parameters.Add("_UnitMeasure", SqlDbType.VarChar).Value = ModeltblProducts1.UnitMeasure;
            mycommand.Parameters.Add("_SellingPrice", SqlDbType.Money).Value = ModeltblProducts1.SellingPrice;
            mycommand.Parameters.Add("_UCost", SqlDbType.Money).Value = ModeltblProducts1.UCost;
            mycommand.Parameters.Add("_CatCode", SqlDbType.VarChar).Value = ModeltblProducts1.CatCode;
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("API/WebAPI/Insert/InserttblCategory")]
        public HttpResponseMessage InsertCategory(ModeltblCategory ModeltblCategory1)
        {
            string SqlStatement = "INSERT INTO tblEntryCategory (CatCode, CatDesc) Values (@_CatCode, @_CatDesc) ";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_CatCode", SqlDbType.VarChar).Value = new FrancisAutoNumController().GetCategoryAutoNum();
            mycommand.Parameters.Add("_CatDesc", SqlDbType.VarChar).Value = ModeltblCategory1.CatDesc;
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("API/WebAPI/Insert/InserttblTable")]
        public HttpResponseMessage InsertTable(ModeltblTable ModeltblTable1)
        {
            string SqlStatement = "INSERT INTO tblEntryTables (TableCode, TableDesc) Values (@_TableCode, @_TableDesc) ";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_TableCode", SqlDbType.VarChar).Value = new FrancisAutoNumController().GetTableAutoNum();
            mycommand.Parameters.Add("_TableDesc", SqlDbType.VarChar).Value = ModeltblTable1.TableDesc;
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("API/WebAPI/Insert/InserttblEntryName")]
        public HttpResponseMessage InsertEntryName(ModeltblEntryName ModeltblEntryName1)
        {
            string SqlStatement = "INSERT INTO tblEntryName (ControlNo, CustName) Values (@_ControlNo, @_CustName) ";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_ControlNo", SqlDbType.VarChar).Value = new FrancisAutoNumController().GetNameAutoNum();
            mycommand.Parameters.Add("_CustName", SqlDbType.VarChar).Value = ModeltblEntryName1.CustName;
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}

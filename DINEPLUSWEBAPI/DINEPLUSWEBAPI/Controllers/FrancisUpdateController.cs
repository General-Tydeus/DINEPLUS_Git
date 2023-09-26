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
using System.Net;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class FrancisUpdateController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        [HttpGet]
        [Route("API/WebAPI/Entry/ProductActive")]
        public HttpResponseMessage EditProfileProductActive(string strURIStockNumber, bool boolURIActive)
        {
            string SqlStatement = "UPDATE tblEntryProducts SET Active=@_Active WHERE StockNumber='" + strURIStockNumber + "'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_Active", SqlDbType.Bit).Value = boolURIActive;
            int n1 = mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("API/WebAPI/Entry/ProductServedDaily")]
        public HttpResponseMessage EditProfileProductServedDaily(string strURIStockNumber, bool boolURIServedDaily)
        {
            string SqlStatement = "UPDATE tblEntryProducts SET ServedDaily=@_ServedDaily WHERE StockNumber='" + strURIStockNumber + "'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_ServedDaily", SqlDbType.Bit).Value = boolURIServedDaily;
            int n1 = mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        [HttpGet]
        [Route("API/WebAPI/Entry/InActiveAllProduct")]
        public HttpResponseMessage ProductInActive()
        {
            string SqlStatement = "UPDATE tblEntryProducts SET Active=@_Active WHERE ServedDaily=0";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_Active", SqlDbType.Bit).Value = 0;
            int n1 = mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("API/WebAPI/Entry/CustNameActive")]
        public HttpResponseMessage EditCustNameActive(string strURIControlNo, bool boolURIActive)
        {
            string SqlStatement = "UPDATE tblEntryName SET Active=@_Active WHERE ControlNo='" + strURIControlNo + "'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_Active", SqlDbType.Bit).Value = boolURIActive;
            int n1 = mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        [HttpPut]
        [Route("API/WebAPI/Entry/UpdateProduct")]
        public HttpResponseMessage UpdateProduct(ModelField ModelField1)
        {
            if (ModelField1.WhatToUpdate == "1")
            {
                string SqlStatement = "UPDATE tblEntryProducts SET ProductDesc=@_ProductDesc WHERE StockNumber='"+ModelField1.StockNumber+"'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_ProductDesc", SqlDbType.VarChar).Value = ModelField1.MyFieldUpdate;
                mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            else if (ModelField1.WhatToUpdate == "2")
            {
                string SqlStatement = "UPDATE tblEntryProducts SET UnitMeasure=@_UnitMeasure WHERE StockNumber='" + ModelField1.StockNumber + "'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_UnitMeasure", SqlDbType.VarChar).Value = ModelField1.MyFieldUpdate;
                mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            else if (ModelField1.WhatToUpdate == "3")
            {
                string SqlStatement = "UPDATE tblEntryProducts SET CatCode=@_CatCode WHERE StockNumber='" + ModelField1.StockNumber + "'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_CatCode", SqlDbType.VarChar).Value = ModelField1.MyFieldUpdate;
                mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            else if (ModelField1.WhatToUpdate == "4")
            {
                string SqlStatement = "UPDATE tblEntryProducts SET SellingPrice=@_SellingPrice WHERE StockNumber='" + ModelField1.StockNumber + "'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_SellingPrice", SqlDbType.Money).Value = ModelField1.MyFieldUpdate;
                mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            else if (ModelField1.WhatToUpdate == "5")
            {
                string SqlStatement = "UPDATE tblEntryProducts SET UCost=@_UCost WHERE StockNumber='" + ModelField1.StockNumber + "'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_UCost", SqlDbType.VarChar).Value = ModelField1.MyFieldUpdate;
                mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        [HttpPut]
        [Route("API/WebAPI/Entry/UpdateCategory")]
        public HttpResponseMessage UpdateCategory(ModelField ModelField1)
        {
            string SqlStatement = "UPDATE tblEntryCategory SET CatDesc=@_CatDesc WHERE CatCode='" + ModelField1.StockNumber + "'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_CatDesc", SqlDbType.VarChar).Value = ModelField1.MyFieldUpdate;
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("API/WebAPI/Entry/UpdateTable")]
        public HttpResponseMessage UpdateTable(ModelField ModelField1)
        {
            string SqlStatement = "UPDATE tblEntryTables SET TableDesc=@_TableDesc WHERE TableCode='" + ModelField1.StockNumber + "'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_TableDesc", SqlDbType.VarChar).Value = ModelField1.MyFieldUpdate;
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("API/WebAPI/Entry/UpdateName")]
        public HttpResponseMessage UpdateName(ModelField ModelField1)
        {
            string SqlStatement = "UPDATE tblEntryName SET CustName=@_CustName WHERE ControlNo='" + ModelField1.StockNumber + "'";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_CustName", SqlDbType.VarChar).Value = ModelField1.MyFieldUpdate;
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public class ModelField
        {
            public string StockNumber { get; set; }
            public string MyFieldUpdate { get; set; }
            public string WhatToUpdate { get; set; }
        }
    }
}

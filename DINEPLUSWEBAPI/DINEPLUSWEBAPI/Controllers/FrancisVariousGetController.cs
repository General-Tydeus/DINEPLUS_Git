using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DINEPLUSWEBAPI.FldrClass;
using DINEPLUSWEBAPI.FldrModel;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class FrancisVariousGetController : ControllerBase
    {
        SqlConnection myconnection;

        [HttpPost]
        [Route("API/WEBAPI/Duplicate/CheckDuplicateProduct")]
        public ActionResult CheckIfDuplicateProduct(DuplicateData DuplicateData1)
        {
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            string CheckNoTransact = string.Format("SELECT Count(*) FROM tblEntryProducts WHERE ProductDesc='" + DuplicateData1.FldValueFieldName + "'");
            SqlCommand com = new SqlCommand(CheckNoTransact, myconnection);
            int CountData = int.Parse(com.ExecuteScalar().ToString());
            myconnection.Close();
            if (CountData > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("API/WEBAPI/Duplicate/CheckDuplicateCategory")]
        public ActionResult CheckIfDuplicateCategory(DuplicateData DuplicateData1)
        {
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            string CheckNoTransact = string.Format("SELECT Count(*) FROM tblEntryCategory WHERE CatDesc='" + DuplicateData1.FldValueFieldName + "'");
            SqlCommand com = new SqlCommand(CheckNoTransact, myconnection);
            int CountData = int.Parse(com.ExecuteScalar().ToString());
            myconnection.Close();
            if (CountData > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("API/WEBAPI/Duplicate/CheckDuplicateTable")]
        public ActionResult CheckIfDuplicateTable(DuplicateData DuplicateData1)
        {
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            string CheckNoTransact = string.Format("SELECT Count(*) FROM tblEntryTable WHERE TableDesc='" + DuplicateData1.FldValueFieldName + "'");
            SqlCommand com = new SqlCommand(CheckNoTransact, myconnection);
            int CountData = int.Parse(com.ExecuteScalar().ToString());
            myconnection.Close();
            if (CountData > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("API/WEBAPI/Duplicate/CheckDuplicateUserName")]
        public ActionResult CheckIfDuplicateUserName(DuplicateData DuplicateData1)
        {
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            string CheckNoTransact = string.Format("SELECT Count(*) FROM tblUser WHERE UserName='" + DuplicateData1.FldValueFieldName + "'");
            SqlCommand com = new SqlCommand(CheckNoTransact, myconnection);
            int CountData = int.Parse(com.ExecuteScalar().ToString());
            myconnection.Close();
            if (CountData > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}

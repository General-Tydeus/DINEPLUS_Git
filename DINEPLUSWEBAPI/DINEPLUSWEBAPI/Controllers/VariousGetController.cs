using DINEPLUSWEBAPI.FldrClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class VariousGetController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataReader dr;

        [Route("API/DINEPLUSWEBAPI/Various/GetDoorMessage")]
        public string GetDoorMsgResult(string strWAPIVoucher)
        {
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            string strDMsg = string.Format("SELECT DoorMsg FROM tblTDoor WHERE Voucher = '" + strWAPIVoucher + "'");
            SqlCommand comDMsg = new SqlCommand(strDMsg, myconnection);
            string GetDMsg = comDMsg.ExecuteScalar().ToString();
            myconnection.Close();
            return GetDMsg;
        }
    }
}

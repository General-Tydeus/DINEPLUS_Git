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
    public class LoginUserController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;
        SqlDataReader dr;

        [HttpGet]
        [Route("API/DINEPLUSWEBAPI/LoginUser/CheckUserPWord")]
        public string GetPWordResult(string strURILogInName, string strURIPWordLog)
        {

            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            string CheckNoTransact = string.Format($"SELECT Count(*) FROM tblUser WHERE UserName = '{strURILogInName}'");
            SqlCommand com = new SqlCommand(CheckNoTransact, myconnection);
            int CountData = int.Parse(com.ExecuteScalar().ToString());
            if (CountData > 0)
            {
                string strDBPassword = string.Format($"SELECT PWord FROM tblUser WHERE UserName = '{strURILogInName}'");
                SqlCommand comPWord = new SqlCommand(strDBPassword, myconnection);
                string GetPWord = comPWord.ExecuteScalar().ToString();
                if (new ClsHash().verifyMd5Hash(strURIPWordLog, GetPWord))
                {
                    myconnection.Close();
                    return "1";//OK
                }
                else
                {
                    myconnection.Close();
                    return "2";//Not ok
                }
            }
            else
            {
                myconnection.Close();
                return "2";//user doesnt exist
            }
        }
    }
}

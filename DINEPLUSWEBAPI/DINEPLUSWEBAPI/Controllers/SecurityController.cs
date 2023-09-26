using DINEPLUSWEBAPI.FldrModel;
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
using System.Security.Cryptography;
using System.Text;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class SecurityController : ControllerBase
    {
        SqlConnection myconnection;
        SqlCommand mycommand;

        [HttpPost]
        [Route("API/WEBAPI/WEBAPISecurity/InsertUser")]
        public HttpResponseMessage PostUser(ModeltblUser ModeltblUser1)
        {
            string SqlStatement = "INSERT INTO tblUser (UserCode, GroupCode, UserName, CompleteName, CNCode) Values (@_UserCode, @_GroupCode, @_UserName, @_CompleteName, @_CNCode) ";
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(SqlStatement, myconnection);
            mycommand.Parameters.Add("_UserCode", SqlDbType.VarChar).Value = new ClsAutoNumber().UserAutoNum();
            mycommand.Parameters.Add("_GroupCode", SqlDbType.VarChar).Value = ModeltblUser1.GroupCode;
            mycommand.Parameters.Add("_UserName", SqlDbType.VarChar).Value = ModeltblUser1.UserName;
            mycommand.Parameters.Add("_CompleteName", SqlDbType.VarChar).Value = ModeltblUser1.CompleteName;
            mycommand.Parameters.Add("_CNCode", SqlDbType.VarChar).Value = ModeltblUser1.CNCode;
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("API/WebAPI/Login/GetOldPWord")]
        public string GetOldPasswordOK(string pristrLogInName, string pristrPWordLog)
        {

            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();

            string strDBPassword = string.Format("SELECT PWord FROM tblUser WHERE UserName = '" + pristrLogInName + "'");
            SqlCommand comPWord = new SqlCommand(strDBPassword, myconnection);
            string GetPWord = comPWord.ExecuteScalar().ToString();
            myconnection.Close();

            if (string.IsNullOrEmpty(GetPWord) && pristrPWordLog != null)
            {
                return "3";//null record not null old password
            }
            else if (string.IsNullOrEmpty(GetPWord) && string.IsNullOrEmpty(pristrPWordLog))
            {
                return "1";//OK
            }
            else if (new ClsHash().verifyMd5Hash(pristrPWordLog, GetPWord))
            {
                return "1";//OK
            }
            else
            {
                return "2";//Not ok
            }
        }

        [HttpGet]
        [Route("API/WebAPI/Login/PutNewPWord")]
        public HttpResponseMessage PostNewPassword(string pristrLogInName, string pristrNewPWord)
        {
            string sqlstatement;
            sqlstatement = "UPDATE tblUser SET PWord=@_PWord WHERE UserName='" + pristrLogInName + "'";

            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();
            mycommand = new SqlCommand(sqlstatement, myconnection);
            mycommand.Parameters.Add("_PWord", SqlDbType.VarChar).Value = new ClsHash().getMd5Hash(pristrNewPWord);
            int n1 = mycommand.ExecuteNonQuery();
            myconnection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
   
    }
}

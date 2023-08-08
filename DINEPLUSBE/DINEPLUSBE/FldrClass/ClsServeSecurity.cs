using DINEPLUSBE.FldrModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUSBE.FldrClass
{
    class ClsServeSecurity
    {
        //private string pristrIPAddress = new ClsGetIPAddress().GetIPAddress();
     
        public async Task<string> CheckUserExists(string servstrUserNameLog)
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri(new ClsGetIPAddress().GetIPAddress() + "/API/WebAPI/Login/GetUserExist/?strURILogInName=" + servstrUserNameLog);

            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            //string strresultFinal = strresult.TrimStart('"').TrimEnd('"');
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
            //1=userExist
            //2=user not exist
        }

        public async Task<string> CheckSalesmanPWord(string servstrUserNameLog, string servstrPWordLog)
        {
                var clientGet = new HttpClient();
                clientGet.BaseAddress = new Uri(new ClsGetIPAddress().GetIPAddress() + "/API/SWMGLWebAPI/Login/CheckSalesmanPWord?pristrUserNameLog=" + servstrUserNameLog + "&pristrPWordLog=" + servstrPWordLog);
                HttpResponseMessage response = await clientGet.GetAsync("");
                string strresult = await response.Content.ReadAsStringAsync();
                string strresultFinal = strresult.Trim('"');
                return strresultFinal;
                //1=OK
                //2=Not OK
        }


        public async Task<string> CheckMemberOldPWord(string servstrUserNameLog, string servstrPWordLog)
        {
                var clientGet = new HttpClient();
                clientGet.BaseAddress = new Uri(new ClsGetIPAddress().GetIPAddress() + "/API/SWMGLWebAPI/Login/GetOldPWord?pristrLogInName=" + servstrUserNameLog + "&pristrPWordLog=" + servstrPWordLog);
                HttpResponseMessage response = await clientGet.GetAsync("");
                string strresult = await response.Content.ReadAsStringAsync();
                string strresultFinal = strresult.Trim('"');
                return strresultFinal;
        }

       
    }
}

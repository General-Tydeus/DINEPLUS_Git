using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUSBE.FldrClass
{
    class ClsLogData
    {
        public async Task<string> CheckUserPWord(string servstrLogInName, string servstrPWordLog)
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/LoginUser/CheckUserPWord?strURILogInName={servstrLogInName}&strURIPWordLog={servstrPWordLog}");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
            //1=OK
            //2=Not OK
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUS.FldrClass
{
    class ClsGetSomething
    {
        public async Task<string> GetCurrentVersion()
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/LoginUser/GetVersionNo");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            if (strresultFinal == "1")
            {
                return "yes";
            }
            else
            {
                return "no";
            }
            //return "Yes";
        }

        public async Task<string> GetDiscount()
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Various/GetDiscount");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
        }
    }
}

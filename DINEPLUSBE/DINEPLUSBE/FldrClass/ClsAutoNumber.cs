using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUSBE.FldrClass
{
    class ClsAutoNumber
    {
        public async Task<string> GetProductAutoNum()
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/AutoNumber/GetAutoNumProduct");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
        }

        public async Task<string> GetCategoryAutoNum()
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/AutoNumber/GetAutoNumCategory");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
        }

        public async Task<string> GetTableAutoNum()
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/AutoNumber/GetAutoNumTable");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
        }

        public async Task<string> GetNameAutoNum()
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/AutoNumber/GetAutoNumName");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
        }

        public async Task<string> GetVoucherAutoNum(string strVoucher, string strCNCode)
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/AutoNumber/GetVoucherAutoNum?strURIVoucher={strVoucher}&strURICNCode={strCNCode}");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
        }
    }
}

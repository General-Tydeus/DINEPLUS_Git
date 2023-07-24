using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUS.FldrClass
{
    class ClsAutoNum
    {

        public async Task<string> GetVoucherAutoNum(string strVoucher, string strCNCode)
        {
            var clientGet = new HttpClient();
            clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/AutoNumber/GetVoucherAutoNum?strUriVoucher={strVoucher}&strURICNCode={strCNCode}");
            HttpResponseMessage response = await clientGet.GetAsync("");
            string strresult = await response.Content.ReadAsStringAsync();
            string strresultFinal = strresult.Trim('"');
            return strresultFinal;
        }
    }
}

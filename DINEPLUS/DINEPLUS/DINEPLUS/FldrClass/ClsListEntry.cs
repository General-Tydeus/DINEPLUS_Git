using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DINEPLUS.FldrModel.ClsModelMain;

namespace DINEPLUS.FldrClass
{
    class ClsListEntry
    {
        public async Task<List<MdlProduct>> GetProductList()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/DINEPLUSWEBAPIEntry/GetProductList");
            var data = JsonConvert.DeserializeObject<List<MdlProduct>>(response);
            return data;
        }
    }
}

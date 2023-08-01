using DINEPLUS.FldrModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUS.FldrClass
{
    public class ClsListEntry
    {
        public async Task<List<MdlProduct>> GetProductList()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/DINEPLUSWEBAPIEntry/GetProductList");
            var data = JsonConvert.DeserializeObject<List<MdlProduct>>(response);
            return data;
        }

        public async Task<List<MdlTables>> GetTblList()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/DINEPLUSWEBAPIEntry/GetTblList");
            var data = JsonConvert.DeserializeObject<List<MdlTables>>(response);
            return data;
        }
    }
}

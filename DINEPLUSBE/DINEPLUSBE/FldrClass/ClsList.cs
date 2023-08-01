using DINEPLUSBE.FldrModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUSBE.FldrClass
{
    class ClsList
    {
        public async Task<List<ModeltblCategory>> GetCategory()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Entry/GetCategory");
            var data = JsonConvert.DeserializeObject<List<ModeltblCategory>>(response);
            return data;
        }

        public async Task<List<ModeltblProducts>> GettblProducts(string strParam, string strStockNumber)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Entry/GetProductListForEdit?strURIParam={strParam}&strURIStockNumber={strStockNumber}");
            var data = JsonConvert.DeserializeObject<List<ModeltblProducts>>(response);
            return data;
        }

        public async Task<List<ModeltblTable>> GetTable()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Entry/GetTable");
            var data = JsonConvert.DeserializeObject<List<ModeltblTable>>(response);
            return data;
        }

        public async Task<List<ModeltblEntryName>> GetName()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Entry/GetName");
            var data = JsonConvert.DeserializeObject<List<ModeltblEntryName>>(response);
            return data;
        }
    }
}

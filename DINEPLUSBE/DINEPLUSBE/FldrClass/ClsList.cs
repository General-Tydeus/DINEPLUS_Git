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

        public async Task<List<ModeltblProducts>> GetCategoryProducts(string strCatCode)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Entry/GetProductListCategory?strURICatCode={strCatCode}");
            var data = JsonConvert.DeserializeObject<List<ModeltblProducts>>(response);
            return data;
        }

        public async Task<List<ModeltblProducts>> GetCategoryProductsDynamic(string strCatCode)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Entry/GetProductListCategoryDynamic?strURICatCode={strCatCode}");
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

        public async Task<List<ModeltblEntryName>> GetNameForVoucher()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Entry/GetNameForVoucher");
            var data = JsonConvert.DeserializeObject<List<ModeltblEntryName>>(response);
            return data;
        }

        public async Task<List<ModelInvSum>> GetInvSum(string strAsOfDate)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Report/GetInventorySummary?strURIAsOfDate={strAsOfDate}");
            var data = JsonConvert.DeserializeObject<List<ModelInvSum>>(response);
            return data;
        }

        public async Task<List<ModelCollection>> GetColSum(string strFromDate, string strToDate)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Report/GetCollectionSummary?strURIFromDate={strFromDate}&strURIToDate={strToDate}");
            var data = JsonConvert.DeserializeObject<List<ModelCollection>>(response);
            return data;
        }

        public async Task<List<ModelSalesProduct>> GetSalesProduct(string strFromDate, string strToDate)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Report/GetSalesProduct?strURIFromDate={strFromDate}&strURIToDate={strToDate}");
            var data = JsonConvert.DeserializeObject<List<ModelSalesProduct>>(response);
            return data;
        }

        public async Task<List<ModeltblGroup>> GetGroupList()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/WEBAPISecurity/GroupList");
            var data = JsonConvert.DeserializeObject<List<ModeltblGroup>>(response);
            return data;
        }

    }
}

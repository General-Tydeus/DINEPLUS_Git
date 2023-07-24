using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DINEPLUS.FldrModel.ClsModelMain;

namespace DINEPLUS.FldrClass
{
    class ClsGetSecurity
    {
        public async Task<ViewtblDetailsUser> GetUserDetailsList(string strUserName)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/ListOthers/UserDetails?strURIUserName={strUserName}");
            var data = JsonConvert.DeserializeObject<ViewtblDetailsUser>(response);
            return data;
        }
    }
}

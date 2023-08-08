using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DINEPLUS.FldrModel;
using Newtonsoft.Json;
using SQLite;

namespace DINEPLUS.FldrServices
{
    public class ClsServeInsertLocal
    {
        static string loadStatus = "";
        public string LoadStatus { get { return loadStatus; } set { loadStatus = value; } }
        public readonly SQLiteAsyncConnection db;

        public ClsServeInsertLocal(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
        }

        public async Task<int> SaveLoginInfo(string strUserName1)
        {
            var varLoginInfo = await GetLoginInfor(strUserName1);
            try
            {
                await db.InsertAsync(varLoginInfo);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public async Task<ViewtblDetailsUser> GetLoginInfor(string strUserName)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/ListOthers/UserDetails?strURIUserName={strUserName}");
            LoadStatus = response;
            var data = JsonConvert.DeserializeObject<ViewtblDetailsUser>(response);
            return data;
        }
    }
}

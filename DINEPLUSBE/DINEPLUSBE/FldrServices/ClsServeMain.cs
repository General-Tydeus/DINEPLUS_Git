using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrModel;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUSBE.FldrServices
{
    public class ClsServeMain
    {
        static string loadStatus = "";
        public string LoadStatus { get { return loadStatus; } set { loadStatus = value; } }
        public readonly SQLiteAsyncConnection db;
        public ClsServeMain(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<ClsModeltblUser>().Wait();
            db.CreateTableAsync<ClsBluetoothPrinter>().Wait();

        }

        public async Task<List<ClsModeltblUser>> GetCurrentUser(string localstrUserName)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/Entry/GetNameOfUser?strURILoginName={localstrUserName}");
            var data = JsonConvert.DeserializeObject<List<ClsModeltblUser>>(response);
            return data;
        }
        public async Task<int> SaveGetCurrentUser(string localstrUserName1)//(/*List<User> user,List<Customer> cust,List<ProductMain> prod*/)
        {
            var varUser = await GetCurrentUser(localstrUserName1);
            try
            {
                await db.InsertAllAsync(varUser);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public Task<ClsModeltblUser> GetCurrentUser()
        {
            return db.Table<ClsModeltblUser>().OrderByDescending(x => x.UserName).FirstOrDefaultAsync();
        }

        public Task<string> CurrentBTPrinter()
        {
            return db.ExecuteScalarAsync<string>("SELECT PrinterName FROM ClsBluetoothPrinter");
        }

        //        var peopleWithNullAge = connection.Table<Person>().Where(p => p.Age == null).ToList();

        //foreach (var person in peopleWithNullAge)
        //{
        //    // Do something with people who have null age
        //}
        //    }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DINEPLUS.FldrClass;
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
        public async Task<int> SaveMain1(tblMain1Local ModeltblMain11)
        {
            int result = await db.InsertAsync(ModeltblMain11);

            if (result > 0)
            {
                return 0;  // Insert successful
            }
            else
            {
                return 1;
            }
        }
       
        public async Task<int> SaveMain2(tblMain2Local ModeltblMain11)
        {
            try
            {
                int result = await db.InsertAsync(ModeltblMain11);

                if (result > 0)
                {
                    return 0;  // Insert successful
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 2;
            }
        }


        public async Task<string> SaveProduct()
        {
            var products = await new ClsListEntry().GetProductList();
            try
            {
                if (products.Count > 0)
                {
                    await db.InsertAllAsync(products);
                    return "0";
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> SaveTable()
        {
            //var products = await new ClsListEntry().GetProductList();
            var table = await new ClsListEntry().GetTblList();
            try
            {
                if (table.Count > 0)
                {
                    await db.InsertAllAsync(table);
                    return "0";
                }
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> SaveDiscount()
        {
            string discountString = await new ClsGetSomething().GetDiscount();
            try
            {
                MdlDiscount discount = new MdlDiscount
                {
                    Discount = discountString
                };

                try
                {
                    await db.InsertAsync(discount);
                    return "0";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}

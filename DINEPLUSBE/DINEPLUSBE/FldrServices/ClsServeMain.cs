using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            //db.CreateTableAsync<ClsModelUser>().Wait();
            
        }

        
    }
}

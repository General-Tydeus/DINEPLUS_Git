using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DINEPLUS.FldrServices
{
    public class ClsServeMain
    {

        static string loadStatus = "";
        public string LoadStatus { get { return loadStatus; } set { loadStatus = value; } }
        public readonly SQLiteAsyncConnection db;
        public ClsServeMain(string dbPath)
        {

            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<MdlTables>().Wait();

        }



        //how to insert into sqlite table from list
        public async Task<int> SaveTblFunc()  
        {
            var varToTbl = await new ClsListEntry().GetTblList();
            try
            {
                await db.InsertAllAsync(varToTbl);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }


        public Task<List<MdlTables>> ImportTableList()
        {
            return db.QueryAsync<MdlTables>($"SELECT * FROM MdlTables ORDER BY TableCode");
        }





    }
}

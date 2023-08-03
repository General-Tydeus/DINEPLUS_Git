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
            db.CreateTableAsync<LocaltblMain1>().Wait();

        }

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

        public Task<int> SOMainCount()
        {
            return db.ExecuteScalarAsync<int>("SELECT COUNT(DocNum) FROM LocaltblMain1");
        }

        public Task<int> SaveClsModelSO1(LocaltblMain1 LocaltblMain11)
        {
            return db.InsertAsync(LocaltblMain11);
        }

        public void TableOccupied(string strTableCode)
        {
            db.ExecuteScalarAsync<MdlTables>($"UPDATE MdlTables SET Status='O', LongStatus='OCCUPIED' WHERE TableCode='{strTableCode}'");
        }

    }
}

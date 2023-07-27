using DINEPLUS.FldrModel;
using SQLite;

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




    }
}

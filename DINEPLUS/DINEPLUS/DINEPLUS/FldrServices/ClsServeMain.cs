using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //db.CreateTableAsync<LocaltblMain1>().Wait();
            //db.CreateTableAsync<LocaltblMain2>().Wait();
            db.CreateTableAsync<ViewtblDetailsUser>().Wait();
            db.CreateTableAsync<ClsBluetoothPrinter>().Wait();
            db.CreateTableAsync<MdlProduct>().Wait();
            db.CreateTableAsync<MdlDiscount>().Wait();
            db.CreateTableAsync<tblMain1Local>().Wait();
            db.CreateTableAsync<tblMain2Local>().Wait();
            db.CreateTableAsync<MdlCategory>().Wait();



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
        public Task<List<MdlProduct>> ImportProductList()
        {
            return db.QueryAsync<MdlProduct>($"SELECT * FROM MdlProduct");
            //return db.Table<MdlProduct>().OrderByDescending(x => x.RowNum).FirstOrDefaultAsync();
        }
        public Task<List<MdlCategory>> ImportCategoryList()
        {
            return db.QueryAsync<MdlCategory>($"SELECT * FROM MdlCategory");
            //return db.Table<MdlProduct>().OrderByDescending(x => x.RowNum).FirstOrDefaultAsync();
        }
        public Task<List<tblMain1Local>> ImportMain1()
        {
            return db.QueryAsync<tblMain1Local>($"SELECT * FROM tblMain1Local");
            //return db.Table<MdlProduct>().OrderByDescending(x => x.RowNum).FirstOrDefaultAsync();
        }
        public Task<List<tblMain2Local>> ImportMain2()
        {
            return db.QueryAsync<tblMain2Local>($"SELECT * FROM tblMain2Local");
            //return db.Table<MdlProduct>().OrderByDescending(x => x.RowNum).FirstOrDefaultAsync();
        }
        public Task<MdlDiscount> ImportDiscount()
        {
            return db.Table<MdlDiscount>().FirstOrDefaultAsync();
            //return db.Table<MdlProduct>().OrderByDescending(x => x.RowNum).FirstOrDefaultAsync();
        }
        public async Task<int> UpdateLocal(tblMain1Local ModeltblMainLocal1)
        {
            //await db.Table<ModeltblMainLocal>().DeleteAsync(x => x.FldGuid == strGUID);
            //await Task.Delay(300);
            //await db.InsertAsync(ModeltblMainLocal1);
            return await db.UpdateAsync(ModeltblMainLocal1);
        }
        public Task<int> SOMainCount()
        {
            return db.ExecuteScalarAsync<int>("SELECT COUNT(DocNum) FROM LocaltblMain1");
        }
        public async Task<string> UpdateMain1(string docnum, double CashReceived, double CAmount, string voucher, double disc)
        {
            try
            {
                await db.ExecuteScalarAsync<tblMain1Local>($"UPDATE tblMain1Local SET CashReceived='{CashReceived}', CAmount='{CAmount}', Discount='{disc}', Serve = 1 WHERE Voucher='{voucher}' AND DocNum='{docnum}'");

                return "0";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        string longstatus;

        public async Task<string> UpdateTable(string TableCode, string Status, string TableDocNum, string TableGUID)
        {
            if (Status == "A")
            {
                longstatus = "AVAILABLE";
            }
            else if (Status == "O")
            {
                longstatus = "OCCUPIED";

            }
            try
            {
                await db.ExecuteScalarAsync<MdlTables>($"UPDATE MdlTables SET Status='{Status}', LongStatus='{longstatus}', TableDocNum='{TableDocNum}', TableGUID='{TableGUID}' WHERE TableCode = '{TableCode}'");

                return "0";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


        }
        //public Task<int> SaveClsModelSO1(LocaltblMain1 LocaltblMain11)
        //{
        //    return db.InsertAsync(LocaltblMain11);
        //}
        //public Task<int> SaveClsModelSO2(List<LocaltblMain2> LocaltblMain21)
        //{
        //    return db.InsertAllAsync(LocaltblMain21);
        //}

        public void TableOccupied(string strTableCode, string myDocNum, string strGUID)
        {
            db.ExecuteScalarAsync<MdlTables>($"UPDATE MdlTables SET Status='O', LongStatus='OCCUPIED', TableDocNum='{myDocNum}', TableGUID='{strGUID}' WHERE TableCode='{strTableCode}'");
        }

        public async Task<bool> CheckTableAsync()
        {
            var result = await db.QueryAsync<MdlTables>("SELECT * FROM MdlTables WHERE Status = 'O'");

            return result.Any();
        }



        public Task<List<tblMain2Local>> LocaltblMainTwo(string myDocNum)
        {
            return db.QueryAsync<tblMain2Local>($"SELECT * FROM tblMain2Local WHERE IC='{myDocNum}' ORDER BY RowNum");
        }

        public Task<List<tblMain1Local>> localMain1Exp(string docnum, string voucher)
        {
            return db.QueryAsync<tblMain1Local>($"SELECT * FROM tblMain1Local WHERE DocNum = '{docnum}' AND Voucher = '{voucher}'");
        }
        
        public Task<List<tblMain2Local>> localMain2Exp()
        {
            return db.QueryAsync<tblMain2Local>($"SELECT * FROM tblMain2Local WHERE Exported = '0'");
        }
        public Task<List<tblMain2Local>> localMain2Save(string doc)
        {
            return db.QueryAsync<tblMain2Local>($"SELECT * FROM tblMain2Local WHERE DocNumLocal = '{doc}'");
        }

        public async void LocalMain1DeleteAsync(string findDocnum)
        {
           await db.ExecuteScalarAsync<tblMain1Local>($"UPDATE tblMain1Local SET Exported='1' WHERE DocNum='{findDocnum}'");
        }

        public async void LocalMain2DeleteAsync(string findDocnum)
        {
            await db.ExecuteScalarAsync<tblMain2Local>($"UPDATE tblMain2Local SET Exported='1' WHERE DocNumLocal='{findDocnum}'");
            //return await db.Table<tblMain2Local>().Where(x => x.DocNumLocal == findDocnum).DeleteAsync();
        }
        public async void DeleteOrder(string docnum, int rownum)
        {
            await db.ExecuteScalarAsync<tblMain2Local>($"Delete From tblMain2Local WHERE DocNumLocal='{docnum}' AND RowNum = '{rownum}'");
        }




        public Task<ViewtblDetailsUser> GetLogInInfo()
        {
            return db.Table<ViewtblDetailsUser>().OrderBy(x => x.UserCode).FirstOrDefaultAsync();
        }


        public Task<string> CurrentBTPrinter()
        {
            return db.ExecuteScalarAsync<string>("SELECT PrinterName FROM" +
            " ClsBluetoothPrinter");
        }

        public async Task<string> TDocNum()
        {
            var docnum = await db.Table<tblMain1Local>()
                     .OrderByDescending(record => record.DocNum)
                     .FirstOrDefaultAsync();

            if (docnum != null)
            {
                return (int.Parse(docnum.DocNum) + 1).ToString("D7");
            }

            return "0000001";
        } 
    }
}

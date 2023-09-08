using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DINEPLUS.FldrModel
{
    public class tblMain1Local
    {
        [PrimaryKey]
        public string GUID { get; set; }
        public string IC { get; set; }
        public string Voucher { get; set; }
        public string DocNum { get; set; }
        public string UserCode { get; set; }
        public string TDate { get; set; }
        public string Reference { get; set; }
        public string ControlNo { get; set; }
        public string Remarks { get; set; }
        public bool Void { get; set; }
        public string CNCode { get; set; }
        public double CashReceived { get; set; }
        public bool Serve { get; set; }
        public bool Exported { get; set; }
        public string TableCode { get; set; }
        public string TableDesc { get; set; }
        public string TableDocNum { get; set; }
        public double CAmount { get; set; }
        public double Discount { get; set; }
        public string OrderTime { get; set; }

    }





    public class tblMain2Local
    {
        [PrimaryKey, AutoIncrement]
        public int RowNum { get; set; }
        public string IC { get; set; }
        public string StockNumber { get; set; }
        public string ProductDesc { get; set; }

        public double PIn { get; set; }
        public double POut { get; set; }
        public double UP { get; set; }
        public double Cost { get; set; }
        public double Discount { get; set; }
        public double Totals { get; set; }
        public string OrderTime { get; set; }
        public string DocNumLocal { get; set; }
        public bool Exported { get; set; }


    }



}

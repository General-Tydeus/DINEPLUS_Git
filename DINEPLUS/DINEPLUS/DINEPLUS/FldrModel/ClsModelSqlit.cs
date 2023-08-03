using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DINEPLUS.FldrModel
{
    public class LocaltblMain1
    {
        [PrimaryKey]
        [AutoIncrement]
        public int RNMain1 { get; set; }
        public string IC { get; set; }
        public string Voucher { get; set; }
        public string DocNum { get; set; }
        public string UserCode { get; set; }
        public DateTime TDate { get; set; }
        public string Reference { get; set; }
        public string ControlNo { get; set; }
        public string Remarks { get; set; }
        public string CNCode { get; set; }
        public string TableCode { get; set; }
        public double CAmount { get; set; }
        public string TableDesc { get; set; }



        public double CashReceived { get; set; }
        public bool Void { get; set; } = false;
        public bool Payed { get; set; } = false;
    }
}

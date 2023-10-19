using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DINEPLUS.FldrModel
{
    public class DeviceInfo
    {
        public string Title { get; set; }
        public string MacAddress { get; set; }
    }
    public class ClsBluetoothPrinter
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string PrinterName { get; set; }
    }
    public class Model123
    {
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
        public int RowNum { get; set; }
        public string StockNumber { get; set; }
        public string ProductDesc { get; set; }

        public double PIn { get; set; }
        public double POut { get; set; }
        public double UP { get; set; }
        public double Cost { get; set; }
        public string DocNumLocal { get; set; }
        public string UnitMeasure { get; set; }
        public double SellingPrice { get; set; }
        public double UCost { get; set; }
        public bool Active { get; set; }
        public double Qty { get; set; } = 0;
        public double Totals { get; set; } = 0;
        public string CatCode { get; set; }
    }
}

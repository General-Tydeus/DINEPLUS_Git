using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DINEPLUSBE.FldrModel
{
    
    public class ViewtblDetailsUser
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string GroupCode { get; set; }
        public string CNCode { get; set; }
        public string CompleteName { get; set; }
    }
    public class ModeltblMain2PD
    {
        public string StockNumber { get; set; }
        public string ProductDesc { get; set; }
        public double PIn { get; set; }
        public double UCost { get; set; }
        public int RowNum { get; set; }
        public double Total { get; set; }
    }

    public class ModeltblMain2AS
    {
        public string StockNumber { get; set; }
        public string ProductDesc { get; set; }
        public double PIn { get; set; }
        public double POut { get; set; }
        public double UCost { get; set; }
        public int RowNum { get; set; }
        public double Total { get; set; }
        public double Qty { get; set; }
    }

    public class ModeltblMain1
    {
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
        public string TableCode { get; set; }
        public double CAmount { get; set; }
        public List<ModeltblMain2> ModelSubtblMain2 { get; set; }
    }

    public class ModeltblMain2
    {
        public string IC { get; set; }
        public string StockNumber { get; set; }
        public double PIn { get; set; }
        public double POut { get; set; }
        public double UP { get; set; }
        public double Cost { get; set; }
        public double Discount { get; set; }
        public int RowNum { get; set; }
        public string OrderTime { get; set; } = DateTime.Now.ToShortTimeString();
    }

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

}


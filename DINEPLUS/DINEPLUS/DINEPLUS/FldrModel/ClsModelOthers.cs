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
}

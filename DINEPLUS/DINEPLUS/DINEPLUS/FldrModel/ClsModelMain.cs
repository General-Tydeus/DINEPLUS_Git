using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DINEPLUS.FldrModel
{

        public class MdlProduct
        {
            [PrimaryKey, AutoIncrement]
            public int RowNum { get; set; }
            public string StockNumber { get; set; }
            public string ProductDesc { get; set; }
            public string UnitMeasure { get; set; }
            public double SellingPrice { get; set; }
            public double UCost { get; set; }
            public bool Active { get; set; }
            public double Qty { get; set; } = 0;
            public double Totals { get; set; } = 0;
            public string CatCode { get; set; }
        }

        public class OrderData
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public double Qty { get; set; }
        }




        public class MdlOrders
        {
            public string StockNumber { get; set; }
            public string ProductDesc { get; set; }
            public string UnitMeasure { get; set; }
            public double SellingPrice { get; set; }
            public double UCost { get; set; }
            public bool Active { get; set; }
            public double Qty { get; set; }
            public int RowNum { get; set; }
            public double Totals { get; set; } = 0;

        }

        public class ViewtblDetailsUser
        {
            public string UserCode { get; set; }
            public string UserName { get; set; }
            public string GroupCode { get; set; }
            public string CNCode { get; set; }
            public string CompleteName { get; set; }
        }


        public class MdlTables
        {
            [PrimaryKey, AutoIncrement]
            public int RowNum { get; set; }
            public string TableCode { get; set; }
            public string TableDesc { get; set; }
            public string Status { get; set; }
            public string LongStatus { get; set; }
            public string TableDocNum { get; set; } = "NA";
            public string TableGUID { get; set; } = "NA";
        }

    public class MdlDiscount
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Discount{ get; set; }
    }
}

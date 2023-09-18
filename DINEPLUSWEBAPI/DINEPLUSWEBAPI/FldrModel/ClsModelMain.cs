using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrModel
{
    public class ClsModelMain
    {
        public class MdlProduct
        {
            public string StockNumber { get; set; }
            public string ProductDesc { get; set; }
            public string UnitMeasure { get; set; }
            public double SellingPrice { get; set; }
            public double UCost { get; set; }
            public bool Active { get; set; }
            public string CatCode { get; set; }

        }
        public class ViewtblDetailsUser
        {
            public string UserCode { get; set; }

            public string UserName { get; set; }

            public string GroupCode { get; set; }

            public string CNCode { get; set; }

            public string CompleteName { get; set; }
        }
    }
}

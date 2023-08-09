using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrModel
{
    public class ModelCollection
    {
        public string RefDoc { get; set; }
        public DateTime TDate { get; set; }
        public double CAmount { get; set; }
    }

    public class ModelSalesProduct
    {
        public string ProductDesc { get; set; }
        public double TotalQty { get; set; }
        public double TotalSales { get; set; }
    }
}

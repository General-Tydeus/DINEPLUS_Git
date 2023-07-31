using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrModel
{
    public class DuplicateData
    {
        public string FldValueFieldName { get; set; }
    }

    public class ModeltblProducts
    {
        public string StockNumber { get; set; }
        public string ProductDesc { get; set; }
        public string UnitMeasure { get; set; }
        public double SellingPrice { get; set; }
        public double UCost { get; set; }
        public bool Active { get; set; }
        public string CatCode { get; set; }
        public string CatDesc { get; set; }
    }

    public class ModeltblCategory
    {
        public string CatCode { get; set; }
        public string CatDesc { get; set; }
    }

    public class ModeltblTable
    {
        public string TableCode { get; set; }
        public string TableDesc { get; set; }
    }

    public class ModeltblEntryName
    {
        public string ControlNo { get; set; }
        public string CustName { get; set; }
        public bool Active { get; set; }
    }
}

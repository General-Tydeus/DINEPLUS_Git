using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrModel
{
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
        public string TableDesc { get; set; }
        public string TableDocNum { get; set; }
        public double CAmount { get; set; }
        public List<ModeltblMain2> ModelSubtblMain2 { get; set; }

        //public List<ModeltblMain3> ModelSubtblMain3 { get; set; }

        //public ModeltblPayMainLoan2 ModelSubtblPayMainLoan2 { get; set; }
    }

    public class ModeltblMain2
    {
        public string IC { get; set; }
        public string StockNumber { get; set; }
        public string ProductDesc { get; set; }

        public double PIn { get; set; }
        public double POut { get; set; }
        public double UP { get; set; }
        public double Cost { get; set; }
        public double Discount { get; set; }
        public double Totals { get; set; }
        public int RowNum { get; set; }
        public string OrderTime { get; set; }
    }

    public class ModeltblMain3
    {
        public string IC { get; set; }
        public string Refer { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string PA { get; set; }
        public bool Reconciled { get; set; }
        public bool SIT { get; set; }
        public int RowNum { get; set; }
        public bool ORPost { get; set; }
        public string ActRemarks { get; set; }
        public string SubNameCode { get; set; }
        public string STCode { get; set; }
        public string DeptCodeCC { get; set; }
        public string MDNumber { get; set; }
        public string SubDepositCode { get; set; }
        public string VoucherNumber { get; set; }
        public bool TaxBase { get; set; }
    }
}

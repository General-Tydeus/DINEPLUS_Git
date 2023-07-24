using DINEPLUSWEBAPI.FldrClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class AutoNumController : ControllerBase
    {
        [HttpGet]
        [Route("API/DINEPLUSWEBAPI/AutoNumber/GetVoucherAutoNum")]
        public string GetVoucherAutoNum(string strURIVoucher, string strURICNCode)
        {
            return new ClsAutoNumber().VoucherAutoNum(strURIVoucher, strURICNCode);
        }
    }
}

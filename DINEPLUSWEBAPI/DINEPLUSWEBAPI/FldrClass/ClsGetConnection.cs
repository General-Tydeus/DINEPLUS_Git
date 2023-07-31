using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrClass
{
    public class ClsGetConnection
    {
        public string PlsConnect()
        {
            return "Server = WINSERVER; Database = DINEPLUS_BE; User ID = server2008; Password = Mssqlone1; Trusted_Connection = False; TrustServerCertificate=True;";



        }
    }
}

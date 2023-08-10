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
            //return "Server = DESKTOP-FHUL9S2\\SQLEXPRESS; Database = DINEPLUS_BE; User ID = server2008; Password = Mssqlone1; Trusted_Connection = False; TrustServerCertificate=True;";
            return "Server = tcp:serverteambig5.database.windows.net,1433; Initial Catalog = DINEPLUS_BE; Persist Security Info = False; User ID = adminteambig5; Password =Watbt123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";


        }
    }
}

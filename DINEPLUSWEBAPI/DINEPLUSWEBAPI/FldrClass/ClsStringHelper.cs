using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrClass
{
    public class ClsStringHelper
    {


        public string LongStats(string strStats)
        {

            if(strStats == "A")
            {
                return "AVAILABLE";
            }else if(strStats == "O")
            {
                return "OCCUPIED";
            }else if(strStats == "R")
            {
                return "RESERVED";
            }
            else
            {
                return "NA";
            }


        }



    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DINEPLUS.FldrClass
{
    class ClsValidation
    {
        public bool isNumeric(string val)
        {
            if (isInt(val) || isDouble(val))
                return true;
            else
                return false;
        }

        public bool isInt(string val)
        {
            int num;
            if (int.TryParse(val, out num))
                return false;
            else
                return true;
        }

        public bool isDouble(string val)
        {
            double num;
            if (double.TryParse(val, out num))
                return false;
            else
                return true;
        }

        public bool emptytxt(string val)
        {
            if (String.IsNullOrEmpty(val))
                return true;
            else
                return false;
        }

        public bool errordate(string val)
        {
            DateTime num;
            if (val == "  /  /")
                return false;
            if (Convert.ToInt16(val.Length) < 10)
                return true;
            else if (!DateTime.TryParse(val, out num))
                return true;
            else
                return false;
        }
    }
}

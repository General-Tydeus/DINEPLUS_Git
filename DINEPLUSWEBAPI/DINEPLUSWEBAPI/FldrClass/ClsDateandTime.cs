using System;
using System.Collections.Generic;
using System.Text;

namespace DINEPLUSWEBAPI.FldrClass
{
    public class ClsDateandTime
    {
        public string plsZoneUsed()
        {
            return "2"; //1=localdb, 2=localtimezone
        }
        public string plsLocalZoneDateToday()
        {
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Singapore Standard Time");
            return String.Format("{0:MM/dd/yyyy}", _localTime);
        }

        public string plsLocalZoneTimeNow()
        {
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Singapore Standard Time");
            return _localTime.ToShortTimeString();
        }

        public string plsLocalZoneDateTimeNow()
        {
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Singapore Standard Time");
            return _localTime.ToString();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DINEPLUSBE.FldrModel
{
    public class ClsModeltblUser
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string CNCode { get; set; }
    }

    public class ModeltblGroup
    {
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
    }

    public class ModeltblUser
    {
        public string UserCode { get; set; }
        public string PWord { get; set; }
        public string GroupCode { get; set; }
        public string UserName { get; set; }
        public string CompleteName { get; set; }
        public string CNCode { get; set; }
    }
}

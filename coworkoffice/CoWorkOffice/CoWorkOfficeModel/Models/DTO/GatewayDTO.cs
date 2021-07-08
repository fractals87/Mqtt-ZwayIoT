using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorkOfficeModel.Models.DTO
{
    public class GatewayDTO
    {
        public string id { get; set; }
        public string description { get; set; }
        public string protocol { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }
}

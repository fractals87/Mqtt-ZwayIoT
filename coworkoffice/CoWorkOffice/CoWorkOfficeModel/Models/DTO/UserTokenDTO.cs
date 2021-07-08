using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorkOfficeModel.Models.DTO
{
    public class UserTokenDTO
    {
        public string user { get; set; }
        public string role { get; set; }
        public string token { get; set; }
    }
}

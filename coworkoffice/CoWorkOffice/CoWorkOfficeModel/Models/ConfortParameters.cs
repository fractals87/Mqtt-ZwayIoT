using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorkOfficeModel.Models
{
    public class ConfortParameter: BaseEntity
    {
        public string Parameter { get; set; }
        public int ConfortValue { get; set; }
    }
}

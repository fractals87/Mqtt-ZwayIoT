using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorkOfficeModel.Models
{
    public class Command
    {
        public int IoTDevice_Id { get; set; }
        public double Value { get; set; }
        public string CustomCommand { get; set; }
    }
}

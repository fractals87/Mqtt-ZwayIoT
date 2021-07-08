using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorkOfficeModel.Models.DTO
{
    public class MeasureDTO
    {
        public DateTime data { get; set; }
        public double valore { get; set; }
        public string room { get; set; }
        public string iotdevice { get; set; }

    }
}

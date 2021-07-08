using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorkOfficeModel.Models.DTO
{
    public class IotDeviceDTO
    {
        public string description { get; set; }
        public string deviceType { get; set; }
        public string probeType { get; set; }
        public string registrationIdentifier { get; set; }
        public string room { get; set; }
    }
}

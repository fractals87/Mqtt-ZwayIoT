using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoWorkOfficeModel.Models
{
    public enum GatewayEnum : ushort
    {
        GatewayHUE = 1,
        GatewayZWay = 2
    }

    public class IoTDevice : BaseEntity
    {
        public string Description { get; set; }
        public string DeviceType { get; set; }
        public string ProbeType { get; set; }
        public string RegistrationIdentifier { get; set; }
        //public string State { get; set; }

        public int Gateway_Id { get; set; }
        [ForeignKey("Gateway_Id")]
        public virtual Gateway Gateway { get; set; }

        public int Room_Id { get; set; }
        [ForeignKey("Room_Id")]
        public virtual Room Room { get; set; }
    }

    public class Gateway : BaseEntity
    {
        public string Description { get; set; }
        public string Protocol { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

    }
}

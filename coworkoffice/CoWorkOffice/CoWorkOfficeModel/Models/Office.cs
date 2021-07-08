using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoWorkOfficeModel.Models
{
    public class Room : BaseEntity
    {
        public string Description { get; set; }

        public virtual ICollection<IoTDevice> IoTDevices { get; set; }
    }

    public class Office : Room
    {
        public int? WaitingRoomId { get; set; }
        [ForeignKey("WaitingRoomId")]
        public virtual WaitingRoom WaitingRoom { get; set; }
    }

    public class WaitingRoom : Room
    {
        public int NMaxCustomer { get; set; }
    }
}

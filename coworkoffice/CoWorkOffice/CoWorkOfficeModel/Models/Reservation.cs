using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoWorkOfficeModel.Models
{
    public class Reservation: BaseEntity
    {
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }

        public int NCustomerExpected { get; set; }

        public int OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public virtual Office Office { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}

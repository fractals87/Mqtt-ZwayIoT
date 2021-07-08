using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoWorkOfficeModel.Models
{
    public class Measure : BaseEntity
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public int IoTDevice_Id { get; set; }
        [ForeignKey("IoTDevice_Id")]
        public virtual IoTDevice IoTDevice { get; set; }

        //public virtual ICollection<Measure> Measures { get; set; }
    }
}

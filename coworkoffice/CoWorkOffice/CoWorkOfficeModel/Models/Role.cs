using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoWorkOfficeModel.Models
{
    public class Role : BaseEntity
    {
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorkOfficeModel.Models.DTO
{
    public class ReservationDTO
    {
        public int id { get; set; }
        public string da { get; set; }
        public string a { get; set; }
        public int clienti { get; set; }
        public int idroom { get; set; }
        public string room  { get; set; }
    }
}

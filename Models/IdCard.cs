using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class IdCard
    {
        public long Id { get; set; }
        public string NationalId { get; set; } 
        public string Name { get; set; }
        public string Firstname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class Member
    {
        public int Id { get; set; }
        public int Login { get; set; } // Mail or Phone

        public string CreationDate { get; set; }
        public string ExpiryDate { get; set; }
        public IdCard IdCard { get; set; }
    }
}

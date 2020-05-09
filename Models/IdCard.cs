using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class IdCard
    {
        public long IdCardId { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.Date)]

        public DateTime ExpiryDate { get; set; }
        public long MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public int IsValidIdCard()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Firstname))
            {
                if (Regex.IsMatch(NationalId, @"\d{2}.\d{2}.\d{2}-\d{3}-\d{2}"))
                {
                    return 200;
                }
                else
                {
                    return 401;
                }
            }
            else
            {
                return 400;
            }
        }
    }
}

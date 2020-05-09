using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class CardMember
    {
        public long CardMemberId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationCardDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiryCardDate { get; set; }
        public bool IsBlacklist { get; set; }
        [DataType(DataType.Date)]
        public DateTime BlacklistEndDate { get; set; }
        public long MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        /*public CardMember()
        {
            CreationCardDate = DateTime.Now;
            ExpiryCardDate = DateTime.Now.AddYears(3);
            IsBlacklist = false;
            BlacklistEndDate = DateTime.MinValue;
        }*/
    }
}

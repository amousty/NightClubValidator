using NightClubValidator.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class MemberCard
    {
        public int MemberCardId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationCardDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiryCardDate { get; set; }
        public bool IsBlacklist { get; set; }
        public bool IsDeactivated { get; set; }
        [DataType(DataType.Date)]
        public DateTime BlacklistEndDate { get; set; }
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public MemberCard()
        {
            CreationCardDate = DateTime.Now;
            ExpiryCardDate = DateTime.Now.AddYears(3);
            IsBlacklist = false;
            BlacklistEndDate = DateTime.MinValue;
            IsDeactivated = DateHelper.IsDateExpired(ExpiryCardDate);
        }

        public bool CardIsValid()
        {
            return !IsBlacklist && BlacklistEndDate < DateTime.Now;
        }
    }
}

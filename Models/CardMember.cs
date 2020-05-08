using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class CardMember
    {
        public long Id { get; set; }
        public DateTime CreationCardDate { get; set; }
        public DateTime ExpiryCardDate { get; set; }
        public bool IsBlacklist { get; set; }
        public DateTime BlacklistEndDate { get; set; }

        public CardMember()
        {
            CreationCardDate = DateTime.Now;
            ExpiryCardDate = CreationCardDate.AddYears(3);
            IsBlacklist = false;
            BlacklistEndDate = DateTime.MinValue;
        }
    }
}

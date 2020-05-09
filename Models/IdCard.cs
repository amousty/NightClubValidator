using Microsoft.AspNetCore.Mvc;
using NightClubValidator.Helper;
using NightClubValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class IdCard : ICard
    {
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        [Remote(action: "IdCardExists", controller: "IdCards")]
        [Key]
        public string NationalId { get; set; }
        public int IdCardId { get; set; } // Not the best name :) 
        public string Name { get; set; }
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.Date)]

        public DateTime ExpiryDate { get; set; }
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public bool CardIsValid()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(FirstName))
            {
                if (Regex.IsMatch(NationalId, @"\d{2}.\d{2}.\d{2}-\d{3}-\d{2}"))
                {
                    if (!DateHelper.IsDateExpired(ExpiryDate) && DateHelper.IsDateExpired(CreatedOn))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

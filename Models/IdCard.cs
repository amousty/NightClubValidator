﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class IdCard
    {
        public long Id { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiryDate { get; set; }


        public IdCard()
        {
        }

        public IdCard(string nationalId, string name, string firstname, DateTime createdOn)
        {
            NationalId = nationalId ?? throw new ArgumentNullException(nameof(nationalId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Firstname = firstname ?? throw new ArgumentNullException(nameof(firstname));
            CreatedOn = createdOn;
            ExpiryDate = createdOn.AddYears(10);
        }

        public int IsValidIdCard()
        {
            if (!string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Firstname))
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

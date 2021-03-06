﻿using NightClubValidator.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        // https://www.twilio.com/blog/validating-phone-numbers-effectively-with-c-and-the-net-frameworks
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public IdCard IdCard { get; set; }
        public ICollection<MemberCard> MemberCards { get; set; }


        public Member()
        {
            MemberCards = new List<MemberCard>();
        }

        private double GetAge()
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - Birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (Birthdate.Date > today.AddYears(-age)) age--;
            return age;
        }

        public bool IsValidUser()
        {
            string validMail = string.IsNullOrEmpty(EmailAddress) ? "empty" : TypeHelper.IsMailAddress(EmailAddress) ? "ok" : "nok";
            string validPhone = string.IsNullOrEmpty(PhoneNumber) ? "empty" : TypeHelper.IsPhoneNumber(PhoneNumber) ? "ok" : "nok";
            // Phone or mail could be empty, but they have to be correct if entered
            if ((validMail != "nok" && validPhone != "nok") && (validMail == "ok" || validPhone == "ok"))
            {
                if (GetAge() < 120 && GetAge() >= 18)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

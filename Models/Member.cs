﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NightClubValidator.Models
{
    public class Member
    {
        public long MemberId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        // https://www.twilio.com/blog/validating-phone-numbers-effectively-with-c-and-the-net-frameworks
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        
        //public long IdCardFK { get; set; }
        //[ForeignKey("IdCardFK")]
        public IdCard IdCard { get; set; }
        //public long CardMemberFK { get; set; }
        //[ForeignKey("CardMemberFK")]
        public ICollection<CardMember> CardMembers { get; set; }


        public Member()
        {
            CardMembers = new List<CardMember>();
        }

        //public Member(string mail, string phone, DateTime birthdate, IdCard idCard, List<CardMember> cardMembers)
        //{
        //    Mail = mail ?? throw new ArgumentNullException(nameof(mail));
        //    Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        //    Birthdate = birthdate;
        //    IdCard = idCard ?? throw new ArgumentNullException(nameof(idCard));
        //    CardMembers = cardMembers ?? throw new ArgumentNullException(nameof(cardMembers));
        //}

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

        public int IsValidUser()
        {
            if (string.IsNullOrEmpty(Mail) || !string.IsNullOrEmpty(Phone))
            {
                if (GetAge() < 120 && GetAge() >= 18)
                {
                    return 200;
                }
                else
                {
                    return 402;
                }
            }
            else
            {
                return 403;
            }
        }
    }
}

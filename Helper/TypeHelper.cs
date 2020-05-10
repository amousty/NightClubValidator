using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NightClubValidator.Helper
{
    public static class TypeHelper
    {
        public static bool IsMailAddress(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").Success;
        }
    }
}

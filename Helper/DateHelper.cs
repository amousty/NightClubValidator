using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NightClubValidator.Helper
{
    public static class DateHelper
    {
        public static bool IsDateExpired(DateTime cardDate) => DateTime.Now.Subtract(cardDate).TotalMilliseconds < 0;
    }
}

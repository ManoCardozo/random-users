using System;

namespace RandomUser.Application.Extensions
{
    public static class DateExtensions
    {
        public static string ToUiDate(this DateTime date)
        {
            return date.ToString("dd-MMM-yyyy");
        }
    }
}

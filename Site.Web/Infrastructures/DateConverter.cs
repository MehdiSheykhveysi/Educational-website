using System;
using System.Globalization;

namespace Site.Web.Infrastructures
{
    public static class DateConverter
    {
        public static string ToShamsi(this DateTime dateTime)
        {
            PersianCalendar calendar = new PersianCalendar();
            return calendar.GetYear(dateTime) + "/" + calendar.GetMonth(dateTime).ToString("00") + "/" +
                   calendar.GetDayOfMonth(dateTime).ToString("00");
        }

        public static DateTime ToGregorian(this string value)
        {
            DateTime dt = DateTime.Parse(value, new CultureInfo("fa-IR"));
            // Get Utc Date
            DateTime dt_utc = dt.ToUniversalTime();
            return dt_utc;
        }
    }
}

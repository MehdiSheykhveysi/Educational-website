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
    }
}

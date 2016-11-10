using FeedVinc.Common.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeedVinc.Common.Services
{
    public static class DateTimeService
    {
        public static string GetPrettyDate(DateTime? date,string language)
        {
            // 1.
            // Get time span elapsed since the date.
            TimeSpan s = DateTime.Now.Subtract(Convert.ToDateTime(date));

            // 2.
            // Get total number of days elapsed.
            int dayDiff = (int)s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            int secDiff = (int)s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }

            // 5.
            // Handle same-day times.
            if (dayDiff == 0 && language=="en-US")
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} hours ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            else if (dayDiff == 0 && language == "tr-TR")
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "şimdi";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 dakika önce";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} dakika önce",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} saat önce",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1 && language=="en-US")
            {
                return "yesterday";
            }
            if (dayDiff < 7 && language == "en-US")
            {
                return string.Format("{0} days ago",
                dayDiff);
            }
            if (dayDiff < 31 && language == "en-US")
            {
                return string.Format("{0} weeks ago",
                Math.Ceiling((double)dayDiff / 7));
            }

            if (dayDiff == 1 && language == "tr-TR")
            {
                return "dün";
            }
            if (dayDiff < 7 && language == "tr-TR")
            {
                return string.Format("{0} gün önce",
                dayDiff);
            }
            if (dayDiff < 31 && language == "tr-TR")
            {
                return string.Format("{0} hafta önce",
                Math.Ceiling((double)dayDiff / 7));
            }

            return null;
        }

        public static string GetHuminizeDate(this DateTime date ,CultureInfo cultureUI,string time)
        {
            Thread.CurrentThread.CurrentCulture = cultureUI;
            Thread.CurrentThread.CurrentUICulture = cultureUI;

            string day = null;
            string month = null;
            string dateIndex = date.Day.ToString();
            int year = date.Year;
            time = DateByLang.Hour + " " + time;

            switch ((int)date.DayOfWeek)
            {
                case 0:
                    day = DateByLang.Sunday;
                    break;
                case 1:
                    day = DateByLang.Monday;
                    break;
                case 2:
                    day = DateByLang.Tuesday;
                    break;
                case 3:
                    day = DateByLang.Wednesday;
                    break;
                case 4:
                    day = DateByLang.Thursday;
                    break;
                case 5:
                    day = DateByLang.Friday;
                    break;
                case 6:
                    day = DateByLang.Saturday;
                    break;
                default:
                    break;
            }


            switch (date.Month)
            {
                case 1:
                    month = DateByLang.January;
                    break;
                case 2:
                    month = DateByLang.February;
                    break;
                case 3:
                    month = DateByLang.March;
                    break;
                case 4:
                    month = DateByLang.April;
                    break;
                case 5:
                    month = DateByLang.May;
                    break;
                case 6:
                    month = DateByLang.June;
                    break;
                case 7:
                    month = DateByLang.July;
                    break;
                case 8:
                    month = DateByLang.August;
                    break;
                case 9:
                    month = DateByLang.September;
                    break;
                case 10:
                    month = DateByLang.October;
                    break;
                case 11:
                    month = DateByLang.November;
                    break;
                case 12:
                    month = DateByLang.December;
                    break;
            }

            return dateIndex + " "  + month + " " + day + " " + year + " " + time;

        }
        
    }
}

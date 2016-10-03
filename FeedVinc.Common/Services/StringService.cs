using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.Common.Services
{
    public static class StringService
    {
        public static string Capitalize(this string value)
        {
            return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length-1);
        }
    }
}

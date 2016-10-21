using FeedVinc.WEB.UI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.UIServices
{
    public static class HtmlTextService
    {
        public static string ConvertToHtml(AdContentVM value,string openTag,string closeTag)
        {
            value.Description = value.Description.Replace("\r\n", ".");
            var model = value.Description.Split('.').ToList();

            var output = string.Format("{0} {1} {2} {3} {4} {5} ","<b>",openTag, value.Title , closeTag , "</br>","</b>");

            foreach (var item in model)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    output += string.Format("{0} - {1} {2}", openTag, item, closeTag);
                }
                
            }

            return output;

        }
    }
}
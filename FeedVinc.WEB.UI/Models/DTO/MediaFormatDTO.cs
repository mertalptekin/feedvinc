using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.DTO
{
    public class MediaFormatDTO
    {
        public HttpPostedFileBase Media { get; set; }
        public string FolderName { get; set; }
        public int MediaType { get; set; }

    }
}
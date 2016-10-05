using FeedVinc.WEB.UI.Models.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.UIServices
{
    public class MediaManagerService
    {
        public static string Save(MediaFormatDTO dto)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Media.FileName);
            dto.Media.SaveAs(HttpContext.Current.Server.MapPath(Path.Combine("~/UserShareMedia/",fileName)));
            return "/UserShareMedia/" + fileName;
        }
    }
}
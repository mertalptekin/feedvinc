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

            if (Directory.Exists(HttpContext.Current.Server.MapPath(dto.FolderName)))
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Media.FileName);
                dto.Media.SaveAs(HttpContext.Current.Server.MapPath("~/UserShareMedia/"+ dto.FolderName + "/" + fileName));

                return "/UserShareMedia/" + dto.FolderName + "/" + fileName;
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(HttpContext.Current.Server.MapPath(dto.FolderName)));
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Media.FileName);
                dto.Media.SaveAs(HttpContext.Current.Server.MapPath("~/UserShareMedia/" + dto.FolderName + "/" + fileName));

                return "/UserShareMedia" + dto.FolderName + "/" + fileName;
            }
        }
    }
}
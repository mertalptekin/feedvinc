using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Attributes
{
    public class MultipleImageUploadValidation:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //100 KB
            //En fazla 5 adet
            //jpg,png,jpeg destepi olan file validasyonu
            string[] allowedMediaFormat = new string[] { ".jpg", ".png", ".jpeg" };
            int totalSize = 0;
            int maxAllowedSize = 1024 * 1024 *100; //100 KB
            List<string> uploadedMediaFormat = new List<string>();
            bool IsValid = true;

            var files = (IEnumerable<HttpPostedFileBase>)value;

            foreach (var item in files)
            {
                if (item == null)
                {
                    return false;
                }

                totalSize += item.ContentLength;
                uploadedMediaFormat.Add(Path.GetExtension(item.FileName));
            }

            if (totalSize>maxAllowedSize)
            {
                return false;
            }

            foreach (var item in allowedMediaFormat)
            {
                if (!allowedMediaFormat.Contains(item))
                {
                    return false;
                }
            }

            return IsValid;
        }
    }
}
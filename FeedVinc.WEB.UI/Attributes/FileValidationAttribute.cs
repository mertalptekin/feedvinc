using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Attributes
{
    public class FileValidationAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = (HttpPostedFileBase)value;

            if (file==null)
            {
                return false;
            }
            else if (file.ContentLength > 1024 * 100)
            {
                return false;
            }

            return true;
        }
    }
}
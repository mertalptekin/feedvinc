using FeedVinc.WEB.UI.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Models
{
    public class AdminAppPostVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Uygulama adı girilmesi zorunludur")]
        public string AppNameTR { get; set; }

        [Required(ErrorMessage = "Uygulama adı girilmesi zorunludur")]
        public string AppNameEn { get; set; }

        [Required(ErrorMessage ="Uygulamanın Ücretli olup olmadığını belirtmediniz")]
        public bool IsFree { get; set; }

        [Required(ErrorMessage = "Uygulama açıklaması girilmesi zorunludur")]
        public string InformationTR { get; set; }

        [Required(ErrorMessage = "Uygulama açıklaması girilmesi zorunludur")]
        public string InformationEN { get; set; }

        [FileValidation(ErrorMessage ="Resim Seçimi Yapmadınız veya yüksek boyutta bir fotoğraf girdiniz")]
        public HttpPostedFileBase AppFotoFile { get; set; }

        public string AppIconPath { get; set; }

        public decimal? SalesPrice { get; set; }
        public string Currency { get; set; }
    }
}
using FeedVinc.WEB.UI.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Models
{
    public class AdminTaskPostVM
    {
        [Required(ErrorMessage = "Görev ismini girmek zorunludur")]
        public string NameTR { get; set; }

        [Required(ErrorMessage = "Görev ismini girmek zorunludur")]
        public string NameEN { get; set; }
        public string TaskLogo { get; set; }

        [Required(ErrorMessage = "Görev Açıklaması zorunludur")]
        public string DescriptionTR { get; set; }
        //Hangi seviyede olduğunu görmek için
        [Required(ErrorMessage = "Görev Açıklaması zorunludur")]
        public string DescriptionEN { get; set; }

        [Range(0, 1000, ErrorMessage = "Görevlendirme Aşaması seçilmelidir")]
        public byte ProjectTaskTypeID { get; set; }
        public bool HasTextInput { get; set; }
        public bool HasHyperLink { get; set; }
        public string HyperLink { get; set; }
        public bool IsDynamic { get; set; }
        [FileValidation(ErrorMessage ="Görev Resmi girmek zorunludur")]
        public HttpPostedFileBase TaskLogoFile { get; set; }

        public bool IsActive { get; set; }
        public int ID { get; set; }

    }
}
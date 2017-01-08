using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Models
{
    public class InvestmentShareVM
    {
        public string ProjectName { get; set; }

        [Required(ErrorMessage ="Yatırım alan proje için fiyat teklifi vermelisiniz")]
        public decimal InvestmentPrice { get; set; }

        [Required(ErrorMessage ="Dolar,Euro veya TL cinsinden birim girmelisiniz")]
        public string Currency { get; set; }
        public DateTime? PrettyDate { get; set; }
        public string ProjectProfileLogo { get; set; }
        public long ShareCount { get; set; }

        [Required(ErrorMessage ="Yatırım alan proje için paylaşım içeriği giriniz")]
        public string InvestmentShareText { get; set; }
        public long ProjectID { get; set; }


    }
}
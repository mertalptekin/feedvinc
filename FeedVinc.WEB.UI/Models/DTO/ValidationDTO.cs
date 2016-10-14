using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.DTO
{
    public class ValidationDTO
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string RedirectURL { get; set; }
        public object Data { get; set; }

    }
}
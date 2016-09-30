using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Account
{
    public class ForgetPasswordVM
    {
        [Required]
        public string Email { get; set; }

    }
}
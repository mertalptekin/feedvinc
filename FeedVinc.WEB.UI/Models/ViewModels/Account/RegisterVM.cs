using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"")]
        public string Password { get; set; }

        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [Required]
        public int UserType { get; set; }

    }
}
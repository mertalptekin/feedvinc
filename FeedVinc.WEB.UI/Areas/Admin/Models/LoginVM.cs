using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.Web.UI.Models.Account
{
    public class LoginVM
    {
        [EmailAddress][Required][DisplayName("E-posta")]
        public string Email { get; set; }

        [Required][DisplayName("Parola")]
        public string Password { get; set; }

        public bool IsPersistant { get; set; }


    }
}
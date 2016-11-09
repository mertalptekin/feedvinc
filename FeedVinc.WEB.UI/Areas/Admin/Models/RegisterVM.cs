using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.Web.UI.Models.Account
{
    public class RegisterVM
    {
        [Required][DisplayName("isim soyisim")]
        public string FullName { get; set; }

        [Required][DisplayName("Kullanıcı adı")]
        public string UserName { get; set; }

        [EmailAddress][DisplayName("E-Posta")]
        public string Email { get; set; }

        [DataType(DataType.Password)][DisplayName("Parola")]
        public string Password { get; set; }

        [Compare("Password")][DisplayName("Parola Tekrar")]
        public string PasswordConfirm { get; set; }

    }
}
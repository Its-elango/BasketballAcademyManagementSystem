using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballAcademy.Models
{
    public class User
    {
        public int ID { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "User name")]
        public string username { get; set; }
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Confirm password ")]
        public string ConfirmPassword { get; set; }

    }
}
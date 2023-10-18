using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BasketballAcademy.Models
{
    public class Admin
    {
        [Display(Name = " ID")]
        public int Id { get; set; }

        [Display(Name = "Full name")]
        public string fullName { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Username ")]
        public string username { get; set; }

        [Display(Name = "Password ")]
        public string password { get; set; }

        [Display(Name = "Confirm password ")]
        public string ConfirmPassword { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketballAcademy.Models
{
    public class Admission
    {
        public int Id { get; set; }
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Display(Name = "Month")]
        public string ChooseMonths { get; set; }

        [Display(Name = "Coach")]
        public string Coach { get; set; }

        public int CoachID { get; set; }

        [Display(Name = "Parent/Guardian name")]
        public string ParentGuardianName { get; set; }

        [Display(Name = "Parent/Guardian phone number")]
        public string ParentGuardianPhone { get; set; }

        public string Payment { get; set; }

        [Display(Name = "Status")]
        public int status { get; set; }

        public byte[] photo { get; set; }
    }
}
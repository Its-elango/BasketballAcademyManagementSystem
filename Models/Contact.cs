using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketballAcademy.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
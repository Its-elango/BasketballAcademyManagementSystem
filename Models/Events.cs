using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketballAcademy.Models
{
    public class Events
    {
        [Display(Name = "Event ID")]
        public int EventID { get; set; }

        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Time")]
        public TimeSpan EventTime { get; set; }
        public string Venue { get; set; }
        public  string Details{ get; set; }

        [Display(Name = "Age group")]
        public string AgeGroup { get; set; }

        public string Incharge { get; set; }

        public List<SelectListItem> coach { get; set; }

        [Display(Name = "Event poster")]
        public  byte[] EventImage  { get; set;}

        [Display(Name = "Prize details")]
        public string PrizeDetails { get; set; }
        public string Contact { get; set; }



    }
}
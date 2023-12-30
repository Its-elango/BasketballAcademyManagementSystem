using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketballAcademy.Controllers
{

    [AllowAnonymous]
    public class HomeController : Controller
    {
        /// <summary>
        /// Action method for displaying the home page.
        /// </summary>
       
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// Action method for displaying the about page.
        /// </summary>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Action method for displaying the gallery page.
        /// </summary>
        public ActionResult Gallery()
        {
            return View();
        }

        /// <summary>
        /// Action method for displaying the contact page.
        /// </summary>
        public ActionResult Contact()
        {
            return View();
        }

    }
}
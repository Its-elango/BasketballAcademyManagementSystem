using BasketballAcademy.Models;
using BasketballAcademy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketballAcademy.Controllers
{
    public class CoachController : Controller
    {

        /// <summary>
        /// Displays the coach's home page.
        /// </summary>
        [Authorize]
        public ActionResult CoachHomePage()
        {
            return View();
        }

        /// <summary>
        /// Displays the page to add a new coach.
        /// </summary>
        [Authorize]
        public ActionResult AddCoach()
        {
            return View();
        }

        // HTTP POST action method for inserting a coach into the database
        [HttpPost]
        [Authorize]
        /// <summary>
        /// Handles the form submission to register a new coach.
        /// </summary>
        /// <param name="Coach">The coach's information to register.</param>
        public ActionResult AddCoach(Coach Coach)
        {
            try
            {
                CoachRepository repository = new CoachRepository();
                repository.RegisterCoach(Coach); 
                ViewBag.Message = "Coach added successfully"; 
                return View();
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Displays a list of coaches.
        /// </summary>
        [Authorize]
        public ActionResult ViewCoach()
        {
            try
            {
                CoachRepository repository = new CoachRepository();
                List<Coach> Coach = repository.ViewCoach(); 
                return View(Coach); 
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }
        /// <summary>
        /// Fetch the respective ID for the coach
        /// </summary>
        /// <param name="id">The ID of the coach to update.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult UpdateCoach( int id,string name,string email)
        {
            try
            {
                Coach coach  = new Coach();
                coach.Id = id;
                coach.FullName = name;
                coach.Email = email;
                ViewBag.Name = name;
                ViewBag.Email = email;
                return View(coach);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }


        [HttpPost]
        [Authorize]
        /// <summary>
        /// Handles the form submission to update coach information.
        /// </summary>
        /// <param name="coach">The updated coach information.</param>
        /// <param name="id">The ID of the coach to update.</param>
        public ActionResult UpdateCoach(Coach coach)
        {
            try
            {
                CoachRepository repository = new CoachRepository();
                repository.UpdateCoach(coach); 
                ViewBag.Message = "Profile updated successfully"; 
                return View();
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Displays the page to register an event for a coach.
        /// </summary>
        [Authorize]
        public ActionResult RegisterEvent()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        /// <summary>
        /// Handles the form submission to register an event for a coach.
        /// </summary>
        /// <param name="coach">The coach for whom the event is registered.</param>
        public ActionResult RegisterEvent(Coach coach)
        {
            try
            {
                CoachRepository repository = new CoachRepository();
                repository.RegisterEvent(coach); 
                ViewBag.Message = "Event Registered successfully";
                return View();
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }


        /// <summary>
        /// Deletes a coach with the specified ID.
        /// </summary>
        /// <param name="Id">The ID of the coach to delete.</param>
        [Authorize]
        public ActionResult Delete(int Id)
        {
            try
            {
                CoachRepository repository = new CoachRepository();
                repository.DeleteCoach(Id); 
                return RedirectToAction("ViewCoach"); 
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [Authorize]
        /// <summary>
        /// Handles the form submission for deleting a coach.
        /// </summary>
        /// <param name="Id">The ID of the coach to delete.</param>
        /// <param name="collection">The form data collection.</param>
        public ActionResult Delete(int Id, FormCollection collection)
        {
            try
            {
                return View();
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

    }
}
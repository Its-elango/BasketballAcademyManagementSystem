using BasketballAcademy.Models;
using BasketballAcademy.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BasketballAcademy.Controllers
{
    [Authorize]
    public class AdmissionController : Controller
    {

        /// <summary>
        /// Displays the enrollment form for players.
        /// </summary>
        /// <returns>The view for enrolling players.</returns>
       
        public ActionResult EnrollPlayer()
        {
            return View();
        }


        /// <summary>
        /// Displays the enrollment form for players with coach information.
        /// </summary>
        /// <param name="id">The ID of the player (if applicable).</param>
        /// <param name="name">The name of the coach associated with the enrollment form.</param>
        /// <returns>The enrollment form view.</returns>
        [HttpGet]
        public ActionResult EnrollPlayer(int coachid, string name,int id)
        {
            try
            {
                Admission admission = new Admission();
                admission.CoachID = coachid;
                admission.Coach = name;
                admission.Id = id;
                return View(admission);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Handles the enrollment form submission for players.
        /// </summary>
        /// <param name="admission">The Admission object containing player information.</param>
        /// <returns>The view indicating the enrollment status.</returns>
        [HttpPost]
        public ActionResult EnrollPlayer(Admission admission)
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
                int result = repository.AdmissionForm(admission);
                if (result == 1)
                {
                    ViewBag.success = "Enrolled successfully";
                }
                else if (result == -1)
                {
                    ViewBag.Enrolled = "You  already enrolled in the academy!";
                }
                else if (result == -2)
                {
                    ViewBag.NotAvailable = "Coach not available";
                }
                return View();
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }


        /// <summary>
        /// Displays the player update form based on the provided player ID.
        /// </summary>
        /// <param name="id">The ID of the player to be updated.</param>
        ///  <param name="email">The email of the player to be updated.</param>
        ///  <param name="name">The name of the player to be updated.</param>
        /// <returns>The player update form view.</returns>
        [HttpGet]
        public ActionResult UpdatePlayer(int id,string email,string name)
        {
            try
            {
                Admission admission = new Admission();
                admission.Id = id;
                admission.Email = email;
                admission.FullName = name;
                return View(admission);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }


        /// <summary>
        /// Handles the submission of player profile updates.
        /// </summary>
        /// <param name="admission">The Admission object containing updated player information.</param>
        /// <returns>The view indicating the update status.</returns>
        [HttpPost]
        public ActionResult UpdatePlayer(Admission admission)
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
              if(repository.UpdateProfile(admission))
                {
                    ViewBag.Success = "Profile updated succesfully";
                }
                return View();
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
             
                return View();
            }
        }

        /// <summary>
        /// Retrieves a list of admissions from the AdmissionRepository and displays them in a view.
        /// </summary>
        /// <returns>An ActionResult representing the view with a list of admissions.</returns>
        public ActionResult ViewPlayer()
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
                List<Admission> admissions = repository.ViewPlayer();
                return View(admissions);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }


        /// <summary>
        /// Displays a list of enrolled players using data from the AdmissionRepository.
        /// </summary>
        /// <returns>An ActionResult representing the view with a list of enrolled players.</returns>
        public ActionResult ViewEnrolledPlayer()
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
                List<Admission> admissions = repository.ViewEnrolledPlayer();
                return View(admissions);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
      
                return View();
            }
        }


        /// <summary>
        /// Updates the status of an item with the specified ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to update.</param>
        /// <param name="status">The new status to set for the item.</param>
        /// <returns>A JsonResult indicating the success or failure of the status update.</returns>
        [HttpPost]
        public JsonResult UpdateStatus(int itemId, int status)
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
                bool success = repository.UpdateState(itemId, status);
                return Json(new { success = true, message = "Status updated successfully" });
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return Json(new { success = false, message = "Error updating status: " + exception.Message });
            }
        }




        /// <summary>
        /// Deletes a player with the specified ID from the system.
        /// </summary>
        /// <param name="Id">The ID of the player to delete.</param>
        /// <returns>ActionResult: Redirects to the "ViewEnrolledPlayer" action after successful deletion; returns a view in case of an exception.</returns>
        public ActionResult Delete(int Id)
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
                repository.DeletePlayer(Id);
                return RedirectToAction("ViewEnrolledPlayer");
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }


        /// <summary>
        /// Retrieves a list of coaches from the AdmissionRepository and displays them in a view.
        /// </summary>
        /// <returns>An ActionResult representing the view with a list of coaches.</returns>
        public ActionResult CoachList()
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
                List<Coach> coaches = repository.CoachList();
                return View(coaches);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }


        /// <summary>
        /// Retrieves a list of players with the specified name from the AdmissionRepository and displays them in a view.
        /// </summary>
        /// <param name="name">The name to filter players by.</param>
        /// <returns>An ActionResult representing the view with a list of filtered players.</returns>
        public ActionResult PlayerList(string name)
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
                List<Admission> players = repository.PlayerList(name);
                if (players.Count == 0)
                {
                    ViewBag.Message = "No players found";
                   return View();
                }
                else
                {
                    return View(players);
                }

              
               
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }


        /// <summary>
        /// Action method for viewing events associated with a player.
        /// </summary>
        /// <param name="id">The ID of the player for whom events are to be viewed.</param>
        public ActionResult ViewPlayerEvent(int id)
        {
            try
            {
                AdmissionRepository repository = new AdmissionRepository();
                List<Events> events = repository.GetEventsByPlayer(id);
                if (events.Count == 0)
                {
                    ViewBag.Message = "No events found..";
                    return View();
                }
                else
                {
                    return View(events);
                }
                             

            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View("ErrorView");
            }
        }

    }
}
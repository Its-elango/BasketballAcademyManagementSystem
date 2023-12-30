using BasketballAcademy.Models;
using BasketballAcademy.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace BasketballAcademy.Controllers
{
    [Authorize]
    public class EventController : Controller
    {


        /// <summary>
        /// Displays a page to add a new event.
        /// </summary>
     
        public ActionResult AddEvents()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
 
        /// <summary>
        /// Handles the form submission to add a new event.
        /// </summary>
        /// <param name="events">The event data to be added.</param>
        public ActionResult AddEvents(Events events )
        {
            try
            {
                EventRepository repository = new EventRepository();
                repository.AddEvents(events); 
                ViewBag.Message = "Event added successfully"; 
                return View();
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Displays a list of events.
        /// </summary>
  
        public ActionResult ViewEvents()
        {
            try
            {
                EventRepository repository = new EventRepository();
                List<Events> events = repository.ViewEvents(); 
                return View(events); 
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }
        /// <summary>
        /// Action method for displaying the home page with events for authorized users.
        /// </summary>
   
        public ActionResult HomeEvent()
        {
            try
            {
                EventRepository repository = new EventRepository();
                List<Events> events = repository.ViewEvents();

                string successMessage = TempData["success"] as string;
                string failureMessage = TempData["failure"] as string;

                ViewBag.SuccessMessage = successMessage;
                ViewBag.FailureMessage = failureMessage;

                return View(events);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View("ErrorView");
            }
        }

        /// <summary>
        /// Action method for deleting an event with the specified ID.
        /// </summary>
        /// <param name="Id">The ID of the event to delete.</param>
   
        public ActionResult Delete(int Id)
        {
            try
            {
                EventRepository repository = new EventRepository();
                repository.DeleteEvent(Id);
                return RedirectToAction("ViewEvents");
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// POST action method for handling the form submission for deleting an event.
        /// </summary>
        /// <param name="Id">The ID of the event to delete.</param>
        /// <param name="collection">The form data collection.</param>
        [HttpPost]

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

        /// <summary>
        /// Action method for registering for an event.
        /// </summary>
        /// <param name="id">The ID of the event to register for.</param>
        /// <param name="coachid">The coach's ID for registration.</param>
 
        public ActionResult RegisterEvent(int id, int coachid)
        {
            try
            {
                EventRepository repository = new EventRepository();
                bool eventRegistered = repository.RegisterEvent(id, coachid);
                if (eventRegistered)
                {
                    TempData["success"] = "Successfully registered";
                }
                else
                {
                    TempData["failure"] = "You are already registered for this event.";
                }
                return RedirectToAction("HomeEvent");
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View("ErrorView");
            }
        }

        /// <summary>
        /// Action method for viewing registration data for a specific event.
        /// </summary>
        /// <param name="id">The ID of the event for which registration data is to be viewed.</param>
        [HttpGet]
  
        public ActionResult ViewRegistration(int id)
        {
            try
            {
                var repository = new EventRepository();
                List<Registeration> registerlist = repository.GetRegistrationData(id);
                return View(registerlist);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Action method for viewing events associated with a coach.
        /// </summary>
        /// <param name="id">The ID of the coach for whom events are to be viewed.</param>
      
        public ActionResult ViewMyEvent(int id)
        {
            try
            {
                EventRepository repository = new EventRepository();
                List<Events> events = repository.GetEventsByCoachId(id);

                return View(events);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View("ErrorView");
            }
        }

    }
}
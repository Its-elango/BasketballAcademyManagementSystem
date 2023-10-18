using BasketballAcademy.Models;
using BasketballAcademy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketballAcademy.Controllers
{
  
    public class AdminController : Controller
    {

        // GET: Admin/Details/5
        /// <summary>
        /// Action method to display a list of administrators.
        /// </summary>
        [Authorize]
        public ActionResult ViewAdmin()
        {
            try
            {
                AdminRepository repository = new AdminRepository();
                List<Admin> admins = repository.ViewAdmin();
                return View(admins);
            }
            catch (Exception exception)
            {
               ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// GET action method for displaying the form to add an administrator.
        /// </summary>
        [Authorize]
        public ActionResult AddAdmin()
        {
            return View();
        }

        /// <summary>
        /// Action method for displaying the admin home page.
        /// </summary>
         [Authorize]
        public ActionResult AdminHomePage()
        {
            return View();
        }

        /// <summary>
        /// POST action method for adding an administrator to the database.
        /// </summary>
        /// <param name="admin">The administrator data to add.</param>
        [Authorize]
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            try
            {
                AdminRepository repository = new AdminRepository();
                repository.AddAdmin(admin); 
                ViewBag.Message = "Admin added successfully"; 
                return View();
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// GET action method for deleting an administrator.
        /// </summary>
        /// <param name="id">The ID of the administrator to delete.</param>
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                AdminRepository repository = new AdminRepository();
                repository.DeleteAdmin(id);
                return RedirectToAction("ViewAdmin"); 
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// POST action method for deleting an administrator.
        /// </summary>
        /// <param name="id">The ID of the administrator to delete.</param>
        /// <param name="collection">Form collection data.</param>
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection)
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
        /// Display the contact form.
        /// </summary>
        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// POST action method for submitting a contact form.
        /// </summary>
        /// <param name="contact">The contact information submitted by the user.</param>
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            try
            {
                AdminRepository repository = new AdminRepository();
                repository.Message(contact);
                ViewBag.Message = "Message sent successfully";
                return View();
            }
            catch (Exception exception)
            {           
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Display a view for viewing messages.
        /// </summary>
        [Authorize]
        public ActionResult ViewMessage()
        {
            try
            {            
                AdminRepository repository = new AdminRepository();
                List<Contact> contact = repository.ViewMessage();
                return View(contact);
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Action method for deleting a message.
        /// </summary>
        /// <param name="id">The ID of the message to delete.</param>
        [Authorize]
        public ActionResult DeleteMessage(int id)
        {
            try
            {
                AdminRepository repository = new AdminRepository();
                repository.DeleteMessage(id);
                return RedirectToAction("ViewMessage");
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }
    }
}

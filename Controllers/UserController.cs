using BasketballAcademy.Models;
using BasketballAcademy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketballAcademy.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Displays the user's home page.
        /// </summary>
        [Authorize]
        public ActionResult UserHomePage()
        {
            return View();
        }

        /// <summary>
        /// Displays the page to add a new user (player).
        /// </summary>
 
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// Handles the form submission to register a new user (player).
        /// </summary>
        /// <param name="player">The user (player) information to register.</param>
        public ActionResult AddUser(User player)
        {
            try
            {
                UserRepository repository = new UserRepository();
                if (repository.RegisterUser(player)) 
                {
                    ViewBag.Success = "Signup successfull";
                }
                else
                {
                    ViewBag.Failure = "Email already taken!! Try with another one";
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
        /// Displays a list of all users (players).
        /// </summary>
        [Authorize]
        public ActionResult ViewAll()
        {
            try
            {
                UserRepository repository = new UserRepository();
                List<User> Player = repository.ViewUser(); 
                return View(Player); 
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

        /// <summary>
        /// Deletes a user (player) with the specified ID.
        /// </summary>
        /// <param name="ID">The ID of the user (player) to delete.</param>
        [Authorize]
        public ActionResult Delete(int ID)
        {
            try
            {
                UserRepository repository = new UserRepository();
                repository.DeleteUser(ID);
                return RedirectToAction("ViewAll"); 
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
        /// Handles the form submission for deleting a user (player).
        /// </summary>
        /// <param name="ID">The ID of the user (player) to delete.</param>
        /// <param name="collection">The form data collection.</param>
        public ActionResult Delete(int ID, FormCollection collection)
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
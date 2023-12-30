using BasketballAcademy.Models;
using BasketballAcademy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BasketballAcademy.Controllers
{
    [AllowAnonymous]

    public class CredentialController : Controller
    {
        // GET: Credential
        /// <summary>
        /// GET action method for displaying the sign-in form.
        /// </summary>
        [RequireHttps]
        public ActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// POST action method for processing the sign-in form submission.
        /// </summary>
        /// <param name="credentials">The user's credentials (username and password).</param>
        [HttpPost]
        [RequireHttps]
        public ActionResult SignIn(Credentials credentials)
        {
            try
            {
                CredentialsRepository repository = new CredentialsRepository();
                bool temp = repository.SignIn(credentials, out int result, out int id, out string name, out string email); 
                if (temp)
                {
                    if (result == 0)
                    {
                        FormsAuthentication.SetAuthCookie(credentials.Username, false);
                        return RedirectToAction("AdminHomePage", "Admin", new { id = Session["Id"], name = Session["fullName"] });
                    }
                    else if (result == 2)
                    {
                        FormsAuthentication.SetAuthCookie(credentials.Username, false);
                        return RedirectToAction("CoachHomePage", "Coach", new { id = Session["Id"], name = Session["fullName"], email = Session["email"] });
                    }
                    else if (result == 3)
                    {
                        FormsAuthentication.SetAuthCookie(credentials.Username, false);
                        return RedirectToAction("UserHomePage", "User", new { id = Session["Id"], name = Session["fullName"], email = Session["email"] });
                    }
                }
                else
                {
                    ViewBag.Message = "Invalid credentials";
                }
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View("ErrorView"); 
            }
            return View();
        }

        /// <summary>
        /// Action method for signing out a user.
        /// </summary>
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            
            Session.Clear();    
            Session.Abandon();
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            authCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(authCookie);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("SignIn", "Credential");
        }

        /// <summary>
        /// Display the Forgot Password form.
        /// </summary>
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// POST action method for handling the Forgot Password form submission.
        /// </summary>
        /// <param name="credentials">The user's credentials for password reset.</param>
        [HttpPost]
        public ActionResult ForgotPassword(Credentials credentials)
        {
            try
            {
                CredentialsRepository repository = new CredentialsRepository();
                if (repository.Forgot(credentials))
                {
                    ViewBag.Success = "Password changed successfully";
                    return View();
                }
                else
                {
                    ViewBag.Failure = "Something went wrong...please check your credentials";
                    return View();
                }
            }
            catch (Exception exception)
            {
                ErrorLogger.LogError(exception);
                return View();
            }
        }

    }
}
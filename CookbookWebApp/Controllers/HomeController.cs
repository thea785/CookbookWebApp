using CookbookBLL;
using CookbookWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookbookWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel m)
        {
            if (!ModelState.IsValid)
                return View();

            // Create user in the database
            int userID = UsersBLL.CreateUser(2, m.Email, m.FirstName, m.LastName, m.Password);

            // Set session for new user
            HttpContext.Session.SetInt32("UserID", userID);
            HttpContext.Session.SetString("Email", m.Email);
            HttpContext.Session.SetInt32("RoleID", 2);

            // Go to index
            return RedirectToAction("Index", "Recipes");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyEmail(string email)
        {
            if (UsersBLL.VerifyEmail(email) == false)
            {
                return Json($"Email {email} is already in use.");
            }

            return Json(true);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel m)
        {
            if (!ModelState.IsValid)
                return View();
            int userID, roleID;
            if (UsersBLL.VerifyPassword(m.Email, m.Password, out userID, out roleID) == false)
            {
                ModelState.AddModelError("", "Invalid Email or Password.");
                return View(m);
            }

            // Set session for the user
            HttpContext.Session.SetInt32("UserID", userID);
            HttpContext.Session.SetString("Email", m.Email);
            HttpContext.Session.SetInt32("RoleID", roleID);

            // Go to index
            return RedirectToAction("Index", "Recipes");
        }

        public IActionResult Logout()
        {
            // Set session to Guest
            HttpContext.Session.SetInt32("UserID", 0);
            HttpContext.Session.SetInt32("RoleID", 1);
            HttpContext.Session.SetString("Email", "");
            // Go to index
            return RedirectToAction("Index", "Recipes");
        }

        [HttpGet]
        public IActionResult UpdatePassword()
        {
            // Check for guest user
            if (HttpContext.Session.GetInt32("UserID") == 0)
            {
                // Go to index
                return RedirectToAction("Index", "Recipes");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult UpdatePassword(UpdatePasswordModel m)
        {
            if (!ModelState.IsValid)
                return View();

            // Check for invalid session
            if (HttpContext.Session.GetString("Email") == null || HttpContext.Session.GetString("Email") == "")
                return View();

            // Update password
            UsersBLL.UpdateUserPassword(HttpContext.Session.GetString("Email"), m.Password);

            // Go to index
            return RedirectToAction("Index", "Recipes");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
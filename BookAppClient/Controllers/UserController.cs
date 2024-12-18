using BookAppClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookAppClient.Controllers
{

    public class UserController : Controller
    {
        private readonly HttpClient client;

        public UserController()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:50983/") 
            };
        }

        
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("User/AddUser", content).Result;  

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AuthenticateUser");
                }
            }
            return View(user);
        }

        
        [HttpGet]
        public ActionResult AuthenticateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AuthenticateUser(string email, string password)
        {
            try
            {
                
                HttpResponseMessage response = client.PostAsync($"User/AuthenticateUser/{email}/{password}", null).Result;

              
                User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);

                if (user != null)
                {
                   
                    Session["UserRole"] = user.Role;
                    Session["UserId"] = user.UserId;
                    Session["Name"] = user.Name;
                    Session["Email"] = user.Email;

                   
                    return RedirectToAction("GetAllBooks", "Books");
                }
                else
                {
                   
                    ViewBag.ErrorMessage = "Authentication failed. Please try again.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while authenticating. Please try again later.";
                return View(); 
            }
        }



   
        [HttpGet]
        public ActionResult GetUserDetails(int userid)
        {
            HttpResponseMessage response = client.GetAsync($"User/GetUserDetails/{userid}").Result;  

            if (response.IsSuccessStatusCode)
            {
                User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);  
                return View(user);
            }

            return View("Error", new HandleErrorInfo(new Exception("Failed to fetch user details"), "User", "GetUserDetails"));
        }

        [HttpGet]
        public ActionResult Logout()
        {
            // Clear all session variables
            Session.Clear();  // This will remove all session data

            // Optionally, you can also abandon the session (this removes all session data, even across requests)
            Session.Abandon();

            // Redirect to the login page (or home page)
            return RedirectToAction("AuthenticateUser", "User");
        }





    }
}

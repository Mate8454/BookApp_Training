﻿using BookAppClient.Models;
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
                BaseAddress = new Uri("http://localhost:50983/") // Set API Address
            };
        }

        // ===================== Add User (GET) =====================
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        // ===================== Add User (POST) =====================
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("User/AddUser", content).Result;  // Blocking the async call here

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AuthenticateUser");
                }
            }
            return View(user);
        }

        //[Route("User/GetAllUsers")]
        //public ActionResult GetAllUsers()
        //{
        //    HttpResponseMessage response = client.GetAsync("User/GetAllUsers").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content.ReadAsStringAsync().Result);
        //        return View(users);
        //    }
        //    else
        //    {
        //        return View("Error", new HandleErrorInfo(new Exception("Failed to fetch users"), "User", "GetAllUsers"));
        //    }
        //}

        // ===================== Authenticate User (GET) =====================
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
                // Call the backend API to authenticate the user synchronously
                HttpResponseMessage response = client.PostAsync($"User/AuthenticateUser/{email}/{password}", null).Result;

                // Deserialize the response into a User object
                User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);

                if (user != null)
                {
                    // If authentication is successful, store the user's role and ID in the session
                    Session["UserRole"] = user.Role;
                    Session["UserId"] = user.UserId;
                    Session["Name"] = user.Name;
                    Session["Email"] = user.Email;

                    // Redirect to the "GetAllBooks" action in the "Books" controller
                    return RedirectToAction("GetAllBooks", "Books");
                }
                else
                {
                    // If authentication fails, show an error message
                    ViewBag.ErrorMessage = "Authentication failed. Please try again.";
                    return View(); // Return the login form view with the error message
                }
            }
            catch (Exception ex)
            {
                // If an error occurs during the authentication process
                ViewBag.ErrorMessage = "An error occurred while authenticating. Please try again later.";
                return View(); // Return the login form view with the error message
            }
        }



        // ===================== Get User Details =====================
        [HttpGet]
        public ActionResult GetUserDetails(int userid)
        {
            HttpResponseMessage response = client.GetAsync($"User/GetUserDetails/{userid}").Result;  // Blocking the async call here

            if (response.IsSuccessStatusCode)
            {
                User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);  // Blocking the async call for reading content
                return View(user);
            }

            return View("Error", new HandleErrorInfo(new Exception("Failed to fetch user details"), "User", "GetUserDetails"));
        }


        // ===================== Update User (GET) =====================
        [HttpGet]
        public ActionResult UpdateUser(int userid )
        {
            HttpResponseMessage response = client.GetAsync($"User/GetUserDetails/{userid}").Result;  // Blocking async call

            if (response.IsSuccessStatusCode)
            {
                User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);  // Blocking async call for reading content
                return View(user);
            }

            return View("Error", new HandleErrorInfo(new Exception("Failed to fetch user for update"), "User", "UpdateUser"));
        }

        // ===================== Update User (POST) =====================
        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("User/UpdateUser", content).Result;  // Blocking async call

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetUserDetails", new { userid = user.UserId });
                }
            }

            return View(user);
        }
    }
}

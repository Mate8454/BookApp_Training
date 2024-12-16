using BookAppClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookAppClient.Controllers
{
    public class CartController : Controller
    {
        private HttpClient _client;
        private string _apiBaseUrl = "http://localhost:50983/"; // Backend API URL

        public CartController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_apiBaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Action method to add a book to the cart
        [HttpPost]
        public ActionResult AddToCart(int bookId)
        {
            try
            {
                // Ensure the user is logged in
                var userId = (int?)Session["UserId"];
                if (userId == null)
                {
                    TempData["Message"] = "User not logged in. Please log in to add items to the cart.";
                    return RedirectToAction("AuthenticateUser", "User");
                }

                // Create the Cart object to send to the backend
                var cart = new Cart
                {
                    UserId = userId.Value,
                    BookId = bookId,
                    Quantity = 1 // Default quantity for now
                };

                // Serialize the Cart object into JSON
                var jsonContent = JsonConvert.SerializeObject(cart);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send POST request to the backend API to add the item to the cart
                var response = _client.PostAsync("Cart/AddToCart", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Book added to cart successfully!";
                }
                else
                {
                    TempData["Message"] = "Error adding book to cart.";
                }

                // Redirect to GetCartItems to show the updated cart
                return RedirectToAction("GetAllBooks","Books");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while adding the book to the cart.";
                return RedirectToAction("GetCartItems", "Cart");
            }
        }




        // Action method to view the cart items
       public ActionResult GetCartItems(int userId)
{
    try
    {
        // Validate userId to make sure it's valid
        if (userId <= 0)
        {
            TempData["Message"] = "Invalid user. Please log in again.";
            return RedirectToAction("Login", "Account"); // Redirect to the login page if userId is invalid
        }

        // Make a synchronous request to fetch cart items
        var response = _client.GetAsync($"Cart/GetCartItems?userId={userId}").Result;

        if (response.IsSuccessStatusCode)
        {
            // Deserialize the response content into a list of Cart items
            var cartItems = JsonConvert.DeserializeObject<List<Cart>>(response.Content.ReadAsStringAsync().Result);
            return View(cartItems); // Pass cart items to the view
        }
        else
        {
            // If API response is not successful, return empty list
            TempData["Message"] = "Failed to load cart items.";
            return View(new List<Cart>());
        }
    }
    catch (Exception ex)
    {
        // Log the exception (optional) for debugging purposes
        System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

        TempData["Message"] = "An error occurred while loading the cart.";
        return View(new List<Cart>()); // Return an empty list in case of error
    }
}







    }
}

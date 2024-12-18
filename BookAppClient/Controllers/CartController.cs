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
        private string _apiBaseUrl = "http://localhost:50983/";

        public CartController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_apiBaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

       
        [HttpPost]
        public ActionResult AddToCart(int bookId)
        {
            try
            {
               
                var userId = (int?)Session["UserId"];
                if (userId == null)
                {
                    TempData["Message"] = "User not logged in. Please log in to add items to the cart.";
                    return RedirectToAction("AuthenticateUser", "User");
                }

              
                var cart = new Cart
                {
                    UserId = userId.Value,
                    BookId = bookId,
                    Quantity = 1 
                };

                
                var jsonContent = JsonConvert.SerializeObject(cart);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

               
                var response = _client.PostAsync("Cart/AddToCart", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Book added to cart successfully!";
                }
                else
                {
                    TempData["Message"] = "Error adding book to cart.";
                }

               
                return RedirectToAction("GetAllBooks", "Books");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while adding the book to the cart.";
                return RedirectToAction("GetCartItems", "Cart");
            }
        }



   
        public ActionResult GetCartItems(int userId)
    {
        try
        {
          
            if (userId <= 0)
            {
                TempData["Message"] = "Invalid user. Please log in again.";
                return RedirectToAction("Login", "Account"); 
            }

          
            var response = _client.GetAsync($"Cart/GetCartItems?userId={userId}").Result;

            if (response.IsSuccessStatusCode)
            {
               
                var cartItems = JsonConvert.DeserializeObject<List<Cart>>(response.Content.ReadAsStringAsync().Result);
                    Session["CartItems"] = cartItems;
                    return View(cartItems); 
            }
            else
            {
                
                TempData["Message"] = "Failed to load cart items.";
                return View(new List<Cart>());
            }
        }
        catch (Exception ex)
        {
           
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

            TempData["Message"] = "An error occurred while loading the cart.";
            return View(new List<Cart>()); 
        }
    }
        [HttpPost]
        public ActionResult DeleteCartItem(int userId, int bookId,int cartId)
        {
            HttpResponseMessage res = _client.DeleteAsync($"Cart/DeleteCartItem?userId={userId}&bookId={bookId}").Result;
           
            return RedirectToAction("GetCartItems", new { userId = userId });
        }

        public ActionResult Checkout()
        {
            return View();
        }


    }
}

using BookAppClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BookAppClient.Controllers
{
    [RoutePrefix("Orders")]
    public class OrdersController : Controller
    {
        // GET: Orders
        HttpClient client;
        public OrdersController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50983/");
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PlaceOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PlaceOrder(int userId, string deliveryAddress)
        {
            try
            {
                // Ensure deliveryAddress is not empty
                if (string.IsNullOrEmpty(deliveryAddress))
                {
                    TempData["ErrorMessage"] = "Delivery address cannot be empty.";
                    return RedirectToAction("Checkout");
                }

                // Create the order object with userId and deliveryAddress
                //Orders newOrder = new Orders
                //{
                //    UserId = userId,
                //    OrderDate = DateTime.Now,
                //    DeliveryAddress = deliveryAddress,
                //    Status = "Pending", // Default status
                //    TotalPrice = 0  // Initially set to 0, will be updated after calculation
                //};

                // Serialize the order object to JSON for the API request
               // StringContent content = new StringContent(JsonConvert.SerializeObject(newOrder), System.Text.Encoding.UTF8, "application/json");

                // Send POST request to the API
                HttpResponseMessage response = client.PostAsync($"Orders/PlaceOrder/"+userId+"/"+deliveryAddress, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var order = JsonConvert.DeserializeObject<Orders>(result);
                    TempData["SuccessMessage"] = "Order placed successfully!";

                    // Redirect to the order details page after order is placed
                    return RedirectToAction("GetOrderById", new { orderId = order.OrderId });
                }
                else
                {
                    // Handle failure
                    TempData["ErrorMessage"] = $"Error placing order: {response.ReasonPhrase}";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Exception: {ex.Message}";
                return View();
            }
        }






        [Route("GetOrdersByUserId/{userId}")]
        public ActionResult GetOrdersByUserId(int userId)
        {
            HttpResponseMessage response = client.GetAsync($"Orders/GetOrdersByUserId/{userId}").Result;
            List<Orders> orders = JsonConvert.DeserializeObject<List<Orders>>(response.Content.ReadAsStringAsync().Result);
            return View(orders);
        }

        [Route("GetOrderById/{orderId}")]
        public ActionResult GetOrderById(int orderId)
        {
            HttpResponseMessage response = client.GetAsync($"Orders/GetOrderById/{orderId}").Result;
            Orders order = JsonConvert.DeserializeObject<Orders>(response.Content.ReadAsStringAsync().Result);
            return View(order);
        }


    }
}
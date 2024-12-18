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
                
                if (string.IsNullOrEmpty(deliveryAddress))
                {
                    TempData["ErrorMessage"] = "Delivery address cannot be empty.";
                    return RedirectToAction("Checkout");
                }

               
                HttpResponseMessage response = client.PostAsync($"Orders/PlaceOrder/"+userId+"/"+deliveryAddress, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var order = JsonConvert.DeserializeObject<Orders>(result);
                    TempData["SuccessMessage"] = "Order placed successfully!";

                   
                    return RedirectToAction("GetOrderById", new { orderId = order.OrderId });
                }
                else
                {
                   
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



        [Route("GetAllOrders")]

        public ActionResult GetAllOrders()
        {
            HttpResponseMessage response = client.GetAsync("Orders/GetAllOrders").Result;
            List<Orders> orders = JsonConvert.DeserializeObject<List<Orders>>(response.Content.ReadAsStringAsync().Result);
            return View(orders);
        }

        [Route("UpdateOrderStatus")]
        public ActionResult UpdateOrderStatus(int orderId)
        {
            HttpResponseMessage response = client.GetAsync("Orders/GetOrderById/" + orderId).Result;

            if (!response.IsSuccessStatusCode)
            {
               
                ModelState.AddModelError("", "Error retrieving order data. Please try again later.");
                return View();  
            }

            try
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;

               
                Orders order = JsonConvert.DeserializeObject<Orders>(responseContent);
                return View(order);
            }
            catch (JsonReaderException ex)
            {
           
                ModelState.AddModelError("", "Error parsing order data.");
                return View();  
            }
        }


        [HttpPost, Route("UpdateOrderStatus")]

        public ActionResult UpdateOrderStatus(Orders orders)
        {

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(orders), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("Orders/UpdateOrderStatus", content).Result;
                return RedirectToAction("GetAllOrders");

            }
            else
            {
                return View();
            }
        }


    }
}
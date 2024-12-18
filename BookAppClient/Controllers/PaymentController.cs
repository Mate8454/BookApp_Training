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
    

        [RoutePrefix("Payment")]
        public class PaymentController : Controller
        {
            HttpClient client;

            
            public PaymentController()
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:50983");  
            }

        [HttpGet]
        public ActionResult AddPayment(int orderId)
        {
            
            var payment = new Payment
            {
                OrderId = orderId,  
                PaymentDate = DateTime.Now  
            };

           
            return View(payment);

        }
        [HttpPost]

            public ActionResult AddPayment(Payment payment)
            {
                if (ModelState.IsValid)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(payment), System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("Payment/AddPayment", content).Result;
                    return RedirectToAction("GetAllBooks","Books");
                }
                else
                {
                    return View();
                }
            }

            [Route("GetPaymentByOrderId/{orderId}")]
            public ActionResult GetPaymentByOrderId(int orderId)
            {
                HttpResponseMessage response = client.GetAsync($"Payment/GetPaymentByOrderId/{orderId}").Result;
                Payment payment = JsonConvert.DeserializeObject<Payment>(response.Content.ReadAsStringAsync().Result);
                return View(payment);
            }

            [Route("GetAllPayments")]
            public ActionResult GetAllPayments()
            {
                HttpResponseMessage response = client.GetAsync($"Payment/GetAllPayments").Result;
                List<Payment> payment = JsonConvert.DeserializeObject<List<Payment>>(response.Content.ReadAsStringAsync().Result);
                return View(payment);
            }
        }
    }

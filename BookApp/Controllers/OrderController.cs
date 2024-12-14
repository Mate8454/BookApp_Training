using BookApp.Entities;
using BookApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookApp.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : ApiController
    {
        private readonly OrderRepository _orderRepository;

        // Constructor initializes the OrderRepository
        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }

        // POST method to place an order
        [HttpPost]
        [Route("placeorder")]
        public IHttpActionResult PlaceOrder(int userId, string deliveryAddress)
        {
            try
            {
                if (string.IsNullOrEmpty(deliveryAddress))
                {
                    return BadRequest("Delivery address is required.");
                }

                var order = _orderRepository.PlaceOrder(userId, deliveryAddress);

                if (order == null)
                {
                    return BadRequest("Failed to place the order.");
                }

                return Ok(order); // Return the order details
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Return internal server error if something fails
            }
        }

        // GET method to get an order by its OrderId
        [HttpGet]
        [Route("get/{orderId}")]
        public IHttpActionResult GetOrderById(int orderId)
        {
            try
            {
                var order = _orderRepository.GetOrderById(orderId);

                if (order == null)
                {
                    return NotFound(); // Return 404 if order not found
                }

                return Ok(order); // Return the order details
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Return internal server error if something fails
            }
        }

        // GET method to get all orders by UserId
        [HttpGet]
        [Route("userorders/{userId}")]
        public IHttpActionResult GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = _orderRepository.GetOrdersByUserId(userId);

                if (orders == null || !orders.Any())
                {
                    return NotFound(); // Return 404 if no orders found
                }

                return Ok(orders); // Return all orders for the user
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Return internal server error if something fails
            }
        }
    }
}

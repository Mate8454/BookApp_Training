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
    [RoutePrefix("Orders")]
    public class OrderController : ApiController
    {
        private readonly OrderRepository _orderRepository;

       
        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }

      
        [HttpPost]
        [Route("PlaceOrder/{userId}/{deliveryAddress}")]
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

                return Ok(order); 
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); 
            }
        }

       
        [HttpGet]
        [Route("GetOrderById/{orderId}")]
        public IHttpActionResult GetOrderById(int orderId)
        {
            try
            {
                var order = _orderRepository.GetOrderById(orderId);

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order); 
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); 
            }
        }

        
        [HttpGet]
        [Route("GetOrdersByUserId/{userId}")]
        public IHttpActionResult GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = _orderRepository.GetOrdersByUserId(userId);

                if (orders == null || !orders.Any())
                {
                    return NotFound(); 
                }

                return Ok(orders); 
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); 
            }
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public IHttpActionResult GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            return Ok(orders);
        }

        [HttpPut]
        [Route("UpdateOrderStatus")]
        public IHttpActionResult UpdateOrderStatus([FromBody]Orders orders)
        {
            _orderRepository.UpdateOrderStatus(orders);
            return Ok("Order status Updated Successfully");
        }
    }
}

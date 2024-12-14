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
    [RoutePrefix("orderitems")]
    public class OrderItemController : ApiController
    {
        private readonly OrderItemRepository _orderItemRepository;

        public OrderItemController()
        {
            _orderItemRepository = new OrderItemRepository();
        }

        // POST: api/orderitems/add
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddOrderItem(OrderItem orderItem)
        {
            try
            {
                if (orderItem == null)
                {
                    return BadRequest("Order item cannot be null.");
                }

                // Add order item to the database
                _orderItemRepository.AddOrderItem(orderItem);
                return Ok("Order item added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);  // Handle error and return proper response
            }
        }

        // DELETE: api/orderitems/remove/{orderItemId}
        [HttpDelete]
        [Route("remove/{orderItemId}")]
        public IHttpActionResult RemoveOrderItem(int orderItemId)
        {
            try
            {
                // Remove the order item from the database
                _orderItemRepository.RemoveOrderItem(orderItemId);
                return Ok("Order item removed successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);  // Handle error and return proper response
            }
        }

        // GET: api/orderitems/{orderId}
        [HttpGet]
        [Route("{orderId}")]
        public IHttpActionResult GetAllOrderItems(int orderId)
        {
            try
            {
                // Get all order items for the given OrderId
                var orderItems = _orderItemRepository.GetAllOrderItems(orderId);

                if (orderItems == null || !orderItems.Any())
                {
                    return NotFound();  // Return 404 if no order items found
                }

                return Ok(orderItems);  // Return 200 OK with the list of order items
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);  // Handle error and return proper response
            }
        }
    }
}

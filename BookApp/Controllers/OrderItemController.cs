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

               
                _orderItemRepository.AddOrderItem(orderItem);
                return Ok("Order item added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);  
            }
        }

        
        [HttpDelete]
        [Route("remove/{orderItemId}")]
        public IHttpActionResult RemoveOrderItem(int orderItemId)
        {
            try
            {
              
                _orderItemRepository.RemoveOrderItem(orderItemId);
                return Ok("Order item removed successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); 
            }
        }

        
        [HttpGet]
        [Route("{orderId}")]
        public IHttpActionResult GetAllOrderItems(int orderId)
        {
            try
            {
               
                var orderItems = _orderItemRepository.GetAllOrderItems(orderId);

                if (orderItems == null || !orderItems.Any())
                {
                    return NotFound();  
                }

                return Ok(orderItems); 
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);  
            }
        }
    }
}

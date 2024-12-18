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
    [RoutePrefix("Cart")]
    public class CartController : ApiController
    {
        private  CartRepository cartRepository;

        
        public CartController()
        {
            cartRepository = new CartRepository(); 
        }

       

        [HttpPost]
        [Route("AddToCart")]
        public IHttpActionResult AddToCart(Cart cart)
        {
            try
            {
                if (cart == null || cart.Quantity <= 0)
                {
                    return BadRequest("Invalid cart data.");
                }

               
                var existingCartItem = cartRepository.GetCartItemByUserAndBook(cart.UserId, cart.BookId);

                if (existingCartItem != null)
                {
                    
                    existingCartItem.Quantity += cart.Quantity; 
                    cartRepository.UpdateCartItem(existingCartItem);
                }
                else
                {
                    
                    cartRepository.AddToCart(cart);
                }

                return Ok("Book added to cart successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

       
        [HttpDelete]
        [Route("DeleteCartItem")]
        public IHttpActionResult DeleteCartItem(int userId, int bookId)
        {
            try
            {
                cartRepository.DeleteCartItem(userId, bookId);
                return Ok("Book removed from cart successfully.");
            }
            catch (InvalidOperationException)
            {
                return NotFound();  
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

       
        [HttpGet]
        [Route("GetCartItems")]
        public IHttpActionResult GetCartItems(int userId)
        {
            try
            {
                var cartItems = cartRepository.GetCartItems(userId);

                if (cartItems == null || !cartItems.Any())
                {
                    return NotFound();  
                }

                return Ok(cartItems);  
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

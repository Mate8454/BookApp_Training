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

        // If you are using DI, this constructor would be automatically injected.
        public CartController()
        {
            cartRepository = new CartRepository(); // In a real scenario, inject BookContext
        }

        // Add a book to the cart
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

                cartRepository.AddToCart(cart);
                return Ok("Book added to cart successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Delete a book from the cart
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
                return NotFound();  // Item not found
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Get all cart items for a user
        [HttpGet]
        [Route("GetCartItems")]
        public IHttpActionResult GetCartItems(int userId)
        {
            try
            {
                var cartItems = cartRepository.GetCartItems(userId);

                if (cartItems == null || !cartItems.Any())
                {
                    return NotFound();  // No items found in cart
                }

                return Ok(cartItems);  // Return cart items
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

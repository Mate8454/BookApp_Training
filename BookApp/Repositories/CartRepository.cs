using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity; // Ensure Entity Framework is included

namespace BookApp.Repositories
{
    public class CartRepository : ICartRepository
    {
        private  BookContext context;

        public CartRepository()
        {
            context = new BookContext();
        }

        public void AddToCart(Cart cart)
        {
            try
            {
               
                context.Cart.Add(cart);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding item to cart.", ex);
            }
        }

        public void DeleteCartItem(int userId, int bookId)
        {
            try
            {
                var cartItem = context.Cart
                                      .SingleOrDefault(e => e.UserId == userId && e.BookId == bookId);

                if (cartItem != null)
                {
                    context.Cart.Remove(cartItem);  // Remove item
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Cart item not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting item from cart.", ex);
            }
        }

        public List<Cart> GetCartItems(int userId)
        {
            try
            {
                // Include related Book details when fetching cart items
                var cartItems = context.Cart
                                       .Where(e => e.UserId == userId)
                                       .Include(e => e.Book)  // Include related Book details
                                       .ToList();

                return cartItems;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving cart items.", ex);
            }
        }

        //qunt
        public Cart GetCartItemByUserAndBook(int userId, int bookId)
        {
            // Check if the user already has the book in the cart
            return context.Cart.FirstOrDefault(ci => ci.UserId == userId && ci.BookId == bookId);
        }

        public void UpdateCartItem(Cart cart)
        {
            // Update the existing cart item with the new quantity
            context.Entry(cart).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}

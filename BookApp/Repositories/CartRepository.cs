﻿using BookApp.Entities;
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
                var existingCartItem = context.Cart
                                              .FirstOrDefault(e => e.UserId == cart.UserId && e.BookId == cart.BookId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += cart.Quantity;  // Increment quantity
                }
                else
                {
                    context.Cart.Add(cart);  // Add new item to cart
                }

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
    }
}
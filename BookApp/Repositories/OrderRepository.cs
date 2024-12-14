using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;  // For Entity Framework Include method (EF 6)

namespace BookApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookContext _context;

        public OrderRepository()
        {
            _context = new BookContext();
        }

        // Place an order for the user
        public Orders PlaceOrder(int userId, string deliveryAddress)
        {
            // Create a new order object
            var order = new Orders
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                DeliveryAddress = deliveryAddress,
                Status = "Placed",  // The order is placed, awaiting processing
                OrderItems = new List<OrderItem>(),  // Initialize the order items list
                TotalPrice = 0  // Initially set to 0, will be updated after calculation
            };

            // Get the user's cart items (include the books and quantity)
            var cartItems = _context.Cart
                                    .Where(c => c.UserId == userId)
                                    .Include(c => c.Book)  // Include book details (Entity Framework Eager Loading)
                                    .ToList();

            double totalPrice = 0;

            // Create order items from cart items
            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity,
                    OrderItemPrice = cartItem.Quantity * cartItem.Book.Price,  // Price of each order item
                    OrderId = order.OrderId  // Associate with the current order
                };

                totalPrice += orderItem.OrderItemPrice;  // Add to the total price

                // Add the order item to the order's list of items
                order.OrderItems.Add(orderItem);
            }

            // Set the total price of the order
            order.TotalPrice = (int)totalPrice;  // Explicitly cast the double to int

            // Add the order to the database
            _context.Orders.Add(order);

            // Now that the order is placed, remove the cart items
            foreach (var cartItem in cartItems)
            {
                _context.Cart.Remove(cartItem);
            }

            // Save changes to the database
            _context.SaveChanges();

            // Eager load OrderItems and User after saving to get the full data (related entities)
            var fullOrder = _context.Orders
                                    .Include(o => o.User)  // Include the User details
                                    .Include(o => o.OrderItems)  // Include the OrderItems
                                    .Include(o => o.OrderItems.Select(oi => oi.Books))  // Include books in order items
                                    .FirstOrDefault(o => o.OrderId == order.OrderId);

            return fullOrder;  // Return the newly created and fully loaded order
        }


        // Get an order by its ID
        public Orders GetOrderById(int orderId)
        {
            // Retrieve the order along with its order items and related book details
            return _context.Orders
                           .Include(o => o.OrderItems)  // Include order items
                           .Include(o => o.OrderItems.Select(oi => oi.Books))  // Include books in order items
                           .FirstOrDefault(o => o.OrderId == orderId);
        }

        // Get all orders for a specific user
        public List<Orders> GetOrdersByUserId(int userId)
        {
            // Retrieve the orders for the user with all associated order items and book details
            return _context.Orders
                           .Where(o => o.UserId == userId)
                           .Include(o => o.OrderItems)  // Include order items
                           .Include(o => o.OrderItems.Select(oi => oi.Books))  // Include books in order items
                           .ToList();
        }
    }
}

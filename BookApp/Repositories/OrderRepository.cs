using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;  

namespace BookApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookContext _context;

        public OrderRepository()
        {
            _context = new BookContext();
        }

        
        public Orders PlaceOrder(int userId, string deliveryAddress)
        {
           
            var order = new Orders
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                DeliveryAddress = deliveryAddress,
                Status = "Placed",  
                OrderItems = new List<OrderItem>(),  
                TotalPrice = 0  
            };

           
            var cartItems = _context.Cart
                                    .Where(c => c.UserId == userId)
                                    .Include(c => c.Book) 
                                    .ToList();

            double totalPrice = 0;

            
            foreach (var cartItem in cartItems)
            {
                 
                

                var orderItem = new OrderItem
                {
                    
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity,
                    OrderItemPrice = cartItem.Quantity * cartItem.Book.Price,
                    
                };
                totalPrice += orderItem.OrderItemPrice;

               
                order.OrderItems.Add(orderItem);
            }

          
            order.TotalPrice = (int)totalPrice;  

          
            _context.Orders.Add(order);

           
            _context.SaveChanges(); 

            //assign the OrderId to each OrderItem and save them
            foreach (var orderItem in order.OrderItems)
            {
                orderItem.OrderId = order.OrderId;  
                _context.OrderItem.Add(orderItem); 
            }

            // remove the cart items
            foreach (var cartItem in cartItems)
            {
                _context.Cart.Remove(cartItem);
            }

            
            _context.SaveChanges();

           
            var fullOrder = _context.Orders
                                    .Include(o => o.User)  
                                    .Include(o => o.OrderItems)  
                                    .Include(o => o.OrderItems.Select(oi => oi.Books))  
                                    .FirstOrDefault(o => o.OrderId == order.OrderId);

            return fullOrder;  
        }


       
        public Orders GetOrderById(int orderId)
        {
           
            return _context.Orders
                           .Include(o => o.OrderItems)  
                           .Include(o => o.OrderItems.Select(oi => oi.Books))  
                           .FirstOrDefault(o => o.OrderId == orderId);
        }

        
        public List<Orders> GetOrdersByUserId(int userId)
        {
          
            return _context.Orders
                           .Where(o => o.UserId == userId)
                           .Include(o => o.OrderItems)  
                           .Include(o => o.OrderItems.Select(oi => oi.Books))  
                           .ToList();
        }

        public List<Orders> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void UpdateOrderStatus(Orders orders)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(b => b.OrderId == orders.OrderId);

                if (order != null)
                {

                    order.Status = orders.Status;

                    


                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

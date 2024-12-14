using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BookApp.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly BookContext _context;

        public OrderItemRepository()
        {
            _context = new BookContext();
        }

        // Add OrderItem to the database
        public void AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItem.Add(orderItem);
            _context.SaveChanges();
        }

        // Remove an OrderItem by OrderItemId
        public void RemoveOrderItem(int orderItemId)
        {
            var orderItem = _context.OrderItem.SingleOrDefault(o => o.OrderItemId == orderItemId);
            if (orderItem != null)
            {
                _context.OrderItem.Remove(orderItem);
                _context.SaveChanges();
            }
        }

        // Get all OrderItems for a specific OrderId
        public List<OrderItem> GetAllOrderItems(int orderId)
        {
            return _context.OrderItem
                           .Where(o => o.OrderId == orderId)
                           .Include(o => o.Books)  // Include book details
                           .ToList();
        }
    }
}

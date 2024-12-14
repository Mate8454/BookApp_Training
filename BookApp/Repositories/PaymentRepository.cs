using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BookApp.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BookContext _context;

        public PaymentRepository()
        {
            _context = new BookContext();
        }

        // Add a payment record to the database
        public void AddPayment(Payment payment)
        {
            try
            {
                _context.Payment.Add(payment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle error (e.g., log it or rethrow)
                throw new Exception("Error while adding payment", ex);
            }
        }

        // Get payment details by OrderId
        public Payment GetPaymentByOrderId(int orderId)
        {
            try
            {
                return _context.Payment
                               .Include(p => p.Orders)  // Eager load the associated Order
                               .FirstOrDefault(p => p.OrderId == orderId);
            }
            catch (Exception ex)
            {
                // Handle error
                throw new Exception("Error while fetching payment details", ex);
            }
        }

        // Get all payments
        public List<Payment> GetAllPayments()
        {
            try
            {
                return _context.Payment.ToList();
            }
            catch (Exception ex)
            {
                // Handle error
                throw new Exception("Error while fetching payments", ex);
            }
        }
    }
}

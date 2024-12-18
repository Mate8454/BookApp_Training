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

       
        public void AddPayment(Payment payment)
        {
            try
            {
                _context.Payment.Add(payment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error while adding payment", ex);
            }
        }

       
        public Payment GetPaymentByOrderId(int orderId)
        {
            try
            {
                return _context.Payment
                               .Include(p => p.Orders)  
                               .FirstOrDefault(p => p.OrderId == orderId);
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error while fetching payment details", ex);
            }
        }

      
        public List<Payment> GetAllPayments()
        {
            try
            {
                return _context.Payment.ToList();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error while fetching payments", ex);
            }
        }
    }
}

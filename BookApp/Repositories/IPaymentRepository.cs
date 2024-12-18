using BookApp.Entities;
using System;
using System.Collections.Generic;

namespace BookApp.Repositories
{
    public interface IPaymentRepository
    {
       
        void AddPayment(Payment payment);

       
        Payment GetPaymentByOrderId(int orderId);

        
        List<Payment> GetAllPayments();
    }
}

using BookApp.Entities;
using System;
using System.Collections.Generic;

namespace BookApp.Repositories
{
    public interface IPaymentRepository
    {
        // Add a payment record to the database
        void AddPayment(Payment payment);

        // Get payment details by OrderId
        Payment GetPaymentByOrderId(int orderId);

        // Get all payments
        List<Payment> GetAllPayments();
    }
}

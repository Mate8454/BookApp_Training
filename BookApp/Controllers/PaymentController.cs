using BookApp.Entities;
using BookApp.Repositories;
using System;
using System.Web.Http;

namespace BookApp.Controllers
{
    [RoutePrefix("Payment")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentRepository _paymentRepository;

        // Constructor for dependency injection
        public PaymentController()
        {
            _paymentRepository = new PaymentRepository();
        }

        // POST: api/Payment/AddPayment
        [Route("AddPayment")]
        [HttpPost]
        public IHttpActionResult AddPayment(Payment payment)
        {
            try
            {
                if (payment == null)
                {
                    return BadRequest("Invalid payment details");
                }

                _paymentRepository.AddPayment(payment);
                return Ok("Payment added successfully");
            }
            catch (Exception ex)
            {
                // Log exception or rethrow custom exception
                return InternalServerError(ex);
            }
        }

        // GET: api/Payment/GetPaymentByOrderId/{orderId}
        [Route("GetPaymentByOrderId/{orderId}")]
        [HttpGet]
        public IHttpActionResult GetPaymentByOrderId(int orderId)
        {
            try
            {
                var payment = _paymentRepository.GetPaymentByOrderId(orderId);

                if (payment == null)
                {
                    return NotFound();  // Payment not found for the given orderId
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                // Log exception or rethrow custom exception
                return InternalServerError(ex);
            }
        }

        // GET: api/Payment/GetAllPayments
        [Route("GetAllPayments")]
        [HttpGet]
        public IHttpActionResult GetAllPayments()
        {
            try
            {
                var payments = _paymentRepository.GetAllPayments();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                // Log exception or rethrow custom exception
                return InternalServerError(ex);
            }
        }
    }
}

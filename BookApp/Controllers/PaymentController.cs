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

       
        public PaymentController()
        {
            _paymentRepository = new PaymentRepository();
        }

       
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
               
                return InternalServerError(ex);
            }
        }

        
        [Route("GetPaymentByOrderId/{orderId}")]
        [HttpGet]
        public IHttpActionResult GetPaymentByOrderId(int orderId)
        {
            try
            {
                var payment = _paymentRepository.GetPaymentByOrderId(orderId);

                if (payment == null)
                {
                    return NotFound();  
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
              
                return InternalServerError(ex);
            }
        }

   
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
              
                return InternalServerError(ex);
            }
        }
    }
}

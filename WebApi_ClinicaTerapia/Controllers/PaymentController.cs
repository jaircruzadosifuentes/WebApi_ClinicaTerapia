using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_ClinicaTerapia.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet("GetPayments")]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            var result = _paymentService.GetPayments();
            return Ok(result);
        } 
        [HttpGet("GetDetailPayPendingGetByIdPayment/{paymentId}")]
        public ActionResult<IEnumerable<Payment>> GetDetailPayPendingGetByIdPayment(int paymentId)
        {
            var result = _paymentService.GetDetailPayPendingGetByIdPayment(paymentId);
            return Ok(result);
        } 
        [HttpGet("GetPaymentsScheduleDetail/{paymentId}")]
        public ActionResult<IEnumerable<PaymentScheduleDetail>> GetPaymentsScheduleDetail(int paymentId)
        {
            var result = _paymentService.GetPaymentsScheduleDetail(paymentId);
            return Ok(result);
        }
        [HttpPut("PutUpdateDebtPayment")]
        public void PutUpdateDebtPayment(PaymentScheduleDetail paymentScheduleDetail)
        {
            _paymentService.PutUpdateDebtPayment(paymentScheduleDetail);
        }
    }
}

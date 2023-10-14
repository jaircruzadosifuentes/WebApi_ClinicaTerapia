using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetPayments();
        bool PutUpdateDebtPayment(PaymentScheduleDetail paymentScheduleDetail);
    }
    public class PaymentService : IPaymentService
    {
        private IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Payment> GetPayments()
        {
            using var context = _unitOfWork.Create();
           
            var payments = context.Repositories.PaymentRepository.GetPayments();
            foreach (var payment in payments)
            {
                payment.PaymentScheduleDetails = (List<PaymentScheduleDetail>)context.Repositories.PaymentRepository.GetPaymentsScheduleDetail(payment.PaymentId);
            }
            return payments;
           
        }

        public bool PutUpdateDebtPayment(PaymentScheduleDetail paymentScheduleDetail)
        {
            using var context = _unitOfWork.Create();
            bool update = context.Repositories.PaymentRepository.PutUpdateDebtPayment(paymentScheduleDetail);
            context.SaveChanges();
            return update;
        }
    }
}

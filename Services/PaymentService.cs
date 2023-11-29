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
        IEnumerable<Payment> GetDetailPayPendingGetByIdPayment(int paymentId);
        IEnumerable<PaymentScheduleDetail> GetPaymentsScheduleDetail(int paymentId);
    }
    public class PaymentService : IPaymentService
    {
        private IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Payment> GetDetailPayPendingGetByIdPayment(int paymentId)
        {
            using var context = _unitOfWork.Create();
            var payments = context.Repositories.PaymentRepository.GetDetailPayPendingGetByIdPayment(paymentId);
            return payments;
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

        public IEnumerable<PaymentScheduleDetail> GetPaymentsScheduleDetail(int paymentId)
        {
            using var context = _unitOfWork.Create();
            var payments = context.Repositories.PaymentRepository.GetPaymentsScheduleDetail(paymentId);
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

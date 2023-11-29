using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetPayments();
        IEnumerable<Payment> GetDetailPayPendingGetByIdPayment(int paymentId);
        IEnumerable<PaymentScheduleDetail> GetPaymentsScheduleDetail(int paymentId);
        bool PutUpdateDebtPayment(PaymentScheduleDetail paymentScheduleDetail);

    }
}

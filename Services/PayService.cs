using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IPayService
    {
        bool PostInsertPaySolicitud(Pay pay);
        IEnumerable<PayDuesDetail> GetPayDueDetailForPatientId(int patientId);
    }
    public class PayService : IPayService
    {
        private IUnitOfWork _unitOfWork;

        public PayService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PayDuesDetail> GetPayDueDetailForPatientId(int patientId)
        {
            using var context = _unitOfWork.Create();
            var payDueDetails = context.Repositories.PayRepository.GetPayDueDetailForPatientId(patientId);
            foreach (var pay in payDueDetails)
            {
                pay.PayDuesDetailHistories = (List<PayDuesDetailHistory>)context.Repositories.PayRepository.GetPayDueDetailForPatientIdHistory((int)pay.PayDuesDetailId);

            }
            return payDueDetails;
        }

        public bool PostInsertPaySolicitud(Pay pay)
        {
            using var context = _unitOfWork.Create();
            bool insert = context.Repositories.PayRepository.PostInsertPaySolicitud(pay);
            context.SaveChanges();
            return insert;
        }
    }
}

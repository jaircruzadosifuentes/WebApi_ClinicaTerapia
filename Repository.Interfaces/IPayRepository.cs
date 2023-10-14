using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPayRepository
    {
        bool PostInsertPaySolicitud(Pay pay);
        IEnumerable<PayDuesDetail> GetPayDueDetailForPatientId(int patientId);
        IEnumerable<PayDuesDetailHistory> GetPayDueDetailForPatientIdHistory(int paymentId);

    }
}

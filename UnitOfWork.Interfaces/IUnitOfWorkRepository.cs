using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IPersonRepository PersonRepository { get; }
        IEmailRepository EmailRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        ICellPhoneRepository CellPhoneRepository { get; }
        IPatientInQueueRepository PatientInQueueRepository { get; }
        IPatientInAttentionRepository PatientInAttentionRepository { get; }
        IErrorRepository ErrorRepository { get; }
        IEmployeedRepository EmployeedRepository { get; }
        IPatientRepository PatientRepository { get; }
        ISolicitudAttentionRepository SolicitudAttentionRepository { get; }
        IPacketsOrUnitSessionsRepository PacketsOrUnitSessionsRepository { get; }
        ICommonRepository CommonRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        IPayRepository PayRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IMessageRepository MessageRepository { get; }
        IMovementsRepository MovementsRepository { get; }
        IProductRepository ProductRepository { get; }
        ISaleRepository SaleRepository { get; }
    }
}

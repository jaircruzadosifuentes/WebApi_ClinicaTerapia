using Repository.Interfaces;
using Repository.SqlServer;
using Repository.SqlServer.CellPhoneRepository;
using Repository.SqlServer.CommonRepository;
using Repository.SqlServer.DocumentRepository;
using Repository.SqlServer.EmailRepository;
using Repository.SqlServer.EmployeedRepository;
using Repository.SqlServer.ErrorRepository;
using Repository.SqlServer.MessageRepository;
using Repository.SqlServer.PacketsOrUnitSessionsRepository;
using Repository.SqlServer.PatientInAttentionRepository;
using Repository.SqlServer.PatientRepository;
using Repository.SqlServer.PaymentRepository;
using Repository.SqlServer.PayRepository;
using Repository.SqlServer.PersonRepository;
using Repository.SqlServer.ScheduleRepository;
using Repository.SqlServer.SolicitudAttentionRepository;
using System.Data.SqlClient;
using UnitOfWork.Interfaces;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository: IUnitOfWorkRepository
    {
        public IPersonRepository PersonRepository { get; }
        public IEmailRepository EmailRepository { get; }
        public IDocumentRepository DocumentRepository { get; }
        public ICellPhoneRepository CellPhoneRepository { get; }
        public IPatientInQueueRepository PatientInQueueRepository { get; }
        public IPatientInAttentionRepository PatientInAttentionRepository { get; }
        public IErrorRepository ErrorRepository { get; }
        public IEmployeedRepository EmployeedRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public ISolicitudAttentionRepository SolicitudAttentionRepository { get; }
        public IPacketsOrUnitSessionsRepository PacketsOrUnitSessionsRepository { get; }
        public ICommonRepository CommonRepository { get; }
        public IScheduleRepository ScheduleRepository { get; }
        public IPayRepository PayRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IMessageRepository MessageRepository { get; }
        public IMovementsRepository MovementsRepository { get; }

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            PersonRepository = new PersonRepository(context, transaction);
            EmailRepository = new EmailRepository(context, transaction);
            DocumentRepository = new DocumentRepository(context, transaction);
            CellPhoneRepository = new CellPhoneRepository(context, transaction);
            PatientInQueueRepository = new PatientInQueueRepository(context, transaction);
            PatientInAttentionRepository = new PatientInAttentionRepository(context, transaction);
            ErrorRepository = new ErrorRepository(context, transaction);
            EmployeedRepository = new EmployeedRepository(context, transaction);
            PatientRepository = new PatientRepository(context, transaction);
            SolicitudAttentionRepository = new SolicitudAttentionRepository(context, transaction);
            PacketsOrUnitSessionsRepository = new PacketsOrUnitSessionsRepository(context, transaction);
            CommonRepository = new CommonRepository(context, transaction);
            ScheduleRepository = new ScheduleRepository(context, transaction);
            PayRepository = new PayRepository(context, transaction);
            PaymentRepository = new PaymentRepository(context, transaction);
            MessageRepository = new MessageRepository(context, transaction);
            MovementsRepository = new MovementsRepository(context, transaction);
        }
    }
}

using Repository.Interfaces;
using Repository.SqlServer;
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
        public IProductRepository ProductRepository { get; }
        public ISaleRepository SaleRepository { get; }
        public IContabilidadRepository ContabilidadRepository { get; }

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
            ProductRepository = new ProductRepository(context, transaction);
            SaleRepository = new SaleRepository(context, transaction);
            ContabilidadRepository = new ContabilidadRepository(context, transaction);
        }
    }
}

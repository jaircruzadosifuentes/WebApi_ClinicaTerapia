using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IDocumentService
    {
        IEnumerable<Document> GetAllDocuments();
    }
    public class DocumentService: IDocumentService
    {
        private IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Document> GetAllDocuments()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.DocumentRepository.GetAllDocuments();
        }
    }
}

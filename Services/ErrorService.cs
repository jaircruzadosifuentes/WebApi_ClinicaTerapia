using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IErrorService
    {
        int InsertErrorRepository(Error error);
    }
    public class ErrorService : IErrorService
    {
        private IUnitOfWork _unitOfWork;

        public ErrorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int InsertErrorRepository(Error error)
        {
            using var context = _unitOfWork.Create();
            var insertId = context.Repositories.ErrorRepository.InsertErrorRepository(error);
            context.SaveChanges();
            return insertId;
        }
    }
}

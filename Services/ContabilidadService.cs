using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IContabilidadService
    {
        List<CajaChicaMontos> GetCajaChicaMontos(DateTime dateOpened, int employeedCashId);
        List<SaleBuyOut> GetDetailMovementsCajaChica(DateTime dateOpened, int employeedCashId);
        bool PostCloseCajaChica(CajaChica cajachica);
        CajaChica VerifyCajaChica(DateTime dateOpened, int employeedCashId);
        CajaChica DetailDataEmployeedCajaChica(int employeedId, DateTime dateApertu);
        bool PostApertuCajaChica(CajaChica cajachica);
        List<CajaChica> GetHistDetailCajaChicaByIdEmployeed(int employeedId);
    }
    public class ContabilidadService : IContabilidadService
    {
        private IUnitOfWork _unitOfWork;

        public ContabilidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CajaChica DetailDataEmployeedCajaChica(int employeedId, DateTime dateApertu)
        {
            using var context = _unitOfWork.Create();
            CajaChica objCajaChica = context.Repositories.ContabilidadRepository.DetailDataEmployeedCajaChica(employeedId, dateApertu);
            return objCajaChica;
        }

        public List<CajaChicaMontos> GetCajaChicaMontos(DateTime dateOpened, int employeedCashId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.ContabilidadRepository.GetCajaChicaMontos(dateOpened, employeedCashId);
        }

        public List<SaleBuyOut> GetDetailMovementsCajaChica(DateTime dateOpened, int employeedCashId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.ContabilidadRepository.GetDetailMovementsCajaChica(dateOpened, employeedCashId);
        }

        public List<CajaChica> GetHistDetailCajaChicaByIdEmployeed(int employeedId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.ContabilidadRepository.GetHistDetailCajaChicaByIdEmployeed(employeedId);
        }

        public bool PostApertuCajaChica(CajaChica cajachica)
        {
            using var context = _unitOfWork.Create();
            bool insert = context.Repositories.ContabilidadRepository.PostApertuCajaChica(cajachica);
            context.SaveChanges();
            return insert;
        }

        public bool PostCloseCajaChica(CajaChica cajachica)
        {
            using var context = _unitOfWork.Create();
            bool insert = context.Repositories.ContabilidadRepository.PostCloseCajaChica(cajachica);
            context.SaveChanges();
            return insert;
        }

        public CajaChica VerifyCajaChica(DateTime dateOpened, int employeedCashId)
        {
            using var context = _unitOfWork.Create();
            CajaChica objCajaChica = context.Repositories.ContabilidadRepository.VerifyCajaChica(dateOpened, employeedCashId);
            return objCajaChica;
        }
    }
}

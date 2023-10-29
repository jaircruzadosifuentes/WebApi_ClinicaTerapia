using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IContabilidadRepository
    {
       List<CajaChicaMontos> GetCajaChicaMontos(DateTime dateOpened, int employeedCashId);
       List<SaleBuyOut> GetDetailMovementsCajaChica(DateTime dateOpened, int employeedCashId);
       List<CajaChica> GetHistDetailCajaChicaByIdEmployeed(int employeedId);
       bool PostCloseCajaChica(CajaChica cajachica);
        CajaChica VerifyCajaChica(DateTime dateOpened, int employeedCashId);
        CajaChica DetailDataEmployeedCajaChica(int employeedId, DateTime dateApertu);
       bool PostApertuCajaChica(CajaChica cajachica);
    }
}

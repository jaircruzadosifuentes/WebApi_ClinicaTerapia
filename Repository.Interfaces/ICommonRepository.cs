using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICommonRepository
    {
        IEnumerable<PayMethod> GetAllPayMethods();
        IEnumerable<Dashboard> GetCountPatientsType();
        bool PostRegisterFrecuencyClinic(Frecuency frecuency);
        bool PutUpdateFrecuencyClinic(Frecuency frecuency);
        IEnumerable<Frecuency> GetAllFrecuency();
        IEnumerable<TypeOfContract> GetInComboTypeOfContract();
        IEnumerable<ModalityContract> GetInComboModalityContract();
        IEnumerable<Role> GetInComboRole();
        IEnumerable<AfpSure> GetInComboAfpSure();
        IEnumerable<Role> GetRoles();
        IEnumerable<Area> GetAreasInSelect();
        bool PostRegisterRole(Role role);
        bool PutDisabledEnabledRole(int roleId, int type);
        bool PutRole(Role role);
        IEnumerable<object> GetReportMensualCategoryTTO();
        IEnumerable<Option> GetOptions(int employeedId);
        IEnumerable<OptionItems> GetOptionsItems(int optionId, int employeedId);
        IEnumerable<Routes> GetRoutes(int employeedId);
        IEnumerable<Option> GetOptionsGeneral();
        IEnumerable<OptionItems> GetOptionsItemsGeneral();
        IEnumerable<Option> GetOptionsByCodeEmployeed(string code);
        IEnumerable<OptionItems> GetOptionsItemsByCodeEmployeed(string code);
        bool PutRemoveAddOptionEmployeed(int optionItemId);
        bool PutAddOptionEmployeed(int optionItemId, int optionId, string code);
        bool PutAddOptionFather(string codeEmployeed, int optionId);
        IEnumerable<VoucherDocument> GetInSelectVoucherDocument();
        IEnumerable<VerifyPatient> VerifyPatientByFullName(string surnames, string names);
        IEnumerable<Config> GetAllConfigs();
        bool PutConfig(Config config);
        IEnumerable<Routes> GetRoutesSpecial(string userCode);
    }
}

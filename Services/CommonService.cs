using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface ICommonService
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
        IEnumerable<Routes> GetRoutes(int employeedId);
        IEnumerable<Option> GetOptionsGeneral();
        IEnumerable<OptionItems> GetOptionsItemGeneral();
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
        IEnumerable<SubCategory> GetSubCategoriesInSelect(int categoryId);
        IEnumerable<Category> GetCategoriesInSelect();

    }
    public class CommonService : ICommonService
    {
        private IUnitOfWork _unitOfWork;

        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Config> GetAllConfigs()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetAllConfigs();
        }

        public IEnumerable<Frecuency> GetAllFrecuency()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetAllFrecuency();
        }

        public IEnumerable<PayMethod> GetAllPayMethods()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetAllPayMethods();
        }

        public IEnumerable<Area> GetAreasInSelect()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetAreasInSelect();
        }

        public IEnumerable<Category> GetCategoriesInSelect()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetCategoriesInSelect();
        }

        public IEnumerable<Dashboard> GetCountPatientsType()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetCountPatientsType();
        }

        public IEnumerable<AfpSure> GetInComboAfpSure()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetInComboAfpSure();
        }

        public IEnumerable<ModalityContract> GetInComboModalityContract()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetInComboModalityContract();
        }

        public IEnumerable<Role> GetInComboRole()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetInComboRole();
        }

        public IEnumerable<TypeOfContract> GetInComboTypeOfContract()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetInComboTypeOfContract();
        }

        public IEnumerable<VoucherDocument> GetInSelectVoucherDocument()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetInSelectVoucherDocument();
        }

        public IEnumerable<Option> GetOptions(int employeedId)
        {
            using var context = _unitOfWork.Create();
            try
            {
                var options = context.Repositories.CommonRepository.GetOptions(employeedId);
                foreach (var option in options)
                {
                    option.Items = (List<OptionItems>)context.Repositories.CommonRepository.GetOptionsItems(option.OptionId, employeedId);
                }
                return options;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Option> GetOptionsByCodeEmployeed(string code)
        {
            using var context = _unitOfWork.Create();
            try
            {
                var options = context.Repositories.CommonRepository.GetOptionsByCodeEmployeed(code);
                return options;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Option> GetOptionsGeneral()
        {
            using var context = _unitOfWork.Create();
            try
            {
                var options = context.Repositories.CommonRepository.GetOptionsGeneral();
                return options;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<OptionItems> GetOptionsItemGeneral()
        {
            using var context = _unitOfWork.Create();
            try
            {
                var options = context.Repositories.CommonRepository.GetOptionsItemsGeneral();
                return options;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<OptionItems> GetOptionsItemsByCodeEmployeed(string code)
        {
            using var context = _unitOfWork.Create();
            try
            {
                var options = context.Repositories.CommonRepository.GetOptionsItemsByCodeEmployeed(code);
                return options;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<object> GetReportMensualCategoryTTO()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetReportMensualCategoryTTO();
        }

        public IEnumerable<Role> GetRoles()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetRoles();
        }

        public IEnumerable<Routes> GetRoutes(int employeedId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetRoutes(employeedId);
        }

        public IEnumerable<Routes> GetRoutesSpecial(string userCode)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetRoutesSpecial(userCode);
        }

        public IEnumerable<SubCategory> GetSubCategoriesInSelect(int categoryId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.GetSubCategoriesInSelect(categoryId);
        }

        public bool PostRegisterFrecuencyClinic(Frecuency frecuency)
        {
            using var context = _unitOfWork.Create();
            var inserta = context.Repositories.CommonRepository.PostRegisterFrecuencyClinic(frecuency);
            context.SaveChanges();
            return inserta; 
        }

        public bool PostRegisterRole(Role role)
        {
            using var context = _unitOfWork.Create();
            var inserta = context.Repositories.CommonRepository.PostRegisterRole(role);
            context.SaveChanges();
            return inserta;
        }

        public bool PutAddOptionEmployeed(int optionItemId, int optionId, string code)
        {
            using var context = _unitOfWork.Create();
            var inserta = context.Repositories.CommonRepository.PutAddOptionEmployeed(optionItemId, optionId, code);
            context.SaveChanges();
            return inserta;
        }

        public bool PutAddOptionFather(string codeEmployeed, int optionId)
        {
            using var context = _unitOfWork.Create();
            var inserta = context.Repositories.CommonRepository.PutAddOptionFather(codeEmployeed, optionId);
            context.SaveChanges();
            return inserta;
        }

        public bool PutConfig(Config config)
        {
            using var context = _unitOfWork.Create();
            var update = context.Repositories.CommonRepository.PutConfig(config);
            context.SaveChanges();
            return update;
        }

        public bool PutDisabledEnabledRole(int roleId, int type)
        {
            using var context = _unitOfWork.Create();
            var update = context.Repositories.CommonRepository.PutDisabledEnabledRole(roleId, type);
            context.SaveChanges();
            return update;
        }

        public bool PutRemoveAddOptionEmployeed(int optionItemId)
        {
            using var context = _unitOfWork.Create();
            var inserta = context.Repositories.CommonRepository.PutRemoveAddOptionEmployeed(optionItemId);
            context.SaveChanges();
            return inserta;
        }

        public bool PutRole(Role role)
        {
            using var context = _unitOfWork.Create();
            var update = context.Repositories.CommonRepository.PutRole(role);
            context.SaveChanges();
            return update;
        }

        public bool PutUpdateFrecuencyClinic(Frecuency frecuency)
        {
            using var context = _unitOfWork.Create();
            var inserta = context.Repositories.CommonRepository.PutUpdateFrecuencyClinic(frecuency);
            context.SaveChanges();
            return inserta;
        }

        public IEnumerable<VerifyPatient> VerifyPatientByFullName(string surnames, string names)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.CommonRepository.VerifyPatientByFullName(surnames, names);
        }
    }
}

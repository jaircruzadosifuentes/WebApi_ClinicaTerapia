using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface ISaleService
    {
        int PostSaveSaleHead(Sale sale);
        Sale GetCorrelativoSale(int saleId);
    }
    public class SaleService : ISaleService
    {
        private IUnitOfWork _unitOfWork;

        public SaleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Sale GetCorrelativoSale(int saleId)
        {
            using var context = _unitOfWork.Create();
            Sale correlativo = context.Repositories.SaleRepository.GetCorrelativoSale(saleId);
            return correlativo;
        }

        public int PostSaveSaleHead(Sale sale)
        {
            using var context = _unitOfWork.Create();
            int saleId = context.Repositories.SaleRepository.PostSaveSaleHead(sale);
            foreach (SaleBuyOutProduct saleBuyOut in sale.SaleBuyOutProducts)
            {
                bool insert = context.Repositories.SaleRepository.PostSaveSaleDetailsProducts(saleBuyOut, saleId);
            }
            context.SaveChanges();
            return saleId;
        }
    }
}

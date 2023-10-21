using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProductoByCategoryIdSubCategoryId(int categoryId, int subCategoryId);
    }
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Product> GetProductoByCategoryIdSubCategoryId(int categoryId, int subCategoryId)
        {
            using var context = _unitOfWork.Create();
            var products = context.Repositories.ProductRepository.GetProductoByCategoryIdSubCategoryId(categoryId, subCategoryId);
            return products; 
        }
    }
}

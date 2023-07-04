using Hgtrg.Ecommerce.BusinessLayer.GenericUtils;
using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using Hgtrg.Ecommerce.DataLayer.Repositories;
using System.Linq.Expressions;

namespace Hgtrg.Ecommerce.BusinessLayer.Services
{
	public interface IProductServices : IGenericService<Product>
    {
        IEnumerable<Product> RetrieveProductsInformations(Expression<Func<Product, bool>> predicate);

	}

    public class ProductServices : GenericService<Product>, IProductServices
    {
        private readonly IProductRepositoy _repository;
        public ProductServices(IUnitOfWork<HgtrgEcommerceContext> unitOfWork,
            IProductRepositoy repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> RetrieveProductsInformations(Expression<Func<Product, bool>> predicate)
        {
            return _repository.RetrieveProductsInformations(predicate);
        }
    }
}

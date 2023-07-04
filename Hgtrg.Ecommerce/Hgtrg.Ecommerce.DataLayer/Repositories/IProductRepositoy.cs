using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hgtrg.Ecommerce.DataLayer.Repositories
{
	public interface IProductRepositoy : IGenericRepository<Product>
    {
		IEnumerable<Product> RetrieveProductsInformations(Expression<Func<Product, bool>> predicate);

    }

    public class ProductRepositry : GenericRepository<Product>, IProductRepositoy
    {
        public ProductRepositry(IUnitOfWork<HgtrgEcommerceContext> unitOfWork) 
            : base(unitOfWork) { }

        public IEnumerable<Product> RetrieveProductsInformations(Expression<Func<Product, bool>> predicate)
        {
            var products = Context.Products.Where(predicate)
                .Include(x => x.Category)
                .Include(x => x.OrderProducts)
                .Include(x => x.ProductImages)
                .Include(x => x.Promotions)
                .Include(x => x.Reviews)
                .Include(x => x.Seller)
                .Include(x => x.SellerProductCategories)
                .ToList();
            return products;
        }
    }
}

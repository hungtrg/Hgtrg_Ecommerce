using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;

namespace Hgtrg.Ecommerce.DataLayer.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork<HgtrgEcommerceContext> unitOfWork) 
            : base(unitOfWork) { }
    }
}

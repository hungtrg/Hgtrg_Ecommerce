using Hgtrg.Ecommerce.BusinessLayer.GenericUtils;
using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using Hgtrg.Ecommerce.DataLayer.Repositories;

namespace Hgtrg.Ecommerce.BusinessLayer.Services
{
    public interface ICategoryServices : IGenericService<Category>
    {
    }

    public class CategorySevices : GenericService<Category>, ICategoryServices
    {
        public CategorySevices(IUnitOfWork<HgtrgEcommerceContext> unitOfWork,
           ICategoryRepository repository) : base(unitOfWork, repository) { }
    }
}

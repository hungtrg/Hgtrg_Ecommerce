using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;

namespace Hgtrg.Ecommerce.DataLayer.Repositories
{
    public interface ISellerRepository : IGenericRepository<Seller>
    {
    }

    public class SellerRepository : GenericRepository<Seller>, ISellerRepository
    {
        public SellerRepository(IUnitOfWork<HgtrgEcommerceContext> unitOfWork) 
            : base(unitOfWork) { }
    }
}

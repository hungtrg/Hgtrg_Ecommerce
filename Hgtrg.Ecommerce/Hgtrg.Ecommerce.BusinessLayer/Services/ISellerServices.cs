using Hgtrg.Ecommerce.BusinessLayer.GenericUtils;
using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using Hgtrg.Ecommerce.DataLayer.Repositories;

namespace Hgtrg.Ecommerce.BusinessLayer.Services
{
    public interface ISellerServices : IGenericService<Seller>
    {
        Seller AddSeller(User user);
    }

    public class SellerServices : GenericService<Seller>, ISellerServices
    {
        public SellerServices(IUnitOfWork<HgtrgEcommerceContext> unitOfWork,
            ISellerRepository repository) : base(unitOfWork, repository) { }

        public Seller AddSeller(User user)
        {
            if (user == null)
            {
                throw new InvalidOperationException("Check user account or role please!");
            }

            var seller = new Seller
            {
                User = user,
                UserId = user.UserId,
                Name = user.Username,
                Description = "Place your descriptions here!"
            };
            
            return Add(seller);
        }
    }
}

using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;

namespace Hgtrg.Ecommerce.DataLayer.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUserById(int userId);
        IEnumerable<User> GetUserByUsername(string username);
    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork<HgtrgEcommerceContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        public User GetUserById(int userId)
        {
            return Context.Users.First(u => u.UserId == userId);
        }

        public IEnumerable<User> GetUserByUsername(string username)
        {
            var user = Context.Users.Where(x => x.Username == username).ToList();
            return user;
        }
    }
}

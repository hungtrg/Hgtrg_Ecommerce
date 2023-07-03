using Hgtrg.Ecommerce.BusinessLayer.DTO.RequestModels;
using Hgtrg.Ecommerce.BusinessLayer.GenericUtils;
using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Enums;
using Hgtrg.Ecommerce.DataLayer.Models;
using Hgtrg.Ecommerce.DataLayer.Repositories;
using System.Security.Cryptography;

namespace Hgtrg.Ecommerce.BusinessLayer.Services
{
    public interface IUserServices : IGenericService<User>
    {
        User RegisterUser(RegisterModel model);
        User RegisterSeller(RegisterModel model);
        User SigninUser(LoginModel model);
    }

    public class UserServices : GenericService<User>, IUserServices
    {
        public UserServices(IUnitOfWork<HgtrgEcommerceContext> unitOfWork,
            IUserRepository repository) : base(unitOfWork, repository) { }

        public User RegisterUser(RegisterModel model)
        {
            // Check if username already exists
            var existedUser = FirstOrDefault(u => u.Username.Equals(model.Username) || u.Email.Equals(model.Email));
            if (existedUser != null)
            {
                throw new InvalidOperationException("User with the same username already exists.");
            }

            // Create new user object
            var user = new User
            {
                Username = model.Username,
                Password = HashPassword(model.Password),
                Email = model.Email,
                Role = Role.Customer.ToString(),
                Status = Convert.ToBoolean(AccountStatus.Active)
            };

            return Add(user);
        }

        public User RegisterSeller(RegisterModel model)
        {
            // Check if username or email already exists
            var user = FirstOrDefault(u => u.Username.Equals(model.Username) || u.Email.Equals(model.Email));
            if (user.Role.Equals(Role.Seller))
            {
                throw new InvalidOperationException("A user with the same username has already registered as a seller.");
            }
            if (user == null)
            {
                // Register new user as seller
                user = RegisterUser(model);
            }

            // Update role to seller if user exists
            user.Role = Role.Seller.ToString();
            return Update(user);
        }

        public User SigninUser(LoginModel model)
        {
            // Retrieve user from repository
            var user = FirstOrDefault(u => u.Username.Equals(model.Username));

            if (user == null)
            {
                throw new InvalidOperationException("User does not existed.");
            }

            // Validate old password
            if (!VerifyPassword(model.Password, user.Password))
            {
                throw new InvalidOperationException("Invalid password.");
            }

            return user;
        }

        private string HashPassword(string password)
        {
            // TODO: Implement password hashing algorithm
            // Generate a random salt
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password and salt using PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and hash into a single string
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // TODO: Implement password verification algorithm
            // Extract the salt and hash from the hashed password string
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            byte[] hash = new byte[20];
            Array.Copy(hashBytes, 16, hash, 0, 20);

            // Compute the hash of the password and salt using PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] computedHash = pbkdf2.GetBytes(20);

            // Compare the computed hash with the stored hash
            for (int i = 0; i < 20; i++)
            {
                if (hash[i] != computedHash[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}

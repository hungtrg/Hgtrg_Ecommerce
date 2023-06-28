using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using System.Security.Cryptography;

namespace Hgtrg.Ecommerce.BusinessLayer.Services
{
    public interface IUserServices
    {
        User GetUserById(int userId);
        IEnumerable<User> GetUserByUsername(string username);
        User CreateUser(string username, string password);
        bool UpdatePassword(int userId, string oldPassword, string newPassword);
        bool DeleteUser(int userId);
    }

    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User CreateUser(string username, string password)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            // Check if username already exists
            var existedUser = _unitOfWork.GetRepository<User>().FindAsync(user => user.Username == username);
            if (existedUser != null)
            {
                throw new InvalidOperationException("User with the same username already exists.");
            }

            // Create new user object
            var user = new User
            {
                Username = username,
                Password = HashPassword(password)
            };

            // Add user to repository
            _unitOfWork.GetRepository<User>().Add(user);

            // Save changes to data store
            _unitOfWork.SaveChangesAsync();

            return user;
        }

        public bool DeleteUser(int userId)
        {
            // Retrieve user from repository
            var user = _unitOfWork.GetRepository<User>().GetByIdAsync(userId).Result;

            if (user == null)
            {
                return false;
            }

            // Delete user from repository
            _unitOfWork.GetRepository<User>().Remove(user);

            // Save changes to data store
            _unitOfWork.SaveChangesAsync();

            return true;
        }

        public User GetUserById(int userId)
        {
            // Retrieve user from repository
            var user = _unitOfWork.GetRepository<User>().GetByIdAsync(userId).Result;

            return user;
        }

        public IEnumerable<User> GetUserByUsername(string username)
        {
            // Retrieve user from repository
            var user = _unitOfWork.GetRepository<User>().FindAsync(user => user.Username.Equals(username)).Result;

            return user;
        }

        public bool UpdatePassword(int userId, string oldPassword, string newPassword)
        {
            // Retrieve user from repository
            var user = _unitOfWork.GetRepository<User>().GetByIdAsync(userId).Result;

            if (user == null)
            {
                return false;
            }

            // Validate old password
            if (!VerifyPassword(oldPassword, user.Password))
            {
                return false;
            }

            // Update password hash
            user.Password = HashPassword(newPassword);

            // Update user in repository
            _unitOfWork.GetRepository<User>().Update(user);

            // Save changes to data store
            _unitOfWork.SaveChangesAsync();

            return true;
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

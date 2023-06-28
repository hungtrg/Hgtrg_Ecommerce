using Hgtrg.Ecommerce.DataLayer.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hgtrg.Ecommerce.DataLayer.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly HgtrgEcommerceContext _dbContext;
        private IDbContextTransaction _transaction;
        private readonly Dictionary<Type, object> _repositories;
        //private readonly IRepository<User> _userRepository;

        public UnitOfWork(HgtrgEcommerceContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _repositories = new Dictionary<Type, object>();
            //_userRepository = new Repository<User>(_dbContext);
        }

        //public IRepository<User> UserRepository => _userRepository;

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.TryGetValue(typeof(TEntity), out var repository))
            {
                return (IRepository<TEntity>)repository;
            }

            var newRepository = new Repository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), newRepository);

            return newRepository;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

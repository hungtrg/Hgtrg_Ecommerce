using Hgtrg.Ecommerce.DataLayer.DataAccess;
using System.Linq.Expressions;

namespace Hgtrg.Ecommerce.BusinessLayer.GenericUtils
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }

    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;

        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

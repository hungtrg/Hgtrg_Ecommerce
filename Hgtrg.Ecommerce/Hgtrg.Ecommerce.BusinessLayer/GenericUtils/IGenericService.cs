using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using System.Linq.Expressions;

namespace Hgtrg.Ecommerce.BusinessLayer.GenericUtils
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
    }

    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private IUnitOfWork<HgtrgEcommerceContext> _unitOfWork;
        private IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork<HgtrgEcommerceContext> unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            return _repository.Add(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}

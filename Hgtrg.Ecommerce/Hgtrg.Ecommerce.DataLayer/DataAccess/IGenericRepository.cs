using Hgtrg.Ecommerce.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hgtrg.Ecommerce.DataLayer.DataAccess
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Update(TEntity entity);
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    {
        private bool _isDisposed;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(IUnitOfWork<HgtrgEcommerceContext> unitOfWork)
        {
            Context = unitOfWork.Context;
        }
        public HgtrgEcommerceContext Context { get; set; }

        private DbSet<TEntity> Entities
        {
            get { return _dbSet ?? (_dbSet = Context.Set<TEntity>()); }
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).ToList();
        }

        public TEntity GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return Entities.Find(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return Entities.Where(predicate).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public TEntity Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity");
                }

                if (Context == null || _isDisposed)
                {
                    Context = new HgtrgEcommerceContext();
                }
                Context.Entry(entity).State = EntityState.Modified;
                // SaveChanges as Context save changes will called with Unit of work

                return entity;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public TEntity Add(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity");
                }

                if (Context == null || _isDisposed)
                {
                    Context = new HgtrgEcommerceContext();
                }
                var result = Entities.Add(entity);
                // SaveChanges as Context save changes will called with Unit of work

                return result.Entity;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void Remove(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity");
                }

                if (Context == null || _isDisposed)
                {
                    Context = new HgtrgEcommerceContext();
                }
                Entities.Remove(entity);

                // SaveChanges as Context save changes will called with Unit of work
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}

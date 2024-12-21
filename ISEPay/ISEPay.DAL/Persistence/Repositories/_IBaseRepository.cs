using Microsoft.EntityFrameworkCore;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface _IBaseRepository<T, T1> where T : BaseEntity<T1>
    {
        T GetById(T1 id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void SaveChanges();
    }

    internal class _BaseRepository<T, T1> : _IBaseRepository<T, T1> where T : BaseEntity<T1>
    {
        protected readonly ISEPayDBContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public _BaseRepository(ISEPayDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public T GetById(T1 id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}

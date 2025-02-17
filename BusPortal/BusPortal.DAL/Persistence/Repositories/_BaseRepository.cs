using BusPortal.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusPortal.DAL.Persistence.Repositories
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
        private  DALDbContext _dbContext;
        protected DbSet<T> DbContext;
        public _BaseRepository(DALDbContext dbContext)
        {
            _dbContext = dbContext;
            DbContext = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbContext.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return [.. DbContext.AsNoTracking()];
        }

        public T GetById(T1 id)
        {
            return DbContext.Find(id);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}

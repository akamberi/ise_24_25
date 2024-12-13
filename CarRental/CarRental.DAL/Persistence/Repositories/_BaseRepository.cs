using CarRental.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Persistence.Repositories;

public interface _IBaseRepository<T, T1> where T : BaseEntity<T1>
{
    T GetById(T1 id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void SaveChanges();
}

internal class _BaseRepository<T, T1> : _IBaseRepository<T, T1> where T : BaseEntity<T1>
{
    private CarRentalDbContext _dbContext;
    protected DbSet<T> _dbSet;
    public _BaseRepository(CarRentalDbContext dbContext)
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
        return [.. _dbSet.AsNoTracking()];
    }

    public T GetById(T1 id)
    {
        return _dbSet.Find(id);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}

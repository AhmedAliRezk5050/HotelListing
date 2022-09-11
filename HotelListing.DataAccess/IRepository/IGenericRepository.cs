using System.Linq.Expressions;

namespace HotelListing.DataAccess.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetAsync(Expression<Func<T, bool>> filter, List<string>? includes = null);

    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        List<string>? includes = null);
    
    Task Add(T entity);
    
    Task AddRange(IEnumerable<T> entities);
    
    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);
}
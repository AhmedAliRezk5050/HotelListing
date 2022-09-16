using System.Linq.Expressions;
using HotelListing.Models.DataTypes;
using X.PagedList;

namespace HotelListing.DataAccess.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetAsync(Expression<Func<T, bool>> filter, List<string>? includes = null);

    Task<IPagedList<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        List<string>? includes = null,
        QueryParameters? queryParameters = null
    );

    Task Add(T entity);

    Task AddRange(IEnumerable<T> entities);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);
}
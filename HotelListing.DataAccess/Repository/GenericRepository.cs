using System.Linq.Expressions;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models.DataTypes;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HotelListing.DataAccess.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;

    internal DbSet<T> dbSet;


    public GenericRepository(DataContext context)
    {
        _context = context;
        dbSet = _context.Set<T>();
    }

    public Task<T?> GetAsync(Expression<Func<T, bool>> filter, List<string>? includes = null)
    {
        var query = dbSet.Where(filter);

        if (includes is not null)
        {
            query = AddIncludes(query, includes);
        }

        return query.FirstOrDefaultAsync();
    }

    public Task<IPagedList<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>,
            IOrderedQueryable<T>>? orderBy = null,
        List<string>? includes = null,
        QueryParameters? queryParameters = null
    )
    {
        IQueryable<T> query = dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (includes is not null)
        {
            query = AddIncludes(query, includes);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        queryParameters ??= new QueryParameters();
        
        return query.AsNoTracking()
            .ToPagedListAsync(queryParameters.PageNumber, queryParameters.PageSize);
    }

    public async Task Add(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public Task AddRange(IEnumerable<T> entities)
    {
        return dbSet.AddRangeAsync(entities);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange();
    }

    IQueryable<T> AddIncludes(IQueryable<T> query, List<string> includes)
    {
        foreach (var i in includes)
        {
            query = query.Include(i);
        }

        return query;
    }
}
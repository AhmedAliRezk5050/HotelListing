using HotelListing.DataAccess.IRepository;
using HotelListing.Models;

namespace HotelListing.DataAccess.Repository;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    private readonly DataContext _context;

    private bool _disposed;

    public CountryRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Country country)
    {
        dbSet.Update(country);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
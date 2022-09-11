using HotelListing.DataAccess.IRepository;

namespace HotelListing.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    private bool _disposed = false;

    public IHotelRepository Hotels { get; }
    
    public ICountryRepository Countries { get; }
    
    public UnitOfWork(DataContext context)
    {
        _context = context;

        Hotels = new HotelRepository(_context);

        Countries = new CountryRepository(_context);
    }
    
   
    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
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
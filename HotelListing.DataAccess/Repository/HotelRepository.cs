using HotelListing.DataAccess.IRepository;
using HotelListing.Models;

namespace HotelListing.DataAccess.Repository;

public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
{
    private readonly DataContext _context;

    private bool _disposed;

    public HotelRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Hotel hotel)
    {
        dbSet.Update(hotel);
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
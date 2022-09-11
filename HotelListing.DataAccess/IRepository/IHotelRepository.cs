using HotelListing.Models;

namespace HotelListing.DataAccess.IRepository;

public interface IHotelRepository : IGenericRepository<Hotel>, IDisposable
{
    void Update(Hotel hotel);
}
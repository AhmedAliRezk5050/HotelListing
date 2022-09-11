using HotelListing.Models;

namespace HotelListing.DataAccess.IRepository;

public interface ICountryRepository : IGenericRepository<Country>, IDisposable
{
    void Update(Country country);
}
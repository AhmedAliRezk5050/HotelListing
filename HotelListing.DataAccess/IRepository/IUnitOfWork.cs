namespace HotelListing.DataAccess.IRepository;

public interface IUnitOfWork : IDisposable
{
    IHotelRepository Hotels { get; }
    
    ICountryRepository Countries { get; }
    
    Task SaveAsync();
}
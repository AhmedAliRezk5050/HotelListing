using HotelListing.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)
       : base(opts) { }


        public DbSet<Country> Countries { get; set; } = null!;

        public DbSet<Hotel> Hotels { get; set; } = null!;
    }
}

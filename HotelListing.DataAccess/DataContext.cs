using HotelListing.DataAccess.Seeding;
using HotelListing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.DataAccess
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> opts)
            : base(opts)
        {
        }


        public DbSet<Country> Countries { get; set; } = null!;

        public DbSet<Hotel> Hotels { get; set; } = null!;

        // Data Seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //  Data Seeding
            //  Using this method requires seeding complete object fields
            modelBuilder.ApplyConfiguration(new RoleSeeding());

            modelBuilder.ApplyConfiguration(new CountrySeeding());

            modelBuilder.ApplyConfiguration(new HotelSeeding());
            //
        }
    }
}
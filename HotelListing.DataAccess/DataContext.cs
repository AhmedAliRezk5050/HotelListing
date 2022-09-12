using HotelListing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

            modelBuilder.Entity<Country>().HasData(
                new Country()
                {
                    Id = 1,
                    Name = "Jamica",
                    ShortName = "JM"
                },
                new Country()
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country()
                {
                    Id = 3,
                    Name = "Cayman Island",
                    ShortName = "CI"
                }
            );
            
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel()
                {
                    Id = 1,
                    Name = "Sandals Resort And Spa",
                    Address = "Negril",
                    CountryId = 1,
                    Rating = 4.5M
                },
                new Hotel()
                {
                    Id = 2,
                    Name = "Comfort Suites",
                    Address = "George Town",
                    CountryId = 3,
                    Rating = 4.3M
                },
                new Hotel()
                {
                    Id = 3,
                    Name = "Grand Palladiem",
                    Address = "Nassua",
                    CountryId = 2,
                    Rating = 4
                }
            );
        }
    }
}
using HotelListing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HotelListing.DataAccess.Seeding;

public class HotelSeeding : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(
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
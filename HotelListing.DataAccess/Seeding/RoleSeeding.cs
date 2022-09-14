using HotelListing.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HotelListing.DataAccess.Seeding;

public class RoleSeeding : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
                new IdentityRole() { Id= "1234691c-941e-46f9-9ca3-d870d8d4d11d",
                    Name = AppRoles.Admin, NormalizedName = AppRoles.Normalize(AppRoles.Admin),
                    ConcurrencyStamp = "eb0d458a-2b0c-45dd-8033-0a8b0e3f32ea"
                },
                new IdentityRole() {Id = "3c814bf4-8013-4027-9f00-8ae5a673d78e",
                    Name = AppRoles.User, NormalizedName = AppRoles.Normalize(AppRoles.User),
                    ConcurrencyStamp = "2bd2d69f-cd93-4939-900f-28c7e822d6bf"
                }
            );
    }
}
namespace HotelListing.Models.DataTypes;

public class CreateTokenResponse
{
    public DateTime? UtcValidTo { get; set; }

    public string? Token { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string Address { get; set; } = null!;

        public double Rating { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; } = null!;
    }
}
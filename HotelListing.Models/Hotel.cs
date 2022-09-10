using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string Address { get; set; } = null!;

        [Precision(3,2)]
        public decimal Rating { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; } = null!;
    }
}
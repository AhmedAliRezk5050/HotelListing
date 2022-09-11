using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(10)]
        public string ShortName { get; set; } = null!;

        // no virtual => no lazy loading
        public List<Hotel> Hotels { get; set; }  = null!;
    }
}
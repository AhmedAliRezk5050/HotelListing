using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Models.DTOs.User
{
    public class LogInUserDto
    {
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Username can be only between {2} to {1} characters")]
        public string UserName { get; set; } = null!;

        [MinLength(6, ErrorMessage = "Password is too short. minimum length is {1}")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}

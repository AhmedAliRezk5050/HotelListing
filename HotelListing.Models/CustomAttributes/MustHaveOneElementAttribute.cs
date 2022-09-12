using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Models.CustomAttributes
{
    public class MustHaveOneElementAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
            value is IList { Count: > 0 }
                ? ValidationResult.Success
                : new ValidationResult("Please provide at least one role") ;
    }
}
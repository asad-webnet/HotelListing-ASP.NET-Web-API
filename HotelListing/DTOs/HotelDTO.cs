using System.ComponentModel.DataAnnotations;
using HotelListing.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.DTOs
{
    public class CreateHotelDTO
    {
        [Required]
        public string Name { get; set; }
        [Required,StringLength(maximumLength:50,ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        [Required]
        [Range(1,5)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
        public CountryDTO Country { get; set; }
    }

    public class HotelDTO : CreateHotelDTO
    {
        public int HotelId { get; set; }
    }
}

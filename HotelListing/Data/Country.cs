using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Data
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        // Navigation property to represent the relationship
        [InverseProperty("Country")]
        public virtual IList<Hotel> Hotels { get; set; }
    }
}

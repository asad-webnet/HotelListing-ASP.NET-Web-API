using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(

                new Hotel
                {
                    HotelId = 1,
                    Name = "Quetta Hotel",
                    Address = "Machi miani road",
                    CountryId = 1, // Set the CountryId directly
                    Rating = 4.5
                },
                new Hotel
                {
                    HotelId = 2,
                    Name = "Hindu Hotel",
                    Address = "Bahawalpur road",
                    CountryId = 2, // Set the CountryId directly
                    Rating = 4.0
                },
                new Hotel
                {
                    HotelId = 3,
                    Name = "Burger King",
                    Address = "Mountpalace burmingham",
                    CountryId = 3, // Set the CountryId directly
                    Rating = 4.2
                }
            );
        }
    }
}

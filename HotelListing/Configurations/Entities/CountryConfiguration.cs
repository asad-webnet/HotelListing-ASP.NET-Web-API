using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            Country country1 = new Country
            {
                CountryId = 1,
                Name = "Pakistan",
                ShortName = "PK"
            };

            Country country2 = new Country
            {
                CountryId = 2,
                Name = "India",
                ShortName = "IND"
            };

            Country country3 = new Country
            {
                CountryId = 3,
                Name = "United States",
                ShortName = "USA"
            };

            builder.HasData(
                country1,
                country2,
                country3
                );
        }
    }
}

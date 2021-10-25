using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
              new Country
              {
                  CountryId = 1,
                  Name = "Jamaica",
                  ShortName = "JM"
              },
              new Country
              {
                  CountryId = 2,
                  Name = "Bahamas",
                  ShortName = "BS"
              },
              new Country
              {
                  CountryId = 3,
                  Name = "Cayman Island",
                  ShortName = "CI"
              }
            );
        }
    }
}

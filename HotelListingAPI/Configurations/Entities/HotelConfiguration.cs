using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                 new Hotel
                 {
                     CountryId = 1,
                     Name = "Sandals Resort and Spa",
                     Address = "Negril",
                     Rating = 4.5,
                     Id = 1,
                 },
                 new Hotel
                 {
                     CountryId = 2,
                     Name = "Grand Palldium",
                     Address = "Nassua",
                     Rating = 4.5,
                     Id = 2,
                 },
                 new Hotel
                 {
                     CountryId = 3,
                     Name = "Marriate",
                     Address = "Gearge Town",
                     Rating = 4,
                     Id = 3,
                 }
             );
        }
    }
}

using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.Configurations
{
    public class MapperInitializer: Profile
    {
        public MapperInitializer()
        {
            // The country data class has a direct correlations with CountryDTO
            // And that goes for either direction (ReverseMap)
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();

        }
    }
}

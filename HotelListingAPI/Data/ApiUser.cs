using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.Data
{
    public class ApiUser : IdentityUser
    {
        // Can add many things for authentication
        // Note: these are custom fields that aren't 
        // included in IdentityUser

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

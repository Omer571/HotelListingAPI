using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.Data
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        // note:virtual means this method can be overridden by any class that inherits it
        // don't need a migration bc it won't go to DB
        // just an option for api users
        public virtual IList<Hotel> Hotels { get; set; }
    }
}

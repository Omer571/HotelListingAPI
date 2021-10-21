using HotelListingAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        // Update functions and such will stage changes (think of git add)
        // when we say Save they will save to db (think of git commit)
        Task Save();
    }
}

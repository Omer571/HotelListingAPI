using HotelListingAPI.Data;
using HotelListingAPI.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelListingAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // Dependecy Injection
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _db = _context.Set<T>(); // get DBSet of type T collection
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        // includes parameter means did user/calling code want to
        // include additional details
        // So for hotel instead of getting hotel, then using foreign key
        // to get country, we could do it in one if we include country
        // note: we can include as many foreign keys as we wants
        
        // Func is a lambda expression and bool is condition
        // (i.e) query => query.something == somethingelse
        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            // Get all records in table
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            // AsNoTracking means db shouldn't care about this
            // or changes made to it
            // Return FirstOrDefault answer that matches expression (passed when calling get)
            // since it is an expression we can by name expression, id expression, etc.
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);

        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                // filter query where expression true (print this for clarity)
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // AsNoTracking means db shouldn't care about this
            // or changes made to it
            return await query.AsNoTracking().ToListAsync();

        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            // entity is a record separate from db
            // so we have to attach it to db and
            // as a bonus, check to make sure record 
            // types match
            _db.Attach(entity);
            // Once entity modified, tells db we have to do an update to it
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RivitaBackend.IRepository;
using RivitaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RivitaBackend.Repository
{
    /// <summary>
    /// Provide generic repository a class like Transportations or ...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            //basically setting to table name that is provided to this class
            _db = context.Set<T>();
        }

        public async Task Delete(int id)
        {
          var entite = await _db.FindAsync(id);
            _db.Remove(entite);
            _context.SaveChanges();
        }

        public async Task DeleteGuid(Guid id)
        {
            var entite = await _db.FindAsync(id);
            _db.Remove(entite);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> includes = null)
        {
            // get all records from table 
            IQueryable<T> query = _db; 

            //check if thay have any include

            if(includes != null)
            {
                // looping through all includes and attaching each include to query
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);

        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if(includes != null)
            {
                foreach (var proertyInclude in includes)
                {
                    query = query.Include(proertyInclude);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {
           await  _db.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _db.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}

using RickLocalization_api.EF;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RickLocalization_api.Services
{
    public interface IMotyService
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Morty[]> ListAsync();
    }

    public class MotyService : IMotyService
    {
        private readonly RickLocalizationContext _context;
        
        public MotyService(RickLocalizationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add
        /// </summary>
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Morty[]> ListAsync()
        {
            IQueryable<Morty> query = _context.Mortys;
            query = query.AsNoTracking().OrderBy(c => c.Name);
            return await query.ToArrayAsync();
        }        
    }
}
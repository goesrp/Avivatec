using RickLocalization_api.EF;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace RickLocalization_api.Services
{
    public interface IRickService
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Rick[]> ListAsync();
    }

    public class RickService : IRickService
    {
        private readonly RickLocalizationContext _context;
        
        public RickService(RickLocalizationContext context)
        {
            _context = context;
        }

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

        public async Task<Rick[]> ListAsync()
        {
            IQueryable<Rick> query = _context.Ricks;
            query = query.AsNoTracking().OrderBy(c => c.Name);
            return await query.ToArrayAsync();
        }        
    }
}
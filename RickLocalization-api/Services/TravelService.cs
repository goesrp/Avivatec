using RickLocalization_api.EF;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RickLocalization_api.Services
{
    public interface ITravelService
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Travel[]> ListAsync();
    }
    public class TravelService : ITravelService
    {
        private readonly RickLocalizationContext _context;
        
        public TravelService(RickLocalizationContext context)
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

        public async Task<Travel[]> ListAsync()
        {
            IQueryable<Travel> query = _context.Travels;
            query = query.AsNoTracking().OrderBy(c => c.Traveid);
            return await query.ToArrayAsync();
        }        
    }
}
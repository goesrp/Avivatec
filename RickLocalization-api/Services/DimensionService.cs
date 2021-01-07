using RickLocalization_api.EF;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RickLocalization_api.Services
{
    public interface IDimensionService
    {
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Dimension[]> ListAsync();
    }

    public class DimensionService : IDimensionService
    {
        private readonly RickLocalizationContext _context;
        
        public DimensionService(RickLocalizationContext context)
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

        public async Task<Dimension[]> ListAsync()
        {
            IQueryable<Dimension> query = _context.Dimensions;
            query = query.AsNoTracking().OrderBy(c => c.Name);
            return await query.ToArrayAsync();
        }        
    }
}
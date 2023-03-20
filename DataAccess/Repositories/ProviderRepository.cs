using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly DataContext _context;

        public ProviderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provider>> GetAllProvidersAsync()
        {
            return await _context.Providers.AsNoTracking().ToListAsync();
        }
    }
}
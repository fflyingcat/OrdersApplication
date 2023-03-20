using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProviderRepository
    {
        public Task<IEnumerable<Provider>> GetAllProvidersAsync();
    }
}
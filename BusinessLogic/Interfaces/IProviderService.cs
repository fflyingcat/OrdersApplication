using Common.DTO;

namespace BusinessLogic.Interfaces
{
    public interface IProviderService
    {
        public Task<IEnumerable<ProviderDto>> GetAllProvidersAsync();
    }
}

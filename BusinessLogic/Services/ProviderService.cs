using AutoMapper;
using BusinessLogic.Interfaces;
using Common.DTO;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public ProviderService(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProviderDto>> GetAllProvidersAsync()
        {
            return _mapper.Map<IEnumerable<ProviderDto>>(await _providerRepository.GetAllProvidersAsync());
        }
    }
}
using AutoMapper;
using Common.DTO;
using DataAccess.Entities;

namespace BusinessLogic.AutomapperProfiles;

public class ProviderProfile : Profile
{
    public ProviderProfile()
    {
        CreateMap<Provider, ProviderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
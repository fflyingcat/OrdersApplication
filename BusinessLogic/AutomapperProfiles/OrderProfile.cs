using AutoMapper;
using Common.DTO;
using DataAccess.Entities;

namespace BusinessLogic.AutomapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderOutDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.ProviderId));

            CreateMap<OrderInDto, Order>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.ProviderId));
        }
    }
}
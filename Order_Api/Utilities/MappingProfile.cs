using AutoMapper;
using Order_Api.Dto;
using Order_Api.Entities;

namespace Order_Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderForCreateDto>().ReverseMap();
            CreateMap<Order, OrderForUpdateDto>().ReverseMap();
            CreateMap<Order, OrderForReturnDto>().ReverseMap();
        }
    }
}

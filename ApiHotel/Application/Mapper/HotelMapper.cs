using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Entities;
using AutoMapper;

namespace ApiHotel.Application.Mapper
{
    public class HotelMapper : Profile
    {
        public HotelMapper()
        {
            CreateMap<Hotel, HotelDto>().ReverseMap();
        }
    }
}

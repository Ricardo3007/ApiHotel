using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Entities;
using AutoMapper;

namespace ApiHotel.Application.Mapper
{
    public class ReservationMapper : Profile
    {
        public ReservationMapper()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();

        }
    }
}

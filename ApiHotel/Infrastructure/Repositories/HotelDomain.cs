using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Contracts;
using ApiHotel.Domain.Entities;
using ApiHotel.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiHotel.Infrastructure.Repositories
{

    public class HotelDomain : IHotelDomain
    {
        private readonly HotelContext _context;

        public HotelDomain(HotelContext context)
        { 
            _context = context;
        }


        public int AddHotel(Hotel hotel)
        {

            _context.Hotel.Add(hotel);
            _context.SaveChanges();

            return hotel.HotelId;
        }

        public void AssignHotelToAgent(int hotelId, int agentId)
        {
            // Crear la relación entre el agente y el hotel en la tabla AgentHotel
            var agentHotel = new AgentHotel
            {
                AgentId = agentId,
                HotelId = hotelId
            };
            _context.AgentHotel.Add(agentHotel);
            _context.SaveChanges();
        }


        public bool AssignRooms(int hotelId, List<RoomCreateDto> roomsDto)
        {
            var hotel = _context.Hotel.Include(h => h.Room).FirstOrDefault(h => h.HotelId == hotelId);
            if (hotel == null) return false;

            var newRooms = roomsDto.Select(r => new Room
            {
                HotelId = hotelId,
                RoomType = r.RoomType,
                BasePrice = r.BasePrice,
                Taxes = r.Taxes,
                Location = r.Location,
                IsActive = true,
            }).ToList();

            _context.Room.AddRange(newRooms);
            _context.SaveChanges();

            return true;
        }


        public Hotel? GetHotelById(int hotelId)
        {
            return _context.Hotel.FirstOrDefault(h => h.HotelId == hotelId);
        }

        public void UpdateHotel(Hotel hotel)
        {
            _context.Hotel.Update(hotel);
            _context.SaveChanges();
        }


        public List<HotelDto> SearchHotels(HotelSearchCriteriaDto searchDto)
        {
            return _context.Hotel
                .Where(h => h.Room.Any(r => r.IsActive &&
                                             r.Reservation.All(res =>
                                                 res.CheckOutDate <= searchDto.CheckInDate ||
                                                 res.CheckInDate >= searchDto.CheckOutDate)))
                .Select(h => new HotelDto
                {
                    Id = h.HotelId,
                    Name = h.Name,
                    Description = h.Description,
                    AvailableRooms = h.Room.Count(r => r.IsActive),
                    BasePrice = h.BasePrice
                })
                .ToList();
        }


        public List<Room> GetRoomsByHotel(int hotelId)
        {
            return _context.Room
                           .Where(r => r.HotelId == hotelId && r.IsActive) // Obtener habitaciones activas del hotel
                           .ToList();
        }





    }
}

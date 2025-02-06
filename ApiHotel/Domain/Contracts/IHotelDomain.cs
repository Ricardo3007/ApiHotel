using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Entities;

namespace ApiHotel.Domain.Contracts
{
    public interface IHotelDomain
    {
        int AddHotel(Hotel hotelDto);

        void AssignHotelToAgent(int hotelId, int agentId);

        bool AssignRooms(int hotelId, List<RoomCreateDto> roomsDto);

        Hotel? GetHotelById(int hotelId);

        void UpdateHotel(Hotel hotel);

        List<HotelDto> SearchHotels(HotelSearchCriteriaDto searchDto);

        List<Room> GetRoomsByHotel(int hotelId);
    }

}

using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.DTOs;

namespace ApiHotel.Application.Contracts
{
    public interface IHotelService
    {
        Result<int> AddHotel(HotelDto hotelDTO);

        Result<bool> AssignRoomsToHotel(AssignRoomsDto assignRoomsDto);

        Result<bool> UpdateHotel(HotelDto hotelDto);

        Result<bool> UpdateHotelStatus(HotelStatusDto hotelStatusDto);

        Result<List<HotelDto>> SearchHotels(HotelSearchCriteriaDto searchCriteria);

        List<RoomDto> GetAvailableRooms(int hotelId, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests);
    }
}

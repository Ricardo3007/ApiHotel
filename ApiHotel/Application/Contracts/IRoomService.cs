using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.DTOs;

namespace ApiHotel.Application.Contracts
{
    public interface IRoomService
    {
        Result<bool> UpdateRoom(RoomUpdateDto roomDto);

        Result<bool> UpdateRoomStatus(RoomUpdateStatusDto roomStatusDto);


    }
}

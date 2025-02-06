using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.Contracts;
using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Contracts;

namespace ApiHotel.Application.Services
{
    public class RoomService:IRoomService
    {
        private readonly IRoomDomain _roomDomain;
        public RoomService(IRoomDomain roomDomain) 
        {
            _roomDomain = roomDomain;
        }


        public Result<bool> UpdateRoom(RoomUpdateDto roomDto)
        {
            try
            {
                var room = _roomDomain.GetRoomById(roomDto.Id);
                if (room == null)
                    return Result<bool>.BadRequest(new List<string> { "Room not found" }, "Validation error");

                room.BasePrice = roomDto.BasePrice;
                room.Taxes = roomDto.Taxes;
                if (!string.IsNullOrWhiteSpace(roomDto.Location)) room.Location = roomDto.Location;
                if (!string.IsNullOrWhiteSpace(roomDto.RoomType)) room.RoomType = roomDto.RoomType;

                _roomDomain.UpdateRoom(room);

                return Result<bool>.Success(true, "Room updated successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error updating room: {ex.Message}");
            }
        }

        public Result<bool> UpdateRoomStatus(RoomUpdateStatusDto roomStatusDto)
        {
            try
            {
                var room = _roomDomain.GetRoomById(roomStatusDto.Id);
                if (room == null)
                    return Result<bool>.BadRequest(new List<string> { "Room not found" }, "Validation error");

                room.IsActive = roomStatusDto.IsActive;

                _roomDomain.UpdateRoom(room);

                return Result<bool>.Success(true, roomStatusDto.IsActive ? "Room activated successfully" : "Room deactivated successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error updating room status: {ex.Message}");
            }
        }


    }
}

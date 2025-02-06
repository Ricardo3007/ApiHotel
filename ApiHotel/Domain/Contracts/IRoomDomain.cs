using ApiHotel.Domain.Entities;

namespace ApiHotel.Domain.Contracts
{
    public interface IRoomDomain
    {
        Room? GetRoomById(int roomId);

        void UpdateRoom(Room room);
    }
}

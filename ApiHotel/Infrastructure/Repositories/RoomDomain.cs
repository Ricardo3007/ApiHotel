using ApiHotel.Domain.Contracts;
using ApiHotel.Domain.Entities;
using ApiHotel.Infrastructure.Context;

namespace ApiHotel.Infrastructure.Repositories
{
    public class RoomDomain:IRoomDomain
    {

        private readonly HotelContext _context;

        public RoomDomain(HotelContext context)
        {
            _context = context;
        }

        public Room? GetRoomById(int roomId)
        {
            return _context.Room.FirstOrDefault(r => r.RoomId == roomId);
        }

        public void UpdateRoom(Room room)
        {
            _context.Room.Update(room);
            _context.SaveChanges();
        }
    }
}

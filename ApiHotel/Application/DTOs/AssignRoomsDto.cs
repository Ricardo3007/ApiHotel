namespace ApiHotel.Application.DTOs
{
    public class AssignRoomsDto
    {
        public int HotelId { get; set; }
        public List<RoomCreateDto> Rooms { get; set; } = new List<RoomCreateDto>();
    }

    public class RoomCreateDto
    {
        public string RoomType { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }
    }

}

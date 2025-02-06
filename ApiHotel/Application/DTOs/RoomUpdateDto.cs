namespace ApiHotel.Application.DTOs
{
    public class RoomUpdateDto
    {

        public int Id { get; set; }

        public int HotelId { get; set; }

        public string RoomType { get; set; } = null!;

        public decimal BasePrice { get; set; }

        public decimal Taxes { get; set; }

        public string? Location { get; set; }

    }
}

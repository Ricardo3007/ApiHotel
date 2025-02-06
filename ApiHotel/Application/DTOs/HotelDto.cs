namespace ApiHotel.Application.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal BasePrice { get; set; }

        public int AgentId { get; set; }

        public int AvailableRooms { get; set; }
    }
}

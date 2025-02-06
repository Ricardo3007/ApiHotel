namespace ApiHotel.Application.DTOs
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public string HotelName { get; set; } = null!;
        public string RoomType { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string EmergencyContactName { get; set; } = null!;
        public string EmergencyContactPhone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal TotalAmount { get; set; }  
        public bool IsActive { get; set; }

        public List<GuestDto> Guests { get; set; }

    }

}

namespace ApiHotel.Application.DTOs
{
    public class ReservationCreateDto
    {
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string EmergencyContactName { get; set; } = null!;
        public string EmergencyContactPhone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public List<GuestCreateDto> Guests { get; set; }
    }

    public class GuestCreateDto
    {

        public int ReservationId { get; set; }

        public string FullName { get; set; } = null!;

        public DateOnly BirthDate { get; set; }

        public string? Gender { get; set; }

        public string DocumentType { get; set; } = null!;

        public string DocumentNumber { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}

namespace ApiHotel.Application.DTOs
{
    public class ReservationDetailDto
    {
        public int ReservationId { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Email { get; set; }

        public List<GuestDetailDto> Guest { get; set; }
    }
    public class GuestDetailDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Phone { get; set; }
    }

    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }

        public HotelDto Hotel { get; set; }
    }


}

using System.ComponentModel.DataAnnotations;

namespace ApiHotel.Application.DTOs
{
    public class HotelSearchCriteriaDto
    {
        [Required(ErrorMessage = "The CheckIn Date is mandatory.")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "The CheckOut Date  is mandatory.")]
        public DateTime CheckOutDate { get; set; }
        //public int NumberOfGuests { get; set; }
        //public string DestinationCity { get; set; }
    }
}

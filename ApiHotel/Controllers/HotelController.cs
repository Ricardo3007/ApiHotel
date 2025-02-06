using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.Contracts;
using ApiHotel.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        /// <summary>
        /// Add hotel
        /// </summary>
        [HttpPost("[action]")]
        public Result<int> AddHotel(HotelDto hotelDTO)
        {
            return _hotelService.AddHotel(hotelDTO);
        }
        
        /// <summary>
        /// Assign rooms to a hotel
        /// </summary>
        [HttpPost("[action]")]
        public Result<bool> AssignRoomsToHotel(AssignRoomsDto assignRoomsDto)
        {
            return _hotelService.AssignRoomsToHotel(assignRoomsDto);
        }


        /// <summary>
        /// modify the values ​​of each hotel
        /// </summary>
        [HttpPut("[action]")]
        public Result<bool> UpdateHotel([FromBody] HotelDto hotelDto)
        {
            return _hotelService.UpdateHotel(hotelDto);
        }

        /// <summary>
        /// Update Hotel Status
        /// </summary>
        [HttpPut("[action]")]
        public Result<bool> UpdateHotelStatus([FromBody] HotelStatusDto hotelStatusDto)
        {
            return _hotelService.UpdateHotelStatus(hotelStatusDto);
        }

        /// <summary>
        /// hotel search engine
        /// </summary>
        [HttpGet("search")]
        public Result<List<HotelDto>> SearchHotels([FromQuery] HotelSearchCriteriaDto searchCriteria)
        {

            return _hotelService.SearchHotels(searchCriteria);

        }

        /// <summary>
        /// Get Available Rooms
        /// </summary>
        [HttpGet("available-rooms/{hotelId}")]
        public ActionResult<List<RoomDto>> GetAvailableRooms(int hotelId, [FromQuery] DateTime checkInDate, [FromQuery] DateTime checkOutDate, [FromQuery] int numberOfGuests)
        {
            var availableRooms = _hotelService.GetAvailableRooms(hotelId, checkInDate, checkOutDate, numberOfGuests);

            if (availableRooms == null || !availableRooms.Any())
            {
                return NotFound("No available rooms found.");
            }

            return Ok(availableRooms);
        }



    }
}

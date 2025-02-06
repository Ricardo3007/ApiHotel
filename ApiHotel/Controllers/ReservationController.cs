using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.Contracts;
using ApiHotel.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService) 
        { 
            _reservationService = reservationService;
        }



        /// <summary>
        /// list each of the reservations made in my hotels
        /// </summary>
        [HttpGet("[action]")]
        public Result<IEnumerable<ReservationDto>> GetReservationsByAgent(int agentId)
        {
            return _reservationService.GetReservationsByAgent(agentId);
        }


        /// <summary>
        /// detail of each of reservations made
        /// </summary>
        [HttpGet("{reservationId}")]
        public Result<ReservationDetailDto> GetReservationDetail(int reservationId)
        {

           return _reservationService.GetReservationDetail(reservationId);

        }

        /// <summary>
        /// Create a reservation
        /// </summary>
        [HttpPost("create")]
        public ActionResult<Result<int>> CreateReservation([FromBody] ReservationCreateDto reservationDto)
        {
            return _reservationService.CreateReservation(reservationDto);
           
        }


    }
}

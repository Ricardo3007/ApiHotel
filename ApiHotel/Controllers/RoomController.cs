using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.Contracts;
using ApiHotel.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService) 
        { 
            _roomService = roomService;
        }


        /// <summary>
        /// modify the values ​​of  each room
        /// </summary>
        [HttpPut("[action]")]
        public Result<bool> UpdateRoom([FromBody] RoomUpdateDto roomDto)
        {
            return _roomService.UpdateRoom(roomDto);
        }


        /// <summary>
        /// Update Room Status
        /// </summary>
        [HttpPut("[action]")]
        public Result<bool> UpdateRoomStatus([FromBody] RoomUpdateStatusDto roomStatusDto)
        {
            return _roomService.UpdateRoomStatus(roomStatusDto);
        }


    }
}

using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.Contracts;
using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Contracts;
using ApiHotel.Domain.Entities;
using AutoMapper;

namespace ApiHotel.Application.Services
{
    public class HotelService : IHotelService
    {

        private readonly IMapper _mapper;
        private readonly IHotelDomain _hotelDomain;

        public HotelService(IMapper mapper, IHotelDomain hotelDomain) 
        { 
            _mapper = mapper;
            _hotelDomain = hotelDomain;
        
        }


        public Result<int> AddHotel(HotelDto hotelDTO)
        {
            try
            {
                #region Validations
                // Validación del nombre obligatorio
                if (string.IsNullOrWhiteSpace(hotelDTO.Name))
                {
                    return Result<int>.BadRequest(new List<string> { "The hotel name is required." }, "Validation error");
                }

                // Validación de agente
                if (hotelDTO.AgentId == 0)
                {
                    return Result<int>.BadRequest(new List<string> { "The agent is invalid." }, "Validation error");
                }

                #endregion

                var hotel = _mapper.Map<Hotel>(hotelDTO);

                // Agregar el hotel
                int hotelId = _hotelDomain.AddHotel(hotel);

                // Asociar el hotel con el agente
                _hotelDomain.AssignHotelToAgent(hotelId, hotelDTO.AgentId);

                return Result<int>.Success(hotelId, "Hotel added successfully");
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Error adding hotel: {ex.Message}");
            }
        }

        public Result<bool> AssignRoomsToHotel(AssignRoomsDto assignRoomsDto)
        {
            try
            {
                #region Validations
                if (assignRoomsDto.HotelId <= 0)
                {
                    return Result<bool>.BadRequest(new List<string> { "Invalid hotel ID." }, "Validation error");
                }

                if (assignRoomsDto.Rooms == null || !assignRoomsDto.Rooms.Any())
                {
                    return Result<bool>.BadRequest(new List<string> { "At least one room must be assigned." }, "Validation error");
                }
                #endregion

                bool assigned = _hotelDomain.AssignRooms(assignRoomsDto.HotelId, assignRoomsDto.Rooms);
                return assigned
                    ? Result<bool>.Success(true, "Rooms assigned successfully.")
                    : Result<bool>.Failure("Failed to assign rooms.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error assigning rooms: {ex.Message}");
            }
        }



        public Result<bool> UpdateHotel(HotelDto hotelDto)
        {
            try
            {
                var hotel = _hotelDomain.GetHotelById(hotelDto.Id);
                if (hotel == null)
                    return Result<bool>.BadRequest(new List<string> { "Hotel not found" }, "Validation error");

                hotel.Name = hotelDto.Name;
                hotel.Description = hotelDto.Description;
                hotel.BasePrice = hotelDto.BasePrice;

                _hotelDomain.UpdateHotel(hotel);

                return Result<bool>.Success(true, "Hotel updated successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error updating hotel: {ex.Message}");
            }
        }


        public Result<bool> UpdateHotelStatus(HotelStatusDto hotelStatusDto)
        {
            try
            {
                var hotel = _hotelDomain.GetHotelById(hotelStatusDto.Id);
                if (hotel == null)
                    return Result<bool>.BadRequest(new List<string> { "Hotel not found" }, "Validation error");

                hotel.IsActive = hotelStatusDto.IsActive;

                _hotelDomain.UpdateHotel(hotel);

                return Result<bool>.Success(true, "Hotel status updated successfully");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error updating hotel status: {ex.Message}");
            }
        }

        public Result<List<HotelDto>> SearchHotels(HotelSearchCriteriaDto searchCriteria)
        {
          
            try
            {
                var hotelDtos = _hotelDomain.SearchHotels(searchCriteria);


                if (hotelDtos == null || !hotelDtos.Any())
                {
                    return Result<List<HotelDto>>.Success(new List<HotelDto>(), "No hotels found based on the search criteria.");
                }

                return Result<List<HotelDto>>.Success(hotelDtos, "Hotels found successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<HotelDto>>.Failure($"Error searching hotels: {ex.Message}");
            }

        }


        public List<RoomDto> GetAvailableRooms(int hotelId, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
        {
            // Obtener todas las habitaciones del hotel
            var rooms = _hotelDomain.GetRoomsByHotel(hotelId);

            // Filtrar las habitaciones disponibles
            var availableRooms = rooms.Where(room =>
                room.IsActive &&
                !room.Reservation.Any(res =>
                    (res.CheckInDate < checkOutDate && res.CheckOutDate > checkInDate)  // Verificar si la habitación ya está reservada
                ))
            .Select(room => new RoomDto
            {
                RoomId = room.RoomId,
                RoomType = room.RoomType,
                BasePrice = room.BasePrice,
                Location = room.Location,
            }).ToList();

            return availableRooms;
        }




    }
}

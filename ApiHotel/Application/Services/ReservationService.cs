using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.Contracts;
using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Contracts;
using ApiHotel.Domain.Entities;
using ApiHotel.Infrastructure.Repositories;
using AutoMapper;
using RecreationalClub.Applications;

namespace ApiHotel.Application.Services
{
    public class ReservationService:IReservationService
    {

        private readonly IMapper _mapper;
        private readonly IReservationDomain _reservationDomain;
        private readonly IHotelDomain _hotelDomain;
        private readonly IRoomDomain _roomDomain;
        private readonly EmailService _emailService;

        public ReservationService(
            IMapper mapper,
            IReservationDomain reservationDomain, 
            IHotelDomain hotelDomain, 
            IRoomDomain roomDomain,
            EmailService emailService
            ) 
        { 
            _mapper = mapper;
            _reservationDomain = reservationDomain;
            _roomDomain = roomDomain;
            _hotelDomain = hotelDomain;
            _emailService = emailService;
        }


        public Result<IEnumerable<ReservationDto>> GetReservationsByAgent(int agentId)
        {
            try
            {
                #region validations
                if (agentId == 0)
                {
                    return Result<IEnumerable<ReservationDto>>.BadRequest(new List<string> { "Agent not found." }, "Validation error");
                }
                #endregion

                var reservations = _reservationDomain.GetReservationsByAgent(agentId);

                var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

                return Result<IEnumerable<ReservationDto>>.Success(reservationDtos, "Reservations retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ReservationDto>>.Failure($"Error retrieving reservations: {ex.Message}");
            }
        }


        public Result<ReservationDetailDto> GetReservationDetail(int reservationId)
        {

            try
            {
                var reservationDetailDto = _reservationDomain.GetReservationDetail(reservationId);

                if (reservationDetailDto == null)
                {
                   return Result<ReservationDetailDto>.BadRequest(new List<string> { "Reservation not found." }, "Not found");

                }

                return Result<ReservationDetailDto>.Success(reservationDetailDto, "Reservation details retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<ReservationDetailDto>.Failure($"Error retrieving reservation details: {ex.Message}");
            }
        }


        public Result<int> CreateReservation(ReservationCreateDto reservationDto)
        {
            try
            {
                #region validations
                if (reservationDto.CheckInDate < DateTime.Today || reservationDto.CheckOutDate <= reservationDto.CheckInDate)
                {
                    return Result<int>.BadRequest(new List<string> { "Invalid reservation dates." }, "Validation error");
                }
                #endregion

                // Crear la reserva con RoomId
                var reservation = new Reservation
                {
                    RoomId = reservationDto.RoomId, // 🔹 Asignando RoomId correctamente
                    CheckInDate = reservationDto.CheckInDate,
                    CheckOutDate = reservationDto.CheckOutDate,
                    NumberOfGuests = reservationDto.NumberOfGuests,
                    EmergencyContactName = reservationDto.EmergencyContactName,
                    EmergencyContactPhone = reservationDto.EmergencyContactPhone,
                    Email = reservationDto.Email
                };

                // Guardar la reserva y obtener el ID
                var reservationId = _reservationDomain.AddReservation(reservation);

                // Asignar el ID de la reserva a los huéspedes y guardarlos
                foreach (var guestDto in reservationDto.Guests)
                {
                    var guest = new Guest
                    {
                        ReservationId = reservationId, 
                        FullName = guestDto.FullName,
                        BirthDate = guestDto.BirthDate,
                        Gender = guestDto.Gender,
                        DocumentType = guestDto.DocumentType,
                        DocumentNumber = guestDto.DocumentNumber,
                        Phone = guestDto.Phone
                    };
                    _reservationDomain.AddGuest(guest); // Guardar cada huésped
                }


                try
                {

                    // Enviar correo electrónico
                    var subject = "Reservation Confirmation";
                    var body = $"Hello  {reservationDto.Guests?.FirstOrDefault()?.FullName},\n\n Thank you for visiting us. " +
                                $"Your reservation has been successfully registered. " +
                                $"Check-in date: {reservationDto.CheckInDate}, Check-out date: {reservationDto.CheckOutDate}.\n\n" +
                                "¡See you next time!";

                    //var body = $"Hola mundo!";

                    _emailService.SendEmailAsync(reservationDto.Email, subject, body).Wait();
                    //_emailService.SendEmailAsync("ferrerricardoantonio@gmail.com", subject, body).Wait();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");

                    return Result<int>.Failure("Reservation successfully!, problems sending email.");
                }
                return Result<int>.Success(reservationId, "Reservation created successfully.");
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Error creating reservation: {ex.Message}");
            }
        }





    }
}

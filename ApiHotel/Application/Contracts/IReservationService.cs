using ApiEliteWebAcceso.Application.Response;
using ApiHotel.Application.DTOs;

namespace ApiHotel.Application.Contracts
{
    public interface IReservationService
    {
        Result<IEnumerable<ReservationDto>> GetReservationsByAgent(int agentId);

        Result<ReservationDetailDto> GetReservationDetail(int reservationId);

        Result<int> CreateReservation(ReservationCreateDto reservationDto);
    }
}

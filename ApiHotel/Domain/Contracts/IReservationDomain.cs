using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Entities;

namespace ApiHotel.Domain.Contracts
{
    public interface IReservationDomain
    {
        List<Reservation> GetReservationsByAgent(int agentId);

        ReservationDetailDto GetReservationDetail(int reservationId);

        int AddReservation(Reservation reservation);

        void AddGuest(Guest guest);

    }
}

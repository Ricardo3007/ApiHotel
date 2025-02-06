using ApiHotel.Application.DTOs;
using ApiHotel.Domain.Contracts;
using ApiHotel.Domain.Entities;
using ApiHotel.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiHotel.Infrastructure.Repositories
{
    public class ReservationDomain:IReservationDomain
    {

        private readonly HotelContext _context;

        public ReservationDomain(HotelContext context)
        {
            _context = context;
        }

        public List<Reservation> GetReservationsByAgent(int agentId)
        {
            return _context.Reservation
                           .Where(r => r.Room.Hotel.AgentHotel.Any(ah => ah.AgentId == agentId)) 
                           .Include(r => r.Room)
                           .ThenInclude(room => room.Hotel)
                           .ToList();
        }

        public ReservationDetailDto GetReservationDetail(int reservationId)
        {
            return _context.Reservation
                           .Where(r => r.ReservationId == reservationId)
                           .Include(r => r.Guest)
                           .Select(r => new ReservationDetailDto
                           {
                               ReservationId = r.ReservationId,
                               CheckInDate = r.CheckInDate.ToString("yyyy-MM-dd HH:mm:ss"),
                               CheckOutDate = r.CheckOutDate.ToString("yyyy-MM-dd HH:mm:ss"),
                               NumberOfGuests = r.NumberOfGuests,
                               EmergencyContactName = r.EmergencyContactName,
                               EmergencyContactPhone = r.EmergencyContactPhone,
                               Email = r.Email,
                               Guest = r.Guest.Select(g => new GuestDetailDto
                               {
                                   FullName = g.FullName,
                                   //BirthDate = g.BirthDate,
                                   Gender = g.Gender,
                                   DocumentType = g.DocumentType,
                                   DocumentNumber = g.DocumentNumber,
                                   Phone = g.Phone
                               }).ToList()
                           })
                           .FirstOrDefault();
        }



        public int AddReservation(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            _context.SaveChanges();
            return reservation.ReservationId;
        }

        public void AddGuest(Guest guest)
        {
            _context.Guest.Add(guest);
            _context.SaveChanges();
        }




    }
}

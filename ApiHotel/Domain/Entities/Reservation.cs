using System;
using System.Collections.Generic;

namespace ApiHotel.Domain.Entities;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int NumberOfGuests { get; set; }

    public string EmergencyContactName { get; set; } = null!;

    public string EmergencyContactPhone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Guest> Guest { get; set; } = new List<Guest>();

    public virtual Room Room { get; set; } = null!;
}

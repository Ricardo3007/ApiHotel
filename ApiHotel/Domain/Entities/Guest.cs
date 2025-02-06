using System;
using System.Collections.Generic;

namespace ApiHotel.Domain.Entities;

public partial class Guest
{
    public int GuestId { get; set; }

    public int ReservationId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string? Gender { get; set; }

    public string DocumentType { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;
}

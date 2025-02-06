using System;
using System.Collections.Generic;

namespace ApiHotel.Domain.Entities;

public partial class Room
{
    public int RoomId { get; set; }

    public int HotelId { get; set; }

    public string RoomType { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public decimal Taxes { get; set; }

    public string? Location { get; set; }

    public bool IsActive { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<Reservation> Reservation { get; set; } = new List<Reservation>();
}

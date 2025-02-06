using System;
using System.Collections.Generic;

namespace ApiHotel.Domain.Entities;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal BasePrice { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AgentHotel> AgentHotel { get; set; } = new List<AgentHotel>();

    public virtual ICollection<Room> Room { get; set; } = new List<Room>();
}

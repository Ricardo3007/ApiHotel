using System;
using System.Collections.Generic;

namespace ApiHotel.Domain.Entities;

public partial class AgentHotel
{
    public int AgentHotelId { get; set; }

    public int AgentId { get; set; }

    public int HotelId { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Hotel Hotel { get; set; } = null!;
}

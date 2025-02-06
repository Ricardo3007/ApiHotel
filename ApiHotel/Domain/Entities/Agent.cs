using System;
using System.Collections.Generic;

namespace ApiHotel.Domain.Entities;

public partial class Agent
{
    public int AgentId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AgentHotel> AgentHotel { get; set; } = new List<AgentHotel>();
}

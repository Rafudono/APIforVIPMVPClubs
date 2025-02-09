using System;
using System.Collections.Generic;

namespace APIforVIPMVPClubs;

public partial class Club
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int IdBoss { get; set; }

    public byte[]? Image { get; set; }

    public virtual User IdBossNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

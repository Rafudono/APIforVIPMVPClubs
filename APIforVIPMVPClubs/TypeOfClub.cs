using System;
using System.Collections.Generic;

namespace APIforVIPMVPClubs;

public partial class TypeOfClub
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();
}

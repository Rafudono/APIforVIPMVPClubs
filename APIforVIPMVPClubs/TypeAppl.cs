using System;
using System.Collections.Generic;

namespace APIforVIPMVPClubs;

public partial class TypeAppl
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Application1> Application1s { get; set; } = new List<Application1>();
}

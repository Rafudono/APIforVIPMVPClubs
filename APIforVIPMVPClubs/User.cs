using System;
using System.Collections.Generic;

namespace APIforVIPMVPClubs;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Patronymic { get; set; }

    public string? Phone { get; set; }

    public int? IdRole { get; set; }

    public virtual ICollection<Application1> Application1s { get; set; } = new List<Application1>();

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Club> ClubsNavigation { get; set; } = new List<Club>();
}

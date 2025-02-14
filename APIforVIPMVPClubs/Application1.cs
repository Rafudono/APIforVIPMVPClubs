using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIforVIPMVPClubs;

public partial class Application1
{
    public int Id { get; set; }

    public int IdStatus { get; set; }

    public byte[]? Image { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime DateOfFiling { get; set; }

    public int IdApplicant { get; set; }

    public int? IdClub { get; set; }

    public int IdType { get; set; }
    public virtual User IdApplicantNavigation { get; set; } = null!;
    public virtual Club? IdClubNavigation { get; set; }
    public virtual StatusApplication IdStatusNavigation { get; set; } = null!;
    public virtual TypeAppl IdTypeNavigation { get; set; } = null!;
}

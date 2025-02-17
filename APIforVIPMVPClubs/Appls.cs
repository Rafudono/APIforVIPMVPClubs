using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIforVIPMVPClubs
{
    public class Appls
    {
        public int Id { get; set; }

        public int IdStatus { get; set; }

        public byte[]? Image { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime DateOfFiling { get; set; }

        public int IdApplicant { get; set; }
        public int? IdTypeClub { get; set; }
        public int? IdClub { get; set; }

        public int IdType { get; set; }

        public User User { get; set; }
    }
}

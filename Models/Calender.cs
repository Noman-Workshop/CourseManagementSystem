using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Calender
    {
        public Calender()
        {
            Agenda = new HashSet<Agendum>();
        }

        public DateTime Day { get; set; }
        public string Type { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Agendum> Agenda { get; set; }
    }
}

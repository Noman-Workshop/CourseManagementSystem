using System;
using System.Collections.Generic;

namespace Models
{
    public partial class ClassTeacher
    {
        public Guid TeacherId { get; set; }
        public Guid ClassId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
    }
}

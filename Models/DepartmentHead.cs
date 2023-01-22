using System;
using System.Collections.Generic;

namespace Models
{
    public partial class DepartmentHead
    {
        public Guid TeacherId { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Course
    {
        public Course()
        {
            Classes = new HashSet<Class>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Credits { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<Class> Classes { get; set; }
    }
}

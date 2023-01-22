using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Department
    {
        public Department()
        {
            Courses = new HashSet<Course>();
            DepartmentHeads = new HashSet<DepartmentHead>();
            Teachers = new HashSet<Teacher>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<DepartmentHead> DepartmentHeads { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            ClassTeachers = new HashSet<ClassTeacher>();
            DepartmentHeads = new HashSet<DepartmentHead>();
        }

        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Employee IdNavigation { get; set; } = null!;
        public virtual ICollection<ClassTeacher> ClassTeachers { get; set; }
        public virtual ICollection<DepartmentHead> DepartmentHeads { get; set; }
    }
}

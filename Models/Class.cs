using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Class
    {
        public Class()
        {
            ClassTeachers = new HashSet<ClassTeacher>();
            Enrollments = new HashSet<Enrollment>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<ClassTeacher> ClassTeachers { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

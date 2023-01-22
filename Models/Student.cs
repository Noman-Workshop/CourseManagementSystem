using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Student
    {
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

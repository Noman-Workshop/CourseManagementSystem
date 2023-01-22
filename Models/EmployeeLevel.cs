using System;
using System.Collections.Generic;

namespace Models
{
    public partial class EmployeeLevel
    {
        public EmployeeLevel()
        {
            Employees = new HashSet<Employee>();
        }

        public string Level { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Rank { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

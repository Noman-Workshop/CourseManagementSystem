using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Migration
    {
        public int Id { get; set; }
        public string Version { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

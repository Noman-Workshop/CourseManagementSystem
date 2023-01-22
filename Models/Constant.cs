using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Constant
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public string Discriminator { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

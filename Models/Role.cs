using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Role {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }

	[StringLength(50, MinimumLength = 3)]
	public string Name { get; set; }

	public ICollection<User> Users { get; set; }
}

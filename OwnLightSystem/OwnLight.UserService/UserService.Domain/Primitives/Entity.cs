using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Primitives;

public abstract class Entity
{
    [Key, Required]
    public Guid Id { get; protected set; } = Guid.NewGuid();

    [Required]
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; }

}

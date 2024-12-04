using System.ComponentModel.DataAnnotations;

namespace DeviceService.Domain.Primitives;

public abstract class Entity
{
    [Key]
    public Guid Id { get; protected set; } = Guid.NewGuid();

    public virtual DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; protected set; }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace AutomationService.Domain.Entities;

public class Room
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
    public string? DeviceIds { get; set; }

    [NotMapped]
    public ICollection<Guid> DeviceIdsList
    {
        get =>
            string.IsNullOrEmpty(DeviceIds) ? [] : DeviceIds.Split(',').Select(Guid.Parse).ToList();
        set => DeviceIds = string.Join(',', value);
    }
}

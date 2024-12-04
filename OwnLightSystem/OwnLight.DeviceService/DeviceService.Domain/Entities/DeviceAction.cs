using System.ComponentModel.DataAnnotations.Schema;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Primitives;

namespace DeviceService.Domain.Entities;

public class DeviceAction : Entity
{
    public Guid UserId { get; set; }

    public Guid DeviceId { get; set; }

    public DeviceActions Action { get; set; }
    
    public ActionStatus Status { get; set; }

    [Column("PerformedAt")]
    public override DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public Device Device { get; set; } = null!;
}

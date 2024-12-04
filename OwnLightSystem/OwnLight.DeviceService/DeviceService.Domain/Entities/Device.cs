using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeviceService.Domain.Enums;
using DeviceService.Domain.Primitives;

namespace DeviceService.Domain.Entities;

public class Device : Entity
{
    public Guid DeviceTypeId { get; set; }
    public virtual required DeviceType DeviceType { get; set; }

    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }

    public Guid? GroupId { get; set; }

    [Range(3, 50)]
    public required string Name { get; set; }

    [DefaultValue(false)]
    public bool? IsDimmable { get; set; }

    [Range(0, 100)]
    public int? Brightness { get; set; }
    
    public DeviceStatus Status { get; set; }

    public ICollection<DeviceAction> DeviceActions { get; set; } = [];
}

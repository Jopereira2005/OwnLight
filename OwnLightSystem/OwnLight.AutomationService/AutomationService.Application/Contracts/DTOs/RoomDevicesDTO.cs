namespace AutomationService.Application.Contracts.DTOs;

public class RoomDevicesDTO
{
    public string Name { get; set; } = null!;
    public List<string>? DeviceIds { get; set; }
}

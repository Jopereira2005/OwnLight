namespace AutomationService.Application.Contracts.DTOs;

public class GroupDevicesDTO
{
    public string Name { get; set; } = null!;
    public List<string>? DeviceIds { get; set; }
}

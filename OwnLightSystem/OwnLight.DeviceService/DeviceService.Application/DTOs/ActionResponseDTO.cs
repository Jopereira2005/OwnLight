namespace DeviceService.Application.DTOs;

public class ActionResponseDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DeviceId { get; set; }
    public required string Action { get; set; }
    public DateTime PerformedAt { get; set; }
    public required string Status { get; set; }
}

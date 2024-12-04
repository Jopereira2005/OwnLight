namespace AutomationService.Application.Contracts.DTOs;

public class RoomResponseDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }

}

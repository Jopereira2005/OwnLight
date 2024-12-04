namespace AutomationService.Application.Contracts.DTOs;

public class GroupResponseDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}

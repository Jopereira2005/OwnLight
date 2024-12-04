namespace UserService.Application.DTOs;

public class UserResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ProfilePictureUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

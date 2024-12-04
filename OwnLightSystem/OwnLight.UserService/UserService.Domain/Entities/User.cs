using System.ComponentModel.DataAnnotations;
using UserService.Domain.Primitives;

namespace UserService.Domain.Entities;

public class User : Entity
{
    [Range(3, 50)]
    public required string Name { get; set; }

    [Range(3, 50)]
    public required string Username { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public ICollection<RefreshToken> Tokens { get; set; } = [];

    public void UpdatePassword(string password)
    {
        Password = password;
        UpdatedAt = DateTime.UtcNow;
    }
}

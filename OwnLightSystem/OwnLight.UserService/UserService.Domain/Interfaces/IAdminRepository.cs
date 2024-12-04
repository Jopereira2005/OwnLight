using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IAdminRepository
{
    public Task<User?> DeleteAllAsync();
}

using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User?> UpdatePasswordAsync(Guid id, string passwordHash);
    public Task<User?> UpdateAsync(User user);
    public Task<User?> DeleteAsync(Guid id);
    public Task<User> RegisterAsync(User user);
    public Task<User?> FindByIdAsync(Guid id);
    public Task<User?> FindByUsernameAsync(string username);
    public Task<User?> FindByEmailAsync(string email);
    public Task<IEnumerable<User>> FindAllAsync(int page, int pageSize);
    public Task<int> CountAsync();
}

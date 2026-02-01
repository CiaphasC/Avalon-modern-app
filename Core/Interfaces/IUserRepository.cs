using AvaloniaModernApp.Core.Entities;
using System.Threading.Tasks;

namespace AvaloniaModernApp.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task SaveAsync(); // Not typical for Repository but useful for simple JSON implementation
}

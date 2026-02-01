using AvaloniaModernApp.Core.Entities;
using System.Threading.Tasks;

namespace AvaloniaModernApp.Core.Interfaces;

public interface IAuthService
{
    Task<User?> LoginAsync(string email, string password);
    Task<bool> RegisterAsync(string email, string password, string name);
    User? CurrentUser { get; }
}

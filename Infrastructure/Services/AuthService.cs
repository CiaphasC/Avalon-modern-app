using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.Core.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaModernApp.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public User? CurrentUser { get; private set; }

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user != null && VerifyPassword(password, user.PasswordHash))
        {
            CurrentUser = user;
            return user;
        }
        return null;
    }

    public async Task<bool> RegisterAsync(string email, string password, string name)
    {
        if (await _userRepository.GetByEmailAsync(email) != null)
        {
            return false; // User already exists
        }

        var newUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
            Name = name,
            PasswordHash = HashPassword(password)
        };

        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveAsync();
        return true;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return HashPassword(password) == storedHash;
    }
}

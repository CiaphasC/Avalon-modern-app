using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AvaloniaModernApp.Infrastructure.Persistence;

public class JsonUserRepository : IUserRepository
{
    private const string FILE_NAME = "users.json";
    private List<User> _users = new();

    public JsonUserRepository()
    {
        if (File.Exists(FILE_NAME))
        {
            var json = File.ReadAllText(FILE_NAME);
            _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
    }

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(FILE_NAME, json);
    }
}

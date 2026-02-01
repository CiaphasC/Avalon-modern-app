using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.Core.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AvaloniaModernApp.Infrastructure.Persistence;

public class JsonContactsRepository : IContactsRepository
{
    private const string FILE_NAME = "contacts.json";
    private List<Contact> _contacts = new();

    public JsonContactsRepository()
    {
        if (File.Exists(FILE_NAME))
        {
            var json = File.ReadAllText(FILE_NAME);
            var list = JsonSerializer.Deserialize<List<Contact>>(json);
            if (list != null)
                _contacts = list;
        }
        else
        {
            SeedDefaults();
            SaveAsync().GetAwaiter().GetResult();
        }
    }

    public Task<IReadOnlyList<Contact>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Contact>>(_contacts);
    }

    public Task AddAsync(Contact contact)
    {
        _contacts.Add(contact);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_contacts, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(FILE_NAME, json);
    }

    private void SeedDefaults()
    {
        _contacts.Add(new Contact { Name = "Alice Johnson", PhoneNumber = "+1 555 0100" });
        _contacts.Add(new Contact { Name = "Bob Smith", PhoneNumber = "+1 555 0101" });
        _contacts.Add(new Contact { Name = "Charlie Brown", PhoneNumber = "+1 555 0102" });
        _contacts.Add(new Contact { Name = "David Wilson", PhoneNumber = "+1 555 0103" });
        _contacts.Add(new Contact { Name = "Eva Martin", PhoneNumber = "+1 555 0104" });
    }
}

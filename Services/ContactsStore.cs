using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaModernApp.Services;

public class ContactsStore : IContactsStore
{
    private readonly IContactsRepository _repository;
    public ObservableCollection<Contact> Contacts { get; } = new();
    private bool _initialized;

    public ContactsStore(IContactsRepository repository)
    {
        _repository = repository;
    }

    public async Task InitializeAsync()
    {
        if (_initialized)
            return;

        var items = await _repository.GetAllAsync().ConfigureAwait(true);
        Contacts.Clear();
        foreach (var contact in items)
        {
            Contacts.Add(contact);
        }

        _initialized = true;
    }

    public async Task AddAsync(Contact contact)
    {
        Contacts.Add(contact);
        await _repository.AddAsync(contact).ConfigureAwait(false);
        await _repository.SaveAsync().ConfigureAwait(false);
    }
}

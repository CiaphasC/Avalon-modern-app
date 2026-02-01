using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaModernApp.ViewModels;

public partial class ContactsViewModel : ViewModelBase
{
    private readonly IContactsStore _contactsStore;

    public ObservableCollection<Contact> Contacts => _contactsStore.Contacts;

    public IEnumerable<Contact> FilteredContacts =>
        string.IsNullOrWhiteSpace(SearchText)
            ? Contacts
            : Contacts.Where(c =>
                c.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                c.PhoneNumber.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

    [ObservableProperty]
    private string _searchText = "";

    public ContactsViewModel(IContactsStore contactsStore)
    {
        _contactsStore = contactsStore;
        _contactsStore.Contacts.CollectionChanged += OnContactsChanged;
    }

    partial void OnSearchTextChanged(string value)
    {
        OnPropertyChanged(nameof(FilteredContacts));
    }

    public async Task AddContactAsync(Contact contact)
    {
        if (string.IsNullOrWhiteSpace(contact.Name) || string.IsNullOrWhiteSpace(contact.PhoneNumber))
            return;

        await _contactsStore.AddAsync(new Contact
        {
            Name = contact.Name.Trim(),
            PhoneNumber = contact.PhoneNumber.Trim()
        });
    }

    public async Task AddContactAsync(string name, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phoneNumber))
            return;

        await _contactsStore.AddAsync(new Contact
        {
            Name = name.Trim(),
            PhoneNumber = phoneNumber.Trim()
        });
    }

    private void OnContactsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(FilteredContacts));
    }
}

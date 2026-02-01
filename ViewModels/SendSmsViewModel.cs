using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaModernApp.ViewModels;

public partial class SendSmsViewModel : ViewModelBase
{
    private readonly IContactsStore _contactsStore;

    public ObservableCollection<SelectableContactViewModel> Contacts { get; } = new();

    [ObservableProperty]
    private string _message = "";

    [ObservableProperty]
    private string _searchText = "";

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private string? _statusMessage;

    public IEnumerable<SelectableContactViewModel> FilteredContacts =>
        string.IsNullOrWhiteSpace(SearchText)
            ? Contacts
            : Contacts.Where(c =>
                c.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                c.PhoneNumber.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

    public int SelectedCount => Contacts.Count(c => c.IsSelected);

    public int MessageLength => Message?.Length ?? 0;

    public SendSmsViewModel(IContactsStore contactsStore)
    {
        _contactsStore = contactsStore;

        foreach (var contact in _contactsStore.Contacts)
        {
            AddSelectable(contact);
        }

        _contactsStore.Contacts.CollectionChanged += OnContactsChanged;
    }

    partial void OnSearchTextChanged(string value)
    {
        OnPropertyChanged(nameof(FilteredContacts));
    }

    partial void OnMessageChanged(string value)
    {
        OnPropertyChanged(nameof(MessageLength));
    }

    private void OnContactsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (var item in e.NewItems)
            {
                if (item is Contact contact)
                {
                    AddSelectable(contact);
                }
            }
        }

        OnPropertyChanged(nameof(FilteredContacts));
    }

    private void AddSelectable(Contact contact)
    {
        var item = new SelectableContactViewModel(contact);
        item.PropertyChanged += OnSelectablePropertyChanged;
        Contacts.Add(item);
    }

    private void OnSelectablePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SelectableContactViewModel.IsSelected))
        {
            OnPropertyChanged(nameof(SelectedCount));
        }
    }

    [RelayCommand]
    private async Task Send()
    {
        ErrorMessage = null;
        StatusMessage = null;

        var recipients = Contacts.Where(c => c.IsSelected).ToList();
        if (recipients.Count == 0)
        {
            ErrorMessage = "Selecciona al menos un contacto.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Message))
        {
            ErrorMessage = "El Message es obligatorio.";
            return;
        }

        // Logic to send SMS would go here
        await Task.Delay(500); // Simulate work

        StatusMessage = $"Message enviado a {recipients.Count} contacto(s).";
        Message = "";
    }
}

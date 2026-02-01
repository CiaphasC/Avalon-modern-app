using AvaloniaModernApp.Core.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaModernApp.ViewModels;

public partial class SelectableContactViewModel : ObservableObject
{
    public Contact Contact { get; }

    public string Name => Contact.Name;
    public string PhoneNumber => Contact.PhoneNumber;
    public string Initials => Contact.Initials;

    [ObservableProperty]
    private bool _isSelected;

    public SelectableContactViewModel(Contact contact)
    {
        Contact = contact;
    }
}

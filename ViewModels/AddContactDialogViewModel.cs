using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaModernApp.ViewModels;

public partial class AddContactDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name = "";

    [ObservableProperty]
    private string _phoneNumber = "";

    public string IconLetter =>
        string.IsNullOrWhiteSpace(Name)
            ? "_"
            : Name.Trim().Substring(0, 1).ToUpper();

    public bool IsValid =>
        !string.IsNullOrWhiteSpace(Name) &&
        !string.IsNullOrWhiteSpace(PhoneNumber);

    partial void OnNameChanged(string value)
    {
        OnPropertyChanged(nameof(IsValid));
        OnPropertyChanged(nameof(IconLetter));
    }

    partial void OnPhoneNumberChanged(string value)
    {
        OnPropertyChanged(nameof(IsValid));
    }
}

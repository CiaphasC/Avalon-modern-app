using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.ViewModels;

namespace AvaloniaModernApp.Views;

public partial class AddContactDialog : Window
{
    public AddContactDialog()
    {
        InitializeComponent();
        DataContext = new AddContactDialogViewModel();
    }

    private AddContactDialogViewModel ViewModel => (AddContactDialogViewModel)DataContext!;

    private void OnAddClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (!ViewModel.IsValid)
            return;

        Close(new Contact
        {
            Name = ViewModel.Name.Trim(),
            PhoneNumber = ViewModel.PhoneNumber.Trim()
        });
    }

    private void OnCancelClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close(null);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

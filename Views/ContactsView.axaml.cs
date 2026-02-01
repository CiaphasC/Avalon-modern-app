using Avalonia.Controls;
using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.ViewModels;

namespace AvaloniaModernApp.Views;

public partial class ContactsView : UserControl
{
    public ContactsView()
    {
        InitializeComponent();
    }

    private async void OnAddContactClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var dialog = new AddContactDialog();
        var owner = TopLevel.GetTopLevel(this) as Window;
        if (owner != null)
        {
            dialog.Icon = owner.Icon;
            var result = await dialog.ShowDialog<Contact?>(owner);
            if (result != null && DataContext is ContactsViewModel vm)
            {
                await vm.AddContactAsync(result);
            }
        }
        else
        {
            dialog.Show();
        }
    }
}

using AvaloniaModernApp.Core.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AvaloniaModernApp.ViewModels;

public partial class DashboardHomeViewModel : ViewModelBase
{
    public ObservableCollection<SmsStat> Stats { get; } = new();

    public DashboardHomeViewModel()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        Stats.Add(new SmsStat { Title = "SMS Enviados", Value = "1,245", Icon = "Send" });
        Stats.Add(new SmsStat { Title = "Contacts", Value = "854", Icon = "People" });
        Stats.Add(new SmsStat { Title = "Fallidos", Value = "12", Icon = "Alert" });
        Stats.Add(new SmsStat { Title = "Saldo", Value = "$45.00", Icon = "CreditCard" });
    }
}

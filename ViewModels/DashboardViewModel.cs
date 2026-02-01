using AvaloniaModernApp.Core.Entities;
using AvaloniaModernApp.Core.Interfaces;
using AvaloniaModernApp.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AvaloniaModernApp.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _userName = "";

    [ObservableProperty]
    private bool _isSidebarOpen = true;

    private readonly DashboardHomeViewModel _homeViewModel;
    private readonly SendSmsViewModel _sendSmsViewModel;
    private readonly ContactsViewModel _contactsViewModel;

    [ObservableProperty]
    private ViewModelBase _currentPage;

    public void Navigate(string tag)
    {
        switch (tag)
        {
            case "Dashboard":
                CurrentPage = _homeViewModel;
                break;
            case "SendSMS":
                CurrentPage = _sendSmsViewModel;
                break;
            case "Contacts":
                CurrentPage = _contactsViewModel;
                break;
            case "Logout":
                Logout();
                break;
        }
    }

    public DashboardViewModel(
        IAuthService authService, 
        INavigationService navigationService,
        DashboardHomeViewModel homeViewModel,
        SendSmsViewModel sendSmsViewModel,
        ContactsViewModel contactsViewModel)
    {
        _authService = authService;
        _navigationService = navigationService;
        _homeViewModel = homeViewModel;
        _sendSmsViewModel = sendSmsViewModel;
        _contactsViewModel = contactsViewModel;
        
        UserName = _authService.CurrentUser?.Name ?? "Usuario";

        // Set initial page
        CurrentPage = _homeViewModel;
    }

    [RelayCommand]
    private void ToggleSidebar()
    {
        IsSidebarOpen = !IsSidebarOpen;
    }

    [RelayCommand]
    private void Logout()
    {
        _navigationService.NavigateTo<LoginViewModel>();
    }
}

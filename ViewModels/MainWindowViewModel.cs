using AvaloniaModernApp.Services;
using AvaloniaModernApp.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaModernApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private ViewModelBase? _currentViewModel;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _currentViewModel = _navigationService.CurrentViewModel;
        
        // Subscribe to navigation changes
        ((NavigationService)_navigationService).PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(NavigationService.CurrentViewModel))
            {
                CurrentViewModel = _navigationService.CurrentViewModel;
            }
        };
    }
}

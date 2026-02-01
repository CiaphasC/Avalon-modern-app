using AvaloniaModernApp.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AvaloniaModernApp.UI.Services;

public interface INavigationService
{
    ViewModelBase? CurrentViewModel { get; }
    void NavigateTo<T>() where T : ViewModelBase;
}

public partial class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, ViewModelBase> _viewModelFactory;

    [ObservableProperty]
    private ViewModelBase? _currentViewModel;

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<T>() where T : ViewModelBase
    {
        CurrentViewModel = _viewModelFactory(typeof(T));
    }
}

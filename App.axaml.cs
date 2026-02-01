using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaModernApp.Core.Interfaces;
using AvaloniaModernApp.Infrastructure.Persistence;
using AvaloniaModernApp.Infrastructure.Services;
using AvaloniaModernApp.Services;
using AvaloniaModernApp.UI.Services;
using AvaloniaModernApp.ViewModels;
using AvaloniaModernApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AvaloniaModernApp;

public partial class App : Application
{
    public IServiceProvider? ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Service Collection
        var collection = new ServiceCollection();

        // Core & Infrastructure
        collection.AddSingleton<IUserRepository, JsonUserRepository>();
        collection.AddSingleton<IAuthService, AuthService>();
        collection.AddSingleton<IClockService, ClockService>();
        collection.AddSingleton<IContactsRepository, JsonContactsRepository>();
        collection.AddSingleton<IContactsStore, ContactsStore>();

        // UI Services
        collection.AddSingleton<INavigationService, NavigationService>();
        collection.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

        // ViewModels
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddTransient<LoginViewModel>();
        collection.AddTransient<DashboardViewModel>();
        collection.AddTransient<DashboardHomeViewModel>();
        collection.AddTransient<SendSmsViewModel>();
        collection.AddTransient<ContactsViewModel>();

        // Build Provider
        ServiceProvider = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();
            
            var mainViewModel = ServiceProvider?.GetRequiredService<MainWindowViewModel>();
            var contactsStore = ServiceProvider?.GetRequiredService<IContactsStore>();
            if (contactsStore != null)
            {
                _ = contactsStore.InitializeAsync();
            }
            
            // Initial Navigation
            var navService = ServiceProvider?.GetRequiredService<INavigationService>();
            navService?.NavigateTo<LoginViewModel>(); // Start at Login
            // navService?.NavigateTo<DashboardViewModel>(); // DEBUG: Start at Dashboard to test crash

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}

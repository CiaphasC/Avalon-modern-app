using AvaloniaModernApp.Core.Interfaces;
using AvaloniaModernApp.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace AvaloniaModernApp.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _email = "";

    [ObservableProperty]
    private string _password = "";
    
    [ObservableProperty]
    private string _name = "";

    [ObservableProperty]
    private bool _isRegistering;

    [ObservableProperty]
    private string? _errorMessage;

    public LoginViewModel(IAuthService authService, INavigationService navigationService)
    {
        _authService = authService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task Login()
    {
        ErrorMessage = null;
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Email y contrasena son obligatorios.";
            return;
        }

        try
        {
            var user = await _authService.LoginAsync(Email, Password);
            if (user != null)
            {
                _navigationService.NavigateTo<DashboardViewModel>();
            }
            else
            {
                ErrorMessage = "Credenciales invalidas.";
            }
        }
        catch (System.Exception ex)
        {
            ErrorMessage = $"Error de Login: {ex.Message}";
            // Log to console for debugging
            System.Console.WriteLine(ex);
        }
    }

    [RelayCommand]
    private async Task Register()
    {
        ErrorMessage = null;
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Name))
        {
            ErrorMessage = "Todos los campos son obligatorios.";
            return;
        }

        var success = await _authService.RegisterAsync(Email, Password, Name);
        if (success)
        {
             // Auto login after register or just switch mode
             IsRegistering = false;
             ErrorMessage = "Registro exitoso! Por favor Login.";
        }
        else
        {
            ErrorMessage = "El usuario ya existe.";
        }
    }

    [RelayCommand]
    private void ToggleMode()
    {
        IsRegistering = !IsRegistering;
        ErrorMessage = null;
    }
}

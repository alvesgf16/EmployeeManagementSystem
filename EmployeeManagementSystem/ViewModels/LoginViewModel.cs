using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _email;

    [ObservableProperty]
    private string? _password;

    #region Commands
    [RelayCommand]
    private async Task Login()
    {
        try
        {
            AuthService authService = new();
            var authenticatedUser = authService.AuthenticateUserLogin(Email, Password);

            if (await SecureStorage.Default.GetAsync("userId") is not null)
            {
                SecureStorage.Default.Remove("userId");
            }

            await SecureStorage.Default.SetAsync("userId", authenticatedUser.Id.ToString());
            App.AuthenticatedUser = authenticatedUser;
            await Constants.AddFlyoutMenuDetails();
        }
        catch (InvalidLoginException ex)
        {
            await Shell.Current.DisplayAlert("Login Failed", ex.Message, "OK");
        }
    }
    #endregion
}
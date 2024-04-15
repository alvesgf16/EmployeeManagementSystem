using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        try
        {
            AuthService authService = new();
            User authenticatedUser = authService.AuthenticateUserLogin(username.Text, password.Text);
            await SecureStorage.Default.SetAsync("user", authenticatedUser.Username);
            await Shell.Current.GoToAsync(nameof(ScheduleView));
        }
        catch (InvalidLoginException ex)
        {
            await DisplayAlert("Login Failed", ex.Message, "OK");
        }
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        OnLoginButtonClicked(sender, e);
    }

}
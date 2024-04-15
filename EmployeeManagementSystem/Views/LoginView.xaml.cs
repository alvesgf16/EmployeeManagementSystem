using EmployeeManagementSystem.Exceptions;
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
            int authenticatedUserId = authService.AuthenticateUserLogin(email.Text, password.Text);
            await SecureStorage.Default.SetAsync("user", authenticatedUserId.ToString());
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
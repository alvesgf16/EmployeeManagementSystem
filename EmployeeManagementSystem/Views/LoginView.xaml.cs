using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem;

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
            var authenticatedUser = new Dictionary<string, object>()
            {
                { "user", authService.AuthenticateUserLogin(username.Text, password.Text) }
            };
            await Shell.Current.GoToAsync(nameof(ManagePayView), authenticatedUser);
        }
        catch (InvalidLoginException ex)
        {
            await DisplayAlert("Login Failed", ex.Message, "OK");
        }
    }

    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterView));
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        OnLoginButtonClicked(sender, e);
    }

}
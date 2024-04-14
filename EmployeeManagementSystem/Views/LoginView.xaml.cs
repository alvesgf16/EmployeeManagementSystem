using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Views;

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
            User authenticatedUser = authService.AuthenticateUserLogin(username.Text, password.Text);
            await Navigation.PushModalAsync(new ManageEmployeeView());
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
}
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Views;
using SQLite;

namespace EmployeeManagementSystem;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}

    private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		
        AuthService authService = new AuthService();
        TableQuery<User> users = authService.GetUsers();
        if (authService.AuthenticateUserLogin(username.Text, password.Text, users))
        {
            AppShell.Current.GoToAsync(nameof(DashboardView));
        }
        else
        {
            DisplayAlert("Login Failed", "Invalid username or password", "OK");
        }
    }

    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        AppShell.Current.GoToAsync(nameof(CreateUserView));
    }
}
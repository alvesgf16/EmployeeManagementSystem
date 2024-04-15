using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView()
	{
		InitializeComponent();
	}

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        AuthService authService = new();
        authService.AddUser(username.Text, password.Text);
        DisplayAlert("User Created", "User has been created", "OK");
        await Shell.Current.GoToAsync("..");
    }

    private async void OnSignInButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
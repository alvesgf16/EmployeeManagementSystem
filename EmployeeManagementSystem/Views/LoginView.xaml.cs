namespace EmployeeManagementSystem;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}

    private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(DashboardView));
    }

    private async void OnSignUpButtonClicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(RegisterView));
    }
}
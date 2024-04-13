namespace EmployeeManagementSystem.Views;

public partial class CreateUserView : ContentPage
{
	public CreateUserView()
	{
		InitializeComponent();
	}

    private void CreateUserClicked(object sender, EventArgs e)
    {
        AuthService authService = new AuthService();
        authService.AddUser(username.Text, password.Text);
        DisplayAlert("User Created", "User has been created", "OK");
        AppShell.Current.GoToAsync(nameof(DashboardView));
    }
}
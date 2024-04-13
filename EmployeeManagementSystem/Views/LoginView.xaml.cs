namespace EmployeeManagementSystem;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}

    private void OnLoginButtonClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync(nameof(DashboardView));
    }

    private void OnSignUpButtonClicked(object sender, EventArgs e)
    {

    }
}
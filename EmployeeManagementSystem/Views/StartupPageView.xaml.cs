namespace EmployeeManagementSystem;

public partial class StartupPageView : ContentPage
{
	public StartupPageView()
	{
		InitializeComponent();
	}

    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(DashboardView));
    }
}
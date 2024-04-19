namespace EmployeeManagementSystem.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();

		if (App.AuthenticatedUser is not null)
		{
			userName.Text = $"Hello, {App.AuthenticatedUser.Name}!";
		}
	}
}
namespace EmployeeManagementSystem;

public partial class RegisterView : ContentPage
{
	public RegisterView()
	{
		InitializeComponent();
	}

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        
    }

    private async void OnSignInButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
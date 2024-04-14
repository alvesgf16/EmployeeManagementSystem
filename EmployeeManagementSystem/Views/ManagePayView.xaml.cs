using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem;

public partial class ManagePayView : ContentPage
{
	public ManagePayView()
	{
		InitializeComponent();
	}

    public ManagePayView(User user)
    {
        InitializeComponent();
    }
}
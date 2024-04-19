using EmployeeManagementSystem.ViewModels;

namespace EmployeeManagementSystem.Views;

public partial class LoadingView : ContentPage
{
	public LoadingView(LoadingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
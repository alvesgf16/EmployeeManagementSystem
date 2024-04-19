using EmployeeManagementSystem.Controls;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem.ViewModels;

public partial class LoadingViewModel
{
    public LoadingViewModel()
    {
        CheckLogin();
    }

    private async void CheckLogin()
    {
        bool isUserLoggedIn = int.TryParse(await SecureStorage.GetAsync("userId"), out int authenticatedUserId);

        await Task.Delay(1);

        if (isUserLoggedIn)
        {
            await Shell.Current.GoToAsync($"{nameof(DashboardView)}");
        }
        else
        {
            EmployeeService employeeManager = new();
            var authenticatedUser = employeeManager.GetEmployeeById(authenticatedUserId);
            App.AuthenticatedUser = authenticatedUser;
            Shell.Current.FlyoutHeader = new FlyoutHeaderControl();
            await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
        }
    }
}

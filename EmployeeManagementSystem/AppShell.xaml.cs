using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem;

public partial class AppShell : Shell
{
    EmployeeService employeeManager = new();

    public AppShell()
    {
        InitializeComponent();
        //register routes of pages
        Routing.RegisterRoute(nameof(DashboardView), typeof(DashboardView));
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(ScheduleView), typeof(ScheduleView));
        Routing.RegisterRoute(nameof(ManageEmployeeView), typeof(ManageEmployeeView));
        Routing.RegisterRoute(nameof(ManagePayView), typeof(ManagePayView));
    }

    protected override async void OnAppearing()
    {
        int userId = int.Parse(await SecureStorage.Default.GetAsync("user"));
        Employee user = employeeManager.GetEmployeeById(userId);

        if (user.Position != Position.Manager)
        {
            ManageEmployeeFlyoutItem.IsVisible = false;
            ManagePayFlyoutItem.IsVisible = false;
        }
    }
}

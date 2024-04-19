using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.ViewModels;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem;

public partial class AppShell : Shell
{
    readonly EmployeeService employeeManager = new();

    public AppShell()
    {
        InitializeComponent();
        BindingContext = new AppShellViewModel();

        //register routes of pages
        Routing.RegisterRoute(nameof(LoadingView), typeof(LoadingView));
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(DashboardView), typeof(DashboardView));
        Routing.RegisterRoute(nameof(ScheduleView), typeof(ScheduleView));
        Routing.RegisterRoute(nameof(ManageEmployeeView), typeof(ManageEmployeeView));
        Routing.RegisterRoute(nameof(ManagePayView), typeof(ManagePayView));
        Routing.RegisterRoute(nameof(WeekDaysSelectionView), typeof(WeekDaysSelectionView));
    }
}

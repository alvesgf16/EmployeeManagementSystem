using EmployeeManagementSystem.Models;
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
        Routing.RegisterRoute(nameof(ManagerDashboardView), typeof(ManagerDashboardView));
        Routing.RegisterRoute(nameof(ManagerScheduleView), typeof(ManagerScheduleView));
        Routing.RegisterRoute(nameof(ManagerEmployeeDetailsView), typeof(ManagerEmployeeDetailsView));
        Routing.RegisterRoute(nameof(ManagerEmployeePayView), typeof(ManagerEmployeePayView));
        Routing.RegisterRoute(nameof(ManagerWeekDaysSelectionView), typeof(ManagerWeekDaysSelectionView));
        Routing.RegisterRoute(nameof(EmployeeDetailsView), typeof(EmployeeDetailsView));
        Routing.RegisterRoute(nameof(EmployeeDashboardView), typeof(EmployeeDashboardView));
    }
}

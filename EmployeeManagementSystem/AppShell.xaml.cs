using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        //register routes of pages
        Routing.RegisterRoute(nameof(DashboardView), typeof(DashboardView));
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
        Routing.RegisterRoute(nameof(ScheduleView), typeof(ScheduleView));
        Routing.RegisterRoute(nameof(ManageEmployeeView), typeof(ManageEmployeeView));
        Routing.RegisterRoute(nameof(ManagePayView), typeof(ManagePayView));

    }
}

using EmployeeManagementSystem.Controls;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem.Models;

public class Constants
{
    public const string DatabaseFilename = "EMS.db3";

    public Constants() { }

    public static string DatabasePath
    {
        get
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directory = new DirectoryInfo(baseDirectory);

            while (directory.Name != "EmployeeManagementSystem")
            {
                directory = directory.Parent;

                if (directory == null)
                {
                    throw new InvalidOperationException("Could not find the project root directory.");
                }
            }

            string projectRoot = directory.FullName;
            string dbDirectory = Path.Combine(projectRoot, "DB");

            return Path.Combine(dbDirectory, DatabaseFilename);
        }
    }

    public static async Task AddFlyoutMenuDetails()
    {
        Shell.Current.FlyoutHeader = new FlyoutHeaderControl();

        var managerFlyout = Shell.Current.Items.Where((shellItem) => shellItem.Route == nameof(ManagerDashboardView)).FirstOrDefault();
        if (managerFlyout is not null) Shell.Current.Items.Remove(managerFlyout);

        var employeeFlyout = Shell.Current.Items.Where((shellItem) => shellItem.Route == nameof(EmployeeDashboardView)).FirstOrDefault();
        if (employeeFlyout is not null) Shell.Current.Items.Remove(employeeFlyout);

        if (App.AuthenticatedUser.Position == Position.Manager)
        {
            var flyoutItem = new FlyoutItem()
            {
                Title = "Dashboard",
                Route = nameof(ManagerDashboardView),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent
                        {
                            Title = "Dashboard",
                            ContentTemplate = new DataTemplate(typeof(ManagerDashboardView))
                        },
                        new ShellContent
                        {
                            Title = "Manage Employees",
                            ContentTemplate = new DataTemplate(typeof(ManagerEmployeeDetailsView))
                        },
                        new ShellContent
                        {
                            Title = "Manage Payroll",
                            ContentTemplate = new DataTemplate(typeof(ManagerEmployeePayView))
                        },
                        new ShellContent
                        {
                            Title = "Manage Schedule",
                            ContentTemplate = new DataTemplate(typeof(ManagerScheduleView))
                        },
                        new ShellContent
                        {
                            Title = "Manage Weekly Schedule",
                            ContentTemplate = new DataTemplate(typeof(ManagerWeekDaysSelectionView))
                        }
                    }
            };

            if (!Shell.Current.Items.Contains(flyoutItem))
            {
                Shell.Current.Items.Add(flyoutItem);
                await Shell.Current.GoToAsync($"//{nameof(ManagerDashboardView)}");
            }
        }
        if (App.AuthenticatedUser.Position == Position.Employee)
        {
            var flyoutItem = new FlyoutItem()
            {
                Title = "Dashboard",
                Route = nameof(EmployeeDashboardView),
                //Route = $"{nameof(EmployeeDashboardView)}?EmpID={App.AuthenticatedUser.Id}",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent 
                        { 
                            Title = "Dashboard", 
                            ContentTemplate = new DataTemplate(typeof(EmployeeDashboardView))
                        },
                        new ShellContent
                        {
                            Title = "Employee Details",
                            ContentTemplate = new DataTemplate(typeof(EmployeeDetailsView))
                        },
                    }
            };

            if (!Shell.Current.Items.Contains(flyoutItem))
            {
                Shell.Current.Items.Add(flyoutItem);
                await Shell.Current.GoToAsync($"//{nameof(EmployeeDashboardView)}");
                //await Shell.Current.GoToAsync($"{nameof(EmployeeDashboardView)}?EmpID={App.AuthenticatedUser.Id}");
            }
        }
    }
}

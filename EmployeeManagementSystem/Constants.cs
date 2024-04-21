using EmployeeManagementSystem.Controls;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem;

// Comments
/*
    Dominic Goncalves, April 20th, 2024.
    Constants Page.
    Declares variables for the name of the Database file and the path to the database file. This allows easy and fail-proof connection to the database from other services.
    Declares a task for creating the flyout menu and views when either an employee or a manager log in to the system. It does this by checking the 'Position' value of the employee object and creating either a 'Manager' view or an 'Employee' view
 */
public class Constants
{   // Set database filename
    public const string DatabaseFilename = "EMS.db3";

    public Constants() { }

    // Use database filename to locate path to database
    public static string DatabasePath
    {
        get
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directory = new DirectoryInfo(baseDirectory);

            while (directory.Name != "EmployeeManagementSystem")
            {
                directory = directory.Parent;

                // ensure that database is correctly found and throws error if it is not
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

    // Checks the 'position' value of the employee object that has logged in to the system, and displays either the Manager flyout (contains more information and has authority to perfrom more tasks) or the Employee flyour (contains less information and only allows basic tasks)
    public static async Task AddFlyoutMenuDetails()
    {
        Shell.Current.FlyoutHeader = new FlyoutHeaderControl();

        var managerFlyout = Shell.Current.Items.Where((shellItem) => shellItem.Route == nameof(ManagerDashboardView)).FirstOrDefault();
        if (managerFlyout is not null) Shell.Current.Items.Remove(managerFlyout);

        var employeeFlyout = Shell.Current.Items.Where((shellItem) => shellItem.Route.Contains(nameof(EmployeeDetailsView))).FirstOrDefault();
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
                Route = $"{nameof(EmployeeDashboardView)}",
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
                        new ShellContent
                        {
                            Title = "PTO/Sick Day Request",
                            ContentTemplate = new DataTemplate(typeof(TimeOffRequestView))
                        },
                    }
            };

            if (!Shell.Current.Items.Contains(flyoutItem))
            {
                Shell.Current.Items.Add(flyoutItem);
                await Shell.Current.GoToAsync($"{nameof(EmployeeDetailsView)}?EmpID={App.AuthenticatedUser.Id}");
            }
        }
    }
}

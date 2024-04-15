using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.Views;

public partial class DashboardView : ContentPage
{
    public DashboardView()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        // Navigate to ManageEmployeeView.xaml
        AppShell.Current.GoToAsync(nameof(ManageEmployeeView));
    }

    // Event handler for the "Pay" button
    private void OnPayButtonClicked(object sender, EventArgs e)
    {
        // Navigate to ManagePayView.xaml
        AppShell.Current.GoToAsync(nameof(ManagePayView));
    }

    // Event handler for the "Schedule" button
    private void OnScheduleButtonClicked(object sender, EventArgs e)
    {
        // Navigate to ScheduleView.xaml
        AppShell.Current.GoToAsync(nameof(ScheduleView));
    }

    public EmployeeService employeeManager = new();

    private void PopulateEmployeeCollection()
    {
        // Retrieve all employees from the database
        List<Employee> employees = employeeManager.GetAllEmployees();
        ObservableCollection<Employee> employeeCollection = new ObservableCollection<Employee>(employees);
        EmployeeListView.ItemsSource = employeeCollection;
    }

    private async void EmployeeListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ManageEmployeeView) + $"?employeeId={((Employee)e.SelectedItem).Id}");
    }
}
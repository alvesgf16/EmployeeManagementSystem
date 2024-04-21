using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Views;

[QueryProperty(nameof(EmpID), "EmpID")]
public partial class EmployeeDashboardView : ContentPage
{
    private string? _empId;

    private readonly EmployeeService _employeeService = new();

    private Employee _employee = new();

    public EmployeeDashboardView()
    {
        InitializeComponent();
    }

    public string EmpID
    {
        get => _empId ?? string.Empty;
        set
        {
            _empId = value;
            var employee = _employeeService.GetEmployeeById(Convert.ToInt32(value));

            if (employee is not null)
            {
                _employee = employee;
            }

            OnPropertyChanged(nameof(EmpID));
        }
    }
}
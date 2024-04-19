using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Views;

[QueryProperty(nameof(EmpID), "EmpID")]
public partial class EmployeeDashboardView : ContentPage
{
    string? empid;
    EmployeeService employeeManager = new();
    Employee employee = new();
    Payment employeePay = new();
    public EmployeeDashboardView()
    {
        InitializeComponent();
    }

    public string EmpID
    {
        get => empid ?? string.Empty;
        set
        {
            empid = value;
            var emp = employeeManager.GetEmployeeById(Convert.ToInt32(value));
            if (emp != null)
            {
                employee = emp;
            }
            OnPropertyChanged(nameof(EmpID));
        }
    }

    public void GetEmployeePayment()
    {
        employeePay = employeeManager.GetEmployeePay(employee.Id);
    }
}
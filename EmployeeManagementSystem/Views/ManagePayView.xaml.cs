using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.ComponentModel;

namespace EmployeeManagementSystem.Views;

public partial class ManagePayView : ContentPage, INotifyPropertyChanged
{
	public ManagePayView()
	{
		InitializeComponent();
        PopulateEmployeePicker();
        PayStubViewModel payStubViewModel = new PayStubViewModel();
        this.BindingContext = payStubViewModel;
    }

    EmployeeService employeeManager = new();


    private void PopulateEmployeePicker()
    {
        // Retrieve all employees from the database
        var employees = employeeManager.GetAllEmployees();

        // Clear existing items in the picker
        EmployeePicker.Items.Clear();


        // Add each employee to the picker
        foreach (var employee in employees)
        {
            EmployeePicker.Items.Add(employee.Name); // Assuming Name property is used for display
        }
    }

    private void OnText_Changed(object sender, EventArgs e) 
    { 
        SearchBar searchBar = (SearchBar)sender;

        EmployeePicker.ItemsSource = employeeManager.GetAllEmployees().Where(employee => employee.Name.ToLower().Contains(searchBar.Text.ToLower())).ToList();

        var employees = employeeManager.GetAllEmployees();

        // Clear existing items in the picker
    }

    private void EmployeePicker_SelectedIndexChanged(object sender, EventArgs e)
    {

        // Get the selected employee index
        int selectedIndex = EmployeePicker.SelectedIndex;
        if (selectedIndex != -1)
        {
            // Retrieve the corresponding employee from the database
            var selectedEmployeeName = EmployeePicker.Items[selectedIndex];
            var selectedEmployee = employeeManager.GetEmployeeByName(selectedEmployeeName);

            int selectedEmployeeId = selectedEmployee.Id;
            Payment selectedPayment = employeeManager.GetEmployeePay(selectedEmployeeId);

            // Populate the entry fields with the selected employee's information
            EmployeeID.Text = selectedEmployee.Id.ToString();
            EmployeeName.Text = selectedEmployee.Name;
            EmployeePosition.Text = selectedEmployee.Position.ToString();
            EmployeeSalary.Text = selectedPayment.Salary.ToString();
            EmployeeHours.Text = selectedPayment.TotalHours.ToString();
            EmployeeHoursThisWeek.Text = selectedPayment.HoursWorkedThisWeek.ToString();
            EmployeeOvertime.Text = selectedPayment.OvertimeHoursWorkedThisWeek.ToString();
            EmployeePerformance.Text = selectedPayment.Performance.ToString();
        }
    }

    private double CalculateTax(double pay)
    {
        double tax = 0;
        double taxRate = 0;
        switch (pay)
        {
            case double p when p < 2137:
                taxRate = 0.15;
                break;
            case double p when p < 4297:
                taxRate = 0.205;
                break;
            case double p when p < 6661:
                taxRate = 0.26;
                break;
            case double p when p < 9490:
                taxRate = 0.29;
                break;
            default:
                taxRate = 0.33;
                break;
        }

        tax = pay * taxRate;
        return tax;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private PayStubViewModel _payStub;
    public PayStubViewModel PayStub
    {
        get { return _payStub; }
        set
        {
            _payStub = value;
            OnPropertyChanged(nameof(PayStub));
        }
    }

    private void CalculatePayButton_Clicked(object sender, EventArgs e)
    {
        int employeeID = int.Parse(EmployeeID.Text);
        Employee selectedEmployee = employeeManager.GetEmployeeById(employeeID);
        Payment selectedPayment = employeeManager.GetEmployeePay(employeeID);

        double hoursWorked = selectedPayment.HoursWorkedThisWeek;
        double overtimeWorked = selectedPayment.OvertimeHoursWorkedThisWeek;
        double salary = selectedPayment.Salary;
        double totalHours = hoursWorked + (overtimeWorked * 1.5);

        double grossPay = totalHours * salary;
        double tax = CalculateTax(grossPay);
        double netPay = grossPay - tax;

        PayStub = new PayStubViewModel
        {
            EmployeeID = selectedEmployee.Id,
            EmployeeName = selectedEmployee.Name,
            EmployeePosition = selectedEmployee.Position.ToString(),
            Salary = salary,
            HoursWorkedThisWeek = hoursWorked,
            OvertimeHoursWorkedThisWeek = overtimeWorked,
            TotalHours = totalHours,
            GrossPay = grossPay,
            Tax = tax,
            NetPay = netPay
        };

        PayStubLayout.IsVisible = true;


    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class PayStubViewModel
{
    public int EmployeeID { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeePosition { get; set; }
    public double Salary { get; set; }
    public double HoursWorkedThisWeek { get; set; }
    public double OvertimeHoursWorkedThisWeek { get; set; }
    public double TotalHours { get; set; }
    public double GrossPay { get; set; }
    public double Tax { get; set; }
    public double NetPay { get; set; }
}
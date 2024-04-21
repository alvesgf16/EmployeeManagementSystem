using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.ComponentModel;

namespace EmployeeManagementSystem.Views;

public partial class ManagerEmployeePayView : ContentPage, INotifyPropertyChanged
{
    private readonly EmployeeService _employeeService = new();

    private readonly PaymentService _paymentService = new();

	public ManagerEmployeePayView()
	{
		InitializeComponent();
        PopulateEmployeePicker();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void PopulateEmployeePicker()
    {
        // Retrieve all employees from the database
        var employees = _employeeService.GetAllEmployees();

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

        EmployeePicker.ItemsSource = _employeeService.GetAllEmployees().Where(employee => employee.Name.ToLower().Contains(searchBar.Text.ToLower())).ToList();

        var employees = _employeeService.GetAllEmployees();

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
            var selectedEmployee = _employeeService.GetEmployeeByName(selectedEmployeeName);

            int selectedEmployeeId = selectedEmployee.Id;
            Payment selectedPayment = _paymentService.GetEmployeePay(selectedEmployeeId);

            // Populate the entry fields with the selected employee's information
            EmployeeID.Text = selectedEmployee.Id.ToString();
            EmployeeName.Text = selectedEmployee.Name;
            EmployeePosition.Text = selectedEmployee.Position.ToString();
            EmployeeSalary.Text = selectedPayment.Salary.ToString();
            EmployeePerformance.Text = selectedPayment.Performance.ToString();
        }
    }

    private void UpdateEPayInfo_Clicked(object sender, EventArgs e)
    {
        int employeeID = int.Parse(EmployeeID.Text);
        Payment selectedPayment = _paymentService.GetEmployeePay(employeeID);

        selectedPayment.Salary = double.Parse(EmployeeSalary.Text);
        selectedPayment.Performance = int.Parse(EmployeePerformance.Text);

        _paymentService.UpdatePayment(selectedPayment);

        EmployeeSalary.Text = selectedPayment.Salary.ToString();
        EmployeePerformance.Text = selectedPayment.Performance.ToString();
        ClearForm();
        DisplayAlert("Confirmation", "Payment information has been saved", "OK");
        PopulateEmployeePicker();
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ClearForm()
    {
        EmployeeID.Text = string.Empty;
        EmployeeName.Text = string.Empty;
        EmployeePosition.Text = string.Empty;
        EmployeeSalary.Text = string.Empty;
        EmployeePerformance.Text = string.Empty;
    }
}
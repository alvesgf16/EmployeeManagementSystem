using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.ComponentModel;
using EmployeeManagementSystem.Exceptions;

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
        try
        {

            // Get the selected employee index
            int selectedIndex = EmployeePicker.SelectedIndex;

            if (selectedIndex != -1)
            {
                int employeeID = int.Parse(EmployeeID.Text);
                Employee selectedEmployee = _employeeService.GetEmployeeById(employeeID);
                Payment selectedPayment = _paymentService.GetEmployeePay(employeeID);

                if (string.IsNullOrEmpty(EmployeeSalary.Text) || string.IsNullOrEmpty(EmployeePerformance.Text))
                {
                    throw new InvalidParameterException("Please fill in all fields");
                }

                if (double.Parse(EmployeeSalary.Text) < 15)
                {
                    throw new InvalidParameterException("Salary cannot below minimum wage");
                }

                if (int.Parse(EmployeePerformance.Text) < 1 || int.Parse(EmployeePerformance.Text) > 10)
                {
                    throw new InvalidParameterException("Performance must be between 0 and 10");
                }

                selectedPayment.Salary = double.Parse(EmployeeSalary.Text);
                selectedPayment.Performance = int.Parse(EmployeePerformance.Text);

        _paymentService.UpdatePayment(selectedPayment);

                EmployeeSalary.Text = selectedPayment.Salary.ToString();
                EmployeePerformance.Text = selectedPayment.Performance.ToString();
                ClearForm();
                DisplayAlert("Confirmation", "Payment information has been saved", "OK");
                PopulateEmployeePicker();

            }
            else
            {
                // Display an error message
                DisplayAlert("Error", "Please select an employee", "OK");
            }
        }
        catch (Exception ex)
        {
            // Display an error message
            DisplayAlert("Error", ex.Message, "OK");
        }
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
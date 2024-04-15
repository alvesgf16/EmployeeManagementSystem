using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Xml.Linq;

namespace EmployeeManagementSystem;

public partial class ManagePayView : ContentPage
{
	public ManagePayView()
	{
		InitializeComponent();
        PopulateEmployeePicker();

    }

    public ManagePayView(User user)
    {
        InitializeComponent();
        PopulateEmployeePicker();
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
            EmployeeHours.Text = selectedPayment.HoursWorked.ToString();
            EmployeePerformance.Text = selectedPayment.Performance.ToString();
        }
    }

    private void EmployeePicker_SelectedIndexChanged_1(System.Object sender, System.EventArgs e)
    {

    }
}
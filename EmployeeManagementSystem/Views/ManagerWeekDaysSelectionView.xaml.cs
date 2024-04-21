using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Views;

public partial class ManagerWeekDaysSelectionView : ContentPage
{
    private readonly EmployeeService _employeeService = new();

    private readonly PaymentService _paymentService = new();

	public ManagerWeekDaysSelectionView()
	{
		InitializeComponent();
        PopulateEmployeePicker();
	}

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

    private void EmployeePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = EmployeePicker.SelectedIndex;
        if (selectedIndex != -1)
        {
            string selectedEmployeeName = EmployeePicker.Items[selectedIndex];
            Employee selectedEmployee = _employeeService.GetEmployeeByName(selectedEmployeeName);
            Payment selectedPayment = _paymentService.GetEmployeePay(selectedEmployee.Id);

            // Do something with the selected employee

            Name.Text = selectedEmployee.Name;
            Salary.Text = selectedPayment.Salary.ToString();
        }
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DaySelectionChanged(object sender, CheckedChangedEventArgs e)
    {
        int selectedIndex = EmployeePicker.SelectedIndex;
        if (selectedIndex != -1)
        {
            string selectedEmployeeName = EmployeePicker.Items[selectedIndex];
            Employee selectedEmployee = _employeeService.GetEmployeeByName(selectedEmployeeName);
            Payment selectedPayment = _paymentService.GetEmployeePay(selectedEmployee.Id);

            int TotalHours = 0;

            int OvertimeHours = 0;

            if (Monday.IsChecked)
            {
                TotalHours += 8;
            }

            if (Tuesday.IsChecked)
            {
                TotalHours += 8;
            }

            if (Wednesday.IsChecked)
            {
                TotalHours += 8;
            }

            if (Thursday.IsChecked)
            {
                TotalHours += 8;
            }

            if (Friday.IsChecked)
            {
                TotalHours += 8;
            }

            if (Saturday.IsChecked)
            {
                TotalHours += 8;
            }

            if (Sunday.IsChecked)
            {
                TotalHours += 8;
            }

            if (TotalHours > 40)
            {
                OvertimeHours = TotalHours - 40;
                TotalHours = 40;
            }

            double ExpectedPay = TotalHours * selectedPayment.Salary + OvertimeHours * selectedPayment.Salary * 1.5;
            TotalHoursEntry.Text = TotalHours.ToString();

            OvertimeHoursEntry.Text = OvertimeHours.ToString();

            ExpectedPayEntry.Text = ExpectedPay.ToString();

        }

    }

    private void SavePaymentButton_Clicked(object sender, EventArgs e)
    {
        int selectedIndex = EmployeePicker.SelectedIndex;
        if (selectedIndex != -1)
        {
            string selectedEmployeeName = EmployeePicker.Items[selectedIndex];
            Employee selectedEmployee = _employeeService.GetEmployeeByName(selectedEmployeeName);
            Payment selectedPayment = _paymentService.GetEmployeePay(selectedEmployee.Id);

            Payment payment = new Payment
            {
                EmployeeID = selectedEmployee.Id,
                Salary = selectedPayment.Salary,
                TotalHours = int.Parse(TotalHoursEntry.Text),
                RegHours = int.Parse(TotalHoursEntry.Text),
                OverTimeHours = int.Parse(OvertimeHoursEntry.Text),
                Performance = 0
            };

            _paymentService.UpdatePayment(payment);
        }
        DisplayAlert("Confirmation", "Weekly payment information has been saved", "OK");
        PopulateEmployeePicker();
    }

    private void ResetWeeklyPayroll_Clicked(object sender, EventArgs e)
    {
        _paymentService.SetAllPaymentsToZero();
    }
}
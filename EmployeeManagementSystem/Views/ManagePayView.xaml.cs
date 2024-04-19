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

    private void UpdateEPayInfo_Clicked(object sender, EventArgs e)
    {
        int employeeID = int.Parse(EmployeeID.Text);
        Employee selectedEmployee = employeeManager.GetEmployeeById(employeeID);
        Payment selectedPayment = employeeManager.GetEmployeePay(employeeID);

        selectedPayment.Salary = double.Parse(EmployeeSalary.Text);
        selectedPayment.TotalHours = double.Parse(EmployeeHours.Text);
        selectedPayment.HoursWorkedThisWeek = double.Parse(EmployeeHoursThisWeek.Text);
        selectedPayment.OvertimeHoursWorkedThisWeek = double.Parse(EmployeeOvertime.Text);
        selectedPayment.Performance = int.Parse(EmployeePerformance.Text);

        employeeManager.UpdateEmployeePay(selectedPayment);

        EmployeeSalary.Text = selectedPayment.Salary.ToString();
        EmployeeHours.Text = selectedPayment.TotalHours.ToString();
        EmployeeHoursThisWeek.Text = selectedPayment.HoursWorkedThisWeek.ToString();
        EmployeeOvertime.Text = selectedPayment.OvertimeHoursWorkedThisWeek.ToString();
        EmployeePerformance.Text = selectedPayment.Performance.ToString();
    }

    private void UpdatePayStub(PayStubViewModel payStub)
    {
        PayStubLayout.Children.Clear();

        Label employeeIDLabel = new Label { Text = "Employee ID: " + payStub.EmployeeID };
        Label employeeNameLabel = new Label { Text = "Employee Name: " + payStub.EmployeeName };
        Label employeePositionLabel = new Label { Text = "Employee Position: " + payStub.EmployeePosition };
        Label salaryLabel = new Label { Text = "Salary: " + payStub.Salary };
        Label hoursWorkedThisWeekLabel = new Label { Text = "Hours Worked This Week: " + payStub.HoursWorkedThisWeek };
        Label overtimeHoursWorkedThisWeekLabel = new Label { Text = "Overtime Hours Worked This Week: " + payStub.OvertimeHoursWorkedThisWeek };
        Label totalHoursLabel = new Label { Text = "Total Hours: " + payStub.TotalHours };
        Label grossPayLabel = new Label { Text = "Gross Pay: " + payStub.GrossPay };
        Label taxLabel = new Label { Text = "Tax: " + payStub.Tax };
        Label netPayLabel = new Label { Text = "Net Pay: " + payStub.NetPay };

        PayStubLayout.Children.Add(employeeIDLabel);
        PayStubLayout.Children.Add(employeeNameLabel);
        PayStubLayout.Children.Add(employeePositionLabel);
        PayStubLayout.Children.Add(salaryLabel);
        PayStubLayout.Children.Add(hoursWorkedThisWeekLabel);
        PayStubLayout.Children.Add(overtimeHoursWorkedThisWeekLabel);
        PayStubLayout.Children.Add(totalHoursLabel);
        PayStubLayout.Children.Add(grossPayLabel);
        PayStubLayout.Children.Add(taxLabel);
        PayStubLayout.Children.Add(netPayLabel);
    }

    private void CalculatePayButton_Clicked(object sender, EventArgs e)
    { 
        double salary = double.Parse(EmployeeSalary.Text);
        double hours = double.Parse(EmployeeHours.Text);
        double overtime = double.Parse(EmployeeOvertime.Text);
        double performance = double.Parse(EmployeePerformance.Text);

        double grossPay = salary * hours + (1.5 * salary * overtime);
        double tax = CalculateTax(grossPay);
        double netPay = grossPay - tax;

        PayStub = new PayStubViewModel
        {
            EmployeeID = int.Parse(EmployeeID.Text),
            EmployeeName = EmployeeName.Text,
            EmployeePosition = EmployeePosition.Text,
            Salary = salary,
            HoursWorkedThisWeek = double.Parse(EmployeeHoursThisWeek.Text),
            OvertimeHoursWorkedThisWeek = overtime,
            TotalHours = hours,
            GrossPay = grossPay,
            Tax = tax,
            NetPay = netPay
        };        

        UpdatePayStub(PayStub);

        PayStubLayout.IsVisible = true;

    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ClearForm()
    {
        EmployeeID.Text = string.Empty;
        EmployeeName.Text = string.Empty;
        EmployeePosition.Text = string.Empty;
        EmployeeSalary.Text = string.Empty;
        EmployeeHours.Text = string.Empty ;
        EmployeeHoursThisWeek.Text = string.Empty;
        EmployeeOvertime.Text = string.Empty;
        EmployeePerformance.Text = string.Empty;
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
using EmployeeManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Views;

public partial class ManagerScheduleView : ContentPage
{
    public ObservableCollection<CalendarDay> CalendarDays { get; set; }
    
    public ICommand DayTappedCommand { get; private set; }

    private DateTime currentMonth;

    public ManagerScheduleView()
    {
        InitializeComponent();
        DayTappedCommand = new Command<CalendarDay>(OnDayTapped);
        currentMonth = DateTime.Today;
        UpdateMonthYearLabel();
        InitializeCalendar();
        PopulateEmployeePicker();
    }

    EmployeeService employeeManager = new();

    private void InitializeCalendar()
    {
        // Initialize the calendar with days, months, and years
        CalendarDays = new ObservableCollection<CalendarDay>();
        // Populate the CalendarDays collection with appropriate data based on the desired date range

        // For example, populate with dummy data for demonstration
        for (int i = 1; i <= DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month); i++)
        {
            CalendarDays.Add(new CalendarDay
            {
                Day = i,
                BackgroundColor = Color.FromHex("#00FFFFFF") // Set default background color
            });
        }

        BindingContext = this;
    }

    private void OnDayTapped(CalendarDay dayViewModel)
    {
        // Handle day selection and highlighting here
        foreach (var day in CalendarDays)
        {
            if (day == dayViewModel)
            {
                day.BackgroundColor = Color.FromHex("#ADD8E6"); // Highlight selected day
                int selectedDay = day.Day;

                // Set the selected date to the Entry named "Date"
                Date.Text = new DateTime(currentMonth.Year, currentMonth.Month, selectedDay).ToString("MM/dd/yyyy");
            }
            else
            {
                day.BackgroundColor = Color.FromHex("#00FFFFFF"); // Reset other days
            }
        }
    }

    private void UpdateMonthYearLabel()
    {
        MonthYearLabel.Text = currentMonth.ToString("MMMM yyyy");
    }

    private void PreviousMonth_Clicked(object sender, EventArgs e)
    {
        currentMonth = currentMonth.AddMonths(-1);
        UpdateMonthYearLabel();
        // You may want to reinitialize the calendar or update it here based on the new month
    }

    private void NextMonth_Clicked(object sender, EventArgs e)
    {
        currentMonth = currentMonth.AddMonths(1);
        UpdateMonthYearLabel();
        // You may want to reinitialize the calendar or update it here based on the new month
    }

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
        }
    }

    private void LogHoursButton_Clicked(object sender, EventArgs e)
    {
        int employeeID = int.Parse(EmployeeID.Text);
        Payment payment = employeeManager.GetEmployeePay(employeeID);
        double hours = double.Parse(HoursToday.Text);

        if (EmployeeID.Text != "Hours")
        {
            if (hours > 8.5)
            {
                double overtime = hours - 8.5;
                payment.EmployeeID = employeeID;
                payment.RegHours = payment.RegHours + 8.5;
                payment.OverTimeHours = payment.OverTimeHours + overtime;
                employeeManager.UpdatePayment(payment);
            }
            else
            {
                payment.RegHours = payment.RegHours + hours;
                employeeManager.UpdatePayment(payment);
            }

            ClearForm();
        }
    }

    private void ClearForm()
    {
        EmployeeID.Text = string.Empty;
        EmployeeName.Text = string.Empty;
        Date.Text = string.Empty;
        HoursToday.Text = string.Empty;
    }
}
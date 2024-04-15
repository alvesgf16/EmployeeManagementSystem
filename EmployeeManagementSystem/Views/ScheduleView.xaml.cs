using EmployeeManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem;

public partial class ScheduleView : ContentPage
{
    public class CalendarDayViewModel
    {
        public int Day { get; set; }
        public Color BackgroundColor { get; set; }
    }

    public ObservableCollection<CalendarDayViewModel> CalendarDays { get; set; }
    public ICommand DayTappedCommand { get; private set; }

    private DateTime currentMonth;

    public ScheduleView()
    {
        InitializeComponent();
        DayTappedCommand = new Command<CalendarDayViewModel>(OnDayTapped);
        currentMonth = DateTime.Today;
        UpdateMonthYearLabel();
        InitializeCalendar();
        PopulateEmployeePicker();
    }

    public ScheduleView(User user)
    {
        InitializeComponent();
        DayTappedCommand = new Command<CalendarDayViewModel>(OnDayTapped);
        currentMonth = DateTime.Today;
        UpdateMonthYearLabel();
        InitializeCalendar();
        PopulateEmployeePicker();
    }

    EmployeeService employeeManager = new();

    private void InitializeCalendar()
    {
        // Initialize the calendar with days, months, and years
        CalendarDays = new ObservableCollection<CalendarDayViewModel>();
        // Populate the CalendarDays collection with appropriate data based on the desired date range

        // For example, populate with dummy data for demonstration
        for (int i = 1; i <= DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month); i++)
        {
            CalendarDays.Add(new CalendarDayViewModel
            {
                Day = i,
                BackgroundColor = Color.FromHex("#00FFFFFF") // Set default background color
            });
        }

        BindingContext = this;
    }

    private void OnDayTapped(CalendarDayViewModel dayViewModel)
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
        if (EmployeeID.Text != "Hours")
        {
            int employeeID = int.Parse(EmployeeID.Text);
            Payment payment = employeeManager.GetEmployeePay(employeeID);
            double hours = double.Parse(HoursToday.Text);

            if (hours > 8.5)
            {
                double overtime = hours - 8.5;
                payment.EmployeeID = employeeID;
                payment.HoursWorkedThisWeek = payment.HoursWorkedThisWeek + 8.5;
                payment.OvertimeHoursWorkedThisWeek = payment.OvertimeHoursWorkedThisWeek + overtime;
                employeeManager.UpdatePayment(payment);
            }
        }
    }
}
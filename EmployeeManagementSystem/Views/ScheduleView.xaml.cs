using EmployeeManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmployeeManagementSystem.Views;

public partial class ScheduleView : ContentPage
{
    public ObservableCollection<CalendarDayModel> CalendarDays { get; set; }
    public ICommand DayTappedCommand { get; private set; }

    private DateTime currentMonth;

    public ScheduleView()
    {
        InitializeComponent();
        DayTappedCommand = new Command<CalendarDayModel>(OnDayTapped);
        currentMonth = DateTime.Today;
        UpdateMonthYearLabel();
        InitializeCalendar();
    }

    public ScheduleView(User user)
    {
        InitializeComponent();
        DayTappedCommand = new Command<CalendarDayModel>(OnDayTapped);
        currentMonth = DateTime.Today;
        UpdateMonthYearLabel();
        InitializeCalendar();
    }

    private void InitializeCalendar()
    {
        // Initialize the calendar with days, months, and years
        CalendarDays = new ObservableCollection<CalendarDayModel>();
        // Populate the CalendarDays collection with appropriate data based on the desired date range

        // For example, populate with dummy data for demonstration
        for (int i = 1; i <= DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month); i++)
        {
            CalendarDays.Add(new CalendarDayModel
            {
                Day = i,
                BackgroundColor = Color.FromHex("#00FFFFFF") // Set default background color
            });
        }

        BindingContext = this;
    }

    private void OnDayTapped(CalendarDayModel dayViewModel)
    {
        // Handle day selection and highlighting here
        foreach (var day in CalendarDays)
        {
            if (day == dayViewModel)
            {
                day.BackgroundColor = Color.FromHex("#ADD8E6"); // Highlight selected day
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
}
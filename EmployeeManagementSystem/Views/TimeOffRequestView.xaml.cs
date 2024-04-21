using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.Views;

public partial class TimeOffRequestView : ContentPage
{
    DateTime datetime = DateTime.Now;
    DateTime? date;
    DateTime? sickdate;
    EmployeeService _employeeService = new();
    PTORequestService _ptoRequestService = new();
    SickDayRequestService SickDayRequestService = new();
    Employee employee = new();
    public ObservableCollection<string> PTORequests { get; set; } = [];
    public ObservableCollection<string> SickDayRequests { get; set; } = [];

    DateTime? SickDate
    {
        get => sickdate;
        set
        {
            sickdate = value;
            OnPropertyChanged();
        }
    }
    DateTime? Date
    {
        get => date;
        set
        {
            date = value;
            OnPropertyChanged();
        }
    }

    public TimeOffRequestView()
	{
		InitializeComponent();
        employee = App.AuthenticatedUser;
        this.BindingContext = employee;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Date = null;
        SickDate = null;
        PTORequestEntry.MinimumDate = datetime;
        PTORequestEntry.Date = datetime;
        SickRequestEntry.MinimumDate = datetime;
        PTORequests.Clear();
        PopulatePTORequests();
        SickDayRequests.Clear();
        PopulateSickDayRequests();
    }

    private void RequestPTO_Clicked(object sender, EventArgs e)
    {
        // Update the existing employee with the new information
        if (Date != null)
        {

            if (employee.AvailablePTODays >= 1)
            {
                if (_ptoRequestService.GetPTORequestByDateAndId((DateTime)Date, employee.Id) is null)
                {
                    employee.AvailablePTODays -= 1;
                    _ptoRequestService.SavePTORequest(new PTORequest { EmployeeID = employee.Id, RequestedDate = (DateTime)Date, Approved = false });
                    _employeeService.UpdateEmployee(employee);
                    DisplayAlert("Success", "Your PTO request has been submitted successfully.", "OK");
                    Date = null;
                }
                else
                {
                    DisplayAlert("Error", "You have already requested PTO for this date.", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Insufficient PTO days available.", "OK");
            }

        }
        else
        {
            DisplayAlert("Error", "Please select a date.", "OK");
        }
    }

   private void RequestSick_Clicked(object sender, EventArgs e)
    {
        if (SickDate != null)
        {
            if (employee.AvailableSickDays >= 1)
            {
                if (SickDayRequestService.GetSickDayRequestsByEmployeeIdAndDate(employee.Id, (DateTime)SickDate) is null)
                {
                    employee.AvailableSickDays -= 1;
                    SickDayRequestService.SaveSickDayRequest(new SickDayRequest { EmployeeID = employee.Id, RequestedDate = (DateTime)SickDate, Approved = false });
                    _employeeService.UpdateEmployee(employee);
                    DisplayAlert("Success", "Your Sick day request has been submitted successfully.", "OK");
                }
                else
                {
                    DisplayAlert("Error", "You have already requested a Sick day for this date.", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Insufficient Sick days available.", "OK");
            }
        }
        else
        {
            DisplayAlert("Error", "Please select a date.", "OK");
        }
    }

    private void Selected_Date(object sender, DateChangedEventArgs e)
    {
        Date = e.NewDate;
    }

    private void SickRequestEntry_DateSelected(object sender, DateChangedEventArgs e)
    {
        SickDate = e.NewDate;
    }

    public void PopulatePTORequests()
    {
        _ptoRequestService.GetPTORequestByEmployeeId(employee.Id).ForEach(r => PTORequests.Add(r.ToString()));
        PTOListView.ItemsSource = PTORequests;
    }
    public void PopulateSickDayRequests()
    {
        SickDayRequestService.GetSickDayRequestsByEmployeeId(employee.Id).ForEach(r => SickDayRequests.Add(r.ToString()));
        SickDayListView.ItemsSource = SickDayRequests;
    }
}
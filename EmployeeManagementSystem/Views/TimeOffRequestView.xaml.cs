using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace EmployeeManagementSystem.Views;

public partial class TimeOffRequestView : ContentPage
{
    string? empid;
    DateTime datetime = DateTime.Now;
    DateTime? date;
    DateTime? sickdate;
    EmployeeService employeeManager = new();
    ScheduleService ScheduleService = new();
    Employee employee = new();

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
        empid ??= App.AuthenticatedUser.Id.ToString();
        employee = App.AuthenticatedUser;
        this.BindingContext = employee;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Date = null;
        PTORequestEntry.MinimumDate = datetime;
        PTORequestEntry.Date = datetime;
        SickRequestEntry.MinimumDate = datetime;
    }

    private void RequestPTO_Clicked(object sender, EventArgs e)
    {
        // Update the existing employee with the new information
        if (Date != null)
        {

            if (employee.AvailablePTODays >= 1)
            {
                if (ScheduleService.GetPTORequestByDateAndId((DateTime)Date, employee.Id) is null)
                {
                    employee.AvailablePTODays -= 1;
                    ScheduleService.SavePTORequest(new PTORequest { EmployeeID = employee.Id, RequestedDate = (DateTime)Date, Approved = false });
                    employeeManager.UpdateEmployee(employee);
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
        // Update the existing employee with the new information
                if (1 <= employee.AvailableSickDays)
                {
                    employee.AvailableSickDays -= 1;
                    employeeManager.UpdateEmployee(employee);
                    DisplayAlert("Success", "Your Sick day request has been submitted successfully.", "OK");
                }
                else
                {
                    DisplayAlert("Error", "Insufficient Sick days available.", "OK");
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
}
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace EmployeeManagementSystem.Views;

[QueryProperty(nameof(EmpID), "EmpID")]
public partial class TimeOffRequestView : ContentPage
{
    string? empid;
    DateTime datetime = DateTime.Now;
    DateTime? date;
    EmployeeService employeeManager = new();
    ScheduleService ScheduleService = new();

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
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        empid ??= App.AuthenticatedUser.Id.ToString();
        PopulateView(GetEmployeeFromDatabase(Convert.ToInt32(empid)));
        Date = null;
        PTORequestEntry.MinimumDate = datetime;
        PTORequestEntry.Date = datetime;
    }

    private void RequestPTO_Clicked(object sender, EventArgs e)
    {
        int employeeId = int.Parse(EmployeeID.Text); // Get the employee Id from somewhere (e.g., a selected item)
        Employee existingEmployee = GetEmployeeFromDatabase(employeeId);

        // Update the existing employee with the new information
        if (ValidateEmployeeInformation() && Date != null)
        {

            if (existingEmployee.AvailablePTODays >= 1)
            {
                if (ScheduleService.GetPTORequestByDateAndId((DateTime)Date, employeeId) is null)
                {
                    existingEmployee.AvailablePTODays -= 1;
                    ScheduleService.SavePTORequest(new PTORequest { EmployeeID = employeeId, RequestedDate = (DateTime)Date, Approved = false });
                    employeeManager.UpdateEmployee(existingEmployee);
                    DisplayAlert("Success", "Your PTO request has been submitted successfully.", "OK");
                    PopulateView(existingEmployee);
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
        int employeeId = int.Parse(EmployeeID.Text); // Get the employee Id from somewhere (e.g., a selected item)
        Employee existingEmployee = GetEmployeeFromDatabase(employeeId);

        // Update the existing employee with the new information
        if (ValidateEmployeeInformation())
        {

                if (1 <= existingEmployee.AvailableSickDays)
                {
                    existingEmployee.AvailableSickDays -= 1;
                    employeeManager.UpdateEmployee(existingEmployee);
                    DisplayAlert("Success", "Your Sick day request has been submitted successfully.", "OK");
                    PopulateView(existingEmployee);
                }
                else
                {
                    DisplayAlert("Error", "Insufficient Sick days available.", "OK");
                }


            
        }

        else
        {
            DisplayAlert("Error", "Invalid Sick days requested.", "OK");
        }

    }

    private bool ValidateEmployeeInformation()
    {
        // Perform validation here (e.g., check if all required fields are filled)
        // You can add more specific validation rules as needed
        return !string.IsNullOrWhiteSpace(EmailEntry.Text) &&
               !string.IsNullOrWhiteSpace(PasswordEntry.Text) &&
               !string.IsNullOrWhiteSpace(NameEntry.Text) &&
               !string.IsNullOrWhiteSpace(SickDaysAvailableEntry.Text) &&
               !string.IsNullOrWhiteSpace(PTODaysAvailableEntry.Text);
    }

    private Employee GetEmployeeFromDatabase(int employeeId)
    {
        // Retrieve the employee from the database based on Id
        // You need to implement this method based on your database access logic
        return employeeManager.GetEmployeeById(employeeId);
    }

    private void PopulateView(Employee employee)
    {
        // Populate the entry fields with the selected employee's information
        EmployeeID.Text = employee.Id.ToString();
        EmailEntry.Text = employee.Email;
        PasswordEntry.Text = employee.Password;
        NameEntry.Text = employee.Name;
        PTODaysAvailableEntry.Text = employee.AvailablePTODays.ToString();
        SickDaysAvailableEntry.Text = employee.AvailableSickDays.ToString();
    }


    public string EmpID
    {
        get => empid ?? string.Empty;
        set
        {
            empid = value;
            var employee = GetEmployeeFromDatabase(Convert.ToInt32(value));
            if (employee != null)
            {
                // Populate the entry fields with the selected employee's information
                EmployeeID.Text = employee.Id.ToString();
                EmailEntry.Text = employee.Email;
                PasswordEntry.Text = employee.Password;
                NameEntry.Text = employee.Name;
                PTODaysAvailableEntry.Text = employee.AvailablePTODays.ToString();
                SickDaysAvailableEntry.Text = employee.AvailableSickDays.ToString();                
            }
            
        }
    }

    private void Selected_Date(object sender, DateChangedEventArgs e)
    {
        Date = e.NewDate;
    }
}
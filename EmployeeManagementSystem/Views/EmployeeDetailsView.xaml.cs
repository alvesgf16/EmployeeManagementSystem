using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Views;

[QueryProperty(nameof(EmpID), "EmpID")]
public partial class EmployeeDetailsView : ContentPage
{
    private string? _empId;

    private readonly EmployeeService _employeeService = new();
    
    public EmployeeDetailsView()
    {
        InitializeComponent();
        _empId ??= App.AuthenticatedUser.Id.ToString();
        PopulateView(GetEmployeeFromDatabase(Convert.ToInt32(_empId)));
    }

    private void UpdateExistingEmployee_Clicked(object sender, EventArgs e)
    {
        if (ValidateEmployeeInformation())
        {
            // Retrieve the existing employee from the database based on Id
            int employeeId = int.Parse(EmployeeID.Text); // Get the employee Id from somewhere (e.g., a selected item)
            Employee existingEmployee = GetEmployeeFromDatabase(employeeId);

            // Update the existing employee with the new information
            existingEmployee.Email = EmailEntry.Text.ToLower();
            existingEmployee.Password = PasswordEntry.Text;
            existingEmployee.Name = NameEntry.Text;
            existingEmployee.PhoneNumber = PhoneNumberEntry.Text;
            existingEmployee.Address = AddressEntry.Text;
            existingEmployee.EContactName = ContactNameEntry.Text;
            existingEmployee.EContactPhone = ContactPhoneNumberEntry.Text;
            existingEmployee.Position = (Position)Enum.Parse(typeof(Position), PositionPicker.SelectedItem.ToString());
            existingEmployee.AvailablePTODays = 0;
            existingEmployee.AvailableSickDays = 10;
            existingEmployee.Shift = (WorkShift)Enum.Parse(typeof(WorkShift), SchedulePicker.SelectedItem.ToString());

            // Save the updated employee to the database
            _employeeService.UpdateEmployee(existingEmployee);
        }
        else
        {
            DisplayAlert("Error", "Please fill out all fields correctly.", "OK");
        }
    }

    private bool ValidateEmployeeInformation()
    {
        // Perform validation here (e.g., check if all required fields are filled)
        // You can add more specific validation rules as needed
        return !string.IsNullOrWhiteSpace(EmailEntry.Text) &&
               !string.IsNullOrWhiteSpace(PasswordEntry.Text) &&
               !string.IsNullOrWhiteSpace(NameEntry.Text) &&
               !string.IsNullOrWhiteSpace(PhoneNumberEntry.Text) &&
               !string.IsNullOrWhiteSpace(AddressEntry.Text) &&
               !string.IsNullOrWhiteSpace(ContactNameEntry.Text) &&
               !string.IsNullOrWhiteSpace(ContactPhoneNumberEntry.Text) &&
               PositionPicker.SelectedItem != null &&
               SchedulePicker.SelectedItem != null;
    }

    private Employee GetEmployeeFromDatabase(int employeeId)
    {
        // Retrieve the employee from the database based on Id
        // You need to implement this method based on your database access logic
        return _employeeService.GetEmployeeById(employeeId);
    }

    private void PopulateView(Employee employee)
    {
        // Populate the entry fields with the selected employee's information
        EmployeeID.Text = employee.Id.ToString();
        EmailEntry.Text = employee.Email;
        PasswordEntry.Text = employee.Password;
        NameEntry.Text = employee.Name;
        PhoneNumberEntry.Text = employee.PhoneNumber;
        AddressEntry.Text = employee.Address;
        ContactNameEntry.Text = employee.EContactName;
        ContactPhoneNumberEntry.Text = employee.EContactPhone;
        PositionPicker.SelectedItem = employee.Position.ToString();
        SchedulePicker.SelectedItem = employee.Shift.ToString();
    }

    public string EmpID
    {
        get => _empId ?? string.Empty;
        set
        {
            _empId = value;
            var employee = GetEmployeeFromDatabase(Convert.ToInt32(value));
            if (employee != null)
            {
                // Populate the entry fields with the selected employee's information
                EmployeeID.Text = employee.Id.ToString();
                EmailEntry.Text = employee.Email;
                PasswordEntry.Text = employee.Password;
                NameEntry.Text = employee.Name;
                PhoneNumberEntry.Text = employee.PhoneNumber;
                AddressEntry.Text = employee.Address;
                ContactNameEntry.Text = employee.EContactName;
                ContactPhoneNumberEntry.Text = employee.EContactPhone;
                PositionPicker.SelectedItem = employee.Position.ToString();
                SchedulePicker.SelectedItem = employee.Shift.ToString();
            }
            OnPropertyChanged(nameof(EmpID));
        }
    }

    private void PopulateView(Employee employee)
    {
        // Populate the entry fields with the selected employee's information
        EmployeeID.Text = employee.Id.ToString();
        EmailEntry.Text = employee.Email;
        PasswordEntry.Text = employee.Password;
        NameEntry.Text = employee.Name;
        PhoneNumberEntry.Text = employee.PhoneNumber;
        AddressEntry.Text = employee.Address;
        ContactNameEntry.Text = employee.EContactName;
        ContactPhoneNumberEntry.Text = employee.EContactPhone;
        PositionPicker.SelectedItem = employee.Position.ToString();
        SchedulePicker.SelectedItem = employee.Shift.ToString();
    }
}
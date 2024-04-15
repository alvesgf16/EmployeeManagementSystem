using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Views;

public partial class ManageEmployeeView : ContentPage
{
    EmployeeService employeeManager = new();
    
    public ManageEmployeeView()
    {
        InitializeComponent();
        PopulateEmployeePicker();
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

    private void EmployeePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get the selected employee index
        int selectedIndex = EmployeePicker.SelectedIndex;
        if (selectedIndex != -1)
        {
            // Retrieve the corresponding employee from the database
            var selectedEmployeeName = EmployeePicker.Items[selectedIndex];
            var selectedEmployee = employeeManager.GetEmployeeByName(selectedEmployeeName);

            // Populate the entry fields with the selected employee's information
            EmployeeID.Text = selectedEmployee.Id.ToString();
            EmailEntry.Text = selectedEmployee.Email;
            PasswordEntry.Text = selectedEmployee.Password;
            NameEntry.Text = selectedEmployee.Name;
            PhoneNumberEntry.Text = selectedEmployee.PhoneNumber;
            AddressEntry.Text = selectedEmployee.Address;
            ContactNameEntry.Text = selectedEmployee.EContactName;
            ContactPhoneNumberEntry.Text = selectedEmployee.EContactPhone;
            PositionPicker.SelectedItem = selectedEmployee.Position.ToString();
            SchedulePicker.SelectedItem = selectedEmployee.Shift.ToString();
        }
    }

    private void AddNewEmployee_Clicked(object sender, EventArgs e)
    {
        if (ValidateEmployeeInformation())
        {
            // Create a new Employee object
            Employee newEmployee = new Employee
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text,
                Name = NameEntry.Text,
                PhoneNumber = PhoneNumberEntry.Text,
                Address = AddressEntry.Text,
                EContactName = ContactNameEntry.Text,
                EContactPhone = ContactPhoneNumberEntry.Text,
                Position = (Position)Enum.Parse(typeof(Position), PositionPicker.SelectedItem.ToString()),
                AvailablePTODays = 0,
                AvailableSickDays = 10,
                Shift = (Schedule)Enum.Parse(typeof(Schedule), SchedulePicker.SelectedItem.ToString()),
                IsActive = true // Assuming new employees are always active
            };

            // Save the new employee to the database
            employeeManager.SaveEmployee(newEmployee);

            double salary = 0;

            if (newEmployee.Position.ToString().ToLower() == "manager")
            {
                salary = 25;
            }
            else
            {
                salary = 15;
            }
            Payment newPayment = new Payment
            {
                EmployeeID = newEmployee.Id,
                Salary = salary,
                TotalHours = 0,
                HoursWorkedThisWeek = 0,
                OvertimeHoursWorkedThisWeek = 0,
                Performance = 0
            };

            //Save the new payment to the database
            employeeManager.SavePayment(newPayment);

        // Clear the form after adding the employee
        ClearForm();
            DisplayAlert("Confirmation", "Employee has been created", "OK");
        }
        else
        {
            DisplayAlert("Error", "Please fill out all fields correctly.", "OK");
        }
        PopulateEmployeePicker();
    }

    private void UpdateExistingEmployee_Clicked(object sender, EventArgs e)
    {
        if (ValidateEmployeeInformation())
        {
            // Retrieve the existing employee from the database based on Id
            int employeeId = int.Parse(EmployeeID.Text); // Get the employee Id from somewhere (e.g., a selected item)
            Employee existingEmployee = GetEmployeeFromDatabase(employeeId);

            // Update the existing employee with the new information
            existingEmployee.Email = EmailEntry.Text;
            existingEmployee.Password = PasswordEntry.Text;
            existingEmployee.Name = NameEntry.Text;
            existingEmployee.PhoneNumber = PhoneNumberEntry.Text;
            existingEmployee.Address = AddressEntry.Text;
            existingEmployee.EContactName = ContactNameEntry.Text;
            existingEmployee.EContactPhone = ContactPhoneNumberEntry.Text;
            existingEmployee.Position = (Position)Enum.Parse(typeof(Position), PositionPicker.SelectedItem.ToString());
            existingEmployee.AvailablePTODays = 0;
            existingEmployee.AvailableSickDays = 10;
            existingEmployee.Shift = (Schedule)Enum.Parse(typeof(Schedule), SchedulePicker.SelectedItem.ToString());

            // Save the updated employee to the database
            employeeManager.UpdateEmployee(existingEmployee);

            // Clear the form after updating the employee
            ClearForm();
        }
        else
        {
            DisplayAlert("Error", "Please fill out all fields correctly.", "OK");
        }
        PopulateEmployeePicker();
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

    private void ClearForm()
    {
        // Clear all entry fields and reset picker selections
        EmployeeID.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
        NameEntry.Text = string.Empty;
        PhoneNumberEntry.Text = string.Empty;
        AddressEntry.Text = string.Empty;
        ContactNameEntry.Text = string.Empty;
        ContactPhoneNumberEntry.Text = string.Empty;
        PositionPicker.SelectedItem = null;
        SchedulePicker.SelectedItem = null;
    }

    private Employee GetEmployeeFromDatabase(int employeeId)
    {
        // Retrieve the employee from the database based on Id
        // You need to implement this method based on your database access logic
        return employeeManager.GetEmployeeById(employeeId);
    }

    private void SetEmployeeInactive_Clicked(object sender, EventArgs e)
    {
        // Get the selected employee index
        int selectedIndex = EmployeePicker.SelectedIndex;
        if (selectedIndex != -1)
        {
            int employeeId = int.Parse(EmployeeID.Text);
            Employee selectedEmployee = GetEmployeeFromDatabase(employeeId);

            selectedEmployee.IsActive = false;
            selectedEmployee.AvailablePTODays = 0;
            selectedEmployee.AvailableSickDays = 0;

            employeeManager.UpdateEmployee(selectedEmployee);

            PopulateEmployeePicker();
            ClearForm();
        }
    }

    private void DeleteEmployee_Clicked(object sender, EventArgs e)
    {
        // Get the selected employee index
        int selectedIndex = EmployeePicker.SelectedIndex;
        if (selectedIndex != -1)
        {
            // Retrieve the corresponding employee from the database
            int employeeId = int.Parse(EmployeeID.Text); // Get the employee Id from somewhere (e.g., a selected item)
            Employee selectedEmployee = GetEmployeeFromDatabase(employeeId);

            // Confirm deletion
            Dispatcher.Dispatch(async () =>
            {
                bool answer = await DisplayAlert("Confirmation", $"Are you sure you want to delete {selectedEmployee.Name}?", "Yes", "No");
                if (answer)
                {
                    // Delete the employee from the database
                    employeeManager.DeleteEmployee(selectedEmployee);

                    // Refresh the picker and clear the form
                    PopulateEmployeePicker();
                    ClearForm();
                }
            });
        }
    }
}
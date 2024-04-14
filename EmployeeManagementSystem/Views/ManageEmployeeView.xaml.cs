using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem;

public partial class ManageEmployeeView : ContentPage
{
    public ManageEmployeeView()
    {
        InitializeComponent();
    }

    public ManageEmployeeView(User user)
    {
        InitializeComponent();
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
            EmailEntry.Text = selectedEmployee.Email;
            PasswordEntry.Text = selectedEmployee.Password;
            NameEntry.Text = selectedEmployee.Name;
            PhoneNumberEntry.Text = selectedEmployee.PhoneNumber;
            AddressEntry.Text = selectedEmployee.Address;
            ContactNameEntry.Text = selectedEmployee.EContactName;
            ContactPhoneNumberEntry.Text = selectedEmployee.EContactPhone;
            PositionPicker.SelectedItem = selectedEmployee.Position.ToString();
            AvailablePTOEntry.Text = selectedEmployee.AvailablePTODays.ToString();
            AvailableSickDaysEntry.Text = selectedEmployee.AvailableSickDays.ToString();
            SchedulePicker.SelectedItem = selectedEmployee.Shift.ToString();
            SalaryEntry.Text = selectedEmployee.Earnings.Salary.ToString();
            HoursWorkedEntry.Text = selectedEmployee.Earnings.HoursWorked.ToString();
            PerformanceEntry.Text = selectedEmployee.Earnings.Performance.ToString();
        }
    }

    private void AddNewEmployee_Clicked(object sender, EventArgs e)
    {
        if (ValidateEmployeeInformation())
        {
            // Get the next available EmployeeID
            int employeeId = employeeManager.GetNextEmployeeId();

            // Create a new Payment object with the same EmployeeID
            Payment payment = new()
            {
                EmployeeID = employeeId,
                Salary = Convert.ToDecimal(SalaryEntry.Text),
                HoursWorked = Convert.ToInt32(HoursWorkedEntry.Text),
                Performance = Convert.ToInt32(PerformanceEntry.Text)
            };

            // Create a new Employee object
            Employee newEmployee = new Employee
            {
                Id = employeeId,
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
            int employeeId = 0; // Get the employee Id from somewhere (e.g., a selected item)
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
            existingEmployee.AvailablePTODays = Convert.ToInt32(AvailablePTOEntry.Text);
            existingEmployee.AvailableSickDays = Convert.ToInt32(AvailableSickDaysEntry.Text);
            existingEmployee.Shift = (Schedule)Enum.Parse(typeof(Schedule), SchedulePicker.SelectedItem.ToString());
            existingEmployee.Earnings.Salary = Convert.ToDecimal(SalaryEntry.Text);
            existingEmployee.Earnings.HoursWorked = Convert.ToInt32(HoursWorkedEntry.Text);
            existingEmployee.Earnings.Performance = Convert.ToInt32(PerformanceEntry.Text);

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
               SchedulePicker.SelectedItem != null &&
               !string.IsNullOrWhiteSpace(AvailablePTOEntry.Text) &&
               !string.IsNullOrWhiteSpace(AvailableSickDaysEntry.Text) &&
               !string.IsNullOrWhiteSpace(SalaryEntry.Text) &&
               !string.IsNullOrWhiteSpace(HoursWorkedEntry.Text) &&
               !string.IsNullOrWhiteSpace(PerformanceEntry.Text);
    }

    private void ClearForm()
    {
        // Clear all entry fields and reset picker selections
        EmailEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
        NameEntry.Text = string.Empty;
        PhoneNumberEntry.Text = string.Empty;
        AddressEntry.Text = string.Empty;
        ContactNameEntry.Text = string.Empty;
        ContactPhoneNumberEntry.Text = string.Empty;
        PositionPicker.SelectedItem = null;
        SchedulePicker.SelectedItem = null;
        AvailablePTOEntry.Text = string.Empty;
        AvailableSickDaysEntry.Text = string.Empty;
        SalaryEntry.Text = string.Empty;
        HoursWorkedEntry.Text = string.Empty;
        PerformanceEntry.Text = string.Empty;
    }

    private Employee GetEmployeeFromDatabase(int employeeId)
    {
        // Retrieve the employee from the database based on Id
        // You need to implement this method based on your database access logic
        return employeeManager.GetEmployeeById(employeeId);
    }

    private void DeleteEmployee_Clicked(object sender, EventArgs e)
    {
        // Get the selected employee index
        int selectedIndex = EmployeePicker.SelectedIndex;
        if (selectedIndex != -1)
        {
            // Retrieve the corresponding employee from the database
            var selectedEmployeeName = EmployeePicker.Items[selectedIndex];
            var selectedEmployee = employeeManager.GetEmployeeByName(selectedEmployeeName);

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
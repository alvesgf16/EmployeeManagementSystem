using EmployeeManagementSystem;
using EmployeeManagementSystem.Models;
using SQLite;

namespace EMSTest
{
    [TestFixture]
    public class Tests
    {
        EmployeeManagementSystem.Models.Employee employee;
        EmployeeManagementSystem.Models.Payment payment;
        EmployeeManagementSystem.Services.EmployeeService _employeeService;
        EmployeeManagementSystem.Constants _constants;
        EmployeeManagementSystem.Models.Position position;

        [SetUp]
        public void Setup()
        {
            _employeeService = new EmployeeManagementSystem.Services.EmployeeService();
            _constants = new EmployeeManagementSystem.Constants();
            employee = new EmployeeManagementSystem.Models.Employee();
            payment = new EmployeeManagementSystem.Models.Payment();
            position = new EmployeeManagementSystem.Models.Position();

            employee.Id = 1;
            employee.Name = "Test Employee";
            employee.Position = Position.Manager;
            employee.PhoneNumber = "1234567890";
            employee.Email = "test@test.com";
            employee.Address = "123 Test St";
            employee.EContactName = "Test EContact";
            employee.EContactPhone = "0987654321";
            employee.AvailablePTODays = 10;
            employee.AvailableSickDays = 10;


            _employeeService.SaveEmployee(employee);

        }

    }
}
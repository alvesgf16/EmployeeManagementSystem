using SQLite;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private SQLiteConnection _database;


        public EmployeeService()
        {
            _database = new SQLiteConnection(Constants.DatabasePath);
            _database.CreateTable<Employee>();
            _database.CreateTable<Payment>();
        }

        //Employee methods
        public int GetNextEmployeeId()
        {
            var lastEmployee = _database.Table<Employee>().OrderByDescending(e => e.Id).FirstOrDefault();
            return lastEmployee is null ? 1 : lastEmployee.Id + 1;
        }

        public void SaveEmployee(Employee employee) => _database.Insert(employee);

        public void DeleteEmployee(Employee employee) => _database.Delete(employee);

        public void UpdateEmployee(Employee employee) => _database.Update(employee);

        public Employee GetEmployeeById(int employeeId) => _database.Find<Employee>(employeeId);

        public List<Employee> GetAllEmployees() => [.. _database.Table<Employee>()];

        public Employee GetEmployeeByName(string name) => _database.Table<Employee>().FirstOrDefault(e => e.Name == name);

        //Payment methods
        public void UpdatePayment(Payment payment) => _database.Update(payment);
        public void SavePayment(Payment payment) => _database.Insert(payment);
        public Payment GetEmployeePay(int employeeId) => _database.Table<Payment>().FirstOrDefault(e => e.EmployeeID == employeeId);

        public void UpdateEmployeePay(Payment selectedPayment) => _database.Update(selectedPayment);

        public void SetAllPaymentsToZero() => _database.Table<Payment>().ToList().ForEach(p => { p.TotalHours = 0; p.RegHours = 0; p.OverTimeHours = 0; p.Performance = 0; });
    }
}

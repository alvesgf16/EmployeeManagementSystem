using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : BaseService
    {
        public List<Employee> GetAllEmployees() => [.. _database.Table<Employee>()];
        
        public Employee GetEmployeeById(int employeeId) => _database.Find<Employee>(employeeId);
        
        public void SaveEmployee(Employee employee) => _database.Insert(employee);
        
        public void UpdateEmployee(Employee employee) => _database.Update(employee);
        
        public void DeleteEmployee(Employee employee) => _database.Delete(employee);
        
        public int GetNextEmployeeId()
        {
            var lastEmployee = _database.Table<Employee>().OrderByDescending(e => e.Id).FirstOrDefault();
            return lastEmployee is null ? 1 : lastEmployee.Id + 1;
        }

        public Employee GetEmployeeByName(string name) => _database.Table<Employee>().FirstOrDefault(e => e.Name == name);
    }
}

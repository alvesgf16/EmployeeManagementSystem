using SQLite;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private SQLiteConnection database;


        public EmployeeService()
        {
            this.database = new SQLiteConnection(Constants.DatabasePath);

            database.CreateTable<Employee>();
            database.CreateTable<Payment>();
        }

        public int GetNextEmployeeId()
        {
            var lastEmployee = database.Table<Employee>().OrderByDescending(e => e.Id).FirstOrDefault();
            return lastEmployee is null ? 1 : lastEmployee.Id + 1;
        }

        public void SaveEmployee(Employee employee) => database.Insert(employee);

        public void DeleteEmployee(Employee employee) => database.Delete(employee);

        public void UpdateEmployee(Employee employee) => database.Update(employee);

        public void UpdatePayment(Payment payment) => database.Update(payment);

        public Employee GetEmployeeById(int employeeId) => database.Table<Employee>().FirstOrDefault(e => e.Id == employeeId);

        public List<Employee> GetAllEmployees() => [.. database.Table<Employee>()];

        public Employee GetEmployeeByName(string name) => database.Table<Employee>().FirstOrDefault(e => e.Name == name);

        public void SavePayment(Payment payment) => database.Insert(payment);

        public Payment GetEmployeePay(int employeeId) => database.Table<Payment>().FirstOrDefault(e => e.EmployeeID == employeeId);
    }
}

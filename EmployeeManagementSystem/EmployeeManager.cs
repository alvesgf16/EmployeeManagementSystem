using SQLite;

namespace EmployeeManagementSystem
{
    public class EmployeeManager
    {
        private SQLiteConnection database;


        public EmployeeManager()
        {
            this.database = new SQLiteConnection(Constants.DatabasePath);

            this.database.CreateTable<Employee>();
            this.database.CreateTable<EmergencyContact>();
            this.database.CreateTable<Payment>();
        }

        public int GetNextEmployeeId()
        {
            var lastEmployee = database.Table<Employee>().OrderByDescending(e => e.Id).FirstOrDefault();
            if (lastEmployee != null)
            {
                return lastEmployee.Id + 1;
            }
            else
            {
                return 1;
            }
        }

        public void SaveEmployee(Employee employee)
        {
            database.Insert(employee);
        }

        public void DeleteEmployee(Employee employee) 
        {  
            database.Delete(employee); 
        }

        public void UpdateEmployee(Employee employee)
        {
            database.Update(employee);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return database.Table<Employee>().FirstOrDefault(e => e.Id == employeeId);
        }

        public List<Employee> GetAllEmployees()
        {
            return database.Table<Employee>().ToList();
        }

        public Employee GetEmployeeByName(string name)
        {
            return database.Table<Employee>().FirstOrDefault(e => e.Name == name);
        }
    }
}

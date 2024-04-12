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

    }
}

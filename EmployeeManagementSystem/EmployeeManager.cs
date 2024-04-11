using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.database.CreateTable<Employee.EmergencyContact>();
            this.database.CreateTable<Employee.Payment>();
        }

    }
}

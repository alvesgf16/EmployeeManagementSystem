﻿using SQLite;

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

        public int GetNextEmployeeId()
        {
            var lastEmployee = _database.Table<Employee>().OrderByDescending(e => e.Id).FirstOrDefault();
            return lastEmployee is null ? 1 : lastEmployee.Id + 1;
        }

        public void SaveEmployee(Employee employee) => _database.Insert(employee);

        public void DeleteEmployee(Employee employee) => _database.Delete(employee);

        public void UpdateEmployee(Employee employee) => _database.Update(employee);

        public void UpdatePayment(Payment payment) => _database.Update(payment);

        public Employee GetEmployeeById(int employeeId) => _database.Table<Employee>().FirstOrDefault(e => e.Id == employeeId);

        public List<Employee> GetAllEmployees() => [.. _database.Table<Employee>()];

        public Employee GetEmployeeByName(string name) => _database.Table<Employee>().FirstOrDefault(e => e.Name == name);

        public void SavePayment(Payment payment) => _database.Insert(payment);

        public Payment GetEmployeePay(int employeeId) => _database.Table<Payment>().FirstOrDefault(e => e.EmployeeID == employeeId);
    }
}

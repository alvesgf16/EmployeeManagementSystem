using EmployeeManagementSystem.Models;
using SQLite;

namespace EmployeeManagementSystem.Services;

public abstract class BaseService
{
    protected readonly SQLiteConnection _database;

    public BaseService()
    {
        _database = new SQLiteConnection(Constants.DatabasePath);
        _database.CreateTable<Employee>();
        _database.CreateTable<Payment>();
        _database.CreateTable<Schedule>();
        _database.CreateTable<SickDayRequest>();
        _database.CreateTable<PTORequest>();
    }
}

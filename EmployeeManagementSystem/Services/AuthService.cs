using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Exceptions;
using SQLite;

namespace EmployeeManagementSystem.Services;

class AuthService
{
    private readonly SQLiteConnection _database;

    public AuthService()
    {
        _database = new SQLiteConnection(Constants.DatabasePath);
    }

    public int AuthenticateUserLogin(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) throw new InvalidLoginException("Invalid email or password");

        var userToAuth = _database.Table<Employee>().FirstOrDefault((employee) => employee.Email == email && employee.Password == password);

        return userToAuth is null ? throw new InvalidLoginException("Invalid email or password") : userToAuth.Id;
    }
}

using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Exceptions;

namespace EmployeeManagementSystem.Services;

internal class AuthService : BaseService
{
    public Employee AuthenticateUserLogin(string? email, string? password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) throw new InvalidLoginException("Invalid email or password");

        var userToAuth = _database.Table<Employee>().FirstOrDefault((employee) => employee.Email == email && employee.Password == password);

        return userToAuth is null ? throw new InvalidLoginException("Invalid email or password") : userToAuth;
    }
}

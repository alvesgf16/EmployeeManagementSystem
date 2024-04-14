using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Exceptions;
using SQLite;

namespace EmployeeManagementSystem.Services;

class AuthService
{
    private readonly SQLiteConnection database;

    public AuthService()
    {

        database = new SQLiteConnection(Constants.DatabasePath);
        database.CreateTable<User>();
    }

    public List<User> GetUsers() => database.Table<User>().ToList();

    public User AuthenticateUserLogin(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) throw new InvalidLoginException("Invalid username or password");

        var userToAuth = database.Find<User>(username);

        return userToAuth is not null && userToAuth.Password == password
            ? userToAuth
            : throw new InvalidLoginException("Invalid username or password");
    }

    public void AddUser(string username, string password) => database.Insert(new User() { Username = username, Password = password });

    public void DeleteUser(string username) => database.Delete<User>(username);
}

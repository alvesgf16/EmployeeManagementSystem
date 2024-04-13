using EmployeeManagementSystem.Models;
using SQLite;


namespace EmployeeManagementSystem
{
    class AuthService
    {
        private SQLiteConnection database;

        public AuthService()
        {
            this.database = new SQLiteConnection(Constants.DatabasePath);
            this.database.CreateTable<User>();
        }

        public TableQuery<User> GetUsers()
        {
            return database.Table<User>();
        }

        public bool AuthenticateUserLogin(string username, string password, TableQuery<User> users)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            else if(users.Where(u => u.Username == username && u.Password == password).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddUser(string username, string password)
        {
            User user = new User() { Password = password, Username = username };
            database.Insert(user);
        }

        public void DeleteUser(string username)
        {
            database.Delete<User>(username);
        }
    }
}

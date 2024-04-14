using SQLite;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models;

public class User
{
    [PrimaryKey]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

    public User()
    {
    }
}

using SQLite;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    class User
    {
        [PrimaryKey]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public User()
        {
        }
    }
}

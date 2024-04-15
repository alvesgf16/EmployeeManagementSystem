using SQLite;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem;

public class Payment()
{
    public int EmployeeID { get; set; }

    public double Salary { get; set; }

    public int HoursWorked { get; set; }

    public int Performance { get; set; }
}
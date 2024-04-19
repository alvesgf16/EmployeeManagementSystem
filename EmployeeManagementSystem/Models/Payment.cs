using SQLite;

namespace EmployeeManagementSystem.Models;

public class Payment()
{
    [PrimaryKey]
    public int EmployeeID { get; set; }

    public double Salary { get; set; }

    public double TotalHours { get; set; }

    public double RegHours { get; set; }

    public double OverTimeHours { get; set; }

    public int Performance { get; set; }
}
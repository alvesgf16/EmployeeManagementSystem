using SQLite;

namespace EmployeeManagementSystem.Models;

// Comments
/*
    Dominic Goncalves, April 20th, 2024.
    Payment Class.

    Linked to Employee class via EmployeeID.
    Payment class holds employee pay and pay calculation information, such as salary and hours worked.
 */

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
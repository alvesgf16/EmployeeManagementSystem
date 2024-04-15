using SQLite;

namespace EmployeeManagementSystem.Models;

public class Payment()
{
    [PrimaryKey]
    public int EmployeeID { get; set; }

    public double Salary { get; set; }

    public double TotalHours { get; set; }

    public double HoursWorkedThisWeek { get; set; }

    public double OvertimeHoursWorkedThisWeek { get; set; }

    public double Performance { get; set; }
}
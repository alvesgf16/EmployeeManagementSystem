namespace EmployeeManagementSystem;

public class Payment(int EmployeeID, decimal salary, int hoursWorked, int performance)
{
    public int EmployeeID { get; set; } = EmployeeID;
    
    public decimal Salary { get; set; } = salary;
    
    public int HoursWorked { get; set; } = hoursWorked;

    public int Performance { get; set; } = performance;
}

using SQLite;
namespace EmployeeManagementSystem;

public partial class Employee
{
    
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [NotNull]
    public string Email { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }
    [Ignore]
    public EmergencyContact? Contact { get; set; }
    [Ignore]
    public Position Position { get; set; }

    public int AvailablePTODays { get; set; }

    public int AvailableSickDays { get; set; }
    [Ignore]
    public Schedule Shift { get; set; }

    public bool IsActive { get; set; }
    [Ignore]
    public Payment Earnings { get; set; }

    public Employee()
    {
    }
}

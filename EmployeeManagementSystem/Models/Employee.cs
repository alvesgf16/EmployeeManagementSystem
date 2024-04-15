using SQLite;
namespace EmployeeManagementSystem;

public partial class Employee()
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Email { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string EContactName { get; set; }

    public string EContactPhone { get; set; }

    public Position Position { get; set; }

    public int AvailablePTODays { get; set; }

    public int AvailableSickDays { get; set; }

    public Schedule Shift { get; set; }

    public bool IsActive { get; set; }

    //override toString method
    public override string ToString() => $"ID: {Id} Name: {Name} Position: {Position} Shift: {Shift}";
}

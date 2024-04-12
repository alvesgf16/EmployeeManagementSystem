namespace EmployeeManagementSystem;

public class EmergencyContact(int EmployeeID, string contactName, string phoneNumber)
{
    public int EmployeeID { get; set; } = EmployeeID;
    public string ContactName { get; set; } = contactName;
    public string PhoneNumber { get; set; } = phoneNumber;
}

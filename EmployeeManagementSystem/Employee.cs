namespace EmployeeManagementSystem
{
    public partial class Employee(int id,
                    string email,
                    string password,
                    string name,
                    string phoneNumber,
                    string address,
                    Position position,
                    Schedule shift,
                    bool isActive)
    {

        public int Id { get; set; } = id;

        public string Email { get; set; } = email;

        public string Password { get; set; } = password;

        public string Name { get; set; } = name;

        public string PhoneNumber { get; set; } = phoneNumber;

        public string Address { get; set; } = address;

        public EmergencyContact? Contact { get; set; } = null;

        public Position Position { get; set; } = position;

        public int AvailablePTODays { get; set; } = 0;

        public int AvailableSickDays { get; set; } = 10;

        public Schedule Shift { get; set; } = shift;

        public bool IsActive { get; set; } = isActive;

        public Payment Earnings { get; set; } = new(id, 15, 0, 1);
    }
}

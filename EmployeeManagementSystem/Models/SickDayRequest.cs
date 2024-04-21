using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EmployeeManagementSystem.Models
{
    public class SickDayRequest
    {
        [PrimaryKey, AutoIncrement]
        public int SickDayRequestId { get; set; }
        [ForeignKey(typeof(Employee))]
        public int EmployeeID { get; set; }
        public DateTime RequestedDate { get; set; }
        public bool Approved { get; set; }
    }
}

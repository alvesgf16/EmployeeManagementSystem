using SQLite;
using SQLiteNetExtensions.Attributes;

// Comments
/*
    Dominic Goncalves, April 20th, 2024.
    PTORequest Class.

    Declares PTORequest class.
    This class is utilized in order for employees to request time off work, and for managers to view the requests, and approve or deny them.
 */

namespace EmployeeManagementSystem.Models
{
    public class PTORequest
    {
        [PrimaryKey, AutoIncrement]
        public int PTORequestId { get; set; }

        [ForeignKey(typeof(Employee))]
        public int EmployeeID { get; set; }

        public DateTime RequestedDate { get; set; }

        public bool Approved { get; set; }

        public override string ToString()
        {
            return $"PTORequestId: {PTORequestId}, EmployeeID: {EmployeeID}, RequestedDate: {RequestedDate.Date}, Approved: {Approved}";
        }
    }
}
